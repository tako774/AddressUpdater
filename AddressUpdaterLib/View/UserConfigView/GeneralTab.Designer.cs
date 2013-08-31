namespace HisoutenSupportTools.AddressUpdater.Lib.View.UserConfigView
{
    partial class GeneralTab
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
            this.waitingColorDialog = new System.Windows.Forms.ColorDialog();
            this.toolBackColorDialog = new System.Windows.Forms.ColorDialog();
            this.themeNameLabel = new System.Windows.Forms.Label();
            this.chatBackColorLabel = new System.Windows.Forms.Label();
            this.chatForeColorDialog = new System.Windows.Forms.ColorDialog();
            this.chatBackColorPanel = new System.Windows.Forms.Panel();
            this.chatForeColorPanel = new System.Windows.Forms.Panel();
            this.chatForeColorLabel = new System.Windows.Forms.Label();
            this.toolBackColorLabel = new System.Windows.Forms.Label();
            this.fightingHostColorPanel = new System.Windows.Forms.Panel();
            this.themeNameInput = new System.Windows.Forms.TextBox();
            this.chatBackColorDialog = new System.Windows.Forms.ColorDialog();
            this.saveThemeButton = new System.Windows.Forms.Button();
            this.horizontalRadioButton = new System.Windows.Forms.RadioButton();
            this.themeListBox = new System.Windows.Forms.ListBox();
            this.generalTextColorLabel = new System.Windows.Forms.Label();
            this.themeLoader = new HisoutenSupportTools.AddressUpdater.Lib.IO.ThemeLoader(this.components);
            this.generalTextColorPanel = new System.Windows.Forms.Panel();
            this.generalTextColorDialog = new System.Windows.Forms.ColorDialog();
            this.toolBackColorPanel = new System.Windows.Forms.Panel();
            this.fightingHostColorLabel = new System.Windows.Forms.Label();
            this.waitingHostColorPanel = new System.Windows.Forms.Panel();
            this.waitingHostColorLabel = new System.Windows.Forms.Label();
            this.clientDivisionOrientationGroupBox = new System.Windows.Forms.GroupBox();
            this.verticalRadioButton = new System.Windows.Forms.RadioButton();
            this.clientDivisionOrientationLabel = new System.Windows.Forms.Label();
            this.ipLabel = new System.Windows.Forms.Label();
            this.updateSpanInput = new System.Windows.Forms.NumericUpDown();
            this.updateSpanLabel = new System.Windows.Forms.Label();
            this.multiBootLabel = new System.Windows.Forms.Label();
            this.ipInput = new System.Windows.Forms.TextBox();
            this.portInput = new System.Windows.Forms.NumericUpDown();
            this.chatPrefixLabel = new System.Windows.Forms.Label();
            this.isDisableMultiBootCheckBox = new System.Windows.Forms.CheckBox();
            this.chatPrefixInput = new System.Windows.Forms.TextBox();
            this.fightingColorDialog = new System.Windows.Forms.ColorDialog();
            this.portLabel = new System.Windows.Forms.Label();
            this.tournamentLabel = new System.Windows.Forms.Label();
            this.isVisibleTournamentCheckBox = new System.Windows.Forms.CheckBox();
            this.fontDialog = new System.Windows.Forms.FontDialog();
            this.hostFontSelect = new System.Windows.Forms.ComboBox();
            this.hostFontLabel = new System.Windows.Forms.Label();
            this.chatFontLabel = new System.Windows.Forms.Label();
            this.chatFontSelect = new System.Windows.Forms.ComboBox();
            this.clientDivisionOrientationGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updateSpanInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.portInput)).BeginInit();
            this.SuspendLayout();
            // 
            // themeNameLabel
            // 
            this.themeNameLabel.AutoSize = true;
            this.themeNameLabel.Location = new System.Drawing.Point(342, 11);
            this.themeNameLabel.Name = "themeNameLabel";
            this.themeNameLabel.Size = new System.Drawing.Size(45, 12);
            this.themeNameLabel.TabIndex = 382;
            this.themeNameLabel.Text = "テーマ名";
            // 
            // chatBackColorLabel
            // 
            this.chatBackColorLabel.AutoSize = true;
            this.chatBackColorLabel.Location = new System.Drawing.Point(342, 177);
            this.chatBackColorLabel.Name = "chatBackColorLabel";
            this.chatBackColorLabel.Size = new System.Drawing.Size(73, 12);
            this.chatBackColorLabel.TabIndex = 378;
            this.chatBackColorLabel.Text = "チャット背景色";
            // 
            // chatBackColorPanel
            // 
            this.chatBackColorPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.chatBackColorPanel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chatBackColorPanel.Location = new System.Drawing.Point(462, 171);
            this.chatBackColorPanel.Name = "chatBackColorPanel";
            this.chatBackColorPanel.Size = new System.Drawing.Size(113, 23);
            this.chatBackColorPanel.TabIndex = 381;
            this.chatBackColorPanel.Click += new System.EventHandler(this.chatBackColorpanel_Click);
            // 
            // chatForeColorPanel
            // 
            this.chatForeColorPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.chatForeColorPanel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chatForeColorPanel.Location = new System.Drawing.Point(462, 143);
            this.chatForeColorPanel.Name = "chatForeColorPanel";
            this.chatForeColorPanel.Size = new System.Drawing.Size(113, 23);
            this.chatForeColorPanel.TabIndex = 379;
            this.chatForeColorPanel.Click += new System.EventHandler(this.chatForeColorPanel_Click);
            // 
            // chatForeColorLabel
            // 
            this.chatForeColorLabel.AutoSize = true;
            this.chatForeColorLabel.Location = new System.Drawing.Point(342, 149);
            this.chatForeColorLabel.Name = "chatForeColorLabel";
            this.chatForeColorLabel.Size = new System.Drawing.Size(73, 12);
            this.chatForeColorLabel.TabIndex = 377;
            this.chatForeColorLabel.Text = "チャット文字色";
            // 
            // toolBackColorLabel
            // 
            this.toolBackColorLabel.AutoSize = true;
            this.toolBackColorLabel.Location = new System.Drawing.Point(342, 37);
            this.toolBackColorLabel.Name = "toolBackColorLabel";
            this.toolBackColorLabel.Size = new System.Drawing.Size(70, 12);
            this.toolBackColorLabel.TabIndex = 372;
            this.toolBackColorLabel.Text = "ツール背景色";
            // 
            // fightingHostColorPanel
            // 
            this.fightingHostColorPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fightingHostColorPanel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.fightingHostColorPanel.Location = new System.Drawing.Point(462, 115);
            this.fightingHostColorPanel.Name = "fightingHostColorPanel";
            this.fightingHostColorPanel.Size = new System.Drawing.Size(113, 23);
            this.fightingHostColorPanel.TabIndex = 376;
            this.fightingHostColorPanel.Click += new System.EventHandler(this.fightingHostColorPanel_Click);
            // 
            // themeNameInput
            // 
            this.themeNameInput.Location = new System.Drawing.Point(462, 8);
            this.themeNameInput.Name = "themeNameInput";
            this.themeNameInput.Size = new System.Drawing.Size(113, 19);
            this.themeNameInput.TabIndex = 80;
            // 
            // saveThemeButton
            // 
            this.saveThemeButton.Location = new System.Drawing.Point(581, 6);
            this.saveThemeButton.Name = "saveThemeButton";
            this.saveThemeButton.Size = new System.Drawing.Size(115, 23);
            this.saveThemeButton.TabIndex = 90;
            this.saveThemeButton.Text = "テーマ保存";
            this.saveThemeButton.UseVisualStyleBackColor = true;
            this.saveThemeButton.Click += new System.EventHandler(this.saveThemeButton_Click);
            // 
            // horizontalRadioButton
            // 
            this.horizontalRadioButton.AutoSize = true;
            this.horizontalRadioButton.Location = new System.Drawing.Point(6, 15);
            this.horizontalRadioButton.Name = "horizontalRadioButton";
            this.horizontalRadioButton.Size = new System.Drawing.Size(35, 16);
            this.horizontalRadioButton.TabIndex = 0;
            this.horizontalRadioButton.Text = "横";
            this.horizontalRadioButton.UseVisualStyleBackColor = true;
            // 
            // themeListBox
            // 
            this.themeListBox.FormattingEnabled = true;
            this.themeListBox.ItemHeight = 12;
            this.themeListBox.Location = new System.Drawing.Point(581, 33);
            this.themeListBox.Name = "themeListBox";
            this.themeListBox.Size = new System.Drawing.Size(115, 160);
            this.themeListBox.TabIndex = 100;
            this.themeListBox.SelectedIndexChanged += new System.EventHandler(this.themeListBox_SelectedIndexChanged);
            // 
            // generalTextColorLabel
            // 
            this.generalTextColorLabel.AutoSize = true;
            this.generalTextColorLabel.Location = new System.Drawing.Point(342, 65);
            this.generalTextColorLabel.Name = "generalTextColorLabel";
            this.generalTextColorLabel.Size = new System.Drawing.Size(87, 12);
            this.generalTextColorLabel.TabIndex = 380;
            this.generalTextColorLabel.Text = "一般のテキスト色";
            // 
            // generalTextColorPanel
            // 
            this.generalTextColorPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.generalTextColorPanel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.generalTextColorPanel.Location = new System.Drawing.Point(462, 59);
            this.generalTextColorPanel.Name = "generalTextColorPanel";
            this.generalTextColorPanel.Size = new System.Drawing.Size(113, 23);
            this.generalTextColorPanel.TabIndex = 374;
            this.generalTextColorPanel.Click += new System.EventHandler(this.generalTextColorPanel_Click);
            // 
            // toolBackColorPanel
            // 
            this.toolBackColorPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.toolBackColorPanel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.toolBackColorPanel.Location = new System.Drawing.Point(462, 31);
            this.toolBackColorPanel.Name = "toolBackColorPanel";
            this.toolBackColorPanel.Size = new System.Drawing.Size(113, 23);
            this.toolBackColorPanel.TabIndex = 373;
            this.toolBackColorPanel.Click += new System.EventHandler(this.toolColorPanel_Click);
            // 
            // fightingHostColorLabel
            // 
            this.fightingHostColorLabel.AutoSize = true;
            this.fightingHostColorLabel.Location = new System.Drawing.Point(342, 121);
            this.fightingHostColorLabel.Name = "fightingHostColorLabel";
            this.fightingHostColorLabel.Size = new System.Drawing.Size(104, 12);
            this.fightingHostColorLabel.TabIndex = 371;
            this.fightingHostColorLabel.Text = "対戦中ホスト背景色";
            // 
            // waitingHostColorPanel
            // 
            this.waitingHostColorPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.waitingHostColorPanel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.waitingHostColorPanel.Location = new System.Drawing.Point(462, 87);
            this.waitingHostColorPanel.Name = "waitingHostColorPanel";
            this.waitingHostColorPanel.Size = new System.Drawing.Size(113, 23);
            this.waitingHostColorPanel.TabIndex = 375;
            this.waitingHostColorPanel.Click += new System.EventHandler(this.waitingHostColorPanel_Click);
            // 
            // waitingHostColorLabel
            // 
            this.waitingHostColorLabel.AutoSize = true;
            this.waitingHostColorLabel.Location = new System.Drawing.Point(342, 93);
            this.waitingHostColorLabel.Name = "waitingHostColorLabel";
            this.waitingHostColorLabel.Size = new System.Drawing.Size(104, 12);
            this.waitingHostColorLabel.TabIndex = 370;
            this.waitingHostColorLabel.Text = "待機中ホスト背景色";
            // 
            // clientDivisionOrientationGroupBox
            // 
            this.clientDivisionOrientationGroupBox.Controls.Add(this.horizontalRadioButton);
            this.clientDivisionOrientationGroupBox.Controls.Add(this.verticalRadioButton);
            this.clientDivisionOrientationGroupBox.Location = new System.Drawing.Point(125, 156);
            this.clientDivisionOrientationGroupBox.Name = "clientDivisionOrientationGroupBox";
            this.clientDivisionOrientationGroupBox.Size = new System.Drawing.Size(187, 41);
            this.clientDivisionOrientationGroupBox.TabIndex = 70;
            this.clientDivisionOrientationGroupBox.TabStop = false;
            // 
            // verticalRadioButton
            // 
            this.verticalRadioButton.AutoSize = true;
            this.verticalRadioButton.Location = new System.Drawing.Point(99, 15);
            this.verticalRadioButton.Name = "verticalRadioButton";
            this.verticalRadioButton.Size = new System.Drawing.Size(35, 16);
            this.verticalRadioButton.TabIndex = 10;
            this.verticalRadioButton.Text = "縦";
            this.verticalRadioButton.UseVisualStyleBackColor = true;
            // 
            // clientDivisionOrientationLabel
            // 
            this.clientDivisionOrientationLabel.AutoSize = true;
            this.clientDivisionOrientationLabel.Location = new System.Drawing.Point(5, 175);
            this.clientDivisionOrientationLabel.Name = "clientDivisionOrientationLabel";
            this.clientDivisionOrientationLabel.Size = new System.Drawing.Size(92, 12);
            this.clientDivisionOrientationLabel.TabIndex = 369;
            this.clientDivisionOrientationLabel.Text = "クライアント分割線";
            // 
            // ipLabel
            // 
            this.ipLabel.AutoSize = true;
            this.ipLabel.Location = new System.Drawing.Point(5, 11);
            this.ipLabel.Name = "ipLabel";
            this.ipLabel.Size = new System.Drawing.Size(63, 12);
            this.ipLabel.TabIndex = 364;
            this.ipLabel.Text = "初期入力IP";
            // 
            // updateSpanInput
            // 
            this.updateSpanInput.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.updateSpanInput.Location = new System.Drawing.Point(125, 62);
            this.updateSpanInput.Margin = new System.Windows.Forms.Padding(4);
            this.updateSpanInput.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.updateSpanInput.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.updateSpanInput.Name = "updateSpanInput";
            this.updateSpanInput.Size = new System.Drawing.Size(187, 19);
            this.updateSpanInput.TabIndex = 30;
            this.updateSpanInput.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // updateSpanLabel
            // 
            this.updateSpanLabel.AutoSize = true;
            this.updateSpanLabel.Location = new System.Drawing.Point(5, 64);
            this.updateSpanLabel.Name = "updateSpanLabel";
            this.updateSpanLabel.Size = new System.Drawing.Size(73, 12);
            this.updateSpanLabel.TabIndex = 366;
            this.updateSpanLabel.Text = "更新間隔(秒)";
            // 
            // multiBootLabel
            // 
            this.multiBootLabel.AutoSize = true;
            this.multiBootLabel.Location = new System.Drawing.Point(5, 112);
            this.multiBootLabel.Name = "multiBootLabel";
            this.multiBootLabel.Size = new System.Drawing.Size(53, 12);
            this.multiBootLabel.TabIndex = 367;
            this.multiBootLabel.Text = "多重起動";
            // 
            // ipInput
            // 
            this.ipInput.Location = new System.Drawing.Point(125, 8);
            this.ipInput.Margin = new System.Windows.Forms.Padding(4);
            this.ipInput.MaxLength = 15;
            this.ipInput.Name = "ipInput";
            this.ipInput.Size = new System.Drawing.Size(187, 19);
            this.ipInput.TabIndex = 10;
            this.ipInput.TextChanged += new System.EventHandler(this.ipInput_TextChanged);
            // 
            // portInput
            // 
            this.portInput.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.portInput.Location = new System.Drawing.Point(125, 35);
            this.portInput.Margin = new System.Windows.Forms.Padding(4);
            this.portInput.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.portInput.Name = "portInput";
            this.portInput.Size = new System.Drawing.Size(187, 19);
            this.portInput.TabIndex = 20;
            this.portInput.Value = new decimal(new int[] {
            10800,
            0,
            0,
            0});
            // 
            // chatPrefixLabel
            // 
            this.chatPrefixLabel.AutoSize = true;
            this.chatPrefixLabel.Location = new System.Drawing.Point(5, 137);
            this.chatPrefixLabel.Name = "chatPrefixLabel";
            this.chatPrefixLabel.Size = new System.Drawing.Size(97, 12);
            this.chatPrefixLabel.TabIndex = 368;
            this.chatPrefixLabel.Text = "チャット先頭文字列";
            // 
            // isDisableMultiBootCheckBox
            // 
            this.isDisableMultiBootCheckBox.AutoSize = true;
            this.isDisableMultiBootCheckBox.Location = new System.Drawing.Point(125, 111);
            this.isDisableMultiBootCheckBox.Name = "isDisableMultiBootCheckBox";
            this.isDisableMultiBootCheckBox.Size = new System.Drawing.Size(124, 16);
            this.isDisableMultiBootCheckBox.TabIndex = 50;
            this.isDisableMultiBootCheckBox.Text = "多重起動を禁止する";
            this.isDisableMultiBootCheckBox.UseVisualStyleBackColor = true;
            // 
            // chatPrefixInput
            // 
            this.chatPrefixInput.Location = new System.Drawing.Point(125, 134);
            this.chatPrefixInput.MaxLength = 20;
            this.chatPrefixInput.Name = "chatPrefixInput";
            this.chatPrefixInput.Size = new System.Drawing.Size(187, 19);
            this.chatPrefixInput.TabIndex = 60;
            // 
            // portLabel
            // 
            this.portLabel.AutoSize = true;
            this.portLabel.Location = new System.Drawing.Point(5, 37);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(81, 12);
            this.portLabel.TabIndex = 365;
            this.portLabel.Text = "初期入力ポート";
            // 
            // tournamentLabel
            // 
            this.tournamentLabel.AutoSize = true;
            this.tournamentLabel.Location = new System.Drawing.Point(5, 90);
            this.tournamentLabel.Name = "tournamentLabel";
            this.tournamentLabel.Size = new System.Drawing.Size(29, 12);
            this.tournamentLabel.TabIndex = 384;
            this.tournamentLabel.Text = "大会";
            // 
            // isVisibleTournamentCheckBox
            // 
            this.isVisibleTournamentCheckBox.AutoSize = true;
            this.isVisibleTournamentCheckBox.Location = new System.Drawing.Point(125, 89);
            this.isVisibleTournamentCheckBox.Name = "isVisibleTournamentCheckBox";
            this.isVisibleTournamentCheckBox.Size = new System.Drawing.Size(100, 16);
            this.isVisibleTournamentCheckBox.TabIndex = 40;
            this.isVisibleTournamentCheckBox.Text = "大会を表示する";
            this.isVisibleTournamentCheckBox.UseVisualStyleBackColor = true;
            // 
            // fontDialog
            // 
            this.fontDialog.ShowEffects = false;
            // 
            // hostFontSelect
            // 
            this.hostFontSelect.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.hostFontSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.hostFontSelect.FormattingEnabled = true;
            this.hostFontSelect.Location = new System.Drawing.Point(462, 200);
            this.hostFontSelect.Name = "hostFontSelect";
            this.hostFontSelect.Size = new System.Drawing.Size(234, 20);
            this.hostFontSelect.TabIndex = 110;
            this.hostFontSelect.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.fontSelect_DrawItem);
            this.hostFontSelect.SelectedIndexChanged += new System.EventHandler(this.hostFontSelect_SelectedIndexChanged);
            // 
            // hostFontLabel
            // 
            this.hostFontLabel.AutoSize = true;
            this.hostFontLabel.Location = new System.Drawing.Point(342, 203);
            this.hostFontLabel.Name = "hostFontLabel";
            this.hostFontLabel.Size = new System.Drawing.Size(89, 12);
            this.hostFontLabel.TabIndex = 388;
            this.hostFontLabel.Text = "ホスト表示フォント";
            // 
            // chatFontLabel
            // 
            this.chatFontLabel.AutoSize = true;
            this.chatFontLabel.Location = new System.Drawing.Point(342, 229);
            this.chatFontLabel.Name = "chatFontLabel";
            this.chatFontLabel.Size = new System.Drawing.Size(94, 12);
            this.chatFontLabel.TabIndex = 390;
            this.chatFontLabel.Text = "チャット表示フォント";
            // 
            // chatFontSelect
            // 
            this.chatFontSelect.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.chatFontSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.chatFontSelect.FormattingEnabled = true;
            this.chatFontSelect.Location = new System.Drawing.Point(462, 226);
            this.chatFontSelect.Name = "chatFontSelect";
            this.chatFontSelect.Size = new System.Drawing.Size(234, 20);
            this.chatFontSelect.TabIndex = 120;
            this.chatFontSelect.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.fontSelect_DrawItem);
            this.chatFontSelect.SelectedIndexChanged += new System.EventHandler(this.chatFontSelect_SelectedIndexChanged);
            // 
            // GeneralTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chatFontLabel);
            this.Controls.Add(this.chatFontSelect);
            this.Controls.Add(this.hostFontLabel);
            this.Controls.Add(this.hostFontSelect);
            this.Controls.Add(this.tournamentLabel);
            this.Controls.Add(this.isVisibleTournamentCheckBox);
            this.Controls.Add(this.themeNameLabel);
            this.Controls.Add(this.chatBackColorLabel);
            this.Controls.Add(this.chatBackColorPanel);
            this.Controls.Add(this.chatForeColorPanel);
            this.Controls.Add(this.chatForeColorLabel);
            this.Controls.Add(this.toolBackColorLabel);
            this.Controls.Add(this.fightingHostColorPanel);
            this.Controls.Add(this.themeNameInput);
            this.Controls.Add(this.saveThemeButton);
            this.Controls.Add(this.themeListBox);
            this.Controls.Add(this.generalTextColorLabel);
            this.Controls.Add(this.generalTextColorPanel);
            this.Controls.Add(this.toolBackColorPanel);
            this.Controls.Add(this.fightingHostColorLabel);
            this.Controls.Add(this.waitingHostColorPanel);
            this.Controls.Add(this.waitingHostColorLabel);
            this.Controls.Add(this.clientDivisionOrientationGroupBox);
            this.Controls.Add(this.clientDivisionOrientationLabel);
            this.Controls.Add(this.ipLabel);
            this.Controls.Add(this.updateSpanInput);
            this.Controls.Add(this.updateSpanLabel);
            this.Controls.Add(this.multiBootLabel);
            this.Controls.Add(this.ipInput);
            this.Controls.Add(this.portInput);
            this.Controls.Add(this.chatPrefixLabel);
            this.Controls.Add(this.isDisableMultiBootCheckBox);
            this.Controls.Add(this.chatPrefixInput);
            this.Controls.Add(this.portLabel);
            this.Name = "GeneralTab";
            this.Size = new System.Drawing.Size(700, 250);
            this.Load += new System.EventHandler(this.GeneralTab_Load);
            this.clientDivisionOrientationGroupBox.ResumeLayout(false);
            this.clientDivisionOrientationGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updateSpanInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.portInput)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ColorDialog waitingColorDialog;
        private System.Windows.Forms.ColorDialog toolBackColorDialog;
        private System.Windows.Forms.Label themeNameLabel;
        private System.Windows.Forms.Label chatBackColorLabel;
        private System.Windows.Forms.ColorDialog chatForeColorDialog;
        private System.Windows.Forms.Panel chatBackColorPanel;
        private System.Windows.Forms.Panel chatForeColorPanel;
        private System.Windows.Forms.Label chatForeColorLabel;
        private System.Windows.Forms.Label toolBackColorLabel;
        private System.Windows.Forms.Panel fightingHostColorPanel;
        private System.Windows.Forms.TextBox themeNameInput;
        private System.Windows.Forms.ColorDialog chatBackColorDialog;
        private System.Windows.Forms.Button saveThemeButton;
        private System.Windows.Forms.RadioButton horizontalRadioButton;
        private System.Windows.Forms.ListBox themeListBox;
        private System.Windows.Forms.Label generalTextColorLabel;
        private HisoutenSupportTools.AddressUpdater.Lib.IO.ThemeLoader themeLoader;
        private System.Windows.Forms.Panel generalTextColorPanel;
        private System.Windows.Forms.ColorDialog generalTextColorDialog;
        private System.Windows.Forms.Panel toolBackColorPanel;
        private System.Windows.Forms.Label fightingHostColorLabel;
        private System.Windows.Forms.Panel waitingHostColorPanel;
        private System.Windows.Forms.Label waitingHostColorLabel;
        private System.Windows.Forms.GroupBox clientDivisionOrientationGroupBox;
        private System.Windows.Forms.RadioButton verticalRadioButton;
        private System.Windows.Forms.Label clientDivisionOrientationLabel;
        private System.Windows.Forms.Label ipLabel;
        private System.Windows.Forms.NumericUpDown updateSpanInput;
        private System.Windows.Forms.Label updateSpanLabel;
        private System.Windows.Forms.Label multiBootLabel;
        private System.Windows.Forms.TextBox ipInput;
        private System.Windows.Forms.NumericUpDown portInput;
        private System.Windows.Forms.Label chatPrefixLabel;
        private System.Windows.Forms.CheckBox isDisableMultiBootCheckBox;
        private System.Windows.Forms.TextBox chatPrefixInput;
        private System.Windows.Forms.ColorDialog fightingColorDialog;
        private System.Windows.Forms.Label portLabel;
        private System.Windows.Forms.Label tournamentLabel;
        private System.Windows.Forms.CheckBox isVisibleTournamentCheckBox;
        private System.Windows.Forms.FontDialog fontDialog;
        private System.Windows.Forms.ComboBox hostFontSelect;
        private System.Windows.Forms.Label hostFontLabel;
        private System.Windows.Forms.Label chatFontLabel;
        private System.Windows.Forms.ComboBox chatFontSelect;
    }
}
