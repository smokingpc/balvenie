using System;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace SerialTerminal.Core
{
    /// <summary>
    /// Full duplex serial engine.
    ///
    /// Design notes
    /// ------------
    /// * RX and TX are two independent long running tasks working on
    ///   SerialPort.BaseStream. SerialStream keeps separate read and write locks,
    ///   so a blocked read never blocks a write and vice versa -- that is what
    ///   makes this genuinely full duplex.
    /// * SerialPort.DataReceived is deliberately NOT used: it is fired on a shared
    ///   thread pool thread, it coalesces, and it is the source of most of the
    ///   classic "Close() deadlocks / ObjectDisposedException" reports.
    /// * TX goes through an unbounded Channel so the UI thread never blocks on a
    ///   write, which matters when hardware flow control (RTS/CTS) is enabled and
    ///   the peer de-asserts CTS.
    /// * On Windows the CancellationToken passed to SerialStream.ReadAsync is not
    ///   honoured for a read that is already pending. The only reliable way to
    ///   unblock it is to close the port, so CloseAsync() disposes the port from a
    ///   worker thread and then waits for the loops with a timeout.
    ///
    /// All events are raised on background threads. The UI layer is responsible for
    /// marshalling to the UI thread (this class stays UI framework agnostic).
    /// </summary>
    public sealed class SerialSession : IDisposable
    {
        private const int StreamReadSize = 1024;
        private const int ReadBufferSize = 4096 + StreamReadSize;

        private SerialPort _Port;
        private CancellationTokenSource _Cts;
        private Channel<byte[]> _TxChannel;
        private Task _ReadTask;
        private Task _WriteTask;
        private int _Closing;

        private long _RxBytes;
        private long _TxBytes;

        /// <summary>Raised for every block of bytes read from the port.</summary>
        public event EventHandler<LogChunk> DataReceived;

        /// <summary>Raised after a block of bytes has actually been handed to the driver.</summary>
        public event EventHandler<LogChunk> DataSent;

        /// <summary>Non fatal or fatal problem, already formatted for display.</summary>
        public event EventHandler<string> Error;

        /// <summary>Port is no longer usable (closed by user, or device disappeared).</summary>
        public event EventHandler<bool> Closed;   // bool: true = unexpected

        public bool IsOpen
        {
            get
            {
                SerialPort port = _Port;
                return port != null && port.IsOpen;
            }
        }

        public long RxBytes { get { return Interlocked.Read(ref _RxBytes); } }
        public long TxBytes { get { return Interlocked.Read(ref _TxBytes); } }

        public SerialPortSettings CurrentSettings { get; private set; }

        public void Open(SerialPortSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException("settings");
            }
            if (IsOpen)
            {
                throw new InvalidOperationException("port is already open");
            }

            SerialPort port = new SerialPort(
                settings._PortName,
                settings._BaudRate,
                settings._Parity,
                settings._DataBits,
                settings._StopBits);

            port.Handshake = settings._Handshake;
            port.DtrEnable = settings._DtrEnable;
            port.RtsEnable = settings._RtsEnable;
            port.ReadBufferSize = settings._ReadBufferSize;
            port.WriteBufferSize = settings._WriteBufferSize;
            port.WriteTimeout = settings._ReadWriteTimeoutMs;
            port.ReadTimeout = settings._ReadWriteTimeoutMs;

            // Throws UnauthorizedAccessException when the port is taken by another
            // process, IOException when the device vanished, ArgumentException on a
            // bad port name. The caller reports these to the user.
            port.Open();

            try {
                port.DiscardInBuffer();
                port.DiscardOutBuffer();
            }
            catch (Exception) {
                // Some virtual COM drivers do not implement discard; harmless.
            }

            _Port = port;
            _Closing = 0;
            Interlocked.Exchange(ref _RxBytes, 0);
            Interlocked.Exchange(ref _TxBytes, 0);
            CurrentSettings = settings;

            _Cts = new CancellationTokenSource();
            _TxChannel = Channel.CreateUnbounded<byte[]>(new UnboundedChannelOptions
            {
                SingleReader = true,
                SingleWriter = false,
                AllowSynchronousContinuations = false
            });

            CancellationToken token = _Cts.Token;
            _ReadTask = Task.Run(function: () => SerialPortReadThread(port, token));
            _WriteTask = Task.Run(function: () => SerialPortWriteThread(port, token));
        }

        /// <summary>Queues bytes for transmission. Never blocks the caller.</summary>
        public bool Send(byte[] data)
        {
            if (data == null || data.Length == 0)
            {
                return false;
            }

            Channel<byte[]> channel = _TxChannel;
            if (channel == null || !IsOpen)
            {
                return false;
            }

            return channel.Writer.TryWrite(data);
        }

        public async Task CloseAsync()
        {
            if (Interlocked.Exchange(ref _Closing, 1) == 1)
            {
                return;
            }

            SerialPort port = _Port;
            CancellationTokenSource cts = _Cts;
            Channel<byte[]> channel = _TxChannel;
            Task readTask = _ReadTask;
            Task writeTask = _WriteTask;

            _Port = null;

            if (channel != null)
            {
                channel.Writer.TryComplete();
            }
            if (cts != null)
            {
                try { cts.Cancel(); } catch (ObjectDisposedException) { }
            }

            if (port != null)
            {
                // Dispose off the UI thread: this is what releases a pending ReadAsync,
                // and on a yanked USB-UART it can take a moment.
                Task disposeTask = Task.Run(() =>
                {
                    try { port.Close(); }
                    catch (Exception) { }
                    try { port.Dispose(); }
                    catch (Exception) { }
                });
                await Task.WhenAny(disposeTask, Task.Delay(3000)).ConfigureAwait(false);
            }

            Task loops = Task.WhenAll(
                readTask ?? Task.CompletedTask,
                writeTask ?? Task.CompletedTask);
            await Task.WhenAny(loops, Task.Delay(2000)).ConfigureAwait(false);

            if (cts != null)
            {
                try { cts.Dispose(); } catch (Exception) { }
            }

            _Cts = null;
            _TxChannel = null;
            _ReadTask = null;
            _WriteTask = null;

            RaiseClosed(false);
        }

        private async Task SerialPortReadThread(SerialPort port, CancellationToken token)
        {
            byte[] buffer = new byte[ReadBufferSize];
            int offset = 0;
            Stream stream;

            try
            {
                stream = port.BaseStream;
            }
            catch (Exception)
            {
                //if port not opened, BaseStream throws InvalidOperationException.
                //This can happen if the port is closed before the read thread starts.
                return;
            }

            try
            {
                while (!token.IsCancellationRequested)
                {
                    int read_size = 0;
                    try 
                    {
                        read_size = stream.Read(buffer, offset, StreamReadSize);
                    }
                    catch (TimeoutException ex)
                    {
                        // Read timeout is normal, just continue the loop to read again.
                        // To prevent a busy loop, add a small sleep here.
                        await Task.Delay(10, token).ConfigureAwait(false);
                    }

                    if (read_size > 0) {
                        if (1 == read_size && buffer[0] == 0)
                        {
                            continue;
                        }
                        byte[] chunk = new byte[read_size];
                        Buffer.BlockCopy(buffer, offset, chunk, 0, read_size);
                        Interlocked.Add(ref _RxBytes, read_size);
                        if ((offset + read_size) > (ReadBufferSize - StreamReadSize)) 
                        {
                            offset = 0;
                            Array.Clear(buffer);
                        }
                        else 
                        { 
                            offset += read_size;
                        }

                        EventHandler<LogChunk> handler = DataReceived;
                        if (handler != null) {
                            handler(this, new LogChunk(Direction.Rx, chunk));
                        }
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // normal shutdown
            }
            catch (ObjectDisposedException)
            {
                // normal shutdown (port disposed to unblock this read)
            }
            catch (InvalidOperationException)
            {
                // port closed underneath us
            }
            catch (Exception ex)
            {
                // IOException / UnauthorizedAccessException: cable pulled, driver removed.
                HandleFatal("RX: " + ex.Message);
            }
        }

        private async Task SerialPortWriteThread(SerialPort port, CancellationToken token)
        {
            Channel<byte[]> channel = _TxChannel;
            Stream stream;

            try
            {
                stream = port.BaseStream;
            }
            catch (Exception)
            {
                return;
            }

            try
            {
                while (!token.IsCancellationRequested) 
                {
                    byte[] data;
                    channel.Reader.TryRead(out data);
                    if (data == null) 
                    {
                        await Task.Delay(10, token).ConfigureAwait(false);
                        continue;
                    }
                    stream.Write(data, 0, data.Length);
                    stream.Flush();
                    Interlocked.Add(ref _TxBytes, data.Length);

                    EventHandler<LogChunk> handler = DataSent;
                    if (handler != null) {
                        handler(this, new LogChunk(Direction.Tx, data));
                    }
                }
            }
            catch (OperationCanceledException)
            {
            }
            catch (ObjectDisposedException)
            {
            }
            catch (InvalidOperationException)
            {
            }
            catch (TimeoutException)
            {
                // WriteTimeout elapsed: almost always hardware flow control with CTS low.
                RaiseError("TX timeout: peer is not asserting CTS, or the cable is wrong.");
            }
            catch (Exception ex)
            {
                HandleFatal("TX: " + ex.Message);
            }
        }

        /// <summary>Called from a read/write loop when the device is gone.</summary>
        private void HandleFatal(string message)
        {
            if (Interlocked.Exchange(ref _Closing, 1) == 1)
            {
                return;
            }

            RaiseError(message);

            SerialPort port = _Port;
            _Port = null;
            if (port != null)
            {
                Task.Run(() =>
                {
                    try { port.Close(); } catch (Exception) { }
                    try { port.Dispose(); } catch (Exception) { }
                });
            }

            Channel<byte[]> channel = _TxChannel;
            if (channel != null)
            {
                channel.Writer.TryComplete();
            }

            CancellationTokenSource cts = _Cts;
            if (cts != null)
            {
                try { cts.Cancel(); } catch (Exception) { }
            }

            RaiseClosed(true);
        }

        public bool TryReadSignals(out bool cts, out bool dsr, out bool cd)
        {
            cts = false;
            dsr = false;
            cd = false;

            SerialPort port = _Port;
            if (port == null || !port.IsOpen)
            {
                return false;
            }

            try
            {
                cts = port.CtsHolding;
                dsr = port.DsrHolding;
                cd = port.CDHolding;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void SetDtr(bool value)
        {
            SerialPort port = _Port;
            if (port != null && port.IsOpen)
            {
                try { port.DtrEnable = value; } catch (Exception ex) { RaiseError(ex.Message); }
            }
        }

        public void SetRts(bool value)
        {
            SerialPort port = _Port;
            if (port != null && port.IsOpen)
            {
                try { port.RtsEnable = value; } catch (Exception ex) { RaiseError(ex.Message); }
            }
        }

        private void RaiseError(string message)
        {
            EventHandler<string> handler = Error;
            if (handler != null)
            {
                handler(this, message);
            }
        }

        private void RaiseClosed(bool unexpected)
        {
            EventHandler<bool> handler = Closed;
            if (handler != null)
            {
                handler(this, unexpected);
            }
        }

        public void Dispose()
        {
            try
            {
                CloseAsync().GetAwaiter().GetResult();
            }
            catch (Exception)
            {
            }
        }
    }
}
