using System;
using System.Collections.ObjectModel;
using HisoutenSupportTools.AddressUpdater.Lib.AddressService;
using HisoutenSupportTools.AddressUpdater.Lib.Model.Tournament;

namespace HisoutenSupportTools.AddressUpdater.Lib.Network
{
    /// <summary>
    /// サーバーインターフェース
    /// </summary>
    public interface IServer : IDisposable
    {
        #region プロパティ
        /// <summary>サーバー名の取得</summary>
        string Name { get; }
        /// <summary>サーバーURIの取得</summary>
        string Uri { get; }
        /// <summary>タイムアウト時間の取得・設定</summary>
        int Timeout { get; set; }
        #endregion

        #region イベント
        /// <summary>接続人数・アナウンス・一覧・チャット受信時に発生します</summary>
        event EventHandler<EventArgs<allData>> AllReceived;
        /// <summary>大会情報受信時に発生します</summary>
        event EventHandler<EventArgs<tournament>> TournamentDataReceived;
        /// <summary>自動マッチング情報受信時に発生します</summary>
        event EventHandler<EventArgs<matchingResult>> MatchingResultReceived;
        /// <summary>エラーが継続的に起きた際に発生します</summary>
        event EventHandler ConsecutiveErrorHappened;
        #endregion

        #region サーバー情報
        /// <summary>
        /// サーバーバージョンの取得
        /// </summary>
        /// <returns>サーバー設定</returns>
        /// <exception cref="CommunicationFailedException">通信失敗時に発生します</exception>
        Version GetVersion();

        /// <summary>
        /// サーバー設定の取得
        /// </summary>
        /// <returns>サーバー設定</returns>
        /// <exception cref="CommunicationFailedException">通信失敗時に発生します</exception>
        serverSetting GetSetting();
        #endregion

        #region 一般
        /// <summary>
        /// 受信
        /// </summary>
        /// <param name="getHost">ホスト情報を受信するかどうか</param>
        /// <param name="getChat">チャット情報を受信するかどうか</param>
        void Receive(bool getHost, bool getChat);

        /// <summary>
        /// 受信
        /// </summary>
        /// <param name="getHost">ホスト情報を受信するかどうか</param>
        /// <param name="getChat">チャット情報を受信するかどうか</param>
        /// <param name="getTournament">大会情報を受信するかどうか</param>
        void Receive(bool getHost, bool getChat, bool getTournament);

        /// <summary>
        /// 退室
        /// </summary>
        void Leave();

        /// <summary>
        /// ホスト登録
        /// </summary>
        /// <param name="host">ホスト</param>
        /// <exception cref="CommunicationFailedException">通信失敗時に発生します</exception>
        void RegisterHost(host host);

        /// <summary>
        /// ホスト登録解除
        /// </summary>
        /// <param name="ip">IP</param>
        void UnregisterHost(string ip);

        /// <summary>
        /// 対戦状態設定
        /// </summary>
        /// <param name="ip">IP</param>
        /// <param name="isFighting">対戦中かどうか</param>
        void SetFighting(string ip, bool isFighting);

        /// <summary>
        /// チャットする
        /// </summary>
        /// <param name="name">名前</param>
        /// <param name="contents">内容</param>
        void DoChat(string name, string contents);

        /// <summary>
        /// 最後に取得したHost日時のリセット
        /// </summary>
        void ResetLastHostTime();

        /// <summary>
        /// 最後に取得したChatの日時のリセット
        /// </summary>
        void ResetLastChatTime();
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
        id EntryTournament(int tournamentNo, string entryName, string ip, int port);

        /// <summary>
        /// 観戦で入る
        /// </summary>
        /// <param name="tournamentNo">大会番号</param>
        /// <param name="entryName">エントリー名</param>
        /// <returns>自分のID(失敗時null)</returns>
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

        #region 管理
        /// <summary>
        /// アナウンスを設定する
        /// </summary>
        /// <param name="keyword">管理者キーワード</param>
        /// <param name="announces">アナウンス</param>
        void SetAnnounces(string keyword, Collection<string> announces);

        /// <summary>
        /// アナウンスする
        /// </summary>
        /// <param name="keyword">管理者キーワード</param>
        /// <param name="contents">内容</param>
        [Obsolete]
        void Announce(string keyword, string contents);

        /// <summary>
        /// アナウンス削除
        /// </summary>
        /// <param name="keyword">管理者キーワード</param>
        /// <param name="no">No</param>
        [Obsolete]
        void RemoveAnnounce(string keyword, int no);

        /// <summary>
        /// アナウンスをクリアする
        /// </summary>
        /// <param name="keyword">管理者キーワード</param>
        [Obsolete]
        void ClearAnnounce(string keyword);

        /// <summary>
        /// 強制ホスト登録
        /// </summary>
        /// <param name="keyword">管理者キーワード</param>
        /// <param name="host">ホスト</param>
        void ForceRegisterHost(string keyword, host host);

        /// <summary>
        /// 強制ホスト登録解除
        /// </summary>
        /// <param name="keyword">管理者キーワード</param>
        /// <param name="no">No</param>
        /// <param name="ip">IP</param>
        void ForceUnregisterHost(string keyword, int no, string ip);

        /// <summary>
        /// 強制大会削除
        /// </summary>
        /// <param name="keyword">管理者キーワード</param>
        /// <param name="tournamentNo">大会番号</param>
        void ForceUnregisterTournament(string keyword, int tournamentNo);

        /// <summary>
        /// 強制対戦状態設定
        /// </summary>
        /// <param name="keyword">管理者キーワード</param>
        /// <param name="no">No</param>
        /// <param name="ip">IP</param>
        /// <param name="isFighting">対戦中かどうか</param>
        void ForceSetFighting(string keyword, int no, string ip, bool isFighting);

        /// <summary>
        /// ホストをクリアする
        /// </summary>
        /// <param name="keyword">管理者キーワード</param>
        void ClearHosts(string keyword);

        /// <summary>
        /// 大会をクリアする
        /// </summary>
        /// <param name="keyword">管理者キーワード</param>
        void ClearTournaments(string keyword);

        /// <summary>
        /// 管理者チャットする
        /// </summary>
        /// <param name="keyword">管理者キーワード</param>
        /// <param name="contents">内容</param>
        void AdminChat(string keyword, string contents);

        /// <summary>
        /// チャットをクリアする
        /// </summary>
        /// <param name="keyword">管理者キーワード</param>
        void ClearChat(string keyword);

        /// <summary>
        /// 指定IDのIPを取得する
        /// </summary>
        /// <param name="keyword">管理者キーワード</param>
        /// <param name="id">ID</param>
        /// <returns>IP</returns>
        string GetIpById(string keyword, string id);

        /// <summary>
        /// リモート管理を許可する
        /// </summary>
        /// <param name="keyword">管理者キーワード</param>
        void AllowRemoteAdmin(string keyword);

        /// <summary>
        /// リモート管理を禁止する
        /// </summary>
        /// <param name="keyword">管理者キーワード</param>
        void DenyRemoteAdmin(string keyword);
        #endregion
    }
}
