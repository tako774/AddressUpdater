using System;
using HisoutenSupportTools.AddressUpdater.Lib.Model;
using HisoutenSupportTools.AddressUpdater.Lib.Model.Tournament;

namespace HisoutenSupportTools.AddressUpdater.Lib.Controller.Tournament
{
    /// <summary>
    /// 大会内情報
    /// </summary>
    public class TournamentInformation : IEquatable<TournamentInformation>
    {
        /// <summary>大会番号</summary>
        public int TournamentNo { get; set; }
        /// <summary>エントリー名</summary>
        public string EntryName { get; set; }
        /// <summary>設定人数</summary>
        public int UsersCount { get; set; }
        /// <summary>参加人数</summary>
        public int PlayersCount { get; set; }
        /// <summary>大会種別</summary>
        public TournamentTypes Type { get; set; }
        /// <summary>役割</summary>
        public int Roles { get; set; }
        /// <summary>開始状態</summary>
        public bool IsStarted { get; set; }

        /// <summary>
        /// 運営かどうか
        /// </summary>
        public bool IsManager
        {
            get { return (Roles & (int)TournamentRoles.Manager) != 0; }
        }

        /// <summary>
        /// 参加者かどうか
        /// </summary>
        public bool IsPlayer
        {
            get { return (Roles & (int)TournamentRoles.Player) != 0; }
        }

        /// <summary>
        /// 観戦者かどうか
        /// </summary>
        public bool IsSpectator
        {
            get { return (Roles & (int)TournamentRoles.Spectator) != 0; }
        }

        /// <summary>
        /// ゲストかどうか
        /// </summary>
        public bool IsGuest
        {
            get { return Roles == (int)TournamentRoles.Guest; }
        }

        /// <summary>
        /// 開始できるかどうか
        /// </summary>
        public bool IsStartable
        {
            get
            {
                // 既に開始されている場合false
                if (IsStarted)
                    return false;

                switch (Type)
                {
                    case TournamentTypes.総当たり:
                        return 2 <= PlayersCount;
                    default:
                        return PlayersCount == UsersCount;
                }
            }
        }

        #region IEquatable<TournamentInformation> メンバ

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(TournamentInformation other)
        {
            if (other == null) return false;
            if (object.ReferenceEquals(this, other)) return true;

            return
                TournamentNo == other.TournamentNo &&
                EntryName == other.EntryName &&
                UsersCount == other.UsersCount &&
                PlayersCount == other.PlayersCount &&
                Type == other.Type &&
                Roles == other.Roles &&
                IsStarted == other.IsStarted;
        }

        #endregion
    }
}
