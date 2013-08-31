using System;
using System.Windows.Forms;
using HisoutenSupportTools.AddressUpdater.Lib.Model.Config;

namespace HisoutenSupportTools.AddressUpdater.Lib.View
{
    /// <summary>
    /// タブ
    /// </summary>
    public partial class TabBase : UserControl
    {
        /// <summary>
        /// ユーザー設定の取得・設定
        /// </summary>
        public UserConfig UserConfig { get; set; }

        /// <summary>
        ///表示言語設定の取得・設定
        /// </summary>
        public Language Language { get; set; }

        /// <summary>
        /// テーマの取得・設定
        /// </summary>
        public Theme Theme { get; set; }

        /// <summary>
        /// タブテキストの取得・設定
        /// </summary>
        protected string TabText
        {
            get { return ((TabPage)Parent).Text; }
            set { ((TabPage)Parent).Text = value; }
        }


        /// <summary>
        /// インスタンスの生成
        /// </summary>
        public TabBase()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabBase_Load(object sender, System.EventArgs e)
        {
            try
            {
                ReflectTheme();
            }
            catch (Exception ex) { System.Diagnostics.Debug.WriteLine(ex.Message); }
        }

        /// <summary>
        /// テーマの反映
        /// </summary>
        public virtual void ReflectTheme()
        {
            throw new NotImplementedException();
        }
    }
}
