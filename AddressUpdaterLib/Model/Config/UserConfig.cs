using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using HisoutenSupportTools.AddressUpdater.Lib.IO;
using HisoutenSupportTools.AddressUpdater.Lib.Api;

namespace HisoutenSupportTools.AddressUpdater.Lib.Model.Config
{
    /// <summary>
    /// ユーザー設定
    /// </summary>
    [Serializable]
    [XmlRoot("ユーザー設定")]
    public class UserConfig
    {
        /// <summary>ユーザー設定ファイル名称</summary>
        private static readonly string FILENAME = Application.ProductName + "User.config";
        /// <summary>保存リトライ回数</summary>
        private const int RETRY_COUNT = 3;

        /// <summary>デフォルトのランクリスト</summary>
        public static readonly Collection<string> DEFAULT_RANKS =
            new Collection<string>(new string[] { "Easy", "Normal", "Extra", "Hard", "Lunatic" });

        /// <summary>デフォルトのサーバー情報リスト</summary>
        public static readonly Collection<ServerInformation> DEFAULT_SERVER_INFORMATIONS =
            new Collection<ServerInformation>(new ServerInformation[]
            {
                new ServerInformation("心綺楼(Tenco!鯖)", AddressUpdaterUri.TENCO_SERVER_TH135),
            });

        /// <summary>設定ファイル</summary>
        private XmlConfigFile<UserConfig> _configFile = new XmlConfigFile<UserConfig>(new FileInfo(FILENAME));

        #region 設定値
        /// <summary>初期入力IP</summary>
        [XmlElement("初期入力IP")]
        public string ServerIp = string.Empty;
        /// <summary>初期入力ポート</summary>
        [XmlElement("初期入力ポート")]
        public int ServerPort = 10800;
        /// <summary>更新間隔</summary>
        [XmlElement("更新間隔")]
        public int UpdateSpan = 15;
        /// <summary>多重起動を禁止するかどうか</summary>
        [XmlElement("多重起動を禁止する")]
        public bool DisableMultiBoot = true;
        /// <summary>チャット先頭文字列</summary>
        [XmlElement("チャット先頭文字列")]
        public string ChatPrefix = string.Empty;
        /// <summary>クライアント分割線</summary>
        [XmlElement("クライアント分割線")]
        public Orientation ClientDivisionOrientation = Orientation.Horizontal;
        /// <summary>大会を表示するかどうか</summary>
        [XmlElement("大会を表示するかどうか")]
        public bool ShowTournaments = true;
        /// <summary>大会のエントリー名</summary>
        [XmlElement("大会のエントリー名")]
        public string TournamentEntryName = string.Empty;

        /// <summary>ツール背景色</summary>
        [XmlElement("ツール背景色")]
        public ColorData ToolBackColor = ColorData.FromColor(SystemColors.Window);
        /// <summary>一般テキスト色</summary>
        [XmlElement("一般テキスト色")]
        public ColorData GeneralTextColor = ColorData.FromColor(SystemColors.WindowText);
        /// <summary>待機中ホスト背景色</summary>
        [XmlElement("待機中ホスト背景色")]
        public ColorData WaitingHostBackColor = ColorData.FromColor(SystemColors.Window);
        /// <summary>対戦中ホスト背景色</summary>
        [XmlElement("対戦中ホスト背景色")]
        public ColorData FightingHostBackColor = new ColorData() { R = 230, G = 230, B = 230 };
        /// <summary>チャット文字色</summary>
        [XmlElement("チャット文字色")]
        public ColorData ChatForeColor = ColorData.FromColor(SystemColors.WindowText);
        /// <summary>チャット背景色</summary>
        [XmlElement("チャット背景色")]
        public ColorData ChatBackColor = ColorData.FromColor(SystemColors.Window);
        /// <summary>ホスト表示フォント</summary>
        [XmlElement("ホスト表示フォント")]
        public FontName HostFont = new FontName() { Name = "MS UI Gothic" };
        /// <summary>チャット表示フォント</summary>
        [XmlElement("チャット表示フォント")]
        public FontName ChatFont = new FontName() { Name = "ＭＳ ゴシック" };

        /// <summary>ランクリスト</summary>
        [XmlElement("ランク")]
        public Collection<string> Ranks = new Collection<string>();
        /// <summary>ランク初期値</summary>
        [XmlElement("ランク初期値")]
        public string Rank = string.Empty;
        /// <summary>コメント初期値</summary>
        [XmlElement("コメント初期値")]
        public string Comment = string.Empty;
        /// <summary>チャットフィルターキーワード</summary>
        [XmlElement("チャットフィルターキーワード")]
        public Collection<string> FilterKeywords = new Collection<string>();
        /// <summary>ハイライトキーワード</summary>
        [XmlElement("ハイライトキーワード")]
        public Collection<string> HighlightKeywords = new Collection<string>();

        /// <summary>おまけ機能</summary>
        [XmlElement("おまけ機能")]
        public Collection<ExtraWindowInformation> ExtraWindowInformations = new Collection<ExtraWindowInformation>();

        /// <summary>Tencoレーティングを使用するかどうか</summary>
        [XmlElement("Tencoレーティングを使用するかどうか")]
        public bool UseTenco = false;
        /// <summary>Tenco使用キャラ</summary>
        [XmlElement("Tenco使用キャラ")]
        public int TencoCharacter = -1;
        /// <summary>Tencoのキャラを非表示にするかどうか</summary>
        [XmlElement("Tencoキャラ非表示")]
        public bool HideTencoCharacter = false;
        /// <summary>Tenco保存フォルダ</summary>
        [XmlElement("Tenco保存フォルダ")]
        public string TencoFolder = string.Empty;
        /// <summary>Tenco保存フォルダ(非)</summary>
        [XmlElement("Tenco保存フォルダ_非")]
        public string TencoFolder2 = string.Empty;
        /// <summary>Tencoアカウント名</summary>
        [XmlElement("Tencoアカウント名")]
        public string TencoAccount = string.Empty;
        /// <summary>Tencoアカウント名(非)</summary>
        [XmlElement("Tencoアカウント名_非")]
        public string TencoAccount2 = string.Empty;

        /// <summary>オートマッチング情報</summary>
        [XmlElement("オートマッチング情報")]
        public AutoMatchingInformation AutoMatchingInformation = new AutoMatchingInformation();

        /// <summary>同時起動ソフトリスト</summary>
        [XmlElement("同時起動ソフト")]
        public Collection<SoftwareInformation> BootSameTimeSofts = new Collection<SoftwareInformation>();
        /// <summary>サーバー情報リスト</summary>
        [XmlElement("サーバー情報")]
        public Collection<ServerInformation> ServerInformations = new Collection<ServerInformation>();
        /// <summary>ウィンドウ設定リスト</summary>
        [XmlElement("ウィンドウ設定")]
        public Collection<WindowConfig> WindowConfigs = new Collection<WindowConfig>();
        #endregion

        /// <summary>
        /// インスタンスの生成
        /// </summary>
        public UserConfig() { }

        /// <summary>
        /// サーバー情報を取得
        /// </summary>
        /// <param name="serverName">サーバー名</param>
        /// <returns>サーバー情報</returns>
        /// <exception cref="System.Collections.Generic.KeyNotFoundException"></exception>
        public ServerInformation GetServerInformation(string serverName)
        {
            foreach (var serverInformation in ServerInformations)
            {
                if (serverInformation.Name == serverName)
                    return serverInformation;
            }

            throw new KeyNotFoundException();
        }

        /// <summary>
        /// サーバー情報を設定
        /// </summary>
        /// <param name="serverInformation">サーバー情報</param>
        public void SetServerInformation(ServerInformation serverInformation)
        {
            for (var i = 0; i < ServerInformations.Count; i++)
            {
                if (ServerInformations[i].Name == serverInformation.Name)
                {
                    ServerInformations[i] = serverInformation;
                    return;
                }
            }

            ServerInformations.Add(serverInformation);
        }

        /// <summary>
        /// ウィンドウ設定を取得
        /// </summary>
        /// <param name="serverName">サーバー名</param>
        /// <returns>サーバー情報</returns>
        /// <exception cref="System.Collections.Generic.KeyNotFoundException"></exception>
        public WindowConfig GetWindowConfig(string serverName)
        {
            foreach (var windowConfig in WindowConfigs)
            {
                if (windowConfig.ServerName == serverName)
                    return windowConfig;
            }

            throw new KeyNotFoundException();
        }

        /// <summary>
        /// ウィンドウ設定を設定
        /// </summary>
        /// <param name="windowConfig"></param>
        public void SetWindowConfig(WindowConfig windowConfig)
        {
            for (var i = 0; i < WindowConfigs.Count; i++)
            {
                if (WindowConfigs[i].ServerName == windowConfig.ServerName)
                {
                    WindowConfigs[i] = windowConfig;
                    return;
                }
            }

            WindowConfigs.Add(windowConfig);
        }

        /// <summary>
        /// 設定読み込み
        /// </summary>
        /// <exception cref="System.IO.FileNotFoundException">ファイルが見つかりません。</exception>
        /// <exception cref="System.Security.SecurityException">呼び出し元に、必要なアクセス許可がありません。</exception>
        /// <exception cref="System.UnauthorizedAccessException">path が読み取り専用か、またはディレクトリです。</exception>
        /// <exception cref="System.InvalidOperationException">逆シリアル化中にエラーが発生しました。元の例外には、System.Exception.InnerException プロパティを使用してアクセスできます。</exception>
        public void Load()
        {
            try
            {
                var config = _configFile.Deserialize();

                ServerIp = config.ServerIp;
                ServerPort = config.ServerPort;
                UpdateSpan = config.UpdateSpan;
                DisableMultiBoot = config.DisableMultiBoot;
                ChatPrefix = config.ChatPrefix;
                ClientDivisionOrientation = config.ClientDivisionOrientation;
                ShowTournaments = config.ShowTournaments;
                TournamentEntryName = config.TournamentEntryName;

                ToolBackColor = config.ToolBackColor;
                GeneralTextColor = config.GeneralTextColor;
                WaitingHostBackColor = config.WaitingHostBackColor;
                FightingHostBackColor = config.FightingHostBackColor;
                ChatForeColor = config.ChatForeColor;
                ChatBackColor = config.ChatBackColor;
                HostFont = config.HostFont;
                ChatFont = config.ChatFont;

                Ranks = config.Ranks;
                Rank = config.Rank;
                Comment = config.Comment.Replace("\n", "\r\n");
                FilterKeywords = config.FilterKeywords;
                HighlightKeywords = config.HighlightKeywords;

                ExtraWindowInformations = config.ExtraWindowInformations;

                BootSameTimeSofts = config.BootSameTimeSofts;
                ServerInformations = config.ServerInformations;
                WindowConfigs = config.WindowConfigs;

                UseTenco = config.UseTenco;
                HideTencoCharacter = config.HideTencoCharacter;
                TencoCharacter = config.TencoCharacter;
                TencoFolder = config.TencoFolder;
                TencoAccount = config.TencoAccount;
                TencoFolder2 = config.TencoFolder2;
                TencoAccount2 = config.TencoAccount2;

                AutoMatchingInformation = config.AutoMatchingInformation;
                AutoMatchingInformation.Comment = AutoMatchingInformation.Comment.Replace("\n", "\r\n");
                if (string.IsNullOrEmpty(AutoMatchingInformation.AccountName))
                {
                    AutoMatchingInformation.AccountName = TencoAccount2;
                    AutoMatchingInformation.Character = TencoCharacter;
                    AutoMatchingInformation.Ip = ServerIp;
                    AutoMatchingInformation.Port = ServerPort;
                }
            }
            catch (FileNotFoundException ex)
            {
                throw ex;
            }
            finally
            {
                if (Ranks.Count <= 0)
                    Ranks = new Collection<string>(DEFAULT_RANKS);
                if (ServerInformations.Count <= 0 && Kernel32.GetUserDefaultLangID() == Kernel32.JAPANESE)
                    ServerInformations = new Collection<ServerInformation>(DEFAULT_SERVER_INFORMATIONS);
            }
        }

        /// <summary>
        /// 設定保存
        /// </summary>
        /// <exception cref="System.UnauthorizedAccessException">呼び出し元に、必要なアクセス許可がありません。</exception>
        /// <exception cref="System.InvalidOperationException">シリアル化中にエラーが発生しました。元の例外には、System.Exception.InnerException プロパティを使用してアクセスできます。</exception>
        /// <exception cref="System.IO.IOException">I/O エラーが発生しました。</exception>
        public void Save()
        {
            RetryAction(
                new Action(delegate { _configFile.Serialize(this); }),
                RETRY_COUNT);
        }

        /// <summary>
        /// 指定回数、例外を無視して処理を試行する。
        /// 指定回数を超えた場合は最後の例外をそのままthrowする。
        /// retryCount = 3 の場合 3回試行し、3回目に例外が発生した場合はその例外がthrowされる。
        /// </summary>
        /// <param name="action">処理</param>
        /// <param name="retryCount">試行回数</param>
        void RetryAction(Action action, int retryCount)
        {
            for (var i = 0; i < retryCount; i++)
            {
                try
                {
                    action.Invoke();
                    return;
                }
                catch (Exception ex)
                {
                    if (i < retryCount - 1)
                        continue;
                    else
                        throw ex;
                }
            }
        }
    }
}
