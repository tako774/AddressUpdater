using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Web.Services.Protocols;
using HisoutenSupportTools.AddressUpdater.Lib.AddressService;
using HisoutenSupportTools.AddressUpdater.Lib.Model.Tournament;

namespace HisoutenSupportTools.AddressUpdater.Lib.Network
{
    /// <summary>
    /// 通信
    /// </summary>
    public class Server : IServer, IDisposable
    {
        /// <summary>バージョン9.1(サーバーのバージョン ホスト登録用)</summary>
        private static readonly Version VERSION9_1 = new Version(9, 1);
        /// <summary>バージョン9.0(サーバーのバージョン ホスト登録用)</summary>
        private static readonly Version VERSION9 = new Version(9, 0);
        /// <summary>バージョン8.0(サーバーのバージョン チャット送信用)</summary>
        private static readonly Version VERSION8 = new Version(8, 0);
        /// <summary>連続して起きるエラーの許容値</summary>
        private const int ERROR_MAX = 10;

        #region フィールド
        /// <summary>サーバー名</summary>
        public readonly string _name;
        /// <summary>非同期通信ID</summary>
        private int _asyncId = 0;
        /// <summary>サーバー</summary>
        private AddressService.AddressService _addressService;
        /// <summary>連続エラー回数</summary>
        private int _errorCount = 0;
        /// <summary>サーバー設定キャッシュ</summary>
        private serverSetting _serverSettingCache;
        /// <summary>サーバーバージョンキャッシュ</summary>
        private Version _serverVersionCache;
        /// <summary>最後に受信したホストの時刻</summary>
        private DateTime _lastHostTime = DateTime.MinValue;
        /// <summary>最後に受信したチャットの時刻</summary>
        private DateTime _lastChatTime = DateTime.MinValue;
        /// <summary>最後に受信した大会の時刻</summary>
        private DateTime _lastTournamentTime = DateTime.MinValue;
        #endregion

        #region プロパティ
        /// <summary>サーバー名の取得</summary>
        public string Name { get { return _name; } }
        /// <summary>サーバーURIの取得</summary>
        public string Uri { get { return _addressService.Url; } }
        /// <summary>タイムアウト時間（単位：ミリ秒）</summary>
        public int Timeout
        {
            get { return _addressService.Timeout; }
            set { _addressService.Timeout = value; }
        }

        /// <summary>非同期通信IDの取得</summary>
        private int AsyncId
        {
            get
            {
                lock (this)
                {
                    _asyncId += 1;
                    if (999 < _asyncId) _asyncId = 1;
                    return _asyncId;
                }
            }
        }
        #endregion

        #region イベント
        /// <summary>接続人数・アナウンス・一覧・チャット受信時に発生します</summary>
        public event EventHandler<EventArgs<allData>> AllReceived;
        /// <summary>大会情報受信時に発生します</summary>
        public event EventHandler<EventArgs<tournament>> TournamentDataReceived;
        /// <summary>自動マッチング情報受信時に発生します</summary>
        public event EventHandler<EventArgs<matchingResult>> MatchingResultReceived;
        /// <summary>エラーが継続的に起きた際に発生します</summary>
        public event EventHandler ConsecutiveErrorHappened;
        #endregion

        /// <summary>
        /// インスタンスの生成
        /// </summary>
        /// <param name="name">サーバー名</param>
        /// <param name="uri">サーバーURI</param>
        public Server(string name, string uri)
        {
            _name = name;
            _addressService = new AddressService.AddressService();
            _addressService.Url = uri;
            _addressService.Proxy = null;
            _addressService.getAllDataCompleted += new getAllDataCompletedEventHandler(_addressService_getAllDataCompleted);
            _addressService.getAllDataExCompleted += new getAllDataExCompletedEventHandler(_addressService_getAllDataExCompleted);
            _addressService.getTournamentDataCompleted += new getTournamentDataCompletedEventHandler(_addressService_getTournamentDataCompleted);
            _addressService.getMatchingResultCompleted += new getMatchingResultCompletedEventHandler(_addressService_getMatchingResultCompleted);
        }

        #region サーバー情報
        /// <summary>
        /// サーバーバージョン取得
        /// </summary>
        /// <returns>サーバーバージョン</returns>
        /// <exception cref="CommunicationFailedException">通信失敗時に発生します</exception>
        public Version GetVersion()
        {
            if (_serverVersionCache != null)
                return _serverVersionCache;

            return ErrorCountFunc<Version>(new Func<Version>(delegate
                {
                    _serverVersionCache = new Version(_addressService.getServerVersion());
                    return _serverVersionCache;
                }));
        }

        /// <summary>
        /// サーバー設定取得
        /// </summary>
        /// <returns>サーバー設定</returns>
        /// <exception cref="CommunicationFailedException">通信失敗時に発生します</exception>
        public serverSetting GetSetting()
        {
            if (_serverSettingCache != null)
                return _serverSettingCache;

            return ErrorCountFunc<serverSetting>(new Func<serverSetting>(delegate
                {
                    _serverSettingCache = _addressService.getServerSetting();
                    return _serverSettingCache;
                }));
        }
        #endregion

        #region 一般
        /// <summary>
        /// 受信
        /// </summary>
        /// <param name="getHost">ホスト情報を受信するかどうか</param>
        /// <param name="getChat">チャット情報を受信するかどうか</param>
        [Obsolete]
        public void Receive(bool getHost, bool getChat)
        {
            _addressService.getAllDataAsync(getHost, getChat, _lastHostTime, true, _lastChatTime, true, AsyncId);
        }

        /// <summary>
        /// 受信
        /// </summary>
        /// <param name="getHost">ホスト情報を受信するかどうか</param>
        /// <param name="getChat">チャット情報を受信するかどうか</param>
        /// <param name="getTournament">大会情報を受信するかどうか</param>
        public void Receive(bool getHost, bool getChat, bool getTournament)
        {
            try
            {
                if (VERSION8 <= GetVersion())
                    _addressService.getAllDataExAsync(getHost, _lastHostTime, true, getChat, _lastChatTime, true, getTournament, _lastTournamentTime, true);
                else
                    _addressService.getAllDataAsync(getHost, getChat, _lastHostTime, true, _lastChatTime, true, AsyncId);
            }
            catch (CommunicationFailedException) { }
        }
        /// <summary>
        /// getAllData完了時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _addressService_getAllDataCompleted(object sender, getAllDataCompletedEventArgs e)
        {
            if (AllReceived == null)
                return;

            ErrorCountAction(new Action(delegate
                {
                    var data = e.Result;

                    if (e.Result.GetHost && e.Result.Hosts != null && 0 < e.Result.Hosts.Length)
                    {
                        var lastHostTime = e.Result.Hosts[0].lastUpdate;
                        foreach (var host in e.Result.Hosts)
                        {
                            if (lastHostTime < host.lastUpdate)
                                lastHostTime = host.lastUpdate;
                        }
                        _lastHostTime = lastHostTime;
                    }
                    if (e.Result.GetChat && e.Result.Chats != null && 0 < e.Result.Chats.Length)
                    {
                        _lastChatTime = e.Result.Chats[e.Result.Chats.Length - 1].Time;
                    }

                    if (AllReceived != null)
                        AllReceived(this, new EventArgs<allData>(data));
                }));
        }

        /// <summary>
        /// getAllDataEx完了時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _addressService_getAllDataExCompleted(object sender, getAllDataExCompletedEventArgs e)
        {
            if (AllReceived == null)
                return;

            ErrorCountAction(new Action(delegate
                {
                    var data = e.Result;

                    if (e.Result.GetHost && e.Result.Hosts != null && 0 < e.Result.Hosts.Length)
                    {
                        var lastHostTime = e.Result.Hosts[0].lastUpdate;
                        foreach (var host in e.Result.Hosts)
                        {
                            if (lastHostTime < host.lastUpdate)
                                lastHostTime = host.lastUpdate;
                        }
                        _lastHostTime = lastHostTime;
                    }
                    if (e.Result.GetTournament && e.Result.Tournaments != null && 0 < e.Result.Tournaments.Length)
                    {
                        var lastTournamentTime = e.Result.Tournaments[0].LastUpdate;
                        foreach (var tournament in e.Result.Tournaments)
                        {
                            if (lastTournamentTime < tournament.LastUpdate)
                                lastTournamentTime = tournament.LastUpdate;
                        }
                        _lastTournamentTime = lastTournamentTime;
                    }
                    if (e.Result.GetChat && e.Result.Chats != null && 0 < e.Result.Chats.Length)
                    {
                        _lastChatTime = e.Result.Chats[e.Result.Chats.Length - 1].Time;
                    }

                    if (AllReceived != null)
                        AllReceived(this, new EventArgs<allData>(data));
                }));
        }

        /// <summary>
        /// 退室
        /// </summary>
        public void Leave()
        {
            ErrorCountAction(new Action(delegate
                {
                    _addressService.leaveAsync(AsyncId);
                }));
        }

        /// <summary>
        /// ホスト登録
        /// </summary>
        /// <param name="host">ホスト</param>
        public void RegisterHost(host host)
        {
            ErrorCountAction(new Action(delegate
                {
                    var h = new host()
                    {
                        Ip = host.Ip,
                        Port = host.Port,
                        Rank = host.Rank,
                        Comment = host.Comment.Replace("\r", string.Empty).Replace("\n", string.Empty),
                    };

                    if (VERSION9 <= GetVersion())
                        _addressService.registerHostInformationAsync(h.Ip, h.Port, h.Rank, h.Comment, AsyncId);
                    else
                        _addressService.registerHostExAsync(h, AsyncId);
                }));
        }

        /// <summary>
        /// ホスト登録解除
        /// </summary>
        /// <param name="ip">IP</param>
        public void UnregisterHost(string ip)
        {
            ErrorCountAction(new Action(delegate
                {
                    _addressService.unregisterHost(ip);
                }));
        }

        /// <summary>
        /// 対戦状態設定
        /// </summary>
        /// <param name="ip">IP</param>
        /// <param name="isFighting">対戦中かどうか</param>
        public void SetFighting(string ip, bool isFighting)
        {
            ErrorCountAction(new Action(delegate
                {
                    _addressService.setFighting(ip, isFighting);
                }));
        }

        /// <summary>
        /// チャットする
        /// </summary>
        /// <param name="name">名前</param>
        /// <param name="contents">内容</param>
        public void DoChat(string name, string contents)
        {
            try
            {
                if (GetVersion() < VERSION8)
                    _addressService.addChatAsync(name + contents, AsyncId);
                else
                    _addressService.addChatExAsync(null, name + contents, AsyncId);
            }
            catch (CommunicationFailedException) { }
        }

        /// <summary>
        /// 最後に取得したHost日時のリセット
        /// </summary>
        public void ResetLastHostTime()
        {
            _lastHostTime = DateTime.MinValue;
        }

        /// <summary>
        /// 最後に取得したChatの日時のリセット
        /// </summary>
        public void ResetLastChatTime()
        {
            _lastChatTime = DateTime.MinValue;
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
            try
            {
                if (GetVersion() < VERSION8)
                    return;
                _addressService.getTournamentDataAsync(tournamentNo, entryName, lastTournamentTime, true, lastTournamentChatTime, true, AsyncId);
            }
            catch (CommunicationFailedException) { }
        }

        /// <summary>
        /// getTournamentData完了時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _addressService_getTournamentDataCompleted(object sender, getTournamentDataCompletedEventArgs e)
        {
            ErrorCountAction(new Action(delegate
                {
                    if (e.Result == null)
                        return;

                    var data = e.Result;

                    if (TournamentDataReceived != null)
                        TournamentDataReceived(this, new EventArgs<tournament>(data));
                }));
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
            try
            {
                if (GetVersion() < VERSION8)
                    return;
                ErrorCountAction(new Action(delegate
                    {
                        _addressService.registerTournament(userCount, (int)type, rank, comment);
                    }));
            }
            catch (CommunicationFailedException) { }
        }

        /// <summary>
        /// 大会登録解除
        /// </summary>
        public void UnregisterTournament()
        {
            try
            {
                if (GetVersion() < VERSION8)
                    return;
                ErrorCountAction(new Action(delegate
                    {
                        _addressService.unregisterTournamentAsync(AsyncId);
                    }));
            }
            catch (CommunicationFailedException) { }
        }

        /// <summary>
        /// アナウンスを設定
        /// </summary>
        /// <param name="tournamentNo">大会番号</param>
        /// <param name="announces">アナウンス</param>
        public void SetTournamentAnnounces(int tournamentNo, Collection<string> announces)
        {
            try
            {
                if (GetVersion() < VERSION8)
                    return;
                ErrorCountAction(new Action(delegate
                {
                    if (announces == null)
                        _addressService.setTournamentAnnouncesAsync(tournamentNo, null, AsyncId);
                    else
                    {
                        string[] announceArray = new string[announces.Count];
                        for (var i = 0; i < announces.Count; i++)
                            announceArray[i] = announces[i];
                        _addressService.setTournamentAnnouncesAsync(tournamentNo, announceArray, AsyncId);
                    }
                }));
            }
            catch (CommunicationFailedException) { }
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
            if (GetVersion() < VERSION8)
                return null;
            return ErrorCountFunc<id>(new Func<id>(delegate
                {
                    return _addressService.entryTournament(tournamentNo, entryName, ip, port);
                }));
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
            if (GetVersion() < VERSION8)
                return null;
            return ErrorCountFunc<id>(new Func<id>(delegate
                {
                    return _addressService.guestEntryTournament(tournamentNo, entryName);
                }));
        }

        /// <summary>
        /// エントリー取り消し
        /// </summary>
        /// <param name="tournamentNo">大会番号</param>
        public void CancelTournamentEntry(int tournamentNo)
        {
            try
            {
                if (GetVersion() < VERSION8)
                    return;
                ErrorCountAction(new Action(delegate
                    {
                        _addressService.cancelTournamentEntryAsync(tournamentNo, AsyncId);
                    }));
            }
            catch (CommunicationFailedException) { }
        }

        /// <summary>
        /// リタイアする
        /// </summary>
        /// <param name="tournamentNo">大会番号</param>
        public void RetireTournament(int tournamentNo)
        {
            try
            {
                if (GetVersion() < VERSION8)
                    return;
                ErrorCountAction(new Action(delegate
                    {
                        _addressService.retireTournamentAsync(tournamentNo, AsyncId);
                    }));
            }
            catch (CommunicationFailedException) { }
        }

        /// <summary>
        /// ホスト中かどうかを設定する
        /// </summary>
        /// <param name="tournamentNo">大会番号</param>
        /// <param name="isWaiting">ホスト中かどうか</param>
        public void SetTournamentWaiting(int tournamentNo, bool isWaiting)
        {
            try
            {
                if (GetVersion() < VERSION8)
                    return;
                ErrorCountAction(new Action(delegate
                    {
                        _addressService.setTournamentPlayerWaitingAsync(tournamentNo, isWaiting, AsyncId);
                    }));
            }
            catch (CommunicationFailedException) { }
        }

        /// <summary>
        /// 対戦中かどうかを設定する
        /// </summary>
        /// <param name="tournamentNo">大会番号</param>
        /// <param name="isFighting">対戦中かどうか</param>
        public void SetTournamentFighting(int tournamentNo, bool isFighting)
        {
            try
            {
                if (GetVersion() < VERSION8)
                    return;
                ErrorCountAction(new Action(delegate
                    {
                        _addressService.setTournamentPlayerFightingAsync(tournamentNo, isFighting, AsyncId);
                    }));
            }
            catch (CommunicationFailedException) { }
        }

        /// <summary>
        /// 結果報告をする
        /// </summary>
        /// <param name="tournamentNo">大会番号</param>
        /// <param name="results">結果</param>
        public void SetTournamentResults(int tournamentNo, int[] results)
        {
            try
            {
                if (GetVersion() < VERSION8)
                    return;
                ErrorCountAction(new Action(delegate
                    {
                        _addressService.setTournamentResultsAsync(tournamentNo, results, AsyncId);
                    }));
            }
            catch (CommunicationFailedException) { }
        }

        /// <summary>
        /// チャットする(大会)
        /// </summary>
        /// <param name="tournamentNo">大会番号</param>
        /// <param name="name">名前</param>
        /// <param name="contents">内容</param>
        public void DoTournamentChat(int tournamentNo, string name, string contents)
        {
            try
            {
                if (GetVersion() < VERSION8)
                    return;
                ErrorCountAction(new Action(delegate
                    {
                        _addressService.addTournamentChatAsync(tournamentNo, name, contents, AsyncId);
                    }));
            }
            catch (CommunicationFailedException) { }
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
            try
            {
                if (GetVersion() < VERSION8)
                    return;
                ErrorCountAction(new Action(delegate
                    {
                        _addressService.addTournamentManagerAsync(tournamentNo, id, AsyncId);
                    }));
            }
            catch (CommunicationFailedException) { }
        }

        /// <summary>
        /// 運営から除く
        /// </summary>
        /// <param name="tournamentNo">大会番号</param>
        /// <param name="id">除くID</param>
        public void RemoveTournamentManager(int tournamentNo, string id)
        {
            try
            {
                if (GetVersion() < VERSION8)
                    return;
                ErrorCountAction(new Action(delegate
                    {
                        _addressService.removeTournamentManagerAsync(tournamentNo, id, AsyncId);
                    }));
            }
            catch (CommunicationFailedException) { }
        }

        /// <summary>
        /// ダミーの参加者を追加
        /// </summary>
        /// <param name="tournamentNo">大会番号</param>
        public void AddTournamentDummyPlayer(int tournamentNo)
        {
            try
            {
                if (GetVersion() < VERSION8)
                    return;
                ErrorCountAction(new Action(delegate
                    {
                        _addressService.addTournamentDummyPlayerAsync(tournamentNo, AsyncId);
                    }));
            }
            catch (CommunicationFailedException) { }
        }

        /// <summary>
        /// 観戦を追加
        /// </summary>
        /// <param name="tournamentNo">大会番号</param>
        /// <param name="id">除くID</param>
        public void AddTournamentSpectator(int tournamentNo, string id)
        {
            try
            {
                if (GetVersion() < VERSION8)
                    return;
                ErrorCountAction(new Action(delegate
                    {
                        _addressService.addTournamentSpectatorAsync(tournamentNo, id, AsyncId);
                    }));
            }
            catch (CommunicationFailedException) { }
        }

        /// <summary>
        /// 観戦から除く
        /// </summary>
        /// <param name="tournamentNo">大会番号</param>
        /// <param name="id">除くID</param>
        public void RemoveTournamentSpectator(int tournamentNo, string id)
        {
            try
            {
                if (GetVersion() < VERSION8)
                    return;
                ErrorCountAction(new Action(delegate
                    {
                        _addressService.removeTournamentSpectatorAsync(tournamentNo, id, AsyncId);
                    }));
            }
            catch (CommunicationFailedException) { }
        }

        /// <summary>
        /// 人数再設定
        /// </summary>
        /// <param name="tournamentNo">大会番号</param>
        /// <param name="userCount">人数</param>
        public void SetTournamentUserCount(int tournamentNo, int userCount)
        {
            try
            {
                if (GetVersion() < VERSION8)
                    return;
                ErrorCountAction(new Action(delegate
                    {
                        _addressService.setTournamentUserCountAsync(tournamentNo, userCount, AsyncId);
                    }));
            }
            catch (CommunicationFailedException) { }
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
            try
            {
                if (GetVersion() < VERSION8)
                    return;
                ErrorCountAction(new Action(delegate
                    {
                        _addressService.startTournamentAsync(tournamentNo, minCount, maxCount, shuffle, AsyncId);
                    }));
            }
            catch (CommunicationFailedException) { }
        }

        /// <summary>
        /// 参加を取り消させる
        /// </summary>
        /// <param name="tournamentNo">大会番号</param>
        /// <param name="id">ID</param>
        public void CancelTournamentEntryByManager(int tournamentNo, string id)
        {
            try
            {
                if (GetVersion() < VERSION8)
                    return;
                ErrorCountAction(new Action(delegate
                    {
                        _addressService.cancelTournamentEntryByManagerAsync(tournamentNo, id, AsyncId);
                    }));
            }
            catch (CommunicationFailedException) { }
        }

        /// <summary>
        /// リタイアさせる
        /// </summary>
        /// <param name="tournamentNo">大会番号</param>
        /// <param name="id">ID</param>
        public void RetireByManager(int tournamentNo, string id)
        {
            try
            {
                if (GetVersion() < VERSION8)
                    return;
                ErrorCountAction(new Action(delegate
                    {
                        _addressService.retireTournamentByManagerAsync(tournamentNo, id, AsyncId);
                    }));
            }
            catch (CommunicationFailedException) { }
        }

        /// <summary>
        /// (参加者を)キックする
        /// </summary>
        /// <param name="tournamentNo">大会番号</param>
        /// <param name="id">キックするID</param>
        public void KickTournamentUser(int tournamentNo, string id)
        {
            try
            {
                if (GetVersion() < VERSION8)
                    return;
                ErrorCountAction(new Action(delegate
                    {
                        _addressService.kickTournamentUserAsync(tournamentNo, id, AsyncId);
                    }));
            }
            catch (CommunicationFailedException) { }
        }

        /// <summary>
        /// 結果報告(運営)
        /// </summary>
        /// <param name="tournamentNo">大会番号</param>
        /// <param name="id">修正対象のID</param>
        /// <param name="results">結果</param>
        public void SetTournamentResultsByManager(int tournamentNo, string id, int[] results)
        {
            try
            {
                if (GetVersion() < VERSION8)
                    return;
                ErrorCountAction(new Action(delegate
                    {
                        _addressService.setTournamentResultsByManagerAsync(tournamentNo, id, results, AsyncId);
                    }));
            }
            catch (CommunicationFailedException) { }
        }
        #endregion

        #region 自動マッチング
        /// <summary>
        /// 自動マッチング登録
        /// </summary>
        /// <param name="user">登録情報</param>
        public void RegisterMatching(tencoUser user)
        {
            try
            {
                if (GetVersion() < VERSION9_1)
                    return;
                ErrorCountAction(new Action(delegate
                {
                    _addressService.registerMatchingAsync(user, AsyncId);
                }));
            }
            catch (CommunicationFailedException) { }
        }

        /// <summary>
        /// 自動マッチング登録解除
        /// </summary>
        public void UnregisterMatching()
        {
            try
            {
                if (GetVersion() < VERSION9_1)
                    return;
                ErrorCountAction(new Action(delegate
                {
                    _addressService.unregisterMatchingAsync(AsyncId);
                }));
            }
            catch (CommunicationFailedException) { }
        }

        /// <summary>
        /// 自動マッチング 対戦履歴追加
        /// </summary>
        /// <param name="oponent">対戦相手のID</param>
        public void AddMatchingHistory(id oponent)
        {
            try
            {
                if (GetVersion() < VERSION9_1)
                    return;
                ErrorCountAction(new Action(delegate
                {
                    _addressService.addMatchingHistoryAsync(oponent, AsyncId);
                }));
            }
            catch (CommunicationFailedException) { }
        }

        /// <summary>
        /// 自動マッチング 準備OK
        /// </summary>
        /// <param name="oponent">対戦相手のID</param>
        public void SetPrepared(id oponent)
        {
            try
            {
                if (GetVersion() < VERSION9_1)
                    return;
                ErrorCountAction(new Action(delegate
                {
                    _addressService.setPreparedAsync(oponent, AsyncId);
                }));
            }
            catch (CommunicationFailedException) { }
        }

        /// <summary>
        /// 自動マッチング スキップ
        /// </summary>
        /// <param name="oponent">対戦相手のID</param>
        public void SkipMatching(id oponent)
        {
            try
            {
                if (GetVersion() < VERSION9_1)
                    return;
                ErrorCountAction(new Action(delegate
                {
                    _addressService.skipMatchingAsync(oponent, AsyncId);
                }));
            }
            catch (CommunicationFailedException) { }
        }

        /// <summary>
        /// マッチング結果取得
        /// </summary>
        public void GetMatchingResult()
        {
            try
            {
                if (GetVersion() < VERSION9_1)
                    return;
                ErrorCountAction(new Action(delegate
                {
                    _addressService.getMatchingResultAsync(AsyncId);
                }));
            }
            catch (CommunicationFailedException) { }
        }

        void _addressService_getMatchingResultCompleted(object sender, getMatchingResultCompletedEventArgs e)
        {
            ErrorCountAction(new Action(delegate
            {
                if (MatchingResultReceived != null)
                    MatchingResultReceived(this, new EventArgs<matchingResult>(e.Result));
            }));
        }
        #endregion

        #region 管理者
        /// <summary>
        /// アナウンスを設定する
        /// </summary>
        /// <param name="keyword">管理者キーワード</param>
        /// <param name="announces">アナウンス</param>
        public void SetAnnounces(string keyword, Collection<string> announces)
        {
            ErrorCountAction(new Action(delegate
                {
                    if (announces == null || announces.Count <= 0)
                        _addressService.setAnnouncesAsync(keyword, null, AsyncId);
                    else
                    {
                        var announceArray = new string[announces.Count];
                        for (var i = 0; i < announces.Count; i++)
                            announceArray[i] = announces[i];
                        _addressService.setAnnouncesAsync(keyword, announceArray, AsyncId);
                    }
                }));
        }

        /// <summary>
        /// アナウンスする
        /// </summary>
        /// <param name="keyword">管理者キーワード</param>
        /// <param name="contents">内容</param>
        public void Announce(string keyword, string contents)
        {
            ErrorCountAction(new Action(delegate
                {
                    _addressService.addAnnounceAsync(keyword, contents, AsyncId);
                }));
        }

        /// <summary>
        /// アナウンス削除
        /// </summary>
        /// <param name="keyword">管理者キーワード</param>
        /// <param name="no">No</param>
        public void RemoveAnnounce(string keyword, int no)
        {
            ErrorCountAction(new Action(delegate
                {
                    _addressService.removeAnnounceAsync(keyword, no, AsyncId);
                }));
        }

        /// <summary>
        /// アナウンスをクリアする
        /// </summary>
        /// <param name="keyword">管理者キーワード</param>
        public void ClearAnnounce(string keyword)
        {
            ErrorCountAction(new Action(delegate
                {
                    _addressService.clearAnnounceAsync(keyword, AsyncId);
                }));
        }

        /// <summary>
        /// 強制ホスト登録
        /// </summary>
        /// <param name="keyword">管理者キーワード</param>
        /// <param name="host">ホスト</param>
        public void ForceRegisterHost(string keyword, host host)
        {
            ErrorCountAction(new Action(delegate
                {
                    _addressService.forceRegisterHostExAsync(keyword, host, AsyncId);
                }));
        }

        /// <summary>
        /// 強制ホスト登録解除
        /// </summary>
        /// <param name="keyword">管理者キーワード</param>
        /// <param name="no">No</param>
        /// <param name="ip">IP</param>
        public void ForceUnregisterHost(string keyword, int no, string ip)
        {
            ErrorCountAction(new Action(delegate
                {
                    _addressService.forceUnregisterHostAsync(keyword, no, ip, AsyncId);
                }));
        }

        /// <summary>
        /// 強制大会削除
        /// </summary>
        /// <param name="keyword">管理者キーワード</param>
        /// <param name="tournamentNo">大会番号</param>
        public void ForceUnregisterTournament(string keyword, int tournamentNo)
        {
            try
            {
                if (GetVersion() < VERSION8)
                    return;
                ErrorCountAction(new Action(delegate
                {
                    _addressService.forceUnregisterTournamentAsync(keyword, tournamentNo, AsyncId);
                }));
            }
            catch (CommunicationFailedException) { }
        }

        /// <summary>
        /// 強制対戦状態設定
        /// </summary>
        /// <param name="keyword">管理者キーワード</param>
        /// <param name="no">No</param>
        /// <param name="ip">IP</param>
        /// <param name="isFighting">対戦中かどうか</param>
        public void ForceSetFighting(string keyword, int no, string ip, bool isFighting)
        {
            ErrorCountAction(new Action(delegate
                {
                    _addressService.forceSetFightingAsync(keyword, no, ip, isFighting, AsyncId);
                }));
        }

        /// <summary>
        /// ホストをクリアする
        /// </summary>
        /// <param name="keyword">管理者キーワード</param>
        public void ClearHosts(string keyword)
        {
            ErrorCountAction(new Action(delegate
                {
                    _addressService.clearHostsAsync(keyword, AsyncId);
                }));
        }

        /// <summary>
        /// 大会をクリアする
        /// </summary>
        /// <param name="keyword">管理者キーワード</param>
        public void ClearTournaments(string keyword)
        {
            try
            {
                if (GetVersion() < VERSION8)
                    return;
                ErrorCountAction(new Action(delegate
                {
                    _addressService.clearTournamentsAsync(keyword, AsyncId);
                }));
            }
            catch (CommunicationFailedException) { }
        }

        /// <summary>
        /// 管理者チャットする
        /// </summary>
        /// <param name="keyword">管理者キーワード</param>
        /// <param name="contents">内容</param>
        public void AdminChat(string keyword, string contents)
        {
            ErrorCountAction(new Action(delegate
                {
                    _addressService.addAdminChatAsync(keyword, contents, AsyncId);
                }));
        }

        /// <summary>
        /// チャットをクリアする
        /// </summary>
        /// <param name="keyword">管理者キーワード</param>
        public void ClearChat(string keyword)
        {
            ErrorCountAction(new Action(delegate
                {
                    _addressService.clearChatAsync(keyword, AsyncId);
                }));
        }

        /// <summary>
        /// 指定IDのIPを取得する
        /// </summary>
        /// <param name="keyword">管理者キーワード</param>
        /// <param name="id">ID</param>
        /// <returns>IP</returns>
        /// <exception cref="CommunicationFailedException">通信失敗時に発生します</exception>
        public string GetIpById(string keyword, string id)
        {
            return ErrorCountFunc<string>(new Func<string>(delegate
                {
                    return _addressService.getAddressById(keyword, id);
                }));
        }

        /// <summary>
        /// リモート管理を許可する
        /// </summary>
        /// <param name="keyword">管理者キーワード</param>
        public void AllowRemoteAdmin(string keyword)
        {
            ErrorCountAction(new Action(delegate
                {
                    _addressService.setEnableRemoteAdminAsync(keyword, true);
                }));
        }

        /// <summary>
        /// リモート管理を禁止する
        /// </summary>
        /// <param name="keyword">管理者キーワード</param>
        public void DenyRemoteAdmin(string keyword)
        {
            ErrorCountAction(new Action(delegate
                {
                    _addressService.setEnableRemoteAdminAsync(keyword, false);
                }));
        }
        #endregion


        #region エラー回数制御処理
        /// <summary>
        /// エラー回数制御処理つきの処理を行う
        /// </summary>
        /// <param name="action">処理</param>
        void ErrorCountAction(Action action)
        {
            try
            {
                action.Invoke();
                _errorCount = 0;
            }
            catch (WebException) { _errorCount += 1; }
            catch (SoapException) { _errorCount += 1; }
            catch (SocketException) { _errorCount += 1; }
            catch (TargetInvocationException) { _errorCount += 1; }
            catch (CommunicationFailedException) { _errorCount += 1; }
            finally
            {
                if (ConsecutiveErrorHappened != null && ERROR_MAX < _errorCount)
                {
                    _errorCount = 0;
                    ConsecutiveErrorHappened(this, new EventArgs());
                }
            }
        }

        /// <summary>
        /// エラー回数制御処理つきの処理を行う
        /// </summary>
        /// <typeparam name="TResult">戻り値の型</typeparam>
        /// <param name="func">処理</param>
        /// <returns>戻り値</returns>
        /// <exception cref="CommunicationFailedException">通信失敗時に発生します</exception>
        TResult ErrorCountFunc<TResult>(Func<TResult> func)
        {
            try
            {
                var result = func.Invoke();
                _errorCount = 0;
                return result;
            }
            catch (WebException ex)
            {
                _errorCount += 1;
                throw new CommunicationFailedException(ex);
            }
            catch (SoapException ex)
            {
                _errorCount += 1;
                throw new CommunicationFailedException(ex);
            }
            catch (SocketException ex)
            {
                _errorCount += 1;
                throw new CommunicationFailedException(ex);
            }
            catch (TargetInvocationException ex)
            {
                _errorCount += 1;
                throw new CommunicationFailedException(ex);
            }
            finally
            {
                if (ConsecutiveErrorHappened != null && ERROR_MAX < _errorCount)
                {
                    _errorCount = 0;
                    ConsecutiveErrorHappened(this, new EventArgs());
                }
            }
        }
        #endregion

        #region IDisposable メンバ

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            _addressService.getAllDataCompleted -= _addressService_getAllDataCompleted;
            _addressService.getAllDataExCompleted -= _addressService_getAllDataExCompleted;
            _addressService.getTournamentDataCompleted -= _addressService_getTournamentDataCompleted;
            _addressService.Dispose();
        }

        #endregion
    }
}
