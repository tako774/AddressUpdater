namespace HisoutenSupportTools.AddressUpdater
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.hostSettingTabPage = new System.Windows.Forms.TabPage();
            this.hostSettingTab = new HisoutenSupportTools.AddressUpdater.Lib.View.HostSettingTab();
            this.hostSettingViewModel = new HisoutenSupportTools.AddressUpdater.Lib.ViewModel.HostSettingViewModel(this.components);
            this.userConfigTabPage = new System.Windows.Forms.TabPage();
            this.userConfigTab = new HisoutenSupportTools.AddressUpdater.View.UserConfigTab();
            this.versionTabPage = new System.Windows.Forms.TabPage();
            this.versionTab = new HisoutenSupportTools.AddressUpdater.Lib.View.VersionTab();
            this.versionViewModel = new HisoutenSupportTools.AddressUpdater.Lib.ViewModel.VersionViewModel(this.components);
            this.tskTimer = new System.Windows.Forms.Timer(this.components);
            this.mainTabControl.SuspendLayout();
            this.hostSettingTabPage.SuspendLayout();
            this.userConfigTabPage.SuspendLayout();
            this.versionTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTabControl
            // 
            this.mainTabControl.Controls.Add(this.hostSettingTabPage);
            this.mainTabControl.Controls.Add(this.userConfigTabPage);
            this.mainTabControl.Controls.Add(this.versionTabPage);
            this.mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabControl.Location = new System.Drawing.Point(0, 0);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(749, 413);
            this.mainTabControl.TabIndex = 0;
            this.mainTabControl.SelectedIndexChanged += new System.EventHandler(this.mainTabControl_SelectedIndexChanged);
            // 
            // hostSettingTabPage
            // 
            this.hostSettingTabPage.Controls.Add(this.hostSettingTab);
            this.hostSettingTabPage.Location = new System.Drawing.Point(4, 22);
            this.hostSettingTabPage.Name = "hostSettingTabPage";
            this.hostSettingTabPage.Size = new System.Drawing.Size(741, 387);
            this.hostSettingTabPage.TabIndex = 1;
            this.hostSettingTabPage.Text = "ホスト設定";
            this.hostSettingTabPage.UseVisualStyleBackColor = true;
            // 
            // hostSettingTab
            // 
            this.hostSettingTab.BackColor = System.Drawing.Color.Transparent;
            this.hostSettingTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hostSettingTab.Language = null;
            this.hostSettingTab.Location = new System.Drawing.Point(0, 0);
            this.hostSettingTab.Name = "hostSettingTab";
            this.hostSettingTab.Size = new System.Drawing.Size(741, 387);
            this.hostSettingTab.TabIndex = 0;
            this.hostSettingTab.Theme = null;
            this.hostSettingTab.UserConfig = null;
            this.hostSettingTab.ViewModel = this.hostSettingViewModel;
            // 
            // hostSettingViewModel
            // 
            this.hostSettingViewModel.Language = null;
            this.hostSettingViewModel.UserConfig = null;
            // 
            // userConfigTabPage
            // 
            this.userConfigTabPage.Controls.Add(this.userConfigTab);
            this.userConfigTabPage.Location = new System.Drawing.Point(4, 22);
            this.userConfigTabPage.Name = "userConfigTabPage";
            this.userConfigTabPage.Size = new System.Drawing.Size(741, 387);
            this.userConfigTabPage.TabIndex = 2;
            this.userConfigTabPage.Text = "ユーザー設定";
            this.userConfigTabPage.UseVisualStyleBackColor = true;
            // 
            // userConfigTab
            // 
            this.userConfigTab.BackColor = System.Drawing.Color.Transparent;
            this.userConfigTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userConfigTab.Language = null;
            this.userConfigTab.Location = new System.Drawing.Point(0, 0);
            this.userConfigTab.Name = "userConfigTab";
            this.userConfigTab.Size = new System.Drawing.Size(741, 387);
            this.userConfigTab.TabIndex = 0;
            this.userConfigTab.Theme = null;
            this.userConfigTab.UserConfig = null;
            // 
            // versionTabPage
            // 
            this.versionTabPage.Controls.Add(this.versionTab);
            this.versionTabPage.Location = new System.Drawing.Point(4, 22);
            this.versionTabPage.Name = "versionTabPage";
            this.versionTabPage.Size = new System.Drawing.Size(741, 387);
            this.versionTabPage.TabIndex = 3;
            this.versionTabPage.Text = "バージョン情報";
            this.versionTabPage.UseVisualStyleBackColor = true;
            // 
            // versionTab
            // 
            this.versionTab.BackColor = System.Drawing.Color.Transparent;
            this.versionTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.versionTab.Language = null;
            this.versionTab.Location = new System.Drawing.Point(0, 0);
            this.versionTab.Name = "versionTab";
            this.versionTab.Size = new System.Drawing.Size(741, 387);
            this.versionTab.TabIndex = 0;
            this.versionTab.Theme = null;
            this.versionTab.UserConfig = null;
            this.versionTab.ViewModel = this.versionViewModel;
            // 
            // tskTimer
            // 
            this.tskTimer.Enabled = true;
            this.tskTimer.Interval = 5000;
            this.tskTimer.Tick += new System.EventHandler(this.tskTimer_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(749, 413);
            this.Controls.Add(this.mainTabControl);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.mainTabControl.ResumeLayout(false);
            this.hostSettingTabPage.ResumeLayout(false);
            this.userConfigTabPage.ResumeLayout(false);
            this.versionTabPage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage hostSettingTabPage;
        private System.Windows.Forms.TabPage userConfigTabPage;
        private System.Windows.Forms.TabPage versionTabPage;
        private HisoutenSupportTools.AddressUpdater.Lib.View.HostSettingTab hostSettingTab;
        private HisoutenSupportTools.AddressUpdater.View.UserConfigTab userConfigTab;
        private HisoutenSupportTools.AddressUpdater.Lib.View.VersionTab versionTab;
        private HisoutenSupportTools.AddressUpdater.Lib.ViewModel.HostSettingViewModel hostSettingViewModel;
        private HisoutenSupportTools.AddressUpdater.Lib.ViewModel.VersionViewModel versionViewModel;
        private System.Windows.Forms.Timer tskTimer;
    }
}