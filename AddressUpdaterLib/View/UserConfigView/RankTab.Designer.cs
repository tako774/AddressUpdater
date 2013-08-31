namespace HisoutenSupportTools.AddressUpdater.Lib.View.UserConfigView
{
    partial class RankTab
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
            this.ranksInput = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ranksInput
            // 
            this.ranksInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ranksInput.Location = new System.Drawing.Point(0, 0);
            this.ranksInput.Multiline = true;
            this.ranksInput.Name = "ranksInput";
            this.ranksInput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.ranksInput.Size = new System.Drawing.Size(150, 150);
            this.ranksInput.TabIndex = 2;
            // 
            // RankTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ranksInput);
            this.Name = "RankTab";
            this.Load += new System.EventHandler(this.RankTab_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ranksInput;
    }
}
