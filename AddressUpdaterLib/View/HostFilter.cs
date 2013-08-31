using System.Collections.ObjectModel;
using HisoutenSupportTools.AddressUpdater.Lib.AddressService;

namespace HisoutenSupportTools.AddressUpdater.Lib.View
{
    /// <summary>
    /// ホスト・大会フィルター
    /// </summary>
    internal class HostFilter
    {
        /// <summary>
        /// 
        /// </summary>
        public Collection<string> IpList { get; private set; }


        /// <summary>
        /// インスタンスの生成
        /// </summary>
        internal HostFilter()
        {
            IpList = new Collection<string>();
        }


        /// <summary>
        /// フィルタ
        /// </summary>
        /// <param name="hosts"></param>
        /// <returns></returns>
        public Collection<host> Filter(Collection<host> hosts)
        {
            var filteredList = new Collection<host>();
            foreach (var host in hosts)
            {
                var hit = false;
                foreach (var ip in IpList)
                {
                    if (host.Ip == ip)
                    {
                        hit = true;
                        break;
                    }
                }

                if(!hit)
                    filteredList.Add((host)host.Clone());
            }

            return filteredList;
        }
    }
}
