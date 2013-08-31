using System;
using System.Collections.ObjectModel;
using HisoutenSupportTools.AddressUpdater.Lib.AddressService;

namespace HisoutenSupportTools.AddressUpdater.Lib.Controller.Tournament
{
    /// <summary>
    /// ユーザー状態変化イベント引数
    /// </summary>
    public class UsersChangedEventArgs : EventArgs
    {
        /// <summary>運営者</summary>
        public readonly ReadOnlyCollection<manager> Managers;
        /// <summary>参加者</summary>
        public readonly ReadOnlyCollection<player> Players;
        /// <summary>観戦者</summary>
        public readonly ReadOnlyCollection<spectator> Spectators;
        /// <summary>ゲスト</summary>
        public readonly ReadOnlyCollection<guest> Guests;
        /// <summary>追放ID</summary>
        public readonly ReadOnlyCollection<id> KickedIds;

        /// <summary>
        /// インスタンスの生成
        /// </summary>
        /// <param name="managers">運営</param>
        /// <param name="players">参加</param>
        /// <param name="spectators">観戦</param>
        /// <param name="guests">ゲスト</param>
        /// <param name="kickedIds">追放</param>
        public UsersChangedEventArgs(
            Collection<manager> managers,
            Collection<player> players,
            Collection<spectator> spectators,
            Collection<guest> guests,
            Collection<id> kickedIds)
        {
            Managers = new ReadOnlyCollection<manager>(managers);
            Players = new ReadOnlyCollection<player>(players);
            Spectators = new ReadOnlyCollection<spectator>(spectators);
            Guests = new ReadOnlyCollection<guest>(guests);
            KickedIds = new ReadOnlyCollection<id>(kickedIds);
        }
    }
}
