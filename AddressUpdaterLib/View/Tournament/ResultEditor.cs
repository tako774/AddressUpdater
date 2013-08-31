using System;
using System.Windows.Forms;
using HisoutenSupportTools.AddressUpdater.Lib.AddressService;
using HisoutenSupportTools.AddressUpdater.Lib.Model;
using HisoutenSupportTools.AddressUpdater.Lib.Model.Config;
using HisoutenSupportTools.AddressUpdater.Lib.Model.Tournament;

namespace HisoutenSupportTools.AddressUpdater.Lib.View.Tournament
{
    /// <summary>
    /// 状況編集コントロール
    /// </summary>
    public partial class ResultEditor : UserControl, IEquatable<ResultEditor>
    {
        private static readonly string[] RESULT_TEXTS = new string[]
        {
            EnumTextAttribute.GetText(MatchResults.None),
            EnumTextAttribute.GetText(MatchResults.Lose),
            EnumTextAttribute.GetText(MatchResults.Win),
        };

        /// <summary>対戦結果編集時に発生します。</summary>
        public event EventHandler<EventArgs<player>> ResultEdited;

        /// <summary>
        /// 大会種別
        /// </summary>
        public TournamentTypes TournamentType { get; set; }

        /// <summary>
        /// テーマの取得・設定
        /// </summary>
        public Theme Theme { get; set; }

        /// <summary>
        /// 対戦結果の取得・設定
        /// </summary>
        public MatchResults Result
        {
            get { return (MatchResults)resultSelector.SelectedIndex; }
            set
            {
                if (resultSelector.SelectedIndex == (int)value)
                    return;
                resultSelector.SelectedIndex = (int)value;
                resultTextLabel.Text = EnumTextAttribute.GetText(value);
            }
        }

        /// <summary>
        /// プレイヤー
        /// </summary>
        public player Player { get; set; }

        /// <summary>
        /// 試合index
        /// </summary>
        public int MatchIndex { get; set; }

        /// <summary>
        /// インスタンスの生成
        /// </summary>
        public ResultEditor()
        {
            InitializeComponent();

            resultSelector.Items.Clear();
            resultSelector.Items.AddRange(RESULT_TEXTS);
        }

        private void ResultEditor_Load(object sender, EventArgs e)
        {
            ReflectTheme();
        }

        /// <summary>
        /// テーマの反映
        /// </summary>
        public void ReflectTheme()
        {
            if (Theme == null)
                return;

            resultTextLabel.ForeColor = Theme.GeneralTextColor.ToColor();

            resultSelector.ForeColor = Theme.ChatForeColor.ToColor();
            resultSelector.BackColor = Theme.ChatBackColor.ToColor();
        }

        private void resultTextLabel_Click(object sender, EventArgs e)
        {
            resultSelector.Visible = true;
            resultSelector.Enabled = true;
            resultTextLabel.Visible = false;
            Application.DoEvents();
            resultSelector.DroppedDown = true;
        }

        private void resultSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Player.MatchResults[MatchIndex] != resultSelector.SelectedIndex)
            {
                resultTextLabel.Text = RESULT_TEXTS[resultSelector.SelectedIndex];
                Player.MatchResults[MatchIndex] = resultSelector.SelectedIndex;
                if (ResultEdited != null)
                    ResultEdited(this, new EventArgs<player>(Player));
            }
        }

        private void resultSelector_DropDownClosed(object sender, EventArgs e)
        {
            resultTextLabel.Visible = true;
            resultSelector.Visible = false;
            resultSelector.Enabled = false;
        }

        #region IEquatable<ResultEditor> メンバ

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(ResultEditor other)
        {
            if (other == null) return false;
            if (object.ReferenceEquals(this, other)) return true;

            return
                TournamentType == other.TournamentType &&
                MatchIndex == other.MatchIndex &&
                Result == other.Result &&
                Player.MatchResults[MatchIndex] == other.Player.MatchResults[MatchIndex];
        }

        #endregion
    }
}
