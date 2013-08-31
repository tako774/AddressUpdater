using System;
using System.Collections.ObjectModel;
using HisoutenSupportTools.AddressUpdater.Lib.AddressService;
using HisoutenSupportTools.AddressUpdater.Lib.Model.Tournament;
using HisoutenSupportTools.AddressUpdater.Lib.Network;

namespace HisoutenSupportTools.AddressUpdater.Lib.Controller
{
    /// <summary>
    /// クライアントインターフェース
    /// </summary>
    public interface IClient : IDisposable
    {
        /// <summary>サーバー名の取得</summary>
        string ServerName { get; }
        /// <summary>サーバーUriの取得</summary>
        string ServerUri { get; }
        /// <summary>次の取得時にホスト情報を全件取得するかどうか</summary>
        bool RefleshNext { get; set; }

        #region イベント
        /// <summary>接続人数変化時に発生します</summary>
        event EventHandler<EventArgs<int>> UserCountChanged;
        /// <summary>アナウンス変化時に発生します</summary>
        event EventHandler<EventArgs<Collection<string>>> AnnounceChanged;
        /// <summary>新規ホスト受信時に発生します</summary>
        event EventHandler<EventArgs<Collection<host>>> NewHostReceived;
        /// <summary>ホスト状態変化時に発生します</summary>
        event EventHandler<EventArgs<Collection<host>>> HostStatusChanged;
        /// <summary>ホスト削除時に発生します</summary>
        event EventHandler<EventArgs<Collection<host>>> HostDeleted;
        /// <summary>新規大会受信時に発生します</summary>
        event EventHandler<EventArgs<Collection<tournamentInformation>>> NewTournamentReceived;
        /// <summary>大会状態変化時に発生します</summary>
        event EventHandler<EventArgs<Collection<tournamentInformation>>> TournamentStatusChanged;
        /// <summary>大会削除時に発生します</summary>
        event EventHandler<EventArgs<Collection<tournamentInformation>>> TournamentDeleted;
        /// <summary>大会情報受信時に発生します</summary>
        event EventHandler<EventArgs<tournament>> TournamentDataReceived;
        /// <summary>チャットを受信した際に発生します</summary>
        event EventHandler<EventArgs<Collection<chat>>> ChatReceived;
        /// <summary>自動マッチング結果変化時に発生します</summary>
        event EventHandler<EventArgs<matchingResult>> MatchingResultChanged;
        /// <summary>自動マッチング結果が空になった時に発生します</summary>
        event EventHandler MatchingResultCleared;
        /// <summary>エラーが継続的に起きた際に発生します</summary>
        event EventHandler ConsecutiveErrorHappened;
        #endregion

        #region 一般
        /// <summary>
        /// サーバー設定取得
        /// </summary>
        /// <returns>サーバー設定</returns>
        /// <exception cref="CommunicationFailedException">通信失敗時に発生します</exception>
        serverSetting GetServerSetting();

        /// <summary>
        /// 受信する
        /// </summary>
        /// <param name="getHost">ホストを取得するかどうか</param>
        /// <param name="getChat">チャットを取得するかどうか</param>
        [Obsolete]
        void Receive(bool getHost, bool getChat);

        /// <summary>
        /// 受信する
        /// </summary>
        /// <param name="getHost">ホストを取得するかどうか</param>
        /// <param name="getChat">チャットを取得するかどうか</param>
        /// <param name="getTournament">大会を取得するかどうか</param>
        void Receive(bool getHost, bool getChat, bool getTournament);

        /// <summary>
        /// 退室
        /// </summary>
        void Leave();

        /// <summary>
        /// ホストを登録する
        /// </summary>
        /// <param name="host">ホスト</param>
        void RegisterHost(host host);

        /// <summary>
        /// ホストの登録解除
        /// </summary>
        /// <param name="ip">IP</param>
        void UnregisterHost(string ip);

        /// <summary>
        /// 対戦状態を設定する
        /// </summary>
        /// <param name="ip">IP</param>
        /// <param name="isFighting">対戦状態</param>
        void SetFighting(string ip, bool isFighting);

        /// <summary>
        /// チャットする
        /// </summary>
        /// <param name="contents">内容</param>
        [Obsolete]
        void DoChat(string contents);

        /// <summary>
        /// チャットする
        /// </summary>
        /// <param name="name">名前</param>
        /// <param name="contents">内容</param>
        void DoChat(string name, string contents);
        #endregion

        #region 大会
        /// <summary>
        /// 大会情報の取得
        /// </summary>
        /// <param name="tournamentNo">大会番号</param>
        /// <param name="entryName">エントリー名</param>
        /// <param name="lastTournamentTime">最後に取得した日時</param>
        /// <param name="lastTournamentChatTime">最後に取得したチャット日時</param>
        void GetTournamentData(int tournamentNo, string entryName, DateTime lastTournamentTime, DateTime lastTournamentChatTime);

        /// <summary>
        /// 大会登録
        /// </summary>
        /// <param name="userCount">人数</param>
        /// <param name="type">大会の種類</param>
        /// <param name="rank">ランク</param>
        /// <param name="comment">コメント</param>
        void RegisterTournament(int userCount, TournamentTypes type, string rank, string comment);

        /// <summary>
        /// 大会登録解除
        /// </summary>
        void UnregisterTournament();

        /// <summary>
        /// アナウンスを設定
        /// </summary>
        /// <param name="tournamentNo">大会番号</param>
        /// <param name="announces">アナウンス</param>
        void SetTournamentAnnounces(int tournamentNo, Collection<string> announces);

        /// <summary>
        /// 大会にエントリー
        /// </summary>
        /// <param name="tournamentNo">大会番号</param>
        /// <param name="entryName">エントリー名</param>
        /// <param name="ip">IP</param>
        /// <param name="port">ポート</param>
        /// <returns>自分のID(失敗時null)</returns>
        /// <exception cref="CommunicationFailedException">通信失敗時に発生します</exception>
        id EntryTournament(int tournamentNo, string entryName, string ip, int port);

        /// <summary>
        /// 観戦で入る
        /// </summary>
        /// <param name="tournamentNo">大会番号</param>
        /// <param name="entryName">エントリー名</param>
        /// <returns>自分のID(失敗時null)</returns>
        /// <exception cref="CommunicationFailedException">通信失敗時に発生します</exception>
        id GuestEntryTournament(int tournamentNo, string entryName);

        /// <summary>
        /// エントリー取り消し
        /// </summary>
        /// <param name="tournamentNo">大会番号</param>
        void CancelTournamentEntry(int tournamentNo);

        /// <summary>
        /// リタイアする
        /// </summary>
        /// <param name="tournamentNo">大会番号</param>
        void RetireTournament(int tournamentNo);

        /// <summary>
        /// ホスト中かどうかを設定する
        /// </summary>
        /// <param name="tournamentNo">大会番号</param>
        /// <param name="isWaiting">ホスト中かどうか</param>
        void SetTournamentWaiting(int tournamentNo, bool isWaiting);

        /// <summary>
        /// 対戦中かどうかを設定する
        /// </summary>
        /// <param name="tournamentNo">大会番号</param>
        /// <param name="isFighting">対戦中かどうか</param>
        void SetTournamentFighting(int tournamentNo, bool isFighting);

        /// <summary>
        /// 結果報告をする
        /// </summary>
        /// <param name="tournamentNo">大会番号</param>
        /// <param name="results">結果</param>
        void SetTournamentResults(int tournamentNo, int[] results);

        /// <summary>
        /// チャットする(大会)
        /// </summary>
        /// <param name="tournamentNo">大会番号</param>
        /// <param name="name">名前</param>
        /// <param name="contents">内容</param>
        void DoTournamentChat(int tournamentNo, string name, string contents);
        #endregion

        #region 大会(運営)
        /// <summary>
        /// 運営を追加
        /// </summary>
        /// <param name="tournamentNo">大会番号</param>
        /// <param name="id">追加するID</param>
        void AddTournamentManager(int tournamentNo, string id);

        /// <summary>
        /// 運営から除く
        /// </summary>
        /// <param name="tournamentNo">大会番号</param>
        /// <param name="id">除くID</param>
        void RemoveTournamentManager(int tournamentNo, string id);

        /// <summary>
        /// ダミーの参加者を追加
        /// </summary>
        /// <param name="tournamentNo">大会番号</param>
        void AddTournamentDummyPlayer(int tournamentNo);

        /// <summary>
        /// 観戦を追加
        /// </summary>
        /// <param name="tournamentNo">大会番号</param>
        /// <param name="id">除くID</param>
        void AddTournamentSpectator(int tournamentNo, string id);

        /// <summary>
        /// 観戦から除く
        /// </summary>
        /// <param name="tournamentNo">大会番号</param>
        /// <param name="id">除くID</param>
        void RemoveTournamentSpectator(int tournamentNo, string id);

        /// <summary>
        /// 人数再設定
        /// </summary>
        /// <param name="tournamentNo">大会番号</param>
        /// <param name="userCount">人数</param>
        void SetTournamentUserCount(int tournamentNo, int userCount);

        /// <summary>
        /// トーナメント開始
        /// </summary>
        /// <param name="tournamentNo">大会番号</param>
        /// <param name="minCount">開始できる最少人数</param>
        /// <param name="maxCount">開始できる最大人数</param>
        /// <param name="shuffle">順番をシャッフルするかどうか</param>
        void StartTournament(int tournamentNo, int minCount, int maxCount, bool shuffle);

        /// <summary>
        /// 参加を取り消させる
        /// </summary>
        /// <param name="tournamentNo">大会番号</param>
        /// <param name="id">ID</param>
        void CancelTournamentEntryByManager(int tournamentNo, string id);

        /// <summary>
        /// リタイアさせる
        /// </summary>
        /// <param name="tournamentNo">大会番号</param>
        /// <param name="id">ID</param>
        void RetireByManager(int tournamentNo, string id);

        /// <summary>
        /// (参加者を)キックする
        /// </summary>
        /// <param name="tournamentNo">大会番号</param>
        /// <param name="id">キックするID</param>
        void KickTournamentUser(int tournamentNo, string id);

        /// <summary>
        /// 結果報告(運営)
        /// </summary>
        /// <param name="tournamentNo">大会番号</param>
        /// <param name="id">修正対象のID</param>
        /// <param name="results">結果</param>
        void SetTournamentResultsByManager(int tournamentNo, string id, int[] results);
        #endregion

        #region 自動マッチング
        /// <summary>
        /// 自動マッチング登録
        /// </summary>
        /// <param name="user">登録情報</param>
        void RegisterMatching(tencoUser user);

        /// <summary>
        /// 自動マッチング登録解除
        /// </summary>
        void UnregisterMatching();

        /// <summary>
        /// 自動マッチング 対戦履歴追加
        /// </summary>
        /// <param name="oponent">対戦相手のID</param>
        void AddMatchingHistory(id oponent);

        /// <summary>
        /// 自動マッチング 準備OK
        /// </summary>
        /// <param name="oponent">対戦相手のID</param>
        void SetPrepared(id oponent);

        /// <summary>
        /// 自動マッチング スキップ
        /// </summary>
        /// <param name="oponent">対戦相手のID</param>
        void SkipMatching(id oponent);

        /// <summary>
        /// マッチング結果取得
        /// </summary>
        void GetMatchingResult();
        #endregion

        /// <summary>
        /// ローカルのホスト情報クリア
        /// </summary>
        void ClearHostCache();

        /// <summary>
        /// ローカルチャット履歴のクリア
        /// </summary>
        void ClearChatCache();
    }
}
