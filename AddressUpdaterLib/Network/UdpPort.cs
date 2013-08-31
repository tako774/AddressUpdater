using System.Net.NetworkInformation;

namespace HisoutenSupportTools.AddressUpdater.Lib.Network
{
    /// <summary>
    /// UDPのポート
    /// </summary>
    public class UdpPort
    {
        /// <summary>
        /// 指定ポートの待受け状態取得
        /// </summary>
        /// <returns>true:待受け中 / false:待受け中でない</returns>
        /// <exception cref="NetworkInformationException">Win32 関数 GetUdpTable の呼び出しが失敗しました。</exception>
        public static bool GetIsListening(int port)
        {
            var udpListeners = IPGlobalProperties.GetIPGlobalProperties().GetActiveUdpListeners();
            foreach (var listener in udpListeners)
                if (listener.Port == port)
                    return true;

            return false;
        }
    }
}
