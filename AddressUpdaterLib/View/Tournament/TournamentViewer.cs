using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using HisoutenSupportTools.AddressUpdater.Lib.AddressService;
using HisoutenSupportTools.AddressUpdater.Lib.Model;

namespace HisoutenSupportTools.AddressUpdater.Lib.View.Tournament
{
    /// <summary>
    /// トーナメント進行状況表示
    /// </summary>
    public class TournamentViewer : MatchStatusViewer
    {
        /// <summary>┐</summary>
        private static readonly string OVER_LINE = "┐";
        /// <summary>├</summary>
        private static readonly string CROSS_LINE = "├";
        /// <summary>┘</summary>
        private static readonly string UNDER_LINE = "┘";
        /// <summary>│</summary>
        private static readonly string VERTIVAL_LINE = "│";

        #region InitializeRootPanel
        /// <summary>
        /// ルートパネル初期化
        /// </summary>
        /// <param name="playerCount"></param>
        protected override void InitializeRootPanel(int playerCount)
        {
            var hierarchyCount = GetHierarchyCount(playerCount);

            TPanel.RowCount = playerCount + (playerCount - 1) + 1; // last +1 dummy row
            TPanel.ColumnCount = hierarchyCount * 3 + 2; // last +1 dummy column
            TPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;

            TPanel.RowStyles.Clear();
            TPanel.ColumnStyles.Clear();

            for (var column = 0; column < TPanel.ColumnCount; column++)
                TPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            for (int i = 0; i < TPanel.ColumnCount; i++)
            {
                // 結果入力列
                // 1, 4, 7, 10, 13...
                if ((i - 1) % 3 == 0)
                {
                    TPanel.ColumnStyles[i].SizeType = SizeType.Absolute;
                    TPanel.ColumnStyles[i].Width = 65;
                }

                // 罫線列
                // 2, 5, 8, 11...
                else if ((i - 2) % 3 == 0)
                {
                    TPanel.ColumnStyles[i].SizeType = SizeType.Absolute;
                    TPanel.ColumnStyles[i].Width = 30;
                }
            }
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

            // 階層数計算
            var hierarchyCount = GetHierarchyCount(players.Count);

            // 抜けている対戦結果を埋める
            FillMatchResults(players, hierarchyCount);

            // set first players
            for (var i = 0; i < players.Count; i++)
            {
                controlInformations.Add(new ControlInformation(CreatePlayerLabel(players[i]), 0, i * 2));
                //if (IsManager || IsSpectator || (IsPlayer && !IsMyself(players[i]) && !players[i].retired))
                //{
                //    if (players[i].waiting)
                //    {
                //        var playerLinkLabel = CreatePlayerLinkLabel(players[i]);
                //        if (players[i].fighting) { playerLinkLabel.BackColor = Theme.FightingHostBackColor.ToColor(); }
                //        controlInformations.Add(new ControlInformation(playerLinkLabel, 0, i * 2));
                //    }
                //    else
                //    {
                //        var playerLabel = CreateLabel(players[i].entryName);
                //        if (IsMyself(players[i]))
                //            playerLabel.BorderStyle = BorderStyle.Fixed3D;
                //        controlInformations.Add(new ControlInformation(playerLabel, 0, i * 2));
                //    }
                //}
                //else
                //{
                //    var playerLabel = CreateLabel(players[i].entryName);
                //    if (IsMyself(players[i]))
                //        playerLabel.BorderStyle = BorderStyle.Fixed3D;
                //    controlInformations.Add(new ControlInformation(playerLabel, 0, i * 2));
                //}
            }




            for (var hierarchy = 0; hierarchy < hierarchyCount; hierarchy++)
            {
                var rowCount = players.Count / 2;
                for (var i = 0; i < hierarchy; i++)
                    rowCount /= 2;


                // for SetWinners
                var winnerRangeSize = (int)Math.Pow(2, hierarchy + 1);
                var winnerRowStart = (int)Math.Pow(2, hierarchy + 1) - 1;
                var winnerRowSpan = (int)Math.Pow(2, hierarchy + 2);

                // for SetStatusSelecters
                var statusRowSize = 4 * (int)Math.Pow(2, hierarchy);
                var statusRowSpace = (int)Math.Pow(2, hierarchy) - 1;
                var statusRangeSize = (int)Math.Pow(2, hierarchy);


                for (var row = 0; row < rowCount; row++)
                {
                    // SetWinners
                    var winnerStartIndex = row * winnerRangeSize;
                    var winnerEndIndex = winnerStartIndex + (winnerRangeSize - 1);

                    var winnerControl = CreateWinnerControl(players, winnerStartIndex, winnerEndIndex, hierarchy);
                    if (winnerControl != null)
                        controlInformations.Add(new ControlInformation(winnerControl, (hierarchy + 1) * 3, winnerRowStart + winnerRowSpan * row));


                    // SetResultEditors
                    var statusOverStartIndex = row * (statusRangeSize * 2);
                    var statusUnderStartIndex = statusOverStartIndex + statusRangeSize;

                    var statusOverIndex = statusRowSize * row + statusRowSpace;
                    var statusUnderIndex = statusOverIndex + (int)Math.Pow(2, hierarchy + 1);

                    // 上
                    var overStatusEditor = CreateResultEditor(players.GetRange(statusOverStartIndex, statusRangeSize), hierarchy);
                    if (overStatusEditor != null)
                        controlInformations.Add(new ControlInformation(overStatusEditor, hierarchy * 3 + 1, statusOverIndex));

                    // 下
                    var underStatusEditor = CreateResultEditor(players.GetRange(statusUnderStartIndex, statusRangeSize), hierarchy);
                    if (underStatusEditor != null)
                        controlInformations.Add(new ControlInformation(underStatusEditor, hierarchy * 3 + 1, statusUnderIndex));


                    // SetLines
                    var lineMiddleIndex = statusOverIndex + (int)Math.Pow(2, hierarchy + 1) / 2;

                    // ┓
                    controlInformations.Add(new ControlInformation(CreateLabel(OVER_LINE), hierarchy * 3 + 2, statusOverIndex));
                    // ┣
                    controlInformations.Add(new ControlInformation(CreateLabel(CROSS_LINE), hierarchy * 3 + 2, lineMiddleIndex));
                    // ┛
                    controlInformations.Add(new ControlInformation(CreateLabel(UNDER_LINE), hierarchy * 3 + 2, statusUnderIndex));

                    // ┃
                    for (var y = 0; y < statusRowSpace; y++)
                        controlInformations.Add(new ControlInformation(CreateLabel(VERTIVAL_LINE), hierarchy * 3 + 2, statusOverIndex + y + 1));
                    for (var y = 0; y < statusRowSpace; y++)
                        controlInformations.Add(new ControlInformation(CreateLabel(VERTIVAL_LINE), hierarchy * 3 + 2, statusOverIndex + statusRowSpace + 2 + y));
                }
            }

            return controlInformations;
        }
        #endregion

        #region FillMatchResults
        /// <summary>
        /// 抜けてる結果を埋める
        /// </summary>
        /// <param name="players"></param>
        /// <param name="hierarchyCount"></param>
        void FillMatchResults(List<player> players, int hierarchyCount)
        {
            foreach (var player in players)
            {
                // fill MatchResult [None]
                if (player.MatchResults == null)
                {
                    player.MatchResults = new int?[hierarchyCount];
                    for (var i = 0; i < hierarchyCount; i++)
                        player.MatchResults[i] = (int)MatchResults.None;
                }
                // resize MatchResults
                else if (player.MatchResults.Length < hierarchyCount)
                {
                    var dummy = player.MatchResults;
                    Array.Resize<int?>(ref dummy, hierarchyCount);
                    player.MatchResults = dummy;

                    // null to [None]
                    for (var i = 0; i < hierarchyCount; i++)
                    {
                        if (!player.MatchResults[i].HasValue)
                            player.MatchResults[i] = (int)MatchResults.None;
                    }
                }

                // fill retired player by [Lose]
                if (player.retired)
                {
                    for (var i = 0; i < hierarchyCount; i++)
                        player.MatchResults[i] = (int)MatchResults.Lose;
                }
            }
        }
        #endregion

        #region CreateControl
        /// <summary>
        /// 対戦結果編集コントロール生成
        /// </summary>
        /// <param name="players"></param>
        /// <param name="hierarchy"></param>
        /// <returns></returns>
        Control CreateResultEditor(List<player> players, int hierarchy)
        {
            var winner = GetBeforeWinner(players, hierarchy);

            if (winner == null)
                return null;


            if (IsManager && !winner.retired || (IsPlayer && IsMyself(winner) && !winner.retired))
                return CreateResultEditor(winner, hierarchy);
            else
                return CreateLabel(STATUS_TEXTS[winner.MatchResults[hierarchy].Value]);
        }

        /// <summary>
        /// 勝者のコントロールを生成
        /// </summary>
        /// <param name="players"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <param name="hierarchy"></param>
        /// <returns></returns>
        Control CreateWinnerControl(List<player> players, int startIndex, int endIndex, int hierarchy)
        {
            var targetPlayers = players.GetRange(startIndex, endIndex - startIndex + 1);

            player winner = null;
            for (var i = 0; i < targetPlayers.Count; i++)
            {
                try
                {
                    if (targetPlayers[i].MatchResults == null)
                        continue;

                    if (targetPlayers[i].MatchResults[hierarchy] == (int)MatchResults.Win)
                    {
                        winner = targetPlayers[i];
                        break;
                    }
                }
                catch (IndexOutOfRangeException) { }
            }

            if (winner == null)
                return null;

            return CreatePlayerLabel(winner);
            //if (IsManager || IsSpectator || (IsPlayer && !IsMyself(winner) && !winner.retired))
            //{
            //    if (winner.waiting)
            //    {
            //        var playerLinkLabel = CreatePlayerLinkLabel(winner);
            //        if (winner.fighting) { playerLinkLabel.BackColor = Theme.FightingHostBackColor.ToColor(); }
            //        return playerLinkLabel;
            //    }
            //    else
            //    {
            //        var winnerLabel = CreateLabel(winner.entryName);
            //        if (IsMyself(winner)) { winnerLabel.BorderStyle = BorderStyle.Fixed3D; }
            //        return winnerLabel;
            //    }
            //}
            //else
            //{
            //    var winnerLabel = CreateLabel(winner.entryName);
            //    if (IsMyself(winner)) { winnerLabel.BorderStyle = BorderStyle.Fixed3D; }
            //    return winnerLabel;
            //}
        }
        #endregion

        /// <summary>
        /// 前試合の勝者を取得
        /// </summary>
        /// <param name="players"></param>
        /// <param name="hierarchy"></param>
        /// <returns></returns>
        private player GetBeforeWinner(List<player> players, int hierarchy)
        {
            if (hierarchy == 0)
                return players[0];

            foreach (var player in players)
            {
                try
                {
                    if (player.MatchResults[hierarchy - 1] == (int)MatchResults.Win)
                        return player;
                }
                catch (Exception) { }
            }

            return null;
        }

        /// <summary>
        /// 階層数計算
        /// </summary>
        /// <param name="playerCount">参加人数</param>
        /// <returns>階層数</returns>
        private int GetHierarchyCount(int playerCount)
        {
            var hierarchyCount = 1;
            var tmp = playerCount;
            while (1 != tmp / 2)
            {
                hierarchyCount += 1;
                tmp /= 2;
            }

            return hierarchyCount;
        }
    }
}
