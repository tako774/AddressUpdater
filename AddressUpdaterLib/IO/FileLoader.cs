using System.IO;

namespace HisoutenSupportTools.AddressUpdater.Lib.IO
{
    /// <summary>
    /// ファイル読み込み
    /// </summary>
    public class FileLoader
    {
        /// <summary>
        /// メモリストリームに読み込み
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static MemoryStream LoadFile(FileInfo file)
        {
            var memStream = new MemoryStream();
            var buffer = new byte[byte.MaxValue];

            using (var fileStream = file.OpenRead())
            {
                while (true)
                {
                    var readsize = fileStream.Read(buffer, 0, buffer.Length);
                    if (readsize == 0)
                        break;

                    memStream.Write(buffer, 0, readsize);
                }
            }

            return memStream;
        }
    }
}
