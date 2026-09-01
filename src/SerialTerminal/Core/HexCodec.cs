using System;
using System.Collections.Generic;
using System.Text;

namespace SerialTerminal.Core
{
    /// <summary>
    /// Tolerant HEX string parser / formatter.
    /// Accepted input examples (all equivalent):
    ///   "01 02 0A"   "0102 0A"   "01,02,0A"   "0x01 0x02 0x0A"   "01-02-0A"
    /// Separators that are ignored: whitespace, ',', ';', ':', '-', '_', '|'.
    /// A leading "0x" / "0X" of a byte is dropped.
    /// </summary>
    public static class HexCodec
    {
        private static readonly char[] _HexDigits = "0123456789ABCDEF".ToCharArray();

        public static bool TryParse(string text, out byte[] data, out string error)
        {
            data = Array.Empty<byte>();
            error = string.Empty;

            if (string.IsNullOrWhiteSpace(text))
            {
                error = "empty input";
                return false;
            }

            List<char> digits = new List<char>(text.Length);
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];

                if (char.IsWhiteSpace(c) || c == ',' || c == ';' || c == ':' ||
                    c == '-' || c == '_' || c == '|')
                {
                    continue;
                }

                // "0x" / "0X" prefix: the '0' has already been pushed, remove it again.
                if ((c == 'x' || c == 'X') && digits.Count > 0 && digits[digits.Count - 1] == '0')
                {
                    digits.RemoveAt(digits.Count - 1);
                    continue;
                }

                if (Uri.IsHexDigit(c))
                {
                    digits.Add(c);
                    continue;
                }

                error = string.Format("invalid HEX character '{0}' at position {1}", c, i);
                return false;
            }

            if (digits.Count == 0)
            {
                error = "no HEX digit found";
                return false;
            }

            if ((digits.Count & 1) != 0)
            {
                error = string.Format("odd number of HEX digits ({0}); every byte needs 2 digits", digits.Count);
                return false;
            }

            byte[] result = new byte[digits.Count / 2];
            for (int i = 0; i < result.Length; i++)
            {
                int hi = Uri.FromHex(digits[i * 2]);
                int lo = Uri.FromHex(digits[i * 2 + 1]);
                result[i] = (byte)((hi << 4) | lo);
            }

            data = result;
            return true;
        }

        public static string ToHexString(byte[] data, int offset, int count, char separator)
        {
            if (data == null || count <= 0)
            {
                return string.Empty;
            }

            StringBuilder sb = new StringBuilder(count * 3);
            for (int i = 0; i < count; i++)
            {
                byte b = data[offset + i];
                sb.Append(_HexDigits[b >> 4]);
                sb.Append(_HexDigits[b & 0x0F]);
                if (separator != '\0')
                {
                    sb.Append(separator);
                }
            }
            return sb.ToString();
        }

        public static string ToHexString(byte[] data)
        {
            return ToHexString(data, 0, data == null ? 0 : data.Length, ' ');
        }
    }
}
