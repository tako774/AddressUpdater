using System;
using System.Runtime.InteropServices;

namespace HisoutenSupportTools.AddressUpdater.Lib.Api
{
    /// <summary>
    /// Kernek32
    /// </summary>
    public class Kernel32
    {
        /// <summary>
        /// 指定されたプロセスのメモリ領域からデータを読み取ります。
        /// 読み取られる領域全体がアクセス可能でなければなりません。
        /// さもないと、関数は失敗します。
        /// </summary>
        /// <param name="hProcess">プロセスのハンドル</param>
        /// <param name="lpBaseAddress">読み取り開始アドレス</param>
        /// <param name="lpBuffer">データを格納するバッファ</param>
        /// <param name="dwSize">読み取りたいバイト数</param>
        /// <param name="lpNumberOfBytesRead">読み取ったバイト数</param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out()] byte[] lpBuffer, int dwSize, out int lpNumberOfBytesRead);

        /// <summary>
        /// 指定されたプロセスのメモリ領域からデータを読み取ります。
        /// 読み取られる領域全体がアクセス可能でなければなりません。
        /// さもないと、関数は失敗します。
        /// </summary>
        /// <param name="hProcess">プロセスのハンドル</param>
        /// <param name="lpBaseAddress">読み取り開始アドレス</param>
        /// <param name="buffer">データを格納するバッファ</param>
        /// <returns></returns>
        public static bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, ref byte[] buffer)
        {
            int size;
            return ReadProcessMemory(hProcess, lpBaseAddress, buffer, buffer.Length, out size);
        }

        /// <summary>
        /// 指定されたプロセスのメモリ領域から1byteデータを読み取ります。
        /// 読み取られる領域全体がアクセス可能でなければなりません。
        /// さもないと、関数は失敗します。
        /// </summary>
        /// <param name="hProcess">プロセスのハンドル</param>
        /// <param name="lpBaseAddress">読み取り開始アドレス</param>
        /// <returns>読み取ったデータ</returns>
        /// <exception cref="ReadProcessMemoryFailedException"></exception>
        public static byte ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress)
        {
            var buffer = new byte[1];
            if (ReadProcessMemory(hProcess, lpBaseAddress, ref buffer))
                return buffer[0];
            else
                throw new ReadProcessMemoryFailedException();
        }


        /// <summary>
        /// システムの既定言語識別子を取得します。
        /// </summary>
        /// <returns>システムの既定言語識別子が返ります。</returns>
        [DllImport("kernel32.dll")]
        public static extern ushort GetSystemDefaultLangID();

        /// <summary>
        /// ユーザーの既定言語識別子を取得します。
        /// </summary>
        /// <returns>ユーザーの既定言語識別子を返します。</returns>
        [DllImport("kernel32.dll")]
        public static extern ushort GetUserDefaultLangID();

        /// <summary></summary>
        public const ushort JAPANESE = 0x0411;
    }
}
