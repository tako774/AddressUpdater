using System;
using System.Collections.ObjectModel;
using HisoutenSupportTools.AddressUpdater.Lib.AddressService;
using HisoutenSupportTools.AddressUpdater.Lib.Model;
using HisoutenSupportTools.AddressUpdater.Lib.Model.Tournament;

namespace HisoutenSupportTools.AddressUpdater.Lib.Controller.Tournament
{
    /// <summary>
    /// 大会データ受信管理
    /// </summary>
    public class TournamentReceiveManager
    {
        /// <summary>
        /// 自分のIDの取得・設定
        /// </summary>
        public id Id { get; set; }

        /// <summary>大会情報</summary>
        private TournamentInformation TournamentInformation
        {
            get { return _tournamentInformation; }
            set
            {
                if (_tournamentInformation.Equals(value))
                    return;

                _tournamentInformation = value;
                if (TournamentInformationChanged != null)
                    TournamentInformationChanged(this, new EventArgs<TournamentInformation>(value));
            }
        }
        private TournamentInformation _tournamentInformation = new TournamentInformation();


        /// <summary>アナウンスキャッシュ</summary>
        private readonly Collection<string> _announceCache;
        /// <summary>運営者情報キャッシュ</summary>
        private readonly Collection<manager> _managerCache;
        /// <summary>参加者情報キャッシュ</summary>
        private readonly Collection<player> _playerCache;
        /// <summary>観戦者情報キャッシュ</summary>
        private readonly Collection<spectator> _spectatorCache;
        /// <summary>ゲスト情報キャッシュ</summary>
        private readonly Collection<guest> _guestCache;
        /// <summary>追放者IDキャッシュ</summary>
        private readonly Collection<id> _kickedIdCache;
        /// <summary>チャットキャッシュ</summary>
        private readonly Collection<chat> _chatCache;
        /// <summary>参加者情報キャッシュ(対戦状態用)</summary>
        private readonly Collection<player> _playerCacheForMatchStatus;
        /// <summary>最後にデータを受信した日時</summary>
        private DateTime _lastDataTime = DateTime.MinValue;
        /// <summary>最後にチャットを受信した日時</summary>
        private DateTime _lastChatTime = DateTime.MinValue;


        #region イベント
        /// <summary>大会削除時に発生します</summary>
        public event EventHandler TournamentDeleted;
        /// <summary>キックされた場合に発生します</summary>
        public event EventHandler Kicked;

        /// <summary>大会情報変化時に発生します</summary>
        public event EventHandler<EventArgs<TournamentInformation>> TournamentInformationChanged;
        /// <summary>ユーザー変化時(役割,ID,エントリー名)に発生します</summary>
        public event EventHandler<UsersChangedEventArgs> UsersChanged;
        /// <summary>アナウンス変化時に発生します</summary>
        public event EventHandler<EventArgs<Collection<string>>> AnnounceChanged;

        /// <summary>試合状況変化時に発生します</summary>
        public event EventHandler<EventArgs<Collection<player>>> MatchStatusChanged;
        /// <summary>チャット受信時に発生します</summary>
        public event EventHandler<EventArgs<Collection<chat>>> ChatReceived;
        #endregion

        #region 初期化
        /// <summary>
        /// インスタンスの生成
        /// </summary>
        public TournamentReceiveManager()
        {
            _announceCache = new Collection<string>();
            _managerCache = new Collection<manager>();
            _playerCache = new Collection<player>();
            _playerCacheForMatchStatus = new Collection<player>();
            _spectatorCache = new Collection<spectator>();
            _guestCache = new Collection<guest>();
            _chatCache = new Collection<chat>();
            _kickedIdCache = new Collection<id>();
        }
        #endregion

        #region データ解析
        /// <summary>
        /// 受信データ解析
        /// </summary>
        /// <param name="data"></param>
        public void AnalizeData(tournament data)
        {
            // 削除確認
            if (data.deleted)
            {
                if (TournamentDeleted != null)
                    TournamentDeleted(this, EventArgs.Empty);
                return;
            }

            // キック確認
            if (IsKicked(data.KickedIds))
            {
                if (Kicked != null)
                    Kicked(this, EventArgs.Empty);
                return;
            }

            // アナウンス解析
            AnalizeAnnounce(data.Announces);

            if (_lastDataTime < data.lastDataUpdate)
            {
                // 大会情報解析
                AnalizeTournamentInformation(data);

                // ユーザー情報解析(Id,役割,エントリー名)
                AnalizeUsers(data);

                // 対戦状況解析
                AnalizeMatchStatus(data.Players);


                _lastDataTime = data.lastDataUpdate;
            }
        }
        #endregion

        #region チャット解析
        /// <summary>
        /// 受信チャット解析
        /// </summary>
        /// <param name="data"></param>
        public void AnalizeChat(tournament data)
        {
            // 削除確認
            if (data.deleted)
            {
                if (TournamentDeleted != null)
                    TournamentDeleted(this, EventArgs.Empty);
                return;
            }

            // キック確認
            if (IsKicked(data.KickedIds))
            {
                if (Kicked != null)
                    Kicked(this, EventArgs.Empty);
                return;
            }

            // アナウンス解析
            AnalizeAnnounce(data.Announces);


            if (data.lastChatUpdate <= _lastChatTime)
                return;
            if (data.Chats == null || data.Chats.Length == 0)
                return;


            var addChats = new Collection<chat>();

            if (_chatCache.Count == 0)
            {
                foreach (var chat in data.Chats)
                {
                    _chatCache.Add((chat)chat.Clone());
                    addChats.Add((chat)chat.Clone());
                }

                if (ChatReceived != null)
                    ChatReceived(this, new EventArgs<Collection<chat>>(addChats));
            }
            else
            {
                // 全部に重複チェックはいらないので、キャッシュの最後から受信分チェック
                var checkChats = new Collection<chat>();
                if (_chatCache.Count < data.Chats.Length)
                {
                    // キャッシュより多い件数受信したなら全チェック
                    foreach (var chat in _chatCache)
                        checkChats.Add(chat); // checkChatsはキャッシュから参照するだけなのでCloneしない
                }
                else if (data.Chats.Length <= _chatCache.Count)
                {
                    // キャッシュ件数以下なら、キャッシュの最後から受信件数分をチェック
                    for (var i = _chatCache.Count - data.Chats.Length; i < _chatCache.Count; i++)
                        checkChats.Add(_chatCache[i]);
                }

                for (var i = 0; i < data.Chats.Length; i++)
                {
                    var exists = false;
                    for (var j = 0; j < checkChats.Count; j++)
                    {
                        if (data.Chats[i].Equals(checkChats[j]))
                        {
                            exists = true;
                            break;
                        }
                    }

                    if (!exists)
                        addChats.Add((chat)data.Chats[i].Clone());
                }

                foreach (var chat in addChats)
                    _chatCache.Add((chat)chat.Clone());

                if (ChatReceived != null)
                    ChatReceived(this, new EventArgs<Collection<chat>>(addChats));
            }


            _lastChatTime = data.lastChatUpdate;
        }
        #endregion


        /// <summary>
        /// 大会情報解析
        /// </summary>
        /// <param name="data"></param>
        private void AnalizeTournamentInformation(tournament data)
        {
            if (Id == null)
                throw new ApplicationException("Id is null");

            var information = new TournamentInformation();

            // 設定人数
            information.UsersCount = data.userCount;

            // 参加人数
            if (data.Players == null)
                information.PlayersCount = 0;
            else
                information.PlayersCount = data.Players.Length;

            // 大会種別
            try { information.Type = (TournamentTypes)data.Type; }
            catch (Exception) { information.Type = TournamentTypes.不明; }

            // 開始状態
            information.IsStarted = data.started;


            // 役割・エントリー名
            var roles = (int)TournamentRoles.None;

            if (data.Guests != null)
            {
                foreach (var guest in data.Guests)
                {
                    if (guest.Id.Equals(Id))
                    {
                        information.EntryName = guest.entryName;
                        roles |= (int)TournamentRoles.Guest;
                        break;
                    }
                }
            }
            if (data.Spectators != null)
            {
                foreach (var spectator in data.Spectators)
                {
                    if (spectator.Id.Equals(Id))
                    {
                        information.EntryName = spectator.entryName;
                        roles |= (int)TournamentRoles.Spectator;
                        break;
                    }
                }
            }
            if (data.Players != null)
            {
                foreach (var player in data.Players)
                {
                    if (player.Id.Equals(Id))
                    {
                        information.EntryName = player.entryName;
                        if (!player.retired)
                            roles |= (int)TournamentRoles.Player;
                        break;
                    }
                }
            }
            if (data.Managers != null)
            {
                foreach (var manager in data.Managers)
                {
                    if (manager.Id.Equals(Id))
                    {
                        information.EntryName = manager.entryName;
                        roles |= (int)TournamentRoles.Manager;
                        break;
                    }
                }
            }
            information.Roles = roles;

            
            TournamentInformation = information;
        }

        /// <summary>
        /// ユーザー情報解析
        /// </summary>
        /// <param name="data"></param>
        private void AnalizeUsers(tournament data)
        {
            var isSame = true;

            if (data.Guests == null) data.Guests = new guest[] { };
            if (data.Spectators == null) data.Spectators = new spectator[] { };
            if (data.Players == null) data.Players = new player[] { };
            if (data.Managers == null) data.Managers = new manager[] { };
            if (data.KickedIds == null) data.KickedIds = new id[] { };


            if (_guestCache.Count != data.Guests.Length)
                isSame = false;
            else
            {
                for (var i = 0; i < _guestCache.Count; i++)
                {
                    if (!_guestCache[i].Equals(data.Guests[i]))
                    {
                        isSame = false;
                        break;
                    }
                }
            }

            if (isSame)
            {
                if (_spectatorCache.Count != data.Spectators.Length)
                    isSame = false;
                else
                {
                    for (var i = 0; i < _spectatorCache.Count; i++)
                    {
                        if(!_spectatorCache[i].Equals(data.Spectators[i]))
                        {
                            isSame = false;
                            break;
                        }
                    }
                }
            }

            if (isSame)
            {
                if (_playerCache.Count != data.Players.Length)
                    isSame = false;
                else
                {
                    for (var i = 0; i < _playerCache.Count; i++)
                    {
                        if (!_playerCache[i].Id.Equals(data.Players[i].Id) ||
                            _playerCache[i].entryName != data.Players[i].entryName ||
                            _playerCache[i].retired != data.Players[i].retired)
                        {
                            isSame = false;
                            break;
                        }
                    }
                }
            }

            if (isSame)
            {
                if (_managerCache.Count != data.Managers.Length)
                    isSame = false;
                else
                {
                    for (var i = 0; i < _managerCache.Count; i++)
                    {
                        if (!_managerCache[i].Equals(data.Managers[i]))
                        {
                            isSame = false;
                            break;
                        }
                    }
                }
            }

            if (isSame)
            {
                if (_kickedIdCache.Count != data.KickedIds.Length)
                    isSame = false;
                else
                {
                    for (var i = 0; i < _kickedIdCache.Count; i++)
                    {
                        if (!_kickedIdCache[i].Equals(data.KickedIds[i]))
                        {
                            isSame = false;
                            break;
                        }
                    }
                }
            }

            if (!isSame)
            {
                var guestCopy = new Collection<guest>();
                _guestCache.Clear();
                foreach (var guest in data.Guests)
                {
                    _guestCache.Add((guest)guest.Clone());
                    guestCopy.Add((guest)guest.Clone());
                }

                var spectatorCopy = new Collection<spectator>();
                _spectatorCache.Clear();
                foreach(var spectator in data.Spectators)
                {
                    _spectatorCache.Add((spectator)spectator.Clone());
                    spectatorCopy.Add((spectator)spectator.Clone());
                }

                var playerCopy = new Collection<player>();
                _playerCache.Clear();
                foreach (var player in data.Players)
                {
                    _playerCache.Add((player)player.Clone());
                    playerCopy.Add((player)player.Clone());
                }

                var managerCopy = new Collection<manager>();
                _managerCache.Clear();
                foreach (var manager in data.Managers)
                {
                    _managerCache.Add((manager)manager.Clone());
                    managerCopy.Add((manager)manager.Clone());
                }

                var kickedIdCopy = new Collection<id>();
                _kickedIdCache.Clear();
                foreach (var kickedId in data.KickedIds)
                {
                    _kickedIdCache.Add((id)kickedId.Clone());
                    kickedIdCopy.Add((id)kickedId.Clone());
                }

                if (UsersChanged != null)
                    UsersChanged(this, new UsersChangedEventArgs(managerCopy, playerCopy, spectatorCopy, guestCopy, kickedIdCopy));
            }
        }

        /// <summary>
        /// アナウンス解析
        /// </summary>
        /// <param name="announces"></param>
        private void AnalizeAnnounce(string[] announces)
        {
            if (announces == null)
                announces = new string[] { };

            var isSame = true;

            if (_announceCache.Count != announces.Length)
                isSame = false;
            else
            {
                for (var i = 0; i < _announceCache.Count; i++)
                {
                    if (_announceCache[i] != announces[i])
                    {
                        isSame = false;
                        break;
                    }
                }
            }


            if (!isSame)
            {
                var copy = new Collection<string>();
                _announceCache.Clear();
                foreach (var announce in announces)
                {
                    _announceCache.Add(announce);
                    copy.Add(announce);
                }

                if (AnnounceChanged != null)
                    AnnounceChanged(this, new EventArgs<Collection<string>>(copy));
            }
        }

        /// <summary>
        /// 対戦状況解析
        /// </summary>
        /// <param name="players"></param>
        private void AnalizeMatchStatus(player[] players)
        {
            if (!TournamentInformation.IsStarted)
                return;

            if (players == null)
                return;

            var isSame = true;
            if (_playerCacheForMatchStatus.Count <= 0)
                isSame = false;
            else
            {
                for (var i = 0; i < players.Length; i++)
                {
                    // waiting
                    if (_playerCacheForMatchStatus[i].waiting != players[i].waiting)
                    {
                        isSame = false;
                        break;
                    }
                    // fighting
                    if (_playerCacheForMatchStatus[i].fighting != players[i].fighting)
                    {
                        isSame = false;
                        break;
                    }
                    // retire
                    if (_playerCacheForMatchStatus[i].retired != players[i].retired)
                    {
                        isSame = false;
                        break;
                    }
                    // 両方null = equal
                    else if (_playerCacheForMatchStatus[i].MatchResults == null && players[i].MatchResults == null)
                    {
                        continue;
                    }
                    // 片方 null = not equal
                    else if (_playerCacheForMatchStatus[i].MatchResults == null && players[i].MatchResults != null)
                    {
                        isSame = false;
                        break;
                    }
                    // 片方 null = not equal
                    else if (_playerCacheForMatchStatus[i].MatchResults != null && players[i].MatchResults == null)
                    {
                        isSame = false;
                        break;
                    }
                    // サイズが違う = not equal
                    else if (_playerCacheForMatchStatus[i].MatchResults.Length != players[i].MatchResults.Length)
                    {
                        isSame = false;
                        break;
                    }
                    // サイズが同じ
                    else
                    {
                        // 各要素を比較していく
                        for (var j = 0; j < _playerCacheForMatchStatus[i].MatchResults.Length; j++)
                        {
                            if (_playerCacheForMatchStatus[i].MatchResults[j] != players[i].MatchResults[j])
                            {
                                isSame = false;
                                break;
                            }
                        }
                    }
                }
            }


            if (!isSame)
            {
                var copy = new Collection<player>();
                _playerCacheForMatchStatus.Clear();
                foreach (var player in players)
                {
                    _playerCacheForMatchStatus.Add((player)player.Clone());
                    copy.Add((player)player.Clone());
                }
                if (MatchStatusChanged != null)
                    MatchStatusChanged(this, new EventArgs<Collection<player>>(copy));
            }
        }

        /// <summary>
        /// キックされたかどうか
        /// </summary>
        /// <param name="kickedIds"></param>
        /// <returns></returns>
        bool IsKicked(id[] kickedIds)
        {
            if (kickedIds == null)
                return false;

            foreach (id kickedId in kickedIds)
                if (kickedId.Equals(Id))
                    return true;

            return false;
        }
    }
}
