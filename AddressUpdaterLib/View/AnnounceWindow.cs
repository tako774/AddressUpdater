using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows.Forms;
using HisoutenSupportTools.AddressUpdater.Lib.Model.Config;

namespace HisoutenSupportTools.AddressUpdater.Lib.View
{
    /// <summary>
    /// アナウンスウィンドウ
    /// </summary>
    public partial class AnnounceWindow : Form
    {
        private const int MIN_WIDTH = 400;
        private const int MIN_HEIGHT = 200;

        /// <summary>
        /// テーマの取得・設定
        /// </summary>
        public Theme Theme { get; set; }

        /// <summary>
        /// アナウンスの取得・設定
        /// </summary>
        /// <remarks>コピーのやりとり</remarks>
        public Collection<string> Announces
        {
            get
            {
                var announces = new Collection<string>();

                var lines = announceInput.Lines;
                foreach (var line in lines)
                {
                    if (!string.IsNullOrEmpty(line))
                        announces.Add(line);
                }

                return announces;
            }
            set
            {
                announceInput.Clear();
                announceInput.Text = string.Empty;
                if (value == null)
                    return;

                for (var i = 0; i < value.Count; i++)
                {
                    announceInput.AppendText(value[i]);
                    if (i < value.Count - 1)
                        announceInput.AppendText(Environment.NewLine);
                }
            }
        }


        /// <summary>
        /// インスタンスの生成
        /// </summary>
        public AnnounceWindow()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnnounceWindow_Load(object sender, EventArgs e)
        {
            ReflectTheme();
        }

        /// <summary>
        /// テーマの反映
        /// </summary>
        public void ReflectTheme()
        {
            BackColor = Theme.ToolBackColor.ToColor();
            okButton.BackColor = SystemColors.Control;
            okButton.UseVisualStyleBackColor = true;
            cancelButton.BackColor = SystemColors.Control;
            cancelButton.UseVisualStyleBackColor = true;

            announceInput.ForeColor = Theme.ChatForeColor.ToColor();
            announceInput.BackColor = Theme.ChatBackColor.ToColor();

            Theme.ChatFont.SetFont(announceInput);
        }

        /// <summary>
        /// サイズを設定
        /// </summary>
        /// <param name="size"></param>
        public void SetSize(Size size)
        {
            if (size.Width < MIN_WIDTH)
                size.Width = MIN_WIDTH;
            if (size.Height < MIN_HEIGHT)
                size.Height = MIN_HEIGHT;

            Size = size;
        }

        /// <summary>
        /// OK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void okButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// 表示状態変更時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnnounceWindow_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
                ActiveControl = announceInput;
        }
    }
}
