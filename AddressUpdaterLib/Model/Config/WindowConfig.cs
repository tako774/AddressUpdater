using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace HisoutenSupportTools.AddressUpdater.Lib.Model.Config
{
    /// <summary>
    /// ウィンドウ設定
    /// </summary>
    [Serializable]
    [XmlRoot("ウィンドウ設定")]
    public class WindowConfig
    {
        /// <summary>サーバー名</summary>
        [XmlElement("サーバー名")]
        public string ServerName = Application.ProductName;
        /// <summary>座標</summary>
        [XmlElement("座標")]
        public Point Location = new Point(0, 0);
        /// <summary>サイズ</summary>
        [XmlElement("サイズ")]
        public Size Size = new Size(740, 455);

        /// <summary>No列幅</summary>
        [XmlElement("No列幅")]
        public int NumberColumnWidth = 34;
        /// <summary>時刻列幅</summary>
        [XmlElement("時刻列幅")]
        public int TimeColumnWidth = 44;
        /// <summary>IP:ポート列幅</summary>
        [XmlElement("IP_ポート列幅")]
        public int IpPortColumnWidth = 123;
        /// <summary>ランク列幅</summary>
        [XmlElement("ランク列幅")]
        public int RankColumnWidth = 82;
        /// <summary>コメント列幅</summary>
        [XmlElement("コメント列幅")]
        public int CommentColumnWidth = 293;

        /// <summary>チャット欄スクロール方向を逆にするかどうか</summary>
        [XmlElement("チャット欄スクロール方向を逆にする")]
        public bool ReverseScroll = false;
        /// <summary>アドレス一覧⇔チャット一覧区切り線位置</summary>
        [XmlElement("区切り線位置")]
        public int SplitterDistance = 150;
        /// <summary>最大化</summary>
        [XmlElement("最大化")]
        public bool Maximized = false;

        #region 初期化
        /// <summary>
        /// インスタンスの生成
        /// </summary>
        public WindowConfig() { }

        /// <summary>
        /// インスタンスの生成
        /// </summary>
        /// <param name="serverName">サーバー名</param>
        public WindowConfig(string serverName)
        {
            ServerName = serverName;
        }
        #endregion
    }
}
