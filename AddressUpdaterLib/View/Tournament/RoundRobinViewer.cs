using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using HisoutenSupportTools.AddressUpdater.Lib.AddressService;
using HisoutenSupportTools.AddressUpdater.Lib.Model;

namespace HisoutenSupportTools.AddressUpdater.Lib.View.Tournament
{
    /// <summary>
    /// 総当り進行状況表示
    /// </summary>
    public class RoundRobinViewer : MatchStatusViewer
    {
        private const string MYSELF_MATCH_TEXT = "＼";

        #region InitializeRootPanel
        /// <summary>
        /// ルートパネル初期化
        /// </summary>
        /// <param name="playerCount"></param>
        protected override void InitializeRootPanel(int playerCount)
        {
            TPanel.RowCount = playerCount + 1 + 1; // last +1 dummy row
            TPanel.ColumnCount = playerCount + 2 + 1; // last +1 dummy column
            TPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;
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
            var controlInformations = new Collection<ControlInformation>();

            controlInformations.Add(new ControlInformation(CreateLabel("結果"), players.Count + 1, 0));

            for (var row = 0; row < players.Count; row++)
            {
                FillMatchResults(players, row);

                // RowHeader
                var rowHeaderLabel = CreateLabel(players[row].entryName);
                if (IsMyself(players[row]))
                    rowHeaderLabel.BorderStyle = BorderStyle.Fixed3D;
                controlInformations.Add(new ControlInformation(rowHeaderLabel, 0, row + 1));

                // ColumnHeader
                controlInformations.Add(new ControlInformation(CreatePlayerLabel(players[row]), row + 1, 0));

                // status
                var winCount = 0;
                var loseCount = 0;
                for (var column = 0; column < players[row].MatchResults.Length; column++)
                {
                    if (row == column)
                    {
                        controlInformations.Add(new ControlInformation(CreateLabel(MYSELF_MATCH_TEXT), column + 1, row + 1));
                        continue;
                    }


                    var text = NONE_TEXT;
                    if (players[row].retired)
                    {
                        text = LOSE_TEXT;
                        loseCount++;
                    }
                    else
                    {
                        switch (players[row].MatchResults[column].Value)
                        {
                            case (int)MatchResults.Lose:
                                text = LOSE_TEXT;
                                loseCount++;
                                break;
                            case (int)MatchResults.Win:
                                text = WIN_TEXT;
                                winCount++;
                                break;
                            default:
                                break;
                        }
                    }


                    if (IsManager)
                    {
                        if (!players[row].retired)
                            controlInformations.Add(new ControlInformation(CreateResultEditor(players[row], column), column + 1, row + 1));
                        else if (text != NONE_TEXT)
                            controlInformations.Add(new ControlInformation(CreateLabel(text), column + 1, row + 1));
                    }
                    else
                    {
                        if (IsMyself(players[row]) && !players[row].retired)
                            controlInformations.Add(new ControlInformation(CreateResultEditor(players[row], column), column + 1, row + 1));
                        else if (text != NONE_TEXT)
                            controlInformations.Add(new ControlInformation(CreateLabel(text), column + 1, row + 1));
                    }
                }

                // results
                if (players[row].retired)
                    controlInformations.Add(new ControlInformation(
                        CreateLabel("リタイア"),
                        players.Count + 1, row + 1));
                else
                    controlInformations.Add(new ControlInformation(
                        CreateLabel(string.Format("{0}勝{1}敗", winCount, loseCount)),
                        players.Count + 1, row + 1));
            }

            return controlInformations;
        }
        #endregion

        #region FillMatchResults
        /// <summary>
        /// 抜けてる結果を埋める
        /// </summary>
        /// <param name="players"></param>
        /// <param name="i"></param>
        private void FillMatchResults(List<player> players, int i)
        {
            // fill MatchResults [None]
            if (players[i].MatchResults == null)
            {
                players[i].MatchResults = new int?[players.Count];
                for (var j = 0; j < players.Count; j++)
                    players[i].MatchResults[j] = (int)MatchResults.None;
            }
            // resize MatchResults
            else if (players[i].MatchResults.Length < players.Count)
            {
                // resize
                var dummy = players[i].MatchResults;
                Array.Resize<int?>(ref dummy, players.Count);
                players[i].MatchResults = dummy;

                // null to [None]
                for (var j = 0; j < players.Count; j++)
                {
                    if (!players[i].MatchResults[j].HasValue)
                        players[i].MatchResults[j] = (int)MatchResults.None;
                }
            }

            // fill retired player by [Lose]
            if (players[i].retired)
            {
                for (var j = 0; j < players.Count; j++)
                {
                    if (i == j)
                        players[i].MatchResults[j] = (int)MatchResults.None;
                    else
                        players[i].MatchResults[j] = (int)MatchResults.Lose;
                }
            }
        }
        #endregion
    }
}
