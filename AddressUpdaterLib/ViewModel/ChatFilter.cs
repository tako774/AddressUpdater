using System.Collections.ObjectModel;
using HisoutenSupportTools.AddressUpdater.Lib.AddressService;

namespace HisoutenSupportTools.AddressUpdater.Lib.ViewModel
{
    /// <summary>
    /// チャットフィルター
    /// </summary>
    internal class ChatFilter
    {
        private Collection<string> _idList = new Collection<string>();
        private Collection<string> _keywords = new Collection<string>();

        /// <summary>
        /// IDフィルタを追加
        /// </summary>
        /// <param name="id">ID</param>
        public void AddIdFilter(string id)
        {
            if (!_idList.Contains(id))
                _idList.Add(id);
        }

        /// <summary>
        /// キーワードフィルタを追加
        /// </summary>
        /// <param name="keyword">キーワード</param>
        public void AddKeywordFilter(string keyword)
        {
            if (!_keywords.Contains(keyword))
                _keywords.Add(keyword);

        }

        /// <summary>
        /// フィルタ
        /// </summary>
        /// <param name="chats"></param>
        /// <returns></returns>
        public Collection<chat> Filter(Collection<chat> chats)
        {
            var filteredChats = new Collection<chat>();

            foreach (var chat in chats)
            {
                if (_idList != null)
                {
                    var hit = false;
                    // idでフィルタ
                    foreach (var id in _idList)
                    {
                        if (chat.Id.value == id)
                        {
                            hit = true;
                            break;
                        }
                    }

                    if (hit)
                        continue;
                }

                if (_keywords != null)
                {
                    // 内容をフィルタ
                    var clone = (chat)chat.Clone();
                    foreach (var keyword in _keywords)
                    {
                        if (clone.Contents.Contains(keyword))
                        {
                            var filterText = string.Empty;
                            for (var i = 0; i < keyword.Length; i++)
                                filterText += "*";

                            clone.Contents = clone.Contents.Replace(keyword, filterText);
                        }
                    }

                    filteredChats.Add(clone);
                }
                else
                {
                    filteredChats.Add((chat)chat.Clone());
                }
            }

            return filteredChats;
        }
    }
}
