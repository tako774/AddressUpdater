using System;
using System.Collections.ObjectModel;
using HisoutenSupportTools.AddressUpdater.Lib.AddressService;
using HisoutenSupportTools.AddressUpdater.Lib.Model.Tournament;
using HisoutenSupportTools.AddressUpdater.Lib.Network;

namespace HisoutenSupportTools.AddressUpdater.Lib.Controller
{
    /// <summary>
    /// クライアント
    /// </summary>
    public class Client : IClient, IDisposable
    {
        #region フィールド
        /// <summary>Server</summary>
        protected IServer _server;
        /// <summary>受信管理オブジェクト</summary>
        protected ReceiveManager _receiveManager;
        /// <summary>次の取得時にホスト情報を全件取得するかどうか</summary>
        protected bool _refleshNext = false;
        #endregion

        #region プロパティ
        /// <summary>サーバー名の取得</summary>
        public string ServerName { get { return _server.Name; } }
        /// <summary>サーバーUriの取得</summary>
        public string ServerUri { get { return _server.Uri; } }
        /// <summary>次の取得時にホスト情報を全件取得するかどうか</summary>
        public bool RefleshNext
        {
            get { return _refleshNext; }
            set { _refleshNext = value; }
        }
        #endregion

        #region イベント
        /// <summary>接続人数変化時に発生します</summary>
        public event EventHandler<EventArgs<int>> UserCountChanged;
        /// <summary>アナウンス変化時に発生します</summary>
        public event EventHandler<EventArgs<Collection<string>>> AnnounceChanged;
        /// <summary>新規ホスト受信時に発生します</summary>
        public event EventHandler<EventArgs<Collection<host>>> NewHostReceived;
        /// <summary>ホスト状態変化時に発生します</summary>
        public event EventHandler<EventArgs<Collection<host>>> HostStatusChanged;
        /// <summary>ホスト削除時に発生します</summary>
        public event EventHandler<EventArgs<Collection<host>>> HostDeleted;
        /// <summary>新規大会受信時に発生します</summary>
        public event EventHandler<EventArgs<Collection<tournamentInformation>>> NewTournamentReceived;
        /// <summary>大会状態変化時に発生します</summary>
        public event EventHandler<EventArgs<Collection<tournamentInformation>>> TournamentStatusChanged;
        /// <summary>大会削除時に発生します</summary>
        public event EventHandler<EventArgs<Collection<tournamentInformation>>> TournamentDeleted;
        /// <summary>大会情報受信時に発生します</summary>
        public event EventHandler<EventArgs<tournament>> TournamentDataReceived;
        /// <summary>チャット受信時に発生します</summary>
        public event EventHandler<EventArgs<Collection<chat>>> ChatReceived;
        /// <summary>自動マッチング結果変化時に発生します</summary>
        public event EventHandler<EventArgs<matchingResult>> MatchingResultChanged;
        /// <summary>自動マッチング結果が空になった時に発生します</summary>
        public event EventHandler MatchingResultCleared;
        /// <summary>エラーが継続的に起きた際に発生します</summary>
        public event EventHandler ConsecutiveErrorHappened;
        #endregion

        #region 初期化
        /// <summary>
        /// インスタンスの生成
        /// </summary>
        /// <param name="serverName">サーバー名</param>
        /// <param name="serverUri">サーバーURI</param>
        public Client(string serverName, string serverUri)
        {
            _server = new Server(serverName, serverUri);
            _server.Timeout = 10000;
            _server.AllReceived += new EventHandler<EventArgs<allData>>(_server_AllReceived);
            _server.TournamentDataReceived += new EventHandler<EventArgs<tournament>>(_server_TournamentDataReceived);
            _server.MatchingResultReceived += new EventHandler<EventArgs<matchingResult>>(_server_MatchingResultReceived);
            _server.ConsecutiveErrorHappened += new EventHandler(_server_ConsecutiveErrorHappened);

            _receiveManager = new ReceiveManager();
            _receiveManager.UserCountChanged += new EventHandler<EventArgs<int>>(_receiveManager_UserCountChanged);
            _receiveManager.AnnounceChanged += new EventHandler<EventArgs<Collection<string>>>(_receiveManager_AnnounceChanged);
            _receiveManager.NewHostReceived += new EventHandler<EventArgs<Collection<host>>>(_receiveManager_NewHostReceived);
            _receiveManager.HostStatusChanged += new EventHandler<EventArgs<Collection<host>>>(_receiveManager_HostStatusChanged);
            _receiveManager.HostDeleted += new EventHandler<EventArgs<Collection<host>>>(_receiveManager_HostDeleted);
            _receiveManager.NewTournamentReceived += new EventHandler<EventArgs<Collection<tournamentInformation>>>(_receiveManager_NewTournamentReceived);
            _receiveManager.TournamentStatusChanged += new EventHandler<EventArgs<Collection<tournamentInformation>>>(_receiveManager_TournamentStatusChanged);
            _receiveManager.TournamentDeleted += new EventHandler<EventArgs<Collection<tournamentInformation>>>(_receiveManager_TournamentDeleted);
            _receiveManager.ChatReceived += new EventHandler<EventArgs<Collection<chat>>>(_receiveManager_ChatReceived);
            _receiveManager.MatchingResultChanged += new EventHandler<EventArgs<matchingResult>>(_receiveManager_MatchingResultChanged);
            _receiveManager.MatchingResultCleared += new EventHandler(_receiveManager_MatchingResultCleared);
        }
        #endregion


        #region 一般
        /// <summary>
        /// サーバー設定取得
        /// </summary>
        /// <returns>サーバー設定</returns>
        /// <exception cref="CommunicationFailedException">通信失敗時に発生します</exception>
        public serverSetting GetServerSetting()
        {
            return _server.GetSetting();
        }

        /// <summary>
        /// 受信する
        /// </summary>
        /// <param name="getHost">ホスト情報を受信するかどうか</param>
        /// <param name="getChat">チャット情報を受信するかどうか</param>
        [Obsolete]
        public void Receive(bool getHost, bool getChat)
        {
            Receive(getHost, getChat, false);
        }

        /// <summary>
        /// 受信する
        /// </summary>
        /// <param name="getHost">ホスト情報を受信するかどうか</param>
        /// <param name="getChat">チャット情報を受信するかどうか</param>
        /// <param name="getTournament">大会情報を受信するかどうか</param>
        public void Receive(bool getHost, bool getChat, bool getTournament)
        {
            _server.Receive(getHost, getChat, getTournament);
        }

        /// <summary>
        /// 退室
        /// </summary>
        public void Leave()
        {
            _server.Leave();
        }

        /// <summary>
        /// ホストを登録する
        /// </summary>
        /// <param name="host">ホスト</param>
        public void RegisterHost(host host)
        {
            _server.RegisterHost(host);
        }

        /// <summary>
        /// ホストの登録解除
        /// </summary>
        /// <param name="ip">IP</param>
        public void UnregisterHost(string ip)
        {
            _server.UnregisterHost(ip);
        }

        /// <summary>
        /// 対戦状態を設定する
        /// </summary>
        /// <param name="ip">IP</param>
        /// <param name="isFighting">対戦中かどうか</param>
        public void SetFighting(string ip, bool isFighting)
        {
            _server.SetFighting(ip, isFighting);
        }

        /// <summary>
        /// チャットする
        /// </summary>
        /// <param name="contents">内容</param>
        [Obsolete]
        public void DoChat(string contents)
        {
            DoChat(null, contents);
        }

        /// <summary>
        /// チャットする
        /// </summary>
        /// <param name="name">名前</param>
        /// <param name="contents">内容</param>
        public void DoChat(string name, string contents)
        {
            _server.DoChat(name, contents);
        }
        #endregion

        #region 大会
        /// <summary>
        /// 大会情報の取得
        /// </summary>
        /// <param name="tournamentNo">大会番号</param>
        /// <param name="entryName">エントリー名</param>
        /// <param name="lastTournamentTime">最後に取得した日時</param>
        /// <param name="lastTournamentChatTime">最後に取得したチャット日時</param>
        public void GetTournamentData(int tournamentNo, string entryName, DateTime lastTournamentTime, DateTime lastTournamentChatTime)
        {
            _server.GetTournamentData(tournamentNo, entryName, lastTournamentTime, lastTournamentChatTime);
        }

        /// <summary>
        /// 大会登録
        /// </summary>
        /// <param name="userCount">人数</param>
        /// <param name="type">大会の種類</param>
        /// <param name="rank">ランク</param>
        /// <param name="comment">コメント</param>
        public void RegisterTournament(int userCount, TournamentTypes type, string rank, string comment)
        {
            _server.RegisterTournament(userCount, type, rank, comment);
        }

        /// <summary>
        /// 大会登録解除
        /// </summary>
        public void UnregisterTournament()
        {
            _server.UnregisterTournament();
        }

        /// <summary>
        /// アナウンスを設定
        /// </summary>
        /// <param name="tournamentNo">大会番号</param>
        /// <param name="announces">アナウンス</param>
        public void SetTournamentAnnounces(int tournamentNo, Collection<string> announces)
        {
            _server.SetTournamentAnnounces(tournamentNo, announces);
        }

        /// <summary>
        /// 大会にエントリー
        /// </summary>
        /// <param name="tournamentNo">大会番号</param>
        /// <param name="entryName">エントリー名</param>
        /// <param name="ip">IP</param>
        /// <param name="port">ポート</param>
        /// <returns>自分のID(失敗時null)</returns>
        /// <exception cref="CommunicationFailedException">通信失敗時に発生します</exception>
        public id EntryTournament(int tournamentNo, string entryName, string ip, int port)
        {
            return _server.EntryTournament(tournamentNo, entryName, ip, port);
        }

        /// <summary>
        /// 観戦で入る
        /// </summary>
        /// <param name="tournamentNo">大会番号</param>
        /// <param name="entryName">エントリー名</param>
        /// <returns>自分のID(失敗時null)</returns>
        /// <exception cref="CommunicationFailedException">通信失敗時に発生します</exception>
        public id GuestEntryTournament(int tournamentNo, string entryName)
        {
            return _server.GuestEntryTournament(tournamentNo, entryName);
        }

        /// <summary>
        /// エントリー取り消し
        /// </summary>
        /// <param name="tournamentNo">大会番号</param>
        public void CancelTournamentEntry(int tournamentNo)
        {
            _server.CancelTournamentEntry(tournamentNo);
        }

        /// <summary>
        /// リタイアする
        /// </summary>
        /// <param name="tournamentNo">大会番号</param>
        public void RetireTournament(int tournamentNo)
        {
            _server.RetireTournament(tournamentNo);
        }

        /// <summary>
        /// ホスト中かどうかを設定する
        /// </summary>
        /// <param name="tournamentNo">大会番号</param>
        /// <param name="isWaiting">ホスト中かどうか</param>
        public void SetTournamentWaiting(int tournamentNo, bool isWaiting)
        {
            _server.SetTournamentWaiting(tournamentNo, isWaiting);
        }

        /// <summary>
        /// 対戦中かどうかを設定する
        /// </summary>
        /// <param name="tournamentNo">大会番号</param>
        /// <param name="isFighting">対戦中かどうか</param>
        public void SetTournamentFighting(int tournamentNo, bool isFighting)
        {
            _server.SetTournamentFighting(tournamentNo, isFighting);
        }

        /// <summary>
        /// 結果報告をする
        /// </summary>
        /// <param name="tournamentNo">大会番号</param>
        /// <param name="results">結果</param>
        public void SetTournamentResults(int tournamentNo, int[] results)
        {
            _server.SetTournamentResults(tournamentNo, results);
        }

        /// <summary>
        /// チャットする(大会)
        /// </summary>
        /// <param name="tournamentNo">大会番号</param>
        /// <param name="name">名前</param>
        /// <param name="contents">内容</param>
        public void DoTournamentChat(int tournamentNo, string name, string contents)
        {
            _server.DoTournamentChat(tournamentNo, name, contents);
        }
        #endregion

        #region 大会(運営)
        /// <summary>
        /// 運営を追加
        /// </summary>
        /// <param name="tournamentNo">大会番号</param>
        /// <param name="id">追加するID</param>
        public void AddTournamentManager(int tournamentNo, string id)
        {
            _server.AddTournamentManager(tournamentNo, id);
        }

        /// <summary>
        /// 運営から除く
        /// </summary>
        /// <param name="tournamentNo">大会番号</param>
        /// <param name="id">除くID</param>
        public void RemoveTournamentManager(int tournamentNo, string id)
        {
            _server.RemoveTournamentManager(tournamentNo, id);
        }

        /// <summary>
        /// ダミーの参加者を追加
        /// </summary>
        /// <param name="tournamentNo">大会番号</param>
        public void AddTournamentDummyPlayer(int tournamentNo)
        {
            _server.AddTournamentDummyPlayer(tournamentNo);
        }

        /// <summary>
        /// 観戦を追加
        /// </summary>
        /// <param name="tournamentNo">大会番号</param>
        /// <param name="id">除くID</param>
        public void AddTournamentSpectator(int tournamentNo, string id)
        {
            _server.AddTournamentSpectator(tournamentNo, id);
        }

        /// <summary>
        /// 観戦から除く
        /// </summary>
        /// <param name="tournamentNo">大会番号</param>
        /// <param name="id">除くID</param>
        public void RemoveTournamentSpectator(int tournamentNo, string id)
        {
            _server.RemoveTournamentSpectator(tournamentNo, id);
        }

        /// <summary>
        /// 人数再設定
        /// </summary>
        /// <param name="tournamentNo">大会番号</param>
        /// <param name="userCount">人数</param>
        public void SetTournamentUserCount(int tournamentNo, int userCount)
        {
            _server.SetTournamentUserCount(tournamentNo, userCount);
        }

        /// <summary>
        /// トーナメント開始
        /// </summary>
        /// <param name="tournamentNo">大会番号</param>
        /// <param name="minCount">開始できる最少人数</param>
        /// <param name="maxCount">開始できる最大人数</param>
        /// <param name="shuffle">順番をシャッフルするかどうか</param>
        public void StartTournament(int tournamentNo, int minCount, int maxCount , bool shuffle)
        {
            _server.StartTournament(tournamentNo, minCount, maxCount, shuffle);
        }

        /// <summary>
        /// 参加を取り消させる
        /// </summary>
        /// <param name="tournamentNo">大会番号</param>
        /// <param name="id">ID</param>
        public void CancelTournamentEntryByManager(int tournamentNo, string id)
        {
            _server.CancelTournamentEntryByManager(tournamentNo, id);
        }

        /// <summary>
        /// リタイアさせる
        /// </summary>
        /// <param name="tournamentNo">大会番号</param>
        /// <param name="id">ID</param>
        public void RetireByManager(int tournamentNo, string id)
        {
            _server.RetireByManager(tournamentNo, id);
        }

        /// <summary>
        /// (参加者を)キックする
        /// </summary>
        /// <param name="tournamentNo">大会番号</param>
        /// <param name="id">キックするID</param>
        public void KickTournamentUser(int tournamentNo, string id)
        {
            _server.KickTournamentUser(tournamentNo, id);
        }

        /// <summary>
        /// 結果報告(運営)
        /// </summary>
        /// <param name="tournamentNo">大会番号</param>
        /// <param name="id">修正対象のID</param>
        /// <param name="results">結果</param>
        public void SetTournamentResultsByManager(int tournamentNo, string id, int[] results)
        {
            _server.SetTournamentResultsByManager(tournamentNo, id, results);
        }
        #endregion

        #region 自動マッチング
        /// <summary>
        /// 自動マッチング登録
        /// </summary>
        /// <param name="user">登録情報</param>
        public void RegisterMatching(tencoUser user)
        {
            _server.RegisterMatching(user);
        }

        /// <summary>
        /// 自動マッチング登録解除
        /// </summary>
        public void UnregisterMatching()
        {
            _receiveManager.ClearMatchingResult();
            _server.UnregisterMatching();
        }

        /// <summary>
        /// 自動マッチング 対戦履歴追加
        /// </summary>
        /// <param name="oponent">対戦相手のID</param>
        public void AddMatchingHistory(id oponent)
        {
            _server.AddMatchingHistory(oponent);
        }

        /// <summary>
        /// 自動マッチング 準備OK
        /// </summary>
        /// <param name="oponent">対戦相手のID</param>
        public void SetPrepared(id oponent)
        {
            _server.SetPrepared(oponent);
        }

        /// <summary>
        /// 自動マッチング スキップ
        /// </summary>
        /// <param name="oponent">対戦相手のID</param>
        public void SkipMatching(id oponent)
        {
            _server.SkipMatching(oponent);
        }

        /// <summary>
        /// マッチング結果取得
        /// </summary>
        public void GetMatchingResult()
        {
            _server.GetMatchingResult();
        }
        #endregion

        /// <summary>
        /// ローカルのホスト情報クリア
        /// </summary>
        public void ClearHostCache()
        {
            _receiveManager.ClearHostCache();
            _server.ResetLastHostTime();
        }

        /// <summary>
        /// チャットのキャッシュをクリア
        /// </summary>
        public void ClearChatCache()
        {
            _receiveManager.ClearChatCache();
            _server.ResetLastChatTime();
        }

        #region イベント処理
        void _server_AllReceived(object sender, EventArgs<allData> e)
        {
            _receiveManager.AnalyzeReceivedData(e.Field);
        }

        void _server_TournamentDataReceived(object sender, EventArgs<tournament> e)
        {
            if (TournamentDataReceived != null)
                TournamentDataReceived(this, e);
        }

        void _server_MatchingResultReceived(object sender, EventArgs<matchingResult> e)
        {
            _receiveManager.AnalyzeReceivedData(e.Field);
        }


        void _receiveManager_UserCountChanged(object sender, EventArgs<int> e)
        {
            if (UserCountChanged == null)
                return;

            UserCountChanged(this, e);
        }

        void _receiveManager_AnnounceChanged(object sender, EventArgs<Collection<string>> e)
        {
            if (AnnounceChanged == null)
                return;

            AnnounceChanged(this, e);
        }

        void _receiveManager_NewHostReceived(object sender, EventArgs<Collection<host>> e)
        {
            if (NewHostReceived == null)
                return;

            NewHostReceived(this, e);
        }

        void _receiveManager_HostStatusChanged(object sender, EventArgs<Collection<host>> e)
        {
            if (HostStatusChanged == null)
                return;

            HostStatusChanged(this, e);
        }

        void _receiveManager_HostDeleted(object sender, EventArgs<Collection<host>> e)
        {
            if (HostDeleted == null)
                return;

            HostDeleted(this, e);
        }

        void _receiveManager_NewTournamentReceived(object sender, EventArgs<Collection<tournamentInformation>> e)
        {
            if (NewTournamentReceived == null)
                return;

            NewTournamentReceived(this, e);
        }

        void _receiveManager_TournamentStatusChanged(object sender, EventArgs<Collection<tournamentInformation>> e)
        {
            if (TournamentStatusChanged == null)
                return;

            TournamentStatusChanged(this, e);
        }

        void _receiveManager_TournamentDeleted(object sender, EventArgs<Collection<tournamentInformation>> e)
        {
            if (TournamentDeleted == null)
                return;

            TournamentDeleted(this, e);
        }

        void _receiveManager_ChatReceived(object sender, EventArgs<Collection<chat>> e)
        {
            if (ChatReceived == null)
                return;

            ChatReceived(this, e);
        }

        void _receiveManager_MatchingResultChanged(object sender, EventArgs<matchingResult> e)
        {
            if (MatchingResultChanged != null)
                MatchingResultChanged(this, e);
        }

        void _receiveManager_MatchingResultCleared(object sender, EventArgs e)
        {
            if (MatchingResultCleared != null)
                MatchingResultCleared(this, e);
        }

        void _server_ConsecutiveErrorHappened(object sender, EventArgs e)
        {
            if (ConsecutiveErrorHappened == null)
                return;

            ConsecutiveErrorHappened(this, e);
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _server.Name;
        }

        #region IDisposable メンバ

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            _receiveManager.UserCountChanged -= _receiveManager_UserCountChanged;
            _receiveManager.AnnounceChanged -= _receiveManager_AnnounceChanged;
            _receiveManager.NewHostReceived -= _receiveManager_NewHostReceived;
            _receiveManager.HostStatusChanged -= _receiveManager_HostStatusChanged;
            _receiveManager.HostDeleted -= _receiveManager_HostDeleted;
            _receiveManager.NewTournamentReceived -= _receiveManager_NewTournamentReceived;
            _receiveManager.TournamentStatusChanged -= _receiveManager_TournamentStatusChanged;
            _receiveManager.TournamentDeleted -= _receiveManager_TournamentDeleted;
            _receiveManager.ChatReceived -= _receiveManager_ChatReceived;

            _server.AllReceived -= _server_AllReceived;
            _server.TournamentDataReceived -= _server_TournamentDataReceived;
            _server.ConsecutiveErrorHappened -= _server_ConsecutiveErrorHappened;
            _server.Dispose();
        }

        #endregion
    }
}
