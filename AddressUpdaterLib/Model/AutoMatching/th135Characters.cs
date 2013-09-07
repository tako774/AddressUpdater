using System;

namespace HisoutenSupportTools.AddressUpdater.Lib.AddressService
{
    /// <summary>
    /// 
    /// </summary>
    public partial class th135Characters : IEquatable<th135Characters>, ICloneable
    {
        /// <summary></summary>
        public static th135Characters Reimu { get { return new th135Characters() { Value = REIMU }; } }
        /// <summary></summary>
        public static th135Characters Marisa { get { return new th135Characters() { Value = MARISA }; } }
        /// <summary></summary>
        public static th135Characters Ichirin { get { return new th135Characters() { Value = ICHIRIN }; } }
        /// <summary></summary>
        public static th135Characters Hijiri { get { return new th135Characters() { Value = HIJIRI }; } }
        /// <summary></summary>
        public static th135Characters Futo { get { return new th135Characters() { Value = FUTO }; } }
        /// <summary></summary>
        public static th135Characters Miko { get { return new th135Characters() { Value = MIKO }; } }
        /// <summary></summary>
        public static th135Characters Nitori { get { return new th135Characters() { Value = NITORI }; } }
        /// <summary></summary>
        public static th135Characters Koishi { get { return new th135Characters() { Value = KOISHI }; } }
        /// <summary></summary>
        public static th135Characters Mamizou { get { return new th135Characters() { Value = MAMIZOU }; } }
        /// <summary></summary>
        public static th135Characters Kokoro { get { return new th135Characters() { Value = kOKORO }; } }

        private const int REIMU = 0;
        private const int MARISA = 1;
        private const int ICHIRIN = 2;
        private const int HIJIRI = 3;
        private const int FUTO = 4;
        private const int MIKO = 5;
        private const int NITORI = 6;
        private const int KOISHI = 7;
        private const int MAMIZOU = 8;
        private const int kOKORO = 9;

        #region IEquatable<th135Characters> メンバ

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(th135Characters other)
        {
            return Value == other.Value;
        }

        #endregion

        #region ICloneable メンバ

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            var clone = new th135Characters()
            {
                Value = Value,
            };

            return clone;
        }

        #endregion
    }
}
