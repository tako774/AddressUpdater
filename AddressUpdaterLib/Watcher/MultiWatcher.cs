using System;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using HisoutenSupportTools.AddressUpdater.Lib.Model;

namespace HisoutenSupportTools.AddressUpdater.Lib.Watcher
{
    /// <summary>
    /// 対戦状態監視
    /// </summary>
    public class MultiWatcher : IWatcher
    {
        /// <summary></summary>
        private Timer _watchTimer;

        #region プロパティ
        /// <summary>ゲーム情報</summary>
        public Collection<GameInformation> GameInformations { get; set; }

        /// <summary>状態更新間隔の取得・設定（単位：ミリ秒）</summary>
        public int Interval
        {
            get { return _watchTimer.Interval; }
            set
            {
                if (_watchTimer.Interval == value)
                    return;
                _watchTimer.Interval = value;
            }
        }
        #endregion

        #region 初期化
        /// <summary>
        /// インスタンスの生成
        /// ゲーム情報は、サーバー側の設定を読み込む
        /// </summary>
        public MultiWatcher()
        {
            _watchTimer = new Timer();
            _watchTimer.Interval = 2500;
            _watchTimer.Tick += new EventHandler(_watchTimer_Tick);

            GameInformations = new Collection<GameInformation>();
        }
        #endregion

        #region IWatcher メンバ

        /// <summary>対戦状態が変化した時に発生します。</summary>
        public event EventHandler<EventArgs<bool>> StatusChanged;

        /// <summary>通信対戦中かどうか</summary>
        public bool IsFighting
        {
            get { return _isFighting; }
            private set
            {
                if (_isFighting == value)
                    return;
                _isFighting = value;
                if (StatusChanged != null)
                    StatusChanged(this, new EventArgs<bool>(value));
            }
        }
        private bool _isFighting;

        /// <summary>監視開始</summary>
        public void Start()
        {
            _watchTimer.Enabled = true;
            _watchTimer.Start();
        }

        /// <summary>監視終了</summary>
        public void Stop()
        {
            _watchTimer.Enabled = false;
            _watchTimer.Stop();
            IsFighting = false;
        }

        #endregion

        /// <summary>
        /// 監視タイマー作動時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _watchTimer_Tick(object sender, EventArgs e)
        {
            GameInformation hitGame = null;
            foreach (var gameInformation in GameInformations)
            {
                if (gameInformation.GetIsFighting())
                {
                    hitGame = gameInformation;
                    break;
                }
            }

            if (hitGame != null)
            {
                IsFighting = true;

                // 検出できたのがあれば最優先で処理したいので並べ替えておく
                if (GameInformations[0] != hitGame)
                {
                    lock (GameInformations)
                    {
                        GameInformations.Remove(hitGame);
                        GameInformations.Insert(0, hitGame);
                    }
                }
            }
            else
            {
                IsFighting = false;
            }
        }
    }
}
