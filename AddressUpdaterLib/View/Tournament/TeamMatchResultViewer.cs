using System;
using System.Collections.Generic;
using System.Text;

namespace HisoutenSupportTools.AddressUpdater.Lib.View.Tournament
{
    /// <summary>
    /// チーム大会試合状況表示クラス
    /// </summary>
    public abstract class TeamMatchResultViewer : MatchStatusViewer
    {
        /// <summary>チーム人数</summary>
        public int NumberOfPlayersOfTeam { get; set; }
    }
}
