namespace HisoutenSupportTools.AddressUpdater.Lib.View
{
    partial class VersionTab
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.omakeGroupBox = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.extraView1 = new HisoutenSupportTools.AddressUpdater.Lib.View.ExtraView();
            this.versionCheckLinkLabel = new System.Windows.Forms.LinkLabel();
            this.versionLabel = new System.Windows.Forms.Label();
            this.adminSiteLinkLabel1 = new System.Windows.Forms.LinkLabel();
            this.adminSiteLinkLabel2 = new System.Windows.Forms.LinkLabel();
            this.versionToolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.omakeGroupBox.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::HisoutenSupportTools.AddressUpdater.Lib.Properties.Resources.icon48;
            this.pictureBox1.Location = new System.Drawing.Point(22, 21);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(48, 48);
            this.pictureBox1.TabIndex = 243;
            this.pictureBox1.TabStop = false;
            // 
            // omakeGroupBox
            // 
            this.omakeGroupBox.Controls.Add(this.tabControl1);
            this.omakeGroupBox.Location = new System.Drawing.Point(260, 17);
            this.omakeGroupBox.Name = "omakeGroupBox";
            this.omakeGroupBox.Size = new System.Drawing.Size(435, 190);
            this.omakeGroupBox.TabIndex = 241;
            this.omakeGroupBox.TabStop = false;
            this.omakeGroupBox.Text = "おまけ機能";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 15);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(429, 172);
            this.tabControl1.TabIndex = 244;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.extraView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(421, 146);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // extraView1
            // 
            this.extraView1.Location = new System.Drawing.Point(0, 0);
            this.extraView1.Name = "extraView1";
            this.extraView1.Size = new System.Drawing.Size(418, 143);
            this.extraView1.TabIndex = 0;
            this.extraView1.ViewModel = null;
            // 
            // versionCheckLinkLabel
            // 
            this.versionCheckLinkLabel.AutoSize = true;
            this.versionCheckLinkLabel.Location = new System.Drawing.Point(20, 123);
            this.versionCheckLinkLabel.Name = "versionCheckLinkLabel";
            this.versionCheckLinkLabel.Size = new System.Drawing.Size(196, 12);
            this.versionCheckLinkLabel.TabIndex = 238;
            this.versionCheckLinkLabel.TabStop = true;
            this.versionCheckLinkLabel.Text = "新しいバージョンが公開されているか確認";
            this.versionCheckLinkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.versionCheckLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.versionCheckLinkLabel_LinkClicked);
            // 
            // versionLabel
            // 
            this.versionLabel.AutoSize = true;
            this.versionLabel.Font = new System.Drawing.Font("Arial", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.versionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.versionLabel.Location = new System.Drawing.Point(76, 36);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(165, 18);
            this.versionLabel.TabIndex = 242;
            this.versionLabel.Text = "AddressUpdater 8.10";
            this.versionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // adminSiteLinkLabel1
            // 
            this.adminSiteLinkLabel1.AutoSize = true;
            this.adminSiteLinkLabel1.Location = new System.Drawing.Point(20, 172);
            this.adminSiteLinkLabel1.Name = "adminSiteLinkLabel1";
            this.adminSiteLinkLabel1.Size = new System.Drawing.Size(72, 12);
            this.adminSiteLinkLabel1.TabIndex = 239;
            this.adminSiteLinkLabel1.TabStop = true;
            this.adminSiteLinkLabel1.Text = "作者のサイト1";
            this.adminSiteLinkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.adminSiteLinkLabel1_LinkClicked);
            // 
            // adminSiteLinkLabel2
            // 
            this.adminSiteLinkLabel2.AutoSize = true;
            this.adminSiteLinkLabel2.Location = new System.Drawing.Point(114, 172);
            this.adminSiteLinkLabel2.Name = "adminSiteLinkLabel2";
            this.adminSiteLinkLabel2.Size = new System.Drawing.Size(72, 12);
            this.adminSiteLinkLabel2.TabIndex = 240;
            this.adminSiteLinkLabel2.TabStop = true;
            this.adminSiteLinkLabel2.Text = "作者のサイト2";
            this.adminSiteLinkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.adminSiteLinkLabel2_LinkClicked);
            // 
            // VersionTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.omakeGroupBox);
            this.Controls.Add(this.versionCheckLinkLabel);
            this.Controls.Add(this.versionLabel);
            this.Controls.Add(this.adminSiteLinkLabel1);
            this.Controls.Add(this.adminSiteLinkLabel2);
            this.Name = "VersionTab";
            this.Size = new System.Drawing.Size(710, 210);
            this.Load += new System.EventHandler(this.VersionTab_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.omakeGroupBox.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox omakeGroupBox;
        private System.Windows.Forms.LinkLabel versionCheckLinkLabel;
        private System.Windows.Forms.Label versionLabel;
        private System.Windows.Forms.LinkLabel adminSiteLinkLabel1;
        private System.Windows.Forms.LinkLabel adminSiteLinkLabel2;
        private System.Windows.Forms.ToolTip versionToolTip;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private ExtraView extraView1;
    }
}
