using System;

namespace HisoutenSupportTools.AddressUpdater.Lib.AddressService
{
    /// <summary>
    /// matchingResult拡張
    /// </summary>
    public partial class matchingResult : IEquatable<matchingResult>, ICloneable
    {
        #region IEquatable<matchingResult> メンバ

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(matchingResult other)
        {
            if (other == null) return false;
            if (object.ReferenceEquals(this, other)) return true;

            var result =
                Host.Equals(other.Host) &&
                Client.Equals(other.Client) &&
                hostPrepared == other.hostPrepared &&
                clientPrepared == other.clientPrepared;

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
            var clone = new matchingResult()
            {
                Host = (tencoUser)Host.Clone(),
                Client = (tencoUser)Client.Clone(),
                hostPrepared = hostPrepared,
                clientPrepared = clientPrepared,
            };

            return clone;
        }

        #endregion
    }
}
