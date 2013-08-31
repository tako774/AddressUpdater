using System;
using System.Drawing;
using System.Windows.Forms;
using HisoutenSupportTools.AddressUpdater.Lib.Model.Config;

namespace HisoutenSupportTools.AddressUpdater.Lib.View
{
    /// <summary>
    /// 遮断ダイアログ
    /// </summary>
    public partial class AddInterceptDialog : Form
    {
        /// <summary>
        /// 遮断するIDの取得・設定
        /// </summary>
        public string Id
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }

        /// <summary>
        /// テーマの取得・設定
        /// </summary>
        public Theme Theme { get; set; }

        /// <summary>
        /// インスタンスの生成
        /// </summary>
        public AddInterceptDialog()
        {
            InitializeComponent();
        }


        private void AddInterceptDialog_Load(object sender, EventArgs e)
        {
            ActiveControl = okButton;

            ReflectTheme();
        }

        private void ReflectTheme()
        {
            BackColor = Theme.ToolBackColor.ToColor();
            okButton.BackColor = SystemColors.Control;
            okButton.UseVisualStyleBackColor = true;
            cancelButton.BackColor = SystemColors.Control;
            cancelButton.UseVisualStyleBackColor = true;

            label1.ForeColor = Theme.GeneralTextColor.ToColor();
            textBox1.ForeColor = Theme.ChatForeColor.ToColor();
            textBox1.BackColor = Theme.ChatBackColor.ToColor();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                okButton.Focus();
                e.Handled = true;
            }
        }
    }
}
