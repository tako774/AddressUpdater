using System;
using System.Xml.Serialization;

namespace HisoutenSupportTools.AddressUpdater.Lib.Model.Config
{
    /// <summary>
    /// テーマ
    /// </summary>
    [Serializable]
    [XmlRoot("テーマ")]
    public class Theme : IEquatable<Theme>
    {
        /// <summary>ツール背景色</summary>
        [XmlElement("ツール背景色")]
        public ColorData ToolBackColor { get; set; }
        /// <summary>一般テキスト色</summary>
        [XmlElement("一般テキスト色")]
        public ColorData GeneralTextColor { get; set; }
        /// <summary>待機中ホスト背景色</summary>
        [XmlElement("待機中ホスト背景色")]
        public ColorData WaitingHostBackColor { get; set; }
        /// <summary>対戦中ホスト背景色</summary>
        [XmlElement("対戦中ホスト背景色")]
        public ColorData FightingHostBackColor { get; set; }
        /// <summary>チャット文字色</summary>
        [XmlElement("チャット文字色")]
        public ColorData ChatForeColor { get; set; }
        /// <summary>チャット背景色</summary>
        [XmlElement("チャット背景色")]
        public ColorData ChatBackColor { get; set; }
        /// <summary>ホスト表示フォント</summary>
        [XmlElement("ホスト表示フォント")]
        public FontName HostFont { get; set; }
        /// <summary>チャット表示フォント</summary>
        [XmlElement("チャット表示フォント")]
        public FontName ChatFont { get; set; }

        #region IEquatable<Theme> メンバ

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Theme other)
        {
            if (other == null) return false;
            if (object.ReferenceEquals(this, other)) return true;

            return
                ToolBackColor == null ? other.ToolBackColor == null : ToolBackColor.Equals(other.ToolBackColor) &&
                GeneralTextColor == null ? other.GeneralTextColor == null : GeneralTextColor.Equals(other.GeneralTextColor) &&
                WaitingHostBackColor == null ? other.WaitingHostBackColor == null : WaitingHostBackColor.Equals(other.WaitingHostBackColor) &&
                FightingHostBackColor == null ? other.FightingHostBackColor == null : FightingHostBackColor.Equals(other.FightingHostBackColor) &&
                ChatForeColor == null ? other.ChatForeColor == null : ChatForeColor.Equals(other.ChatForeColor) &&
                ChatBackColor == null ? other.ChatBackColor == null : ChatBackColor.Equals(other.ChatBackColor) &&
                HostFont == other.HostFont &&
                ChatFont == other.ChatFont;
        }

        #endregion
    }
}
