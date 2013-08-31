using System;
using System.Collections.ObjectModel;
using HisoutenSupportTools.AddressUpdater.Lib.AddressService;

namespace HisoutenSupportTools.AddressUpdater.Lib.Controller
{
    /// <summary>
    /// 管理者クライアント
    /// </summary>
    public class AdminClient : Client
    {
        /// <summary>
        /// 管理者キーワード
        /// </summary>
        private readonly string _keyword;

        /// <summary>
        /// インスタンスの生成
        /// </summary>
        /// <param name="serverName">サーバー名</param>
        /// <param name="serverURI">サーバーURI</param>
        /// <param name="keyword">管理者キーワード</param>
        public AdminClient(string serverName, string serverURI, string keyword)
            : base(serverName, serverURI)
        {
            _keyword = keyword;
        }

        #region
        /// <summary>
        /// 強制ホスト登録
        /// </summary>
        /// <param name="host">ホスト</param>
        public void ForceRegisterHost(host host)
        {
            _server.ForceRegisterHost(_keyword, host);
        }

        /// <summary>
        /// 強制ホスト登録解除
        /// </summary>
        /// <param name="no">No</param>
        /// <param name="ip">IP</param>
        public void ForceUnregisterHost(int no, string ip)
        {
            _server.ForceUnregisterHost(_keyword, no, ip);
        }

        /// <summary>
        /// 強制大会削除
        /// </summary>
        /// <param name="tournamentNo">大会番号</param>
        public void ForceUnregisterTournament(int tournamentNo)
        {
            _server.ForceUnregisterTournament(_keyword, tournamentNo);
        }

        /// <summary>
        /// 強制対戦状態設定
        /// </summary>
        /// <param name="no">No</param>
        /// <param name="ip">IP</param>
        /// <param name="isFighting">対戦中かどうか</param>
        public void ForceSetFighting(int no, string ip, bool isFighting)
        {
            _server.ForceSetFighting(_keyword, no, ip, isFighting);
        }

        /// <summary>
        /// 管理者チャットする
        /// </summary>
        /// <param name="name">名前</param>
        /// <param name="contents">内容</param>
        public void AdminChat(string name, string contents)
        {
            _server.AdminChat(_keyword, name + contents);
        }

        /// <summary>
        /// アナウンスを設定する
        /// </summary>
        /// <param name="announces">アナウンス</param>
        public void SetAnnounces(Collection<string> announces)
        {
            _server.SetAnnounces(_keyword, announces);
        }

        /// <summary>
        /// アナウンスする
        /// </summary>
        /// <param name="contents">内容</param>
        [Obsolete]
        public void Announce(string contents)
        {
            _server.Announce(_keyword, contents);
        }

        /// <summary>
        /// アナウンスを削除する
        /// </summary>
        /// <param name="no">No(1~</param>
        [Obsolete]
        public void RemoveAnnounce(int no)
        {
            _server.RemoveAnnounce(_keyword, no);
        }

        /// <summary>
        /// アナウンスをクリアする
        /// </summary>
        [Obsolete]
        public void ClearAnnounce()
        {
            _server.ClearAnnounce(_keyword);
        }

        /// <summary>
        /// ホスト情報をクリアする
        /// </summary>
        public void ClearHosts()
        {
            _server.ClearHosts(_keyword);
        }

        /// <summary>
        /// 大会情報をクリアする
        /// </summary>
        public void ClearTournaments()
        {
            _server.ClearTournaments(_keyword);
        }

        /// <summary>
        /// チャット情報をクリアする
        /// </summary>
        public void ClearChat()
        {
            _server.ClearChat(_keyword);
        }

        /// <summary>
        /// IPを取得する
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>IP</returns>
        public string GetIpById(string id)
        {
            return _server.GetIpById(_keyword, id);
        }

        /// <summary>
        /// リモート管理を許可する
        /// </summary>
        public void AllowRemoteAdmin()
        {
            _server.AllowRemoteAdmin(_keyword);
        }

        /// <summary>
        /// リモート管理を禁止する
        /// </summary>
        public void DenyRemoteAdmin()
        {
            _server.DenyRemoteAdmin(_keyword);
        }
        #endregion
    }
}
