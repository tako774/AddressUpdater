using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using HisoutenSupportTools.AddressUpdater.Lib.View;

namespace HisoutenSupportTools.AddressUpdater.View
{
    public partial class UserConfigTab : TabBase
    {
        /// <summary>
        /// インスタンスの生成
        /// </summary>
        public UserConfigTab()
        {
            InitializeComponent();


            generalTab.UserConfig = Program.USER_CONFIG;
            generalTab.Theme = Program.THEME;

            rankTab.UserConfig = Program.USER_CONFIG;
            rankTab.Theme = Program.THEME;

            highlightKeywordsTab.UserConfig = Program.USER_CONFIG;
            highlightKeywordsTab.Theme = Program.THEME;

            chatFilterTab.UserConfig = Program.USER_CONFIG;
            chatFilterTab.Theme = Program.THEME;

            sameTimeBootTab.UserConfig = Program.USER_CONFIG;
            sameTimeBootTab.Theme = Program.THEME;

            serverTab.UserConfig = Program.USER_CONFIG;
            serverTab.Theme = Program.THEME;
        }

        /// <summary>
        /// 同時起動ソフトの起動
        /// </summary>
        public void Boot()
        {
            sameTimeBootTab.Boot();
        }

        /// <summary>
        /// Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserConfigTab_Load(object sender, EventArgs e)
        {
            ReflectLanguage();

            for (var i = 0; i < userConfigTabControl.TabCount; i++)
                userConfigTabControl.SelectedIndex = i;

            userConfigTabControl.SelectedIndex = 0;
        }

        /// <summary>
        /// 表示言語の反映
        /// </summary>
        private void ReflectLanguage()
        {
            if (Language == null)
                return;

            try { userConfigTabControl.TabPages["generalTabPage"].Text = Language["UserConfigTab_GeneralTab"]; }
            catch (KeyNotFoundException) { }
            try { userConfigTabControl.TabPages["ranksTabPage"].Text = Language["UserConfigTab_RanksTab"]; }
            catch (KeyNotFoundException) { }
            try { userConfigTabControl.TabPages["chatFilterTabPage"].Text = Language["UserConfigTab_ChatFilterTab"]; }
            catch (KeyNotFoundException) { }
            try { userConfigTabControl.TabPages["bootSameTimeTabPage"].Text = Language["UserConfigTab_BootSameTimeTab"]; }
            catch (KeyNotFoundException) { }
            try { userConfigTabControl.TabPages["serverInformationsTabPage"].Text = Language["UserConfigTab_ServerInformationTab"]; }
            catch (KeyNotFoundException) { }
            try { saveUserConfigButton.Text = Language["UserConfigTab_Save"]; }
            catch (KeyNotFoundException) { }

            generalTab.Language = Language;
            rankTab.Language = Language;
            highlightKeywordsTab.Language = Language;
            chatFilterTab.Language = Language;
            sameTimeBootTab.Language = Language;
            serverTab.Language = Language;

            generalTab.ReflectLanguage(Language);
            sameTimeBootTab.ReflectLanguage(Language);
            serverTab.ReflectLanguage(Language);
        }

        /// <summary>
        /// テーマの反映
        /// </summary>
        public override void ReflectTheme()
        {
            BackColor = Program.THEME.ToolBackColor.ToColor();
            saveUserConfigButton.BackColor = SystemColors.Control;
            saveUserConfigButton.UseVisualStyleBackColor = true;

            splitContainer1.BackColor = Program.THEME.ToolBackColor.ToColor();
            splitContainer1.Panel1.BackColor = Program.THEME.ToolBackColor.ToColor();
            splitContainer1.Panel2.BackColor = Program.THEME.ToolBackColor.ToColor();

            generalTab.ReflectTheme();
            rankTab.ReflectTheme();
            highlightKeywordsTab.ReflectTheme();
            chatFilterTab.ReflectTheme();
            sameTimeBootTab.ReflectTheme();
            serverTab.ReflectTheme();
        }

        /// <summary>
        /// 保存ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveUserConfigButton_Click(object sender, EventArgs e)
        {
            try { Program.USER_CONFIG.Load(); }
            catch (FileNotFoundException) { }

            // 一般
            Program.USER_CONFIG.ServerIp = generalTab.Ip;
            Program.USER_CONFIG.ServerPort = generalTab.Port;
            Program.USER_CONFIG.UpdateSpan = generalTab.UpdateSpan;
            Program.USER_CONFIG.ShowTournaments = generalTab.ShowTournaments;
            Program.USER_CONFIG.DisableMultiBoot = generalTab.DisableMultiBoot;
            Program.USER_CONFIG.ChatPrefix = generalTab.ChatPrefix;
            Program.USER_CONFIG.ClientDivisionOrientation = generalTab.ClientDivisionOrientation;
            Program.USER_CONFIG.ToolBackColor = Program.THEME.ToolBackColor;
            Program.USER_CONFIG.GeneralTextColor = Program.THEME.GeneralTextColor;
            Program.USER_CONFIG.WaitingHostBackColor = Program.THEME.WaitingHostBackColor;
            Program.USER_CONFIG.FightingHostBackColor = Program.THEME.FightingHostBackColor;
            Program.USER_CONFIG.ChatForeColor = Program.THEME.ChatForeColor;
            Program.USER_CONFIG.ChatBackColor = Program.THEME.ChatBackColor;
            Program.USER_CONFIG.HostFont = Program.THEME.HostFont;
            Program.USER_CONFIG.ChatFont = Program.THEME.ChatFont;

            // ランク
            Program.USER_CONFIG.Ranks = rankTab.GetRanks();

            // ハイライト
            Program.USER_CONFIG.HighlightKeywords = highlightKeywordsTab.GetKeywords();

            // チャットフィルター
            Program.USER_CONFIG.FilterKeywords = chatFilterTab.GetKeywords();

            // 同時起動
            Program.USER_CONFIG.BootSameTimeSofts = sameTimeBootTab.GetSoftwareInformations();

            // サーバー情報
            Program.USER_CONFIG.ServerInformations = serverTab.GetServerInformations();

            try
            {
                Program.USER_CONFIG.Save();

                // 即反映させる
                ((MainForm)ParentForm).ReflectSavedConfig();
            }
            catch (UnauthorizedAccessException ex) { ShowSaveFailedMessage(ex); }
            catch (InvalidOperationException ex) { ShowSaveFailedMessage(ex); }
            catch (IOException ex) { ShowSaveFailedMessage(ex); }
        }

        /// <summary>
        /// 保存失敗メッセージ表示
        /// </summary>
        /// <param name="ex">例外</param>
        private void ShowSaveFailedMessage(Exception ex)
        {
            var message = "設定の保存に失敗しました。";
            try { message = Language["UserConfigTab_SaveConfigFailed"]; }
            catch (KeyNotFoundException) { }

            MessageBox.Show(
                ParentForm,
                message + Environment.NewLine + Environment.NewLine + ex.Message,
                Application.ProductName,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        private void generalTab_ReflectThemeRequested(object sender, EventArgs e)
        {
            ((MainForm)ParentForm).ReflectTheme();
        }
    }
}
