using System.Net.NetworkInformation;

namespace HisoutenSupportTools.AddressUpdater.Lib.Network
{
    /// <summary>
    /// TCPのポート
    /// </summary>
    public class TcpPort
    {
        /// <summary>
        /// 指定ポートの待受け状態取得
        /// </summary>
        /// <returns>true:待受け中 / false:待受け中でない</returns>
        /// <exception cref="NetworkInformationException">Win32 関数 GetTcpTable が失敗しました。</exception>
        public static bool GetIsListening(int port)
        {
            var udpListeners = IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpListeners();
            foreach (var listener in udpListeners)
                if (listener.Port == port)
                    return true;

            return false;
        }
    }
}
