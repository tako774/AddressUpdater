using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using HisoutenSupportTools.AddressUpdater.Lib;
using HisoutenSupportTools.AddressUpdater.Lib.Controller;
using HisoutenSupportTools.AddressUpdater.Lib.Model;
using HisoutenSupportTools.AddressUpdater.Lib.Model.Config;
using HisoutenSupportTools.AddressUpdater.Lib.View;
using HisoutenSupportTools.AddressUpdater.Lib.ViewModel;
using HisoutenSupportTools.AddressUpdater.View;

namespace HisoutenSupportTools.AddressUpdater
{
    /// <summary>
    /// メインフォーム
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>終了処理中かどうか</summary>
        private bool _closing = false;
        /// <summary>対戦記録ツールの状態</summary>
        private TskStatus _tskStatus = new TskStatus();

        /// <summary>
        /// インスタンスの生成
        /// </summary>
        public MainForm()
        {
            InitializeComponent();


            hostSettingTab.UserConfig = Program.USER_CONFIG;
            hostSettingTab.Language = Program.LANGUAGE;
            hostSettingTab.Theme = Program.THEME;

            userConfigTab.UserConfig = Program.USER_CONFIG;
            userConfigTab.Language = Program.LANGUAGE;
            userConfigTab.Theme = Program.THEME;

            versionTab.UserConfig = Program.USER_CONFIG;
            versionTab.Language = Program.LANGUAGE;
            versionTab.Theme = Program.THEME;
        }

        private bool _isLoading = false;
        /// <summary>
        /// Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            _isLoading = true;
            try
            {
                Text = Application.ProductName;

                ReflectWindowConfig();
                ReflectLanguage();
                SetServers();
                SelectUserConfigTab();
                mainTabControl.SelectedIndex = mainTabControl.TabCount - 1;
                mainTabControl.SelectedIndex = 0;

                for (var i = 0; i < mainTabControl.TabCount; i++)
                {
                    if (GetTab(i) is UserConfigTab)
                    {
                        ((UserConfigTab)GetTab(i)).Boot();
                        break;
                    }
                }
            }
            finally { _isLoading = false; }
        }

        /// <summary>
        /// Closing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _closing = true;

            // 自動マッチングの情報を退避しておく
            var autoMatchingInformation = new AutoMatchingInformation()
            {
                AccountName = Program.USER_CONFIG.AutoMatchingInformation.AccountName,
                Character = Program.USER_CONFIG.AutoMatchingInformation.Character,
                MatchingSpan = Program.USER_CONFIG.AutoMatchingInformation.MatchingSpan,
                IsHostable = Program.USER_CONFIG.AutoMatchingInformation.IsHostable,
                IsRoomOnry = Program.USER_CONFIG.AutoMatchingInformation.IsRoomOnry,
                Ip = Program.USER_CONFIG.AutoMatchingInformation.Ip,
                Port = Program.USER_CONFIG.AutoMatchingInformation.Port,
                Comment = Program.USER_CONFIG.AutoMatchingInformation.Comment,
            };

            // 全クライアントタブ通信停止
            for (var i = 0; i < mainTabControl.TabCount; i++)
            {
                if (GetTab(i) is ClientTab)
                    ((ClientTab)GetTab(i)).Close();
                else
                    break;
            }

            // 表示設定保存
            // 一度も表示してないタブを表示しておく
            for (var i = 0; i < mainTabControl.TabCount; i++)
            {
                if (GetTab(i) is ClientTab)
                {
                    if (!((ClientTab)GetTab(i)).Showed)
                        mainTabControl.SelectedIndex = i;
                }
                else
                    break;
            }


            var entryName = Program.USER_CONFIG.TournamentEntryName;

            try { Program.USER_CONFIG.Load(); }
            catch (FileNotFoundException) { }

            var windowConfigs = GetWindowConfigs();
            foreach (var windowConfig in windowConfigs)
            {
                Program.USER_CONFIG.SetWindowConfig(windowConfig);
            }

            Program.USER_CONFIG.ExtraWindowInformations = versionTab.ExtraWindowInformations;

            Program.USER_CONFIG.UseTenco = hostSettingTab.ViewModel.RegisterMode == RegisterMode.Tenco;
            Program.USER_CONFIG.TencoCharacter = (int)hostSettingTab.ViewModel.SelectedCharacter;
            Program.USER_CONFIG.HideTencoCharacter = hostSettingTab.ViewModel.IsHideCharacter;
            Program.USER_CONFIG.Rank = hostSettingTab.ViewModel.Rank;
            Program.USER_CONFIG.Comment = hostSettingTab.ViewModel.Comment;
            Program.USER_CONFIG.TencoFolder = hostSettingTab.ViewModel.TencoFolder;
            Program.USER_CONFIG.TencoFolder2 = hostSettingTab.ViewModel.TencoFolder2;
            Program.USER_CONFIG.TencoAccount = hostSettingTab.ViewModel.TencoAccountName;
            Program.USER_CONFIG.TencoAccount2 = hostSettingTab.ViewModel.TencoAccountName2;

            Program.USER_CONFIG.AutoMatchingInformation = autoMatchingInformation;

            Program.USER_CONFIG.TournamentEntryName = entryName;

            try { Program.USER_CONFIG.Save(); }
            catch (UnauthorizedAccessException ex) { ShowSaveConfigFailedMessage(ex); }
            catch (InvalidOperationException ex) { ShowSaveConfigFailedMessage(ex); }
            catch (IOException ex) { ShowSaveConfigFailedMessage(ex); }


            // このツールで開けてたらUPnPポート閉じる
            hostSettingTab.Close();
        }

        /// <summary>
        /// 表示設定保存失敗メッセージの表示
        /// </summary>
        /// <param name="ex">例外</param>
        private void ShowSaveConfigFailedMessage(Exception ex)
        {
            var message = "設定の保存に失敗しました。";
            try { message = Program.LANGUAGE["MainForm_SaveConfigFailed"]; }
            catch (KeyNotFoundException) { }

            MessageBox.Show(
                this,
                message + Environment.NewLine + Environment.NewLine + ex.Message,
                Application.ProductName,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        /// <summary>
        /// 各サーバーの表示設定を取得
        /// </summary>
        /// <returns>表示設定リスト</returns>
        private List<WindowConfig> GetWindowConfigs()
        {
            var windowConfigs = new List<WindowConfig>();
            for (int i = 0; i < mainTabControl.TabCount; i++)
            {
                if (GetTab(i) is ClientTab)
                    windowConfigs.Add(((ClientTab)GetTab(i)).GetWindowConfig());
                else
                    break;
            }

            return windowConfigs;
        }

        /// <summary>
        /// ウィンドウ設定の反映
        /// </summary>
        private void ReflectWindowConfig()
        {
            ServerInformation firstVisibleServer = null;
            foreach (var serverInformation in Program.USER_CONFIG.ServerInformations)
            {
                if (serverInformation.Visible)
                {
                    firstVisibleServer = serverInformation;
                    break;
                }
            }

            WindowConfig windowConfig;
            if (firstVisibleServer == null)
                windowConfig = new WindowConfig(Application.ProductName);
            else
            {
                try { windowConfig = Program.USER_CONFIG.GetWindowConfig(firstVisibleServer.Name); }
                catch (KeyNotFoundException) { windowConfig = new WindowConfig(Application.ProductName); }
            }

            if (windowConfig.Maximized)
                WindowState = FormWindowState.Maximized;

            Location = windowConfig.Location;
            Size = windowConfig.Size;
        }

        /// <summary>
        /// 表示言語の反映
        /// </summary>
        private void ReflectLanguage()
        {
            try { mainTabControl.TabPages["hostSettingTabPage"].Text = Program.LANGUAGE["MainForm_HostSettingTab"]; }
            catch (KeyNotFoundException) { }

            try { mainTabControl.TabPages["userConfigTabPage"].Text = Program.LANGUAGE["MainForm_UserConfigTab"]; }
            catch (KeyNotFoundException) { }

            try { mainTabControl.TabPages["versionTabPage"].Text = Program.LANGUAGE["MainForm_VersionTab"]; }
            catch (KeyNotFoundException) { }
        }

        /// <summary>
        /// サーバーのセット
        /// </summary>
        public void SetServers()
        {
            // 全クライアントタブ通信停止&除去
            while (GetTab(0) is ClientTab)
            {
                ((ClientTab)GetTab(0)).Close();
                mainTabControl.TabPages.RemoveAt(0);
            }

            // 設定からクライアントのタブ生成
            var visibleServers = new List<ServerInformation>();
            foreach (var serverInformation in Program.USER_CONFIG.ServerInformations)
            {
                if (!serverInformation.Visible)
                    continue;

                visibleServers.Add(serverInformation);
            }
            if (visibleServers.Count == 0)
            {
                visibleServers.Add(new ServerInformation(Application.ProductName, AddressUpdaterUri.ADMIN_SERVICE, true));
            }

            for (var i = visibleServers.Count - 1; 0 <= i; i--)
            {
                var client = new Client(visibleServers[i].Name, visibleServers[i].Uri);

                var clientTab = new ClientTab();
                clientTab.DivisionOrientation = Program.USER_CONFIG.ClientDivisionOrientation;
                clientTab.HostSetting = hostSettingViewModel;
                clientTab.VersionViewModel = versionViewModel;
                clientTab.UserConfig = Program.USER_CONFIG;
                clientTab.Language = Program.LANGUAGE;
                clientTab.TskStatus = _tskStatus;
                clientTab.Client = client;
                clientTab.Theme = Program.THEME;
                InsertTab(0, clientTab);
            }
        }

        ///// <summary>
        ///// タブの追加
        ///// </summary>
        ///// <param name="tab"></param>
        //private void AddTab(TabBase tab)
        //{
        //    TabPage tabPage = new TabPage();
        //    tabPage.UseVisualStyleBackColor = true;
        //    tab.Dock = DockStyle.Fill;
        //    tabPage.Controls.Add(tab);

        //    mainTabControl.TabPages.Add(tabPage);
        //}

        /// <summary>
        /// タブの挿入
        /// </summary>
        /// <param name="index"></param>
        /// <param name="tab"></param>
        private void InsertTab(int index, TabBase tab)
        {
            var tabPage = new TabPage();
            tabPage.UseVisualStyleBackColor = true;
            tab.Dock = DockStyle.Fill;
            tabPage.Controls.Add(tab);

            mainTabControl.TabPages.Insert(index, tabPage);
        }

        /// <summary>
        /// タブの除去
        /// </summary>
        /// <param name="index"></param>
        private void RemoveTab(int index)
        {
            var tab = GetTab(index);
            if (tab is ClientTab)
                ((ClientTab)tab).Close();

            mainTabControl.TabPages.RemoveAt(index);
        }

        /// <summary>
        /// タブの取得
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private TabBase GetTab(int index)
        {
            return (TabBase)mainTabControl.TabPages[index].Controls[0];
        }

        /// <summary>
        /// タブ変更時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mainTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedTab = GetTab(mainTabControl.SelectedIndex);
            if (selectedTab is ClientTab)
            {
                ((ClientTab)selectedTab).ReflectWindowConfig();
                ((ClientTab)selectedTab).Showed = true;
                Application.DoEvents();

                if (!_closing)
                    ((ClientTab)selectedTab).ReflectServerConfig();
            }
            else if (selectedTab is HostSettingTab)
            {
                if (!_isLoading)
                    ((HostSettingTab)selectedTab).UpdateRatingOnTabChange();
            }
        }

        /// <summary>
        /// ユーザー設定タブの選択
        /// </summary>
        public void SelectUserConfigTab()
        {
            for (var i = 0; i < mainTabControl.TabCount; i++)
            {
                if (GetTab(i) is UserConfigTab)
                {
                    mainTabControl.SelectedIndex = i;
                    break;
                }
            }
        }

        /// <summary>
        /// 色設定の反映
        /// </summary>
        public void ReflectTheme()
        {
            foreach (TabPage tabPage in mainTabControl.TabPages)
            {
                ((TabBase)tabPage.Controls[0]).ReflectTheme();
            }
        }

        /// <summary>
        /// 保存された設定の反映
        /// </summary>
        internal void ReflectSavedConfig()
        {
            hostSettingViewModel.Ip = Program.USER_CONFIG.ServerIp;
            hostSettingViewModel.Port = Program.USER_CONFIG.ServerPort;
            hostSettingTab.ReloadRanks();
            SetServers();
            SelectUserConfigTab();
        }

        /// <summary>
        /// 対戦記録ツール確認タイマー処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tskTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                Process tsk;
                var processes = Process.GetProcessesByName("Solfisk");
                if (processes.Length == 0)
                {
                    _tskStatus.Status = false;
                    return;
                }
                tsk = processes[0];

                if (!tsk.Responding)
                {
                    _tskStatus.Status = false;
                    tsk = null;
                    return;
                }

                _tskStatus.Status = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                _tskStatus.Status = false;
            }
        }
    }
}
