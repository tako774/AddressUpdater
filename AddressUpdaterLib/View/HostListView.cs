using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using HisoutenSupportTools.AddressUpdater.Lib.AddressService;
using HisoutenSupportTools.AddressUpdater.Lib.Controller;
using HisoutenSupportTools.AddressUpdater.Lib.Model;
using HisoutenSupportTools.AddressUpdater.Lib.Model.Tournament;

namespace HisoutenSupportTools.AddressUpdater.Lib.View
{
    /// <summary>
    /// ホスト一覧表示コントロール
    /// </summary>
    public partial class HostListView : UserControl
    {
        /// <summary>ホスト・大会情報キャッシュ</summary>
        private readonly AddressUpdaterDataSet.HostDataTable _hostCache;
        /// <summary>ホストフィルター</summary>
        private HostFilter _hostFilter;

        #region プロパティ
        /// <summary>一覧文字色の取得・設定</summary>
        public Color ListForeColor
        {
            get { return listView.ForeColor; }
            set { listView.ForeColor = value; }
        }

        /// <summary>一覧背景色の取得・設定</summary>
        public Color ListBackColor
        {
            get { return listView.BackColor; }
            set { listView.BackColor = value; }
        }

        /// <summary>待機中ホスト背景色の取得・設定</summary>
        public Color WaitingHostBackColor { get; set; }
        /// <summary>対戦中ホスト背景色の取得・設定</summary>
        public Color FightingHostBackColor { get; set; }
        /// <summary>大会を表示するかどうか</summary>
        public bool ShowTournaments { get; set; }

        /// <summary>ContextMenuStripの取得・設定</summary>
        public override ContextMenuStrip ContextMenuStrip
        {
            get { return listView.ContextMenuStrip; }
            set { listView.ContextMenuStrip = value; }
        }

        /// <summary>ランクフィルター</summary>
        private string _rankFilter;
        /// <summary>ランクフィルターの取得・設定</summary>
        public string RankFilter
        {
            get { return _rankFilter; }
            set
            {
                _rankFilter = value;
                RefreshView();
            }
        }

        /// <summary>No列幅の取得・設定</summary>
        public int NoColumnHeaderWidth
        {
            get { return noColumnHeader.Width; }
            set { noColumnHeader.Width = value; }
        }

        /// <summary>時刻列幅の取得・設定</summary>
        public int TimeColumnHeaderWidth
        {
            get { return timeColumnHeader.Width; }
            set { timeColumnHeader.Width = value; }
        }

        /// <summary>IP:PORT列幅の取得・設定</summary>
        public int IpPortColumnHeaderWidth
        {
            get { return ip_portColumnHeader.Width; }
            set { ip_portColumnHeader.Width = value; }
        }

        /// <summary>ランク列幅の取得・設定</summary>
        public int RankColumnHeaderWidth
        {
            get { return rankColumnHeader.Width; }
            set { rankColumnHeader.Width = value; }
        }

        /// <summary>コメント列幅の取得・設定</summary>
        public int CommentColumnHeaderWidth
        {
            get { return commentColumnHeader.Width; }
            set { commentColumnHeader.Width = value; }
        }

        /// <summary>選択中のアイテムがホストかどうか</summary>
        public bool IsSelectedItemHost
        {
            get { return !SelectedItem.SubItems[0].Text.Contains("T"); }
        }

        /// <summary>選択中アイテムの取得</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ListViewItem SelectedItem
        {
            get
            {
                lock (this)
                {
                    if (listView.SelectedItems == null || listView.SelectedItems.Count <= 0)
                        return null;

                    return listView.SelectedItems[0];
                }
            }
        }

        /// <summary>選択中ホストNoの取得</summary>
        [Browsable(false)]
        public int SelectedHostNo
        {
            get
            {
                lock (this)
                {
                    if (SelectedItem == null)
                        return -1;

                    if (SelectedItem.SubItems[0].Text.Contains("T"))
                        return -1;

                    return int.Parse(SelectedItem.SubItems[0].Text);
                }
            }
        }

        /// <summary>選択中大会Noの取得</summary>
        [Browsable(false)]
        public int SelectedTournamentNo
        {
            get
            {
                lock (this)
                {
                    if (SelectedItem == null)
                        return -1;

                    if (!SelectedItem.SubItems[0].Text.Contains("T"))
                        return -1;

                    return int.Parse(SelectedItem.SubItems[0].Text.Replace("T", string.Empty));
                }
            }
        }

        /// <summary>選択中ホストIP:PORTの取得</summary>
        [Browsable(false)]
        public string SelectedHostIpPort
        {
            get
            {
                lock (this)
                {
                    if (SelectedItem == null)
                        return null;

                    return SelectedItem.SubItems[2].Text;
                }
            }
        }

        /// <summary>選択中大会種別の取得</summary>
        [Browsable(false)]
        public TournamentTypes SelectedTournamentType
        {
            get
            {
                lock (this)
                {
                    if (SelectedItem == null)
                        return TournamentTypes.トーナメント;

                    var typeText = SelectedItem.SubItems[2].Text.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries)[0];
                    return (TournamentTypes)Enum.Parse(typeof(TournamentTypes), typeText);
                }
            }
        }

        /// <summary>選択中大会人数の取得</summary>
        [Browsable(false)]
        public int SelectedTournamentUserCount
        {
            get
            {
                lock (this)
                {
                    if (SelectedItem == null)
                        return 0;

                    return int.Parse(SelectedItem.SubItems[2].Text.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries)[1].Replace("人", string.Empty));
                }
            }
        }
        #endregion

        /// <summary>ホスト変化時に発生します</summary>
        public event EventHandler<EventArgs> HostChanged;
        /// <summary>ダブルクリック時に発生します</summary>
        public new event EventHandler<EventArgs> DoubleClick;


        /// <summary>
        /// インスタンスの生成
        /// </summary>
        public HostListView()
        {
            InitializeComponent();

            WaitingHostBackColor = SystemColors.Window;
            FightingHostBackColor = Color.FromArgb(230, 230, 230);

            listView.Items.Clear();

            _hostCache = new AddressUpdaterDataSet.HostDataTable();
            _hostCache.CaseSensitive = false;

            _hostFilter = new HostFilter();
        }

        /// <summary>
        /// 表示言語の反映
        /// </summary>
        /// <param name="language"></param>
        public void ReflectLanguage(Language language)
        {
            try { noColumnHeader.Text = language["HostListView_No"]; }
            catch (KeyNotFoundException) { }
            try { timeColumnHeader.Text = language["HostListView_Time"]; }
            catch (KeyNotFoundException) { }
            try { ip_portColumnHeader.Text = language["HostListView_IpPort"]; }
            catch (KeyNotFoundException) { }
            try { rankColumnHeader.Text = language["HostListView_Rank"]; }
            catch (KeyNotFoundException) { }
            try { commentColumnHeader.Text = language["HostListView_Comment"]; }
            catch (KeyNotFoundException) { }
        }

        private bool _binded = false;
        private IClient _client;
        /// <summary>
        /// クライアントのバインド
        /// </summary>
        /// <param name="client">クライアント</param>
        public void BindClient(IClient client)
        {
            if (_binded || client == null)
                return;

            _client = client;
            _client.NewHostReceived += new EventHandler<EventArgs<Collection<host>>>(_client_NewHostReceived);
            _client.HostStatusChanged += new EventHandler<EventArgs<Collection<host>>>(_client_HostStatusChanged);
            _client.HostDeleted += new EventHandler<EventArgs<Collection<host>>>(_client_HostDeleted);
            _client.NewTournamentReceived += new EventHandler<EventArgs<Collection<tournamentInformation>>>(_client_NewTournamentReceived);
            _client.TournamentStatusChanged += new EventHandler<EventArgs<Collection<tournamentInformation>>>(_client_TournamentStatusChanged);
            _client.TournamentDeleted += new EventHandler<EventArgs<Collection<tournamentInformation>>>(_client_TournamentDeleted);

            _binded = true;
        }

        /// <summary>
        /// クライアントのバインド解除
        /// </summary>
        public void UnBindClient()
        {
            if (!_binded || _client == null)
                return;

            _client.NewHostReceived -= _client_NewHostReceived;
            _client.HostStatusChanged -= _client_HostStatusChanged;
            _client.HostDeleted -= _client_HostDeleted;
            _client.NewTournamentReceived -= _client_NewTournamentReceived;
            _client.TournamentStatusChanged -= _client_TournamentStatusChanged;
            _client.TournamentDeleted -= _client_TournamentDeleted;

            _binded = false;
            _client = null;
        }

        /// <summary>
        /// フォント変更時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HostListView_FontChanged(object sender, EventArgs e)
        {
            listView.Font = Font;
        }

        void _client_NewHostReceived(object sender, EventArgs<Collection<host>> e)
        {
            try { Invoke(new MethodInvoker(delegate { lock (this) { AddHostRows(e.Field); } })); }
            catch (ObjectDisposedException) { }
            catch (InvalidOperationException) { }
        }

        void _client_HostStatusChanged(object sender, EventArgs<Collection<host>> e)
        {
            try { Invoke(new MethodInvoker(delegate { lock (this) { UpdateHostRows(e.Field); } })); }
            catch (ObjectDisposedException) { }
            catch (InvalidOperationException) { }
        }

        void _client_HostDeleted(object sender, EventArgs<Collection<host>> e)
        {
            try { Invoke(new MethodInvoker(delegate { lock (this) { DeleteHostRows(e.Field); } })); }
            catch (ObjectDisposedException) { }
            catch (InvalidOperationException) { }
        }

        void _client_NewTournamentReceived(object sender, EventArgs<Collection<tournamentInformation>> e)
        {
            if (!ShowTournaments)
                return;

            try { Invoke(new MethodInvoker(delegate { lock (this) { AddTournamentRows(e.Field); } })); }
            catch (ObjectDisposedException) { }
            catch (InvalidOperationException) { }
        }

        void _client_TournamentStatusChanged(object sender, EventArgs<Collection<tournamentInformation>> e)
        {
            if (!ShowTournaments)
                return;

            try { Invoke(new MethodInvoker(delegate { lock (this) { UpdateTournamentRows(e.Field); } })); }
            catch (ObjectDisposedException) { }
            catch (InvalidOperationException) { }
        }

        void _client_TournamentDeleted(object sender, EventArgs<Collection<tournamentInformation>> e)
        {
            if (!ShowTournaments)
                return;

            try { Invoke(new MethodInvoker(delegate { lock (this) { DeleteTournamentRows(e.Field); } })); }
            catch (ObjectDisposedException) { }
            catch (InvalidOperationException) { }
        }

        /// <summary>
        /// ホスト行をテーブルに追加
        /// </summary>
        /// <param name="newHosts">ホスト情報</param>
        private void AddHostRows(Collection<host> newHosts)
        {
            try
            {
                foreach (var host in newHosts)
                {
                    if (!_hostFilter.IpList.Contains(host.Ip))
                        _hostCache.AddHostRow(host.ToHostRow(_hostCache));
                }
                _hostCache.AcceptChanges();

                AddItems(_hostFilter.Filter(newHosts));
            }
            catch (ConstraintException ex) { System.Diagnostics.Debug.WriteLine(ex); }
            finally
            {
                if (HostChanged != null)
                    HostChanged(this, new EventArgs());
            }
        }

        private void AddItems(Collection<host> newHosts)
        {
            var waitingHosts = GetWaitingHosts(newHosts);
            waitingHosts = FilterByRank(waitingHosts);
            waitingHosts.Sort(HostLastUpdateComparison);
            var fightingHosts = GetFightingHosts(newHosts);
            fightingHosts = FilterByRank(fightingHosts);
            fightingHosts.Sort(HostLastUpdateComparison);

            foreach (var host in fightingHosts)
            {
                var index = GetFightingStartIndex();
                if (0 <= index)
                    listView.Items.Insert(index, ToListViewItem(host));
                else
                    listView.Items.Add(ToListViewItem(host));
            }

            foreach (var host in waitingHosts)
                listView.Items.Insert(0, ToListViewItem(host));
        }

        /// <summary>
        /// 大会行をテーブルに追加
        /// </summary>
        /// <param name="newTournaments">大会情報</param>
        private void AddTournamentRows(Collection<tournamentInformation> newTournaments)
        {
            try
            {
                foreach (var tournament in newTournaments)
                {
                    _hostCache.AddHostRow(tournament.ToHostRow(_hostCache));
                }
                _hostCache.AcceptChanges();

                AddItems(newTournaments);
            }
            catch (ConstraintException ex) { System.Diagnostics.Debug.WriteLine(ex); }
            finally
            {
                if (HostChanged != null)
                    HostChanged(this, new EventArgs());
            }
        }

        private void AddItems(Collection<tournamentInformation> newTournaments)
        {
            var waitingTournaments = GetWaitingTournaments(newTournaments);
            waitingTournaments = FilterByRank(waitingTournaments);
            waitingTournaments.Sort(TournamentLastUpdateComparison);
            var startedTournaments = GetStartedTournaments(newTournaments);
            startedTournaments = FilterByRank(startedTournaments);
            startedTournaments.Sort(TournamentLastUpdateComparison);

            foreach (var tournament in startedTournaments)
            {
                var index = GetFightingStartIndex();
                if (0 <= index)
                    listView.Items.Insert(index, ToListViewItem(tournament));
                else
                    listView.Items.Add(ToListViewItem(tournament));
            }

            foreach (var tournament in waitingTournaments)
                listView.Items.Insert(0, ToListViewItem(tournament));
        }

        /// <summary>
        /// ホスト行を更新
        /// </summary>
        /// <param name="statusChangedHosts">ホスト情報</param>
        private void UpdateHostRows(Collection<host> statusChangedHosts)
        {
            try
            {
                foreach (var changedHost in statusChangedHosts)
                {
                    foreach (AddressUpdaterDataSet.HostRow hostRow in _hostCache)
                    {
                        if (hostRow.Id == changedHost.Id.value && hostRow.Ip == changedHost.Ip)
                        {
                            hostRow.BeginEdit();
                            hostRow.No = changedHost.No;
                            hostRow.Id = changedHost.Id.value;
                            hostRow.Time = changedHost.Time;
                            hostRow.LastUpdate = changedHost.lastUpdate;
                            hostRow.Ip = changedHost.Ip;
                            hostRow.Port = changedHost.Port;
                            hostRow.Rank = changedHost.Rank;
                            hostRow.Comment = changedHost.Comment;
                            hostRow.IsFighting = changedHost.IsFighting;
                            hostRow.IsDeleted = changedHost.isDeleted;
                            hostRow.EndEdit();
                            break;
                        }
                    }
                }
                _hostCache.AcceptChanges();

                UpdateItems(_hostFilter.Filter(statusChangedHosts));
            }
            catch (ConstraintException ex) { Console.WriteLine(ex); }
            finally
            {
                if (HostChanged != null)
                    HostChanged(this, new EventArgs());
            }
        }

        private void UpdateItems(Collection<host> statusChangedHosts)
        {
            foreach (var changedHost in statusChangedHosts)
            {
                for (var i = 0; i < listView.Items.Count; i++)
                {
                    if (listView.Items[i].SubItems[0].Text.Contains("T"))
                        continue;
                    if (int.Parse(listView.Items[i].SubItems[0].Text) == changedHost.No)
                    {
                        listView.Items.RemoveAt(i);
                        break;
                    }
                }
            }

            var waitingHosts = GetWaitingHosts(statusChangedHosts);
            waitingHosts = FilterByRank(waitingHosts);
            waitingHosts.Sort(HostLastUpdateComparison);
            var fightingHosts = GetFightingHosts(statusChangedHosts);
            fightingHosts = FilterByRank(fightingHosts);
            fightingHosts.Sort(HostLastUpdateComparison);


            foreach (var host in fightingHosts)
            {
                int index = GetFightingStartIndex();
                if (0 <= index)
                    listView.Items.Insert(index, ToListViewItem(host));
                else
                    listView.Items.Add(ToListViewItem(host));
            }

            foreach (var host in waitingHosts)
                listView.Items.Insert(0, ToListViewItem(host));
        }

        /// <summary>
        /// 大会行を更新
        /// </summary>
        /// <param name="statusChangedTournaments">大会情報</param>
        private void UpdateTournamentRows(Collection<tournamentInformation> statusChangedTournaments)
        {
            try
            {
                foreach (var changedTournament in statusChangedTournaments)
                {
                    foreach (AddressUpdaterDataSet.HostRow hostRow in _hostCache)
                    {
                        if (hostRow.No == changedTournament.No && hostRow.Id == changedTournament.Id.value)
                        {
                            hostRow.BeginEdit();
                            hostRow.No = changedTournament.No;
                            hostRow.Id = changedTournament.Id.value;
                            hostRow.Time = changedTournament.Time;
                            hostRow.LastUpdate = changedTournament.LastUpdate;
                            try { hostRow.Ip = Enum.GetName(typeof(TournamentTypes), changedTournament.Type); }
                            catch (Exception) { hostRow.Ip = Enum.GetName(typeof(TournamentTypes), TournamentTypes.不明); }
                            hostRow.Port = changedTournament.UserCount;
                            hostRow.Rank = changedTournament.Rank;
                            hostRow.Comment = changedTournament.Comment;
                            hostRow.IsFighting = changedTournament.Started;
                            hostRow.IsDeleted = changedTournament.Deleted;
                            hostRow.EndEdit();
                            break;
                        }
                    }
                }
                _hostCache.AcceptChanges();

                UpdateItems(statusChangedTournaments);
            }
            catch (ConstraintException ex) { Console.WriteLine(ex); }
            finally
            {
                if (HostChanged != null)
                    HostChanged(this, new EventArgs());
            }
        }

        private void UpdateItems(Collection<tournamentInformation> statusChangedTournaments)
        {
            foreach (var changedTournament in statusChangedTournaments)
            {
                for (var i = 0; i < listView.Items.Count; i++)
                {
                    if (int.Parse(listView.Items[i].SubItems[0].Text.Replace("T", string.Empty)) == changedTournament.No)
                    {
                        listView.Items.RemoveAt(i);
                        break;
                    }
                }
            }

            var waitingTournaments = GetWaitingTournaments(statusChangedTournaments);
            waitingTournaments = FilterByRank(waitingTournaments);
            waitingTournaments.Sort(TournamentLastUpdateComparison);
            var startedTournaments = GetStartedTournaments(statusChangedTournaments);
            startedTournaments = FilterByRank(startedTournaments);
            startedTournaments.Sort(TournamentLastUpdateComparison);


            foreach (var tournament in startedTournaments)
            {
                int index = GetFightingStartIndex();
                if (0 <= index)
                    listView.Items.Insert(index, ToListViewItem(tournament));
                else
                    listView.Items.Add(ToListViewItem(tournament));
            }

            foreach (var tournament in waitingTournaments)
                listView.Items.Insert(0, ToListViewItem(tournament));
        }

        /// <summary>
        /// ホスト行をテーブルから削除
        /// </summary>
        /// <param name="deletedHosts">ホスト情報</param>
        private void DeleteHostRows(Collection<host> deletedHosts)
        {
            try
            {
                foreach (var deletedHost in deletedHosts)
                {
                    for (var i = 0; i < _hostCache.Count; i++)
                    {
                        if (_hostCache[i].Id == deletedHost.Id.value && _hostCache[i].Ip == deletedHost.Ip)
                        {
                            _hostCache.RemoveHostRow(_hostCache[i]);
                            break;
                        }
                    }
                }
                _hostCache.AcceptChanges();

                DeleteItems(deletedHosts);
            }
            finally
            {
                if (HostChanged != null)
                    HostChanged(this, new EventArgs());
            }
        }

        private void DeleteItems(Collection<host> deletedHosts)
        {
            foreach (var deletedHost in deletedHosts)
            {
                for (int i = 0; i < listView.Items.Count; i++)
                {
                    if (listView.Items[i].SubItems[0].Text.Contains("T"))
                        continue;
                    if (int.Parse(listView.Items[i].SubItems[0].Text) == deletedHost.No)
                    {
                        listView.Items.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 大会行をテーブルから削除
        /// </summary>
        /// <param name="deletedTournaments">大会情報</param>
        private void DeleteTournamentRows(Collection<tournamentInformation> deletedTournaments)
        {
            try
            {
                foreach (var deletedTournament in deletedTournaments)
                {
                    for (var i = 0; i < _hostCache.Count; i++)
                    {
                        if (_hostCache[i].No == deletedTournament.No && _hostCache[i].Id == deletedTournament.Id.value)
                        {
                            _hostCache.RemoveHostRow(_hostCache[i]);
                            break;
                        }
                    }
                }
                _hostCache.AcceptChanges();

                DeleteItems(deletedTournaments);
            }
            finally
            {
                if (HostChanged != null)
                    HostChanged(this, new EventArgs());
            }
        }

        private void DeleteItems(Collection<tournamentInformation> deletedTournaments)
        {
            foreach (var deletedTournament in deletedTournaments)
            {
                for (int i = 0; i < listView.Items.Count; i++)
                {
                    if (!listView.Items[i].SubItems[0].Text.Contains("T"))
                        continue;
                    if (int.Parse(listView.Items[i].SubItems[0].Text.Replace("T", string.Empty)) == deletedTournament.No)
                    {
                        listView.Items.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 表示中のホストを取得
        /// </summary>
        /// <returns>表示中のホスト</returns>
        public Collection<host> GetShowingHosts()
        {
            var waitingHosts = new Collection<host>();
            var fightingHosts = new Collection<host>();
            var showingHosts = new Collection<host>();

            var waitingHostView = CreateView("IsFighting = 'false'");
            var fightingHostView = CreateView("IsFighting = 'true'");

            foreach (DataRowView rowView in waitingHostView)
            {
                var hostRow = (AddressUpdaterDataSet.HostRow)rowView.Row;
                showingHosts.Add(host.FromHostRow(hostRow));
            }
            foreach (DataRowView rowView in fightingHostView)
            {
                var hostRow = (AddressUpdaterDataSet.HostRow)rowView.Row;
                showingHosts.Add(host.FromHostRow(hostRow));
            }

            return new Collection<host>(showingHosts);
        }

        /// <summary>
        /// リフレッシュ
        /// </summary>
        /// <remarks>キャッシュから一覧表示</remarks>
        public void RefreshView()
        {
            lock (this)
            {
                listView.Items.Clear();
                var hostView = CreateView("");
                foreach (DataRowView rowView in hostView)
                {
                    listView.Items.Add(ToListViewItem(rowView));
                }
            }
        }

        /// <summary>
        /// クリア
        /// </summary>
        public void Clear()
        {
            lock (this)
            {
                listView.Items.Clear();
                _hostCache.Clear();
                _hostCache.AcceptChanges();
                if (_client != null)
                    _client.ClearHostCache();
            }
        }

        /// <summary>
        /// フィルターしているIPを取得
        /// </summary>
        /// <returns>フィルターIPリスト</returns>
        public Collection<string> GetIpFilters()
        {
            var filters = new Collection<string>();
            foreach (var ip in _hostFilter.IpList)
            {
                filters.Add(ip);
            }

            return filters;
        }

        /// <summary>
        /// IPフィルターを追加
        /// </summary>
        /// <param name="ip">ip</param>
        public void AddIpFilter(string ip)
        {
            var tournamentTypeNames = new Collection<string>(Enum.GetNames(typeof(TournamentTypes)));
            if (tournamentTypeNames.Contains(ip))
                return;

            if (!_hostFilter.IpList.Contains(ip))
            {
                lock (this)
                {
                    _hostFilter.IpList.Add(ip);
                    var removeRows = new Collection<AddressUpdaterDataSet.HostRow>();
                    foreach (AddressUpdaterDataSet.HostRow host in _hostCache)
                    {
                        if(_hostFilter.IpList.Contains(host.Ip))
                        {
                            removeRows.Add(host);
                            continue;
                        }
                    }

                    foreach (var row in removeRows)
                        _hostCache.RemoveHostRow(row);
                }

                RefreshView();
            }
        }

        /// <summary>
        /// 待機中ホストの抽出
        /// </summary>
        /// <param name="hosts">ホスト</param>
        /// <returns>待機中ホスト</returns>
        private List<host> GetWaitingHosts(Collection<host> hosts)
        {
            var waitingHosts = new List<host>();

            foreach (var host in hosts)
                if (!host.IsFighting)
                    waitingHosts.Add((host)host.Clone());

            return waitingHosts;
        }

        /// <summary>
        /// 待機中大会の抽出
        /// </summary>
        /// <param name="tournaments">大会</param>
        /// <returns>待機中大会</returns>
        private List<tournamentInformation> GetWaitingTournaments(Collection<tournamentInformation> tournaments)
        {
            var waitingTournaments = new List<tournamentInformation>();

            foreach (var tournament in tournaments)
                if (!tournament.Started)
                    waitingTournaments.Add((tournamentInformation)tournament.Clone());

            return waitingTournaments;
        }

        /// <summary>
        /// 対戦中ホストの抽出
        /// </summary>
        /// <param name="hosts">ホスト</param>
        /// <returns>対戦中ホスト</returns>
        private List<host> GetFightingHosts(Collection<host> hosts)
        {
            var fightingHosts = new List<host>();

            foreach (var host in hosts)
                if (host.IsFighting)
                    fightingHosts.Add((host)host.Clone());

            return fightingHosts;
        }

        /// <summary>
        /// 開始された大会の抽出
        /// </summary>
        /// <param name="tournaments">大会</param>
        /// <returns>開始された大会</returns>
        private List<tournamentInformation> GetStartedTournaments(Collection<tournamentInformation> tournaments)
        {
            var startedTournaments = new List<tournamentInformation>();

            foreach (var tournament in tournaments)
                if (tournament.Started)
                    startedTournaments.Add((tournamentInformation)tournament.Clone());

            return startedTournaments;
        }

        /// <summary>
        /// ランクフィルタ処理
        /// </summary>
        /// <param name="hosts"></param>
        /// <returns></returns>
        private List<host> FilterByRank(List<host> hosts)
        {
            if (_rankFilter == null || _rankFilter == "")
                return hosts;

            var filteredList = new List<host>();
            foreach (var host in hosts)
            {
                if (host.Rank.ToLower().Contains(_rankFilter.ToLower()))
                    filteredList.Add((host)host.Clone());
            }

            return filteredList;
        }

        /// <summary>
        /// ランクフィルタ処理
        /// </summary>
        /// <param name="tournaments"></param>
        /// <returns></returns>
        private List<tournamentInformation> FilterByRank(List<tournamentInformation> tournaments)
        {
            if (_rankFilter == null || _rankFilter == "")
                return tournaments;

            var filteredList = new List<tournamentInformation>();
            foreach (var tournament in tournaments)
            {
                if (tournament.Rank.ToLower().Contains(_rankFilter.ToLower()))
                    filteredList.Add((tournamentInformation)tournament.Clone());
            }

            return filteredList;
        }

        /// <summary>
        /// 最終更新時刻比較
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private static int HostLastUpdateComparison(host a, host b)
        {
            return a.lastUpdate.CompareTo(b.lastUpdate);
        }

        /// <summary>
        /// 最終更新時刻比較
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private static int TournamentLastUpdateComparison(tournamentInformation a, tournamentInformation b)
        {
            return a.LastUpdate.CompareTo(b.LastUpdate);
        }

        /// <summary>
        /// 一覧表示 対戦中ホストの開始位置取得
        /// </summary>
        /// <returns></returns>
        private int GetFightingStartIndex()
        {
            lock (this)
            {
                if (listView.Items.Count <= 0)
                    return 0;

                for (var i = 0; i < listView.Items.Count; i++)
                {
                    if (listView.Items[i].BackColor == FightingHostBackColor)
                        return i;
                }

                return -1;
            }
        }

        /// <summary>
        /// host → ListViewItem
        /// </summary>
        /// <param name="host">ホスト</param>
        /// <returns>ListViewItem</returns>
        private ListViewItem ToListViewItem(host host)
        {
            var item = new ListViewItem(new string[]
            {
                host.No.ToString(),
                host.Time.ToString("HH:mm"),
                string.Format("{0}:{1}", host.Ip, host.Port),
                host.Rank,
                host.Comment
            });

            if (host.IsFighting)
                item.BackColor = FightingHostBackColor;
            else
                item.BackColor = WaitingHostBackColor;

            return item;
        }

        /// <summary>
        /// tournamentInformation → ListViewItem
        /// </summary>
        /// <param name="tournament">大会</param>
        /// <returns>ListViewItem</returns>
        private ListViewItem ToListViewItem(tournamentInformation tournament)
        {
            var typeString = "";
            try { typeString = Enum.GetName(typeof(TournamentTypes), tournament.Type); }
            catch (Exception) { typeString = Enum.GetName(typeof(TournamentTypes), TournamentTypes.不明); }

            var item = new ListViewItem(new string[]
            {
                "T" + tournament.No.ToString(),
                tournament.Time.ToString("HH:mm"),
                string.Format("{0}:{1}/{2}人", typeString, tournament.PlayersCount, tournament.UserCount),
                tournament.Rank,
                tournament.Comment
            });

            if (tournament.Started)
                item.BackColor = FightingHostBackColor;
            else
                item.BackColor = WaitingHostBackColor;

            return item;
        }

        /// <summary>
        /// DataRowView → ListViewItem
        /// </summary>
        /// <param name="rowView">ビュー</param>
        /// <returns>ListViewItem</returns>
        private ListViewItem ToListViewItem(DataRowView rowView)
        {
            var hostRow = (AddressUpdaterDataSet.HostRow)rowView.Row;

            var tournamentTypeNames = new Collection<string>( Enum.GetNames(typeof(TournamentTypes)));
            var isTournamentRow = tournamentTypeNames.Contains(hostRow.Ip);

            var item = new ListViewItem(new string[]
            {
                isTournamentRow ? "T" + hostRow.No.ToString() : hostRow.No.ToString(),
                hostRow.Time.ToString("HH:mm"),
                isTournamentRow ? string.Format("{0}:{1}/{2}人", hostRow.Ip, hostRow.PlayersCount, hostRow.Port) : string.Format("{0}:{1}", hostRow.Ip, hostRow.Port),
                hostRow.Rank,
                hostRow.Comment
            });

            if (hostRow.IsFighting)
                item.BackColor = FightingHostBackColor;
            else
                item.BackColor = WaitingHostBackColor;

            return item;
        }

        private DataView CreateView(string filter)
        {
            var view = new DataView(_hostCache);
            view.Sort = "IsFighting, LastUpdate DESC";

            if (filter == null || filter == "")
            {
                view.RowFilter = string.Format("Rank LIKE '*{0}*'", _rankFilter);
                return view;
            }

            view.RowFilter = string.Format("{0} AND Rank LIKE '*{1}*'", filter, _rankFilter);
            return view;
        }

        /// <summary>
        /// 一覧ダブルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView_DoubleClick(object sender, System.EventArgs e)
        {
            if (DoubleClick == null)
                return;

            lock (this)
            {
                DoubleClick(sender, e);
            }
        }
    }
}
