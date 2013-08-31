namespace HisoutenSupportTools.AddressUpdater.Lib.View.UserConfigView
{
    partial class ServerTab
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
            this.serverInformationsGridView = new System.Windows.Forms.DataGridView();
            this.visivilityColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.serverNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.urlColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.importButton = new System.Windows.Forms.Button();
            this.exportButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.upButton = new System.Windows.Forms.Button();
            this.downButton = new System.Windows.Forms.Button();
            this.serverListLoader = new HisoutenSupportTools.AddressUpdater.Lib.IO.ServerListLoader(this.components);
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.serverInformationsGridView)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.serverInformationsGridView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.flowLayoutPanel1);
            this.splitContainer1.Panel2MinSize = 30;
            this.splitContainer1.Size = new System.Drawing.Size(676, 150);
            this.splitContainer1.SplitterDistance = 118;
            this.splitContainer1.SplitterWidth = 2;
            this.splitContainer1.TabIndex = 182;
            // 
            // serverInformationsGridView
            // 
            this.serverInformationsGridView.AllowUserToAddRows = false;
            this.serverInformationsGridView.AllowUserToDeleteRows = false;
            this.serverInformationsGridView.AllowUserToResizeRows = false;
            this.serverInformationsGridView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.serverInformationsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.serverInformationsGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.visivilityColumn,
            this.serverNameColumn,
            this.urlColumn});
            this.serverInformationsGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.serverInformationsGridView.Location = new System.Drawing.Point(0, 0);
            this.serverInformationsGridView.Name = "serverInformationsGridView";
            this.serverInformationsGridView.RowHeadersVisible = false;
            this.serverInformationsGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.serverInformationsGridView.RowTemplate.Height = 21;
            this.serverInformationsGridView.Size = new System.Drawing.Size(676, 118);
            this.serverInformationsGridView.TabIndex = 10;
            this.serverInformationsGridView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.serverInformationsGridView_MouseDown);
            // 
            // visivilityColumn
            // 
            this.visivilityColumn.HeaderText = "表示";
            this.visivilityColumn.Name = "visivilityColumn";
            this.visivilityColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.visivilityColumn.Width = 40;
            // 
            // serverNameColumn
            // 
            this.serverNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.serverNameColumn.HeaderText = "サーバー名";
            this.serverNameColumn.Name = "serverNameColumn";
            this.serverNameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.serverNameColumn.Width = 63;
            // 
            // urlColumn
            // 
            this.urlColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.urlColumn.HeaderText = "アドレス";
            this.urlColumn.Name = "urlColumn";
            this.urlColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.importButton);
            this.flowLayoutPanel1.Controls.Add(this.exportButton);
            this.flowLayoutPanel1.Controls.Add(this.addButton);
            this.flowLayoutPanel1.Controls.Add(this.deleteButton);
            this.flowLayoutPanel1.Controls.Add(this.upButton);
            this.flowLayoutPanel1.Controls.Add(this.downButton);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(676, 30);
            this.flowLayoutPanel1.TabIndex = 81;
            // 
            // importButton
            // 
            this.importButton.Location = new System.Drawing.Point(3, 3);
            this.importButton.Name = "importButton";
            this.importButton.Size = new System.Drawing.Size(80, 23);
            this.importButton.TabIndex = 20;
            this.importButton.Text = "インポート";
            this.importButton.UseVisualStyleBackColor = true;
            this.importButton.Click += new System.EventHandler(this.importServerButton_Click);
            // 
            // exportButton
            // 
            this.exportButton.Location = new System.Drawing.Point(89, 3);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(80, 23);
            this.exportButton.TabIndex = 30;
            this.exportButton.Text = "エクスポート";
            this.exportButton.UseVisualStyleBackColor = true;
            this.exportButton.Click += new System.EventHandler(this.exportServerButton_Click);
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(175, 3);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(80, 23);
            this.addButton.TabIndex = 40;
            this.addButton.Text = "追加";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addServerButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(261, 3);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(80, 23);
            this.deleteButton.TabIndex = 60;
            this.deleteButton.Text = "削除";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.removeServerButton_Click);
            // 
            // upButton
            // 
            this.upButton.Location = new System.Drawing.Point(347, 3);
            this.upButton.Name = "upButton";
            this.upButton.Size = new System.Drawing.Size(40, 23);
            this.upButton.TabIndex = 70;
            this.upButton.Text = "▲";
            this.upButton.UseVisualStyleBackColor = true;
            this.upButton.Click += new System.EventHandler(this.upServerButton_Click);
            // 
            // downButton
            // 
            this.downButton.Location = new System.Drawing.Point(393, 3);
            this.downButton.Name = "downButton";
            this.downButton.Size = new System.Drawing.Size(40, 23);
            this.downButton.TabIndex = 80;
            this.downButton.Text = "▼";
            this.downButton.UseVisualStyleBackColor = true;
            this.downButton.Click += new System.EventHandler(this.downServerButton_Click);
            // 
            // ServerTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ServerTab";
            this.Size = new System.Drawing.Size(676, 150);
            this.Load += new System.EventHandler(this.ServerTab_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.serverInformationsGridView)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private HisoutenSupportTools.AddressUpdater.Lib.IO.ServerListLoader serverListLoader;
        private System.Windows.Forms.DataGridView serverInformationsGridView;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button importButton;
        private System.Windows.Forms.Button exportButton;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button upButton;
        private System.Windows.Forms.Button downButton;
        private System.Windows.Forms.DataGridViewCheckBoxColumn visivilityColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn serverNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn urlColumn;
    }
}
