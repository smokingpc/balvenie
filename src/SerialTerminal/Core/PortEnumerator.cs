using System;
using System.Collections.Generic;
using System.IO.Ports;

namespace SerialTerminal.Core
{
    public static class PortEnumerator
    {
        /// <summary>COM port names sorted numerically (COM2 before COM10).</summary>
        public static string[] GetPortNames()
        {
            string[] names;
            try
            {
                names = SerialPort.GetPortNames();
            }
            catch (Exception)
            {
                return Array.Empty<string>();
            }

            List<string> list = new List<string>(names);
            list.Sort(CompareComName);
            return list.ToArray();
        }

        private static int CompareComName(string a, string b)
        {
            int na = ExtractNumber(a);
            int nb = ExtractNumber(b);
            if (na >= 0 && nb >= 0 && na != nb)
            {
                return na.CompareTo(nb);
            }
            return string.Compare(a, b, StringComparison.OrdinalIgnoreCase);
        }

        private static int ExtractNumber(string name)
        {
            int i = 0;
            while (i < name.Length && !char.IsDigit(name[i]))
            {
                i++;
            }
            int value = 0;
            bool any = false;
            while (i < name.Length && char.IsDigit(name[i]))
            {
                value = value * 10 + (name[i] - '0');
                i++;
                any = true;
            }
            return any ? value : -1;
        }
    }
}
