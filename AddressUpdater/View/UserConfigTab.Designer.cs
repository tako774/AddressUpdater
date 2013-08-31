namespace HisoutenSupportTools.AddressUpdater.View
{
    partial class UserConfigTab
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.userConfigTabControl = new System.Windows.Forms.TabControl();
            this.generalTabPage = new System.Windows.Forms.TabPage();
            this.generalTab = new HisoutenSupportTools.AddressUpdater.Lib.View.UserConfigView.GeneralTab();
            this.ranksTabPage = new System.Windows.Forms.TabPage();
            this.rankTab = new HisoutenSupportTools.AddressUpdater.Lib.View.UserConfigView.RankTab();
            this.chatFilterTabPage = new System.Windows.Forms.TabPage();
            this.chatFilterTab = new HisoutenSupportTools.AddressUpdater.Lib.View.UserConfigView.ChatFilterTab();
            this.bootSameTimeTabPage = new System.Windows.Forms.TabPage();
            this.sameTimeBootTab = new HisoutenSupportTools.AddressUpdater.Lib.View.UserConfigView.SameTimeBootTab();
            this.serverInformationsTabPage = new System.Windows.Forms.TabPage();
            this.serverTab = new HisoutenSupportTools.AddressUpdater.Lib.View.UserConfigView.ServerTab();
            this.panel1 = new System.Windows.Forms.Panel();
            this.saveUserConfigButton = new System.Windows.Forms.Button();
            this.highlightTabPage = new System.Windows.Forms.TabPage();
            this.highlightKeywordsTab = new HisoutenSupportTools.AddressUpdater.Lib.View.UserConfigView.HighlightKeywordsTab();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.userConfigTabControl.SuspendLayout();
            this.generalTabPage.SuspendLayout();
            this.ranksTabPage.SuspendLayout();
            this.chatFilterTabPage.SuspendLayout();
            this.bootSameTimeTabPage.SuspendLayout();
            this.serverInformationsTabPage.SuspendLayout();
            this.panel1.SuspendLayout();
            this.highlightTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.userConfigTabControl);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Panel2MinSize = 44;
            this.splitContainer1.Size = new System.Drawing.Size(710, 325);
            this.splitContainer1.SplitterDistance = 279;
            this.splitContainer1.SplitterWidth = 2;
            this.splitContainer1.TabIndex = 185;
            // 
            // userConfigTabControl
            // 
            this.userConfigTabControl.Controls.Add(this.generalTabPage);
            this.userConfigTabControl.Controls.Add(this.ranksTabPage);
            this.userConfigTabControl.Controls.Add(this.highlightTabPage);
            this.userConfigTabControl.Controls.Add(this.chatFilterTabPage);
            this.userConfigTabControl.Controls.Add(this.bootSameTimeTabPage);
            this.userConfigTabControl.Controls.Add(this.serverInformationsTabPage);
            this.userConfigTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userConfigTabControl.Location = new System.Drawing.Point(0, 0);
            this.userConfigTabControl.Name = "userConfigTabControl";
            this.userConfigTabControl.SelectedIndex = 0;
            this.userConfigTabControl.Size = new System.Drawing.Size(710, 279);
            this.userConfigTabControl.TabIndex = 10;
            // 
            // generalTabPage
            // 
            this.generalTabPage.Controls.Add(this.generalTab);
            this.generalTabPage.Location = new System.Drawing.Point(4, 22);
            this.generalTabPage.Name = "generalTabPage";
            this.generalTabPage.Size = new System.Drawing.Size(702, 253);
            this.generalTabPage.TabIndex = 0;
            this.generalTabPage.Text = "一般";
            this.generalTabPage.UseVisualStyleBackColor = true;
            // 
            // generalTab
            // 
            this.generalTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.generalTab.ChatPrefix = "";
            this.generalTab.ClientDivisionOrientation = System.Windows.Forms.Orientation.Horizontal;
            this.generalTab.DisableMultiBoot = true;
            this.generalTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.generalTab.Ip = "";
            this.generalTab.Language = null;
            this.generalTab.Location = new System.Drawing.Point(0, 0);
            this.generalTab.Margin = new System.Windows.Forms.Padding(0);
            this.generalTab.Name = "generalTab";
            this.generalTab.Port = 10800;
            this.generalTab.ShowTournaments = false;
            this.generalTab.Size = new System.Drawing.Size(702, 253);
            this.generalTab.TabIndex = 171;
            this.generalTab.Theme = null;
            this.generalTab.UpdateSpan = 15;
            this.generalTab.UserConfig = null;
            this.generalTab.ReflectThemeRequested += new System.EventHandler(this.generalTab_ReflectThemeRequested);
            // 
            // ranksTabPage
            // 
            this.ranksTabPage.Controls.Add(this.rankTab);
            this.ranksTabPage.Location = new System.Drawing.Point(4, 22);
            this.ranksTabPage.Name = "ranksTabPage";
            this.ranksTabPage.Size = new System.Drawing.Size(702, 253);
            this.ranksTabPage.TabIndex = 2;
            this.ranksTabPage.Text = "ランク";
            this.ranksTabPage.UseVisualStyleBackColor = true;
            // 
            // rankTab
            // 
            this.rankTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.rankTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rankTab.Language = null;
            this.rankTab.Location = new System.Drawing.Point(0, 0);
            this.rankTab.Name = "rankTab";
            this.rankTab.Size = new System.Drawing.Size(702, 253);
            this.rankTab.TabIndex = 0;
            this.rankTab.Theme = null;
            this.rankTab.UserConfig = null;
            // 
            // chatFilterTabPage
            // 
            this.chatFilterTabPage.Controls.Add(this.chatFilterTab);
            this.chatFilterTabPage.Location = new System.Drawing.Point(4, 22);
            this.chatFilterTabPage.Name = "chatFilterTabPage";
            this.chatFilterTabPage.Size = new System.Drawing.Size(702, 253);
            this.chatFilterTabPage.TabIndex = 4;
            this.chatFilterTabPage.Text = "チャットフィルター";
            this.chatFilterTabPage.UseVisualStyleBackColor = true;
            // 
            // chatFilterTab
            // 
            this.chatFilterTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.chatFilterTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chatFilterTab.Language = null;
            this.chatFilterTab.Location = new System.Drawing.Point(0, 0);
            this.chatFilterTab.Name = "chatFilterTab";
            this.chatFilterTab.Size = new System.Drawing.Size(702, 253);
            this.chatFilterTab.TabIndex = 0;
            this.chatFilterTab.Theme = null;
            this.chatFilterTab.UserConfig = null;
            // 
            // bootSameTimeTabPage
            // 
            this.bootSameTimeTabPage.Controls.Add(this.sameTimeBootTab);
            this.bootSameTimeTabPage.Location = new System.Drawing.Point(4, 22);
            this.bootSameTimeTabPage.Name = "bootSameTimeTabPage";
            this.bootSameTimeTabPage.Size = new System.Drawing.Size(702, 253);
            this.bootSameTimeTabPage.TabIndex = 1;
            this.bootSameTimeTabPage.Text = "同時起動";
            this.bootSameTimeTabPage.UseVisualStyleBackColor = true;
            // 
            // sameTimeBootTab
            // 
            this.sameTimeBootTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.sameTimeBootTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sameTimeBootTab.Language = null;
            this.sameTimeBootTab.Location = new System.Drawing.Point(0, 0);
            this.sameTimeBootTab.Name = "sameTimeBootTab";
            this.sameTimeBootTab.Size = new System.Drawing.Size(702, 253);
            this.sameTimeBootTab.TabIndex = 0;
            this.sameTimeBootTab.Theme = null;
            this.sameTimeBootTab.UserConfig = null;
            // 
            // serverInformationsTabPage
            // 
            this.serverInformationsTabPage.Controls.Add(this.serverTab);
            this.serverInformationsTabPage.Location = new System.Drawing.Point(4, 22);
            this.serverInformationsTabPage.Name = "serverInformationsTabPage";
            this.serverInformationsTabPage.Size = new System.Drawing.Size(702, 253);
            this.serverInformationsTabPage.TabIndex = 3;
            this.serverInformationsTabPage.Text = "サーバー";
            this.serverInformationsTabPage.UseVisualStyleBackColor = true;
            // 
            // serverTab
            // 
            this.serverTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.serverTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.serverTab.Language = null;
            this.serverTab.Location = new System.Drawing.Point(0, 0);
            this.serverTab.Name = "serverTab";
            this.serverTab.Size = new System.Drawing.Size(702, 253);
            this.serverTab.TabIndex = 0;
            this.serverTab.Theme = null;
            this.serverTab.UserConfig = null;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.saveUserConfigButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(710, 44);
            this.panel1.TabIndex = 182;
            // 
            // saveUserConfigButton
            // 
            this.saveUserConfigButton.Location = new System.Drawing.Point(6, 4);
            this.saveUserConfigButton.Name = "saveUserConfigButton";
            this.saveUserConfigButton.Size = new System.Drawing.Size(313, 34);
            this.saveUserConfigButton.TabIndex = 20;
            this.saveUserConfigButton.Text = "保存する";
            this.saveUserConfigButton.UseVisualStyleBackColor = true;
            this.saveUserConfigButton.Click += new System.EventHandler(this.saveUserConfigButton_Click);
            // 
            // highlightTabPage
            // 
            this.highlightTabPage.Controls.Add(this.highlightKeywordsTab);
            this.highlightTabPage.Location = new System.Drawing.Point(4, 22);
            this.highlightTabPage.Name = "highlightTabPage";
            this.highlightTabPage.Size = new System.Drawing.Size(702, 253);
            this.highlightTabPage.TabIndex = 5;
            this.highlightTabPage.Text = "ハイライト";
            this.highlightTabPage.UseVisualStyleBackColor = true;
            // 
            // highlightKeywordsTab
            // 
            this.highlightKeywordsTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.highlightKeywordsTab.Language = null;
            this.highlightKeywordsTab.Location = new System.Drawing.Point(0, 0);
            this.highlightKeywordsTab.Name = "highlightKeywordsTab";
            this.highlightKeywordsTab.Size = new System.Drawing.Size(702, 253);
            this.highlightKeywordsTab.TabIndex = 0;
            this.highlightKeywordsTab.Theme = null;
            this.highlightKeywordsTab.UserConfig = null;
            // 
            // UserConfigTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "UserConfigTab";
            this.Size = new System.Drawing.Size(710, 325);
            this.Load += new System.EventHandler(this.UserConfigTab_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.userConfigTabControl.ResumeLayout(false);
            this.generalTabPage.ResumeLayout(false);
            this.ranksTabPage.ResumeLayout(false);
            this.chatFilterTabPage.ResumeLayout(false);
            this.bootSameTimeTabPage.ResumeLayout(false);
            this.serverInformationsTabPage.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.highlightTabPage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button saveUserConfigButton;
        private System.Windows.Forms.TabControl userConfigTabControl;
        private System.Windows.Forms.TabPage generalTabPage;
        private HisoutenSupportTools.AddressUpdater.Lib.View.UserConfigView.GeneralTab generalTab;
        private System.Windows.Forms.TabPage ranksTabPage;
        private HisoutenSupportTools.AddressUpdater.Lib.View.UserConfigView.RankTab rankTab;
        private System.Windows.Forms.TabPage bootSameTimeTabPage;
        private HisoutenSupportTools.AddressUpdater.Lib.View.UserConfigView.SameTimeBootTab sameTimeBootTab;
        private System.Windows.Forms.TabPage serverInformationsTabPage;
        private HisoutenSupportTools.AddressUpdater.Lib.View.UserConfigView.ServerTab serverTab;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabPage chatFilterTabPage;
        private HisoutenSupportTools.AddressUpdater.Lib.View.UserConfigView.ChatFilterTab chatFilterTab;
        private System.Windows.Forms.TabPage highlightTabPage;
        private HisoutenSupportTools.AddressUpdater.Lib.View.UserConfigView.HighlightKeywordsTab highlightKeywordsTab;
    }
}
