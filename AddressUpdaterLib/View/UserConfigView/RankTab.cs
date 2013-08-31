using System;
using System.Collections.ObjectModel;

namespace HisoutenSupportTools.AddressUpdater.Lib.View.UserConfigView
{
    /// <summary>
    /// ランクタブ
    /// </summary>
    public partial class RankTab : TabBase
    {
        /// <summary>
        /// ランクの一覧取得
        /// </summary>
        /// <returns>ランク一覧</returns>
        public Collection<string> GetRanks()
        {
            string[] lines = ranksInput.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            Collection<string> ranks = new Collection<string>();
            foreach (string rank in lines)
            {
                ranks.Add(rank);
            }
            return ranks;
        }


        /// <summary>
        /// インスタンスの生成
        /// </summary>
        public RankTab()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RankTab_Load(object sender, EventArgs e)
        {
            if (UserConfig == null)
                return;

            ranksInput.Clear();
            foreach (var rank in UserConfig.Ranks)
            {
                ranksInput.AppendText(rank);
                ranksInput.AppendText(Environment.NewLine);
            }
        }

        /// <summary>
        /// 色設定の反映
        /// </summary>
        public override void ReflectTheme()
        {
            BackColor = Theme.ToolBackColor.ToColor();

            ranksInput.ForeColor = Theme.ChatForeColor.ToColor();
            ranksInput.BackColor = Theme.ChatBackColor.ToColor();
        }
    }
}
