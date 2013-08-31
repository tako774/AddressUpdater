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
    public class Th123TencoClient
    {
        /// <summary>Tenco!マイページ</summary>
        private Uri _mypage;
        /// <summary>レーティング</summary>
        private Collection<Th123Rating> _ratings;

        /// <summary>Tenco!アカウント名</summary>
        public readonly string AccountName;

        /// <summary>
        /// インスタンスの生成
        /// </summary>
        /// <param name="accountName">アカウント名</param>
        /// <exception cref="System.ArgumentNullException">account が 空 です。</exception>
        /// <exception cref="System.UriFormatException"></exception>
        public Th123TencoClient(string accountName)
        {
            if (string.IsNullOrEmpty(accountName))
                throw new System.ArgumentNullException("account");

            AccountName = accountName;
            _mypage = new Uri(string.Format("http://tenco.xrea.jp/game/2/account/{0}/output=xml", accountName));
            _ratings = new Collection<Th123Rating>();
        }

        /// <summary>
        /// レーティングの取得
        /// </summary>
        /// <returns>レーティング</returns>
        public ReadOnlyCollection<Th123Rating> GetRatings()
        {
            return new ReadOnlyCollection<Th123Rating>(_ratings);
        }

        /// <summary>
        /// サーバーからレーティング情報を取得
        /// </summary>
        /// <exception cref="System.Net.WebException"></exception>
        /// <exception cref="System.Xml.XmlException"></exception>
        public void UpdateRating()
        {
            var webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;
            try
            {
                var mypageXml = webClient.DownloadString(_mypage);
                Debug.WriteLine(mypageXml);

                var ratingDoc = new XmlDocument();
                ratingDoc.LoadXml(mypageXml);

                var ratings = new Collection<Th123Rating>();
                var characters = ratingDoc.SelectNodes("/gameAccount/account/game[id=2]/type1");
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

                    ratings.Add(new Th123Rating((Th123Characters)characterId, ratingValue, ratingDeviation, matchAccounts, matchCount));
                }

                _ratings = ratings;
            }
            catch (System.NotSupportedException) { }
        }
    }
}
