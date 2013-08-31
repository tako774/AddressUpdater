using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace HisoutenSupportTools.AddressUpdater.Lib.Network
{
    /// <summary>
    /// UPnP
    /// </summary>
    public partial class Upnp : Component
    {
        /// <summary>ポート開放UPnPスクリプトファイル名</summary>
        private const string OPEN_SCRIPT_FILENAME = "open.vbs";
        /// <summary>ポート閉じるスクリプトファイル名</summary>
        private const string CLOSE_SCRIPT_FILENAME = "close.vbs";

        /// <summary>ポート開けたかどうか</summary>
        private bool _opened = false;

        /// <summary>
        /// ポート
        /// </summary>
        private int _port = 10800;
        /// <summary>
        /// ポートの取得・設定
        /// </summary>
        [Description("ポート")]
        [DefaultValue(10800)]
        public int Port
        {
            get { return _port; }
            set { _port = value; }
        }

        #region 初期化
        /// <summary>
        /// インスタンスの生成
        /// </summary>
        public Upnp()
        {
            InitializeComponent();
        }

        /// <summary>
        /// インスタンスの生成
        /// </summary>
        /// <param name="container"></param>
        public Upnp(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        #endregion

        /// <summary>
        /// ポート開ける
        /// </summary>
        /// <returns>true:成功 / false:失敗</returns>
        public bool Open(string name)
        {
            if (!_opened)
                _opened = OpenPort(ProtocolType.Udp, name);

            return _opened;
        }

        /// <summary>
        /// ポート閉じる
        /// </summary>
        /// <returns>true:成功 / false:失敗</returns>
        public bool Close()
        {
            if (_opened)
                _opened = !ClosePort(ProtocolType.Udp);

            return !_opened;
        }

        #region private
        /// <summary>
        /// ポート開放
        /// </summary>
        /// <param name="protocol">プロトコル</param>
        /// <param name="name"></param>
        /// <returns>true:成功 / false:失敗</returns>
        private bool OpenPort(ProtocolType protocol, string name)
        {
            // 全部のNICで
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface nic in nics)
            {
                // IP取得（IPv4）してから
                UnicastIPAddressInformationCollection machineIPs = nic.GetIPProperties().UnicastAddresses;
                string machineIP = null;
                for (int i = 0; i < machineIPs.Count; i++)
                {
                    if (machineIPs[i].Address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        machineIP = machineIPs[i].Address.ToString();
                        break;
                    }
                }
                if (machineIP == null || machineIP == "127.0.0.1")
                    continue;

                // VBスクリプトで実行
                FileInfo scriptFile = new FileInfo(OPEN_SCRIPT_FILENAME);
                NatUPnPScript script = new NatUPnPScript();
                string scriptText = script.GetOpenScriptString(_port, ProtocolType.Udp, machineIP, name);
                try
                {
                    using (FileStream s = scriptFile.Create())
                    using (StreamWriter writer = new StreamWriter(s))
                    {
                        writer.Write(scriptText);
                        writer.Flush();
                        writer.Close();
                    }

                    if (!scriptFile.Exists)
                        continue;

                    using (Process openPortProcess = new Process())
                    {
                        openPortProcess.StartInfo = new ProcessStartInfo(scriptFile.FullName);
                        if (openPortProcess.Start())
                        {
                            while (!openPortProcess.HasExited)
                            {
                                System.Threading.Thread.Sleep(500);
                            }
                            if (openPortProcess.ExitCode == 0)
                                return true;
                        }
                    }
                }
                finally
                {
                    if (scriptFile.Exists)
                        scriptFile.Delete();
                }
            }

            return false;
        }

        /// <summary>
        /// ポート閉じる
        /// </summary>
        /// <param name="protocol">プロトコル</param>
        /// <returns>true:成功 / false:失敗</returns>
        private bool ClosePort(ProtocolType protocol)
        {
            // 全部のNICで
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface nic in nics)
            {
                // IP取得（IPv4）してから
                UnicastIPAddressInformationCollection machineIPs = nic.GetIPProperties().UnicastAddresses;
                string machineIP = null;
                for (int i = 0; i < machineIPs.Count; i++)
                {
                    if (machineIPs[i].Address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        machineIP = machineIPs[i].Address.ToString();
                        break;
                    }
                }
                if (machineIP == null || machineIP == "127.0.0.1")
                    continue;

                // VBスクリプトで実行
                FileInfo scriptFile = new FileInfo(CLOSE_SCRIPT_FILENAME);
                NatUPnPScript script = new NatUPnPScript();
                string scriptText = script.GetCloseScriptString(_port, ProtocolType.Udp);
                try
                {
                    using (FileStream s = scriptFile.Create())
                    using (StreamWriter writer = new StreamWriter(s))
                    {
                        writer.Write(scriptText);
                        writer.Flush();
                        writer.Close();
                    }

                    if (!scriptFile.Exists)
                        continue;

                    using (Process openPortProcess = new Process())
                    {
                        openPortProcess.StartInfo = new ProcessStartInfo(scriptFile.FullName);
                        if (openPortProcess.Start())
                        {
                            while (!openPortProcess.HasExited)
                            {
                                System.Threading.Thread.Sleep(500);
                            }
                            if (openPortProcess.ExitCode == 0)
                                return true;
                        }
                    }
                }
                finally
                {
                    if (scriptFile.Exists)
                        scriptFile.Delete();
                }
            }

            return false;
        }

        /// <summary>
        /// NATUPnPスクリプト
        /// </summary>
        private class NatUPnPScript
        {
            private const string CREATE_OBJECT_SCRIPT =
                "Dim portMappings\r\n" +
                "Set portMappings = CreateObject(\"HNetCfg.NATUPnP\").StaticPortMappingCollection\r\n" +
                "If portMappings Is Nothing Then WScript.Quit(1)\r\n";

            public string GetOpenScriptString(int port, ProtocolType protocolType, string machineIp, string name)
            {
                return CREATE_OBJECT_SCRIPT +
                    string.Format("Call portMappings.Add({0}, \"{1}\", {2}, \"{3}\", TRUE, \"{4}\")\r\n",
                    port,
                    protocolType.ToString().ToUpper(),
                    port,
                    machineIp,
                    name);
            }

            public string GetCloseScriptString(int port, ProtocolType protocolType)
            {
                return CREATE_OBJECT_SCRIPT +
                    string.Format("Call portMappings.Remove({0}, \"{1}\")\r\n",
                    port,
                    protocolType.ToString().ToUpper());
            }
        }
        #endregion
    }
}
