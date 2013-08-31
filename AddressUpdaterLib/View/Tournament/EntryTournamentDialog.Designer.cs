namespace HisoutenSupportTools.AddressUpdater.Lib.View.Tournament
{
    partial class EntryTournamentDialog
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
            this.guestEntryButton = new System.Windows.Forms.Button();
            this.entryNameLabel = new System.Windows.Forms.Label();
            this.entryNameInput = new System.Windows.Forms.TextBox();
            this.entryButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.ip_portLabel = new System.Windows.Forms.Label();
            this.ipInput = new System.Windows.Forms.TextBox();
            this.portInput = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.portInput)).BeginInit();
            this.SuspendLayout();
            // 
            // guestEntryButton
            // 
            this.guestEntryButton.Location = new System.Drawing.Point(106, 91);
            this.guestEntryButton.Name = "guestEntryButton";
            this.guestEntryButton.Size = new System.Drawing.Size(84, 30);
            this.guestEntryButton.TabIndex = 50;
            this.guestEntryButton.Text = "ゲストで入室";
            this.guestEntryButton.UseVisualStyleBackColor = true;
            this.guestEntryButton.Click += new System.EventHandler(this.guestEntryButton_Click);
            // 
            // entryNameLabel
            // 
            this.entryNameLabel.AutoSize = true;
            this.entryNameLabel.Location = new System.Drawing.Point(12, 19);
            this.entryNameLabel.Name = "entryNameLabel";
            this.entryNameLabel.Size = new System.Drawing.Size(60, 12);
            this.entryNameLabel.TabIndex = 10;
            this.entryNameLabel.Text = "エントリー名";
            // 
            // entryNameInput
            // 
            this.entryNameInput.Location = new System.Drawing.Point(78, 16);
            this.entryNameInput.MaxLength = 20;
            this.entryNameInput.Name = "entryNameInput";
            this.entryNameInput.Size = new System.Drawing.Size(204, 19);
            this.entryNameInput.TabIndex = 10;
            this.entryNameInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.entryNameInput_KeyPress);
            // 
            // entryButton
            // 
            this.entryButton.Location = new System.Drawing.Point(14, 91);
            this.entryButton.Name = "entryButton";
            this.entryButton.Size = new System.Drawing.Size(84, 30);
            this.entryButton.TabIndex = 40;
            this.entryButton.Text = "参加する";
            this.entryButton.UseVisualStyleBackColor = true;
            this.entryButton.Click += new System.EventHandler(this.entryButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(198, 91);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(84, 30);
            this.cancelButton.TabIndex = 60;
            this.cancelButton.Text = "キャンセル";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // ip_portLabel
            // 
            this.ip_portLabel.AutoSize = true;
            this.ip_portLabel.Location = new System.Drawing.Point(12, 44);
            this.ip_portLabel.Name = "ip_portLabel";
            this.ip_portLabel.Size = new System.Drawing.Size(45, 12);
            this.ip_portLabel.TabIndex = 51;
            this.ip_portLabel.Text = "IP:ポート";
            // 
            // ipInput
            // 
            this.ipInput.Location = new System.Drawing.Point(78, 41);
            this.ipInput.MaxLength = 15;
            this.ipInput.Name = "ipInput";
            this.ipInput.Size = new System.Drawing.Size(130, 19);
            this.ipInput.TabIndex = 20;
            this.ipInput.TextChanged += new System.EventHandler(this.ipInput_TextChanged);
            this.ipInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ipInput_KeyPress);
            // 
            // portInput
            // 
            this.portInput.Location = new System.Drawing.Point(214, 41);
            this.portInput.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.portInput.Name = "portInput";
            this.portInput.Size = new System.Drawing.Size(68, 19);
            this.portInput.TabIndex = 30;
            this.portInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.portInput.Value = new decimal(new int[] {
            10800,
            0,
            0,
            0});
            this.portInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.portInput_KeyPress);
            // 
            // EntryTournamentDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(300, 141);
            this.Controls.Add(this.portInput);
            this.Controls.Add(this.ipInput);
            this.Controls.Add(this.ip_portLabel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.entryButton);
            this.Controls.Add(this.entryNameInput);
            this.Controls.Add(this.entryNameLabel);
            this.Controls.Add(this.guestEntryButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EntryTournamentDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "T99 エントリー";
            this.Load += new System.EventHandler(this.EntryTournamentDialog_Load);
            this.Shown += new System.EventHandler(this.EntryTournamentDialog_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.portInput)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button guestEntryButton;
        private System.Windows.Forms.Label entryNameLabel;
        private System.Windows.Forms.TextBox entryNameInput;
        private System.Windows.Forms.Button entryButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label ip_portLabel;
        private System.Windows.Forms.TextBox ipInput;
        private System.Windows.Forms.NumericUpDown portInput;
    }
}