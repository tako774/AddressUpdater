namespace HisoutenSupportTools.AddressUpdater.Lib.Model
{
    /// <summary>
    /// 試合状況
    /// </summary>
    public enum MatchResults : int
    {
        /// <summary>未対戦</summary>
        [EnumText("")]
        None = 0,
        /// <summary>負け</summary>
        [EnumText("×")]
        Lose = 1,
        /// <summary>勝ち</summary>
        [EnumText("○")]
        Win = 2,
    }
}
