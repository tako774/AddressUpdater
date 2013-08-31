using System;
using System.Xml.Serialization;

namespace HisoutenSupportTools.AddressUpdater.Lib.Model.Config
{
    /// <summary>
    /// サーバー情報
    /// </summary>
    [Serializable]
    [XmlRoot("サーバー情報")]
    public class ServerInformation
    {
        /// <summary>サーバー名</summary>
        [XmlElement("サーバー名")]
        public string Name;
        /// <summary>Uri</summary>
        [XmlElement("アドレス")]
        public string Uri;
        /// <summary>表示するかどうか</summary>
        [XmlElement("表示する")]
        public bool Visible = true;

        /// <summary>
        /// インスタンスの生成
        /// </summary>
        public ServerInformation() { }

        /// <summary>
        /// インスタンスの生成
        /// </summary>
        /// <param name="name">サーバー名</param>
        /// <param name="uri">URI</param>
        public ServerInformation(string name, string uri)
            : this(name, uri, true)
        { }

        /// <summary>
        /// インスタンスの生成
        /// </summary>
        /// <param name="name">サーバー名</param>
        /// <param name="uri">URI</param>
        /// <param name="visible">表示するかどうか</param>
        public ServerInformation(string name, string uri, bool visible)
        {
            Name = name;
            Uri = uri;
            Visible = visible;
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
