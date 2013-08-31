using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Net;
using System.Windows.Forms;
using HisoutenSupportTools.AddressUpdater.Lib.Model.Config;

namespace HisoutenSupportTools.AddressUpdater.Lib.View.UserConfigView
{
    /// <summary>
    /// 一般タブ
    /// </summary>
    public partial class GeneralTab : TabBase
    {
        private string _defaultThemeName = "デフォルト";

        private Dictionary<string, Theme> _themes;

        #region property
        /// <summary>IPの取得・設定</summary>
        public string Ip
        {
            get { return ipInput.Text; }
            set { ipInput.Text = value; }
        }

        /// <summary>ポートの取得・設定</summary>
        public int Port
        {
            get { return (int)portInput.Value; }
            set { portInput.Value = value; }
        }

        /// <summary>大会を表示するかどうかの取得・設定</summary>
        public bool ShowTournaments
        {
            get { return isVisibleTournamentCheckBox.Checked; }
            set { isVisibleTournamentCheckBox.Checked = value; }
        }

        /// <summary>多重起動を禁止するかどうかの取得・設定</summary>
        public bool DisableMultiBoot
        {
            get { return isDisableMultiBootCheckBox.Checked; }
            set { isDisableMultiBootCheckBox.Checked = value; }
        }

        /// <summary>
        /// 更新間隔（秒）の取得・設定
        /// </summary>
        public int UpdateSpan
        {
            get { return (int)updateSpanInput.Value; }
            set { updateSpanInput.Value = value; }
        }

        /// <summary>チャット先頭文字列の取得・設定</summary>
        public string ChatPrefix
        {
            get { return chatPrefixInput.Text; }
            set { chatPrefixInput.Text = value; }
        }

        /// <summary>クライアント表示方向の取得・設定</summary>
        public Orientation ClientDivisionOrientation
        {
            get
            {
                if (verticalRadioButton.Checked)
                    return Orientation.Vertical;
                else
                    return Orientation.Horizontal;
            }
            set
            {
                if (value == Orientation.Vertical)
                {
                    horizontalRadioButton.Checked = false;
                    verticalRadioButton.Checked = true;
                }
                else
                {
                    verticalRadioButton.Checked = false;
                    horizontalRadioButton.Checked = true;
                }
            }
        }
        #endregion

        /// <summary>
        /// テーマ反映要求発生時に呼び出されます。
        /// </summary>
        public event EventHandler ReflectThemeRequested;

        /// <summary>
        /// インスタンスの生成
        /// </summary>
        public GeneralTab()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GeneralTab_Load(object sender, EventArgs e)
        {
            if (UserConfig == null)
                return;

            Ip = UserConfig.ServerIp;
            Port = UserConfig.ServerPort;
            ShowTournaments = UserConfig.ShowTournaments;
            DisableMultiBoot = UserConfig.DisableMultiBoot;
            UpdateSpan = UserConfig.UpdateSpan;
            ChatPrefix = UserConfig.ChatPrefix;
            ClientDivisionOrientation = UserConfig.ClientDivisionOrientation;

            var families = new InstalledFontCollection().Families;
            foreach (var family in families)
            {
                if (family.IsStyleAvailable(FontStyle.Regular))
                {
                    hostFontSelect.Items.Add(family.Name);
                    chatFontSelect.Items.Add(family.Name);
                }
            }

            LoadThemes();
        }

        /// <summary>
        /// 表示言語の反映
        /// </summary>
        /// <param name="language"></param>
        public void ReflectLanguage(Language language)
        {
            try { ipLabel.Text = language["GeneralTab_DefaultIp"]; }
            catch (KeyNotFoundException) { }
            try { portLabel.Text = language["GeneralTab_DefaultPort"]; }
            catch (KeyNotFoundException) { }
            try { updateSpanLabel.Text = language["GeneralTab_UpdateSpan"]; }
            catch (KeyNotFoundException) { }
            try { tournamentLabel.Text = language["GeneralTab_Tournament"]; }
            catch (KeyNotFoundException) { }
            try { isVisibleTournamentCheckBox.Text = language["GeneralTab_IsVisibleTournament"]; }
            catch (KeyNotFoundException) { }
            try { multiBootLabel.Text = language["GeneralTab_MultiBoot"]; }
            catch (KeyNotFoundException) { }
            try { isDisableMultiBootCheckBox.Text = language["GeneralTab_IsDisableMultiBoot"]; }
            catch (KeyNotFoundException) { }
            try { chatPrefixLabel.Text = language["GeneralTab_ChatPrefix"]; }
            catch (KeyNotFoundException) { }
            try { clientDivisionOrientationLabel.Text = language["GeneralTab_ClientOrientation"]; }
            catch (KeyNotFoundException) { }
            try { horizontalRadioButton.Text = language["GeneralTab_Horizontal"]; }
            catch (KeyNotFoundException) { }
            try { verticalRadioButton.Text = language["GeneralTab_Vertical"]; }
            catch (KeyNotFoundException) { }
            try { themeNameLabel.Text = language["GeneralTab_ThemeName"]; }
            catch (KeyNotFoundException) { }
            try { saveThemeButton.Text = language["GeneralTab_SaveTheme"]; }
            catch (KeyNotFoundException) { }
            try
            {
                _defaultThemeName = language["GeneralTab_DefaultThemeName"];
                LoadThemes();
            }
            catch (KeyNotFoundException) { }
            try { toolBackColorLabel.Text = language["GeneralTab_ToolBackColor"]; }
            catch (KeyNotFoundException) { }
            try { generalTextColorLabel.Text = language["GeneralTab_GeneralTextColor"]; }
            catch (KeyNotFoundException) { }
            try { waitingHostColorLabel.Text = language["GeneralTab_WaitingHostBackColor"]; }
            catch (KeyNotFoundException) { }
            try { fightingHostColorLabel.Text = language["GeneralTab_FightingHostBackColor"]; }
            catch (KeyNotFoundException) { }
            try { chatForeColorLabel.Text = language["GeneralTab_ChatForeColor"]; }
            catch (KeyNotFoundException) { }
            try { chatBackColorLabel.Text = language["GeneralTab_ChatBackColor"]; }
            catch (KeyNotFoundException) { }
            try { hostFontLabel.Text = language["GeneralTab_HostFont"]; }
            catch (KeyNotFoundException) { }
            try { chatFontLabel.Text = language["GeneralTab_ChatFont"]; }
            catch (KeyNotFoundException) { }
        }

        /// <summary>
        /// テーマの反映
        /// </summary>
        public override void ReflectTheme()
        {
            BackColor = Theme.ToolBackColor.ToColor();
            saveThemeButton.BackColor = SystemColors.Control;
            saveThemeButton.UseVisualStyleBackColor = true;

            toolBackColorDialog.Color = Theme.ToolBackColor.ToColor();
            toolBackColorPanel.BackColor = Theme.ToolBackColor.ToColor();

            generalTextColorDialog.Color = Theme.GeneralTextColor.ToColor();
            generalTextColorPanel.BackColor = Theme.GeneralTextColor.ToColor();
            foreach (Control control in Controls)
            {
                if (control is Label || control is CheckBox)
                    control.ForeColor = Theme.GeneralTextColor.ToColor();
            }
            horizontalRadioButton.ForeColor = Theme.GeneralTextColor.ToColor();
            verticalRadioButton.ForeColor = Theme.GeneralTextColor.ToColor();

            waitingColorDialog.Color = Theme.WaitingHostBackColor.ToColor();
            waitingHostColorPanel.BackColor = Theme.WaitingHostBackColor.ToColor();

            fightingColorDialog.Color = Theme.FightingHostBackColor.ToColor();
            fightingHostColorPanel.BackColor = Theme.FightingHostBackColor.ToColor();

            chatForeColorDialog.Color = Theme.ChatForeColor.ToColor();
            chatForeColorPanel.BackColor = Theme.ChatForeColor.ToColor();
            ipInput.ForeColor = Theme.ChatForeColor.ToColor();
            portInput.ForeColor = Theme.ChatForeColor.ToColor();
            updateSpanInput.ForeColor = Theme.ChatForeColor.ToColor();
            chatPrefixInput.ForeColor = Theme.ChatForeColor.ToColor();
            themeNameInput.ForeColor = Theme.ChatForeColor.ToColor();
            hostFontSelect.ForeColor = Theme.ChatForeColor.ToColor();
            chatFontSelect.ForeColor = Theme.ChatForeColor.ToColor();

            chatBackColorDialog.Color = Theme.ChatBackColor.ToColor();
            chatBackColorPanel.BackColor = Theme.ChatBackColor.ToColor();
            ipInput.BackColor = Theme.ChatBackColor.ToColor();
            portInput.BackColor = Theme.ChatBackColor.ToColor();
            updateSpanInput.BackColor = Theme.ChatBackColor.ToColor();
            chatPrefixInput.BackColor = Theme.ChatBackColor.ToColor();
            themeNameInput.BackColor = Theme.ChatBackColor.ToColor();
            hostFontSelect.BackColor = Theme.ChatBackColor.ToColor();
            chatFontSelect.BackColor = Theme.ChatBackColor.ToColor();

            themeListBox.BackColor = Theme.ToolBackColor.ToColor();
            themeListBox.ForeColor = Theme.GeneralTextColor.ToColor();


            Theme.ChatFont.SetFont(chatPrefixInput);
            Theme.HostFont.SetFont(hostFontSelect);
            Theme.ChatFont.SetFont(chatFontSelect);
            hostFontSelect.Text = Theme.HostFont.Name;
            chatFontSelect.Text = Theme.ChatFont.Name;
        }


        private void LoadThemes()
        {
            _themes = themeLoader.Load();

            themeListBox.Items.Clear();
            themeListBox.Items.Add(_defaultThemeName);
            foreach (var theme in _themes)
                themeListBox.Items.Add(theme.Key);
        }

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

        private void themeListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (themeListBox.SelectedItem == null)
                return;

            themeNameInput.Text = themeListBox.SelectedItem.ToString();

            if (themeListBox.SelectedItem.ToString() == _defaultThemeName)
            {
                Theme.ToolBackColor = ColorData.FromColor(SystemColors.Window);
                Theme.GeneralTextColor = ColorData.FromColor(SystemColors.ControlText);
                Theme.WaitingHostBackColor = ColorData.FromColor(SystemColors.Window);
                Theme.FightingHostBackColor = new ColorData(230, 230, 230);
                Theme.ChatForeColor = ColorData.FromColor(SystemColors.WindowText);
                Theme.ChatBackColor = ColorData.FromColor(SystemColors.Window);
                Theme.HostFont = new FontName() { Name = "MS UI Gothic" };
                Theme.ChatFont = new FontName() { Name = "ＭＳ ゴシック" };
            }
            else
            {
                foreach (var theme in _themes)
                {
                    if (themeListBox.SelectedItem.ToString() == theme.Key)
                    {
                        Theme.ToolBackColor = theme.Value.ToolBackColor;
                        Theme.GeneralTextColor = theme.Value.GeneralTextColor;
                        Theme.WaitingHostBackColor = theme.Value.WaitingHostBackColor;
                        Theme.FightingHostBackColor = theme.Value.FightingHostBackColor;
                        Theme.ChatForeColor = theme.Value.ChatForeColor;
                        Theme.ChatBackColor = theme.Value.ChatBackColor;
                        Theme.HostFont = theme.Value.HostFont == null ? new FontName() { Name = "MS UI Gothic" } : theme.Value.HostFont;
                        Theme.ChatFont = theme.Value.ChatFont == null ? new FontName() { Name = "ＭＳ ゴシック" } : theme.Value.ChatFont;
                        break;
                    }
                }
            }

            if (ReflectThemeRequested != null)
                ReflectThemeRequested(this, EventArgs.Empty);
        }

        private void saveThemeButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(themeNameInput.Text))
                return;

            if (themeNameInput.Text == _defaultThemeName)
            {
                var message = "という名前は使用できません。";
                try { message = Language["GeneralTab_IllegalThemeName"]; }
                catch (KeyNotFoundException) { }

                MessageBox.Show(
                    ParentForm,
                    "[" + _defaultThemeName + "]" + message,
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                LoadThemes();
                return;
            }

            try
            {
                themeLoader.Save(themeNameInput.Text, Theme);
                LoadThemes();
            }
            catch (UnauthorizedAccessException ex) { System.Diagnostics.Debug.WriteLine(ex.Message); }
            catch (IOException ex) { System.Diagnostics.Debug.WriteLine(ex.Message); }
        }


        private void toolColorPanel_Click(object sender, EventArgs e)
        {
            if (toolBackColorDialog.ShowDialog() != DialogResult.OK)
                return;

            Theme.ToolBackColor = ColorData.FromColor(toolBackColorDialog.Color);
            if (ReflectThemeRequested != null)
                ReflectThemeRequested(this, EventArgs.Empty);
        }

        private void generalTextColorPanel_Click(object sender, EventArgs e)
        {
            if (generalTextColorDialog.ShowDialog() != DialogResult.OK)
                return;

            Theme.GeneralTextColor = ColorData.FromColor(generalTextColorDialog.Color);
            if (ReflectThemeRequested != null)
                ReflectThemeRequested(this, EventArgs.Empty);
        }

        private void waitingHostColorPanel_Click(object sender, EventArgs e)
        {
            if (waitingColorDialog.ShowDialog() != DialogResult.OK)
                return;

            Theme.WaitingHostBackColor = ColorData.FromColor(waitingColorDialog.Color);
            if (ReflectThemeRequested != null)
                ReflectThemeRequested(this, EventArgs.Empty);
        }

        private void fightingHostColorPanel_Click(object sender, EventArgs e)
        {
            if (fightingColorDialog.ShowDialog() != DialogResult.OK)
                return;

            Theme.FightingHostBackColor = ColorData.FromColor(fightingColorDialog.Color);
            if (ReflectThemeRequested != null)
                ReflectThemeRequested(this, EventArgs.Empty);
        }

        private void chatForeColorPanel_Click(object sender, EventArgs e)
        {
            if (chatForeColorDialog.ShowDialog() != DialogResult.OK)
                return;

            Theme.ChatForeColor = ColorData.FromColor(chatForeColorDialog.Color);
            if (ReflectThemeRequested != null)
                ReflectThemeRequested(this, EventArgs.Empty);
        }

        private void chatBackColorpanel_Click(object sender, EventArgs e)
        {
            if (chatBackColorDialog.ShowDialog() != DialogResult.OK)
                return;

            Theme.ChatBackColor = ColorData.FromColor(chatBackColorDialog.Color);
            if (ReflectThemeRequested != null)
                ReflectThemeRequested(this, EventArgs.Empty);
        }

        private void hostFontSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hostFontSelect.Text))
                return;

            Theme.HostFont = new FontName() { Name = hostFontSelect.Text };
            if (ReflectThemeRequested != null)
                ReflectThemeRequested(this, EventArgs.Empty);
        }

        private void chatFontSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(chatFontSelect.Text))
                return;

            Theme.ChatFont = new FontName() { Name = chatFontSelect.Text };
            if (ReflectThemeRequested != null)
                ReflectThemeRequested(this, EventArgs.Empty);
        }

        private void fontSelect_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            e.DrawFocusRectangle();

            var text = string.Empty;
            if (0 <= e.Index)
                text = hostFontSelect.Items[e.Index].ToString();
            else
                text = hostFontSelect.Text;

            using (var font = new Font(text, hostFontSelect.Font.Size))
            using (var brush = new SolidBrush(e.ForeColor))
            {
                float margin_top = (e.Bounds.Height - e.Graphics.MeasureString(text, font).Height) / 2;
                e.Graphics.DrawString(text, font, brush, e.Bounds.X, e.Bounds.Y + margin_top);
            }
        }
    }
}
