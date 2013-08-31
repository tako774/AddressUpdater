namespace HisoutenSupportTools.AddressUpdater.Lib.View.UserConfigView
{
    partial class SameTimeBootTab
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
            this.dragdropLabel = new System.Windows.Forms.Label();
            this.bootSameTimeListView = new System.Windows.Forms.ListView();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.deleteButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dragdropLabel
            // 
            this.dragdropLabel.AutoSize = true;
            this.dragdropLabel.Location = new System.Drawing.Point(9, 8);
            this.dragdropLabel.Name = "dragdropLabel";
            this.dragdropLabel.Size = new System.Drawing.Size(72, 12);
            this.dragdropLabel.TabIndex = 175;
            this.dragdropLabel.Text = "Drag&&Drop可";
            // 
            // bootSameTimeListView
            // 
            this.bootSameTimeListView.AllowDrop = true;
            this.bootSameTimeListView.CheckBoxes = true;
            this.bootSameTimeListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bootSameTimeListView.FullRowSelect = true;
            this.bootSameTimeListView.GridLines = true;
            this.bootSameTimeListView.LargeImageList = this.imageList;
            this.bootSameTimeListView.Location = new System.Drawing.Point(0, 0);
            this.bootSameTimeListView.Name = "bootSameTimeListView";
            this.bootSameTimeListView.Size = new System.Drawing.Size(284, 118);
            this.bootSameTimeListView.TabIndex = 10;
            this.bootSameTimeListView.UseCompatibleStateImageBehavior = false;
            this.bootSameTimeListView.DragDrop += new System.Windows.Forms.DragEventHandler(this.bootSameTimeListView_DragDrop);
            this.bootSameTimeListView.DragEnter += new System.Windows.Forms.DragEventHandler(this.bootSameTimeListView_DragEnter);
            this.bootSameTimeListView.DragLeave += new System.EventHandler(this.bootSameTimeListView_DragLeave);
            // 
            // imageList
            // 
            this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
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
            this.splitContainer1.Panel1.Controls.Add(this.bootSameTimeListView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dragdropLabel);
            this.splitContainer1.Panel2.Controls.Add(this.deleteButton);
            this.splitContainer1.Panel2.Controls.Add(this.addButton);
            this.splitContainer1.Panel2MinSize = 30;
            this.splitContainer1.Size = new System.Drawing.Size(284, 150);
            this.splitContainer1.SplitterDistance = 118;
            this.splitContainer1.SplitterWidth = 2;
            this.splitContainer1.TabIndex = 178;
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(199, 3);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(75, 23);
            this.deleteButton.TabIndex = 30;
            this.deleteButton.Text = "削除";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(102, 3);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 20;
            this.addButton.Text = "追加";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // SameTimeBootTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "SameTimeBootTab";
            this.Size = new System.Drawing.Size(284, 150);
            this.Load += new System.EventHandler(this.SameTimeBootTab_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label dragdropLabel;
        private System.Windows.Forms.ListView bootSameTimeListView;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}
