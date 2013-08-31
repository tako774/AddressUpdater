using System;

namespace HisoutenSupportTools.AddressUpdater.Lib.AddressService
{
    /// <summary>
    /// manager拡張
    /// </summary>
    /// <remarks>Webサービスで使用するmanagerクラスを拡張</remarks>
    public partial class manager : IEquatable<manager>, ICloneable
    {
        #region IEquatable<manager> メンバ

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(manager other)
        {
            if (other == null) return false;
            if (object.ReferenceEquals(this, other)) return true;

            return Id.Equals(other.Id) && entryName == other.entryName;
        }

        #endregion

        #region ICloneable メンバ

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            return new manager()
            {
                Id = (id)Id.Clone(),
                entryName = entryName,
            };
        }

        #endregion
    }
}
