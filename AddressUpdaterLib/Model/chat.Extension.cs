using System;

namespace HisoutenSupportTools.AddressUpdater.Lib.AddressService
{
    /// <summary>
    /// chat拡張
    /// </summary>
    /// <remarks>Webサービスで使用するchatクラスを拡張</remarks>
	public partial class chat : IEquatable<chat>, ICloneable
	{
        /// <summary></summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (string.IsNullOrEmpty(Name))
                return string.Format("{0:HH:mm:ss} [{1}] : {2}", Time, Id, Contents);


            var contents = Contents.TrimEnd();
            var nameString = Name;
            var lastChar = Name.Substring(Name.Length - 1, 1);
            if (lastChar == ":" || lastChar == "：")
                nameString = Name.Substring(0, Name.Length - 1);

            return string.Format("{0:HH:mm:ss} [{1}] : {2}：{3}", Time, Id, nameString, Contents);
        }

        #region IEquatable<chat> メンバ

        /// <summary></summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(chat other)
        {
            if (other == null) return false;
            if (object.ReferenceEquals(this, other)) return true;

            return
                Time == other.Time &&
                Id.Equals(other.Id) &&
                Name == other.Name &&
                Contents == other.Contents;
        }

        #endregion

        #region ICloneable メンバ

        /// <summary></summary>
        /// <returns></returns>
        public object Clone()
        {
            return new chat()
            {
                Id = (id)Id.Clone(),
                Time = Time,
                Name = Name,
                Contents = Contents
            };
        }

        #endregion
    }
}
