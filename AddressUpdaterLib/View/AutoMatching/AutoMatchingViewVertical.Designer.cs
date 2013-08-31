namespace HisoutenSupportTools.AddressUpdater.Lib.View.AutoMatching
{
    partial class AutoMatchingViewVertical
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
            this.informationPanel.Location = new System.Drawing.Point(1, 29);
            this.informationPanel.Size = new System.Drawing.Size(293, 205);
            // 
            // resultPanel
            // 
            this.resultPanel.Location = new System.Drawing.Point(1, 29);
            this.resultPanel.Size = new System.Drawing.Size(293, 205);
            // 
            // commentInput
            // 
            this.commentInput.Location = new System.Drawing.Point(3, 126);
            this.commentInput.Size = new System.Drawing.Size(283, 72);
            // 
            // commentLabel
            // 
            this.commentLabel.Location = new System.Drawing.Point(3, 108);
            this.commentLabel.Visible = false;
            // 
            // portInput
            // 
            this.portInput.Location = new System.Drawing.Point(227, 101);
            // 
            // ip_portLabel
            // 
            this.ip_portLabel.Location = new System.Drawing.Point(50, 103);
            // 
            // ipInput
            // 
            this.ipInput.Location = new System.Drawing.Point(101, 101);
            // 
            // isRoomOnlySelect
            // 
            this.isRoomOnlySelect.Location = new System.Drawing.Point(170, 79);
            // 
            // isHostableSelect
            // 
            this.isHostableSelect.Location = new System.Drawing.Point(101, 79);
            // 
            // matchingSpanInput
            // 
            this.matchingSpanInput.Location = new System.Drawing.Point(101, 54);
            this.matchingSpanInput.Size = new System.Drawing.Size(120, 19);
            // 
            // accountNameInput
            // 
            this.accountNameInput.Size = new System.Drawing.Size(184, 19);
            // 
            // matchingSpanLabel
            // 
            this.matchingSpanLabel.Location = new System.Drawing.Point(19, 56);
            this.matchingSpanLabel.Size = new System.Drawing.Size(76, 12);
            this.matchingSpanLabel.Text = "マッチング幅 ±";
            // 
            // characterSelect
            // 
            this.characterSelect.Location = new System.Drawing.Point(101, 28);
            this.characterSelect.Size = new System.Drawing.Size(184, 20);
            // 
            // characterLabel
            // 
            this.characterLabel.Location = new System.Drawing.Point(64, 31);
            // 
            // oponentCommentOutput
            // 
            this.oponentCommentOutput.Location = new System.Drawing.Point(3, 101);
            this.oponentCommentOutput.Size = new System.Drawing.Size(280, 97);
            // 
            // oponentIsRoomOnlyLabel
            // 
            this.oponentIsRoomOnlyLabel.Location = new System.Drawing.Point(132, 80);
            // 
            // oponentRatingLabel
            // 
            this.oponentRatingLabel.Location = new System.Drawing.Point(4, 80);
            // 
            // AutoMatchingViewVertical
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "AutoMatchingViewVertical";
            this.Size = new System.Drawing.Size(299, 239);
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
