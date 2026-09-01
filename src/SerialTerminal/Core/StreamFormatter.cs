using System;
using System.Text;

namespace SerialTerminal.Core
{
    /// <summary>
    /// Turns a stream of byte blocks into displayable text.
    ///
    /// It is stateful on purpose:
    ///  * Text mode keeps a Decoder so a multi byte character split across two
    ///    reads (very common at 9600 baud with UTF-8 or Big5) is not mangled.
    ///  * Hex mode keeps the current column so the 16 bytes per line grid survives
    ///    across blocks.
    ///  * CR/LF normalisation keeps a "swallow the next LF" flag across blocks.
    /// </summary>
    public sealed class StreamFormatter
    {
        private Encoding _Encoding = System.Text.Encoding.Latin1;
        private Decoder _Decoder = System.Text.Encoding.Latin1.GetDecoder();
        private int _HexColumn;
        private bool _SwallowNextLf;
        private bool _AtLineStart = true;
        private Direction _LastDirection = Direction.None;
        private DateTime _LastTime = DateTime.MinValue;

        public DisplayMode Mode { get; set; }
        public bool ShowTimestamp { get; set; }
        public int HexBytesPerLine { get; set; }
        public TimeSpan GapThreshold { get; set; }

        public StreamFormatter()
        {
            Mode = DisplayMode.Text;
            ShowTimestamp = false;
            HexBytesPerLine = 16;
            GapThreshold = TimeSpan.FromMilliseconds(300);
        }

        public Encoding Encoding
        {
            get { return _Encoding; }
            set
            {
                _Encoding = value ?? System.Text.Encoding.Latin1;
                _Decoder = _Encoding.GetDecoder();
            }
        }

        /// <summary>Forget all partial state (call after Clear, or on mode change).</summary>
        public void Reset()
        {
            _Decoder = _Encoding.GetDecoder();
            _HexColumn = 0;
            _SwallowNextLf = false;
            _AtLineStart = true;
            _LastDirection = Direction.None;
            _LastTime = DateTime.MinValue;
        }

        public string Format(LogChunk chunk)
        {
            StringBuilder sb = new StringBuilder();

            if (chunk.Direction == Direction.Info)
            {
                if (!_AtLineStart)
                {
                    sb.Append('\n');
                }
                sb.Append("*** ").Append(chunk.Text).Append('\n');
                _AtLineStart = true;
                _HexColumn = 0;
                _LastDirection = Direction.Info;
                _LastTime = chunk.Time;
                return sb.ToString();
            }

            bool directionChanged = chunk.Direction != _LastDirection;
            bool longGap = (chunk.Time - _LastTime) > GapThreshold;

            if (ShowTimestamp && (directionChanged || longGap))
            {
                if (!_AtLineStart)
                {
                    sb.Append('\n');
                    _AtLineStart = true;
                }
                sb.Append('[')
                  .Append(chunk.Time.ToString("HH:mm:ss.fff"))
                  .Append(' ')
                  .Append(chunk.Direction == Direction.Tx ? "TX" : "RX")
                  .Append("] ");
                _HexColumn = 0;
                _AtLineStart = false;
            }
            else if (directionChanged && !_AtLineStart)
            {
                // Without timestamps, still keep TX and RX on separate lines so the
                // colours are readable.
                sb.Append('\n');
                _AtLineStart = true;
                _HexColumn = 0;
            }

            if (Mode == DisplayMode.Hex)
            {
                AppendHex(sb, chunk.Data);
            }
            else
            {
                AppendText(sb, chunk.Data);
            }

            _LastDirection = chunk.Direction;
            _LastTime = chunk.Time;

            if (sb.Length > 0)
            {
                _AtLineStart = sb[sb.Length - 1] == '\n';
            }

            return sb.ToString();
        }

        private void AppendHex(StringBuilder sb, byte[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                byte b = data[i];
                sb.Append(ToHexNibble(b >> 4));
                sb.Append(ToHexNibble(b & 0x0F));
                _HexColumn++;

                if (HexBytesPerLine > 0 && _HexColumn >= HexBytesPerLine)
                {
                    sb.Append('\n');
                    _HexColumn = 0;
                }
                else
                {
                    sb.Append(' ');
                }
            }
        }

        private static char ToHexNibble(int v)
        {
            return (char)(v < 10 ? ('0' + v) : ('A' + (v - 10)));
        }

        private void AppendText(StringBuilder sb, byte[] data)
        {
            int charCount = _Decoder.GetCharCount(data, 0, data.Length, false);
            if (charCount <= 0)
            {
                return;   // incomplete multi byte sequence, wait for the next block
            }

            char[] chars = new char[charCount];
            int produced = _Decoder.GetChars(data, 0, data.Length, chars, 0, false);

            for (int i = 0; i < produced; i++)
            {
                char c = chars[i];

                if (c == '\r')
                {
                    sb.Append('\n');
                    _SwallowNextLf = true;
                    continue;
                }

                if (c == '\n')
                {
                    if (_SwallowNextLf)
                    {
                        _SwallowNextLf = false;   // this LF belonged to a CRLF pair
                        continue;
                    }
                    sb.Append('\n');
                    continue;
                }

                _SwallowNextLf = false;

                if (c == '\t')
                {
                    sb.Append('\t');
                    continue;
                }

                // Other C0/C1 control characters are shown as a dot so a stray 0x00
                // does not truncate the display.
                if (c < 0x20 || c == 0x7F)
                {
                    sb.Append('.');
                    continue;
                }

                sb.Append(c);
            }
        }
    }
}
