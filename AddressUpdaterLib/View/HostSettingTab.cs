using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using HisoutenSupportTools.AddressUpdater.Lib.Model;
using HisoutenSupportTools.AddressUpdater.Lib.Tenco;
using HisoutenSupportTools.AddressUpdater.Lib.ViewModel;

namespace HisoutenSupportTools.AddressUpdater.Lib.View
{
    /// <summary>
    /// ホスト設定タブ
    /// </summary>
    public partial class HostSettingTab : TabBase
    {
        /// <summary>
        /// HostSettingViewModelの取得・設定
        /// </summary>
        public HostSettingViewModel ViewModel { get; set; }


        /// <summary>
        /// インスタンスの生成
        /// </summary>
        public HostSettingTab()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 閉じる（ポート開けてたら閉じる）
        /// </summary>
        public void Close()
        {
            if (!ViewModel.IsPortClosed)
                ViewModel.ClosePort();
        }

        private bool _isLoading = false;
        /// <summary>
        /// Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HostSettingTab_Load(object sender, EventArgs e)
        {
            if (Language != null)
                ReflectLanguage();

            if (UserConfig == null)
                return;

            if (ViewModel == null)
                return;

            ViewModel.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ViewModel_PropertyChanged);
            ViewModel.UserConfig = UserConfig;

            SetBindings();

            _isLoading = true;
            try
            {
                if (UserConfig.UseTenco)
                    ViewModel.RegisterMode = RegisterMode.Tenco;
                else
                    ViewModel.RegisterMode = RegisterMode.Normal;

                ViewModel.Ip = UserConfig.ServerIp;
                ViewModel.Port = UserConfig.ServerPort;
                try { ViewModel.SelectedCharacter = (Th123Characters)UserConfig.TencoCharacter; }
                catch (Exception) { }
                ViewModel.IsHideCharacter = UserConfig.HideTencoCharacter;
                ViewModel.Rank = UserConfig.Rank;
                ViewModel.Comment = UserConfig.Comment;
                ViewModel.TencoFolder = UserConfig.TencoFolder;
                ViewModel.TencoFolder2 = UserConfig.TencoFolder2;
                ViewModel.TencoAccountName = UserConfig.TencoAccount;
                ViewModel.TencoAccountName2 = UserConfig.TencoAccount2;

            }
            finally { _isLoading = false; }
        }

        /// <summary>
        /// 
        /// </summary>
        private void ReflectLanguage()
        {
            try { aboutIpLabel.Text = Language["HostSettingTab_AboutIp"]; }
            catch (KeyNotFoundException) { }
            try { normalHostRadioButton.Text = Language["HostSettingTab_RegisterType_Normal"]; }
            catch (KeyNotFoundException) { }
            try { tencoRadioButton.Text = Language["HostSettingTab_RegisterType_Tenco"]; }
            catch (KeyNotFoundException) { }
            try { tournamentRadioButton.Text = Language["HostSettingTab_RegisterType_Tournament"]; }
            catch (KeyNotFoundException) { }

            try { ipPortLabel.Text = Language["HostSettingTab_IpPort"]; }
            catch (KeyNotFoundException) { }
            try { openPortButton.Text = Language["HostSettingTab_OpenPort"]; }
            catch (KeyNotFoundException) { }
            try { rankLabel.Text = Language["HostSettingTab_Rank"]; }
            catch (KeyNotFoundException) { }
            try { isHideCharacterCheckBox.Text = Language["HostSettingTab_IsHideCharacter"]; }
            catch (KeyNotFoundException) { }

            ViewModel.Language = Language;
        }

        #region SetBindings
        /// <summary>
        /// 
        /// </summary>
        private void SetBindings()
        {
            ipInput.DataBindings.Add("Text", ViewModel, "Ip", false, DataSourceUpdateMode.OnPropertyChanged);
            tournamentTypeSelect.DataSource = ViewModel.TournamentTypeList;
            tournamentTypeSelect.DisplayMember = "Key";
            tournamentTypeSelect.ValueMember = "Value";
            tournamentTypeSelect.DataBindings.Add("SelectedValue", ViewModel, "SelectedTournamentType", false, DataSourceUpdateMode.OnPropertyChanged);
            portInput.DataBindings.Add("Value", ViewModel, "Port", false, DataSourceUpdateMode.OnPropertyChanged);
            userCountInput.DataBindings.Add("Value", ViewModel, "UserCount", false, DataSourceUpdateMode.OnPropertyChanged);
            openPortButton.DataBindings.Add("Visible", ViewModel, "IsPortClosed", false, DataSourceUpdateMode.OnPropertyChanged);
            openPortButton.DataBindings.Add("Enabled", ViewModel, "IsPortClosed", false, DataSourceUpdateMode.OnPropertyChanged);
            rankSelect.DataSource = ViewModel.Ranks;
            rankSelect.DataBindings.Add("Text", ViewModel, "Rank", false, DataSourceUpdateMode.OnPropertyChanged);
            characterSelect.DataSource = ViewModel.Characters;
            characterSelect.DisplayMember = "Key";
            characterSelect.ValueMember = "Value";
            characterSelect.DataBindings.Add("SelectedValue", ViewModel, "SelectedCharacter", false, DataSourceUpdateMode.OnPropertyChanged);
            isHideCharacterCheckBox.DataBindings.Add("Checked", ViewModel, "IsHideCharacter", false, DataSourceUpdateMode.OnPropertyChanged);
            ViewModel.LoadTemplates();
            templateSelect.DataSource = ViewModel.Templates;
            templateSelect.DisplayMember = "Key";
            templateSelect.ValueMember = "Value";
            commentInput.DataBindings.Add("Text", ViewModel, "Comment", false, DataSourceUpdateMode.OnPropertyChanged);

            tencoTabControl.DataBindings.Add("SelectedIndex", ViewModel, "SelectedTabIndex", false, DataSourceUpdateMode.OnPropertyChanged);

            tencoFolderText.DataBindings.Add("Text", ViewModel, "TencoFolderName");
            accountInput.DataBindings.Add("Text", ViewModel, "TencoAccountName");
            tencoFolderText2.DataBindings.Add("Text", ViewModel, "TencoFolderName2");
            accountInput2.DataBindings.Add("Text", ViewModel, "TencoAccountName2");
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public void ReloadRanks()
        {
            rankSelect.DataSource = ViewModel.Ranks;
        }

        #region ViewModel_PropertyChanged
        void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "RegisterMode":
                    switch (ViewModel.RegisterMode)
                    {
                        case RegisterMode.Normal:
                            normalHostRadioButton.Checked = true;
                            ipPortLabel.Text = "IP:ポート";
                            rankLabel.Text = "ランク";
                            try
                            {
                                ipPortLabel.Text = Language["HostSettingTab_IpPort"];
                                rankLabel.Text = Language["HostSettingTab_Rank"];
                            }
                            catch (KeyNotFoundException) { }
                            break;
                        case RegisterMode.Tenco:
                            tencoRadioButton.Checked = true;
                            ipPortLabel.Text = "IP:ポート";
                            rankLabel.Text = "キャラ";
                            try
                            {
                                ipPortLabel.Text = Language["HostSettingTab_IpPort"];
                                rankLabel.Text = Language["HostSettingTab_Character"];
                            }
                            catch (KeyNotFoundException) { }
                            if (!_isLoading) new Action(ViewModel.GetTencoRatings).BeginInvoke(null, null);
                            break;
                        case RegisterMode.Tournament:
                            tournamentRadioButton.Checked = true;
                            ipPortLabel.Text = "種類:人数";
                            rankLabel.Text = "ランク";
                            try
                            {
                                ipPortLabel.Text = Language["HostSettingTab_TournamentType_UserCount"];
                                rankLabel.Text = Language["HostSettingTab_Rank"];
                            }
                            catch (KeyNotFoundException) { }
                            break;
                        default:
                            break;
                    }
                    ipInput.Enabled = (ViewModel.RegisterMode != RegisterMode.Tournament);
                    ipInput.Visible = (ViewModel.RegisterMode != RegisterMode.Tournament);
                    portInput.Enabled = (ViewModel.RegisterMode != RegisterMode.Tournament);
                    portInput.Visible = (ViewModel.RegisterMode != RegisterMode.Tournament);
                    tournamentTypeSelect.Enabled = (ViewModel.RegisterMode == RegisterMode.Tournament);
                    tournamentTypeSelect.Visible = (ViewModel.RegisterMode == RegisterMode.Tournament);
                    userCountInput.Enabled = (ViewModel.RegisterMode == RegisterMode.Tournament);
                    userCountInput.Visible = (ViewModel.RegisterMode == RegisterMode.Tournament);
                    rankSelect.Enabled = (ViewModel.RegisterMode != RegisterMode.Tenco);
                    rankSelect.Visible = (ViewModel.RegisterMode != RegisterMode.Tenco);
                    characterSelect.Enabled = (ViewModel.RegisterMode == RegisterMode.Tenco);
                    characterSelect.Visible = (ViewModel.RegisterMode == RegisterMode.Tenco);
                    isHideCharacterCheckBox.Enabled = (ViewModel.RegisterMode == RegisterMode.Tenco);
                    isHideCharacterCheckBox.Visible = (ViewModel.RegisterMode == RegisterMode.Tenco);
                    break;

                case "IsValidIp":
                    if (ViewModel.IsValidIp)
                        ipInput.BackColor = Theme.ChatBackColor.ToColor();
                    else
                        ipInput.BackColor = Color.Pink;
                    break;

                case "IsValidUserCount":
                    if (ViewModel.IsValidUserCount)
                        userCountInput.BackColor = Theme.ChatBackColor.ToColor();
                    else
                        userCountInput.BackColor = Color.Pink;
                    break;

                case "Templates":
                    templateSelect.DataSource = ViewModel.Templates;
                    break;

                case "TencoFolder":
                    folderBrowserDialog.SelectedPath = ViewModel.TencoFolder;
                    break;

                case "TencoFolder2":
                    folderBrowserDialog2.SelectedPath = ViewModel.TencoFolder2;
                    break;

                case "Ratings":
                    Invoke(new Action(delegate
                    {
                        ratingList.Items.Clear();
                        foreach (var rating in ViewModel.Ratings)
                        {
                            ratingList.Items.Add(new ListViewItem(new string[]
                            {
                                EnumTextAttribute.GetText(rating.Character),
                                rating.Value.ToString(),
                                rating.Deviation.ToString(),
                                rating.MatchAccounts.ToString(),
                                rating.MatchCount.ToString(),
                            }));
                        }
                    }));
                    break;

                case "Ratings2":
                    Invoke(new Action(delegate
                    {
                        ratingList2.Items.Clear();
                        foreach (var rating in ViewModel.Ratings2)
                        {
                            ratingList2.Items.Add(new ListViewItem(new string[]
                            {
                                EnumTextAttribute.GetText(rating.Character),
                                rating.Value.ToString(),
                                rating.Deviation.ToString(),
                                rating.MatchAccounts.ToString(),
                                rating.MatchCount.ToString(),
                            }));
                        }
                    }));
                    break;

                default:
                    break;
            }
        }
        #endregion

        #region ReflectTheme
        /// <summary>
        /// テーマの反映
        /// </summary>
        public override void ReflectTheme()
        {
            BackColor = Theme.ToolBackColor.ToColor();
            foreach (TabPage page in tencoTabControl.TabPages)
                page.BackColor = Theme.ToolBackColor.ToColor();

            openPortButton.BackColor = SystemColors.Control;
            openPortButton.UseVisualStyleBackColor = true;
            tencoFolderButton.BackColor = SystemColors.Control;
            tencoFolderButton.UseVisualStyleBackColor = true;
            sendRecordsButton.BackColor = SystemColors.Control;
            sendRecordsButton.UseVisualStyleBackColor = true;
            updateRatingButton.BackColor = SystemColors.Control;
            updateRatingButton.UseVisualStyleBackColor = true;
            tencoFolderButton2.BackColor = SystemColors.Control;
            tencoFolderButton2.UseVisualStyleBackColor = true;
            sendRecordsButton2.BackColor = SystemColors.Control;
            sendRecordsButton2.UseVisualStyleBackColor = true;
            updateRatingButton2.BackColor = SystemColors.Control;
            updateRatingButton2.UseVisualStyleBackColor = true;

            normalHostRadioButton.ForeColor = Theme.GeneralTextColor.ToColor();
            tencoRadioButton.ForeColor = Theme.GeneralTextColor.ToColor();
            tournamentRadioButton.ForeColor = Theme.GeneralTextColor.ToColor();
            ipPortLabel.ForeColor = Theme.GeneralTextColor.ToColor();
            rankLabel.ForeColor = Theme.GeneralTextColor.ToColor();
            isHideCharacterCheckBox.ForeColor = Theme.GeneralTextColor.ToColor();

            ipInput.ForeColor = Theme.ChatForeColor.ToColor();
            ipInput.BackColor = Theme.ChatBackColor.ToColor();
            tournamentTypeSelect.ForeColor = Theme.ChatForeColor.ToColor();
            tournamentTypeSelect.BackColor = Theme.ChatBackColor.ToColor();
            portInput.ForeColor = Theme.ChatForeColor.ToColor();
            portInput.BackColor = Theme.ChatBackColor.ToColor();
            userCountInput.ForeColor = Theme.ChatForeColor.ToColor();
            userCountInput.BackColor = Theme.ChatBackColor.ToColor();
            rankSelect.ForeColor = Theme.ChatForeColor.ToColor();
            rankSelect.BackColor = Theme.ChatBackColor.ToColor();
            characterSelect.ForeColor = Theme.ChatForeColor.ToColor();
            characterSelect.BackColor = Theme.ChatBackColor.ToColor();
            templateSelect.ForeColor = Theme.ChatForeColor.ToColor();
            templateSelect.BackColor = Theme.ChatBackColor.ToColor();
            commentInput.ForeColor = Theme.ChatForeColor.ToColor();
            commentInput.BackColor = Theme.ChatBackColor.ToColor();

            accountInput.ForeColor = Theme.ChatForeColor.ToColor();
            accountInput.BackColor = Theme.ChatBackColor.ToColor();
            ratingList.BackColor = Theme.ToolBackColor.ToColor();
            ratingList.ForeColor = Theme.GeneralTextColor.ToColor();
            accountInput2.ForeColor = Theme.ChatForeColor.ToColor();
            accountInput2.BackColor = Theme.ChatBackColor.ToColor();
            ratingList2.BackColor = Theme.ToolBackColor.ToColor();
            ratingList2.ForeColor = Theme.GeneralTextColor.ToColor();
        }
        #endregion

        #region 登録モード
        /// <summary>
        /// 登録モード変更時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (normalHostRadioButton.Checked)
                ViewModel.RegisterMode = RegisterMode.Normal;
            else if (tencoRadioButton.Checked)
                ViewModel.RegisterMode = RegisterMode.Tenco;
            else//if(tournamentRadioButton.Checked)
                ViewModel.RegisterMode = RegisterMode.Tournament;
        }
        #endregion

        #region 開放ボタン
        /// <summary>
        /// 開放ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openPortButton_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            Application.DoEvents();
            try { ViewModel.OpenPort(); }
            finally { Cursor = Cursors.Default; }
        }
        #endregion

        #region テンプレ選択
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void templateSelect_DropDown(object sender, EventArgs e)
        {
            try
            {
                var selectedTemplate = templateSelect.SelectedItem;
                ViewModel.LoadTemplates();
                templateSelect.SelectedItem = selectedTemplate;
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
        }

        /// <summary>
        /// テンプレ選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void templateSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (0 < templateSelect.SelectedIndex)
                ViewModel.Comment = ((KeyValuePair<string, string>)templateSelect.SelectedItem).Value;
        }
        #endregion

        #region Tenco!関連
        /// <summary>
        /// Tenco!フォルダボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tencoFolderButton_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
                return;

            ViewModel.TencoFolder = folderBrowserDialog.SelectedPath;
        }
        private void tencoFolderButton2_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog2.ShowDialog() != DialogResult.OK)
                return;

            ViewModel.TencoFolder2 = folderBrowserDialog2.SelectedPath;
        }

        /// <summary>
        /// 戦績送信ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sendRecordsButton_Click(object sender, EventArgs e)
        {
            ViewModel.SendRecords();
        }
        private void sendRecordsButton2_Click(object sender, EventArgs e)
        {
            ViewModel.SendRecords2();
        }

        /// <summary>
        /// Tenco! アカウント名 クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tencoAccountLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ViewModel.OpenMypage();
        }
        private void tencoAccountLinkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ViewModel.OpenMypage2();
        }

        /// <summary>
        /// レート値確認ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void updateRatingButton_Click(object sender, EventArgs e)
        {
            new Action(ViewModel.GetTencoRatings).BeginInvoke(null, null);
        }
        private void updateRatingButton2_Click(object sender, EventArgs e)
        {
            new Action(ViewModel.GetTencoRatings).BeginInvoke(null, null);
        }

        /// <summary>
        /// タブ変更時のレート更新
        /// </summary>
        public void UpdateRatingOnTabChange()
        {
            if (!tencoRadioButton.Checked)
                return;

            new Action(ViewModel.GetTencoRatings).BeginInvoke(null, null);
        }
        #endregion
    }
}
