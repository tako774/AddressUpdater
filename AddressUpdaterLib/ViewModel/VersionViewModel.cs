using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Web.Services.Protocols;
using System.Windows.Forms;
using HisoutenSupportTools.AddressUpdater.Lib.Api;
using HisoutenSupportTools.AddressUpdater.Lib.Network;

namespace HisoutenSupportTools.AddressUpdater.Lib.ViewModel
{
    /// <summary>
    /// 
    /// </summary>
    public partial class VersionViewModel : ViewModelBase
    {
        /// <summary>ウィンドウ情報</summary>
        public Dictionary<string,string> WindowInformations
        {
            get { return _windowInformations; }
            private set
            {
                if (_windowInformations == value)
                    return;

                _windowInformations = value;
                OnPropertyChanged("WindowInformations");
            }
        }
        private Dictionary<string, string> _windowInformations = new Dictionary<string, string>();

        /// <summary>
        /// 
        /// </summary>
        public VersionViewModel()
        {
            InitializeComponent();

            Initialize();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        public VersionViewModel(IContainer container)
        {
            container.Add(this);

            InitializeComponent();

            Initialize();
        }

        void Initialize()
        {
            _windowInformations.Add("東方非想天則 ～ 超弩級ギニョルの謎を追え Ver1.03", "th123_103");
            _windowInformations.Add("東方緋想天 Ver1.06", "th105_106");
        }


        #region AddWindowInformation
        /// <summary>
        /// ウィンドウ情報の追加
        /// </summary>
        /// <param name="caption">キャプション</param>
        /// <param name="className">クラス名</param>
        public void AddWindowInformation(string caption, string className)
        {
            if (caption == null)
                return;

            string buffer;
            var exists = _windowInformations.TryGetValue(caption, out buffer);
            if (exists)
                return;

            _windowInformations.Add(caption, className);
            OnPropertyChanged("WindowInformations");
        }
        #endregion

        #region CheckVersion
        /// <summary>
        /// バージョン確認
        /// </summary>
        /// <returns>確認結果</returns>
        /// <exception cref="HisoutenSupportTools.AddressUpdater.Lib.Network.CommunicationFailedException"></exception>
        public CheckVersionResults CheckVersion()
        {
            var version = new Version(Application.ProductVersion);

            using (var service = new VersionCheckService.VersionCheckService())
            {
                try
                {
                    var latestVersion = new Version(service.getClientVersion());
                    if (latestVersion <= version)
                        return CheckVersionResults.Latest;
                    else
                        return CheckVersionResults.ExistsNew;
                }
                catch (WebException ex) { throw new CommunicationFailedException(ex); }
                catch (SoapException ex) { throw new CommunicationFailedException(ex); }
            }
        }
        #endregion

        #region OpenBrowser
        /// <summary>
        /// ブラウザを開く
        /// </summary>
        /// <param name="uri">uri</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.ComponentModel.Win32Exception"></exception>
        public void OpenBrowser(string uri)
        {
            if (string.IsNullOrEmpty(uri))
                throw new ArgumentNullException("uri");

            try { Process.Start(uri); }
            catch (ObjectDisposedException) { }
        }
        #endregion
    }

    /// <summary>
    /// バージョンチェック結果
    /// </summary>
    public enum CheckVersionResults
    {
        /// <summary>最新のバージョン</summary>
        Latest,
        /// <summary>新バージョンが存在</summary>
        ExistsNew,
    }
}
