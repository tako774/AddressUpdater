using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using HisoutenSupportTools.AddressUpdater.Lib.Api;
using HisoutenSupportTools.AddressUpdater.Lib.Network;
using HisoutenSupportTools.AddressUpdater.Lib.ViewModel;
using HisoutenSupportTools.AddressUpdater.Lib.Model.Config;
using System.Collections.ObjectModel;

namespace HisoutenSupportTools.AddressUpdater.Lib.View
{
    /// <summary>
    /// バージョン情報タブ
    /// </summary>
    public partial class VersionTab : TabBase
    {
        #region property
        /// <summary>VersionViewModelの取得・設定</summary>
        public VersionViewModel ViewModel { get; set; }

        /// <summary>おまけ機能ウィンドウ情報の取得</summary>
        public Collection<ExtraWindowInformation> ExtraWindowInformations
        {
            get
            {
                var informations = new Collection<ExtraWindowInformation>();
                foreach (TabPage page in tabControl1.TabPages)
                {
                    if (page.Controls[0] is ExtraView)
                    {
                        var extraViewModel = ((ExtraView)page.Controls[0]).ViewModel;
                        if (!string.IsNullOrEmpty(extraViewModel.Caption))
                            informations.Add(extraViewModel.WindowInformation);
                    }
                }

                return new Collection<ExtraWindowInformation>(informations);
            }
        }
        #endregion


        /// <summary>
        /// インスタンスの生成
        /// </summary>
        public VersionTab()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VersionTab_Load(object sender, EventArgs e)
        {
            if (Language != null)
                ReflectLanguage();

            try
            {
                var assemblyName = Assembly.GetEntryAssembly().GetName();
                versionLabel.Text = string.Format(
                    "{0} {1}", assemblyName.Name, assemblyName.Version.ToString(2));
                versionToolTip.SetToolTip(
                    versionLabel,
                    string.Format("build:{0} revision:{1}", assemblyName.Version.Build, assemblyName.Version.Revision));

                SetExtraTabPages();
            }
            catch (NullReferenceException) { }
        }

        void SetExtraTabPages()
        {
            tabControl1.TabPages.Clear();
            for (var i = 0; i < UserConfig.ExtraWindowInformations.Count; i++)
            {
                var viewModel = new ExtraViewModel();
                viewModel.WindowInformations = ViewModel.WindowInformations;
                viewModel.WindowInformation = UserConfig.ExtraWindowInformations[i];

                var view = new ExtraView();
                view.Dock = DockStyle.Fill;
                view.ReflectTheme(Theme);
                view.ReflectLanguage(Language);
                view.ViewModel = viewModel;

                tabControl1.TabPages.Add((i + 1).ToString());
                tabControl1.TabPages[i].Controls.Add(view);
            }

            var addButton = new Button() { Text = "追加" };
            addButton.Dock = DockStyle.Fill;
            addButton.Click += new EventHandler(addButton_Click);
            tabControl1.TabPages.Add("+");
            tabControl1.TabPages[tabControl1.TabCount - 1].Controls.Add(addButton);
        }

        void addButton_Click(object sender, EventArgs e)
        {
            UserConfig.ExtraWindowInformations.Add(new ExtraWindowInformation()
            {
                Caption = "東方心綺楼 Ver.1.21"
            });
            SetExtraTabPages();
            tabControl1.SelectedIndex = tabControl1.TabCount - 2;
        }

        #region ReflectLanguage
        /// <summary>
        /// 
        /// </summary>
        private void ReflectLanguage()
        {
            try { versionCheckLinkLabel.Text = Language["VersionTab_CheckVersion"]; }
            catch (KeyNotFoundException) { }
            try { adminSiteLinkLabel1.Text = Language["VersionTab_site1"]; }
            catch (KeyNotFoundException) { }
            try { adminSiteLinkLabel2.Text = Language["VersionTab_site2"]; }
            catch (KeyNotFoundException) { }

            try { omakeGroupBox.Text = Language["VersionTab_Extra"]; }
            catch (KeyNotFoundException) { }
        }
        #endregion

        #region ReflectTheme
        /// <summary>
        /// テーマの反映
        /// </summary>
        public override void ReflectTheme()
        {
            BackColor = Theme.ToolBackColor.ToColor();

            omakeGroupBox.ForeColor = Theme.GeneralTextColor.ToColor();
            omakeGroupBox.BackColor = Theme.ToolBackColor.ToColor();

            foreach (TabPage page in tabControl1.TabPages)
            {
                page.BackColor = Theme.ToolBackColor.ToColor();
                if (page.Controls[0] is ExtraView)
                    ((ExtraView)page.Controls[0]).ReflectTheme(Theme);
            }
        }
        #endregion

        #region バージョン確認
        /// <summary>
        /// バージョン確認
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void versionCheckLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                switch (ViewModel.CheckVersion())
                {
                    case CheckVersionResults.Latest:
                        var message = "最新のバージョンです。";
                        try { message = Language["VersionTab_LatestVersion"]; }
                        catch (KeyNotFoundException) { }
                        MessageBox.Show(
                            ParentForm,
                            Application.ProductVersion + Environment.NewLine + message,
                            Application.ProductName,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        break;

                    case CheckVersionResults.ExistsNew:
                        var message1 = "新しいバージョンが公開されています。";
                        var message2 = "ダウンロードページを開きますか?";
                        try { message1 = Language["VersionTab_FoundNewVersion_1"]; }
                        catch (KeyNotFoundException) { }
                        try { message2 = Language["VersionTab_FoundNewVersion_2"]; }
                        catch (KeyNotFoundException) { }
                        if (MessageBox.Show(
                            ParentForm,
                            message1 + Environment.NewLine + message2,
                            Application.ProductName,
                            MessageBoxButtons.YesNoCancel,
                            MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            try { ViewModel.OpenBrowser(AddressUpdaterUri.ADMIN_SITE + "download.html"); }
                            catch (InvalidOperationException ex)
                            {
                                var message_ = "ブラウザの起動に失敗しました。";
                                try { message_ = Language["VersionTab_OpenBrowserFailed"]; }
                                catch (KeyNotFoundException) { }
                                MessageBox.Show(
                                    ParentForm,
                                    message_ + Environment.NewLine + Environment.NewLine + ex.Message,
                                    Application.ProductName,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                            }
                            catch (Win32Exception ex)
                            {
                                var message_ = "ブラウザの起動に失敗しました。";
                                try { message_ = Language["VersionTab_OpenBrowserFailed"]; }
                                catch (KeyNotFoundException) { }
                                MessageBox.Show(
                                    ParentForm,
                                    message_ + Environment.NewLine + Environment.NewLine + ex.Message,
                                    Application.ProductName,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                            }
                        }
                        break;

                    default:
                        break;
                }
            }
            catch (CommunicationFailedException ex)
            {
                var message = "最新バージョン情報の取得に失敗しました。";
                try { message = Language["VersionTab_CheckVersionFailed"]; }
                catch (KeyNotFoundException) { }
                MessageBox.Show(
                    ParentForm,
                    message + Environment.NewLine + Environment.NewLine + ex.InnerException.Message,
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        #endregion

        /// <summary>
        /// 作者のサイト1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void adminSiteLinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try { ViewModel.OpenBrowser(AddressUpdaterUri.ADMIN_SITE); }
            catch (InvalidOperationException) { }
            catch (Win32Exception) { }
        }

        /// <summary>
        /// 作者のサイト2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void adminSiteLinkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try { ViewModel.OpenBrowser(AddressUpdaterUri.ADMIN_SITE_SUB); }
            catch (InvalidOperationException) { }
            catch (Win32Exception) { }
        }
    }
}
