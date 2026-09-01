using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using SerialTerminal.Core;

namespace SerialTerminal
{
    public partial class MainForm : Form
    {
        // --- UI update policy -------------------------------------------------
        // The serial threads never touch the UI. They only enqueue. A WinForms
        // timer drains the queue every FlushIntervalMs and does one append per
        // batch, which is what keeps the UI alive at 921600 baud.
        private const int FlushIntervalMs = 40;
        private const int MaxChunksPerFlush = 2000;
        private const int MaxOutputChars = 400000;
        private const int TrimChars = 120000;

        private static readonly Color _ColorRx = Color.FromArgb(0x10, 0x10, 0x10);
        private static readonly Color _ColorTx = Color.FromArgb(0x00, 0x66, 0xCC);
        private static readonly Color _ColorInfo = Color.FromArgb(0xB0, 0x50, 0x00);

        private readonly SerialSession _Session = new SerialSession();
        private readonly ConcurrentQueue<LogChunk> _Pending = new ConcurrentQueue<LogChunk>();
        private readonly StreamFormatter _Formatter = new StreamFormatter();
        private readonly System.Windows.Forms.Timer _FlushTimer = new System.Windows.Forms.Timer();
        private readonly System.Windows.Forms.Timer _StatusTimer = new System.Windows.Forms.Timer();
        private readonly List<string> _SendHistory = new List<string>();

        private int _HistoryIndex = -1;
        private bool _ForceClose;

        public MainForm()
        {
            InitializeComponent();
        }

        // ====================================================================
        // Startup / shutdown
        // ====================================================================

        private void MainForm_Load(object sender, EventArgs e)
        {
            PopulateStaticCombos();
            RefreshPortList();

            _Formatter.Encoding = Encoding.Latin1;
            _Formatter.Mode = DisplayMode.Text;

            _Session.DataReceived += OnSessionData;
            _Session.DataSent += OnSessionSent;
            _Session.Error += OnSessionError;
            _Session.Closed += OnSessionClosed;

            _FlushTimer.Interval = FlushIntervalMs;
            _FlushTimer.Tick += FlushTimer_Tick;
            _FlushTimer.Start();

            _StatusTimer.Interval = 400;
            _StatusTimer.Tick += StatusTimer_Tick;
            _StatusTimer.Start();

            UpdateConnectedUi(false);
        }

        private async void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_ForceClose || !_Session.IsOpen)
            {
                _FlushTimer.Stop();
                _StatusTimer.Stop();
                return;
            }

            // Closing a serial port can block for a moment; do it without freezing
            // the window, then close for real.
            e.Cancel = true;
            _FlushTimer.Stop();
            _StatusTimer.Stop();
            Enabled = false;
            await _Session.CloseAsync();
            _ForceClose = true;
            Close();
        }

        // ====================================================================
        // Combo population
        // ====================================================================

        private void PopulateStaticCombos()
        {
            int[] bauds = { 300, 600, 1200, 2400, 4800, 9600, 14400, 19200, 38400,
                            57600, 115200, 230400, 460800, 921600 };
            foreach (int baud in bauds)
            {
                _CmbBaud.Items.Add(baud.ToString(CultureInfo.InvariantCulture));
            }
            _CmbBaud.Text = "115200";

            _CmbDataBits.Items.AddRange(new object[] { "5", "6", "7", "8" });
            _CmbDataBits.SelectedItem = "8";

            _CmbParity.Items.AddRange(new object[]
            {
                new ComboItem("None", Parity.None),
                new ComboItem("Odd", Parity.Odd),
                new ComboItem("Even", Parity.Even),
                new ComboItem("Mark", Parity.Mark),
                new ComboItem("Space", Parity.Space)
            });
            _CmbParity.SelectedIndex = 0;

            _CmbStopBits.Items.AddRange(new object[]
            {
                new ComboItem("1", StopBits.One),
                new ComboItem("1.5", StopBits.OnePointFive),
                new ComboItem("2", StopBits.Two)
            });
            _CmbStopBits.SelectedIndex = 0;

            _CmbFlow.Items.AddRange(new object[]
            {
                new ComboItem("None", Handshake.None),
                new ComboItem("RTS/CTS", Handshake.RequestToSend),
                new ComboItem("XON/XOFF", Handshake.XOnXOff),
                new ComboItem("RTS/CTS + XON/XOFF", Handshake.RequestToSendXOnXOff)
            });
            _CmbFlow.SelectedIndex = 0;

            _CmbView.Items.AddRange(new object[]
            {
                new ComboItem("Text", DisplayMode.Text),
                new ComboItem("Hex", DisplayMode.Hex)
            });
            _CmbView.SelectedIndex = 0;

            _CmbEncoding.Items.Add(new ComboItem("Latin-1 (raw)", Encoding.Latin1));
            _CmbEncoding.Items.Add(new ComboItem("ASCII", Encoding.ASCII));
            _CmbEncoding.Items.Add(new ComboItem("UTF-8", new UTF8Encoding(false)));
            try
            {
                _CmbEncoding.Items.Add(new ComboItem("Big5 (CP950)", Encoding.GetEncoding(950)));
            }
            catch (Exception)
            {
                // provider not registered / code page unavailable
            }
            _CmbEncoding.SelectedIndex = 0;

            _CmbEol.Items.AddRange(new object[]
            {
                new ComboItem("None", ""),
                new ComboItem("CR", "\r"),
                new ComboItem("LF", "\n"),
                new ComboItem("CR+LF", "\r\n")
            });
            _CmbEol.SelectedIndex = 0;
        }

        private void RefreshPortList()
        {
            string current = _CmbPort.SelectedItem as string;
            _CmbPort.Items.Clear();

            string[] ports = PortEnumerator.GetPortNames();
            _CmbPort.Items.AddRange(ports);

            if (current != null && _CmbPort.Items.Contains(current))
            {
                _CmbPort.SelectedItem = current;
            }
            else if (_CmbPort.Items.Count > 0)
            {
                _CmbPort.SelectedIndex = 0;
            }
        }

        // ====================================================================
        // Open / close
        // ====================================================================

        private async void BtnConnect_Click(object sender, EventArgs e)
        {
            if (_Session.IsOpen)
            {
                _BtnConnect.Enabled = false;
                await _Session.CloseAsync();
                _BtnConnect.Enabled = true;
                return;
            }

            SerialPortSettings settings = BuildSettings();
            if (settings == null)
            {
                return;
            }

            try
            {
                _Session.Open(settings);
            }
            catch (UnauthorizedAccessException)
            {
                ShowError(settings.PortName + " is in use by another application.");
                return;
            }
            catch (ArgumentException ex)
            {
                ShowError("Invalid port settings: " + ex.Message);
                return;
            }
            catch (IOException ex)
            {
                ShowError("Cannot open " + settings.PortName + ": " + ex.Message);
                return;
            }
            catch (InvalidOperationException ex)
            {
                ShowError(ex.Message);
                return;
            }

            _Formatter.Reset();
            _Pending.Enqueue(new LogChunk("Opened " + settings));
            UpdateConnectedUi(true);
        }

        private SerialPortSettings BuildSettings()
        {
            string portName = _CmbPort.SelectedItem as string;
            if (string.IsNullOrEmpty(portName))
            {
                ShowError("No COM port selected. Click Refresh.");
                return null;
            }

            int baudRate;
            if (!int.TryParse(_CmbBaud.Text.Trim(), NumberStyles.Integer,
                              CultureInfo.InvariantCulture, out baudRate) || baudRate <= 0)
            {
                ShowError("Baud rate must be a positive integer.");
                return null;
            }

            SerialPortSettings settings = new SerialPortSettings();
            settings.PortName = portName;
            settings.BaudRate = baudRate;
            settings.DataBits = int.Parse((string)_CmbDataBits.SelectedItem, CultureInfo.InvariantCulture);
            settings.Parity = (Parity)((ComboItem)_CmbParity.SelectedItem).Value;
            settings.StopBits = (StopBits)((ComboItem)_CmbStopBits.SelectedItem).Value;
            settings.Handshake = (Handshake)((ComboItem)_CmbFlow.SelectedItem).Value;
            settings.DtrEnable = _ChkDtr.Checked;
            settings.RtsEnable = _ChkRts.Checked;
            return settings;
        }

        private void UpdateConnectedUi(bool connected)
        {
            _BtnConnect.Text = connected ? "Close" : "Open";
            _CmbPort.Enabled = !connected;
            _BtnRefresh.Enabled = !connected;
            _CmbBaud.Enabled = !connected;
            _CmbDataBits.Enabled = !connected;
            _CmbParity.Enabled = !connected;
            _CmbStopBits.Enabled = !connected;
            _CmbFlow.Enabled = !connected;
            _BtnSend.Enabled = connected;
            _TxtSend.Enabled = connected;

            _LblStatus.Text = connected && _Session.CurrentSettings != null
                ? "Open  " + _Session.CurrentSettings
                : "Closed";
        }

        // ====================================================================
        // Session events (background threads -- enqueue only)
        // ====================================================================

        private void OnSessionData(object sender, LogChunk chunk)
        {
            _Pending.Enqueue(chunk);
        }

        private void OnSessionSent(object sender, LogChunk chunk)
        {
            _Pending.Enqueue(chunk);
        }

        private void OnSessionError(object sender, string message)
        {
            _Pending.Enqueue(new LogChunk("ERROR: " + message));
        }

        private void OnSessionClosed(object sender, bool unexpected)
        {
            _Pending.Enqueue(new LogChunk(unexpected
                ? "Port closed unexpectedly (device removed?)"
                : "Port closed"));

            if (IsHandleCreated)
            {
                BeginInvoke(new Action(() => UpdateConnectedUi(false)));
            }
        }

        // ====================================================================
        // UI pump
        // ====================================================================

        private void FlushTimer_Tick(object sender, EventArgs e)
        {
            if (_Pending.IsEmpty)
            {
                return;
            }

            StringBuilder sb = new StringBuilder();
            Direction batchDirection = Direction.None;
            bool wroteAnything = false;

            NativeMethods.SetRedraw(_RtbOutput, false);
            try
            {
                LogChunk chunk;
                int processed = 0;

                while (processed < MaxChunksPerFlush && _Pending.TryDequeue(out chunk))
                {
                    processed++;

                    if (chunk.Direction == Direction.Tx && !_ChkLocalEcho.Checked)
                    {
                        continue;
                    }

                    // Flush the accumulated text whenever the colour has to change.
                    if (chunk.Direction != batchDirection && sb.Length > 0)
                    {
                        AppendColored(sb.ToString(), ColorFor(batchDirection));
                        sb.Length = 0;
                        wroteAnything = true;
                    }

                    batchDirection = chunk.Direction;
                    sb.Append(_Formatter.Format(chunk));
                }

                if (sb.Length > 0)
                {
                    AppendColored(sb.ToString(), ColorFor(batchDirection));
                    wroteAnything = true;
                }

                if (wroteAnything)
                {
                    TrimOutput();
                }
            }
            finally
            {
                // Repaint must be re-enabled BEFORE ScrollToCaret: the rich edit
                // control cannot compute the scroll position while redraw is off.
                NativeMethods.SetRedraw(_RtbOutput, true);
                if (wroteAnything)
                {
                    if (_ChkAutoScroll.Checked)
                    {
                        _RtbOutput.SelectionStart = _RtbOutput.TextLength;
                        _RtbOutput.SelectionLength = 0;
                        _RtbOutput.ScrollToCaret();
                    }
                    _RtbOutput.Invalidate();
                }
            }
        }

        private static Color ColorFor(Direction direction)
        {
            if (direction == Direction.Tx) return _ColorTx;
            if (direction == Direction.Info) return _ColorInfo;
            return _ColorRx;
        }

        private void AppendColored(string text, Color color)
        {
            _RtbOutput.SelectionStart = _RtbOutput.TextLength;
            _RtbOutput.SelectionLength = 0;
            _RtbOutput.SelectionColor = color;
            _RtbOutput.AppendText(text);
        }

        private void TrimOutput()
        {
            if (_RtbOutput.TextLength <= MaxOutputChars)
            {
                return;
            }

            int savedStart = _RtbOutput.SelectionStart;
            _RtbOutput.Select(0, TrimChars);
            _RtbOutput.SelectedText = string.Empty;
            int newStart = Math.Max(0, savedStart - TrimChars);
            _RtbOutput.Select(Math.Min(newStart, _RtbOutput.TextLength), 0);
        }

        private void StatusTimer_Tick(object sender, EventArgs e)
        {
            _LblRxCount.Text = "RX " + FormatBytes(_Session.RxBytes);
            _LblTxCount.Text = "TX " + FormatBytes(_Session.TxBytes);

            bool cts, dsr, cd;
            if (_Session.TryReadSignals(out cts, out dsr, out cd))
            {
                _LblSignals.Text = string.Format("CTS {0}  DSR {1}  DCD {2}",
                    cts ? "1" : "0", dsr ? "1" : "0", cd ? "1" : "0");
            }
            else
            {
                _LblSignals.Text = "CTS -  DSR -  DCD -";
            }
        }

        private static string FormatBytes(long count)
        {
            if (count < 1024) return count + " B";
            if (count < 1024 * 1024) return (count / 1024.0).ToString("F1") + " KB";
            return (count / (1024.0 * 1024.0)).ToString("F2") + " MB";
        }

        // ====================================================================
        // Sending
        // ====================================================================

        private void BtnSend_Click(object sender, EventArgs e)
        {
            SendCurrentInput();
        }

        private void TxtSend_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendCurrentInput();
                return;
            }

            if (e.KeyCode == Keys.Up && _SendHistory.Count > 0)
            {
                e.SuppressKeyPress = true;
                if (_HistoryIndex < 0) _HistoryIndex = _SendHistory.Count;
                if (_HistoryIndex > 0) _HistoryIndex--;
                _TxtSend.Text = _SendHistory[_HistoryIndex];
                _TxtSend.SelectionStart = _TxtSend.TextLength;
                return;
            }

            if (e.KeyCode == Keys.Down && _SendHistory.Count > 0)
            {
                e.SuppressKeyPress = true;
                if (_HistoryIndex >= 0 && _HistoryIndex < _SendHistory.Count - 1)
                {
                    _HistoryIndex++;
                    _TxtSend.Text = _SendHistory[_HistoryIndex];
                }
                else
                {
                    _HistoryIndex = -1;
                    _TxtSend.Text = string.Empty;
                }
                _TxtSend.SelectionStart = _TxtSend.TextLength;
            }
        }

        private void SendCurrentInput()
        {
            if (!_Session.IsOpen)
            {
                ShowError("Port is not open.");
                return;
            }

            string input = _TxtSend.Text;
            if (input.Length == 0)
            {
                return;
            }

            byte[] payload;

            if (_RdoHex.Checked)
            {
                string error;
                if (!HexCodec.TryParse(input, out payload, out error))
                {
                    ShowError("HEX parse error: " + error);
                    return;
                }
                // EOL bytes are appended in HEX mode too, using the raw ASCII codes.
                payload = AppendEol(payload, Encoding.ASCII);
            }
            else
            {
                Encoding encoding = CurrentEncoding();
                byte[] body = encoding.GetBytes(input);
                payload = AppendEol(body, encoding);
            }

            if (!_Session.Send(payload))
            {
                ShowError("Send failed: the port is closing.");
                return;
            }

            _SendHistory.Add(input);
            if (_SendHistory.Count > 200)
            {
                _SendHistory.RemoveAt(0);
            }
            _HistoryIndex = -1;
            _TxtSend.SelectAll();
            _TxtSend.Focus();
        }

        private byte[] AppendEol(byte[] body, Encoding encoding)
        {
            string eol = (string)((ComboItem)_CmbEol.SelectedItem).Value;
            if (string.IsNullOrEmpty(eol))
            {
                return body;
            }

            byte[] tail = encoding.GetBytes(eol);
            byte[] result = new byte[body.Length + tail.Length];
            Buffer.BlockCopy(body, 0, result, 0, body.Length);
            Buffer.BlockCopy(tail, 0, result, body.Length, tail.Length);
            return result;
        }

        private Encoding CurrentEncoding()
        {
            ComboItem item = _CmbEncoding.SelectedItem as ComboItem;
            return item == null ? Encoding.Latin1 : (Encoding)item.Value;
        }

        // ====================================================================
        // View options
        // ====================================================================

        private void CmbView_SelectedIndexChanged(object sender, EventArgs e)
        {
            _Formatter.Mode = (DisplayMode)((ComboItem)_CmbView.SelectedItem).Value;
            _Formatter.Reset();
        }

        private void CmbEncoding_SelectedIndexChanged(object sender, EventArgs e)
        {
            _Formatter.Encoding = CurrentEncoding();
        }

        private void ChkTimestamp_CheckedChanged(object sender, EventArgs e)
        {
            _Formatter.ShowTimestamp = _ChkTimestamp.Checked;
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            _RtbOutput.Clear();
            _Formatter.Reset();
        }

        private void BtnSaveLog_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = "Text file (*.txt)|*.txt|All files (*.*)|*.*";
                dialog.FileName = "serial-" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".txt";
                if (dialog.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                try
                {
                    File.WriteAllText(dialog.FileName, _RtbOutput.Text, new UTF8Encoding(true));
                }
                catch (Exception ex)
                {
                    ShowError("Save failed: " + ex.Message);
                }
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            RefreshPortList();
        }

        private void ChkDtr_CheckedChanged(object sender, EventArgs e)
        {
            if (_Session.IsOpen)
            {
                _Session.SetDtr(_ChkDtr.Checked);
            }
        }

        private void ChkRts_CheckedChanged(object sender, EventArgs e)
        {
            if (_Session.IsOpen)
            {
                _Session.SetRts(_ChkRts.Checked);
            }
        }

        private void ShowError(string message)
        {
            MessageBox.Show(this, message, "Serial Terminal",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        // ====================================================================
        // Helpers
        // ====================================================================

        private sealed class ComboItem
        {
            public readonly string Text;
            public readonly object Value;

            public ComboItem(string text, object value)
            {
                Text = text;
                Value = value;
            }

            public override string ToString()
            {
                return Text;
            }
        }

        private static class NativeMethods
        {
            private const int WM_SETREDRAW = 0x000B;

            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

            /// <summary>
            /// Suppresses repaint while a batch of text is appended. Without this the
            /// RichTextBox repaints once per AppendText and the UI flickers badly.
            /// </summary>
            public static void SetRedraw(Control control, bool enable)
            {
                if (control == null || !control.IsHandleCreated)
                {
                    return;
                }
                SendMessage(control.Handle, WM_SETREDRAW, enable ? (IntPtr)1 : IntPtr.Zero, IntPtr.Zero);
            }
        }
    }
}
