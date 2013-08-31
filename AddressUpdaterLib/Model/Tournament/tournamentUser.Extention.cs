using System;

namespace HisoutenSupportTools.AddressUpdater.Lib.AddressService
{
    /// <summary>
    /// tournamentUser拡張
    /// </summary>
    /// <remarks>Webサービスで使用するtournamentUserクラスを拡張</remarks>
    public partial class tournamentUser : IEquatable<tournamentUser>, ICloneable
    {
        #region IEquatable<tournamentUser> メンバ

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(tournamentUser other)
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
        public virtual object Clone()
        {
            return new tournamentUser()
            {
                Id = (id)Id.Clone(),
                entryName = entryName,
            };
        }

        #endregion
    }
}
