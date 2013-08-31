using System;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using HisoutenSupportTools.AddressUpdater.Lib.Model.Config;

namespace HisoutenSupportTools.AddressUpdater.Lib.View.Tournament
{
    /// <summary>
    /// 大会参加ウィンドウ
    /// </summary>
    public partial class EntryTournamentDialog : Form
    {
        #region プロパティ
        /// <summary>
        /// ユーザー設定参照の取得・設定
        /// </summary>
        public UserConfig UserConfig { get; set; }

        /// <summary>
        /// テーマの取得・設定
        /// </summary>
        public Theme Theme { get; set; }

        /// <summary>
        /// 大会番号の取得・設定
        /// </summary>
        public int TournamentNo
        {
            get { return _tournamentNo; }
            set
            {
                if (_tournamentNo == value)
                    return;
                _tournamentNo = value;
                SetWindowText();
            }
        }
        private int _tournamentNo;

        /// <summary>エントリー名の取得・設定</summary>
        public string EntryName
        {
            get { return entryNameInput.Text.Trim(); }
            private set
            {
                if (entryNameInput.Text == value)
                    return;
                entryNameInput.Text = value;
            }
        }

        /// <summary>
        /// IPの取得・設定
        /// </summary>
        public string Ip
        {
            get { return ipInput.Text; }
            private set
            {
                if (ipInput.Text == value)
                    return;
                ipInput.Text = value;
            }
        }

        /// <summary>
        /// ポートの取得・設定
        /// </summary>
        public int Port
        {
            get { return (int)portInput.Value; }
            private set
            {
                if (portInput.Value == value)
                    return;
                portInput.Value = value;
            }
        }
        #endregion


        /// <summary>
        /// インスタンスの生成
        /// </summary>
        public EntryTournamentDialog()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EntryTournamentDialog_Load(object sender, EventArgs e)
        {
            ReflectTheme();

            EntryName = UserConfig.TournamentEntryName;
            Ip = UserConfig.ServerIp;
            Port = UserConfig.ServerPort;
        }

        /// <summary>
        /// テーマの反映
        /// </summary>
        private void ReflectTheme()
        {
            BackColor = Theme.ToolBackColor.ToColor();
            entryButton.BackColor = SystemColors.Control;
            entryButton.UseVisualStyleBackColor = true;
            guestEntryButton.BackColor = SystemColors.Control;
            guestEntryButton.UseVisualStyleBackColor = true;
            cancelButton.BackColor = SystemColors.Control;
            cancelButton.UseVisualStyleBackColor = true;

            entryNameLabel.ForeColor = Theme.GeneralTextColor.ToColor();
            ip_portLabel.ForeColor = Theme.GeneralTextColor.ToColor();

            entryNameInput.ForeColor = Theme.ChatForeColor.ToColor();
            ipInput.ForeColor = Theme.ChatForeColor.ToColor();
            portInput.ForeColor = Theme.ChatForeColor.ToColor();
            entryNameInput.BackColor = Theme.ChatBackColor.ToColor();
            ipInput.BackColor = Theme.ChatBackColor.ToColor();
            portInput.BackColor = Theme.ChatBackColor.ToColor();
        }

        /// <summary>
        /// Shown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EntryTournamentDialog_Shown(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(EntryName))
                entryNameInput.Focus();
            else
            {
                if (guestEntryButton.Enabled && guestEntryButton.Visible)
                    guestEntryButton.Focus();
                else
                    entryButton.Focus();
            }
        }

        /// <summary>
        /// ウィンドウテキストのセット
        /// </summary>
        void SetWindowText()
        {
            Text = string.Format("T{0} 入室", _tournamentNo);
        }

        /// <summary>
        /// IP入力欄テキスト変更時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ipInput_TextChanged(object sender, EventArgs e)
        {
            if (ValidateIp())
                ipInput.BackColor = Theme.ChatBackColor.ToColor();
            else
                ipInput.BackColor = Color.Pink;
        }

        /// <summary>
        /// IP入力を検証する
        /// </summary>
        /// <returns>true:正常 / false:異常</returns>
        private bool ValidateIp()
        {
            if (ipInput.Text.Replace(" ", string.Empty) == string.Empty)
                return true;

            var ipParts = ipInput.Text.Split('.');

            if (ipParts.Length != 4)
                return false;

            if (ipParts[0].Replace(" ", string.Empty) == "10")
                return false;

            if (ipParts[0].Replace(" ", string.Empty) == "192" && ipParts[1].Replace(" ", string.Empty) == "168")
                return false;

            try
            {
                if (ipParts[0].Replace(" ", string.Empty) == "172" && 16 <= int.Parse(ipParts[1].Replace(" ", string.Empty)) && int.Parse(ipParts[1].Replace(" ", string.Empty)) <= 31)
                    return false;

                IPAddress.Parse(ipInput.Text.Replace(" ", string.Empty));
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private void entryButton_Click(object sender, EventArgs e)
        {
            if (!ValidateIp())
            {
                // IP不正
                MessageBox.Show(
                    this,
                    "IPを正しく入力してください。", Application.ProductName,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ipInput.Focus();
                return;
            }

            if (EntryName.Replace(" ", "").Replace("　", "") == string.Empty)
            {
                // エントリー名不正
                MessageBox.Show(
                    this,
                    "大会に参加する場合は、エントリー名を入力してください。", Application.ProductName,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                entryNameInput.Focus();
                return;
            }

            UserConfig.TournamentEntryName = EntryName;
            DialogResult = DialogResult.Yes;
        }

        private void guestEntryButton_Click(object sender, EventArgs e)
        {
            UserConfig.TournamentEntryName = EntryName;
            DialogResult = DialogResult.No;
        }

        private void entryNameInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                ipInput.Focus();
                e.Handled = true;
            }
        }

        private void ipInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                portInput.Focus();
                e.Handled = true;
            }
        }

        private void portInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                entryButton.Focus();
                e.Handled = true;
            }
        }
    }
}
