using System;
using System.Text;
using System.Windows.Forms;

namespace SerialTerminal
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            // Big5 / CP950 and other legacy code pages are not built into .NET 8;
            // register the extra provider before any Encoding.GetEncoding call.
            Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}
