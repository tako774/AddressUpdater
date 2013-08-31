using System;

namespace HisoutenSupportTools.AddressUpdater.Lib.AddressService
{
    /// <summary>
    /// spectator拡張
    /// </summary>
    /// <remarks>Webサービスで使用するspectatorクラスを拡張</remarks>
    public partial class spectator : IEquatable<spectator>, ICloneable
    {
        #region IEquatable<spectator> メンバ

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(spectator other)
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
            return new spectator()
            {
                Id = (id)Id.Clone(),
                entryName = entryName,
            };
        }

        #endregion
    }
}
