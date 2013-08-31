using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text;
using HisoutenSupportTools.AddressUpdater.Lib.AddressService;

namespace HisoutenSupportTools.AddressUpdater.Lib.IO
{
    /// <summary>
    /// addtess.txt更新
    /// </summary>
    public partial class AddressTxtUpdater : Component
    {
        /// <summary>ファイル名</summary>
        private const string FILENAME = "address.txt";
        /// <summary>保存リトライ回数</summary>
        private const int RETRY_COUNT = 3;

        /// <summary>ファイル</summary>
        private FileInfo _file = new FileInfo(FILENAME);

        /// <summary>
        /// インスタンスの生成
        /// </summary>
        public AddressTxtUpdater()
        {
            InitializeComponent();
        }

        /// <summary>
        /// インスタンスの生成
        /// </summary>
        /// <param name="container"></param>
        public AddressTxtUpdater(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="hosts">ホスト一覧</param>
        /// <exception cref="System.UnauthorizedAccessException">呼び出し元に、必要なアクセス許可がありません。</exception>
        /// <exception cref="System.IO.IOException">I/O エラーが発生しました。</exception>
        public void Update(Collection<host> hosts)
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
                            writer.WriteLine(DateTime.Now.ToString(";yyyy/MM/dd HH:mm:ss") + " の一覧");
                            foreach (var host in hosts)
                            {
                                string lineText = string.Format(
                                    "{0}:{1};{2};{3}",
                                    host.Ip,
                                    host.Port,
                                    host.Rank,
                                    host.IsFighting ? "対戦中" : "待機中");
                                writer.WriteLine(lineText);
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
                    if (i == RETRY_COUNT - 1)
                        throw ex;
                }
            }
        }
    }
}
