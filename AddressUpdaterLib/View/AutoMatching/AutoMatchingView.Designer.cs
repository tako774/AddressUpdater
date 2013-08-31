#pragma warning disable 1591

namespace HisoutenSupportTools.AddressUpdater.Lib.View.AutoMatching
{
    partial class AutoMatchingView
    {
        /// <summary> 
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナで生成されたコード

        /// <summary> 
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.containerPanel = new System.Windows.Forms.Panel();
            this.registerCheckBox = new System.Windows.Forms.CheckBox();
            this.resultPanel = new System.Windows.Forms.Panel();
            this.oponentCommentOutput = new System.Windows.Forms.TextBox();
            this.messageLabel = new System.Windows.Forms.Label();
            this.oponentIsRoomOnlyLabel = new System.Windows.Forms.Label();
            this.copyIpButton = new System.Windows.Forms.Button();
            this.oponentRatingLabel = new System.Windows.Forms.Label();
            this.oponentAccountNameOutput = new System.Windows.Forms.TextBox();
            this.skipButton = new System.Windows.Forms.Button();
            this.informationPanel = new System.Windows.Forms.Panel();
            this.commentInput = new System.Windows.Forms.TextBox();
            this.commentLabel = new System.Windows.Forms.Label();
            this.portInput = new System.Windows.Forms.NumericUpDown();
            this.ip_portLabel = new System.Windows.Forms.Label();
            this.ipInput = new System.Windows.Forms.TextBox();
            this.isRoomOnlySelect = new System.Windows.Forms.CheckBox();
            this.isHostableSelect = new System.Windows.Forms.CheckBox();
            this.accountNameLabel = new System.Windows.Forms.Label();
            this.matchingSpanInput = new System.Windows.Forms.NumericUpDown();
            this.accountNameInput = new System.Windows.Forms.TextBox();
            this.matchingSpanLabel = new System.Windows.Forms.Label();
            this.characterSelect = new System.Windows.Forms.ComboBox();
            this.characterLabel = new System.Windows.Forms.Label();
            this.receiveTimer = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.hostCheckTimer = new System.Windows.Forms.Timer(this.components);
            this.textChangeToggle1 = new HisoutenSupportTools.AddressUpdater.Lib.View.TextChangeToggle(this.components);
            this.colorChangeToggle1 = new HisoutenSupportTools.AddressUpdater.Lib.View.ColorChangeToggle(this.components);
            this.textChangeToggle2 = new HisoutenSupportTools.AddressUpdater.Lib.View.TextChangeToggle(this.components);
            this.containerPanel.SuspendLayout();
            this.resultPanel.SuspendLayout();
            this.informationPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.portInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.matchingSpanInput)).BeginInit();
            this.SuspendLayout();
            // 
            // containerPanel
            // 
            this.containerPanel.AutoScroll = true;
            this.containerPanel.Controls.Add(this.registerCheckBox);
            this.containerPanel.Controls.Add(this.resultPanel);
            this.containerPanel.Controls.Add(this.informationPanel);
            this.containerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.containerPanel.Location = new System.Drawing.Point(0, 0);
            this.containerPanel.Name = "containerPanel";
            this.containerPanel.Size = new System.Drawing.Size(659, 259);
            this.containerPanel.TabIndex = 0;
            // 
            // registerCheckBox
            // 
            this.registerCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.registerCheckBox.Location = new System.Drawing.Point(1, 3);
            this.registerCheckBox.Name = "registerCheckBox";
            this.registerCheckBox.Size = new System.Drawing.Size(100, 24);
            this.registerCheckBox.TabIndex = 4;
            this.registerCheckBox.Text = "登録する";
            this.registerCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.registerCheckBox.UseVisualStyleBackColor = true;
            this.registerCheckBox.CheckedChanged += new System.EventHandler(this.registerCheckBox_CheckedChanged);
            // 
            // resultPanel
            // 
            this.resultPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.resultPanel.Controls.Add(this.oponentCommentOutput);
            this.resultPanel.Controls.Add(this.messageLabel);
            this.resultPanel.Controls.Add(this.oponentIsRoomOnlyLabel);
            this.resultPanel.Controls.Add(this.copyIpButton);
            this.resultPanel.Controls.Add(this.oponentRatingLabel);
            this.resultPanel.Controls.Add(this.oponentAccountNameOutput);
            this.resultPanel.Controls.Add(this.skipButton);
            this.resultPanel.Location = new System.Drawing.Point(107, 118);
            this.resultPanel.Name = "resultPanel";
            this.resultPanel.Size = new System.Drawing.Size(449, 134);
            this.resultPanel.TabIndex = 35;
            this.resultPanel.Visible = false;
            // 
            // oponentCommentOutput
            // 
            this.oponentCommentOutput.AcceptsReturn = true;
            this.oponentCommentOutput.Location = new System.Drawing.Point(3, 78);
            this.oponentCommentOutput.Multiline = true;
            this.oponentCommentOutput.Name = "oponentCommentOutput";
            this.oponentCommentOutput.ReadOnly = true;
            this.oponentCommentOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.oponentCommentOutput.Size = new System.Drawing.Size(438, 49);
            this.oponentCommentOutput.TabIndex = 40;
            this.oponentCommentOutput.Visible = false;
            // 
            // messageLabel
            // 
            this.messageLabel.AutoSize = true;
            this.messageLabel.Location = new System.Drawing.Point(3, 6);
            this.messageLabel.Name = "messageLabel";
            this.messageLabel.Size = new System.Drawing.Size(127, 12);
            this.messageLabel.TabIndex = 0;
            this.messageLabel.Text = "対戦相手を探しています...";
            // 
            // oponentIsRoomOnlyLabel
            // 
            this.oponentIsRoomOnlyLabel.AutoSize = true;
            this.oponentIsRoomOnlyLabel.ForeColor = System.Drawing.Color.Red;
            this.oponentIsRoomOnlyLabel.Location = new System.Drawing.Point(304, 56);
            this.oponentIsRoomOnlyLabel.Name = "oponentIsRoomOnlyLabel";
            this.oponentIsRoomOnlyLabel.Size = new System.Drawing.Size(50, 12);
            this.oponentIsRoomOnlyLabel.TabIndex = 16;
            this.oponentIsRoomOnlyLabel.Text = "室内のみ";
            this.oponentIsRoomOnlyLabel.Visible = false;
            // 
            // copyIpButton
            // 
            this.copyIpButton.Location = new System.Drawing.Point(111, 24);
            this.copyIpButton.Name = "copyIpButton";
            this.copyIpButton.Size = new System.Drawing.Size(75, 23);
            this.copyIpButton.TabIndex = 20;
            this.copyIpButton.Text = "IPコピー";
            this.copyIpButton.UseVisualStyleBackColor = true;
            this.copyIpButton.Visible = false;
            this.copyIpButton.Click += new System.EventHandler(this.copyIpButton_Click);
            // 
            // oponentRatingLabel
            // 
            this.oponentRatingLabel.AutoSize = true;
            this.oponentRatingLabel.Location = new System.Drawing.Point(192, 56);
            this.oponentRatingLabel.Name = "oponentRatingLabel";
            this.oponentRatingLabel.Size = new System.Drawing.Size(94, 12);
            this.oponentRatingLabel.TabIndex = 5;
            this.oponentRatingLabel.Text = "相手のキャラ:レート";
            this.oponentRatingLabel.Visible = false;
            // 
            // oponentAccountNameOutput
            // 
            this.oponentAccountNameOutput.Location = new System.Drawing.Point(3, 53);
            this.oponentAccountNameOutput.Name = "oponentAccountNameOutput";
            this.oponentAccountNameOutput.ReadOnly = true;
            this.oponentAccountNameOutput.Size = new System.Drawing.Size(183, 19);
            this.oponentAccountNameOutput.TabIndex = 30;
            this.oponentAccountNameOutput.Text = "相手のアカウント名";
            this.oponentAccountNameOutput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.oponentAccountNameOutput.Visible = false;
            // 
            // skipButton
            // 
            this.skipButton.Location = new System.Drawing.Point(3, 24);
            this.skipButton.Name = "skipButton";
            this.skipButton.Size = new System.Drawing.Size(75, 23);
            this.skipButton.TabIndex = 10;
            this.skipButton.Text = "スキップ";
            this.skipButton.UseVisualStyleBackColor = true;
            this.skipButton.Visible = false;
            this.skipButton.Click += new System.EventHandler(this.skipButton_Click);
            // 
            // informationPanel
            // 
            this.informationPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.informationPanel.Controls.Add(this.commentInput);
            this.informationPanel.Controls.Add(this.commentLabel);
            this.informationPanel.Controls.Add(this.portInput);
            this.informationPanel.Controls.Add(this.ip_portLabel);
            this.informationPanel.Controls.Add(this.ipInput);
            this.informationPanel.Controls.Add(this.isRoomOnlySelect);
            this.informationPanel.Controls.Add(this.isHostableSelect);
            this.informationPanel.Controls.Add(this.accountNameLabel);
            this.informationPanel.Controls.Add(this.matchingSpanInput);
            this.informationPanel.Controls.Add(this.accountNameInput);
            this.informationPanel.Controls.Add(this.matchingSpanLabel);
            this.informationPanel.Controls.Add(this.characterSelect);
            this.informationPanel.Controls.Add(this.characterLabel);
            this.informationPanel.Location = new System.Drawing.Point(107, 3);
            this.informationPanel.Name = "informationPanel";
            this.informationPanel.Size = new System.Drawing.Size(549, 109);
            this.informationPanel.TabIndex = 33;
            // 
            // commentInput
            // 
            this.commentInput.AcceptsReturn = true;
            this.commentInput.Location = new System.Drawing.Point(101, 53);
            this.commentInput.MaxLength = 100;
            this.commentInput.Multiline = true;
            this.commentInput.Name = "commentInput";
            this.commentInput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.commentInput.Size = new System.Drawing.Size(438, 49);
            this.commentInput.TabIndex = 81;
            this.commentInput.Text = "コメント";
            // 
            // commentLabel
            // 
            this.commentLabel.AutoSize = true;
            this.commentLabel.Location = new System.Drawing.Point(57, 56);
            this.commentLabel.Name = "commentLabel";
            this.commentLabel.Size = new System.Drawing.Size(38, 12);
            this.commentLabel.TabIndex = 74;
            this.commentLabel.Text = "コメント";
            // 
            // portInput
            // 
            this.portInput.Location = new System.Drawing.Point(481, 27);
            this.portInput.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.portInput.Name = "portInput";
            this.portInput.Size = new System.Drawing.Size(58, 19);
            this.portInput.TabIndex = 73;
            this.portInput.Value = new decimal(new int[] {
            10800,
            0,
            0,
            0});
            this.portInput.Visible = false;
            // 
            // ip_portLabel
            // 
            this.ip_portLabel.AutoSize = true;
            this.ip_portLabel.Location = new System.Drawing.Point(290, 30);
            this.ip_portLabel.Name = "ip_portLabel";
            this.ip_portLabel.Size = new System.Drawing.Size(45, 12);
            this.ip_portLabel.TabIndex = 71;
            this.ip_portLabel.Text = "IP:ポート";
            this.ip_portLabel.Visible = false;
            // 
            // ipInput
            // 
            this.ipInput.Location = new System.Drawing.Point(341, 27);
            this.ipInput.MaxLength = 15;
            this.ipInput.Name = "ipInput";
            this.ipInput.Size = new System.Drawing.Size(120, 19);
            this.ipInput.TabIndex = 72;
            this.ipInput.Text = "255.255.255.255";
            this.ipInput.Visible = false;
            // 
            // isRoomOnlySelect
            // 
            this.isRoomOnlySelect.AutoSize = true;
            this.isRoomOnlySelect.Location = new System.Drawing.Point(186, 29);
            this.isRoomOnlySelect.Name = "isRoomOnlySelect";
            this.isRoomOnlySelect.Size = new System.Drawing.Size(69, 16);
            this.isRoomOnlySelect.TabIndex = 52;
            this.isRoomOnlySelect.Text = "室内のみ";
            this.isRoomOnlySelect.UseVisualStyleBackColor = true;
            // 
            // isHostableSelect
            // 
            this.isHostableSelect.AutoSize = true;
            this.isHostableSelect.Location = new System.Drawing.Point(101, 29);
            this.isHostableSelect.Name = "isHostableSelect";
            this.isHostableSelect.Size = new System.Drawing.Size(63, 16);
            this.isHostableSelect.TabIndex = 51;
            this.isHostableSelect.Text = "ホスト可";
            this.isHostableSelect.UseVisualStyleBackColor = true;
            this.isHostableSelect.CheckedChanged += new System.EventHandler(this.isHostableSelect_CheckedChanged);
            // 
            // accountNameLabel
            // 
            this.accountNameLabel.AutoSize = true;
            this.accountNameLabel.Location = new System.Drawing.Point(3, 6);
            this.accountNameLabel.Name = "accountNameLabel";
            this.accountNameLabel.Size = new System.Drawing.Size(92, 12);
            this.accountNameLabel.TabIndex = 11;
            this.accountNameLabel.Text = "Tencoアカウント名";
            // 
            // matchingSpanInput
            // 
            this.matchingSpanInput.Location = new System.Drawing.Point(481, 4);
            this.matchingSpanInput.Maximum = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.matchingSpanInput.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.matchingSpanInput.Name = "matchingSpanInput";
            this.matchingSpanInput.Size = new System.Drawing.Size(58, 19);
            this.matchingSpanInput.TabIndex = 31;
            this.matchingSpanInput.Value = new decimal(new int[] {
            150,
            0,
            0,
            0});
            // 
            // accountNameInput
            // 
            this.accountNameInput.Location = new System.Drawing.Point(101, 3);
            this.accountNameInput.MaxLength = 50;
            this.accountNameInput.Name = "accountNameInput";
            this.accountNameInput.Size = new System.Drawing.Size(183, 19);
            this.accountNameInput.TabIndex = 12;
            // 
            // matchingSpanLabel
            // 
            this.matchingSpanLabel.AutoSize = true;
            this.matchingSpanLabel.Location = new System.Drawing.Point(417, 6);
            this.matchingSpanLabel.Name = "matchingSpanLabel";
            this.matchingSpanLabel.Size = new System.Drawing.Size(58, 12);
            this.matchingSpanLabel.TabIndex = 23;
            this.matchingSpanLabel.Text = "マッチ幅 ±";
            // 
            // characterSelect
            // 
            this.characterSelect.DisplayMember = "Key";
            this.characterSelect.DropDownHeight = 350;
            this.characterSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.characterSelect.FormattingEnabled = true;
            this.characterSelect.IntegralHeight = false;
            this.characterSelect.Location = new System.Drawing.Point(341, 3);
            this.characterSelect.Name = "characterSelect";
            this.characterSelect.Size = new System.Drawing.Size(70, 20);
            this.characterSelect.TabIndex = 22;
            this.characterSelect.ValueMember = "Value";
            // 
            // characterLabel
            // 
            this.characterLabel.AutoSize = true;
            this.characterLabel.Location = new System.Drawing.Point(304, 6);
            this.characterLabel.Name = "characterLabel";
            this.characterLabel.Size = new System.Drawing.Size(31, 12);
            this.characterLabel.TabIndex = 21;
            this.characterLabel.Text = "キャラ";
            // 
            // receiveTimer
            // 
            this.receiveTimer.Interval = 5000;
            this.receiveTimer.Tick += new System.EventHandler(this.receiveTimer_Tick);
            // 
            // hostCheckTimer
            // 
            this.hostCheckTimer.Interval = 2000;
            this.hostCheckTimer.Tick += new System.EventHandler(this.hostCheckTimer_Tick);
            // 
            // textChangeToggle1
            // 
            this.textChangeToggle1.CheckedText = "登録中";
            this.textChangeToggle1.NormalText = "登録する";
            this.textChangeToggle1.Target = this.registerCheckBox;
            // 
            // colorChangeToggle1
            // 
            this.colorChangeToggle1.Target = this.registerCheckBox;
            // 
            // textChangeToggle2
            // 
            this.textChangeToggle2.CheckedText = "情報入力 △";
            this.textChangeToggle2.NormalText = "情報入力 ▽";
            this.textChangeToggle2.Target = null;
            // 
            // AutoMatchingView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.containerPanel);
            this.Name = "AutoMatchingView";
            this.Size = new System.Drawing.Size(659, 259);
            this.Load += new System.EventHandler(this.AutoMatchingView_Load);
            this.containerPanel.ResumeLayout(false);
            this.resultPanel.ResumeLayout(false);
            this.resultPanel.PerformLayout();
            this.informationPanel.ResumeLayout(false);
            this.informationPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.portInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.matchingSpanInput)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel containerPanel;
        protected internal System.Windows.Forms.CheckBox registerCheckBox;
        private TextChangeToggle textChangeToggle1;
        private ColorChangeToggle colorChangeToggle1;
        private TextChangeToggle textChangeToggle2;
        private System.Windows.Forms.Timer receiveTimer;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Timer hostCheckTimer;
        protected internal System.Windows.Forms.Panel informationPanel;
        protected internal System.Windows.Forms.Panel resultPanel;
        protected internal System.Windows.Forms.TextBox commentInput;
        protected internal System.Windows.Forms.Label commentLabel;
        protected internal System.Windows.Forms.NumericUpDown portInput;
        protected internal System.Windows.Forms.Label ip_portLabel;
        protected internal System.Windows.Forms.TextBox ipInput;
        protected internal System.Windows.Forms.CheckBox isRoomOnlySelect;
        protected internal System.Windows.Forms.CheckBox isHostableSelect;
        protected internal System.Windows.Forms.Label accountNameLabel;
        protected internal System.Windows.Forms.NumericUpDown matchingSpanInput;
        protected internal System.Windows.Forms.TextBox accountNameInput;
        protected internal System.Windows.Forms.Label matchingSpanLabel;
        protected internal System.Windows.Forms.ComboBox characterSelect;
        protected internal System.Windows.Forms.Label characterLabel;
        protected internal System.Windows.Forms.TextBox oponentCommentOutput;
        protected internal System.Windows.Forms.Label messageLabel;
        protected internal System.Windows.Forms.Label oponentIsRoomOnlyLabel;
        protected internal System.Windows.Forms.Button copyIpButton;
        protected internal System.Windows.Forms.Label oponentRatingLabel;
        protected internal System.Windows.Forms.TextBox oponentAccountNameOutput;
        protected internal System.Windows.Forms.Button skipButton;
    }
}

#pragma warning restore 1591
