using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security;
using System.Threading;
using System.Windows.Forms;
using HisoutenSupportTools.AddressUpdater.Lib;
using HisoutenSupportTools.AddressUpdater.Lib.Model.Config;

namespace HisoutenSupportTools.AddressUpdater
{
    static class Program
    {
        /// <summary>ユーザー設定</summary>
        public static readonly UserConfig USER_CONFIG = new UserConfig();
        /// <summary>表示言語</summary>
        public static readonly Language LANGUAGE = new Language();
        /// <summary>現在のテーマ</summary>
        public static readonly Theme THEME = new Theme();


        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            // 言語ファイル読み込み
            LoadLanguageFile();

            // ユーザー設定読み込み
            LoadUserConfig();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (USER_CONFIG.DisableMultiBoot)
            {
                using (Mutex mutex = new Mutex(true, Application.ProductName))
                {
                    if (mutex.WaitOne(0, false))
                    {
                        try { Application.Run(new MainForm()); }
                        finally { mutex.ReleaseMutex(); }
                    }
                }
            }
            else
            {
                Application.Run(new MainForm());
            }
        }

        /// <summary>
        /// ユーザー設定読み込み
        /// </summary>
        private static void LoadUserConfig()
        {
            try { USER_CONFIG.Load(); }
            catch (FileNotFoundException) { }
            catch (SecurityException ex) { ShowLoadConfigFailedMessage(ex); }
            catch (UnauthorizedAccessException ex) { ShowLoadConfigFailedMessage(ex); }
            catch (InvalidOperationException ex) { ShowLoadConfigFailedMessage(ex); }
            finally
            {
                THEME.ToolBackColor = USER_CONFIG.ToolBackColor;
                THEME.GeneralTextColor = USER_CONFIG.GeneralTextColor;
                THEME.WaitingHostBackColor = USER_CONFIG.WaitingHostBackColor;
                THEME.FightingHostBackColor = USER_CONFIG.FightingHostBackColor;
                THEME.ChatForeColor = USER_CONFIG.ChatForeColor;
                THEME.ChatBackColor = USER_CONFIG.ChatBackColor;
                THEME.HostFont = USER_CONFIG.HostFont;
                THEME.ChatFont = USER_CONFIG.ChatFont;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private static void LoadLanguageFile()
        {
            try { LANGUAGE.Load(); }
            catch (Exception ex) { Debug.WriteLine(ex); }
        }

        /// <summary>
        /// ユーザー設定読み込み失敗メッセージ表示
        /// </summary>
        /// <param name="ex"></param>
        private static void ShowLoadConfigFailedMessage(Exception ex)
        {
            var message = "ユーザー設定ファイルの読み込みに失敗しました。";
            try { message = LANGUAGE["Program_LoadConfigFailed"]; }
            catch (KeyNotFoundException) { }

            MessageBox.Show(
                message + Environment.NewLine + Environment.NewLine + ex.Message,
                Application.ProductName,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}
