using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using HisoutenSupportTools.AddressUpdater.Lib.AddressService;
using HisoutenSupportTools.AddressUpdater.Lib.Model;
using HisoutenSupportTools.AddressUpdater.Lib.Model.Config;
using HisoutenSupportTools.AddressUpdater.Lib.Model.Tournament;
using HisoutenSupportTools.AddressUpdater.Lib.Tenco;

namespace HisoutenSupportTools.AddressUpdater.Lib.ViewModel
{
    /// <summary>
    /// 
    /// </summary>
    public partial class HostSettingViewModel : ViewModelBase
    {
        private const string TEMPLATES_FOLDER_NAME = "CommentTemplates";
        private const string SEND_BAT_FILENAME = @".\TencoSend.bat";
        private const int DEFAULT_RATING = 1500;


        #region property
        /// <summary>
        /// ユーザー設定の取得・設定
        /// </summary>
        [Browsable(false)]
        public UserConfig UserConfig { get; set; }

        /// <summary>
        /// 言語設定の取得・設定
        /// </summary>
        [Browsable(false)]
        public Language Language { get; set; }

        /// <summary>
        /// 登録モードの取得・設定
        /// </summary>
        [DefaultValue(null)]
        public RegisterMode? RegisterMode
        {
            get { return _registerMode; }
            set
            {
                if (_registerMode == value)
                    return;

                _registerMode = value;
                OnPropertyChanged("RegisterMode");
            }
        }
        private RegisterMode? _registerMode = null;

        /// <summary>
        /// IPの取得・設定
        /// </summary>
        [DefaultValue("")]
        public string Ip
        {
            get { return _ip; }
            set
            {
                if (_ip == value)
                    return;

                _ip = value;
                OnPropertyChanged("Ip");
                IsValidIp = ValidateIp();
            }
        }
        private string _ip = "";

        /// <summary>
        /// Ipが妥当かどうか
        /// </summary>
        [DefaultValue(true)]
        public bool IsValidIp
        {
            get { return _isValidIp; }
            set
            {
                if (_isValidIp == value)
                    return;

                _isValidIp = value;
                OnPropertyChanged("IsValidIp");
            }
        }
        private bool _isValidIp = true;

        /// <summary>
        /// トーナメント種別リスト
        /// </summary>
        public ReadOnlyCollection<KeyValuePair<string, TournamentTypes>> TournamentTypeList
        {
            get
            {
                if (_tournamentTypeList != null)
                    return _tournamentTypeList;

                var tournamentTypeList = new Collection<KeyValuePair<string, TournamentTypes>>();
                var types = new List<TournamentTypes>((TournamentTypes[])Enum.GetValues(typeof(TournamentTypes)));
                types.Remove(TournamentTypes.不明);
                foreach (var type in types)
                    tournamentTypeList.Add(new KeyValuePair<string, TournamentTypes>(EnumTextAttribute.GetText(type), type));

                _tournamentTypeList = new ReadOnlyCollection<KeyValuePair<string, TournamentTypes>>(tournamentTypeList);
                return _tournamentTypeList;
            }
        }
        private ReadOnlyCollection<KeyValuePair<string, TournamentTypes>> _tournamentTypeList;

        /// <summary>
        /// 大会種別の取得・設定
        /// </summary>
        [DefaultValue(TournamentTypes.トーナメント)]
        public TournamentTypes SelectedTournamentType
        {
            get { return _selectedTournamentType; }
            set
            {
                if (_selectedTournamentType == value)
                    return;

                _selectedTournamentType = value;
                OnPropertyChanged("TournamentType");
                IsValidUserCount = ValidateUserCount();
            }
        }
        private TournamentTypes _selectedTournamentType = TournamentTypes.トーナメント;

        /// <summary>
        /// ポートの取得・設定
        /// </summary>
        [DefaultValue(10800)]
        public int Port
        {
            get { return _port; }
            set
            {
                if (_port == value)
                    return;

                _port = value;
                OnPropertyChanged("Port");


                if (IsPortClosed)
                {
                    upnp.Port = value;
                    return;
                }

                IsPortClosed = upnp.Close();
                if (IsPortClosed)
                    upnp.Port = value;
            }
        }
        private int _port = 10800;

        /// <summary>
        /// ポート閉じてるかどうか
        /// </summary>
        [DefaultValue(true)]
        public bool IsPortClosed
        {
            get { return _isPortClosed; }
            private set
            {
                if (_isPortClosed == value)
                    return;

                _isPortClosed = value;
                OnPropertyChanged("IsPortClosed");
            }
        }
        private bool _isPortClosed = true;

        /// <summary>
        /// (大会)人数の取得・設定
        /// </summary>
        [DefaultValue(4)]
        public int UserCount
        {
            get { return _userCount; }
            set
            {
                if (_userCount == value)
                    return;

                _userCount = value;
                OnPropertyChanged("UserCount");
                IsValidUserCount = ValidateUserCount();
            }
        }
        private int _userCount = 4;

        /// <summary>
        /// 人数が妥当かどうか
        /// </summary>
        [DefaultValue(true)]
        public bool IsValidUserCount
        {
            get { return _isValidUserCount; }
            private set
            {
                if (_isValidUserCount == value)
                    return;

                _isValidUserCount = value;
                OnPropertyChanged("IsValidUserCount");
            }
        }
        private bool _isValidUserCount = true;

        /// <summary>
        /// ランク一覧の取得
        /// </summary>
        public ReadOnlyCollection<string> Ranks
        {
            get
            {
                if (UserConfig == null)
                    return new ReadOnlyCollection<string>(new string[] { string.Empty, "Easy", "Normal", "Extra", "Hard", "Lunatic" });

                var ranks = new Collection<string>();
                ranks.Add(string.Empty);
                foreach (var rank in UserConfig.Ranks)
                    ranks.Add(rank);

                return new ReadOnlyCollection<string>(ranks);
            }
        }

        /// <summary>
        /// ランクの取得・設定
        /// </summary>
        [DefaultValue("")]
        public string Rank
        {
            get { return _rank; }
            set
            {
                if (_rank == value)
                    return;

                _rank = value;
                OnPropertyChanged("Rank");
            }
        }
        private string _rank = "";

        /// <summary>
        /// キャラ一覧の取得
        /// </summary>
        public ReadOnlyCollection<KeyValuePair<string, Th135Characters>> Characters
        {
            get
            {
                var characters = new Collection<KeyValuePair<string, Th135Characters>>();
                foreach (Th135Characters value in Enum.GetValues(typeof(Th135Characters)))
                    characters.Add(new KeyValuePair<string, Th135Characters>(EnumTextAttribute.GetText(value), value));
                return new ReadOnlyCollection<KeyValuePair<string, Th135Characters>>(characters);
            }
        }

        /// <summary>
        /// 選択されたキャラ
        /// </summary>
        [DefaultValue(Th135Characters.Random)]
        public Th135Characters SelectedCharacter
        {
            get { return _selectedCharacter; }
            set
            {
                if (_selectedCharacter == value)
                    return;

                _selectedCharacter = value;
                OnPropertyChanged("Character");
            }
        }
        private Th135Characters _selectedCharacter = Th135Characters.Random;

        /// <summary>
        /// キャラ名を隠すかどうかの取得・設定
        /// </summary>
        [DefaultValue(false)]
        public bool IsHideCharacter
        {
            get { return _isHideCharacter; }
            set
            {
                if (_isHideCharacter == value)
                    return;

                _isHideCharacter = value;
                OnPropertyChanged("IsHideCharacter");
            }
        }
        private bool _isHideCharacter = false;

        /// <summary>
        /// テンプレート一覧の取得
        /// </summary>
        public ReadOnlyCollection<KeyValuePair<string, string>> Templates
        {
            get
            {
                if (_templates == null)
                    LoadTemplates();

                return _templates;
            }
            private set
            {
                _templates = value;
                OnPropertyChanged("Templates");
            }
        }
        private ReadOnlyCollection<KeyValuePair<string, string>> _templates;

        /// <summary>
        /// コメントの取得・設定
        /// </summary>
        [DefaultValue("")]
        public string Comment
        {
            get { return _comment; }
            set
            {
                if (_comment == value)
                    return;

                _comment = value;
                OnPropertyChanged("Comment");
            }
        }
        private string _comment = "";

        /// <summary>
        /// 選択されているゲーム(for Tenco)
        /// </summary>
        [DefaultValue(Games.Th135)]
        public Games SelectedGame
        {
            get { return _selectedGame; }
            set
            {
                if (_selectedGame == value)
                    return;

                _selectedGame = value;
                OnPropertyChanged("SelectedGame");
            }
        }
        private Games _selectedGame = Games.Th135;

        /// <summary>
        /// 選択されているタブのIndex(for Tenco)
        /// </summary>
        [DefaultValue(0)]
        public int SelectedTabIndex
        {
            get { return _selectedTabIndex; }
            set
            {
                if (_selectedTabIndex == value)
                    return;

                _selectedTabIndex = value;
                if (value == 0)
                    SelectedGame = Games.Th135;
                else if (value == 1)
                    SelectedGame = Games.Th105;
                OnPropertyChanged("SelectedTabIndex");
            }
        }
        private int _selectedTabIndex = 0;

        /// <summary>
        /// Tenco!フォルダ表示名
        /// </summary>
        [DefaultValue("")]
        public string TencoFolderName
        {
            get { return _tencoFolderName; }
            private set
            {
                if (_tencoFolderName == value)
                    return;

                _tencoFolderName = value;
                OnPropertyChanged("TencoFolderName");
            }
        }
        private string _tencoFolderName = "";

        /// <summary>
        /// Tenco!フォルダ表示名(非)
        /// </summary>
        [DefaultValue("")]
        public string TencoFolderName2
        {
            get { return _tencoFolderName2; }
            private set
            {
                if (_tencoFolderName2 == value)
                    return;

                _tencoFolderName2 = value;
                OnPropertyChanged("TencoFolderName2");
            }
        }
        private string _tencoFolderName2 = "";


        /// <summary>
        /// Tenco!フォルダの取得・設定
        /// </summary>
        [DefaultValue("")]
        public string TencoFolder
        {
            get { return _tencoFolder; }
            set
            {
                if (_tencoFolder == value)
                    return;

                _tencoFolder = value;
                TencoFolderName = value.Substring(value.LastIndexOf("\\") + 1, value.Length - value.LastIndexOf("\\") - 1);
                OnPropertyChanged("TencoFolder");
            }
        }
        private string _tencoFolder = "";

        /// <summary>
        /// Tenco!フォルダの取得・設定(非)
        /// </summary>
        [DefaultValue("")]
        public string TencoFolder2
        {
            get { return _tencoFolder2; }
            set
            {
                if (_tencoFolder2 == value)
                    return;

                _tencoFolder2 = value;
                TencoFolderName2 = value.Substring(value.LastIndexOf("\\") + 1, value.Length - value.LastIndexOf("\\") - 1);
                OnPropertyChanged("TencoFolder2");
            }
        }
        private string _tencoFolder2 = "";

        /// <summary>
        /// Tenco!アカウント名の取得・設定
        /// </summary>
        [DefaultValue("")]
        public string TencoAccountName
        {
            get { return _tencoAccountName; }
            set
            {
                if (_tencoAccountName == value)
                    return;

                _tencoAccountName = value;
                OnPropertyChanged("TencoAccountName");
            }
        }
        private string _tencoAccountName = "";

        /// <summary>
        /// Tenco!アカウント名の取得・設定(非)
        /// </summary>
        [DefaultValue("")]
        public string TencoAccountName2
        {
            get { return _tencoAccountName2; }
            set
            {
                if (_tencoAccountName2 == value)
                    return;

                _tencoAccountName2 = value;
                OnPropertyChanged("TencoAccountName2");
            }
        }
        private string _tencoAccountName2 = "";

        /// <summary>
        /// レート情報
        /// </summary>
        public ReadOnlyCollection<Th105Rating> Ratings
        {
            get { return _ratings; }
            private set
            {
                if (_ratings == value)
                    return;

                _ratings = value;
                OnPropertyChanged("Ratings");
            }
        }
        private ReadOnlyCollection<Th105Rating> _ratings = new ReadOnlyCollection<Th105Rating>(new Collection<Th105Rating>());

        /// <summary>
        /// レート情報(非)
        /// </summary>
        public ReadOnlyCollection<Th135Rating> Ratings2
        {
            get { return _ratings2; }
            private set
            {
                if (_ratings2 == value)
                    return;

                _ratings2 = value;
                OnPropertyChanged("Ratings2");
            }
        }
        private ReadOnlyCollection<Th135Rating> _ratings2 = new ReadOnlyCollection<Th135Rating>(new Collection<Th135Rating>());
        #endregion


        /// <summary>
        /// 
        /// </summary>
        public HostSettingViewModel()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        public HostSettingViewModel(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }


        #region GetHost, GetTencoHost
        /// <summary>
        /// ホストの取得
        /// </summary>
        public host GetHost()
        {
            return new host(Ip, Port, Rank, Comment);
        }

        /// <summary>
        /// Tenco!ホストの取得
        /// </summary>
        /// <returns></returns>
        public host GetTencoHost()
        {
            if (SelectedGame == Games.Th135)
            {
                Th135Rating rating = null;
                foreach (var r in Ratings2)
                {
                    if (r.Character == SelectedCharacter)
                    {
                        rating = r;
                        break;
                    }
                }

                if (rating == null)
                {
                    if (IsHideCharacter)
                    {
                        return new host(
                            Ip, Port,
                            DEFAULT_RATING.ToString() + "?",
                            Comment);
                    }
                    else
                    {
                        return new host(
                            Ip, Port,
                            string.Format("{0}:{1}?", EnumTextAttribute.GetText(SelectedCharacter), DEFAULT_RATING),
                            Comment);
                    }
                }


                if (IsHideCharacter)
                {
                    return new host(
                        Ip, Port,
                        string.Format("{0}±{1}", rating.Value, rating.Deviation),
                        Comment);
                }
                else
                {
                    return new host(
                        Ip, Port,
                        string.Format("{0}:{1}±{2}", EnumTextAttribute.GetText(rating.Character), rating.Value, rating.Deviation),
                        Comment);
                }
            }
            else if (SelectedGame == Games.Th105)
            {
                Th105Rating rating = null;
                foreach (var r in Ratings)
                {
                    var searchCharacter = (byte)SelectedCharacter;
                    if (SelectedCharacter == Th135Characters.Random)
                        searchCharacter = (byte)Th105Characters.Random;

                    if (r.Character == (Th105Characters)searchCharacter)
                    {
                        rating = r;
                        break;
                    }
                }

                if (rating == null)
                {
                    if (IsHideCharacter)
                    {
                        return new host(
                            Ip, Port,
                            DEFAULT_RATING.ToString() + "?",
                            Comment);
                    }
                    else
                    {
                        return new host(
                            Ip, Port,
                            string.Format("{0}:{1}?", EnumTextAttribute.GetText(SelectedCharacter), DEFAULT_RATING),
                            Comment);
                    }
                }


                if (IsHideCharacter)
                {
                    return new host(
                        Ip, Port,
                        string.Format("{0}±{1}", rating.Value, rating.Deviation),
                        Comment);
                }
                else
                {
                    return new host(
                        Ip, Port,
                        string.Format("{0}:{1}±{2}", EnumTextAttribute.GetText(rating.Character), rating.Value, rating.Deviation),
                        Comment);
                }
            }
            else
            {
                throw new ApplicationException();
            }
        }
        #endregion


        #region LoadTemplates
        /// <summary>
        /// テンプレートのロード
        /// </summary>
        public void LoadTemplates()
        {
            try
            {
                var templates = new Collection<KeyValuePair<string, string>>();
                var defaultCommentText = "コメント";
                try { defaultCommentText = Language["HostSettingTab_DefaultCommentTemplate"]; }
                catch (KeyNotFoundException) { }
                templates.Add(new KeyValuePair<string, string>(defaultCommentText, string.Empty));
                var directory = new DirectoryInfo(TEMPLATES_FOLDER_NAME);
                if (!directory.Exists)
                {
                    Templates = new ReadOnlyCollection<KeyValuePair<string, string>>(templates);
                    return;
                }

                var txtFiles = directory.GetFiles("*.txt");
                foreach (var file in txtFiles)
                {
                    var templateName = file.Name;
                    if (file.Extension != null && file.Extension != string.Empty)
                        templateName = templateName.Replace(file.Extension, string.Empty);
                    try
                    {
                        using (var stream = file.OpenRead())
                        using (var reader = new StreamReader(stream, Encoding.GetEncoding("shift_jis")))
                        {
                            try { templates.Add(new KeyValuePair<string, string>(templateName, reader.ReadToEnd())); }
                            finally { reader.Close(); }
                        }
                    }
                    catch (UnauthorizedAccessException ex) { Debug.WriteLine(ex); }
                    catch (DirectoryNotFoundException ex) { Debug.WriteLine(ex); }
                    catch (IOException ex) { Debug.WriteLine(ex); }
                }

                Templates = new ReadOnlyCollection<KeyValuePair<string, string>>(templates);
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
        }
        #endregion

        #region OpenPort
        /// <summary>
        /// 
        /// </summary>
        public void OpenPort()
        {
            IsPortClosed = !upnp.Open(Application.ProductName);
        }
        #endregion

        #region ClosePort()
        /// <summary>
        /// 
        /// </summary>
        public void ClosePort()
        {
            IsPortClosed = upnp.Close();
        }
        #endregion

        #region GetTencoRatings
        private DateTime _lastRatingGetTime = DateTime.MinValue;
        private DateTime _lastRatingGetTime2 = DateTime.MinValue;

        /// <summary>
        /// 
        /// </summary>
        public void GetTencoRatings()
        {
            if(SelectedGame == Games.Th135)
            {
                if (string.IsNullOrEmpty(TencoAccountName2))
                    return;

                // 取得は毎時間1回まで
                var allowTime = _lastRatingGetTime2;
                allowTime = allowTime.AddHours(1);
                if (DateTime.Now < allowTime)
                    return;

                try
                {
                    var tenoClient = new Th135TencoClient(TencoAccountName2);
                    tenoClient.UpdateRating();
                    var ratings = tenoClient.GetRatings();

                    decimal ratingSum = 0;
                    decimal deviationSum = 0;
                    int matchAccountsSum = 0;
                    int matchCountSum = 0;
                    foreach (var rating in ratings)
                    {
                        ratingSum += rating.Value;
                        deviationSum += rating.Deviation * rating.Deviation;
                        matchAccountsSum += rating.MatchAccounts;
                        matchCountSum += rating.MatchCount;
                    }

                    var allRatings = new Collection<Th135Rating>();
                    foreach (var rating in ratings)
                        allRatings.Add(rating);

                    if (0 < ratings.Count)
                    {
                        decimal ratingAverage = ratingSum / ratings.Count;
                        decimal rdesSum = 0;
                        foreach (Th135Rating rating in ratings)
                        {
                            rdesSum += (rating.Value - ratingAverage) * (rating.Value - ratingAverage);
                        }
                        var average = new Th135Rating(
                            Th135Characters.Random,
                            (int)Math.Round(ratingAverage, 0),
                            (int)Math.Round(Math.Sqrt((double)((deviationSum + rdesSum) / ratings.Count)), 0),
                            matchAccountsSum,
                            matchCountSum);
                        allRatings.Add(average);
                    }

                    Ratings2 = new ReadOnlyCollection<Th135Rating>(allRatings);

                    _lastRatingGetTime2 = DateTime.Now;
                }
                catch (ArgumentNullException ex) { Debug.WriteLine(ex); }
                catch (UriFormatException ex) { Debug.WriteLine(ex); }
                catch (WebException ex) { Debug.WriteLine(ex); }
                catch (Exception ex) { System.Diagnostics.Debug.WriteLine(ex); }
            }
            else if (SelectedGame == Games.Th105)
            {
                if (string.IsNullOrEmpty(TencoAccountName))
                    return;

                // 取得は毎時間1回まで
                var allowTime = _lastRatingGetTime;
                allowTime = allowTime.AddHours(1);
                if (DateTime.Now < allowTime)
                    return;

                try
                {
                    var tenoClient = new Th105TencoClient(TencoAccountName);
                    tenoClient.UpdateRating();
                    var ratings = tenoClient.GetRatings();

                    decimal ratingSum = 0;
                    decimal deviationSum = 0;
                    int matchAccountsSum = 0;
                    int matchCountSum = 0;
                    foreach (var rating in ratings)
                    {
                        ratingSum += rating.Value;
                        deviationSum += rating.Deviation * rating.Deviation;
                        matchAccountsSum += rating.MatchAccounts;
                        matchCountSum += rating.MatchCount;
                    }

                    var allRatings = new Collection<Th105Rating>();
                    foreach (var rating in ratings)
                        allRatings.Add(rating);

                    if (0 < ratings.Count)
                    {
                        decimal ratingAverage = ratingSum / ratings.Count;
                        decimal rdesSum = 0;
                        foreach (Th105Rating rating in ratings)
                        {
                            rdesSum += (rating.Value - ratingAverage) * (rating.Value - ratingAverage);
                        }
                        var average = new Th105Rating(
                            Th105Characters.Random,
                            (int)Math.Round(ratingAverage, 0),
                            (int)Math.Round(Math.Sqrt((double)((deviationSum + rdesSum) / ratings.Count)), 0),
                            matchAccountsSum,
                            matchCountSum);
                        allRatings.Add(average);
                    }

                    Ratings = new ReadOnlyCollection<Th105Rating>(allRatings);

                    _lastRatingGetTime = DateTime.Now;
                }
                catch (ArgumentNullException ex) { Debug.WriteLine(ex); }
                catch (UriFormatException ex) { Debug.WriteLine(ex); }
                catch (WebException ex) { Debug.WriteLine(ex); }
                catch (Exception ex) { System.Diagnostics.Debug.WriteLine(ex); }
            }
        }
        #endregion

        #region OpenMypage
        /// <summary>
        /// 
        /// </summary>
        public void OpenMypage()
        {
            if (string.IsNullOrEmpty(TencoAccountName))
                return;

            Process.Start(string.Format(
                "http://tenco.info/game/3/account/{0}/",
                TencoAccountName));
        }

        /// <summary>
        /// 
        /// </summary>
        public void OpenMypage2()
        {
            if (string.IsNullOrEmpty(TencoAccountName2))
                return;

            Process.Start(string.Format(
                "http://tenco.info/game/4/account/{0}/",
                TencoAccountName2));
        }
        #endregion

        #region SendRecords
        /// <summary>
        /// 
        /// </summary>
        public void SendRecords()
        {
            try { Process.Start(new ProcessStartInfo("hks_report") { WorkingDirectory = TencoFolder }); }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
        }

        /// <summary>
        /// 
        /// </summary>
        public void SendRecords2()
        {
            try { Process.Start(new ProcessStartInfo("skr_report") { WorkingDirectory = TencoFolder2 }); }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
        }
        #endregion


        #region private
        private bool ValidateIp()
        {
            if (Ip.Replace(" ", string.Empty) == string.Empty)
                return true;

            var ipParts = Ip.Split('.');

            if (ipParts.Length != 4)
                return false;

            if (ipParts[0].Replace(" ", string.Empty) == "10")
                return false;

            if (ipParts[0].Replace(" ", string.Empty) == "192" && ipParts[1].Replace(" ", string.Empty) == "168")
                return false;

            try
            {
                if (ipParts[0].Replace(" ", string.Empty) == "172" && 16 <= int.Parse(ipParts[1].Replace(" ", string.Empty)) && int.Parse(ipParts[1].Replace(" ", string.Empty)) <= 31)
                    return false;

                IPAddress.Parse(Ip.Replace(" ", string.Empty));
                return true;
            }
            catch (FormatException) { return false; }
        }

        bool ValidateUserCount()
        {
            switch (SelectedTournamentType)
            {
                case TournamentTypes.トーナメント:
                    // 4,8,16,32...
                    var tmp = 4;
                    while (tmp <= UserCount)
                    {
                        if (tmp == UserCount)
                            return true;
                        tmp *= 2;
                    }
                    return false;
                //case TournamentTypes.トーナメント2人:
                //    // 4,8,16,32...
                //    break;
                //case TournamentTypes.トーナメント3人:
                //    // 6,12, 24,48...
                //    break;
                case TournamentTypes.総当たり:
                    return 2 <= UserCount;
                case TournamentTypes.総当たり2人:
                    return 4 <= UserCount && UserCount % 2 == 0;
                case TournamentTypes.総当たり3人:
                    return 6 <= UserCount && UserCount % 3 == 0;
                default:
                    return false;
            }
        }
        #endregion
    }
}
