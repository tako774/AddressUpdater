using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using HisoutenSupportTools.AddressUpdater.Lib.Model;
using HisoutenSupportTools.AddressUpdater.Lib.AddressService;

namespace HisoutenSupportTools.AddressUpdater.Lib.View.Tournament
{
    /// <summary>
    /// チームトーナメント進行状況表示
    /// </summary>
    public class TeamTournamentViewer : TeamMatchResultViewer
    {
        #region InitializeRootPanel
        /// <summary>
        /// ルートパネル初期化
        /// </summary>
        /// <param name="playerCount"></param>
        protected override void InitializeRootPanel(int playerCount)
        {
            var hierarchyCount = GetHierarchyCount(playerCount);

            TPanel.RowCount = playerCount * 2 - 1 + 1; // last +1 dummy row
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
            FillMatchResults(players);

            var controlInformations = new Collection<ControlInformation>();

            return controlInformations;
        }

        private void FillMatchResults(List<player> players)
        {
            throw new NotImplementedException();
        }
        #endregion

        /// <summary>
        /// 階層数計算
        /// </summary>
        /// <param name="playerCount">参加人数</param>
        /// <returns>階層数</returns>
        private int GetHierarchyCount(int playerCount)
        {
            var hierarchyCount = 1;
            var tmp = playerCount / NumberOfPlayersOfTeam;
            while (1 != tmp / 2)
            {
                hierarchyCount += 1;
                tmp /= 2;
            }

            return hierarchyCount;
        }
    }
}
