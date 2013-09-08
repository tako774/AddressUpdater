using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using HisoutenSupportTools.AddressUpdater.Lib;
using HisoutenSupportTools.AddressUpdater.Lib.AddressService;
using HisoutenSupportTools.AddressUpdater.Lib.Controller;
using HisoutenSupportTools.AddressUpdater.Lib.Model;
using HisoutenSupportTools.AddressUpdater.Lib.Model.Config;
using HisoutenSupportTools.AddressUpdater.Lib.Model.Tournament;
using HisoutenSupportTools.AddressUpdater.Lib.Network;
using HisoutenSupportTools.AddressUpdater.Lib.View;
using HisoutenSupportTools.AddressUpdater.Lib.View.AutoMatching;
using HisoutenSupportTools.AddressUpdater.Lib.View.Tournament;
using HisoutenSupportTools.AddressUpdater.Lib.ViewModel;
using HisoutenSupportTools.AddressUpdater.Lib.ViewModel.AutoMatching;
using HisoutenSupportTools.AddressUpdater.Lib.Watcher;

namespace HisoutenSupportTools.AddressUpdater.View
{
    /// <summary>
    /// クライアントタブ
    /// </summary>
    public partial class ClientTab : TabBase
    {
        /// <summary>一覧更新タイマー状態</summary>
        /// <remarks>一覧右クリック時に、更新を一時中断する時用</remarks>
        private bool _isUpdateTimerRuned = false;

        /// <summary>対戦状態監視</summary>
        private MultiWatcher _multiWatcher = new MultiWatcher();

        /// <summary>自動マッチングビュー</summary>
        private AutoMatchingView _autoMatchingView;

        /// <summary>表示したトーナメントウィンドウのコレクション</summary>
        private Collection<TournamentWindow> _tournamentWindows = new Collection<TournamentWindow>();

        /// <summary>表示済みかどうかの取得・設定</summary>
        public bool Showed { get; set; }

        /// <summary>
        /// 分割方向の取得・設定
        /// </summary>
        public Orientation DivisionOrientation
        {
            get { return splitContainer2.Orientation; }
            set
            {
                splitContainer2.Orientation = value;
                if (value == Orientation.Horizontal)
                {
                    tableLayoutPanel1.RowStyles[1].Height = 0;
                    _autoMatchingView = new AutoMatchingViewHorizontal() { ViewModel = autoMatchingViewModel, };
                }
                else
                {
                    tableLayoutPanel1.RowStyles[1].Height = 22;
                    _autoMatchingView = new AutoMatchingViewVertical() { ViewModel = autoMatchingViewModel, };
                }

                _autoMatchingView.Dock = DockStyle.Fill;
                autoMatchingViewPanel.Controls.Add(_autoMatchingView);
            }
        }

        /// <summary>
        /// 天則観の状態
        /// </summary>
        public TskStatus TskStatus
        {
            get { return _tskStatus; }
            set
            {
                if (_tskStatus == value)
                    return;

                if (value != null)
                {
                    _tskStatus = value;
                    _tskStatus.StatusChanged += new EventHandler<EventArgs<bool>>(_tskStatus_StatusChanged);
                }
            }
        }
        private TskStatus _tskStatus;

        private bool _enableAutoMatching = false;
        void _tskStatus_StatusChanged(object sender, EventArgs<bool> e)
        {
            if (_enableAutoMatching)
            {
                autoMatchingCheckBox.Enabled = e.Field;
                autoMatchingCheckBox.Visible = e.Field;

                if (!e.Field)
                {
                    _autoMatchingView.Unregister();
                    autoMatchingCheckBox.Checked = false;
                }
            }
            else
            {
                autoMatchingCheckBox.Enabled = false;
                autoMatchingCheckBox.Visible = false;
                autoMatchingCheckBox.Checked = false;
            }
        }

        /// <summary>
        /// HostSettingViewModelの取得・設定
        /// </summary>
        public HostSettingViewModel HostSetting { get; set; }
        /// <summary>
        /// VersionViewModelの取得・設定
        /// </summary>
        public VersionViewModel VersionViewModel { get; set; }
        /// <summary>
        /// AutoMatchingViewModelの取得
        /// </summary>
        public AutoMatchingViewModel AutoMatchingViewModel
        {
            get { return autoMatchingViewModel; }
        }

        /// <summary>
        /// クライアントの取得・設定
        /// </summary>
        public IClient Client
        {
            get { return _client; }
            set
            {
                _client = value;
                if (_client != null)
                {
                    Text = _client.ServerName;
                    hostListView.BindClient(_client);
                    autoMatchingViewModel.BindClient(_client);
                    chatViewModel.UserConfig = UserConfig;
                    chatViewModel.BindClient(_client);
                    _client.UserCountChanged += new EventHandler<EventArgs<int>>(_client_UserCountChanged);
                    _client.ConsecutiveErrorHappened += new EventHandler(_client_ConsecutiveErrorHappened);
                    hostController.Client = _client;
                }
            }
        }
        private IClient _client;

        /// <summary>
        /// 接続人数変化時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _client_UserCountChanged(object sender, EventArgs<int> e)
        {
            Invoke(new MethodInvoker(delegate
            {
                TabText = _client.ServerName + " " + e.Field.ToString() + "人";
            }));
        }

        /// <summary>
        /// 連続通信エラー発生時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _client_ConsecutiveErrorHappened(object sender, EventArgs e)
        {
            Invoke(new MethodInvoker(delegate
            {
                receiveTimer.Stop();
                hostController.Stop();
                receiveHostCheckBox.Checked = false;
                receiveChatCheckBox.Checked = false;
                registerHostCheckBox.Checked = false;
                receiveHostCheckBox.BackColor = Color.Silver;
                receiveChatCheckBox.BackColor = Color.Silver;
                registerHostCheckBox.BackColor = Color.Silver;
                receiveHostCheckBox.Text = "通信失敗";
                receiveChatCheckBox.Text = "通信失敗";
                registerHostCheckBox.Text = "通信失敗";
                try
                {
                    receiveHostCheckBox.Text = Language["ClientTab_CommunicationFailed"];
                    receiveChatCheckBox.Text = Language["ClientTab_CommunicationFailed"];
                    registerHostCheckBox.Text = Language["ClientTab_CommunicationFailed"];
                }
                catch (KeyNotFoundException) { }
                TabText = _client.ServerName;
            }));
        }

        /// <summary>
        /// インスタンスの生成
        /// </summary>
        public ClientTab()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClientTab_Load(object sender, EventArgs e)
        {
            rankFilterComboBox.Items.Clear();
            rankFilterComboBox.Items.Add("全て表示");
            foreach (var rank in UserConfig.Ranks)
                rankFilterComboBox.Items.Add(rank);
            rankFilterComboBox.SelectedIndex = 0;
            hostController.Interval = UserConfig.UpdateSpan * 1000;

            chatView.HighlightKeywords = UserConfig.HighlightKeywords;

            hostListView.ShowTournaments = UserConfig.ShowTournaments;

            autoMatchingViewModel.UserConfig = UserConfig;
            autoMatchingViewModel.ReflectUserConfig();

            if (_client == null)
                return;

            TabText = _client.ServerName;

            ReflectLanguage();

            receiveTimer.Start();
        }

        /// <summary>
        /// 表示言語の反映
        /// </summary>
        private void ReflectLanguage()
        {
            try
            {
                receiveHostCheckBox.Text = Language["ClientTab_ReceiveHost"];
                textChangeToggle1.NormalText = Language["ClientTab_ReceiveHost"];
            }
            catch (KeyNotFoundException) { }
            try { textChangeToggle1.CheckedText = Language["ClientTab_ReceiveHost_Active"]; }
            catch (KeyNotFoundException) { }
            try
            {
                receiveChatCheckBox.Text = Language["ClientTab_ReceiveChat"];
                textChangeToggle2.NormalText = Language["ClientTab_ReceiveChat"];
            }
            catch (KeyNotFoundException) { }
            try { textChangeToggle2.CheckedText = Language["ClientTab_ReceiveChat_Active"]; }
            catch (KeyNotFoundException) { }
            try
            {
                registerHostCheckBox.Text = Language["ClientTab_RegisterHost"];
                textChangeToggle3.NormalText = Language["ClientTab_RegisterHost"];
            }
            catch (KeyNotFoundException) { }
            try { textChangeToggle3.CheckedText = Language["ClientTab_RegisterHost_Active"]; }
            catch (KeyNotFoundException) { }
            try { rankFilterComboBox.Items[0] = Language["ClientTab_DefaultRankFilter"]; }
            catch (KeyNotFoundException) { }
            try { addressTxtUpdateCheckBox.Text = Language["ClientTab_AddressTxt"]; }
            catch (KeyNotFoundException) { }
            try { reverseDirectionCheckBox.Text = Language["ClientTab_ReverseChat"]; }
            catch (KeyNotFoundException) { }
            try { sendButton.Text = Language["ClientTab_Send"]; }
            catch (KeyNotFoundException) { }

            try { クリップボードにコピーToolStripMenuItem.Text = Language["ClientTab_CopyToClipBoard"]; }
            catch (KeyNotFoundException) { }
            try { 大会に参加ToolStripMenuItem.Text = Language["ClientTab_EntryTournament"]; }
            catch (KeyNotFoundException) { }
            try { このIPのホストを表示しないToolStripMenuItem.Text = Language["ClientTab_HideHost"]; }
            catch (KeyNotFoundException) { }

            try { コピーToolStripMenuItem.Text = Language["ClientTab_Copy"]; }
            catch (KeyNotFoundException) { }
            try { 古いチャットを削除ToolStripMenuItem.Text = Language["ClientTab_Reflesh"]; }
            catch (KeyNotFoundException) { }
            try { 遮断するToolStripMenuItem.Text = Language["ClientTab_Intercept"]; }
            catch (KeyNotFoundException) { }

            hostListView.ReflectLanguage(Language);
        }

        /// <summary>
        /// テーマの反映
        /// </summary>
        public override void ReflectTheme()
        {
            BackColor = Program.THEME.ToolBackColor.ToColor();
            sendButton.BackColor = SystemColors.Control;
            sendButton.UseVisualStyleBackColor = true;

            rankFilterComboBox.ForeColor = Program.THEME.ChatForeColor.ToColor();
            rankFilterComboBox.BackColor = Program.THEME.ChatBackColor.ToColor();
            addressTxtUpdateCheckBox.ForeColor = Program.THEME.GeneralTextColor.ToColor();
            reverseDirectionCheckBox.ForeColor = Program.THEME.GeneralTextColor.ToColor();

            Theme.HostFont.SetFont(hostListView);
            hostListView.ListForeColor = Program.THEME.GeneralTextColor.ToColor();
            hostListView.ListBackColor = Program.THEME.ToolBackColor.ToColor();
            hostListView.WaitingHostBackColor = Program.THEME.WaitingHostBackColor.ToColor();
            hostListView.FightingHostBackColor = Program.THEME.FightingHostBackColor.ToColor();
            hostListView.RefreshView();

            chatView.TextForeColor = Program.THEME.ChatForeColor.ToColor();
            chatView.TextBackColor = Program.THEME.ChatBackColor.ToColor();

            chatInput.ForeColor = Program.THEME.ChatForeColor.ToColor();
            chatInput.BackColor = Program.THEME.ChatBackColor.ToColor();

            Theme.ChatFont.SetFont(chatInput);
            Theme.ChatFont.SetFont(chatView);

            _autoMatchingView.ReflectTheme(Program.THEME);

            foreach (var tournamentWindow in _tournamentWindows)
                tournamentWindow.ReflectTheme();
        }

        /// <summary>
        /// 閉じる
        /// </summary>
        public void Close()
        {
            _client.UserCountChanged -= _client_UserCountChanged;
            _client.ConsecutiveErrorHappened -= _client_ConsecutiveErrorHappened;
            _client.Dispose();

            if (_tskStatus != null)
                _tskStatus.StatusChanged -= new EventHandler<EventArgs<bool>>(_tskStatus_StatusChanged);

            receiveTimer.Enabled = false;
            receiveTimer.Stop();
            hostController.Stop();
            hostListView.UnBindClient();
            _autoMatchingView.Close();
            autoMatchingViewModel.UnBindClient();
            chatViewModel.UnBindClient();
        }

        /// <summary>
        /// ウィンドウ設定の取得
        /// </summary>
        /// <returns>ウィンドウ設定</returns>
        public WindowConfig GetWindowConfig()
        {
            WindowConfig windowConfig;
            try
            {
                windowConfig = Program.USER_CONFIG.GetWindowConfig(_client.ServerName);
            }
            catch (KeyNotFoundException)
            {
                windowConfig = new WindowConfig(_client.ServerName);
            }

            switch (ParentForm.WindowState)
            {
                case FormWindowState.Normal:
                    windowConfig.Location = ParentForm.Location;
                    windowConfig.Size = ParentForm.Size;
                    windowConfig.Maximized = ParentForm.WindowState == FormWindowState.Maximized;
                    windowConfig.NumberColumnWidth = hostListView.NoColumnHeaderWidth;
                    windowConfig.TimeColumnWidth = hostListView.TimeColumnHeaderWidth;
                    windowConfig.IpPortColumnWidth = hostListView.IpPortColumnHeaderWidth;
                    windowConfig.RankColumnWidth = hostListView.RankColumnHeaderWidth;
                    windowConfig.CommentColumnWidth = hostListView.CommentColumnHeaderWidth;
                    windowConfig.ReverseScroll = reverseDirectionCheckBox.Checked;
                    windowConfig.SplitterDistance = splitContainer2.SplitterDistance;
                    break;
                case FormWindowState.Maximized:
                    windowConfig.Maximized = ParentForm.WindowState == FormWindowState.Maximized;
                    windowConfig.NumberColumnWidth = hostListView.NoColumnHeaderWidth;
                    windowConfig.TimeColumnWidth = hostListView.TimeColumnHeaderWidth;
                    windowConfig.IpPortColumnWidth = hostListView.IpPortColumnHeaderWidth;
                    windowConfig.RankColumnWidth = hostListView.RankColumnHeaderWidth;
                    windowConfig.CommentColumnWidth = hostListView.CommentColumnHeaderWidth;
                    windowConfig.ReverseScroll = reverseDirectionCheckBox.Checked;
                    windowConfig.SplitterDistance = splitContainer2.SplitterDistance;
                    break;
                case FormWindowState.Minimized:
                    break;
            }

            return windowConfig;
        }

        private bool _windowConfigReflected = false;
        /// <summary>
        /// ウィンドウ設定を反映
        /// </summary>
        public void ReflectWindowConfig()
        {
            if (_client == null || _windowConfigReflected)
                return;

            try
            {
                WindowConfig config;
                try { config = Program.USER_CONFIG.GetWindowConfig(_client.ServerName); }
                catch (KeyNotFoundException) { config = new WindowConfig(_client.ServerName); }

                reverseDirectionCheckBox.Checked = config.ReverseScroll;

                hostListView.NoColumnHeaderWidth = config.NumberColumnWidth;
                hostListView.TimeColumnHeaderWidth = config.TimeColumnWidth;
                hostListView.IpPortColumnHeaderWidth = config.IpPortColumnWidth;
                hostListView.RankColumnHeaderWidth = config.RankColumnWidth;
                hostListView.CommentColumnHeaderWidth = config.CommentColumnWidth;

                splitContainer2.SplitterDistance = config.SplitterDistance;
            }
            finally { _windowConfigReflected = true; }
        }

        private bool _serverSettingReflected = false;
        /// <summary>
        /// サーバー設定を反映
        /// </summary>
        public void ReflectServerConfig()
        {
            if (_client == null || _serverSettingReflected)
                return;

            if (!backgroundWorker.IsBusy)
                backgroundWorker.RunWorkerAsync();
        }

        /// <summary>
        /// サーバー設定反映処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try { e.Result = _client.GetServerSetting(); }
            catch (CommunicationFailedException) { e.Cancel = true; }
        }

        /// <summary>
        /// サーバー設定反映処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
                return;

            var setting = (serverSetting)e.Result;
            receiveChatCheckBox.Enabled = setting.enableChat;
            chatInput.MaxLength = setting.chatMaxLength;
            if (setting.hisoutenFightingScenes != null)
            {
                var values = new List<byte>();
                var sceneValueStrings = setting.hisoutenFightingScenes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var valueString in sceneValueStrings)
                {
                    try { values.Add(byte.Parse(valueString)); }
                    catch (Exception) { continue; }
                }
            }

            _enableAutoMatching = setting.enableAutoMatching;

            if (setting.gameInformations != null && 0 < setting.gameInformations.Length)
            {
                var gameInformations = new Collection<GameInformation>();
                foreach (var str in setting.gameInformations)
                {
                    try
                    {
                        var gameInformation = GameInformation.FromString(str);
                        if (gameInformation != null)
                        {
                            gameInformations.Add(gameInformation);
                            VersionViewModel.AddWindowInformation(gameInformation.Caption, gameInformation.ClassName);
                        }
                    }
                    catch (Exception ex) { System.Diagnostics.Debug.WriteLine(ex); }
                }

                _multiWatcher.GameInformations = gameInformations;
                _autoMatchingView.Watcher.GameInformations = gameInformations;

                if (0 < _multiWatcher.GameInformations.Count)
                    hostController.Watcher = _multiWatcher;
            }
            _serverSettingReflected = true;
            Enabled = true;
        }

        /// <summary>
        /// 一覧受信チェックボックス
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void receiveHostCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (receiveHostCheckBox.Checked)
                _client.Receive(receiveHostCheckBox.Checked, receiveChatCheckBox.Checked, UserConfig.ShowTournaments);
        }

        /// <summary>
        /// チャット受信チェックボックス
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void receiveChatCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (receiveChatCheckBox.Checked)
                _client.Receive(receiveHostCheckBox.Checked, receiveChatCheckBox.Checked, UserConfig.ShowTournaments);

            sendButton.Enabled = receiveChatCheckBox.Checked;
            chatInput.Enabled = receiveChatCheckBox.Checked;
        }

        /// <summary>
        /// ホスト登録チェックボックス
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void registerHostCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            switch (HostSetting.RegisterMode)
            {
                case RegisterMode.Normal:
                    if (registerHostCheckBox.Checked)
                    {
                        hostController.Start(HostSetting.GetHost());
                        hostController.UpdateStatus();
                    }
                    else
                    {
                        hostController.Stop();
                        _client.UnregisterTournament();
                    }
                    break;
                case RegisterMode.Tenco:
                    if (registerHostCheckBox.Checked)
                    {
                        hostController.Start(HostSetting.GetTencoHost());
                        hostController.UpdateStatus();
                    }
                    else
                    {
                        hostController.Stop();
                        _client.UnregisterTournament();
                    }
                    break;
                case RegisterMode.Tournament:
                    if (registerHostCheckBox.Checked && !_client.GetServerSetting().enableTournament)
                    {
                        MessageBox.Show(
                            ParentForm,
                            "このサーバーで大会の登録はできません。", Application.ProductName,
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        registerHostCheckBox.Checked = false;
                        return;
                    }
                    if (!HostSetting.IsValidUserCount)
                    {
                        registerHostCheckBox.Checked = false;
                        return;
                    }

                    if (registerHostCheckBox.Checked)
                    {
                        _client.RegisterTournament(
                            HostSetting.UserCount,
                            HostSetting.SelectedTournamentType,
                            HostSetting.Rank,
                            HostSetting.Comment);
                    }
                    else
                    {
                        _client.UnregisterTournament();
                        hostController.Stop();
                    }
                    break;
                default:
                    break;
            }

            new HisoutenSupportTools.AddressUpdater.Lib.Action(delegate
                {
                    System.Threading.Thread.Sleep(250);
                    _client.Receive(receiveHostCheckBox.Checked, false, receiveHostCheckBox.Checked && UserConfig.ShowTournaments);
                }).BeginInvoke(null, null);
        }

        /// <summary>
        /// ランクフィルタ変更時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rankFilterComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rankFilterComboBox.SelectedIndex == 0)
                hostListView.RankFilter = string.Empty;
            else
                hostListView.RankFilter = rankFilterComboBox.SelectedItem.ToString();

            UpdateAddressTxt();
        }

        /// <summary>
        /// address.txt更新チェックボックス
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addressTxtUpdateCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateAddressTxt();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hostListView_HostChanged(object sender, EventArgs e)
        {
            UpdateAddressTxt();
        }

        private void UpdateAddressTxt()
        {
            if (!addressTxtUpdateCheckBox.Checked)
                return;

            try { addressTxtUpdater.Update(hostListView.GetShowingHosts()); }
            catch (System.UnauthorizedAccessException) { }
            catch (System.IO.IOException) { }
        }

        /// <summary>
        /// チャット方向選択チェックボックス
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void reverseDirectionCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            chatViewModel.IsReverse = reverseDirectionCheckBox.Checked;
        }

        private void クリップボードにコピーToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (hostListView.SelectedItem == null)
                return;

            var numberString = hostListView.SelectedItem.SubItems[0].Text;
            if (!numberString.Contains("T"))
                CopyToClipboard(hostListView.SelectedHostIpPort);
        }

        private void hostListView_DoubleClick(object sender, EventArgs e)
        {
            if (hostListView.SelectedItem == null)
                return;

            var numberString = hostListView.SelectedItem.SubItems[0].Text;
            if (numberString.Contains("T"))
                EntryTournament();
            else
                CopyToClipboard(hostListView.SelectedHostIpPort);
        }

        private void CopyToClipboard(string text)
        {
            lock (this)
            {
                // 選択されている行が無い場合は何もしない（基本的にそれは無いはずだけど）
                if (hostListView.SelectedItem == null)
                    return;

                // クリップボードにコピー
                var selectedItem = hostListView.SelectedItem;
                var retryCount = 3;
                for (var i = 0; i < retryCount; i++)
                {
                    try
                    {
                        Clipboard.SetText(text);

                        selectedItem.Selected = false;
                        Application.DoEvents();
                        System.Threading.Thread.Sleep(100);
                        selectedItem.Selected = true;
                        Application.DoEvents();
                        break;
                    }
                    catch (NullReferenceException ex) { System.Diagnostics.Debug.WriteLine(ex); }
                    catch (ArgumentException ex) { System.Diagnostics.Debug.WriteLine(ex); }
                    catch (IndexOutOfRangeException ex) { System.Diagnostics.Debug.WriteLine(ex); }
                    catch (System.Runtime.InteropServices.ExternalException ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex);
                        if (i < retryCount - 1)
                            System.Threading.Thread.Sleep(100);
                    }
                }
            }
        }

        private void 大会に参加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EntryTournament();
        }

        void EntryTournament()
        {
            var no = hostListView.SelectedTournamentNo;
            // 大会じゃない場合-1なので何もしない
            if (no < 0)
                return;

            var dialog = new EntryTournamentDialog()
            {
                TournamentNo = no,
                UserConfig = UserConfig,
                Theme = Theme,
            };

            var result = dialog.ShowDialog(ParentForm);
            var window = new TournamentWindow()
            {
                UserConfig = UserConfig,
                Theme = Theme,
                TournamentNo = no,
                Watcher = hostController.Watcher,
            };
            window.TournamentInformation.EntryName = dialog.EntryName;
            switch (result)
            {
                case DialogResult.Yes:
                case DialogResult.OK:
                    UserConfig.TournamentEntryName = dialog.EntryName;
                    try
                    {
                        var id = _client.EntryTournament(no, dialog.EntryName, dialog.Ip, dialog.Port);
                        if (id != null)
                        {
                            window.Interval = UserConfig.UpdateSpan * 1000;
                            window.ServerSetting = _client.GetServerSetting();
                            window.Client = new Client(_client.ServerName, _client.ServerUri);
                            window.TournamentInformation.Roles = (int)TournamentRoles.Player;
                            window.Id = id;
                            window.Port = dialog.Port;
                            window.ReverseChat = reverseDirectionCheckBox.Checked;
                            window.WindowClosed += (sender, e) => { _tournamentWindows.Remove((TournamentWindow)sender); };
                            _tournamentWindows.Add(window);
                            window.Show();
                        }
                        else
                        {
                            MessageBox.Show(
                                ParentForm,
                                "参加できませんでした。", Application.ProductName,
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (CommunicationFailedException)
                    {
                        MessageBox.Show(
                                ParentForm,
                                "参加できませんでした。", Application.ProductName,
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    break;

                case DialogResult.No:
                    UserConfig.TournamentEntryName = dialog.EntryName;
                    try
                    {
                        var guestId = _client.GuestEntryTournament(no, dialog.EntryName);
                        if (guestId != null)
                        {
                            window.Interval = UserConfig.UpdateSpan * 1000;
                            window.Client = new Client(_client.ServerName, _client.ServerUri);
                            window.TournamentInformation.Roles = (int)TournamentRoles.Guest;
                            window.Id = guestId;
                            window.Port = dialog.Port;
                            window.ReverseChat = reverseDirectionCheckBox.Checked;
                            window.WindowClosed += (sender, e) => { _tournamentWindows.Remove((TournamentWindow)sender); };
                            _tournamentWindows.Add(window);
                            window.Show();
                        }
                        else
                        {
                            MessageBox.Show(
                                ParentForm,
                                "参加できませんでした。", Application.ProductName,
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (CommunicationFailedException)
                    {
                        MessageBox.Show(
                                ParentForm,
                                "参加できませんでした。", Application.ProductName,
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    break;

                default:
                    break;
            }
        }

        private void このIPのホストを表示しないToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lock (this)
            {
                // 選択されている行が無い場合は何もしない（基本的にそれは無いはずだけど）
                if (hostListView.SelectedItem == null)
                    return;

                var selectedItem = hostListView.SelectedItem;

                var ip_port = hostListView.SelectedHostIpPort.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                if (ip_port.Length < 1)
                    return;

                var ip = ip_port[0];
                hostListView.AddIpFilter(ip);
            }
        }

        /// <summary>
        /// チャット入力部キープレス
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chatInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                sendButton.Focus();
                e.Handled = true;
            }
        }

        /// <summary>
        /// 送信ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sendButton_Click(object sender, EventArgs e)
        {
            sendButton.Enabled = false;
            try
            {
                // 空の発言は無視
                if (chatInput.Text.Replace(" ", string.Empty).Replace("　", string.Empty) == string.Empty)
                    return;

                // 名前
                var name = string.Empty;
                if (_client.GetServerSetting().enableChatPrefix)
                    name = UserConfig.ChatPrefix;

                // 送信内容
                var sendText = chatInput.Text.Replace("\r", string.Empty).Replace("\n", string.Empty);

                _client.DoChat(name, sendText);

                // 非同期で発言するので少しだけ待つ
                System.Threading.Thread.Sleep(100);
                _client.Receive(receiveHostCheckBox.Checked, receiveChatCheckBox.Checked, UserConfig.ShowTournaments);
                chatInput.Text = string.Empty;
                Application.DoEvents();
            }
            catch (CommunicationFailedException)
            { }
            finally
            {
                sendButton.Enabled = true;
                Application.DoEvents();
                chatInput.Focus();
            }
        }

        private void コピーToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chatView.Copy();
        }

        private void 古いチャットを削除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chatViewModel.ClearChat();
            _client.Receive(receiveHostCheckBox.Checked, receiveChatCheckBox.Checked, UserConfig.ShowTournaments);
        }

        /// <summary>
        /// 受信タイマー作動時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void receiveTimer_Tick(object sender, EventArgs e)
        {
            if (receiveHostCheckBox.Checked || receiveChatCheckBox.Checked)
                _client.Receive(receiveHostCheckBox.Checked, receiveChatCheckBox.Checked, UserConfig.ShowTournaments);
        }

        /// <summary>
        /// コンテキストメニューOpen時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hostListContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            if (hostListView.SelectedItem == null)
            {
                // 選択されていない場合はコンテキストメニューを開かない
                // （念のため「クリップボードにコピー」を無効化）
                クリップボードにコピーToolStripMenuItem.Enabled = false;
                e.Cancel = true;
            }
            else
            {
                // 選択されている場合は「クリップボードにコピー」を有効化
                クリップボードにコピーToolStripMenuItem.Enabled = true;

                // 一覧更新タイマーの状態を保持
                _isUpdateTimerRuned = receiveTimer.Enabled;

                // 受信を止める
                receiveTimer.Enabled = false;
                receiveTimer.Stop();
            }
        }

        /// <summary>
        /// コンテキストメニューClose時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hostListContextMenuStrip_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            if (_isUpdateTimerRuned) // 開くときに更新タイマーが作動していた場合
            {
                // 受信再開
                receiveTimer.Enabled = true;
                receiveTimer.Start();
            }

            // 一覧更新タイマー状態変数を初期化しておく
            _isUpdateTimerRuned = false;
        }

        /// <summary>
        /// 遮断する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 遮断するToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var regex = new Regex(@"[0-9a-f]{8}");
            var match = regex.Match(chatView.SelectedText);
            if (!match.Success)
                return;

            var interceptForm = new AddInterceptDialog() { Theme = Theme };
            interceptForm.Id = match.Value;
            if (interceptForm.ShowDialog(this) == DialogResult.OK)
            {
                chatViewModel.AddIdFilter(interceptForm.Id);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void autoMatchingCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();

            _autoMatchingView.Visible = autoMatchingCheckBox.Checked;
            hostListView.Visible = !autoMatchingCheckBox.Checked;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void autoMatchingViewModel_OponentFound(object sender, EventArgs e)
        {
            Invoke(new MethodInvoker(delegate
            {
                if (!autoMatchingCheckBox.Checked)
                    errorProvider1.SetError(autoMatchingCheckBox, "対戦相手が見つかりました");
            }));
        }
    }
}
