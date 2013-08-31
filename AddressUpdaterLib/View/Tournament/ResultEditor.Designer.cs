namespace HisoutenSupportTools.AddressUpdater.Lib.View.Tournament
{
    partial class ResultEditor
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
            this.resultTextLabel = new System.Windows.Forms.Label();
            this.resultSelector = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // statusTextLabel
            // 
            this.resultTextLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultTextLabel.Location = new System.Drawing.Point(0, 0);
            this.resultTextLabel.Name = "statusTextLabel";
            this.resultTextLabel.Size = new System.Drawing.Size(96, 20);
            this.resultTextLabel.TabIndex = 0;
            this.resultTextLabel.Text = "label1";
            this.resultTextLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.resultTextLabel.Click += new System.EventHandler(this.resultTextLabel_Click);
            // 
            // statusSelector
            // 
            this.resultSelector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.resultSelector.Enabled = false;
            this.resultSelector.FormattingEnabled = true;
            this.resultSelector.Location = new System.Drawing.Point(0, 0);
            this.resultSelector.Name = "statusSelector";
            this.resultSelector.Size = new System.Drawing.Size(96, 20);
            this.resultSelector.TabIndex = 1;
            this.resultSelector.Visible = false;
            this.resultSelector.SelectedIndexChanged += new System.EventHandler(this.resultSelector_SelectedIndexChanged);
            this.resultSelector.DropDownClosed += new System.EventHandler(this.resultSelector_DropDownClosed);
            // 
            // StatusEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.resultTextLabel);
            this.Controls.Add(this.resultSelector);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DoubleBuffered = true;
            this.Name = "StatusEditor";
            this.Size = new System.Drawing.Size(96, 20);
            this.Load += new System.EventHandler(this.ResultEditor_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label resultTextLabel;
        private System.Windows.Forms.ComboBox resultSelector;
    }
}
