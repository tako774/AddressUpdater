using System;
using System.Xml.Serialization;

namespace HisoutenSupportTools.AddressUpdater.Lib.Model.Config
{
    /// <summary>
    /// オートマッチング情報
    /// </summary>
    [Serializable]
    [XmlRoot("オートマッチング情報")]
    public class AutoMatchingInformation
    {
        /// <summary>アカウント名</summary>
        [XmlElement("アカウント名")]
        public string AccountName = string.Empty;
        /// <summary>キャラ</summary>
        [XmlElement("キャラ")]
        public int Character = 0;
        /// <summary>ホスト可</summary>
        [XmlElement("ホスト可")]
        public bool IsHostable = false;
        /// <summary>室内のみ</summary>
        [XmlElement("室内のみ")]
        public bool IsRoomOnry = false;
        /// <summary>IP</summary>
        [XmlElement("IP")]
        public string Ip = string.Empty;
        /// <summary>Nice port</summary>
        [XmlElement("ポート")]
        public int Port = 10800;
        /// <summary>コメント</summary>
        [XmlElement("コメント")]
        public string Comment = string.Empty;
        /// <summary>マッチング幅</summary>
        [XmlElement("マッチング幅")]
        public int MatchingSpan = 150;
    }
}
