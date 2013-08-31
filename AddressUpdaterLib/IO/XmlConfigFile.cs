using System.IO;
using System.Xml.Serialization;

namespace HisoutenSupportTools.AddressUpdater.Lib.IO
{
    /// <summary>
    /// XmlSerialize可能な設定オブジェクト⇔設定ファイル
    /// </summary>
    /// <typeparam name="TConfig">設定クラス</typeparam>
    public class XmlConfigFile<TConfig>
    {
        /// <summary>設定ファイル</summary>
        private FileInfo _file;

        /// <summary>ファイルが存在するかどうかを示す値を取得します。</summary>
        public bool Exists { get { return _file.Exists; } }

        /// <summary>
        /// インスタンスの生成
        /// </summary>
        /// <param name="fileName">設定ファイル名</param>
        public XmlConfigFile(string fileName)
            : this(new FileInfo(fileName))
        { }

        /// <summary>
        /// インスタンスの生成
        /// </summary>
        /// <param name="file">設定ファイル</param>
        public XmlConfigFile(FileInfo file)
        {
            _file = file;
        }

        /// <summary>
        /// シリアル化
        /// </summary>
        /// <param name="config">設定</param>
        /// <exception cref="System.UnauthorizedAccessException">呼び出し元に、必要なアクセス許可がありません。</exception>
        /// <exception cref="System.InvalidOperationException">シリアル化中にエラーが発生しました。元の例外には、System.Exception.InnerException プロパティを使用してアクセスできます。</exception>
        /// <exception cref="System.IO.IOException">I/O エラーが発生しました。</exception>
        public void Serialize(TConfig config)
        {
            var serializer = new XmlSerializer(typeof(TConfig));
            using (var stream = _file.Create())
            {
                try
                {
                    serializer.Serialize(stream, config);
                    stream.Flush();
                }
                finally { stream.Close(); }
            }
        }

        /// <summary>
        /// 逆シリアル化
        /// </summary>
        /// <returns>設定</returns>
        /// <exception cref="System.Security.SecurityException">呼び出し元に、必要なアクセス許可がありません。</exception>
        /// <exception cref="System.IO.FileNotFoundException">ファイルが見つかりません。</exception>
        /// <exception cref="System.UnauthorizedAccessException">path が読み取り専用か、またはディレクトリです。</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">割り当てられていないドライブであるなど、指定されたパスが無効です。</exception>
        /// <exception cref="System.InvalidOperationException">逆シリアル化中にエラーが発生しました。元の例外には、System.Exception.InnerException プロパティを使用してアクセスできます。</exception>
        public TConfig Deserialize()
        {
            var deserializer = new XmlSerializer(typeof(TConfig));
            using (var reader = _file.OpenText())
            {
                try
                {
                    var config = deserializer.Deserialize(reader);

                    if (config is TConfig)
                        return (TConfig)config;
                }
                finally { reader.Close(); }
            }

            return default(TConfig);
        }

        /// <summary>
        /// 指定したファイルを新しい場所に移動します。オプションで新しいファイル名を指定することもできます。
        /// </summary>
        /// <param name="destFileName">ファイルの移動先のパス。異なるファイル名を指定できます。</param>
        /// <exception cref="System.IO.IOException">I/O エラーが発生しました。移動先のファイルが既に存在するか、移動先のデバイスの準備ができていない、などの原因が考えられます。</exception>
        /// <exception cref="System.Security.SecurityException">呼び出し元に、必要なアクセス許可がありません。</exception>
        /// <exception cref="System.UnauthorizedAccessException">destFileName が読み取り専用か、またはディレクトリです。</exception>
        public void MoveTo(string destFileName)
        {
            _file.MoveTo(destFileName);
        }
    }
}
