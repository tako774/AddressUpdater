using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Media;
using HisoutenSupportTools.AddressUpdater.Lib.AddressService;
using HisoutenSupportTools.AddressUpdater.Lib.Controller;
using HisoutenSupportTools.AddressUpdater.Lib.Model.Config;
using HisoutenSupportTools.AddressUpdater.Lib.Tenco;

namespace HisoutenSupportTools.AddressUpdater.Lib.ViewModel.AutoMatching
{
    /// <summary>
    /// 
    /// </summary>
    public partial class AutoMatchingViewModel : ViewModelBase
    {
        private const string MATCHING_WAV = @"matching.wav";

        private IClient _client;

        /// <summary>対戦相手が見つかった場合に発生します</summary>
        public event EventHandler OponentFound;

        #region property
        /// <summary>
        /// 
        /// </summary>
        public UserConfig UserConfig { get; set; }

        /// <summary></summary>
        [DefaultValue("")]
        public string AccountName
        {
            get { return _accountName; }
            set
            {
                if (_accountName == value)
                    return;

                _accountName = value;
                if (UserConfig != null)
                    UserConfig.AutoMatchingInformation.AccountName = value;
                OnPropertyChanged("AccountName");
            }
        }
        private string _accountName = "";

        /// <summary></summary>
        [DefaultValue(false)]
        public bool IsHostable
        {
            get { return _isHostable; }
            set
            {
                if (_isHostable == value)
                    return;

                _isHostable = value;
                if (UserConfig != null)
                    UserConfig.AutoMatchingInformation.IsHostable = value;
                OnPropertyChanged("IsHostable");
            }
        }
        private bool _isHostable;

        /// <summary></summary>
        [DefaultValue(false)]
        public bool IsRoomOnly
        {
            get { return _isRoomOnly; }
            set
            {
                if (_isRoomOnly == value)
                    return;

                _isRoomOnly = value;
                if (UserConfig != null)
                    UserConfig.AutoMatchingInformation.IsRoomOnry = value;
                OnPropertyChanged("IsRoomOnly");
            }
        }
        private bool _isRoomOnly = false;

        /// <summary></summary>
        [DefaultValue("")]
        public string Ip
        {
            get { return _ip; }
            set
            {
                if (_ip == value)
                    return;

                _ip = value;
                if (UserConfig != null)
                    UserConfig.AutoMatchingInformation.Ip = value;
                OnPropertyChanged("Ip");
            }
        }
        private string _ip = "";

        /// <summary></summary>
        [DefaultValue(10800)]
        public int Port
        {
            get { return _port; }
            set
            {
                if (_port == value)
                    return;

                _port = value;
                if (UserConfig != null)
                    UserConfig.AutoMatchingInformation.Port = value;
                OnPropertyChanged("Port");
            }
        }
        private int _port = 10800;

        /// <summary></summary>
        [DefaultValue(Th135Characters.Reimu)]
        public Th135Characters Character
        {
            get { return _character; }
            set
            {
                if (_character == value)
                    return;

                _character = value;
                if (UserConfig != null)
                    UserConfig.AutoMatchingInformation.Character = (int)value;
                OnPropertyChanged("Character");
            }
        }
        private Th135Characters _character = Th135Characters.Reimu;

        /// <summary></summary>
        public ReadOnlyCollection<KeyValuePair<string, Th135Characters>> Characters
        {
            get
            {
                var characters = new Collection<KeyValuePair<string, Th135Characters>>();
                foreach (Th135Characters value in Enum.GetValues(typeof(Th135Characters)))
                {
                    if (value != Th135Characters.Random)
                        characters.Add(new KeyValuePair<string, Th135Characters>(EnumTextAttribute.GetText(value), value));
                }
                return new ReadOnlyCollection<KeyValuePair<string, Th135Characters>>(characters);
            }
        }

        /// <summary></summary>
        [DefaultValue("コメント")]
        public string Comment
        {
            get { return _comment; }
            set
            {
                if (_comment == value)
                    return;

                _comment = value;
                if (UserConfig != null)
                    UserConfig.AutoMatchingInformation.Comment = value;
                OnPropertyChanged("Comment");
            }
        }
        private string _comment = "コメント";

        /// <summary></summary>
        [DefaultValue(150)]
        public int MatchingSpan
        {
            get { return _matchingSpan; }
            set
            {
                if (_matchingSpan == value)
                    return;

                _matchingSpan = value;
                if (UserConfig != null)
                    UserConfig.AutoMatchingInformation.MatchingSpan = value;
                OnPropertyChanged("MatchingSpan");
            }
        }
        private int _matchingSpan = 150;

        /// <summary></summary>
        public ReadOnlyCollection<Th135Rating> Ratings
        {
            get { return _ratings; }
            set
            {
                if (_ratings == value)
                    return;

                _ratings = value;
                OnPropertyChanged("Ratings");
            }
        }
        private ReadOnlyCollection<Th135Rating> _ratings;

        /// <summary></summary>
        [DefaultValue(null)]
        public tencoUser Oponent
        {
            get { return _oponent; }
            set
            {
                if (_oponent == value)
                    return;

                _oponent = value;
                if (_oponent != null && _oponent.Comment != null)
                    _oponent.Comment = _oponent.Comment.Replace("\n", "\r\n");
                OnPropertyChanged("Oponent");
            }
        }
        private tencoUser _oponent;

        /// <summary></summary>
        [DefaultValue(MatchingPhase.Empty)]
        public MatchingPhase Phase
        {
            get { return _phase; }
            set
            {
                if (_phase == value)
                    return;

                if (_phase == MatchingPhase.Empty || _phase == MatchingPhase.Matching)
                {
                    if (value != MatchingPhase.Empty && value != MatchingPhase.Matching)
                    {
                        if (File.Exists(MATCHING_WAV))
                        {
                            try
                            {
                                var soundPlayer = new SoundPlayer(MATCHING_WAV);
                                soundPlayer.Play();
                            }
                            catch (Exception ex) { System.Diagnostics.Debug.WriteLine(ex.Message); }
                        }

                        if (OponentFound != null)
                            OponentFound(this, EventArgs.Empty);
                    }
                }

                _phase = value;
                OnPropertyChanged("Phase");
            }
        }
        private MatchingPhase _phase = MatchingPhase.Empty;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public AutoMatchingViewModel()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        public AutoMatchingViewModel(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        public void BindClient(IClient client)
        {
            if (client == null)
                throw new ArgumentNullException("client");

            _client = client;
            _client.MatchingResultChanged += new EventHandler<EventArgs<HisoutenSupportTools.AddressUpdater.Lib.AddressService.matchingResult>>(client_MatchingResultChanged);
            _client.MatchingResultCleared += new EventHandler(client_MatchingResultCleared);
        }

        /// <summary>
        /// 
        /// </summary>
        public void UnBindClient()
        {
            _client.MatchingResultChanged -= client_MatchingResultChanged;
            _client.MatchingResultCleared -= client_MatchingResultCleared;
        }

        /// <summary>
        /// 
        /// </summary>
        public void ReflectUserConfig()
        {
            AccountName = UserConfig.AutoMatchingInformation.AccountName;
            Character = (Th135Characters)UserConfig.AutoMatchingInformation.Character;
            MatchingSpan = UserConfig.AutoMatchingInformation.MatchingSpan;
            IsHostable = UserConfig.AutoMatchingInformation.IsHostable;
            IsRoomOnly = UserConfig.AutoMatchingInformation.IsRoomOnry;
            Ip = UserConfig.AutoMatchingInformation.Ip;
            Port = UserConfig.AutoMatchingInformation.Port;
            Comment = UserConfig.AutoMatchingInformation.Comment;
        }

        #region GetRating
        /// <summary>
        /// 
        /// </summary>
        public void GetRating()
        {
        }
        #endregion

        #region Register
        /// <summary>
        /// 
        /// </summary>
        public void Register()
        {
            Phase = MatchingPhase.Matching;
            Oponent = null;
            th135Characters character = new th135Characters() { Value = (int)Character };
            Th135Rating rating = null;
            foreach (var r in Ratings)
                if (r.Character == Character)
                    rating = r;
            var ratingValue = (rating == null) ? 1500 : rating.Value;
            var ratingDeviation = (rating == null) ? 350 : rating.Deviation;

            _client.RegisterMatching(new tencoUser()
            {
                AccountName = AccountName,
                IsHostable = IsHostable,
                IsRoomOnly = IsRoomOnly,
                Ip = Ip,
                Port = Port,
                Comment = Comment,
                MatchingSpan = MatchingSpan,
                Rating = new tencoRating()
                {
                    Character = character,
                    Value = ratingValue,
                    Deviation = ratingDeviation,
                },
            });
        }
        #endregion

        #region Unregister
        /// <summary>
        /// 
        /// </summary>
        public void Unregister()
        {
            Phase = MatchingPhase.Matching;
            Oponent = null;
            _client.UnregisterMatching();
        }
        #endregion

        #region SetPrepared
        /// <summary>
        /// 
        /// </summary>
        public void SetPrepared()
        {
            _client.SetPrepared(Oponent.Id);
        }
        #endregion

        #region AddHistory
        /// <summary>
        /// 
        /// </summary>
        public void AddHistory()
        {
            _client.AddMatchingHistory(Oponent.Id);
        }
        #endregion

        #region Skip
        /// <summary>
        /// 
        /// </summary>
        public void Skip()
        {
            if (Oponent == null)
                return;

            _client.SkipMatching(Oponent.Id);
        }
        #endregion

        #region GetMatchingResult
        /// <summary>
        /// 
        /// </summary>
        public void GetMatchingResult()
        {
            _client.GetMatchingResult();
        }
        #endregion


        void client_MatchingResultChanged(object sender, EventArgs<HisoutenSupportTools.AddressUpdater.Lib.AddressService.matchingResult> e)
        {
            if (e.Field.Host.AccountName == AccountName)
                Oponent = e.Field.Client;
            else if (e.Field.Client.AccountName == AccountName)
                Oponent = e.Field.Host;
            else
            {
                Oponent = null;
                return;
            }

            if (Oponent == e.Field.Client)
            {
                // 自分がホスト側
                if (!e.Field.hostPrepared)
                    Phase = MatchingPhase.HostGettingReady;
                else
                    Phase = MatchingPhase.WaitingClientConnecting;
            }
            else if (Oponent == e.Field.Host)
            {
                // 自分がクライアント側
                if (!e.Field.hostPrepared)
                    Phase = MatchingPhase.WaitingHostGettingReady;
                else
                    Phase = MatchingPhase.WaitingFightStart;
            }
        }

        void client_MatchingResultCleared(object sender, EventArgs e)
        {
            Oponent = null;
            Phase = MatchingPhase.Matching;
        }
    }
}
