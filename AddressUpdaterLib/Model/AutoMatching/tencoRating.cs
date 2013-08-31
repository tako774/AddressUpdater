using System;

namespace HisoutenSupportTools.AddressUpdater.Lib.AddressService
{
    /// <summary>
    /// rating拡張
    /// </summary>
    public partial class tencoRating : IEquatable<tencoRating>, ICloneable
    {
        #region IEquatable<tencoRating> メンバ

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(tencoRating other)
        {
            if (other == null) return false;
            if (object.ReferenceEquals(this, other)) return true;

            var result =
                Character.Equals(other.Character) &&
                Value == other.Value &&
                Deviation == other.Deviation;

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
            var clone = new tencoRating()
            {
                Character = (th123Characters)Character.Clone(),
                Value = Value,
                Deviation = Deviation,
            };

            return clone;
        }

        #endregion
    }
}
