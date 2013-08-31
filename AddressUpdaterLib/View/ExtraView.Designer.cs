namespace HisoutenSupportTools.AddressUpdater.Lib.View
{
    partial class ExtraView
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
            this.sameTimeChangeCheckBox = new System.Windows.Forms.CheckBox();
            this.setWindowPosButton = new System.Windows.Forms.Button();
            this.windowCaptionInput = new System.Windows.Forms.TextBox();
            this.getWindowRectButton = new System.Windows.Forms.Button();
            this.heightInput = new System.Windows.Forms.NumericUpDown();
            this.heightLabel = new System.Windows.Forms.Label();
            this.widthInput = new System.Windows.Forms.NumericUpDown();
            this.widthLabel = new System.Windows.Forms.Label();
            this.yInput = new System.Windows.Forms.NumericUpDown();
            this.yLabel = new System.Windows.Forms.Label();
            this.xInput = new System.Windows.Forms.NumericUpDown();
            this.xLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.heightInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.widthInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.yInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xInput)).BeginInit();
            this.SuspendLayout();
            // 
            // sameTimeChangeCheckBox
            // 
            this.sameTimeChangeCheckBox.AutoSize = true;
            this.sameTimeChangeCheckBox.Location = new System.Drawing.Point(297, 8);
            this.sameTimeChangeCheckBox.Name = "sameTimeChangeCheckBox";
            this.sameTimeChangeCheckBox.Size = new System.Drawing.Size(72, 16);
            this.sameTimeChangeCheckBox.TabIndex = 126;
            this.sameTimeChangeCheckBox.Text = "同時変更";
            this.sameTimeChangeCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.sameTimeChangeCheckBox.UseVisualStyleBackColor = true;
            // 
            // setWindowPosButton
            // 
            this.setWindowPosButton.Location = new System.Drawing.Point(150, 100);
            this.setWindowPosButton.Name = "setWindowPosButton";
            this.setWindowPosButton.Size = new System.Drawing.Size(113, 36);
            this.setWindowPosButton.TabIndex = 132;
            this.setWindowPosButton.Text = "移動";
            this.setWindowPosButton.UseVisualStyleBackColor = true;
            this.setWindowPosButton.Click += new System.EventHandler(this.setWindowPosButton_Click);
            // 
            // windowCaptionInput
            // 
            this.windowCaptionInput.Location = new System.Drawing.Point(6, 6);
            this.windowCaptionInput.Name = "windowCaptionInput";
            this.windowCaptionInput.Size = new System.Drawing.Size(285, 19);
            this.windowCaptionInput.TabIndex = 125;
            this.windowCaptionInput.Text = "東方非想天則 ～ 超弩級ギニョルの謎を追え Ver1.03";
            this.windowCaptionInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // getWindowRectButton
            // 
            this.getWindowRectButton.Location = new System.Drawing.Point(6, 100);
            this.getWindowRectButton.Name = "getWindowRectButton";
            this.getWindowRectButton.Size = new System.Drawing.Size(114, 36);
            this.getWindowRectButton.TabIndex = 131;
            this.getWindowRectButton.Text = "現在位置取得";
            this.getWindowRectButton.UseVisualStyleBackColor = true;
            this.getWindowRectButton.Click += new System.EventHandler(this.getWindowRectButton_Click);
            // 
            // heightInput
            // 
            this.heightInput.Location = new System.Drawing.Point(181, 66);
            this.heightInput.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.heightInput.Name = "heightInput";
            this.heightInput.Size = new System.Drawing.Size(82, 19);
            this.heightInput.TabIndex = 130;
            this.heightInput.Value = new decimal(new int[] {
            480,
            0,
            0,
            0});
            // 
            // heightLabel
            // 
            this.heightLabel.AutoSize = true;
            this.heightLabel.Location = new System.Drawing.Point(148, 68);
            this.heightLabel.Name = "heightLabel";
            this.heightLabel.Size = new System.Drawing.Size(25, 12);
            this.heightLabel.TabIndex = 124;
            this.heightLabel.Text = "高さ";
            // 
            // widthInput
            // 
            this.widthInput.Location = new System.Drawing.Point(38, 66);
            this.widthInput.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.widthInput.Name = "widthInput";
            this.widthInput.Size = new System.Drawing.Size(82, 19);
            this.widthInput.TabIndex = 129;
            this.widthInput.Value = new decimal(new int[] {
            640,
            0,
            0,
            0});
            // 
            // widthLabel
            // 
            this.widthLabel.AutoSize = true;
            this.widthLabel.Location = new System.Drawing.Point(4, 68);
            this.widthLabel.Name = "widthLabel";
            this.widthLabel.Size = new System.Drawing.Size(17, 12);
            this.widthLabel.TabIndex = 123;
            this.widthLabel.Text = "幅";
            // 
            // yInput
            // 
            this.yInput.Location = new System.Drawing.Point(181, 37);
            this.yInput.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.yInput.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.yInput.Name = "yInput";
            this.yInput.Size = new System.Drawing.Size(82, 19);
            this.yInput.TabIndex = 128;
            // 
            // yLabel
            // 
            this.yLabel.AutoSize = true;
            this.yLabel.Location = new System.Drawing.Point(148, 39);
            this.yLabel.Name = "yLabel";
            this.yLabel.Size = new System.Drawing.Size(12, 12);
            this.yLabel.TabIndex = 122;
            this.yLabel.Text = "Y";
            // 
            // xInput
            // 
            this.xInput.Location = new System.Drawing.Point(38, 37);
            this.xInput.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.xInput.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.xInput.Name = "xInput";
            this.xInput.Size = new System.Drawing.Size(82, 19);
            this.xInput.TabIndex = 127;
            // 
            // xLabel
            // 
            this.xLabel.AutoSize = true;
            this.xLabel.Location = new System.Drawing.Point(4, 39);
            this.xLabel.Name = "xLabel";
            this.xLabel.Size = new System.Drawing.Size(12, 12);
            this.xLabel.TabIndex = 121;
            this.xLabel.Text = "X";
            // 
            // ExtraView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.sameTimeChangeCheckBox);
            this.Controls.Add(this.setWindowPosButton);
            this.Controls.Add(this.windowCaptionInput);
            this.Controls.Add(this.getWindowRectButton);
            this.Controls.Add(this.heightInput);
            this.Controls.Add(this.heightLabel);
            this.Controls.Add(this.widthInput);
            this.Controls.Add(this.widthLabel);
            this.Controls.Add(this.yInput);
            this.Controls.Add(this.yLabel);
            this.Controls.Add(this.xInput);
            this.Controls.Add(this.xLabel);
            this.Name = "ExtraView";
            this.Size = new System.Drawing.Size(418, 143);
            this.Load += new System.EventHandler(this.ExtraView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.heightInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.widthInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.yInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xInput)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox sameTimeChangeCheckBox;
        private System.Windows.Forms.Button setWindowPosButton;
        private System.Windows.Forms.TextBox windowCaptionInput;
        private System.Windows.Forms.Button getWindowRectButton;
        private System.Windows.Forms.NumericUpDown heightInput;
        private System.Windows.Forms.Label heightLabel;
        private System.Windows.Forms.NumericUpDown widthInput;
        private System.Windows.Forms.Label widthLabel;
        private System.Windows.Forms.NumericUpDown yInput;
        private System.Windows.Forms.Label yLabel;
        private System.Windows.Forms.NumericUpDown xInput;
        private System.Windows.Forms.Label xLabel;
    }
}
