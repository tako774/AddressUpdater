namespace HisoutenSupportTools.AddressUpdater.Lib.ViewModel
{
    /// <summary>
    /// 自動マッチング 進行状況
    /// </summary>
    public enum MatchingPhase : int
    {
        /// <summary>空</summary>
        Empty,

        /// <summary>マッチング中</summary>
        Matching,

        /// <summary>ホスト準備中</summary>
        HostGettingReady,
        /// <summary>クライアントの接続待ち</summary>
        WaitingClientConnecting,

        /// <summary>ホストの準備待ち</summary>
        WaitingHostGettingReady,
        /// <summary>試合開始待ち</summary>
        WaitingFightStart,
    }
}
