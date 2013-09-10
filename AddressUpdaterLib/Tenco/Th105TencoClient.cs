using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Xml;

namespace HisoutenSupportTools.AddressUpdater.Lib.Tenco
{
    /// <summary>
    /// Tenco!クライアント
    /// </summary>
    public class Th105TencoClient
    {
        /// <summary>Tenco!マイページ</summary>
        private Uri _mypage;
        /// <summary>レーティング</summary>
        private Collection<Th105Rating> _ratings;

        /// <summary>
        /// インスタンスの生成
        /// </summary>
        /// <param name="account">アカウント名</param>
        /// <exception cref="System.ArgumentNullException">account が 空 です。</exception>
        /// <exception cref="System.UriFormatException"></exception>
        public Th105TencoClient(string account)
        {
            if (string.IsNullOrEmpty(account))
                throw new System.ArgumentNullException("account");
            _mypage = new Uri(string.Format("http://tenco.info/game/{1}/account/{0}/output=xml", account, (int)Games.Th105));
        }

        /// <summary>
        /// レーティングの取得
        /// </summary>
        /// <returns>レーティング</returns>
        public ReadOnlyCollection<Th105Rating> GetRatings()
        {
            return new ReadOnlyCollection<Th105Rating>(_ratings);
        }

        /// <summary>
        /// サーバーからレーティング情報を取得
        /// </summary>
        /// <exception cref="System.Net.WebException"></exception>
        /// <exception cref="System.Xml.XmlException"></exception>
        public void UpdateRating()
        {
            var webClient = new WebClient();
            var asbName = System.Reflection.Assembly.GetExecutingAssembly().GetName();

            webClient.Encoding = Encoding.UTF8;
            webClient.Headers.Add(HttpRequestHeader.UserAgent, String.Format("{0}/{1}", asbName.Name, asbName.Version));
            try
            {
                var mypageXml = webClient.DownloadString(_mypage);
                Debug.WriteLine(mypageXml);

                var ratingDoc = new XmlDocument();
                ratingDoc.LoadXml(mypageXml);

                var ratings = new Collection<Th105Rating>();
                var characters = ratingDoc.SelectNodes(String.Format("/gameAccount/account/game[id={0}]/type1", (int)Games.Th105));
                foreach (XmlNode character in characters)
                {
                    var characterIdNode = character.SelectSingleNode("id");
                    var ratingValueNode = character.SelectSingleNode("rating_value");
                    var ratingDeviationNode = character.SelectSingleNode("ratings_deviation");
                    var matchAccountsNode = character.SelectSingleNode("matched_accounts");
                    var matchCountNode = character.SelectSingleNode("match_counts");

                    var characterId = byte.Parse(characterIdNode.FirstChild.Value);
                    var ratingValue = int.Parse(ratingValueNode.FirstChild.Value);
                    var ratingDeviation = int.Parse(ratingDeviationNode.FirstChild.Value);
                    var matchAccounts = int.Parse(matchAccountsNode.FirstChild.Value);
                    var matchCount = int.Parse(matchCountNode.FirstChild.Value);

                    ratings.Add(new Th105Rating((Th105Characters)characterId, ratingValue, ratingDeviation, matchAccounts, matchCount));
                }

                _ratings = ratings;
            }
            catch (System.NotSupportedException) { }
        }
    }
}
