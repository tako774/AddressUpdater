using System;
using System.ComponentModel;
using System.Diagnostics;
using HisoutenSupportTools.AddressUpdater.Lib.Api;

namespace HisoutenSupportTools.AddressUpdater.Lib.Watcher
{
    /// <summary>
    /// 緋想天 対戦状態監視コンポーネント
    /// </summary>
    public partial class Th105Watcher : Component, IWatcher
    {
        /// <summary>th105 process</summary>
        private Process _th105 = null;
        /// <summary>scene pointer</summary>
        private IntPtr _scenePtr;

        #region プロパティ
        /// <summary>
        /// ウィンドウキャプションの取得・設定
        /// </summary>
        [Description("緋想天のウィンドウキャプション")]
        [DefaultValue("東方緋想天 Ver1.06")]
        [Localizable(true)]
        [Bindable(true)]
        public string WindowCaption
        {
            get { return _windowCaption; }
            set { _windowCaption = value; }
        }
        private string _windowCaption;

        /// <summary>
        /// クラス名の取得・設定
        /// </summary>
        [Description("緋想天のクラス名")]
        [DefaultValue("th105_106")]
        [Localizable(true)]
        [Bindable(true)]
        public string ClassName
        {
            get { return _className; }
            set
            {
                if (_className == value)
                    return;
                _className = value;
            }
        }
        private string _className;

        /// <summary>
        /// シーンID格納アドレスの取得・設定
        /// </summary>
        [Description("シーンID格納アドレス")]
        [DefaultValue("0x006ECE78")]
        [Localizable(true)]
        [Bindable(true)]
        public string SceneIdAddress
        {
            get
            {
                if (IntPtr.Size == sizeof(long))
                    return string.Format("0x{0}", _scenePtr.ToString("X16"));
                else
                    return string.Format("0x{0}", _scenePtr.ToString("X8"));
            }
            set
            {
                if (_sceneIdAddress == value)
                    return;

                _sceneIdAddress = value;
                if (IntPtr.Size == sizeof(long))
                    _scenePtr = new IntPtr(Convert.ToInt64(value, 16));
                else
                    _scenePtr = new IntPtr(Convert.ToInt32(value, 16));
            }
        }
        private string _sceneIdAddress;

        /// <summary>
        /// 対戦中を示すシーン値
        /// </summary>
        [Description("対戦中を示すシーン値")]
        [DefaultValue(new byte[] { 8, 9, 10, 11, 13, 14 })]
        [Localizable(true)]
        [Bindable(true)]
        public byte[] FightingScenes
        {
            get { return _fightingScenes; }
            set { _fightingScenes = value; }
        }
        private byte[] _fightingScenes;

        /// <summary>
        /// 状態更新間隔の取得・設定（単位：ミリ秒）
        /// </summary>
        [Description("状態更新間隔（単位：ミリ秒）")]
        [DefaultValue(2500)]
        [Localizable(true)]
        [Bindable(true)]
        public int Interval
        {
            get { return watchTimer.Interval; }
            set { watchTimer.Interval = value; }
        }
        #endregion

        #region 初期化
        /// <summary>
        /// インスタンスの生成
        /// </summary>
        public Th105Watcher()
        {
            InitializeComponent();


            Initialize();
        }

        /// <summary>
        /// インスタンスの生成
        /// </summary>
        /// <param name="container"></param>
        public Th105Watcher(IContainer container)
        {
            container.Add(this);

            InitializeComponent();


            Initialize();
        }

        /// <summary>
        /// 初期化
        /// </summary>
        private void Initialize()
        {
            WindowCaption = "東方緋想天 Ver1.06";
            ClassName = "th105_106";
            SceneIdAddress = "0x006ECE78";
            FightingScenes = new byte[] { 8, 9, 10, 11, 13, 14 };
        }
        #endregion

        #region IWatcher メンバ

        /// <summary>対戦状態が変化した時に発生します。</summary>
        public event EventHandler<EventArgs<bool>> StatusChanged;

        /// <summary>
        /// 通信対戦中かどうか
        /// </summary>
        [Description("通信対戦中かどうか")]
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

        /// <summary>
        /// 監視開始
        /// </summary>
        public void Start()
        {
            watchTimer.Enabled = true;
            watchTimer.Start();
        }

        /// <summary>
        /// 監視終了
        /// </summary>
        public void Stop()
        {
            watchTimer.Enabled = false;
            watchTimer.Stop();
            IsFighting = false;

            if (_th105 != null)
                _th105 = null;
        }

        #endregion

        #region private
        /// <summary>
        /// 監視タイマー作動時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void watchTimer_Tick(object sender, EventArgs e)
        {
            // プロセス応答確認
            if (_th105 != null)
            {
                if (!CheckResponding())
                    return;
            }

            // プロセス取得
            if (_th105 == null)
            {
                if (!GetProcess())
                    return;
            }

            // シーン検出
            try
            {
                var scene = Kernel32.ReadProcessMemory(_th105.Handle, _scenePtr);
                IsFighting = IsFightingScene(scene);
            }
            catch (InvalidOperationException) { IsFighting = false; }
            catch (NotSupportedException) { IsFighting = false; }
            catch (ReadProcessMemoryFailedException) { IsFighting = false; }
        }

        /// <summary>
        /// 対戦中のシーンIDかどうか
        /// </summary>
        /// <param name="sceneId">シーンID</param>
        /// <returns>true:対戦中 / false:対戦中じゃない</returns>
        private bool IsFightingScene(byte sceneId)
        {
            foreach (var scene in _fightingScenes)
            {
                if (scene == sceneId)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// 応答確認
        /// </summary>
        /// <returns></returns>
        private bool CheckResponding()
        {
            bool result = false;
            // プロセス応答確認
            try
            {
                if (!_th105.Responding)
                    _th105 = null;
                else
                    result = true;
            }
            catch (PlatformNotSupportedException) { _th105 = null; }
            catch (InvalidOperationException) { _th105 = null; }
            catch (NotSupportedException) { _th105 = null; }
            return result;
        }

        /// <summary>
        /// プロセス取得
        /// </summary>
        /// <returns></returns>
        private bool GetProcess()
        {
            // ウィンドウハンドル取得
            IntPtr hisoutenWindowHandle = IntPtr.Zero;
            if (ClassName != null)
                hisoutenWindowHandle = User32.FindWindow(ClassName, WindowCaption);
            else
                hisoutenWindowHandle = User32.FindWindow(WindowCaption);
            if (hisoutenWindowHandle == IntPtr.Zero)
            {
                _th105 = null;
                return false;
            }

            // プロセスID取得
            uint hisoutenProcessId;
            User32.GetWindowThreadProcessId(hisoutenWindowHandle, out hisoutenProcessId);
            if (hisoutenProcessId == 0)
            {
                _th105 = null;
                return false;
            }

            // 緋想天のプロセス取得
            try
            {
                _th105 = Process.GetProcessById((int)hisoutenProcessId);
                return true;
            }
            catch (ArgumentException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                _th105 = null;
                return false;
            }
        }
        #endregion
    }
}
