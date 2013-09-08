namespace HisoutenSupportTools.AddressUpdater.View
{
    partial class ClientTab
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.receiveHostCheckBox = new System.Windows.Forms.CheckBox();
            this.receiveChatCheckBox = new System.Windows.Forms.CheckBox();
            this.autoMatchingCheckBox = new System.Windows.Forms.CheckBox();
            this.registerHostCheckBox = new System.Windows.Forms.CheckBox();
            this.rankFilterComboBox = new System.Windows.Forms.ComboBox();
            this.addressTxtUpdateCheckBox = new System.Windows.Forms.CheckBox();
            this.reverseDirectionCheckBox = new System.Windows.Forms.CheckBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.hostListView = new HisoutenSupportTools.AddressUpdater.Lib.View.HostListView();
            this.hostListContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.クリップボードにコピーToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.大会に参加ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.このIPのホストを表示しないToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoMatchingViewPanel = new System.Windows.Forms.Panel();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.chatView = new HisoutenSupportTools.AddressUpdater.Lib.View.ChatView();
            this.chatContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.コピーToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.古いチャットを削除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.遮断するToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chatViewModel = new HisoutenSupportTools.AddressUpdater.Lib.ViewModel.ChatViewModel(this.components);
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.sendButton = new System.Windows.Forms.Button();
            this.chatInput = new System.Windows.Forms.TextBox();
            this.autoMatchingViewModel = new HisoutenSupportTools.AddressUpdater.Lib.ViewModel.AutoMatching.AutoMatchingViewModel(this.components);
            this.colorChangeToggle1 = new HisoutenSupportTools.AddressUpdater.Lib.View.ColorChangeToggle(this.components);
            this.colorChangeToggle2 = new HisoutenSupportTools.AddressUpdater.Lib.View.ColorChangeToggle(this.components);
            this.textChangeToggle1 = new HisoutenSupportTools.AddressUpdater.Lib.View.TextChangeToggle(this.components);
            this.textChangeToggle2 = new HisoutenSupportTools.AddressUpdater.Lib.View.TextChangeToggle(this.components);
            this.colorChangeToggle3 = new HisoutenSupportTools.AddressUpdater.Lib.View.ColorChangeToggle(this.components);
            this.textChangeToggle3 = new HisoutenSupportTools.AddressUpdater.Lib.View.TextChangeToggle(this.components);
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.hostController = new HisoutenSupportTools.AddressUpdater.Lib.Controller.HostController(this.components);
            this.addressTxtUpdater = new HisoutenSupportTools.AddressUpdater.Lib.IO.AddressTxtUpdater(this.components);
            this.receiveTimer = new System.Windows.Forms.Timer(this.components);
            this.colorChangeToggle4 = new HisoutenSupportTools.AddressUpdater.Lib.View.ColorChangeToggle(this.components);
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.hostListContextMenuStrip.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.chatContextMenuStrip.SuspendLayout();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.flowLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(810, 200);
            this.splitContainer1.SplitterDistance = 34;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 2;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.receiveHostCheckBox);
            this.flowLayoutPanel1.Controls.Add(this.receiveChatCheckBox);
            this.flowLayoutPanel1.Controls.Add(this.autoMatchingCheckBox);
            this.flowLayoutPanel1.Controls.Add(this.registerHostCheckBox);
            this.flowLayoutPanel1.Controls.Add(this.rankFilterComboBox);
            this.flowLayoutPanel1.Controls.Add(this.addressTxtUpdateCheckBox);
            this.flowLayoutPanel1.Controls.Add(this.reverseDirectionCheckBox);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(810, 34);
            this.flowLayoutPanel1.TabIndex = 61;
            // 
            // receiveHostCheckBox
            // 
            this.receiveHostCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.receiveHostCheckBox.BackColor = System.Drawing.SystemColors.Control;
            this.receiveHostCheckBox.Location = new System.Drawing.Point(3, 5);
            this.receiveHostCheckBox.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.receiveHostCheckBox.Name = "receiveHostCheckBox";
            this.receiveHostCheckBox.Size = new System.Drawing.Size(100, 24);
            this.receiveHostCheckBox.TabIndex = 10;
            this.receiveHostCheckBox.Text = "一覧の更新";
            this.receiveHostCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.receiveHostCheckBox.UseVisualStyleBackColor = true;
            this.receiveHostCheckBox.CheckedChanged += new System.EventHandler(this.receiveHostCheckBox_CheckedChanged);
            // 
            // receiveChatCheckBox
            // 
            this.receiveChatCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.receiveChatCheckBox.BackColor = System.Drawing.SystemColors.Control;
            this.receiveChatCheckBox.Location = new System.Drawing.Point(109, 5);
            this.receiveChatCheckBox.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.receiveChatCheckBox.Name = "receiveChatCheckBox";
            this.receiveChatCheckBox.Size = new System.Drawing.Size(100, 24);
            this.receiveChatCheckBox.TabIndex = 20;
            this.receiveChatCheckBox.Text = "チャット開始";
            this.receiveChatCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.receiveChatCheckBox.UseVisualStyleBackColor = true;
            this.receiveChatCheckBox.CheckedChanged += new System.EventHandler(this.receiveChatCheckBox_CheckedChanged);
            // 
            // autoMatchingCheckBox
            // 
            this.autoMatchingCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.autoMatchingCheckBox.BackColor = System.Drawing.SystemColors.Control;
            this.autoMatchingCheckBox.Enabled = false;
            this.autoMatchingCheckBox.Location = new System.Drawing.Point(215, 5);
            this.autoMatchingCheckBox.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.autoMatchingCheckBox.Name = "autoMatchingCheckBox";
            this.autoMatchingCheckBox.Size = new System.Drawing.Size(100, 24);
            this.autoMatchingCheckBox.TabIndex = 25;
            this.autoMatchingCheckBox.Text = "自動マッチング";
            this.autoMatchingCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.autoMatchingCheckBox.UseVisualStyleBackColor = true;
            this.autoMatchingCheckBox.Visible = false;
            this.autoMatchingCheckBox.CheckedChanged += new System.EventHandler(this.autoMatchingCheckBox_CheckedChanged);
            // 
            // registerHostCheckBox
            // 
            this.registerHostCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.registerHostCheckBox.BackColor = System.Drawing.SystemColors.Control;
            this.registerHostCheckBox.Location = new System.Drawing.Point(321, 5);
            this.registerHostCheckBox.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.registerHostCheckBox.Name = "registerHostCheckBox";
            this.registerHostCheckBox.Size = new System.Drawing.Size(100, 24);
            this.registerHostCheckBox.TabIndex = 30;
            this.registerHostCheckBox.Text = "ホスト登録";
            this.registerHostCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.registerHostCheckBox.UseVisualStyleBackColor = true;
            this.registerHostCheckBox.CheckedChanged += new System.EventHandler(this.registerHostCheckBox_CheckedChanged);
            // 
            // rankFilterComboBox
            // 
            this.rankFilterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.rankFilterComboBox.FormattingEnabled = true;
            this.rankFilterComboBox.Items.AddRange(new object[] {
            "全て表示"});
            this.rankFilterComboBox.Location = new System.Drawing.Point(427, 7);
            this.rankFilterComboBox.Margin = new System.Windows.Forms.Padding(3, 7, 3, 3);
            this.rankFilterComboBox.Name = "rankFilterComboBox";
            this.rankFilterComboBox.Size = new System.Drawing.Size(133, 20);
            this.rankFilterComboBox.TabIndex = 40;
            this.rankFilterComboBox.SelectedIndexChanged += new System.EventHandler(this.rankFilterComboBox_SelectedIndexChanged);
            // 
            // addressTxtUpdateCheckBox
            // 
            this.addressTxtUpdateCheckBox.AutoSize = true;
            this.addressTxtUpdateCheckBox.Location = new System.Drawing.Point(566, 10);
            this.addressTxtUpdateCheckBox.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.addressTxtUpdateCheckBox.Name = "addressTxtUpdateCheckBox";
            this.addressTxtUpdateCheckBox.Size = new System.Drawing.Size(104, 16);
            this.addressTxtUpdateCheckBox.TabIndex = 50;
            this.addressTxtUpdateCheckBox.Text = "address.txt更新";
            this.addressTxtUpdateCheckBox.UseVisualStyleBackColor = true;
            this.addressTxtUpdateCheckBox.CheckedChanged += new System.EventHandler(this.addressTxtUpdateCheckBox_CheckedChanged);
            // 
            // reverseDirectionCheckBox
            // 
            this.reverseDirectionCheckBox.AutoSize = true;
            this.reverseDirectionCheckBox.Location = new System.Drawing.Point(676, 10);
            this.reverseDirectionCheckBox.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.reverseDirectionCheckBox.Name = "reverseDirectionCheckBox";
            this.reverseDirectionCheckBox.Size = new System.Drawing.Size(122, 16);
            this.reverseDirectionCheckBox.TabIndex = 60;
            this.reverseDirectionCheckBox.Text = "チャット欄最新を上に";
            this.reverseDirectionCheckBox.UseVisualStyleBackColor = true;
            this.reverseDirectionCheckBox.CheckedChanged += new System.EventHandler(this.reverseDirectionCheckBox_CheckedChanged);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer2.Panel1MinSize = 1;
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Panel2MinSize = 0;
            this.splitContainer2.Size = new System.Drawing.Size(810, 165);
            this.splitContainer2.SplitterDistance = 74;
            this.splitContainer2.SplitterWidth = 6;
            this.splitContainer2.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 0F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(810, 74);
            this.tableLayoutPanel1.TabIndex = 71;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.hostListView);
            this.panel1.Controls.Add(this.autoMatchingViewPanel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(810, 74);
            this.panel1.TabIndex = 81;
            // 
            // hostListView
            // 
            this.hostListView.CommentColumnHeaderWidth = 197;
            this.hostListView.ContextMenuStrip = this.hostListContextMenuStrip;
            this.hostListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hostListView.FightingHostBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.hostListView.IpPortColumnHeaderWidth = 122;
            this.hostListView.ListBackColor = System.Drawing.SystemColors.Window;
            this.hostListView.ListForeColor = System.Drawing.SystemColors.WindowText;
            this.hostListView.Location = new System.Drawing.Point(0, 0);
            this.hostListView.Margin = new System.Windows.Forms.Padding(0);
            this.hostListView.Name = "hostListView";
            this.hostListView.NoColumnHeaderWidth = 28;
            this.hostListView.RankColumnHeaderWidth = 62;
            this.hostListView.RankFilter = null;
            this.hostListView.ShowTournaments = false;
            this.hostListView.Size = new System.Drawing.Size(810, 74);
            this.hostListView.TabIndex = 70;
            this.hostListView.TimeColumnHeaderWidth = 38;
            this.hostListView.WaitingHostBackColor = System.Drawing.SystemColors.Window;
            this.hostListView.DoubleClick += new System.EventHandler<System.EventArgs>(this.hostListView_DoubleClick);
            this.hostListView.HostChanged += new System.EventHandler<System.EventArgs>(this.hostListView_HostChanged);
            // 
            // hostListContextMenuStrip
            // 
            this.hostListContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.クリップボードにコピーToolStripMenuItem,
            this.toolStripMenuItem1,
            this.大会に参加ToolStripMenuItem,
            this.toolStripMenuItem4,
            this.このIPのホストを表示しないToolStripMenuItem});
            this.hostListContextMenuStrip.Name = "contextMenuStrip1";
            this.hostListContextMenuStrip.Size = new System.Drawing.Size(248, 114);
            this.hostListContextMenuStrip.Closed += new System.Windows.Forms.ToolStripDropDownClosedEventHandler(this.hostListContextMenuStrip_Closed);
            this.hostListContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.hostListContextMenuStrip_Opening);
            // 
            // クリップボードにコピーToolStripMenuItem
            // 
            this.クリップボードにコピーToolStripMenuItem.Name = "クリップボードにコピーToolStripMenuItem";
            this.クリップボードにコピーToolStripMenuItem.Size = new System.Drawing.Size(247, 22);
            this.クリップボードにコピーToolStripMenuItem.Text = "クリップボードにコピー(&C)";
            this.クリップボードにコピーToolStripMenuItem.Click += new System.EventHandler(this.クリップボードにコピーToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(247, 22);
            // 
            // 大会に参加ToolStripMenuItem
            // 
            this.大会に参加ToolStripMenuItem.Name = "大会に参加ToolStripMenuItem";
            this.大会に参加ToolStripMenuItem.Size = new System.Drawing.Size(247, 22);
            this.大会に参加ToolStripMenuItem.Text = "大会に参加";
            this.大会に参加ToolStripMenuItem.Click += new System.EventHandler(this.大会に参加ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(247, 22);
            // 
            // このIPのホストを表示しないToolStripMenuItem
            // 
            this.このIPのホストを表示しないToolStripMenuItem.Name = "このIPのホストを表示しないToolStripMenuItem";
            this.このIPのホストを表示しないToolStripMenuItem.Size = new System.Drawing.Size(247, 22);
            this.このIPのホストを表示しないToolStripMenuItem.Text = "このIPのホストを表示しない(&I)";
            this.このIPのホストを表示しないToolStripMenuItem.Click += new System.EventHandler(this.このIPのホストを表示しないToolStripMenuItem_Click);
            // 
            // autoMatchingViewPanel
            // 
            this.autoMatchingViewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.autoMatchingViewPanel.Location = new System.Drawing.Point(0, 0);
            this.autoMatchingViewPanel.Margin = new System.Windows.Forms.Padding(0);
            this.autoMatchingViewPanel.Name = "autoMatchingViewPanel";
            this.autoMatchingViewPanel.Size = new System.Drawing.Size(810, 74);
            this.autoMatchingViewPanel.TabIndex = 81;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer3.IsSplitterFixed = true;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.chatView);
            this.splitContainer3.Panel1MinSize = 0;
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.splitContainer4);
            this.splitContainer3.Panel2MinSize = 21;
            this.splitContainer3.Size = new System.Drawing.Size(810, 85);
            this.splitContainer3.SplitterDistance = 63;
            this.splitContainer3.SplitterWidth = 1;
            this.splitContainer3.TabIndex = 0;
            // 
            // chatView
            // 
            this.chatView.ContextMenuStrip = this.chatContextMenuStrip;
            this.chatView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chatView.HighlightKeywords = null;
            this.chatView.Location = new System.Drawing.Point(0, 0);
            this.chatView.Margin = new System.Windows.Forms.Padding(0);
            this.chatView.Name = "chatView";
            this.chatView.Size = new System.Drawing.Size(810, 63);
            this.chatView.TabIndex = 80;
            this.chatView.TextBackColor = System.Drawing.SystemColors.Window;
            this.chatView.TextForeColor = System.Drawing.SystemColors.WindowText;
            this.chatView.ViewModel = this.chatViewModel;
            // 
            // chatContextMenuStrip
            // 
            this.chatContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.コピーToolStripMenuItem,
            this.toolStripMenuItem2,
            this.古いチャットを削除ToolStripMenuItem,
            this.toolStripMenuItem3,
            this.遮断するToolStripMenuItem});
            this.chatContextMenuStrip.Name = "chatContextMenuStrip";
            this.chatContextMenuStrip.Size = new System.Drawing.Size(203, 114);
            // 
            // コピーToolStripMenuItem
            // 
            this.コピーToolStripMenuItem.Name = "コピーToolStripMenuItem";
            this.コピーToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.コピーToolStripMenuItem.Text = "コピー(&C)";
            this.コピーToolStripMenuItem.Click += new System.EventHandler(this.コピーToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(202, 22);
            // 
            // 古いチャットを削除ToolStripMenuItem
            // 
            this.古いチャットを削除ToolStripMenuItem.Name = "古いチャットを削除ToolStripMenuItem";
            this.古いチャットを削除ToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.古いチャットを削除ToolStripMenuItem.Text = "古いチャットを削除(&S)";
            this.古いチャットを削除ToolStripMenuItem.Click += new System.EventHandler(this.古いチャットを削除ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(202, 22);
            // 
            // 遮断するToolStripMenuItem
            // 
            this.遮断するToolStripMenuItem.Name = "遮断するToolStripMenuItem";
            this.遮断するToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.遮断するToolStripMenuItem.Text = "遮断する(&I)";
            this.遮断するToolStripMenuItem.Click += new System.EventHandler(this.遮断するToolStripMenuItem_Click);
            // 
            // chatViewModel
            // 
            this.chatViewModel.UserConfig = null;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer4.IsSplitterFixed = true;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.sendButton);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.chatInput);
            this.splitContainer4.Panel2.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.splitContainer4.Size = new System.Drawing.Size(810, 21);
            this.splitContainer4.SplitterDistance = 81;
            this.splitContainer4.SplitterWidth = 1;
            this.splitContainer4.TabIndex = 0;
            // 
            // sendButton
            // 
            this.sendButton.BackColor = System.Drawing.SystemColors.Control;
            this.sendButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sendButton.Enabled = false;
            this.sendButton.Location = new System.Drawing.Point(0, 0);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(81, 21);
            this.sendButton.TabIndex = 90;
            this.sendButton.Text = "送信";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // chatInput
            // 
            this.chatInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chatInput.Enabled = false;
            this.chatInput.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.chatInput.Location = new System.Drawing.Point(0, 1);
            this.chatInput.Name = "chatInput";
            this.chatInput.Size = new System.Drawing.Size(728, 19);
            this.chatInput.TabIndex = 100;
            this.chatInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.chatInput_KeyPress);
            // 
            // autoMatchingViewModel
            // 
            this.autoMatchingViewModel.Ratings = null;
            this.autoMatchingViewModel.UserConfig = null;
            this.autoMatchingViewModel.OponentFound += new System.EventHandler(this.autoMatchingViewModel_OponentFound);
            // 
            // colorChangeToggle1
            // 
            this.colorChangeToggle1.Target = this.receiveHostCheckBox;
            // 
            // colorChangeToggle2
            // 
            this.colorChangeToggle2.Target = this.receiveChatCheckBox;
            // 
            // textChangeToggle1
            // 
            this.textChangeToggle1.CheckedText = "更新中";
            this.textChangeToggle1.NormalText = "一覧の更新";
            this.textChangeToggle1.Target = this.receiveHostCheckBox;
            // 
            // textChangeToggle2
            // 
            this.textChangeToggle2.CheckedText = "チャット中";
            this.textChangeToggle2.NormalText = "チャット開始";
            this.textChangeToggle2.Target = this.receiveChatCheckBox;
            // 
            // colorChangeToggle3
            // 
            this.colorChangeToggle3.Target = this.registerHostCheckBox;
            // 
            // textChangeToggle3
            // 
            this.textChangeToggle3.CheckedText = "登録中";
            this.textChangeToggle3.NormalText = "ホスト登録";
            this.textChangeToggle3.Target = this.registerHostCheckBox;
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            // 
            // hostController
            // 
            this.hostController.Client = null;
            this.hostController.Interval = 15000;
            // 
            // receiveTimer
            // 
            this.receiveTimer.Enabled = true;
            this.receiveTimer.Interval = 15000;
            this.receiveTimer.Tick += new System.EventHandler(this.receiveTimer_Tick);
            // 
            // colorChangeToggle4
            // 
            this.colorChangeToggle4.Target = this.autoMatchingCheckBox;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // ClientTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Controls.Add(this.splitContainer1);
            this.Enabled = false;
            this.Name = "ClientTab";
            this.Size = new System.Drawing.Size(810, 200);
            this.Load += new System.EventHandler(this.ClientTab_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.hostListContextMenuStrip.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.chatContextMenuStrip.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.Panel2.PerformLayout();
            this.splitContainer4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.CheckBox reverseDirectionCheckBox;
        private System.Windows.Forms.CheckBox addressTxtUpdateCheckBox;
        private System.Windows.Forms.ComboBox rankFilterComboBox;
        private System.Windows.Forms.CheckBox receiveChatCheckBox;
        private System.Windows.Forms.CheckBox receiveHostCheckBox;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private HisoutenSupportTools.AddressUpdater.Lib.View.HostListView hostListView;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private HisoutenSupportTools.AddressUpdater.Lib.View.ChatView chatView;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.TextBox chatInput;
        private HisoutenSupportTools.AddressUpdater.Lib.View.ColorChangeToggle colorChangeToggle1;
        private HisoutenSupportTools.AddressUpdater.Lib.View.ColorChangeToggle colorChangeToggle2;
        private HisoutenSupportTools.AddressUpdater.Lib.View.TextChangeToggle textChangeToggle1;
        private HisoutenSupportTools.AddressUpdater.Lib.View.TextChangeToggle textChangeToggle2;
        private System.Windows.Forms.CheckBox registerHostCheckBox;
        private HisoutenSupportTools.AddressUpdater.Lib.View.ColorChangeToggle colorChangeToggle3;
        private HisoutenSupportTools.AddressUpdater.Lib.View.TextChangeToggle textChangeToggle3;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private HisoutenSupportTools.AddressUpdater.Lib.Controller.HostController hostController;
        private HisoutenSupportTools.AddressUpdater.Lib.IO.AddressTxtUpdater addressTxtUpdater;
        private System.Windows.Forms.ContextMenuStrip chatContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem コピーToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 遮断するToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 古いチャットを削除ToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip hostListContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem クリップボードにコピーToolStripMenuItem;
        private System.Windows.Forms.Timer receiveTimer;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStripMenuItem このIPのホストを表示しないToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem 大会に参加ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private HisoutenSupportTools.AddressUpdater.Lib.ViewModel.ChatViewModel chatViewModel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.CheckBox autoMatchingCheckBox;
        private HisoutenSupportTools.AddressUpdater.Lib.View.ColorChangeToggle colorChangeToggle4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private HisoutenSupportTools.AddressUpdater.Lib.ViewModel.AutoMatching.AutoMatchingViewModel autoMatchingViewModel;
        private System.Windows.Forms.Panel autoMatchingViewPanel;

    }
}
