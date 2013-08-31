using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace HisoutenSupportTools.AddressUpdater.Lib.View
{
    /// <summary>
    /// 色の変わるトグルボタン
    /// </summary>
    public partial class ColorChangeToggle : Component
    {
        #region フィールド
        /// <summary>機能を追加するチェックボックス</summary>
        private CheckBox _target;
        /// <summary>通常時背景色</summary>
        private Color _normalBackColor = Form.DefaultBackColor;
        /// <summary>チェック状態の背景色</summary>
        private Color _checkedBackColor = Color.Pink;
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
                {
                    _normalBackColor = _target.BackColor;
                    _target.CheckedChanged += new System.EventHandler(_target_CheckedChanged);
                }
            }
        }

        /// <summary>
        /// チェック状態の背景色
        /// </summary>
        [Description("チェック状態の背景色")]
        [DefaultValue(typeof(Color), "Pink")]
        public Color CheckedBackColor
        {
            get { return _checkedBackColor; }
            set { _checkedBackColor = value; }
        }

        /// <summary>
        /// インスタンスの生成
        /// </summary>
        public ColorChangeToggle()
        {
            InitializeComponent();
        }

        /// <summary>
        /// インスタンスの生成
        /// </summary>
        /// <param name="container"></param>
        public ColorChangeToggle(IContainer container)
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
            if (_target.Checked)
                _target.BackColor = _checkedBackColor;
            else
            {
                _target.BackColor = _normalBackColor;
                _target.UseVisualStyleBackColor = true;
            }
        }
    }
}
