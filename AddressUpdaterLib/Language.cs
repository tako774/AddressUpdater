using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using HisoutenSupportTools.AddressUpdater.Lib.IO;

namespace HisoutenSupportTools.AddressUpdater.Lib
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class Language
    {
        private const string FILE_NAME = "language.xml";

        /// <summary>
        /// 
        /// </summary>
        public Collection<NamedText> Texts;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="System.Collections.Generic.KeyNotFoundException"></exception>
        public string this[string name]
        {
            get
            {
                for (var i = 0; i < Texts.Count; i++)
                {
                    if (Texts[i].Name.Equals(name, StringComparison.CurrentCulture))
                        return Texts[i].Text;
                }

                throw new KeyNotFoundException();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Language()
        {
            Texts = new Collection<NamedText>();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="System.UnauthorizedAccessException">呼び出し元に、必要なアクセス許可がありません。</exception>
        /// <exception cref="System.InvalidOperationException">シリアル化中にエラーが発生しました。元の例外には、System.Exception.InnerException プロパティを使用してアクセスできます。</exception>
        /// <exception cref="System.IO.IOException">I/O エラーが発生しました。</exception>
        public void Save()
        {
            var file = new XmlConfigFile<Language>(FILE_NAME);
            file.Serialize(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="System.Security.SecurityException">呼び出し元に、必要なアクセス許可がありません。</exception>
        /// <exception cref="System.IO.FileNotFoundException">ファイルが見つかりません。</exception>
        /// <exception cref="System.UnauthorizedAccessException">path が読み取り専用か、またはディレクトリです。</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">割り当てられていないドライブであるなど、指定されたパスが無効です。</exception>
        /// <exception cref="System.InvalidOperationException">逆シリアル化中にエラーが発生しました。元の例外には、System.Exception.InnerException プロパティを使用してアクセスできます。</exception>
        public void Load()
        {
            lock (this)
            {
                var file = new XmlConfigFile<Language>(FILE_NAME);
                var languageFile = file.Deserialize();
                Texts.Clear();
                foreach (var language in languageFile.Texts)
                    Texts.Add(language);
            }
        }


        /// <summary></summary>
        [Serializable]
        public class NamedText
        {
            /// <summary></summary>
            public string Name { get; set; }
            /// <summary></summary>
            public string Text { get; set; }

            /// <summary></summary>
            public NamedText() { }
            /// <summary></summary>
            /// <param name="name"></param>
            /// <param name="text"></param>
            public NamedText(string name, string text)
            {
                Name = name;
                Text = text;
            }
        }
    }
}
