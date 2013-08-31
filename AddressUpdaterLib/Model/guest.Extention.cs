using System;

namespace HisoutenSupportTools.AddressUpdater.Lib.AddressService
{
    /// <summary>
    /// guest拡張
    /// </summary>
    /// <remarks>Webサービスで使用するguestクラスを拡張</remarks>
    public partial class guest : IEquatable<guest>, ICloneable
    {
        #region IEquatable<guest> メンバ

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(guest other)
        {
            if (other == null) return false;
            if (object.ReferenceEquals(this, other)) return true;

            return
                Id.Equals(other.Id) &&
                entryName == other.entryName;
        }

        #endregion

        #region ICloneable メンバ

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            return new guest()
            {
                Id = (id)Id.Clone(),
                entryName = entryName,
                lastAccessTime = lastAccessTime,
            };
        }

        #endregion
    }
}
