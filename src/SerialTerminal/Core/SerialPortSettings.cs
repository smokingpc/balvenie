using System.IO.Ports;

namespace SerialTerminal.Core
{
    /// <summary>Plain snapshot of everything the UI can configure on the port.</summary>
    public sealed class SerialPortSettings
    {
        public string _PortName = "COM1";
        public int _BaudRate = 115200;
        public int _DataBits = 8;
        public Parity _Parity = Parity.None;
        public StopBits _StopBits = StopBits.One;
        public Handshake _Handshake = Handshake.None;

        // Many USB-UART bridges and MCU boards only transmit while DTR/RTS are asserted.
        public bool _DtrEnable = true;
        public bool _RtsEnable = true;

        public int _ReadBufferSize = 1 << 16;
        public int _WriteBufferSize = 1 << 16;
        public int _ReadWriteTimeoutMs = 5000;

        public override string ToString()
        {
            return string.Format("{0} {1},{2},{3},{4} flow={5}",
                _PortName, _BaudRate, _DataBits, _Parity, _StopBits, _Handshake);
        }
    }
}
