using System;
using System.Xml.Serialization;

namespace HisoutenSupportTools.AddressUpdater.Lib.Model.Config
{
    /// <summary>
    /// おまけ機能ウィンドウ情報
    /// </summary>
    [Serializable]
    [XmlRoot("ウィンドウ情報")]
    public class ExtraWindowInformation
    {
        /// <summary>キャプション</summary>
        [XmlElement("キャプション")]
        public string Caption = string.Empty;
        /// <summary>X</summary>
        [XmlElement("X")]
        public int X = 0;
        /// <summary>Y</summary>
        [XmlElement("Y")]
        public int Y = 0;
        /// <summary>幅</summary>
        [XmlElement("幅")]
        public int Width = 640;
        /// <summary>高さ</summary>
        [XmlElement("高さ")]
        public int Height = 480;
    }
}
