using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using HisoutenSupportTools.AddressUpdater.Lib.AddressService;
using HisoutenSupportTools.AddressUpdater.Lib.Controller;
using HisoutenSupportTools.AddressUpdater.Lib.Model.Config;

namespace HisoutenSupportTools.AddressUpdater.Lib.ViewModel
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ChatViewModel : ViewModelBase
    {
        /// <summary>アナウンス強調表示用</summary>
        private const string ANNOUNCE_LINE = "__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/";

        /// <summary>クライアント</summary>
        private IClient _client;
        /// <summary>チャットフィルター</summary>
        private ChatFilter _filter = new ChatFilter();
        /// <summary>アナウンス情報キャッシュ</summary>
        private readonly Collection<string> _announceCache = new Collection<string>();
        /// <summary>チャット情報キャッシュ</summary>
        private readonly Collection<chat> _chatCache = new Collection<chat>();

        #region property
        /// <summary>ユーザー設定の取得・設定</summary>
        public UserConfig UserConfig { get; set; }

        /// <summary>
        /// スクロール方向を逆にするかどうか
        /// </summary>
        [DefaultValue(false)]
        public bool IsReverse
        {
            get { return _isReverse; }
            set
            {
                if (_isReverse == value)
                    return;

                _isReverse = value;
                OnPropertyChanged("IsReverse");
                UpdateText();
            }
        }
        private bool _isReverse;

        /// <summary>
        /// テキスト
        /// </summary>
        [DefaultValue("")]
        public string Text
        {
            get { return _text; }
            set
            {
                if (_text == value)
                    return;

                _text = value;
                OnPropertyChanged("Text");
            }
        }
        private string _text = "";
        #endregion


        #region 初期化
        /// <summary>
        /// 
        /// </summary>
        public ChatViewModel()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        public ChatViewModel(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        #endregion


        #region BindClient, UnBindClient
        /// <summary>
        /// 通信クライアントのバインド
        /// </summary>
        /// <param name="client">クライアント</param>
        public void BindClient(IClient client)
        {
            if (_isBinded)
                return;

            if (client == null)
                return;

            _filter = new ChatFilter();
            foreach (var keyword in UserConfig.FilterKeywords)
                _filter.AddKeywordFilter(keyword);

            _client = client;
            _client.AnnounceChanged += new EventHandler<EventArgs<Collection<string>>>(_client_AnnounceChanged);
            _client.ChatReceived += new EventHandler<EventArgs<Collection<chat>>>(_client_ChatReceived);

            _isBinded = true;
        }

        private bool _isBinded = false;

        /// <summary>
        /// 通信クライアントのバインド解除
        /// </summary>
        public void UnBindClient()
        {
            if (!_isBinded)
                return;

            if (_client == null)
                return;

            _client.AnnounceChanged -= _client_AnnounceChanged;
            _client.ChatReceived -= _client_ChatReceived;
            _client = null;

            _isBinded = false;
        }
        #endregion

        #region AddIdFilter
        /// <summary>
        /// IDフィルタの追加
        /// </summary>
        /// <param name="id"></param>
        public void AddIdFilter(string id)
        {
            _filter.AddIdFilter(id);
            UpdateText();
        }
        #endregion

        #region ClearChat
        /// <summary>
        /// チャットのクリア
        /// </summary>
        public void ClearChat()
        {
            _client.ClearChatCache();
            _chatCache.Clear();

            UpdateText();
        }
        #endregion


        #region private
        void UpdateText()
        {
            var filteredChats = _filter.Filter(_chatCache);

            if (!IsReverse)
            {
                var text = new StringBuilder();
                for (var i = 0; i < filteredChats.Count; i++)
                {
                    text.Append(filteredChats[i].ToString());
                    if (i < filteredChats.Count - 1)
                        text.Append(Environment.NewLine);
                }

                if (0 < _announceCache.Count)
                {
                    if (0 < _chatCache.Count)
                        text.Append(Environment.NewLine);
                    text.AppendLine(ANNOUNCE_LINE);
                    for (var i = 0; i < _announceCache.Count; i++)
                    {
                        text.Append(_announceCache[i]);
                        if (i < _announceCache.Count - 1)
                            text.Append(Environment.NewLine);
                    }
                }

                Text = text.ToString();
            }
            else
            {
                var reversedChats = new List<chat>();
                foreach (var chat in filteredChats)
                    reversedChats.Add((chat)chat.Clone());
                reversedChats.Reverse();

                var text = new StringBuilder();
                if (0 < _announceCache.Count)
                {
                    foreach (var announce in _announceCache)
                        text.AppendLine(announce);
                    text.AppendLine(ANNOUNCE_LINE);
                }
                for (var i = 0; i < reversedChats.Count; i++)
                {
                    text.Append(reversedChats[i].ToString());
                    if (i < reversedChats.Count - 1)
                        text.Append(Environment.NewLine);
                }

                Text = text.ToString();
            }
        }

        void _client_AnnounceChanged(object sender, EventArgs<Collection<string>> e)
        {
            _announceCache.Clear();
            foreach (var announce in e.Field)
                _announceCache.Add(announce);

            UpdateText();
        }

        void _client_ChatReceived(object sender, EventArgs<Collection<chat>> e)
        {
            foreach (var chat in e.Field)
                _chatCache.Add(chat);

            UpdateText();
        }
        #endregion
    }
}
