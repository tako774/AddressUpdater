using System;
using HisoutenSupportTools.AddressUpdater.Lib.Model;

namespace HisoutenSupportTools.AddressUpdater.Lib.AddressService
{
    /// <summary>
    /// host拡張
    /// </summary>
    /// <remarks>Webサービスで使用するhostクラスを拡張</remarks>
    public partial class host : IEquatable<host>, ICloneable
    {
        /// <summary>
        /// インスタンスの生成
        /// </summary>
        public host() { }

        /// <summary>
        /// インスタンスの生成
        /// </summary>
        /// <param name="ip">IP</param>
        /// <param name="port">ポート</param>
        /// <param name="rank">ランク</param>
        /// <param name="comment">コメント</param>
        public host(string ip, int port, string rank, string comment)
        {
            Ip = ip;
            Port = port;
            Rank = rank;
            Comment = comment;
        }

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
            row.No = No.ToString();
            row.Time = Time;
            row.LastUpdate = lastUpdate;
            row.IpPort = Ip + ":" + Port.ToString();
            row.Rank = Rank;
            row.Comment = Comment;
            row.IsFighting = IsFighting;
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
            row.LastUpdate = lastUpdate;
            row.Ip = Ip;
            row.Port = Port;
            row.Rank = Rank;
            row.Comment = Comment;
            row.IsFighting = IsFighting;
            row.IsDeleted = isDeleted;
            row.EndEdit();
            return row;
        }

        /// <summary>
        /// HostRowから生成
        /// </summary>
        /// <param name="hostRow">HostRow</param>
        /// <returns>host</returns>
        public static host FromHostRow(AddressUpdaterDataSet.HostRow hostRow)
        {
            var host = new host();
            host.No = hostRow.No;
            host.Id = new id() { value = hostRow.Id };
            host.Time = hostRow.Time;
            host.lastUpdate = hostRow.LastUpdate;
            host.Ip = hostRow.Ip;
            host.Port = hostRow.Port;
            host.Rank = hostRow.Rank;
            host.Comment = hostRow.Comment;
            host.IsFighting = hostRow.IsFighting;
            host.isDeleted = hostRow.IsDeleted;
            return host;
        }
        #endregion

        #region IEquatable<host> メンバ

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(host other)
        {
            if (other == null) return false;
            if (object.ReferenceEquals(this, other)) return true;

            return
                No == other.No &&
                Id.Equals(other.Id) &&
                Time == other.Time &&
                Ip == other.Ip &&
                Port == other.Port &&
                Rank == other.Rank &&
                Comment == other.Comment &&
                IsFighting == other.IsFighting;
        }

        #endregion

        #region ICloneable メンバ

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return new host()
            {
                No = No,
                Id = (id)Id.Clone(),
                Time = Time,
                lastUpdate = lastUpdate,
                Ip = Ip,
                Port = Port,
                Rank = Rank,
                Comment = Comment,
                IsFighting = IsFighting,
                isDeleted = isDeleted,
            };
        }

        #endregion
    }
}
