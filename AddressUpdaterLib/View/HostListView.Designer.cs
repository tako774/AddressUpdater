namespace HisoutenSupportTools.AddressUpdater.Lib.View
{
    partial class HostListView
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "999",
            "23:59",
            "255.255.255.255:10800",
            "Phantasm",
            "えーりん！えーりん！たすけてえーりん！"}, -1);
            this.listView = new System.Windows.Forms.ListView();
            this.noColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.timeColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.ip_portColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.rankColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.commentColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // listView
            // 
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.noColumnHeader,
            this.timeColumnHeader,
            this.ip_portColumnHeader,
            this.rankColumnHeader,
            this.commentColumnHeader});
            this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.listView.FullRowSelect = true;
            this.listView.GridLines = true;
            this.listView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.listView.Location = new System.Drawing.Point(0, 0);
            this.listView.MultiSelect = false;
            this.listView.Name = "listView";
            this.listView.ShowItemToolTips = true;
            this.listView.Size = new System.Drawing.Size(478, 150);
            this.listView.TabIndex = 0;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            this.listView.DoubleClick += new System.EventHandler(this.listView_DoubleClick);
            // 
            // noColumnHeader
            // 
            this.noColumnHeader.Text = "No";
            this.noColumnHeader.Width = 28;
            // 
            // timeColumnHeader
            // 
            this.timeColumnHeader.Text = "時刻";
            this.timeColumnHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.timeColumnHeader.Width = 38;
            // 
            // ip_portColumnHeader
            // 
            this.ip_portColumnHeader.Text = "IP:ポート";
            this.ip_portColumnHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ip_portColumnHeader.Width = 122;
            // 
            // rankColumnHeader
            // 
            this.rankColumnHeader.Text = "ランク";
            this.rankColumnHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.rankColumnHeader.Width = 62;
            // 
            // commentColumnHeader
            // 
            this.commentColumnHeader.Text = "コメント";
            this.commentColumnHeader.Width = 197;
            // 
            // HostListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listView);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "HostListView";
            this.Size = new System.Drawing.Size(478, 150);
            this.FontChanged += new System.EventHandler(this.HostListView_FontChanged);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ColumnHeader noColumnHeader;
        private System.Windows.Forms.ColumnHeader timeColumnHeader;
        private System.Windows.Forms.ColumnHeader ip_portColumnHeader;
        private System.Windows.Forms.ColumnHeader rankColumnHeader;
        private System.Windows.Forms.ColumnHeader commentColumnHeader;
    }
}
