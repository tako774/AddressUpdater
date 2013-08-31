using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text;
using HisoutenSupportTools.AddressUpdater.Lib.Model.Config;

namespace HisoutenSupportTools.AddressUpdater.Lib.IO
{
    /// <summary>
    /// サーバー一覧読み込み
    /// </summary>
    public partial class ServerListLoader : Component
    {
        /// <summary>一覧ファイル名</summary>
        private const string FILENAME = "auservers.txt";
        /// <summary>保存リトライ回数</summary>
        private const int RETRY_COUNT = 3;

        /// <summary>一覧ファイル</summary>
        private FileInfo _file = new FileInfo(FILENAME);

        /// <summary>ファイルが存在するかどうか</summary>
        public static bool Exists { get { return File.Exists(FILENAME); } }

        /// <summary>
        /// インスタンスの生成
        /// </summary>
        public ServerListLoader()
        {
            InitializeComponent();
        }

        /// <summary>
        /// インスタンスの生成
        /// </summary>
        /// <param name="container"></param>
        public ServerListLoader(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="serverInformations">サーバー情報リスト</param>
        /// <exception cref="System.UnauthorizedAccessException">呼び出し元に、必要なアクセス許可がありません。</exception>
        /// <exception cref="System.IO.IOException">I/O エラーが発生しました。</exception>
        public void Save(Collection<ServerInformation> serverInformations)
        {
            for (var i = 0; i < RETRY_COUNT; i++)
            {
                try
                {
                    using (var stream = _file.Create())
                    using (var writer = new StreamWriter(stream, Encoding.GetEncoding("shift_jis")))
                    {
                        try
                        {
                            foreach (var serverInformation in serverInformations)
                            {
                                writer.WriteLine(string.Format(
                                    "{0}{1}|{2}",
                                    serverInformation.Visible ? string.Empty : ";",
                                    serverInformation.Name,
                                    serverInformation.Uri));
                            }
                            writer.Flush();
                        }
                        finally { writer.Close(); }
                    }
                    break;
                }
                catch (IOException ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                    if (i < RETRY_COUNT - 1)
                        continue;
                    else
                        throw ex;
                }
            }
        }

        /// <summary>
        /// 読み込み
        /// </summary>
        /// <returns>サーバー情報リスト</returns>
        /// <exception cref="System.IO.FileNotFoundException">ファイルが見つかりません。</exception>
        /// <exception cref="System.IO.IOException">I/O エラーが発生しました。</exception>
        /// <exception cref="System.OutOfMemoryException">返される文字列用のバッファを割り当てるためにはメモリが不足しています。</exception>
        public Collection<ServerInformation> Load()
        {
            if (!_file.Exists)
                throw new FileNotFoundException("ファイルが見つかりません。", FILENAME);

            var serverInformations = new Collection<ServerInformation>();
            using (var stream = _file.OpenRead())
            using (var reader = new StreamReader(stream, Encoding.GetEncoding("shift_jis")))
            {
                try
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        if (line == null)
                            continue;

                        var servernameAndUri = line.Split('|');
                        if (servernameAndUri.Length != 2)
                            continue;

                        var hidden = (0 < line.Length && line[0] == ';');
                        var serverName = servernameAndUri[0];
                        if (hidden)
                            serverName = serverName.Substring(1);
                        var uri = servernameAndUri[1];

                        serverInformations.Add(new ServerInformation(serverName, uri, !hidden));
                    }
                }
                finally { reader.Close(); }
            }

            return serverInformations;
        }
    }
}
