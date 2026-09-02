namespace SerialTerminal
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer _Components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (_Components != null))
            {
                _Components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent() {
            _PnlSettings = new System.Windows.Forms.Panel();
            _LblSignals = new System.Windows.Forms.Label();
            _BtnConnect = new System.Windows.Forms.Button();
            _ChkRts = new System.Windows.Forms.CheckBox();
            _ChkDtr = new System.Windows.Forms.CheckBox();
            _CmbFlow = new System.Windows.Forms.ComboBox();
            _LblFlow = new System.Windows.Forms.Label();
            _CmbStopBits = new System.Windows.Forms.ComboBox();
            _LblStopBits = new System.Windows.Forms.Label();
            _CmbParity = new System.Windows.Forms.ComboBox();
            _LblParity = new System.Windows.Forms.Label();
            _CmbDataBits = new System.Windows.Forms.ComboBox();
            _LblDataBits = new System.Windows.Forms.Label();
            _CmbBaud = new System.Windows.Forms.ComboBox();
            _LblBaud = new System.Windows.Forms.Label();
            _BtnRefresh = new System.Windows.Forms.Button();
            _CmbPort = new System.Windows.Forms.ComboBox();
            _LblPort = new System.Windows.Forms.Label();
            _PnlView = new System.Windows.Forms.Panel();
            _BtnSaveLog = new System.Windows.Forms.Button();
            _BtnClear = new System.Windows.Forms.Button();
            _ChkLocalEcho = new System.Windows.Forms.CheckBox();
            _ChkAutoScroll = new System.Windows.Forms.CheckBox();
            _ChkTimestamp = new System.Windows.Forms.CheckBox();
            _CmbEncoding = new System.Windows.Forms.ComboBox();
            _LblEncoding = new System.Windows.Forms.Label();
            _CmbView = new System.Windows.Forms.ComboBox();
            _LblView = new System.Windows.Forms.Label();
            _PnlSend = new System.Windows.Forms.Panel();
            _BtnSend = new System.Windows.Forms.Button();
            _CmbEol = new System.Windows.Forms.ComboBox();
            _LblEol = new System.Windows.Forms.Label();
            _TxtSend = new System.Windows.Forms.TextBox();
            _RdoHex = new System.Windows.Forms.RadioButton();
            _RdoText = new System.Windows.Forms.RadioButton();
            _RtbOutput = new System.Windows.Forms.RichTextBox();
            _StatusStrip = new System.Windows.Forms.StatusStrip();
            _LblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            _LblRxCount = new System.Windows.Forms.ToolStripStatusLabel();
            _LblTxCount = new System.Windows.Forms.ToolStripStatusLabel();
            _PnlSettings.SuspendLayout();
            _PnlView.SuspendLayout();
            _PnlSend.SuspendLayout();
            _StatusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // _PnlSettings
            // 
            _PnlSettings.Controls.Add(_LblSignals);
            _PnlSettings.Controls.Add(_BtnConnect);
            _PnlSettings.Controls.Add(_ChkRts);
            _PnlSettings.Controls.Add(_ChkDtr);
            _PnlSettings.Controls.Add(_CmbFlow);
            _PnlSettings.Controls.Add(_LblFlow);
            _PnlSettings.Controls.Add(_CmbStopBits);
            _PnlSettings.Controls.Add(_LblStopBits);
            _PnlSettings.Controls.Add(_CmbParity);
            _PnlSettings.Controls.Add(_LblParity);
            _PnlSettings.Controls.Add(_CmbDataBits);
            _PnlSettings.Controls.Add(_LblDataBits);
            _PnlSettings.Controls.Add(_CmbBaud);
            _PnlSettings.Controls.Add(_LblBaud);
            _PnlSettings.Controls.Add(_BtnRefresh);
            _PnlSettings.Controls.Add(_CmbPort);
            _PnlSettings.Controls.Add(_LblPort);
            _PnlSettings.Dock = System.Windows.Forms.DockStyle.Top;
            _PnlSettings.Location = new System.Drawing.Point(0, 0);
            _PnlSettings.Name = "_PnlSettings";
            _PnlSettings.Size = new System.Drawing.Size(1348, 92);
            _PnlSettings.TabIndex = 0;
            // 
            // _LblSignals
            // 
            _LblSignals.AutoSize = true;
            _LblSignals.Location = new System.Drawing.Point(592, 55);
            _LblSignals.Name = "_LblSignals";
            _LblSignals.Size = new System.Drawing.Size(145, 21);
            _LblSignals.TabIndex = 16;
            _LblSignals.Text = "CTS -  DSR -  DCD -";
            // 
            // _BtnConnect
            // 
            _BtnConnect.Location = new System.Drawing.Point(440, 49);
            _BtnConnect.Name = "_BtnConnect";
            _BtnConnect.Size = new System.Drawing.Size(130, 34);
            _BtnConnect.TabIndex = 15;
            _BtnConnect.Text = "Open";
            _BtnConnect.UseVisualStyleBackColor = true;
            _BtnConnect.Click += BtnConnect_Click;
            // 
            // _ChkRts
            // 
            _ChkRts.AutoSize = true;
            _ChkRts.Checked = true;
            _ChkRts.CheckState = System.Windows.Forms.CheckState.Checked;
            _ChkRts.Location = new System.Drawing.Point(352, 53);
            _ChkRts.Name = "_ChkRts";
            _ChkRts.Size = new System.Drawing.Size(55, 25);
            _ChkRts.TabIndex = 14;
            _ChkRts.Text = "RTS";
            _ChkRts.UseVisualStyleBackColor = true;
            _ChkRts.CheckedChanged += ChkRts_CheckedChanged;
            // 
            // _ChkDtr
            // 
            _ChkDtr.AutoSize = true;
            _ChkDtr.Checked = true;
            _ChkDtr.CheckState = System.Windows.Forms.CheckState.Checked;
            _ChkDtr.Location = new System.Drawing.Point(274, 53);
            _ChkDtr.Name = "_ChkDtr";
            _ChkDtr.Size = new System.Drawing.Size(57, 25);
            _ChkDtr.TabIndex = 13;
            _ChkDtr.Text = "DTR";
            _ChkDtr.UseVisualStyleBackColor = true;
            _ChkDtr.CheckedChanged += ChkDtr_CheckedChanged;
            // 
            // _CmbFlow
            // 
            _CmbFlow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            _CmbFlow.Location = new System.Drawing.Point(60, 51);
            _CmbFlow.Name = "_CmbFlow";
            _CmbFlow.Size = new System.Drawing.Size(200, 29);
            _CmbFlow.TabIndex = 12;
            // 
            // _LblFlow
            // 
            _LblFlow.AutoSize = true;
            _LblFlow.Location = new System.Drawing.Point(12, 55);
            _LblFlow.Name = "_LblFlow";
            _LblFlow.Size = new System.Drawing.Size(43, 21);
            _LblFlow.TabIndex = 11;
            _LblFlow.Text = "Flow";
            // 
            // _CmbStopBits
            // 
            _CmbStopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            _CmbStopBits.Location = new System.Drawing.Point(884, 9);
            _CmbStopBits.Name = "_CmbStopBits";
            _CmbStopBits.Size = new System.Drawing.Size(100, 29);
            _CmbStopBits.TabIndex = 10;
            // 
            // _LblStopBits
            // 
            _LblStopBits.AutoSize = true;
            _LblStopBits.Location = new System.Drawing.Point(836, 13);
            _LblStopBits.Name = "_LblStopBits";
            _LblStopBits.Size = new System.Drawing.Size(41, 21);
            _LblStopBits.TabIndex = 9;
            _LblStopBits.Text = "Stop";
            // 
            // _CmbParity
            // 
            _CmbParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            _CmbParity.Location = new System.Drawing.Point(714, 9);
            _CmbParity.Name = "_CmbParity";
            _CmbParity.Size = new System.Drawing.Size(110, 29);
            _CmbParity.TabIndex = 8;
            // 
            // _LblParity
            // 
            _LblParity.AutoSize = true;
            _LblParity.Location = new System.Drawing.Point(660, 13);
            _LblParity.Name = "_LblParity";
            _LblParity.Size = new System.Drawing.Size(49, 21);
            _LblParity.TabIndex = 7;
            _LblParity.Text = "Parity";
            // 
            // _CmbDataBits
            // 
            _CmbDataBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            _CmbDataBits.Location = new System.Drawing.Point(568, 9);
            _CmbDataBits.Name = "_CmbDataBits";
            _CmbDataBits.Size = new System.Drawing.Size(80, 29);
            _CmbDataBits.TabIndex = 6;
            // 
            // _LblDataBits
            // 
            _LblDataBits.AutoSize = true;
            _LblDataBits.Location = new System.Drawing.Point(520, 13);
            _LblDataBits.Name = "_LblDataBits";
            _LblDataBits.Size = new System.Drawing.Size(42, 21);
            _LblDataBits.TabIndex = 5;
            _LblDataBits.Text = "Data";
            // 
            // _CmbBaud
            // 
            _CmbBaud.Location = new System.Drawing.Point(376, 9);
            _CmbBaud.Name = "_CmbBaud";
            _CmbBaud.Size = new System.Drawing.Size(130, 29);
            _CmbBaud.TabIndex = 4;
            // 
            // _LblBaud
            // 
            _LblBaud.AutoSize = true;
            _LblBaud.Location = new System.Drawing.Point(322, 13);
            _LblBaud.Name = "_LblBaud";
            _LblBaud.Size = new System.Drawing.Size(45, 21);
            _LblBaud.TabIndex = 3;
            _LblBaud.Text = "Baud";
            // 
            // _BtnRefresh
            // 
            _BtnRefresh.Location = new System.Drawing.Point(208, 7);
            _BtnRefresh.Name = "_BtnRefresh";
            _BtnRefresh.Size = new System.Drawing.Size(100, 33);
            _BtnRefresh.TabIndex = 2;
            _BtnRefresh.Text = "Refresh";
            _BtnRefresh.UseVisualStyleBackColor = true;
            _BtnRefresh.Click += BtnRefresh_Click;
            // 
            // _CmbPort
            // 
            _CmbPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            _CmbPort.Location = new System.Drawing.Point(60, 9);
            _CmbPort.Name = "_CmbPort";
            _CmbPort.Size = new System.Drawing.Size(140, 29);
            _CmbPort.TabIndex = 1;
            // 
            // _LblPort
            // 
            _LblPort.AutoSize = true;
            _LblPort.Location = new System.Drawing.Point(12, 13);
            _LblPort.Name = "_LblPort";
            _LblPort.Size = new System.Drawing.Size(38, 21);
            _LblPort.TabIndex = 0;
            _LblPort.Text = "Port";
            // 
            // _PnlView
            // 
            _PnlView.Controls.Add(_BtnSaveLog);
            _PnlView.Controls.Add(_BtnClear);
            _PnlView.Controls.Add(_ChkLocalEcho);
            _PnlView.Controls.Add(_ChkAutoScroll);
            _PnlView.Controls.Add(_ChkTimestamp);
            _PnlView.Controls.Add(_CmbEncoding);
            _PnlView.Controls.Add(_LblEncoding);
            _PnlView.Controls.Add(_CmbView);
            _PnlView.Controls.Add(_LblView);
            _PnlView.Dock = System.Windows.Forms.DockStyle.Top;
            _PnlView.Location = new System.Drawing.Point(0, 92);
            _PnlView.Name = "_PnlView";
            _PnlView.Size = new System.Drawing.Size(1348, 46);
            _PnlView.TabIndex = 1;
            // 
            // _BtnSaveLog
            // 
            _BtnSaveLog.Location = new System.Drawing.Point(964, 7);
            _BtnSaveLog.Name = "_BtnSaveLog";
            _BtnSaveLog.Size = new System.Drawing.Size(120, 33);
            _BtnSaveLog.TabIndex = 8;
            _BtnSaveLog.Text = "Save log";
            _BtnSaveLog.UseVisualStyleBackColor = true;
            _BtnSaveLog.Click += BtnSaveLog_Click;
            // 
            // _BtnClear
            // 
            _BtnClear.Location = new System.Drawing.Point(856, 7);
            _BtnClear.Name = "_BtnClear";
            _BtnClear.Size = new System.Drawing.Size(100, 33);
            _BtnClear.TabIndex = 7;
            _BtnClear.Text = "Clear";
            _BtnClear.UseVisualStyleBackColor = true;
            _BtnClear.Click += BtnClear_Click;
            // 
            // _ChkLocalEcho
            // 
            _ChkLocalEcho.AutoSize = true;
            _ChkLocalEcho.Checked = true;
            _ChkLocalEcho.CheckState = System.Windows.Forms.CheckState.Checked;
            _ChkLocalEcho.Location = new System.Drawing.Point(732, 11);
            _ChkLocalEcho.Name = "_ChkLocalEcho";
            _ChkLocalEcho.Size = new System.Drawing.Size(83, 25);
            _ChkLocalEcho.TabIndex = 6;
            _ChkLocalEcho.Text = "Echo TX";
            _ChkLocalEcho.UseVisualStyleBackColor = true;
            // 
            // _ChkAutoScroll
            // 
            _ChkAutoScroll.AutoSize = true;
            _ChkAutoScroll.Checked = true;
            _ChkAutoScroll.CheckState = System.Windows.Forms.CheckState.Checked;
            _ChkAutoScroll.Location = new System.Drawing.Point(600, 11);
            _ChkAutoScroll.Name = "_ChkAutoScroll";
            _ChkAutoScroll.Size = new System.Drawing.Size(103, 25);
            _ChkAutoScroll.TabIndex = 5;
            _ChkAutoScroll.Text = "Auto scroll";
            _ChkAutoScroll.UseVisualStyleBackColor = true;
            // 
            // _ChkTimestamp
            // 
            _ChkTimestamp.AutoSize = true;
            _ChkTimestamp.Location = new System.Drawing.Point(468, 11);
            _ChkTimestamp.Name = "_ChkTimestamp";
            _ChkTimestamp.Size = new System.Drawing.Size(106, 25);
            _ChkTimestamp.TabIndex = 4;
            _ChkTimestamp.Text = "Timestamp";
            _ChkTimestamp.UseVisualStyleBackColor = true;
            _ChkTimestamp.CheckedChanged += ChkTimestamp_CheckedChanged;
            // 
            // _CmbEncoding
            // 
            _CmbEncoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            _CmbEncoding.Location = new System.Drawing.Point(272, 9);
            _CmbEncoding.Name = "_CmbEncoding";
            _CmbEncoding.Size = new System.Drawing.Size(180, 29);
            _CmbEncoding.TabIndex = 3;
            _CmbEncoding.SelectedIndexChanged += CmbEncoding_SelectedIndexChanged;
            // 
            // _LblEncoding
            // 
            _LblEncoding.AutoSize = true;
            _LblEncoding.Location = new System.Drawing.Point(186, 13);
            _LblEncoding.Name = "_LblEncoding";
            _LblEncoding.Size = new System.Drawing.Size(74, 21);
            _LblEncoding.TabIndex = 2;
            _LblEncoding.Text = "Encoding";
            // 
            // _CmbView
            // 
            _CmbView.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            _CmbView.Location = new System.Drawing.Point(60, 9);
            _CmbView.Name = "_CmbView";
            _CmbView.Size = new System.Drawing.Size(110, 29);
            _CmbView.TabIndex = 1;
            _CmbView.SelectedIndexChanged += CmbView_SelectedIndexChanged;
            // 
            // _LblView
            // 
            _LblView.AutoSize = true;
            _LblView.Location = new System.Drawing.Point(12, 13);
            _LblView.Name = "_LblView";
            _LblView.Size = new System.Drawing.Size(44, 21);
            _LblView.TabIndex = 0;
            _LblView.Text = "View";
            // 
            // _PnlSend
            // 
            _PnlSend.Controls.Add(_BtnSend);
            _PnlSend.Controls.Add(_CmbEol);
            _PnlSend.Controls.Add(_LblEol);
            _PnlSend.Controls.Add(_TxtSend);
            _PnlSend.Controls.Add(_RdoHex);
            _PnlSend.Controls.Add(_RdoText);
            _PnlSend.Dock = System.Windows.Forms.DockStyle.Bottom;
            _PnlSend.Location = new System.Drawing.Point(0, 646);
            _PnlSend.Name = "_PnlSend";
            _PnlSend.Size = new System.Drawing.Size(1348, 52);
            _PnlSend.TabIndex = 3;
            // 
            // _BtnSend
            // 
            _BtnSend.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            _BtnSend.Location = new System.Drawing.Point(1206, 10);
            _BtnSend.Name = "_BtnSend";
            _BtnSend.Size = new System.Drawing.Size(130, 34);
            _BtnSend.TabIndex = 5;
            _BtnSend.Text = "Send";
            _BtnSend.UseVisualStyleBackColor = true;
            _BtnSend.Click += BtnSend_Click;
            // 
            // _CmbEol
            // 
            _CmbEol.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            _CmbEol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            _CmbEol.Location = new System.Drawing.Point(1084, 12);
            _CmbEol.Name = "_CmbEol";
            _CmbEol.Size = new System.Drawing.Size(110, 29);
            _CmbEol.TabIndex = 4;
            // 
            // _LblEol
            // 
            _LblEol.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            _LblEol.AutoSize = true;
            _LblEol.Location = new System.Drawing.Point(1042, 16);
            _LblEol.Name = "_LblEol";
            _LblEol.Size = new System.Drawing.Size(38, 21);
            _LblEol.TabIndex = 3;
            _LblEol.Text = "EOL";
            // 
            // _TxtSend
            // 
            _TxtSend.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            _TxtSend.Font = new System.Drawing.Font("Consolas", 9.6F);
            _TxtSend.Location = new System.Drawing.Point(170, 12);
            _TxtSend.Name = "_TxtSend";
            _TxtSend.Size = new System.Drawing.Size(859, 26);
            _TxtSend.TabIndex = 2;
            _TxtSend.KeyDown += TxtSend_KeyDown;
            // 
            // _RdoHex
            // 
            _RdoHex.AutoSize = true;
            _RdoHex.Location = new System.Drawing.Point(92, 15);
            _RdoHex.Name = "_RdoHex";
            _RdoHex.Size = new System.Drawing.Size(54, 25);
            _RdoHex.TabIndex = 1;
            _RdoHex.Text = "Hex";
            _RdoHex.UseVisualStyleBackColor = true;
            // 
            // _RdoText
            // 
            _RdoText.AutoSize = true;
            _RdoText.Checked = true;
            _RdoText.Location = new System.Drawing.Point(12, 15);
            _RdoText.Name = "_RdoText";
            _RdoText.Size = new System.Drawing.Size(54, 25);
            _RdoText.TabIndex = 0;
            _RdoText.TabStop = true;
            _RdoText.Text = "Text";
            _RdoText.UseVisualStyleBackColor = true;
            // 
            // _RtbOutput
            // 
            _RtbOutput.BackColor = System.Drawing.Color.White;
            _RtbOutput.DetectUrls = false;
            _RtbOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            _RtbOutput.Font = new System.Drawing.Font("Consolas", 9.6F);
            _RtbOutput.HideSelection = false;
            _RtbOutput.Location = new System.Drawing.Point(0, 138);
            _RtbOutput.Name = "_RtbOutput";
            _RtbOutput.ReadOnly = true;
            _RtbOutput.Size = new System.Drawing.Size(1348, 508);
            _RtbOutput.TabIndex = 2;
            _RtbOutput.Text = "";
            // 
            // _StatusStrip
            // 
            _StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { _LblStatus, _LblRxCount, _LblTxCount });
            _StatusStrip.Location = new System.Drawing.Point(0, 698);
            _StatusStrip.Name = "_StatusStrip";
            _StatusStrip.Size = new System.Drawing.Size(1348, 23);
            _StatusStrip.TabIndex = 4;
            // 
            // _LblStatus
            // 
            _LblStatus.Name = "_LblStatus";
            _LblStatus.Size = new System.Drawing.Size(49, 17);
            _LblStatus.Text = "Closed";
            // 
            // _LblRxCount
            // 
            _LblRxCount.Name = "_LblRxCount";
            _LblRxCount.Size = new System.Drawing.Size(46, 17);
            _LblRxCount.Text = "RX 0 B";
            // 
            // _LblTxCount
            // 
            _LblTxCount.Name = "_LblTxCount";
            _LblTxCount.Size = new System.Drawing.Size(45, 17);
            _LblTxCount.Text = "TX 0 B";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            ClientSize = new System.Drawing.Size(1348, 721);
            Controls.Add(_RtbOutput);
            Controls.Add(_PnlView);
            Controls.Add(_PnlSettings);
            Controls.Add(_PnlSend);
            Controls.Add(_StatusStrip);
            Font = new System.Drawing.Font("Segoe UI", 9.6F);
            MinimumSize = new System.Drawing.Size(1000, 558);
            Name = "MainForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Serial Terminal";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            _PnlSettings.ResumeLayout(false);
            _PnlSettings.PerformLayout();
            _PnlView.ResumeLayout(false);
            _PnlView.PerformLayout();
            _PnlSend.ResumeLayout(false);
            _PnlSend.PerformLayout();
            _StatusStrip.ResumeLayout(false);
            _StatusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel _PnlSettings;
        private System.Windows.Forms.Label _LblPort;
        private System.Windows.Forms.ComboBox _CmbPort;
        private System.Windows.Forms.Button _BtnRefresh;
        private System.Windows.Forms.Label _LblBaud;
        private System.Windows.Forms.ComboBox _CmbBaud;
        private System.Windows.Forms.Label _LblDataBits;
        private System.Windows.Forms.ComboBox _CmbDataBits;
        private System.Windows.Forms.Label _LblParity;
        private System.Windows.Forms.ComboBox _CmbParity;
        private System.Windows.Forms.Label _LblStopBits;
        private System.Windows.Forms.ComboBox _CmbStopBits;
        private System.Windows.Forms.Label _LblFlow;
        private System.Windows.Forms.ComboBox _CmbFlow;
        private System.Windows.Forms.CheckBox _ChkDtr;
        private System.Windows.Forms.CheckBox _ChkRts;
        private System.Windows.Forms.Button _BtnConnect;
        private System.Windows.Forms.Label _LblSignals;
        private System.Windows.Forms.Panel _PnlView;
        private System.Windows.Forms.Label _LblView;
        private System.Windows.Forms.ComboBox _CmbView;
        private System.Windows.Forms.Label _LblEncoding;
        private System.Windows.Forms.ComboBox _CmbEncoding;
        private System.Windows.Forms.CheckBox _ChkTimestamp;
        private System.Windows.Forms.CheckBox _ChkAutoScroll;
        private System.Windows.Forms.CheckBox _ChkLocalEcho;
        private System.Windows.Forms.Button _BtnClear;
        private System.Windows.Forms.Button _BtnSaveLog;
        private System.Windows.Forms.Panel _PnlSend;
        private System.Windows.Forms.RadioButton _RdoText;
        private System.Windows.Forms.RadioButton _RdoHex;
        private System.Windows.Forms.TextBox _TxtSend;
        private System.Windows.Forms.Label _LblEol;
        private System.Windows.Forms.ComboBox _CmbEol;
        private System.Windows.Forms.Button _BtnSend;
        private System.Windows.Forms.RichTextBox _RtbOutput;
        private System.Windows.Forms.StatusStrip _StatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel _LblStatus;
        private System.Windows.Forms.ToolStripStatusLabel _LblRxCount;
        private System.Windows.Forms.ToolStripStatusLabel _LblTxCount;
    }
}
