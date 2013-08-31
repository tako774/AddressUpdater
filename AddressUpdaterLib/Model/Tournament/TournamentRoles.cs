using System;

namespace HisoutenSupportTools.AddressUpdater.Lib.Model.Tournament
{
    /// <summary>
    /// 大会での役割
    /// </summary>
    [Flags]
    public enum TournamentRoles : int
    {
        /// <summary>None</summary>
        [EnumText("")]
        None = 0,
        /// <summary>ゲスト</summary>
        [EnumText("ｹﾞｽﾄ")]
        Guest = 1,
        /// <summary>観戦</summary>
        [EnumText("観戦")]
        Spectator = 2,
        /// <summary>参加</summary>
        [EnumText("参加")]
        Player = 4,
        /// <summary>運営</summary>
        [EnumText("運営")]
        Manager = 8,
    }
}
