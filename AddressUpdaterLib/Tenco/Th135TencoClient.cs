using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Xml;

namespace HisoutenSupportTools.AddressUpdater.Lib.Tenco
{
    /// <summary>
    /// Tenco!クライアント(非)
    /// </summary>
    public class Th135TencoClient
    {
        /// <summary>Tenco!マイページ</summary>
        private Uri _mypage;
        /// <summary>レーティング</summary>
        private Collection<Th135Rating> _ratings;

        /// <summary>Tenco!アカウント名</summary>
        public readonly string AccountName;

        /// <summary>
        /// インスタンスの生成
        /// </summary>
        /// <param name="accountName">アカウント名</param>
        /// <exception cref="System.ArgumentNullException">account が 空 です。</exception>
        /// <exception cref="System.UriFormatException"></exception>
        public Th135TencoClient(string accountName)
        {
            if (string.IsNullOrEmpty(accountName))
                throw new System.ArgumentNullException("account");

            AccountName = accountName;
            _mypage = new Uri(string.Format("http://tenco.info/game/{1}/account/{0}/output=xml", accountName, (int)Games.Th135));
            _ratings = new Collection<Th135Rating>();
        }

        /// <summary>
        /// レーティングの取得
        /// </summary>
        /// <returns>レーティング</returns>
        public ReadOnlyCollection<Th135Rating> GetRatings()
        {
            return new ReadOnlyCollection<Th135Rating>(_ratings);
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

                var ratings = new Collection<Th135Rating>();
                var characters = ratingDoc.SelectNodes(String.Format("/gameAccount/account/game[id={0}]/type1", (int)Games.Th135));
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

                    ratings.Add(new Th135Rating((Th135Characters)characterId, ratingValue, ratingDeviation, matchAccounts, matchCount));
                }

                _ratings = ratings;
            }
            catch (System.NotSupportedException) { }
        }
    }
}
