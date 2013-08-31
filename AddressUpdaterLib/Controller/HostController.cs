using System;
using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using HisoutenSupportTools.AddressUpdater.Lib.AddressService;
using HisoutenSupportTools.AddressUpdater.Lib.Network;
using HisoutenSupportTools.AddressUpdater.Lib.Watcher;

namespace HisoutenSupportTools.AddressUpdater.Lib.Controller
{
    /// <summary>
    /// ホストコントローラー
    /// </summary>
    public partial class HostController : Component
    {
        #region フィールド
        /// <summary>ホスト</summary>
        private host _host;
        /// <summary>クライアント</summary>
        private IClient _client;
        /// <summary>対戦状態監視オブジェクト</summary>
        private IWatcher _watcher;
        /// <summary>サーバーに登録したかどうか</summary>
        private bool _isRegistered = false;
        /// <summary>対戦中に設定したかどうか</summary>
        private bool _isSetFighting = false;
        #endregion

        /// <summary>サーバー名の取得</summary>
        [Browsable(false)]
        public string ServerName { get { return _client.ServerName; } }

        /// <summary>
        /// 更新間隔の取得・設定
        /// </summary>
        public int Interval
        {
            get { return updateTimer.Interval; }
            set { updateTimer.Interval = value; }
        }

        /// <summary>
        /// クライアントの取得・設定
        /// </summary>
        public IClient Client
        {
            get { return _client; }
            set
            {
                _client = value;
                if (_client != null)
                    _client.ConsecutiveErrorHappened += new EventHandler(_client_ConsecutiveErrorHappened);
            }
        }

        /// <summary>
        /// 対戦状態監視オブジェクトの取得・設定
        /// </summary>
        public IWatcher Watcher
        {
            get { return _watcher; }
            set { _watcher = value; }
        }

        /// <summary>
        /// インスタンスの生成
        /// </summary>
        public HostController()
        {
            InitializeComponent();
        }

        /// <summary>
        /// インスタンスの生成
        /// </summary>
        /// <param name="container"></param>
        public HostController(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        /// <summary>
        /// インスタンスの生成
        /// </summary>
        /// <param name="client">クライアント</param>
        /// <param name="watcher">対戦状態監視オブジェクト</param>
        public HostController(IClient client, IWatcher watcher)
        {
            _client = client;
            _watcher = watcher;

            _client.ConsecutiveErrorHappened += new EventHandler(_client_ConsecutiveErrorHappened);
        }

        /// <summary>
        /// 登録開始
        /// </summary>
        /// <param name="host">登録するホスト</param>
        public void Start(host host)
        {
            _host = host;
            updateTimer.Enabled = true;
            updateTimer.Start();
            _watcher.Start();
        }

        /// <summary>
        /// 登録終了
        /// </summary>
        public void Stop()
        {
            _watcher.Stop();
            updateTimer.Enabled = false;
            updateTimer.Stop();

            if (!_isRegistered)
                return;

            try { _client.UnregisterHost(_host.Ip); }
            catch (CommunicationFailedException) { }
            finally
            {
                _isRegistered = false;
                _isSetFighting = false;
            }
        }

        /// <summary>
        /// 登録状態更新
        /// </summary>
        public void UpdateStatus()
        {
            // 非同期更新
            new MethodInvoker(DoUpdateStatus).BeginInvoke(null, null);
        }

        #region private
        /// <summary>
        /// 更新タイマー作動時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void updateTimer_Tick(object sender, EventArgs e)
        {
            UpdateStatus();
        }

        /// <summary>
        /// 登録する
        /// </summary>
        private void Register()
        {
            try
            {
                _client.RegisterHost(_host);
                _isRegistered = true;
            }
            catch (CommunicationFailedException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                _isRegistered = false;
            }
        }

        /// <summary>
        /// 対戦中状態を設定する
        /// </summary>
        private void SetFightingStatus()
        {
            if (_watcher.IsFighting)
            {
                // 対戦中に設定する場合、成功したら_isRegisteredFighting = true / 通信失敗したら_isRegisteredFighting = false
                try
                {
                    _client.SetFighting(_host.Ip, true);
                    _isSetFighting = true;
                }
                catch (CommunicationFailedException)
                {
                    _isSetFighting = false;
                }
            }
            else
            {
                // 対戦中ではない状態に設定する場合、成功したら_isRegisteredFighting = false / 通信失敗したら_isRegisteredFighting = true
                try
                {
                    _client.SetFighting(_host.Ip, false);
                    _isSetFighting = false;
                }
                catch (CommunicationFailedException)
                {
                    _isSetFighting = true;
                }
            }
        }

        /// <summary>
        /// 登録を削除する
        /// </summary>
        private void UnRegister()
        {
            try
            {
                _client.UnregisterHost(_host.Ip);
                _isRegistered = false;
                _isSetFighting = false;
            }
            catch (CommunicationFailedException) { }
        }

        /// <summary>
        /// 登録状態更新
        /// </summary>
        private void DoUpdateStatus()
        {
            if (updateTimer.Enabled) // 更新タイマー作動中
            {
                // 待ち受け状態
                var isListening = false;
                try
                {
                    isListening = UdpPort.GetIsListening(_host.Port);
                }
                catch (NetworkInformationException ex)
                {
                    // UDP状況の取得に失敗している
                    System.Diagnostics.Debug.WriteLine(ex);
                }

                // 待受け中で、登録されていないなら登録する
                if (isListening && !_isRegistered)
                    Register();
                // 待受け中で、登録されているなら何もしない
                if (isListening && _isRegistered) { }


                // 待受け中でなく、登録されているなら削除する
                if (!isListening && _isRegistered)
                    UnRegister();
                // 待受け中でなく、登録されていないなら何もしない
                if (!isListening && !_isRegistered) { }


                // 対戦中状態が登録と違うなら設定する
                if (_watcher.IsFighting != _isSetFighting)
                    SetFightingStatus();
            }
            else // 更新タイマー作動中ではない
            {
                // 登録されているなら、削除する
                if (_isRegistered)
                    UnRegister();
            }
        }
        #endregion

        /// <summary>
        /// 連続通信エラー発生時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _client_ConsecutiveErrorHappened(object sender, EventArgs e)
        {
            // 登録停止
            updateTimer.Enabled = false;
            updateTimer.Stop();
            _watcher.Stop();
            _isRegistered = false;
            _isSetFighting = false;
        }
    }
}
