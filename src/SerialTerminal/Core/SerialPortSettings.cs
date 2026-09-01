using System.IO.Ports;

namespace SerialTerminal.Core
{
    /// <summary>Plain snapshot of everything the UI can configure on the port.</summary>
    public sealed class SerialPortSettings
    {
        public string PortName = "COM1";
        public int BaudRate = 115200;
        public int DataBits = 8;
        public Parity Parity = Parity.None;
        public StopBits StopBits = StopBits.One;
        public Handshake Handshake = Handshake.None;

        // Many USB-UART bridges and MCU boards only transmit while DTR/RTS are asserted.
        public bool DtrEnable = true;
        public bool RtsEnable = true;

        public int ReadBufferSize = 1 << 16;
        public int WriteBufferSize = 1 << 16;
        public int WriteTimeoutMs = 3000;

        public override string ToString()
        {
            return string.Format("{0} {1},{2},{3},{4} flow={5}",
                PortName, BaudRate, DataBits, Parity, StopBits, Handshake);
        }
    }
}
