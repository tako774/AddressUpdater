using System;
using System.Collections.ObjectModel;
using HisoutenSupportTools.AddressUpdater.Lib.AddressService;

namespace HisoutenSupportTools.AddressUpdater.Lib.Controller
{
    /// <summary>
    /// 受信管理
    /// </summary>
    public class ReceiveManager
    {
        #region フィールド
        /// <summary>接続人数キャッシュ</summary>
        private int _userCountCache;
        /// <summary>アナウンスキャッシュ</summary>
        private readonly Collection<string> _announceCache;
        /// <summary>ホスト情報キャッシュ</summary>
        private readonly Collection<host> _hostCache;
        /// <summary>大会情報キャッシュ</summary>
        private readonly Collection<tournamentInformation> _tournamentCache;
        /// <summary>チャットキャッシュ</summary>
        private readonly Collection<chat> _chatCache;
        /// <summary>自動マッチング結果キャッシュ</summary>
        private matchingResult _matchingResultCache;
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
        /// <summary>チャット受信時に発生します</summary>
        public event EventHandler<EventArgs<Collection<chat>>> ChatReceived;
        /// <summary>自動マッチング結果変化時に発生します</summary>
        public event EventHandler<EventArgs<matchingResult>> MatchingResultChanged;
        /// <summary>自動マッチング結果が空になった時に発生します</summary>
        public event EventHandler MatchingResultCleared;
        #endregion

        #region 初期化
        /// <summary>
        /// インスタンスの生成
        /// </summary>
        public ReceiveManager()
        {
            _userCountCache = 0;
            _announceCache = new Collection<string>();
            _hostCache = new Collection<host>();
            _tournamentCache = new Collection<tournamentInformation>();
            _chatCache = new Collection<chat>();
        }
        #endregion

        /// <summary>
        /// 受信データ解析
        /// </summary>
        /// <param name="data">受信データ</param>
        public void AnalyzeReceivedData(AddressService.allData data)
        {
            // 接続人数
            AnalyzeUserCount(data.UserCount);

            // アナウンス
            AnalyzeAnnounce(data.Announces);

            // ホスト
            AnalyzeHost(data.GetHost, data.Hosts);

            // 大会
            AnalyzeTournament(data.GetTournament, data.Tournaments);

            // チャット
            AnalyzeChat(data.GetChat, data.Chats);
        }

        /// <summary>
        /// 受信データ解析(自動マッチング)
        /// </summary>
        /// <param name="data">受信データ</param>
        public void AnalyzeReceivedData(AddressService.matchingResult data)
        {
            if (data == null)
            {
                if (_matchingResultCache != null && MatchingResultCleared != null)
                    MatchingResultCleared(this, EventArgs.Empty);

                _matchingResultCache = null;
                return;
            }

            if (!data.Equals(_matchingResultCache))
            {
                if (MatchingResultChanged != null)
                    MatchingResultChanged(this, new EventArgs<matchingResult>((matchingResult)data.Clone()));

                _matchingResultCache = data;
                return;
            }
        }

        /// <summary>
        /// ローカルのホスト情報クリア
        /// </summary>
        public void ClearHostCache()
        {
            _hostCache.Clear();
        }

        /// <summary>
        /// ローカルチャット履歴のクリア
        /// </summary>
        public void ClearChatCache()
        {
            _chatCache.Clear();
        }

        /// <summary>
        /// 自動マッチング結果のクリア
        /// </summary>
        public void ClearMatchingResult()
        {
            _matchingResultCache = null;
            if (MatchingResultCleared != null)
                MatchingResultCleared(this, EventArgs.Empty);
        }


        #region 接続人数解析
        /// <summary>
        /// 接続人数解析
        /// </summary>
        /// <param name="userCount">接続人数</param>
        private void AnalyzeUserCount(int userCount)
        {
            if (_userCountCache == userCount)
                return;

            _userCountCache = userCount;

            if (UserCountChanged != null)
                UserCountChanged(this, new EventArgs<int>(_userCountCache));
        }
        #endregion

        #region アナウンス解析
        /// <summary>
        /// アナウンス解析
        /// </summary>
        /// <param name="announces">アナウンス</param>
        private void AnalyzeAnnounce(string[] announces)
        {
            var receivedAnnounces = (announces == null) ? new string[0] : announces;

            lock (_announceCache)
            {
                var isSame = true;

                // 件数同じ場合比較
                if (_announceCache.Count == receivedAnnounces.Length)
                {
                    for (int i = 0; i < _announceCache.Count; i++)
                    {
                        if (_announceCache[i] != receivedAnnounces[i])
                        {
                            isSame = false;
                            break;
                        }
                    }
                }
                else
                {
                    isSame = false;
                }


                if (isSame)
                    return;


                var copyCollection = new Collection<string>();

                _announceCache.Clear();
                foreach (var announce in receivedAnnounces)
                {
                    _announceCache.Add(announce);
                    copyCollection.Add(announce);
                }

                if (AnnounceChanged != null)
                    AnnounceChanged(this, new EventArgs<Collection<string>>(copyCollection));
            }
        }
        #endregion

        #region ホスト情報解析
        /// <summary>
        /// ホスト情報解析
        /// </summary>
        /// <param name="getHost"></param>
        /// <param name="hosts"></param>
        private void AnalyzeHost(bool getHost, host[] hosts)
        {
            if (!getHost || hosts == null || hosts.Length == 0)
                return;

            var newHosts = new Collection<host>();
            var changedHosts = new Collection<host>();
            var deletedHosts = new Collection<host>();

            lock (_hostCache)
            {
                for (var i = 0; i < hosts.Length; i++)
                {
                    bool exists = false;
                    // キャッシュから確認
                    for (var j = 0; j < _hostCache.Count; j++)
                    {
                        // キャッシュにあるホスト(状態の変わったホスト,IDと時刻で判別)
                        if (hosts[i].Id.Equals(_hostCache[j].Id) && hosts[i].Time == _hostCache[j].Time)
                        {
                            // まずキャッシュから削除
                            _hostCache.RemoveAt(j);
                            exists = true;

                            if (hosts[i].isDeleted)
                            {
                                // isDeleted なら削除すべきホスト
                                deletedHosts.Add((host)hosts[i].Clone());
                            }
                            else
                            {
                                // そうでなければ状態変更したホスト
                                _hostCache.Insert(j, (host)hosts[i].Clone());
                                changedHosts.Add((host)hosts[i].Clone());
                            }
                            break;
                        }
                    }

                    // キャッシュに無い isDeleted でもないなら新規ホスト
                    if (!exists && !hosts[i].isDeleted)
                    {
                        _hostCache.Add((host)hosts[i].Clone());
                        newHosts.Add((host)hosts[i].Clone());
                    }
                }

                if (0 < deletedHosts.Count && HostDeleted != null)
                {
                    HostDeleted(this, new EventArgs<Collection<host>>(deletedHosts));
                }

                if (0 < changedHosts.Count && HostStatusChanged != null)
                {
                    HostStatusChanged(this, new EventArgs<Collection<host>>(changedHosts));
                }

                if (0 < newHosts.Count && NewHostReceived != null)
                {
                    NewHostReceived(this, new EventArgs<Collection<host>>(newHosts));
                }
            }
        }
        #endregion

        #region 大会情報解析
        /// <summary>
        /// 大会情報解析
        /// </summary>
        /// <param name="getTournament"></param>
        /// <param name="tournaments"></param>
        private void AnalyzeTournament(bool getTournament, tournamentInformation[] tournaments)
        {
            if (!getTournament || tournaments == null || tournaments.Length == 0)
                return;

            var newTournaments = new Collection<tournamentInformation>();
            var changedTournaments = new Collection<tournamentInformation>();
            var deletedTournaments = new Collection<tournamentInformation>();

            lock (_tournamentCache)
            {
                for (var i = 0; i < tournaments.Length; i++)
                {
                    bool exists = false;
                    // キャッシュから確認
                    for (var j = 0; j < _tournamentCache.Count; j++)
                    {
                        // キャッシュにある大会(状態の変わった大会,IDと時刻で判別)
                        if (tournaments[i].Id.Equals(_tournamentCache[j].Id) && tournaments[i].Time == _tournamentCache[j].Time)
                        {
                            // まずキャッシュから削除
                            _tournamentCache.RemoveAt(j);
                            exists = true;

                            if (tournaments[i].Deleted)
                            {
                                // isDeleted なら削除すべき大会
                                deletedTournaments.Add((tournamentInformation)tournaments[i].Clone());
                            }
                            else
                            {
                                // そうでなければ状態変更した大会
                                _tournamentCache.Insert(j, (tournamentInformation)tournaments[i].Clone());
                                changedTournaments.Add((tournamentInformation)tournaments[i].Clone());
                            }
                            break;
                        }
                    }

                    // キャッシュに無い isDeleted でもないなら新規大会
                    if (!exists && !tournaments[i].Deleted)
                    {
                        _tournamentCache.Add((tournamentInformation)tournaments[i].Clone());
                        newTournaments.Add((tournamentInformation)tournaments[i].Clone());
                    }
                }

                if (0 < deletedTournaments.Count && TournamentDeleted != null)
                {
                    TournamentDeleted(this, new EventArgs<Collection<tournamentInformation>>(deletedTournaments));
                }

                if (0 < changedTournaments.Count && TournamentStatusChanged != null)
                {
                    TournamentStatusChanged(this, new EventArgs<Collection<tournamentInformation>>(changedTournaments));
                }

                if (0 < newTournaments.Count && NewTournamentReceived != null)
                {
                    NewTournamentReceived(this, new EventArgs<Collection<tournamentInformation>>(newTournaments));
                }
            }
        }
        #endregion

        #region チャット解析
        /// <summary>
        /// チャット解析
        /// </summary>
        /// <param name="getChat"></param>
        /// <param name="chats"></param>
        private void AnalyzeChat(bool getChat, chat[] chats)
        {
            if (!getChat || chats == null || chats.Length == 0)
                return;

            var addChats = new Collection<chat>();

            // キャッシュ0の時はそのまま
            if (_chatCache.Count == 0)
            {
                lock (_chatCache)
                {
                    foreach (var chat in chats)
                    {
                        _chatCache.Add((chat)chat.Clone());
                        addChats.Add((chat)chat.Clone());
                    }

                    if (ChatReceived != null)
                        ChatReceived(this, new EventArgs<Collection<chat>>(addChats));
                }
                return;
            }



            lock (_chatCache)
            {
                // 全部に重複チェックはいらないので、キャッシュの最後から受信分チェック
                var checkChats = new Collection<chat>();
                if (_chatCache.Count < chats.Length)
                {
                    // キャッシュより多い件数受信したなら全チェック
                    foreach (var chat in _chatCache)
                        checkChats.Add(chat); // checkChatsはキャッシュから参照するだけなのでCloneしない
                }
                else if (chats.Length <= _chatCache.Count)
                {
                    // キャッシュ件数以下なら、キャッシュの最後から受信件数分をチェック
                    for (var i = _chatCache.Count - chats.Length; i < _chatCache.Count; i++)
                        checkChats.Add(_chatCache[i]);
                }


                for (var i = 0; i < chats.Length; i++)
                {
                    var exists = false;
                    for (var j = 0; j < checkChats.Count; j++)
                    {
                        if (chats[i].Equals(checkChats[j]))
                        {
                            exists = true;
                            break;
                        }
                    }

                    if (!exists)
                        addChats.Add((chat)chats[i].Clone());
                }

                foreach (var chat in addChats)
                    _chatCache.Add((chat)chat.Clone());

                if (ChatReceived != null)
                    ChatReceived(this, new EventArgs<Collection<chat>>(addChats));
            }
        }
        #endregion
    }
}
