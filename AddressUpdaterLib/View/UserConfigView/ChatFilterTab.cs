using System;
using System.Collections.ObjectModel;

namespace HisoutenSupportTools.AddressUpdater.Lib.View.UserConfigView
{
    /// <summary>
    /// チャットフィルタータブ
    /// </summary>
    public partial class ChatFilterTab : TabBase
    {
        /// <summary>
        /// キーワードの一覧取得
        /// </summary>
        /// <returns>キーワード一覧</returns>
        public Collection<string> GetKeywords()
        {
            string[] lines = keywordInput.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            Collection<string> keywords = new Collection<string>();
            foreach (string keyword in lines)
            {
                keywords.Add(keyword);
            }
            return keywords;
        }


        /// <summary>
        /// インスタンスの生成
        /// </summary>
        public ChatFilterTab()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChatFilterTab_Load(object sender, EventArgs e)
        {
            if (UserConfig == null)
                return;

            keywordInput.Clear();
            foreach (var keyword in UserConfig.FilterKeywords)
            {
                keywordInput.AppendText(keyword);
                keywordInput.AppendText(Environment.NewLine);
            }
        }

        /// <summary>
        /// 色設定の反映
        /// </summary>
        public override void ReflectTheme()
        {
            BackColor = Theme.ToolBackColor.ToColor();

            keywordInput.ForeColor = Theme.ChatForeColor.ToColor();
            keywordInput.BackColor = Theme.ChatBackColor.ToColor();
        }
    }
}
