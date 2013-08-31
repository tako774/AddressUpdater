using System;

namespace HisoutenSupportTools.AddressUpdater.Lib.AddressService
{
    /// <summary>
    /// id拡張
    /// </summary>
    /// <remarks>Webサービスで使用するidクラスを拡張</remarks>
    public partial class id : IEquatable<id>, ICloneable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return value;
        }

        #region IEquatable<id> メンバ

        /// <summary></summary>
        public bool Equals(id other)
        {
            if (other == null) return false;
            if (object.ReferenceEquals(this, other)) return true;

            return value == other.value;
        }

        #endregion

        #region ICloneable メンバ

        /// <summary></summary>
        /// <returns></returns>
        public object Clone()
        {
            return new id() { value = value };
        }

        #endregion
    }
}
