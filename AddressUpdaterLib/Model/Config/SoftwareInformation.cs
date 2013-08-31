using System;
using System.IO;
using System.Xml.Serialization;

namespace HisoutenSupportTools.AddressUpdater.Lib.Model.Config
{
    /// <summary>
    /// ソフト情報
    /// </summary>
    [Serializable]
    [XmlRoot("ソフト情報")]
    public class SoftwareInformation
    {
        /// <summary>ファイルの名前</summary>
        [XmlElement("ファイル名")]
        public string Name;
        /// <summary>ファイルのパス</summary>
        [XmlElement("パス")]
        public string Path;
        /// <summary>起動するかどうか</summary>
        [XmlElement("起動する")]
        public bool Boot;

        /// <summary>ファイルが存在するかどうか</summary>
        public bool Exists { get { return File.Exists(Path); } }

        /// <summary>
        /// インスタンスの生成
        /// </summary>
        public SoftwareInformation() { }

        /// <summary>
        /// インスタンスの生成
        /// </summary>
        /// <param name="name">ファイルの名前</param>
        /// <param name="path">ファイルのパス</param>
        /// <param name="boot">同時に起動するかどうか</param>
        public SoftwareInformation(string name, string path, bool boot)
        {
            Name = name;
            Path = path;
            Boot = boot;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
