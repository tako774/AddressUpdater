using System;
using HisoutenSupportTools.AddressUpdater.Lib.Model;
using HisoutenSupportTools.AddressUpdater.Lib.Model.Tournament;

namespace HisoutenSupportTools.AddressUpdater.Lib.AddressService
{
    /// <summary>
    /// tournamentInformation拡張
    /// </summary>
    public partial class tournamentInformation : IEquatable<tournamentInformation>, ICloneable
    {
        /// <summary>
        /// インスタンスの生成
        /// </summary>
        public tournamentInformation() { }

        /// <summary>
        /// HostsRowに変換
        /// </summary>
        /// <param name="table">所属するテーブル</param>
        /// <returns>HostsRow</returns>
        public AddressUpdaterDataSet.HostsRow ToHostsRow(AddressUpdaterDataSet.HostsDataTable table)
        {
            var row = table.NewHostsRow();
            row.BeginEdit();
            row.Id = Id.value;
            row.No = "T" + No;
            row.Time = Time;
            row.LastUpdate = LastUpdate;
            row.IpPort = Enum.GetName(typeof(TournamentTypes), Type) + ":" + PlayersCount + "/" + UserCount.ToString();
            row.Rank = Rank;
            row.Comment = Comment;
            row.IsFighting = Started;
            row.EndEdit();

            return row;
        }

        #region
        /// <summary>
        /// HostRowに変換
        /// </summary>
        /// <param name="table">所属するテーブル</param>
        /// <returns>HostRow</returns>
        public AddressUpdaterDataSet.HostRow ToHostRow(AddressUpdaterDataSet.HostDataTable table)
        {
            var row = table.NewHostRow();
            row.BeginEdit();
            row.No = No;
            row.Id = Id.value;
            row.Time = Time;
            row.LastUpdate = LastUpdate;
            row.Ip = Enum.GetName(typeof(TournamentTypes), Type);
            row.Port = UserCount;
            row.PlayersCount = PlayersCount;
            row.Rank = Rank;
            row.Comment = Comment;
            row.IsFighting = Started;
            row.IsDeleted = Deleted;
            row.EndEdit();
            return row;
        }

        /// <summary>
        /// HostRowから生成
        /// </summary>
        /// <param name="hostRow">HostRow</param>
        /// <returns>tournamentInformation</returns>
        public static tournamentInformation FromHostRow(AddressUpdaterDataSet.HostRow hostRow)
        {
            var tournament = new tournamentInformation();
            tournament.No = hostRow.No;
            tournament.Id = new id() { value = hostRow.Id };
            tournament.Time = hostRow.Time;
            tournament.LastUpdate = hostRow.LastUpdate;
            tournament.Type = (int)Enum.Parse(typeof(TournamentTypes), hostRow.Ip);
            tournament.UserCount = hostRow.Port;
            tournament.PlayersCount = hostRow.PlayersCount;
            tournament.Rank = hostRow.Rank;
            tournament.Comment = hostRow.Comment;
            tournament.Started = hostRow.IsFighting;
            tournament.Deleted = hostRow.IsDeleted;
            return tournament;
        }
        #endregion

        #region IEquatable<tournamentInformation> メンバ

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(tournamentInformation other)
        {
            if (other == null) return false;
            if (object.ReferenceEquals(this, other)) return true;

            return
                No == other.No &&
                Id.Equals(other.Id) &&
                Time == other.Time &&
                Type == other.Type &&
                UserCount == other.UserCount &&
                PlayersCount == other.PlayersCount &&
                Rank == other.Rank &&
                Comment == other.Comment &&
                Started == other.Started;
        }

        #endregion

        #region ICloneable メンバ

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return new tournamentInformation()
            {
                No = No,
                Id = (id)Id.Clone(),
                Time = Time,
                LastUpdate = LastUpdate,
                Type = Type,
                UserCount = UserCount,
                PlayersCount = PlayersCount,
                Rank = Rank,
                Comment = Comment,
                Started = Started,
                Deleted = Deleted,
            };
        }

        #endregion
    }
}
