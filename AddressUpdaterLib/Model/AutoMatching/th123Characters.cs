using System;

namespace HisoutenSupportTools.AddressUpdater.Lib.AddressService
{
    /// <summary>
    /// 
    /// </summary>
    public partial class th123Characters : IEquatable<th123Characters>, ICloneable
    {
        /// <summary></summary>
        public static th123Characters Reimu { get { return new th123Characters() { Value = REIMU }; } }
        /// <summary></summary>
        public static th123Characters Marisa { get { return new th123Characters() { Value = MARISA }; } }
        /// <summary></summary>
        public static th123Characters Sakuya { get { return new th123Characters() { Value = SAKUYA }; } }
        /// <summary></summary>
        public static th123Characters Alice { get { return new th123Characters() { Value = ALICE }; } }
        /// <summary></summary>
        public static th123Characters Patchouli { get { return new th123Characters() { Value = PATCHOULI }; } }
        /// <summary></summary>
        public static th123Characters Youmu { get { return new th123Characters() { Value = YOUMU }; } }
        /// <summary></summary>
        public static th123Characters Remilia { get { return new th123Characters() { Value = REMILIA }; } }
        /// <summary></summary>
        public static th123Characters Yuyuko { get { return new th123Characters() { Value = YUYUKO }; } }
        /// <summary></summary>
        public static th123Characters Yukari { get { return new th123Characters() { Value = YUKARI }; } }
        /// <summary></summary>
        public static th123Characters Suika { get { return new th123Characters() { Value = SUIKA }; } }
        /// <summary></summary>
        public static th123Characters Reisen { get { return new th123Characters() { Value = REISEN }; } }
        /// <summary></summary>
        public static th123Characters Aya { get { return new th123Characters() { Value = AYA }; } }
        /// <summary></summary>
        public static th123Characters Komachi { get { return new th123Characters() { Value = KOMACHI }; } }
        /// <summary></summary>
        public static th123Characters Iku { get { return new th123Characters() { Value = IKU }; } }
        /// <summary></summary>
        public static th123Characters Tenshi { get { return new th123Characters() { Value = TENSHI }; } }
        /// <summary></summary>
        public static th123Characters Sanae { get { return new th123Characters() { Value = SANAE }; } }
        /// <summary></summary>
        public static th123Characters Cirno { get { return new th123Characters() { Value = CIRNO }; } }
        /// <summary></summary>
        public static th123Characters Meirin { get { return new th123Characters() { Value = MEIRIN }; } }
        /// <summary></summary>
        public static th123Characters Utsuho { get { return new th123Characters() { Value = UTSUHO }; } }
        /// <summary></summary>
        public static th123Characters Suwako { get { return new th123Characters() { Value = SUWAKO }; } }

        private const int REIMU = 0;
        private const int MARISA = 1;
        private const int SAKUYA = 2;
        private const int ALICE = 3;
        private const int PATCHOULI = 4;
        private const int YOUMU = 5;
        private const int REMILIA = 6;
        private const int YUYUKO = 7;
        private const int YUKARI = 8;
        private const int SUIKA = 9;
        private const int REISEN = 10;
        private const int AYA = 11;
        private const int KOMACHI = 12;
        private const int IKU = 13;
        private const int TENSHI = 14;
        private const int SANAE = 15;
        private const int CIRNO = 16;
        private const int MEIRIN = 17;
        private const int UTSUHO = 18;
        private const int SUWAKO = 19;

        #region IEquatable<th123Characters> メンバ

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(th123Characters other)
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
            var clone = new th123Characters()
            {
                Value = Value,
            };

            return clone;
        }

        #endregion
    }
}
