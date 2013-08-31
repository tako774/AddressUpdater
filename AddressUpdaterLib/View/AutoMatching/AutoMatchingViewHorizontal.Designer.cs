namespace HisoutenSupportTools.AddressUpdater.Lib.View.AutoMatching
{
    partial class AutoMatchingViewHorizontal
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
            this.informationPanel.SuspendLayout();
            this.resultPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.portInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.matchingSpanInput)).BeginInit();
            this.SuspendLayout();
            // 
            // informationPanel
            // 
            this.informationPanel.Size = new System.Drawing.Size(545, 134);
            // 
            // resultPanel
            // 
            this.resultPanel.Location = new System.Drawing.Point(107, 3);
            this.resultPanel.Size = new System.Drawing.Size(545, 134);
            // 
            // commentInput
            // 
            this.commentInput.Size = new System.Drawing.Size(438, 74);
            // 
            // ip_portLabel
            // 
            this.ip_portLabel.Location = new System.Drawing.Point(282, 30);
            // 
            // ipInput
            // 
            this.ipInput.Location = new System.Drawing.Point(333, 27);
            // 
            // characterSelect
            // 
            this.characterSelect.Location = new System.Drawing.Point(333, 3);
            this.characterSelect.Size = new System.Drawing.Size(78, 20);
            // 
            // characterLabel
            // 
            this.characterLabel.Location = new System.Drawing.Point(296, 6);
            // 
            // skipButton
            // 
            this.skipButton.Location = new System.Drawing.Point(3, 25);
            // 
            // AutoMatchingViewHorizontal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "AutoMatchingViewHorizontal";
            this.Size = new System.Drawing.Size(656, 141);
            this.informationPanel.ResumeLayout(false);
            this.informationPanel.PerformLayout();
            this.resultPanel.ResumeLayout(false);
            this.resultPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.portInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.matchingSpanInput)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

    }
}
