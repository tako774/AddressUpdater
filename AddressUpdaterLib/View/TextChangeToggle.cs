using System.ComponentModel;
using System.Windows.Forms;

namespace HisoutenSupportTools.AddressUpdater.Lib.View
{
    public partial class TextChangeToggle : Component
    {
        #region フィールド
        /// <summary>機能を追加するチェックボックス</summary>
        private CheckBox _target;
        /// <summary>通常時テキスト</summary>
        private string _normalText = "";
        /// <summary>チェック状態のテキスト</summary>
        private string _checkedText = "";
        #endregion

        /// <summary>
        /// 機能を追加するチェックボックス
        /// </summary>
        [Description("機能を追加するチェックボックス")]
        public CheckBox Target
        {
            get { return _target; }
            set
            {
                _target = value;
                if (_target != null)
                    _target.CheckedChanged += new System.EventHandler(_target_CheckedChanged);
            }
        }

        /// <summary>
        /// 通常時のテキスト
        /// </summary>
        [Description("チェック状態のテキスト")]
        [DefaultValue("")]
        public string NormalText
        {
            get { return _normalText; }
            set { _normalText = value; }
        }

        /// <summary>
        /// チェック状態のテキスト
        /// </summary>
        [Description("チェック状態のテキスト")]
        [DefaultValue("")]
        public string CheckedText
        {
            get { return _checkedText; }
            set { _checkedText = value; }
        }

        /// <summary>
        /// インスタンスの生成
        /// </summary>
        public TextChangeToggle()
        {
            InitializeComponent();
        }

        /// <summary>
        /// インスタンスの生成
        /// </summary>
        /// <param name="container"></param>
        public TextChangeToggle(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        /// <summary>
        /// チェック状態変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _target_CheckedChanged(object sender, System.EventArgs e)
        {
            _target.Text = _target.Checked ? _checkedText : _normalText;
        }
    }
}
