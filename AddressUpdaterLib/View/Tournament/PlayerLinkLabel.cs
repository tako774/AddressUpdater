using System.Windows.Forms;
using HisoutenSupportTools.AddressUpdater.Lib.AddressService;

namespace HisoutenSupportTools.AddressUpdater.Lib.View.Tournament
{
    /// <summary>
    /// プレイヤーリンクラベル
    /// </summary>
    public class PlayerLinkLabel : LinkLabel
    {
        /// <summary>
        /// プレイヤーの取得・設定
        /// </summary>
        public player Player
        {
            get { return _player; }
            set
            {
                if (_player == null)
                {
                    _player = value;
                    return;
                }

                if (_player.Equals(value))
                    return;
                _player = value;

                Text = _player.entryName;
            }
        }
        private player _player;
    }
}
