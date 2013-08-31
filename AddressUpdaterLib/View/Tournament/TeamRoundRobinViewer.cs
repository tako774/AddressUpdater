using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using HisoutenSupportTools.AddressUpdater.Lib.AddressService;
using HisoutenSupportTools.AddressUpdater.Lib.Model;

namespace HisoutenSupportTools.AddressUpdater.Lib.View.Tournament
{
    /// <summary>
    /// チーム総当り進行状況表示
    /// </summary>
    public class TeamRoundRobinViewer : TeamMatchResultViewer
    {
        private const string MYSELF_MATCH_TEXT = "＼";

        #region InitializeRootPanel
        /// <summary>
        /// ルートパネル初期化
        /// </summary>
        /// <param name="playerCount"></param>
        protected override void InitializeRootPanel(int playerCount)
        {
            TPanel.RowCount = playerCount + NumberOfPlayersOfTeam + 1; // last +1 dummy row
            TPanel.ColumnCount = playerCount / NumberOfPlayersOfTeam + 2 + 1; // last +1 dummy column
            TPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Outset;
            TPanel.RowStyles.Clear();
            TPanel.ColumnStyles.Clear();
            TPanel.AutoSize = true;
        }
        #endregion

        #region CreateControlInformations
        /// <summary>
        /// コントロール情報生成
        /// </summary>
        /// <param name="players"></param>
        /// <returns></returns>
        protected override Collection<ControlInformation> CreateControlInformations(List<player> players)
        {
            FillMatchResults(players);

            var controlInformations = new Collection<ControlInformation>();

            controlInformations.Add(new ControlInformation(CreateLabel(string.Empty, BorderStyle.FixedSingle), 0, 0, 0, NumberOfPlayersOfTeam));
            controlInformations.Add(new ControlInformation(CreateLabel("結果", BorderStyle.FixedSingle), players.Count / NumberOfPlayersOfTeam + 1, 0, 0, NumberOfPlayersOfTeam));

            var teamWins = new int[players.Count / NumberOfPlayersOfTeam];
            for (var i = 0; i < teamWins.Length; i++)
                teamWins[i] = 0;
            var teamLoses = new int[players.Count / NumberOfPlayersOfTeam];
            for (var i = 0; i < teamLoses.Length; i++)
                teamLoses[i] = 0;

            for (var row = 0; row < players.Count; row++)
            {
                var teamIndex = row / NumberOfPlayersOfTeam;

                // RowHeader
                var rowHeaderLabel = CreateLabel(players[row].entryName);
                if (IsMyself(players[row]))
                    rowHeaderLabel.BorderStyle = BorderStyle.Fixed3D;
                controlInformations.Add(new ControlInformation(rowHeaderLabel, 0, row + NumberOfPlayersOfTeam));

                // ColumnHeader
                controlInformations.Add(new ControlInformation(CreatePlayerLabel(players[row]), teamIndex + 1, row % NumberOfPlayersOfTeam));

                // status
                for (var column = 0; column < players[row].MatchResults.Length; column++)
                {
                    if (teamIndex == column)
                    {
                        if (row % NumberOfPlayersOfTeam == 0)
                            controlInformations.Add(new ControlInformation(CreateLabel(MYSELF_MATCH_TEXT, BorderStyle.FixedSingle), column + 1, row + NumberOfPlayersOfTeam, 0, NumberOfPlayersOfTeam));

                        continue;
                    }


                    var text = NONE_TEXT;
                    if (players[row].retired)
                    {
                        text = LOSE_TEXT;
                        teamLoses[teamIndex]++;
                    }
                    else
                    {
                        switch (players[row].MatchResults[column].Value)
                        {
                            case (int)MatchResults.Lose:
                                text = LOSE_TEXT;
                                teamLoses[teamIndex]++;
                                break;
                            case (int)MatchResults.Win:
                                text = WIN_TEXT;
                                teamWins[teamIndex]++;
                                break;
                            default:
                                break;
                        }
                    }


                    if (IsManager)
                    {
                        if (!players[row].retired)
                            controlInformations.Add(new ControlInformation(CreateResultEditor(players[row], column), column + 1, row + NumberOfPlayersOfTeam));
                        else if (text != NONE_TEXT)
                            controlInformations.Add(new ControlInformation(CreateLabel(text), column + 1, row + NumberOfPlayersOfTeam));
                    }
                    else
                    {
                        if (IsMyself(players[row]) && !players[row].retired)
                            controlInformations.Add(new ControlInformation(CreateResultEditor(players[row], column), column + 1, row + NumberOfPlayersOfTeam));
                        else if (text != NONE_TEXT)
                            controlInformations.Add(new ControlInformation(CreateLabel(text), column + 1, row + NumberOfPlayersOfTeam));
                    }
                }

                // results
                if (row % NumberOfPlayersOfTeam == NumberOfPlayersOfTeam - 1)
                {
                    if (players[row - 1].retired || players[row].retired)
                        controlInformations.Add(new ControlInformation(
                            CreateLabel("リタイア", BorderStyle.FixedSingle),
                            players.Count / NumberOfPlayersOfTeam + 1, row + 1, 0, NumberOfPlayersOfTeam));
                    else
                        controlInformations.Add(new ControlInformation(
                            CreateLabel(string.Format("{0} - {1}", teamWins[teamIndex], teamLoses[teamIndex]), BorderStyle.FixedSingle),
                            players.Count / NumberOfPlayersOfTeam + 1, row + 1, 0, NumberOfPlayersOfTeam));
                }
            }

            return controlInformations;
        }
        #endregion

        #region FillMatchResults
        private void FillMatchResults(List<player> players)
        {
            var matchCount = players.Count / NumberOfPlayersOfTeam;

            foreach (var player in players)
            {
                // fill MatchResults [None]
                if (player.MatchResults == null)
                {
                    player.MatchResults = new int?[matchCount];
                    for (var i = 0; i < matchCount; i++)
                        player.MatchResults[i] = (int)MatchResults.None;
                }
                // resize MatchResults
                else if (player.MatchResults.Length < matchCount)
                {
                    var dummy = player.MatchResults;
                    Array.Resize<int?>(ref dummy, matchCount);
                    player.MatchResults = dummy;

                    // null to [None]
                    for (var i = 0; i < matchCount; i++)
                    {
                        if (!player.MatchResults[i].HasValue)
                            player.MatchResults[i] = (int)MatchResults.None;
                    }
                }

                // fill retired player by [Lose]
                if (player.retired)
                    for (var i = 0; i < matchCount; i++)
                        player.MatchResults[i] = (int)MatchResults.Lose;
            }
        }
        #endregion
    }
}
