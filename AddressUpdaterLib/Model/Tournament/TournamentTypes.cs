namespace HisoutenSupportTools.AddressUpdater.Lib.Model.Tournament
{
    /// <summary>
    /// 大会種別
    /// </summary>
    public enum TournamentTypes : int
    {
        /// <summary>不明</summary>
        [EnumText("不明")]
        不明 = 0,
        /// <summary>トーナメント</summary>
        [EnumText("トーナメント")]
        トーナメント = 1,
        ///// <summary>トーナメント(2人)</summary>
        //[EnumText("トーナメント(2人)")]
        //トーナメント2人 = 10,
        ///// <summary>トーナメント(3人)</summary>
        //[EnumText("トーナメント(3人)")]
        //トーナメント3人 = 20,
        /// <summary>総当たり</summary>
        [EnumText("総当たり")]
        総当たり = 2,
        /// <summary>総当たり(2人)</summary>
        [EnumText("総当たり(2人)")]
        総当たり2人 = 110,
        /// <summary>総当たり(3人)</summary>
        [EnumText("総当たり(3人)")]
        総当たり3人 = 120,
    }
}
