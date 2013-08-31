using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using HisoutenSupportTools.AddressUpdater.Lib.AddressService;
using HisoutenSupportTools.AddressUpdater.Lib.Api;
using HisoutenSupportTools.AddressUpdater.Lib.Controller;
using HisoutenSupportTools.AddressUpdater.Lib.Controller.Tournament;
using HisoutenSupportTools.AddressUpdater.Lib.IO;
using HisoutenSupportTools.AddressUpdater.Lib.Model.Config;
using HisoutenSupportTools.AddressUpdater.Lib.Model.Tournament;
using HisoutenSupportTools.AddressUpdater.Lib.Network;
using HisoutenSupportTools.AddressUpdater.Lib.ViewModel;
using HisoutenSupportTools.AddressUpdater.Lib.Watcher;

namespace HisoutenSupportTools.AddressUpdater.Lib.View.Tournament
{
    /// <summary>
    /// 大会ウィンドウ
    /// </summary>
    public partial class TournamentWindow : Form
    {
        /// <summary>アナウンス強調表示用</summary>
        private const string ANNOUNCE_LINE = "__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/";

        #region フィールド
        private DateTime _lastDataTime = DateTime.MinValue;
        private DateTime _lastChatTime = DateTime.MinValue;
        private TournamentReceiveManager _receiveManager;
        private Collection<string> _announces = new Collection<string>();
        private Collection<chat> _chats = new Collection<chat>();
        private ChatFilter _chatFilter = new ChatFilter();
        private AnnounceWindow _announceWindow = new AnnounceWindow();
        private MatchStatusViewer _viewer;
        private IWatcher _watcher = new MultiWatcher();
        #endregion

        #region プロパティ
        /// <summary>
        /// サーバー設定
        /// </summary>
        public serverSetting ServerSetting { get; set; }

        /// <summary>
        /// ユーザー設定
        /// </summary>
        public UserConfig UserConfig { get; set; }

        /// <summary>
        /// テーマ
        /// </summary>
        public Theme Theme { get; set; }

        /// <summary>対戦状態監視オブジェクト</summary>
        public IWatcher Watcher
        {
            get { return _watcher; }
            set { _watcher = value; }
        }

        /// <summary>
        /// 更新間隔
        /// </summary>
        public int Interval
        {
            get { return receiveTimer.Interval; }
            set
            {
                if (receiveTimer.Interval == value)
                    return;
                receiveTimer.Interval = value;
            }
        }

        /// <summary>
        /// 通信クライアントの取得・設定
        /// </summary>
        public IClient Client
        {
            get { return _client; }
            set { _client = value; }
        }
        private IClient _client;

        /// <summary>大会番号の取得・設定</summary>
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

        /// <summary>
        /// エントリーIDの取得・設定
        /// </summary>
        public id Id
        {
            get { return _id; }
            set
            {
                if (_id != null && _id.Equals(value))
                    return;
                _id = value;
                _receiveManager.Id = value;
                SetWindowText();
            }
        }
        private id _id;

        /// <summary>
        /// ポートの取得・設定
        /// </summary>
        public int Port
        {
            get { return _port; }
            set
            {
                if (_port == value)
                    return;

                _port = value;
            }
        }
        private int _port = -1;

        /// <summary>
        /// 大会内情報
        /// </summary>
        public TournamentInformation TournamentInformation
        {
            get { return _tournamentInformation;}
            set
            {
                if (_tournamentInformation.Equals(value))
                    return;

                _tournamentInformation = value;
                SetWindowText();
                SetDefaultShuffle();
                SetView();
                SetStartButtonEnabled();
                TimerControl();
            }
        }
        private TournamentInformation _tournamentInformation = new TournamentInformation();

        /// <summary>
        /// チャットのスクロール方向を逆にするかどうか
        /// </summary>
        public bool ReverseChat
        {
            get { return reverseDirectionCheckBox.Checked; }
            set
            {
                if (reverseDirectionCheckBox.Checked == value)
                    return;
                reverseDirectionCheckBox.Checked = value;
            }
        }
        #endregion

        /// <summary>フォームが閉じた時に発生します</summary>
        public event EventHandler WindowClosed;

        /// <summary>
        /// インスタンスの生成
        /// </summary>
        public TournamentWindow()
        {
            InitializeComponent();

            _receiveManager = new TournamentReceiveManager();

            _receiveManager.TournamentDeleted += new EventHandler(_receiveManager_TournamentDeleted);
            _receiveManager.Kicked += new EventHandler(_receiveManager_Kicked);

            _receiveManager.TournamentInformationChanged += new EventHandler<EventArgs<TournamentInformation>>(_receiveManager_TournamentInformationChanged);
            _receiveManager.UsersChanged += new EventHandler<UsersChangedEventArgs>(_receiveManager_UsersChanged);
            _receiveManager.AnnounceChanged += new EventHandler<EventArgs<Collection<string>>>(_receiveManager_AnnounceChanged);

            _receiveManager.MatchStatusChanged += new EventHandler<EventArgs<Collection<player>>>(_receiveManager_MatchStatusChanged);
            _receiveManager.ChatReceived += new EventHandler<EventArgs<Collection<chat>>>(_receiveManager_ChatReceived);
        }


        /// <summary>
        /// Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TournamentWindow_Load(object sender, EventArgs e)
        {
            // ---------------------------------------------------------------------------------------
            try
            {
                var themeDir = new DirectoryInfo("AUTheme");
                var imageFiles = themeDir.GetFiles("tournament.*");
                if (0 < imageFiles.Length)
                {
                    var memImage = FileLoader.LoadFile(imageFiles[0]);
                    playersPanel.BackgroundImage = Bitmap.FromStream(memImage);
                    playersPanel.BackgroundImageLayout = ImageLayout.Tile;
                }
            }
            catch (Exception) { }
            // ---------------------------------------------------------------------------------------

            if (ServerSetting != null)
            {
                chatInput.MaxLength = ServerSetting.chatMaxLength;
            }

            if (_client != null)
            {
                _client.TournamentDataReceived += new EventHandler<EventArgs<tournament>>(_client_TournamentDataReceived);
                _client.ConsecutiveErrorHappened += new EventHandler(_client_ConsecutiveErrorHappened);
            }

            _announceWindow.Theme = Theme;
            ReflectTheme();

            if (UserConfig != null)
            {
                foreach (var keyword in UserConfig.FilterKeywords)
                    _chatFilter.AddKeywordFilter(keyword);
            }
        }

        /// <summary>
        /// テーマの反映
        /// </summary>
        public void ReflectTheme()
        {
            if (Theme == null)
                return;

            BackColor = Theme.ToolBackColor.ToColor();
            announceButton.BackColor = SystemColors.Control;
            announceButton.UseVisualStyleBackColor = true;
            entryButton.BackColor = SystemColors.Control;
            entryButton.UseVisualStyleBackColor = true;
            addDummyPlayerButton.BackColor = SystemColors.Control;
            addDummyPlayerButton.UseVisualStyleBackColor = true;
            cancelEntryButton.BackColor = SystemColors.Control;
            cancelEntryButton.UseVisualStyleBackColor = true;
            retireButton.BackColor = SystemColors.Control;
            retireButton.UseVisualStyleBackColor = true;

            startButton.BackColor = SystemColors.Control;
            startButton.UseVisualStyleBackColor = true;

            sendButton.BackColor = SystemColors.Control;
            sendButton.UseVisualStyleBackColor = true;

            reverseDirectionCheckBox.ForeColor = Theme.GeneralTextColor.ToColor();

            shuffleCheckBox.ForeColor = Theme.GeneralTextColor.ToColor();
            usersListView.BackColor = Theme.ToolBackColor.ToColor();
            usersListView.ForeColor = Theme.GeneralTextColor.ToColor();

            chatView.ForeColor = Theme.ChatForeColor.ToColor();
            chatView.BackColor = Theme.ChatBackColor.ToColor();
            chatInput.ForeColor = Theme.ChatForeColor.ToColor();
            chatInput.BackColor = Theme.ChatBackColor.ToColor();

            Theme.ChatFont.SetFont(chatView);
            Theme.ChatFont.SetFont(chatInput);

            foreach (Control control in playersPanel.Controls)
                if (control is Label)
                    control.ForeColor = Theme.GeneralTextColor.ToColor();

            _announceWindow.ReflectTheme();

            if (_viewer != null)
                _viewer.ReflectTheme();
        }


        /// <summary>
        /// 継続エラー発生時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _client_ConsecutiveErrorHappened(object sender, EventArgs e)
        {
            Invoke(new MethodInvoker(delegate
                {
                    updateDataCheckBox.Checked = false;
                    updateDataCheckBox.Text = "通信失敗";
                }));
        }

        /// <summary>
        /// Shown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TournamentWindow_Shown(object sender, EventArgs e)
        {
            // 初回に2連続で受信しないようにフラグ使用(Receive内で確認)
            _isFirstChecking = true;
            try
            {
                updateDataCheckBox.Checked = true;
                updateChatCheckBox.Checked = true;
            }
            finally { _isFirstChecking = false; }

            Receive();
        }
        bool _isFirstChecking = false;

        /// <summary>
        /// Closing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TournamentWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            _client.TournamentDataReceived -= _client_TournamentDataReceived;
            _client.Dispose();

            _receiveManager.TournamentDeleted -= _receiveManager_TournamentDeleted;
            _receiveManager.Kicked -= _receiveManager_Kicked;
            _receiveManager.TournamentInformationChanged -= _receiveManager_TournamentInformationChanged;
            _receiveManager.UsersChanged -= _receiveManager_UsersChanged;
            _receiveManager.AnnounceChanged -= _receiveManager_AnnounceChanged;
            _receiveManager.MatchStatusChanged -= _receiveManager_MatchStatusChanged;
            _receiveManager.ChatReceived -= _receiveManager_ChatReceived;

            _watcher.Stop();

            watchTimer.Enabled = false;
            watchTimer.Stop();
            receiveTimer.Enabled = false;
            receiveTimer.Stop();
            listenTimer.Enabled = false;
            listenTimer.Stop();
        }

        /// <summary>
        /// Closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TournamentWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (WindowClosed != null)
                WindowClosed(this, EventArgs.Empty);
        }

        #region 受信データ解析
        /// <summary>
        /// 大会情報受信時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _client_TournamentDataReceived(object sender, EventArgs<tournament> e)
        {
            if (e.Field == null)
                return;

            try
            {
                Invoke(new MethodInvoker(delegate
                {
                    if (updateDataCheckBox.Checked)
                        _receiveManager.AnalizeData(e.Field);
                    if (updateChatCheckBox.Checked)
                        _receiveManager.AnalizeChat(e.Field);
                }));
            }
            catch (Exception ex) { System.Diagnostics.Debug.WriteLine(ex.Message); }
        }

        /// <summary>
        /// 大会削除時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _receiveManager_TournamentDeleted(object sender, EventArgs e)
        {
            try
            {
                updateDataCheckBox.Checked = false;
                updateChatCheckBox.Checked = false;
                chatView.Text = "この大会は削除されました。";
                foreach (Control control in Controls)
                    control.Enabled = false;
            }
            catch (Exception) { }
        }

        /// <summary>
        /// キック時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _receiveManager_Kicked(object sender, EventArgs e)
        {
            try
            {
                updateDataCheckBox.Checked = false;
                updateChatCheckBox.Checked = false;
                chatView.Text = "キックされました。";
                foreach (Control control in Controls)
                    control.Enabled = false;
            }
            catch (Exception) { }
        }

        /// <summary>
        /// 大会情報変化時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _receiveManager_TournamentInformationChanged(object sender, EventArgs<TournamentInformation> e)
        {
            TournamentInformation = e.Field;
        }

        /// <summary>
        /// ユーザー情報変化時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _receiveManager_UsersChanged(object sender, UsersChangedEventArgs e)
        {
            var items = new List<ListViewItem>();

            foreach (var manager in e.Managers)
            {
                items.Add(new ListViewItem(new string[]
                {
                    manager.Id.ToString(),
                    EnumTextAttribute.GetText(TournamentRoles.Manager),
                    manager.entryName,
                }));
            }

            foreach (var player in e.Players)
            {
                if (player.retired)
                    continue;

                items.Add(new ListViewItem(new string[]
                {
                    player.Id.ToString(),
                    EnumTextAttribute.GetText(TournamentRoles.Player),
                    player.entryName,
                }));
            }

            foreach (var spectator in e.Spectators)
            {
                items.Add(new ListViewItem(new string[]
                {
                    spectator.Id.ToString(),
                    EnumTextAttribute.GetText(TournamentRoles.Spectator),
                    spectator.entryName,
                }));
            }

            foreach (var guest in e.Guests)
            {
                items.Add(new ListViewItem(new string[]
                {
                    guest.Id.ToString(),
                    EnumTextAttribute.GetText(TournamentRoles.Guest),
                    guest.entryName,
                }));
            }

            foreach (var kickedId in e.KickedIds)
            {
                items.Add(new ListViewItem(new string[]
                {
                    kickedId.ToString(),
                    "追放",
                    string.Empty,
                }));
            }

            var addItemArray = items.ToArray();
            usersListView.Items.Clear();
            usersListView.Items.AddRange(addItemArray);

            // 開始前の場合は参加者リストを更新
            if (!TournamentInformation.IsStarted)
                UpdatePlayersList(e.Players);
        }

        /// <summary>
        /// プレイヤー情報更新
        /// </summary>
        /// <param name="players"></param>
        void UpdatePlayersList(IList<player> players)
        {
            playersPanel.SuspendLayout();
            playersPanel.ColumnCount = 2 + 1; // +1 dummy column
            playersPanel.RowCount = players.Count + 1; // +1 dummy row

            playersPanel.ColumnStyles.Clear();
            for (var i = 0; i < playersPanel.ColumnCount; i++)
            {
                playersPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40));
                playersPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                playersPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            }

            playersPanel.RowStyles.Clear();
            for (var i = 0; i < playersPanel.RowCount; i++)
                playersPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));


            var numberLabels = new Collection<Label>();
            var nameLabels = new Collection<Label>();

            for (var i = 0; i < players.Count; i++)
            {
                numberLabels.Add(new Label()
                {
                    Text = (i + 1).ToString(),
                    ForeColor = Theme.GeneralTextColor.ToColor(),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill,
                    Margin = Padding.Empty,
                });
                nameLabels.Add(new Label()
                {
                    Text = players[i].entryName,
                    ForeColor = Theme.GeneralTextColor.ToColor(),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill,
                    Margin = Padding.Empty,
                });
            }

            playersPanel.Controls.Clear();
            for (var row = 0; row < numberLabels.Count; row++)
            {
                playersPanel.Controls.Add(numberLabels[row], 0, row);
                playersPanel.Controls.Add(nameLabels[row], 1, row);
            }
            playersPanel.ResumeLayout();
        }

        /// <summary>
        /// アナウンス変化時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _receiveManager_AnnounceChanged(object sender, EventArgs<Collection<string>> e)
        {
            _announceWindow.Announces = e.Field;
            _announces = e.Field;
            UpdateChatView();
        }

        /// <summary>
        /// 対戦状況変化時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _receiveManager_MatchStatusChanged(object sender, EventArgs<Collection<player>> e)
        {
            switch (TournamentInformation.Type)
            {
                case TournamentTypes.トーナメント:
                    if (_viewer == null)
                    {
                        _viewer = new TournamentViewer()
                        {
                            Id = Id,
                            TPanel = playersPanel,
                            TournamentType = TournamentInformation.Type,
                            Roles = TournamentInformation.Roles,
                            Theme = Theme,
                        };
                        _viewer.ResultEdited += new EventHandler<EventArgs<player>>(_viewer_ResultEdited);
                    }
                    _viewer.SetView(e.Field);
                    break;
                case TournamentTypes.総当たり:
                    if (_viewer == null)
                    {
                        _viewer = new RoundRobinViewer()
                        {
                            Id = Id,
                            TPanel = playersPanel,
                            TournamentType = TournamentInformation.Type,
                            Roles = TournamentInformation.Roles,
                            Theme = Theme,
                        };
                        _viewer.ResultEdited += new EventHandler<EventArgs<player>>(_viewer_ResultEdited);
                    }
                    _viewer.SetView(e.Field);
                    break;
                case TournamentTypes.総当たり2人:
                    if (_viewer == null)
                    {
                        _viewer = new TeamRoundRobinViewer()
                        {
                            Id = Id,
                            TPanel = playersPanel,
                            TournamentType = TournamentInformation.Type,
                            Roles = TournamentInformation.Roles,
                            Theme = Theme,
                            NumberOfPlayersOfTeam = 2,
                        };
                        _viewer.ResultEdited += new EventHandler<EventArgs<player>>(_viewer_ResultEdited);
                    }
                    _viewer.SetView(e.Field);
                    break;
                case TournamentTypes.総当たり3人:
                    if (_viewer == null)
                    {
                        _viewer = new TeamRoundRobinViewer()
                        {
                            Id = Id,
                            TPanel = playersPanel,
                            TournamentType = TournamentInformation.Type,
                            Roles = TournamentInformation.Roles,
                            Theme = Theme,
                            NumberOfPlayersOfTeam = 3,
                        };
                        _viewer.ResultEdited += new EventHandler<EventArgs<player>>(_viewer_ResultEdited);
                    }
                    _viewer.SetView(e.Field);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 対戦結果編集時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _viewer_ResultEdited(object sender, EventArgs<player> e)
        {
            if (!updateDataCheckBox.Checked)
            {
                var exists = false;
                for(var i =0; i < _sendWaitingPlayers.Count; i++)
                {
                    if(_sendWaitingPlayers[i].Id.Equals(e.Field.Id))
                    {
                        exists = true;
                        _sendWaitingPlayers[i].MatchResults = e.Field.MatchResults;
                    }
                }
                if(!exists)
                    _sendWaitingPlayers.Add(e.Field);

                return;
            }

            if (TournamentInformation.IsManager)
                _client.SetTournamentResultsByManager(TournamentNo, e.Field.Id.value, ToIntArray(e.Field.MatchResults));
            else
                _client.SetTournamentResults(TournamentNo, ToIntArray(e.Field.MatchResults));

            Receive(100);
        }
        /// <summary>送信待ちプレイヤー情報</summary>
        private Collection<player> _sendWaitingPlayers = new Collection<player>();

        /// <summary>
        /// チャット受信時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _receiveManager_ChatReceived(object sender, EventArgs<Collection<chat>> e)
        {
            foreach (chat chat in e.Field)
                _chats.Add(chat);

            if (!reverseDirectionCheckBox.Checked && _announces.Count <= 0)
            {
                // アナウンス無し、逆転無しの場合のみの処理
                foreach (chat chat in _chatFilter.Filter(e.Field))
                {
                    if (0 < chatView.TextLength)
                        chatView.AppendText(Environment.NewLine);
                    chatView.AppendText(chat.ToString());
                    ScrollToLatest();
                }
            }
            else
                UpdateChatView();
        }


        /// <summary>
        /// チャット表示更新
        /// </summary>
        private void UpdateChatView()
        {
            var filteredChats = _chatFilter.Filter(_chats);
            var setText = new StringBuilder();

            if (reverseDirectionCheckBox.Checked)
            {
                var reversedChats = new List<chat>(filteredChats);
                reversedChats.Reverse();
                foreach (var announce in _announces)
                {
                    if (0 < setText.Length)
                        setText.AppendLine();
                    setText.Append(announce);
                }

                if (0 < _announces.Count)
                {
                    if (0 < setText.Length)
                        setText.AppendLine();
                    setText.Append(ANNOUNCE_LINE);
                }

                foreach (var chat in reversedChats)
                {
                    if (0 < setText.Length)
                        setText.AppendLine();
                    setText.Append(chat.ToString());
                }
            }
            else
            {
                foreach (var chat in filteredChats)
                {
                    if (0 < setText.Length)
                        setText.AppendLine();
                    setText.Append(chat.ToString());
                }

                if (0 < _announces.Count)
                {
                    if (0 < setText.Length)
                        setText.AppendLine();
                    setText.Append(ANNOUNCE_LINE);
                }

                foreach (var announce in _announces)
                {
                    setText.AppendLine();
                    setText.Append(announce);
                }
            }

            var text = setText.ToString();
            chatView.Text = text;
            ScrollToLatest();
        }


        /// <summary>
        /// チャット表示を最新行へ
        /// </summary>
        void ScrollToLatest()
        {
            if (reverseDirectionCheckBox.Checked)
            {
                chatView.SelectionStart = 0;
                chatView.ScrollToCaret();
            }
            else
            {
                try
                {
                    chatView.SelectionStart = chatView.Text.Length;
                    chatView.ScrollToCaret();

                    // VistaだとRichTextBoxがあと一歩言うこと聞かないのでWindowsメッセージで1行スクロール
                    User32.SendMessage(chatView.Handle, User32.EM_LINESCROLL, 0, 1);

                    // 横スクロール位置を一番左に

                    var maxCount = short.MaxValue;
                    var count = 0;
                    while (User32.GetScrollPos(chatView.Handle, User32.SB_HORZ) != 0 || maxCount < count)
                    {
                        User32.SendMessage(chatView.Handle, User32.WM_HSCROLL, User32.SB_PAGELEFT, 0);
                        count++;
                    }
                    User32.SetScrollPos(chatView.Handle, User32.SB_HORZ, 0, true);
                }
                catch (Exception) { }
            }
        }

        /// <summary>
        /// ウィンドウテキストをセット
        /// </summary>
        void SetWindowText()
        {
            var rolesString = string.Empty;
            if (_tournamentInformation.Roles == (int)TournamentRoles.Guest)
                rolesString = string.Format("【{0}】", EnumTextAttribute.GetText(TournamentRoles.Guest));
            else if (_tournamentInformation.IsSpectator)
                rolesString = string.Format("【{0}】", EnumTextAttribute.GetText(TournamentRoles.Spectator));
            else
            {
                if (_tournamentInformation.IsManager)
                    rolesString += string.Format("【{0}】", EnumTextAttribute.GetText(TournamentRoles.Manager));
                if (_tournamentInformation.IsPlayer)
                    rolesString += string.Format("【{0}】", EnumTextAttribute.GetText(TournamentRoles.Player));
            }

            Text = string.Format(
                "T{0:d2} {1} {2}/{3}人 {4} {5}",
                _tournamentNo,
                Enum.GetName(typeof(TournamentTypes), _tournamentInformation.Type),
                _tournamentInformation.PlayersCount,
                _tournamentInformation.UsersCount,
                rolesString,
                _tournamentInformation.EntryName);
        }

        /// <summary>
        /// シャッフルするかどうかの初期値セット
        /// </summary>
        private void SetDefaultShuffle()
        {
            switch (TournamentInformation.Type)
            {
                case TournamentTypes.トーナメント:
                    shuffleCheckBox.Checked = true;
                    break;
                default:
                    shuffleCheckBox.Checked = false;
                    break;
            }
        }

        /// <summary>
        /// 役割に応じて表示を変える処理
        /// </summary>
        private void SetView()
        {
            if(TournamentInformation.IsManager)
            {
                announceButton.Enabled = true;
                announceButton.Visible = true;
                addDummyPlayerButton.Enabled = !TournamentInformation.IsStarted && TournamentInformation.PlayersCount < TournamentInformation.UsersCount;
                addDummyPlayerButton.Visible = !TournamentInformation.IsStarted && TournamentInformation.PlayersCount < TournamentInformation.UsersCount;
                splitContainer2.Panel2Collapsed = false;
            }
            else
            {
                announceButton.Enabled = false;
                announceButton.Visible = false;
                addDummyPlayerButton.Enabled = false;
                addDummyPlayerButton.Visible = false;
                splitContainer2.Panel2Collapsed = true;
            }

            if (TournamentInformation.IsPlayer)
            {
                entryButton.Enabled = false;
                entryButton.Visible = false;
                cancelEntryButton.Enabled = !TournamentInformation.IsStarted;
                cancelEntryButton.Visible = !TournamentInformation.IsStarted;
                retireButton.Enabled = TournamentInformation.IsStarted;
                retireButton.Visible = TournamentInformation.IsStarted;
            }
            else
            {
                entryButton.Enabled = !TournamentInformation.IsStarted && (TournamentInformation.PlayersCount < TournamentInformation.UsersCount);
                entryButton.Visible = !TournamentInformation.IsStarted && (TournamentInformation.PlayersCount < TournamentInformation.UsersCount);
                cancelEntryButton.Enabled = false;
                cancelEntryButton.Visible = false;
                retireButton.Enabled = false;
                retireButton.Visible = false;
            }

            if (_viewer != null)
                _viewer.Roles = TournamentInformation.Roles;
        }

        /// <summary>
        /// 開始ボタン・開始時シャッフルボタンの有効/無効
        /// </summary>
        private void SetStartButtonEnabled()
        {
            startButton.Enabled = TournamentInformation.IsStartable;
            shuffleCheckBox.Enabled = TournamentInformation.IsStartable;
        }

        /// <summary>
        /// タイマー制御・対戦監視制御
        /// </summary>
        private void TimerControl()
        {
            if (TournamentInformation.IsStarted && TournamentInformation.IsPlayer)
            {
                listenTimer.Enabled = true;
                listenTimer.Start();
                watchTimer.Enabled = true;
                watchTimer.Start();
                _watcher.Start();
            }
            else
            {
                listenTimer.Enabled = false;
                listenTimer.Stop();
                watchTimer.Enabled = false;
                watchTimer.Stop();
                _watcher.Stop();
            }
        }

        /// <summary>
        /// 待ち受け状態監視タイマー処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listenTimer_Tick(object sender, EventArgs e)
        {
            if (!updateDataCheckBox.Checked)
                return;

            if (_viewer == null)
                return;

            if (!TournamentInformation.IsPlayer)
            {
                listenTimer.Stop();
                return;
            }


            bool isListening = false;
            if (0 <= _port)
                isListening = UdpPort.GetIsListening(_port);
            else
                return;

            var myself = _viewer.GetMyself();
            if (myself == null)
                return;

            if (!myself.retired && myself.waiting != isListening)
                _client.SetTournamentWaiting(TournamentNo, isListening);
        }

        /// <summary>
        /// 対戦状態監視タイマー処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void watchTimer_Tick(object sender, EventArgs e)
        {
            if (!updateDataCheckBox.Checked)
                return;

            if (_viewer == null)
                return;

            if (!TournamentInformation.IsPlayer)
            {
                _watcher.Stop();
                return;
            }

            var myself = _viewer.GetMyself();
            if (myself == null)
                return;

            if (!myself.retired && myself.fighting != _watcher.IsFighting)
                _client.SetTournamentFighting(TournamentNo, _watcher.IsFighting);
        }
        #endregion

        #region 操作
        /// <summary>
        /// データ更新チェックボックス
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void updateDataCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateCheckBox_CheckedChanged();
        }

        /// <summary>
        /// チャット更新チェックボックス
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void updateChatCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            sendButton.Enabled = updateChatCheckBox.Checked;
            chatInput.Enabled = updateChatCheckBox.Checked;

            UpdateCheckBox_CheckedChanged();
        }

        /// <summary>
        /// 更新チェックボックス変更時の処理
        /// </summary>
        void UpdateCheckBox_CheckedChanged()
        {
            lock (_sendWaitingPlayers)
            {
                // 送信待ち編集があるなら先に送る
                if (updateDataCheckBox.Checked && 0 < _sendWaitingPlayers.Count)
                {
                    foreach (var player in _sendWaitingPlayers)
                    {
                        if (TournamentInformation.IsManager)
                            _client.SetTournamentResultsByManager(TournamentNo, player.Id.value, ToIntArray(player.MatchResults));
                        else
                            _client.SetTournamentResults(TournamentNo, ToIntArray(player.MatchResults));
                        System.Threading.Thread.Sleep(50);
                    }

                    _sendWaitingPlayers.Clear();
                }
            }

            // どっちかチェックされてれば受信
            if (updateDataCheckBox.Checked || updateChatCheckBox.Checked)
            {
                receiveTimer.Enabled = true;
                receiveTimer.Start();
                Receive();
            }
            else
            {
                receiveTimer.Enabled = false;
                receiveTimer.Stop();
            }
        }

        /// <summary>
        /// 受信タイマー作動時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void receiveTimer_Tick(object sender, EventArgs e)
        {
            Receive();
        }

        /// <summary>
        /// 受信する
        /// </summary>
        void Receive()
        {
            Receive(0);
        }

        /// <summary>
        /// 受信する
        /// </summary>
        /// <param name="waitTime">受信前に待機させる時間(ミリ秒)</param>
        void Receive(int waitTime)
        {
            if (_isFirstChecking)
                return;

            new Action(delegate
            {
                System.Threading.Thread.Sleep(waitTime);
                _client.GetTournamentData(TournamentNo, TournamentInformation.EntryName, _lastDataTime, _lastChatTime);
            }).BeginInvoke(null, null);
        }

        /// <summary>
        /// ダミーを追加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addDummyPlayerButton_Click(object sender, EventArgs e)
        {
            _client.AddTournamentDummyPlayer(TournamentNo);
            Receive(100);
        }

        /// <summary>
        /// 参加する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void entryButton_Click(object sender, EventArgs e)
        {
            var dialog = new EntryTournamentDialog()
            {
                UserConfig = UserConfig,
                TournamentNo = TournamentNo,
                Theme = Theme,
            };

            var result = dialog.ShowDialog(this);

            switch (result)
            {
                case DialogResult.Yes:
                case DialogResult.OK:
                    UserConfig.TournamentEntryName = dialog.EntryName;
                    try
                    {
                        var id = _client.EntryTournament(TournamentNo, dialog.EntryName, dialog.Ip, dialog.Port);
                        if (id != null)
                        {
                            Id = id;
                            Port = dialog.Port;
                            TournamentInformation.EntryName = dialog.EntryName;
                            Receive();
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
                        var guestId = _client.GuestEntryTournament(TournamentNo, dialog.EntryName);
                        if (guestId != null)
                        {
                            Id = guestId;
                            Port = dialog.Port;
                            TournamentInformation.EntryName = dialog.EntryName;
                            Receive();
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

        /// <summary>
        /// 参加取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelEntryButton_Click(object sender, EventArgs e)
        {
            _client.CancelTournamentEntry(TournamentNo);
            Receive(100);
        }

        /// <summary>
        /// リタイア
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void retireButton_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                this,
                "リタイアしてもよろしいですか？", Application.ProductName,
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Asterisk);

            if (result != DialogResult.Yes)
                return;

            _client.RetireTournament(TournamentNo);
            Receive(100);
        }

        /// <summary>
        /// チャット欄スクロール方向選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void reverseDirectionCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateChatView();
        }

        /// <summary>
        /// コピー(C)クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void コピーToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try { Clipboard.SetText(chatView.SelectedText); }
            catch (Exception) { }
        }

        /// <summary>
        /// URIクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chatView_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            try { Process.Start(e.LinkText); }
            catch (Exception) { }
        }

        /// <summary>
        /// 送信ボタン(チャット)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sendButton_Click(object sender, EventArgs e)
        {
            sendButton.Enabled = false;
            try
            {
                if (string.IsNullOrEmpty(chatInput.Text))
                    return;

                _client.DoTournamentChat(TournamentNo, TournamentInformation.EntryName, chatInput.Text);
                Receive(100);
            }
            finally
            {
                sendButton.Enabled = true;
                chatInput.Text = string.Empty;
                chatInput.Focus();
            }
        }

        private void chatInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                sendButton.Focus();
                e.Handled = true;
            }
        }

        #region 運営の操作
        /// <summary>
        /// アナウンス
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void announceButton_Click(object sender, EventArgs e)
        {
            _announceWindow.SetSize(chatView.Size);

            if (_announceWindow.ShowDialog(this) == DialogResult.OK)
            {
                _client.SetTournamentAnnounces(TournamentNo, _announceWindow.Announces);
                Receive(100);
            }
        }

        /// <summary>
        /// 開始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startButton_Click(object sender, EventArgs e)
        {
            switch (TournamentInformation.Type)
            {
                case TournamentTypes.トーナメント:
                    _client.StartTournament(TournamentNo, TournamentInformation.UsersCount, TournamentInformation.UsersCount, shuffleCheckBox.Checked);
                    break;
                case TournamentTypes.総当たり:
                    _client.StartTournament(TournamentNo, 2, TournamentInformation.UsersCount, shuffleCheckBox.Checked);
                    break;
                default:
                    _client.StartTournament(TournamentNo, TournamentInformation.UsersCount, TournamentInformation.UsersCount, shuffleCheckBox.Checked);
                    break;
            }
            Receive(100);
        }

        private void 運営に任命するToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (usersListView.SelectedItems == null || usersListView.SelectedItems.Count <= 0)
                return;

            foreach (ListViewItem item in usersListView.SelectedItems)
            {
                _client.AddTournamentManager(TournamentNo, item.SubItems[0].Text);
                System.Threading.Thread.Sleep(50);
            }
            Receive(100);
        }

        private void 運営から外すToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (usersListView.SelectedItems == null || usersListView.SelectedItems.Count <= 0)
                return;

            foreach (ListViewItem item in usersListView.SelectedItems)
            {
                _client.RemoveTournamentManager(TournamentNo, item.SubItems[0].Text);
                System.Threading.Thread.Sleep(50);
            }
            Receive(100);
        }

        private void 観戦を許可ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (usersListView.SelectedItems == null || usersListView.SelectedItems.Count <= 0)
                return;

            foreach (ListViewItem item in usersListView.SelectedItems)
            {
                _client.AddTournamentSpectator(TournamentNo, item.SubItems[0].Text);
                System.Threading.Thread.Sleep(50);
            }
            Receive(100);
        }

        private void 観戦を禁止ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (usersListView.SelectedItems == null || usersListView.SelectedItems.Count <= 0)
                return;

            foreach (ListViewItem item in usersListView.SelectedItems)
            {
                _client.RemoveTournamentSpectator(TournamentNo, item.SubItems[0].Text);
                System.Threading.Thread.Sleep(50);
            }
            Receive(100);
        }

        private void 参加取消ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (usersListView.SelectedItems == null || usersListView.SelectedItems.Count <= 0)
                return;

            foreach (ListViewItem item in usersListView.SelectedItems)
            {
                _client.CancelTournamentEntryByManager(TournamentNo, item.SubItems[0].Text);
                System.Threading.Thread.Sleep(50);
            }
            Receive(100);
        }

        private void リタイアさせるToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (usersListView.SelectedItems == null || usersListView.SelectedItems.Count <= 0)
                return;

            foreach (ListViewItem item in usersListView.SelectedItems)
            {
                _client.RetireByManager(TournamentNo, item.SubItems[0].Text);
                System.Threading.Thread.Sleep(50);
            }
            Receive(100);
        }

        private void 追放するToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (usersListView.SelectedItems == null || usersListView.SelectedItems.Count <= 0)
                return;

            foreach (ListViewItem item in usersListView.SelectedItems)
            {
                _client.KickTournamentUser(TournamentNo, item.SubItems[0].Text);
                System.Threading.Thread.Sleep(50);
            }
            Receive(100);
        }
        #endregion

        #endregion

        /// <summary>
        /// int?[] から int[] に変換
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        private int[] ToIntArray(int?[] array)
        {
            var arr = new int[array.Length];
            for (var i = 0; i < array.Length; i++)
            {
                if (array[i].HasValue)
                    arr[i] = array[i].Value;
            }

            return arr;
        }
    }
}
