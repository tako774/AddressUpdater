using System;

namespace HisoutenSupportTools.AddressUpdater.Lib.AddressService
{
    /// <summary>
    /// tencoUser拡張
    /// </summary>
    public partial class tencoUser : IEquatable<tencoUser>, ICloneable
    {
        #region IEquatable<tencoUser> メンバ

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(tencoUser other)
        {
            if (other == null) return false;
            if (object.ReferenceEquals(this, other)) return true;

            var result =
                Id.Equals(other.Id) &&
                Time == other.Time &&
                AccountName == other.AccountName &&
                IsHostable == other.IsHostable &&
                IsRoomOnly == other.IsRoomOnly &&
                Ip == other.Ip &&
                Port == other.Port &&
                Comment == other.Comment &&
                MatchingSpan == other.MatchingSpan &&
                Rating.Equals(other.Rating);

            return result;
        }

        #endregion

        #region ICloneable メンバ

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            var clone = new tencoUser()
            {
                Id = (id)Id.Clone(),
                Time = Time,
                AccountName = AccountName,
                IsHostable = IsHostable,
                IsRoomOnly = IsRoomOnly,
                MatchingSpan = MatchingSpan,
                Ip = Ip,
                Port = Port,
                Comment = Comment,
                Rating = (tencoRating)Rating.Clone(),
            };

            return clone;
        }

        #endregion
    }
}
