using System;
using System.Collections.Generic;
using HisoutenSupportTools.AddressUpdater.Lib.Util;

namespace HisoutenSupportTools.AddressUpdater.Lib.AddressService
{
    /// <summary>
    /// player拡張
    /// </summary>
    /// <remarks>Webサービスで使用するplayerクラスを拡張</remarks>
    public partial class player : IComparable<player>, IEquatable<player>, ICloneable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return entryName;
        }

        #region IComparable<player> メンバ

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(player other)
        {
            return entryNo.CompareTo(other.entryNo);
        }

        #endregion

        #region IEquatable<player> メンバ

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(player other)
        {
            if (other == null) return false;
            if (object.ReferenceEquals(this, other)) return true;

            return
                Id.Equals(other.Id) &&
                entryName == other.entryName &&
                entryNo == other.entryNo &&
                ip == other.ip &&
                port == other.port &&
                ArrayExtention.ElementsEquals<int?>(MatchResults, other.MatchResults) &&
                waiting == other.waiting &&
                fighting == other.fighting &&
                retired == other.retired;
        }

        #endregion

        #region ICloneable メンバ

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            var clone = new player()
            {
                Id = (id)Id.Clone(),
                entryName = entryName,
                entryNo = entryNo,
                ip = ip,
                port = port,
                waiting = waiting,
                fighting = fighting,
                retired = retired,
            };

            if (MatchResults != null)
            {
                var results = new List<int?>();
                foreach (var result in MatchResults)
                    results.Add(result);
                clone.MatchResults = results.ToArray();
            }

            return clone;
        }

        #endregion
    }
}
