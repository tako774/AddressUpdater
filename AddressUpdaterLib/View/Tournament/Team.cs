using System.Collections.ObjectModel;
using HisoutenSupportTools.AddressUpdater.Lib.AddressService;

namespace HisoutenSupportTools.AddressUpdater.Lib.View.Tournament
{
    /// <summary>
    /// チーム
    /// </summary>
    public class Team
    {
        /// <summary>
        /// プレイヤーの取得
        /// </summary>
        public Collection<player> Players { get; private set; }

        /// <summary>
        /// インスタンスの生成
        /// </summary>
        /// <param name="players">チーム構成員</param>
        public Team(Collection<player> players)
        {
            Players = players;
        }
    }
}
