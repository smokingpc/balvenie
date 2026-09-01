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

        private void InitializeComponent()
        {
            this._Components = new System.ComponentModel.Container();
            this._PnlSettings = new System.Windows.Forms.Panel();
            this._LblSignals = new System.Windows.Forms.Label();
            this._BtnConnect = new System.Windows.Forms.Button();
            this._ChkRts = new System.Windows.Forms.CheckBox();
            this._ChkDtr = new System.Windows.Forms.CheckBox();
            this._CmbFlow = new System.Windows.Forms.ComboBox();
            this._LblFlow = new System.Windows.Forms.Label();
            this._CmbStopBits = new System.Windows.Forms.ComboBox();
            this._LblStopBits = new System.Windows.Forms.Label();
            this._CmbParity = new System.Windows.Forms.ComboBox();
            this._LblParity = new System.Windows.Forms.Label();
            this._CmbDataBits = new System.Windows.Forms.ComboBox();
            this._LblDataBits = new System.Windows.Forms.Label();
            this._CmbBaud = new System.Windows.Forms.ComboBox();
            this._LblBaud = new System.Windows.Forms.Label();
            this._BtnRefresh = new System.Windows.Forms.Button();
            this._CmbPort = new System.Windows.Forms.ComboBox();
            this._LblPort = new System.Windows.Forms.Label();
            this._PnlView = new System.Windows.Forms.Panel();
            this._BtnSaveLog = new System.Windows.Forms.Button();
            this._BtnClear = new System.Windows.Forms.Button();
            this._ChkLocalEcho = new System.Windows.Forms.CheckBox();
            this._ChkAutoScroll = new System.Windows.Forms.CheckBox();
            this._ChkTimestamp = new System.Windows.Forms.CheckBox();
            this._CmbEncoding = new System.Windows.Forms.ComboBox();
            this._LblEncoding = new System.Windows.Forms.Label();
            this._CmbView = new System.Windows.Forms.ComboBox();
            this._LblView = new System.Windows.Forms.Label();
            this._PnlSend = new System.Windows.Forms.Panel();
            this._BtnSend = new System.Windows.Forms.Button();
            this._CmbEol = new System.Windows.Forms.ComboBox();
            this._LblEol = new System.Windows.Forms.Label();
            this._TxtSend = new System.Windows.Forms.TextBox();
            this._RdoHex = new System.Windows.Forms.RadioButton();
            this._RdoText = new System.Windows.Forms.RadioButton();
            this._RtbOutput = new System.Windows.Forms.RichTextBox();
            this._StatusStrip = new System.Windows.Forms.StatusStrip();
            this._LblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this._LblRxCount = new System.Windows.Forms.ToolStripStatusLabel();
            this._LblTxCount = new System.Windows.Forms.ToolStripStatusLabel();
            this._PnlSettings.SuspendLayout();
            this._PnlView.SuspendLayout();
            this._PnlSend.SuspendLayout();
            this._StatusStrip.SuspendLayout();
            this.SuspendLayout();
            //
            // _PnlSettings
            //
            this._PnlSettings.Controls.Add(this._LblSignals);
            this._PnlSettings.Controls.Add(this._BtnConnect);
            this._PnlSettings.Controls.Add(this._ChkRts);
            this._PnlSettings.Controls.Add(this._ChkDtr);
            this._PnlSettings.Controls.Add(this._CmbFlow);
            this._PnlSettings.Controls.Add(this._LblFlow);
            this._PnlSettings.Controls.Add(this._CmbStopBits);
            this._PnlSettings.Controls.Add(this._LblStopBits);
            this._PnlSettings.Controls.Add(this._CmbParity);
            this._PnlSettings.Controls.Add(this._LblParity);
            this._PnlSettings.Controls.Add(this._CmbDataBits);
            this._PnlSettings.Controls.Add(this._LblDataBits);
            this._PnlSettings.Controls.Add(this._CmbBaud);
            this._PnlSettings.Controls.Add(this._LblBaud);
            this._PnlSettings.Controls.Add(this._BtnRefresh);
            this._PnlSettings.Controls.Add(this._CmbPort);
            this._PnlSettings.Controls.Add(this._LblPort);
            this._PnlSettings.Dock = System.Windows.Forms.DockStyle.Top;
            this._PnlSettings.Location = new System.Drawing.Point(0, 0);
            this._PnlSettings.Name = "_PnlSettings";
            this._PnlSettings.Size = new System.Drawing.Size(1350, 92);
            this._PnlSettings.TabIndex = 0;
            //
            // _LblSignals
            //
            this._LblSignals.AutoSize = true;
            this._LblSignals.Location = new System.Drawing.Point(592, 55);
            this._LblSignals.Name = "_LblSignals";
            this._LblSignals.Size = new System.Drawing.Size(180, 21);
            this._LblSignals.TabIndex = 16;
            this._LblSignals.Text = "CTS -  DSR -  DCD -";
            //
            // _BtnConnect
            //
            this._BtnConnect.Location = new System.Drawing.Point(440, 49);
            this._BtnConnect.Name = "_BtnConnect";
            this._BtnConnect.Size = new System.Drawing.Size(130, 34);
            this._BtnConnect.TabIndex = 15;
            this._BtnConnect.Text = "Open";
            this._BtnConnect.UseVisualStyleBackColor = true;
            this._BtnConnect.Click += new System.EventHandler(this.BtnConnect_Click);
            //
            // _ChkRts
            //
            this._ChkRts.AutoSize = true;
            this._ChkRts.Checked = true;
            this._ChkRts.CheckState = System.Windows.Forms.CheckState.Checked;
            this._ChkRts.Location = new System.Drawing.Point(352, 53);
            this._ChkRts.Name = "_ChkRts";
            this._ChkRts.Size = new System.Drawing.Size(66, 25);
            this._ChkRts.TabIndex = 14;
            this._ChkRts.Text = "RTS";
            this._ChkRts.UseVisualStyleBackColor = true;
            this._ChkRts.CheckedChanged += new System.EventHandler(this.ChkRts_CheckedChanged);
            //
            // _ChkDtr
            //
            this._ChkDtr.AutoSize = true;
            this._ChkDtr.Checked = true;
            this._ChkDtr.CheckState = System.Windows.Forms.CheckState.Checked;
            this._ChkDtr.Location = new System.Drawing.Point(274, 53);
            this._ChkDtr.Name = "_ChkDtr";
            this._ChkDtr.Size = new System.Drawing.Size(67, 25);
            this._ChkDtr.TabIndex = 13;
            this._ChkDtr.Text = "DTR";
            this._ChkDtr.UseVisualStyleBackColor = true;
            this._ChkDtr.CheckedChanged += new System.EventHandler(this.ChkDtr_CheckedChanged);
            //
            // _CmbFlow
            //
            this._CmbFlow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._CmbFlow.Location = new System.Drawing.Point(60, 51);
            this._CmbFlow.Name = "_CmbFlow";
            this._CmbFlow.Size = new System.Drawing.Size(200, 29);
            this._CmbFlow.TabIndex = 12;
            //
            // _LblFlow
            //
            this._LblFlow.AutoSize = true;
            this._LblFlow.Location = new System.Drawing.Point(12, 55);
            this._LblFlow.Name = "_LblFlow";
            this._LblFlow.Size = new System.Drawing.Size(45, 21);
            this._LblFlow.TabIndex = 11;
            this._LblFlow.Text = "Flow";
            //
            // _CmbStopBits
            //
            this._CmbStopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._CmbStopBits.Location = new System.Drawing.Point(884, 9);
            this._CmbStopBits.Name = "_CmbStopBits";
            this._CmbStopBits.Size = new System.Drawing.Size(100, 29);
            this._CmbStopBits.TabIndex = 10;
            //
            // _LblStopBits
            //
            this._LblStopBits.AutoSize = true;
            this._LblStopBits.Location = new System.Drawing.Point(836, 13);
            this._LblStopBits.Name = "_LblStopBits";
            this._LblStopBits.Size = new System.Drawing.Size(43, 21);
            this._LblStopBits.TabIndex = 9;
            this._LblStopBits.Text = "Stop";
            //
            // _CmbParity
            //
            this._CmbParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._CmbParity.Location = new System.Drawing.Point(714, 9);
            this._CmbParity.Name = "_CmbParity";
            this._CmbParity.Size = new System.Drawing.Size(110, 29);
            this._CmbParity.TabIndex = 8;
            //
            // _LblParity
            //
            this._LblParity.AutoSize = true;
            this._LblParity.Location = new System.Drawing.Point(660, 13);
            this._LblParity.Name = "_LblParity";
            this._LblParity.Size = new System.Drawing.Size(51, 21);
            this._LblParity.TabIndex = 7;
            this._LblParity.Text = "Parity";
            //
            // _CmbDataBits
            //
            this._CmbDataBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._CmbDataBits.Location = new System.Drawing.Point(568, 9);
            this._CmbDataBits.Name = "_CmbDataBits";
            this._CmbDataBits.Size = new System.Drawing.Size(80, 29);
            this._CmbDataBits.TabIndex = 6;
            //
            // _LblDataBits
            //
            this._LblDataBits.AutoSize = true;
            this._LblDataBits.Location = new System.Drawing.Point(520, 13);
            this._LblDataBits.Name = "_LblDataBits";
            this._LblDataBits.Size = new System.Drawing.Size(42, 21);
            this._LblDataBits.TabIndex = 5;
            this._LblDataBits.Text = "Data";
            //
            // _CmbBaud
            //
            this._CmbBaud.Location = new System.Drawing.Point(376, 9);
            this._CmbBaud.Name = "_CmbBaud";
            this._CmbBaud.Size = new System.Drawing.Size(130, 29);
            this._CmbBaud.TabIndex = 4;
            //
            // _LblBaud
            //
            this._LblBaud.AutoSize = true;
            this._LblBaud.Location = new System.Drawing.Point(322, 13);
            this._LblBaud.Name = "_LblBaud";
            this._LblBaud.Size = new System.Drawing.Size(48, 21);
            this._LblBaud.TabIndex = 3;
            this._LblBaud.Text = "Baud";
            //
            // _BtnRefresh
            //
            this._BtnRefresh.Location = new System.Drawing.Point(208, 7);
            this._BtnRefresh.Name = "_BtnRefresh";
            this._BtnRefresh.Size = new System.Drawing.Size(100, 33);
            this._BtnRefresh.TabIndex = 2;
            this._BtnRefresh.Text = "Refresh";
            this._BtnRefresh.UseVisualStyleBackColor = true;
            this._BtnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            //
            // _CmbPort
            //
            this._CmbPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._CmbPort.Location = new System.Drawing.Point(60, 9);
            this._CmbPort.Name = "_CmbPort";
            this._CmbPort.Size = new System.Drawing.Size(140, 29);
            this._CmbPort.TabIndex = 1;
            //
            // _LblPort
            //
            this._LblPort.AutoSize = true;
            this._LblPort.Location = new System.Drawing.Point(12, 13);
            this._LblPort.Name = "_LblPort";
            this._LblPort.Size = new System.Drawing.Size(40, 21);
            this._LblPort.TabIndex = 0;
            this._LblPort.Text = "Port";
            //
            // _PnlView
            //
            this._PnlView.Controls.Add(this._BtnSaveLog);
            this._PnlView.Controls.Add(this._BtnClear);
            this._PnlView.Controls.Add(this._ChkLocalEcho);
            this._PnlView.Controls.Add(this._ChkAutoScroll);
            this._PnlView.Controls.Add(this._ChkTimestamp);
            this._PnlView.Controls.Add(this._CmbEncoding);
            this._PnlView.Controls.Add(this._LblEncoding);
            this._PnlView.Controls.Add(this._CmbView);
            this._PnlView.Controls.Add(this._LblView);
            this._PnlView.Dock = System.Windows.Forms.DockStyle.Top;
            this._PnlView.Location = new System.Drawing.Point(0, 92);
            this._PnlView.Name = "_PnlView";
            this._PnlView.Size = new System.Drawing.Size(1350, 46);
            this._PnlView.TabIndex = 1;
            //
            // _BtnSaveLog
            //
            this._BtnSaveLog.Location = new System.Drawing.Point(964, 7);
            this._BtnSaveLog.Name = "_BtnSaveLog";
            this._BtnSaveLog.Size = new System.Drawing.Size(120, 33);
            this._BtnSaveLog.TabIndex = 8;
            this._BtnSaveLog.Text = "Save log";
            this._BtnSaveLog.UseVisualStyleBackColor = true;
            this._BtnSaveLog.Click += new System.EventHandler(this.BtnSaveLog_Click);
            //
            // _BtnClear
            //
            this._BtnClear.Location = new System.Drawing.Point(856, 7);
            this._BtnClear.Name = "_BtnClear";
            this._BtnClear.Size = new System.Drawing.Size(100, 33);
            this._BtnClear.TabIndex = 7;
            this._BtnClear.Text = "Clear";
            this._BtnClear.UseVisualStyleBackColor = true;
            this._BtnClear.Click += new System.EventHandler(this.BtnClear_Click);
            //
            // _ChkLocalEcho
            //
            this._ChkLocalEcho.AutoSize = true;
            this._ChkLocalEcho.Checked = true;
            this._ChkLocalEcho.CheckState = System.Windows.Forms.CheckState.Checked;
            this._ChkLocalEcho.Location = new System.Drawing.Point(732, 11);
            this._ChkLocalEcho.Name = "_ChkLocalEcho";
            this._ChkLocalEcho.Size = new System.Drawing.Size(100, 25);
            this._ChkLocalEcho.TabIndex = 6;
            this._ChkLocalEcho.Text = "Echo TX";
            this._ChkLocalEcho.UseVisualStyleBackColor = true;
            //
            // _ChkAutoScroll
            //
            this._ChkAutoScroll.AutoSize = true;
            this._ChkAutoScroll.Checked = true;
            this._ChkAutoScroll.CheckState = System.Windows.Forms.CheckState.Checked;
            this._ChkAutoScroll.Location = new System.Drawing.Point(600, 11);
            this._ChkAutoScroll.Name = "_ChkAutoScroll";
            this._ChkAutoScroll.Size = new System.Drawing.Size(118, 25);
            this._ChkAutoScroll.TabIndex = 5;
            this._ChkAutoScroll.Text = "Auto scroll";
            this._ChkAutoScroll.UseVisualStyleBackColor = true;
            //
            // _ChkTimestamp
            //
            this._ChkTimestamp.AutoSize = true;
            this._ChkTimestamp.Location = new System.Drawing.Point(468, 11);
            this._ChkTimestamp.Name = "_ChkTimestamp";
            this._ChkTimestamp.Size = new System.Drawing.Size(116, 25);
            this._ChkTimestamp.TabIndex = 4;
            this._ChkTimestamp.Text = "Timestamp";
            this._ChkTimestamp.UseVisualStyleBackColor = true;
            this._ChkTimestamp.CheckedChanged += new System.EventHandler(this.ChkTimestamp_CheckedChanged);
            //
            // _CmbEncoding
            //
            this._CmbEncoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._CmbEncoding.Location = new System.Drawing.Point(272, 9);
            this._CmbEncoding.Name = "_CmbEncoding";
            this._CmbEncoding.Size = new System.Drawing.Size(180, 29);
            this._CmbEncoding.TabIndex = 3;
            this._CmbEncoding.SelectedIndexChanged += new System.EventHandler(this.CmbEncoding_SelectedIndexChanged);
            //
            // _LblEncoding
            //
            this._LblEncoding.AutoSize = true;
            this._LblEncoding.Location = new System.Drawing.Point(186, 13);
            this._LblEncoding.Name = "_LblEncoding";
            this._LblEncoding.Size = new System.Drawing.Size(80, 21);
            this._LblEncoding.TabIndex = 2;
            this._LblEncoding.Text = "Encoding";
            //
            // _CmbView
            //
            this._CmbView.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._CmbView.Location = new System.Drawing.Point(60, 9);
            this._CmbView.Name = "_CmbView";
            this._CmbView.Size = new System.Drawing.Size(110, 29);
            this._CmbView.TabIndex = 1;
            this._CmbView.SelectedIndexChanged += new System.EventHandler(this.CmbView_SelectedIndexChanged);
            //
            // _LblView
            //
            this._LblView.AutoSize = true;
            this._LblView.Location = new System.Drawing.Point(12, 13);
            this._LblView.Name = "_LblView";
            this._LblView.Size = new System.Drawing.Size(44, 21);
            this._LblView.TabIndex = 0;
            this._LblView.Text = "View";
            //
            // _PnlSend
            //
            this._PnlSend.Controls.Add(this._BtnSend);
            this._PnlSend.Controls.Add(this._CmbEol);
            this._PnlSend.Controls.Add(this._LblEol);
            this._PnlSend.Controls.Add(this._TxtSend);
            this._PnlSend.Controls.Add(this._RdoHex);
            this._PnlSend.Controls.Add(this._RdoText);
            this._PnlSend.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._PnlSend.Location = new System.Drawing.Point(0, 649);
            this._PnlSend.Name = "_PnlSend";
            this._PnlSend.Size = new System.Drawing.Size(1350, 52);
            this._PnlSend.TabIndex = 3;
            //
            // _BtnSend
            //
            this._BtnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._BtnSend.Location = new System.Drawing.Point(1208, 10);
            this._BtnSend.Name = "_BtnSend";
            this._BtnSend.Size = new System.Drawing.Size(130, 34);
            this._BtnSend.TabIndex = 5;
            this._BtnSend.Text = "Send";
            this._BtnSend.UseVisualStyleBackColor = true;
            this._BtnSend.Click += new System.EventHandler(this.BtnSend_Click);
            //
            // _CmbEol
            //
            this._CmbEol.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._CmbEol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._CmbEol.Location = new System.Drawing.Point(1086, 12);
            this._CmbEol.Name = "_CmbEol";
            this._CmbEol.Size = new System.Drawing.Size(110, 29);
            this._CmbEol.TabIndex = 4;
            //
            // _LblEol
            //
            this._LblEol.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._LblEol.AutoSize = true;
            this._LblEol.Location = new System.Drawing.Point(1044, 16);
            this._LblEol.Name = "_LblEol";
            this._LblEol.Size = new System.Drawing.Size(36, 21);
            this._LblEol.TabIndex = 3;
            this._LblEol.Text = "EOL";
            //
            // _TxtSend
            //
            this._TxtSend.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._TxtSend.Font = new System.Drawing.Font("Consolas", 12F);
            this._TxtSend.Location = new System.Drawing.Point(170, 12);
            this._TxtSend.Name = "_TxtSend";
            this._TxtSend.Size = new System.Drawing.Size(862, 29);
            this._TxtSend.TabIndex = 2;
            this._TxtSend.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSend_KeyDown);
            //
            // _RdoHex
            //
            this._RdoHex.AutoSize = true;
            this._RdoHex.Location = new System.Drawing.Point(92, 15);
            this._RdoHex.Name = "_RdoHex";
            this._RdoHex.Size = new System.Drawing.Size(66, 25);
            this._RdoHex.TabIndex = 1;
            this._RdoHex.Text = "Hex";
            this._RdoHex.UseVisualStyleBackColor = true;
            //
            // _RdoText
            //
            this._RdoText.AutoSize = true;
            this._RdoText.Checked = true;
            this._RdoText.Location = new System.Drawing.Point(12, 15);
            this._RdoText.Name = "_RdoText";
            this._RdoText.Size = new System.Drawing.Size(68, 25);
            this._RdoText.TabIndex = 0;
            this._RdoText.TabStop = true;
            this._RdoText.Text = "Text";
            this._RdoText.UseVisualStyleBackColor = true;
            //
            // _RtbOutput
            //
            this._RtbOutput.BackColor = System.Drawing.Color.White;
            this._RtbOutput.DetectUrls = false;
            this._RtbOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this._RtbOutput.Font = new System.Drawing.Font("Consolas", 12F);
            this._RtbOutput.HideSelection = false;
            this._RtbOutput.Location = new System.Drawing.Point(0, 138);
            this._RtbOutput.Name = "_RtbOutput";
            this._RtbOutput.ReadOnly = true;
            this._RtbOutput.Size = new System.Drawing.Size(1350, 511);
            this._RtbOutput.TabIndex = 2;
            this._RtbOutput.Text = "";
            this._RtbOutput.WordWrap = true;
            //
            // _StatusStrip
            //
            this._StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._LblStatus,
            this._LblRxCount,
            this._LblTxCount});
            this._StatusStrip.Location = new System.Drawing.Point(0, 701);
            this._StatusStrip.Name = "_StatusStrip";
            this._StatusStrip.Size = new System.Drawing.Size(1350, 28);
            this._StatusStrip.TabIndex = 4;
            //
            // _LblStatus
            //
            this._LblStatus.Name = "_LblStatus";
            this._LblStatus.Size = new System.Drawing.Size(56, 23);
            this._LblStatus.Text = "Closed";
            //
            // _LblRxCount
            //
            this._LblRxCount.Name = "_LblRxCount";
            this._LblRxCount.Size = new System.Drawing.Size(60, 23);
            this._LblRxCount.Text = "RX 0 B";
            //
            // _LblTxCount
            //
            this._LblTxCount.Name = "_LblTxCount";
            this._LblTxCount.Size = new System.Drawing.Size(60, 23);
            this._LblTxCount.Text = "TX 0 B";
            //
            // MainForm
            //
            // Auto scaling is ON, driven by DPI rather than by font metrics.
            //
            // Dpi mode compares AutoScaleDimensions (the design time DPI, 96) with the
            // runtime DPI, so the factor is exactly 1.0 at 100% scaling -- the window
            // really is 1366x768 there -- and exactly 1.5 at 150%, where every
            // coordinate, the window size and the point sized font all grow together.
            //
            // Font mode would compare design time against runtime font metrics
            // (tmAveCharWidth / tmHeight of Segoe UI 12pt). Those constants have to be
            // hard coded in this file, and any mismatch stretches the whole form by the
            // error, per axis. Dpi mode has no such constant to get wrong.
            // Switch to AutoScaleMode.Font here if you prefer the WinForms default; the
            // layout tolerates it, only the startup size drifts a little.
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.ClientSize = new System.Drawing.Size(1350, 729);
            this.Controls.Add(this._RtbOutput);
            this.Controls.Add(this._PnlView);
            this.Controls.Add(this._PnlSettings);
            this.Controls.Add(this._PnlSend);
            this.Controls.Add(this._StatusStrip);
            this.MinimumSize = new System.Drawing.Size(1000, 560);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Serial Terminal";
            // Set last so the startup window is 1366 x 768 at 100% display scaling,
            // whatever the non client metrics of the current theme are. Auto scaling
            // multiplies this by the DPI factor, so it grows on a high DPI monitor.
            this.Size = new System.Drawing.Size(1366, 768);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this._PnlSettings.ResumeLayout(false);
            this._PnlSettings.PerformLayout();
            this._PnlView.ResumeLayout(false);
            this._PnlView.PerformLayout();
            this._PnlSend.ResumeLayout(false);
            this._PnlSend.PerformLayout();
            this._StatusStrip.ResumeLayout(false);
            this._StatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
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
