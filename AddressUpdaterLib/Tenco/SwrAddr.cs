using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using HisoutenSupportTools.AddressUpdater.Lib.Api;

namespace HisoutenSupportTools.AddressUpdater.Lib.Tenco
{
    /// <summary>
    /// 
    /// </summary>
    public class SwrAddr
    {
        Process _process;

        private const string CAPTION = "東方緋想天 Ver1.06";
        private const string CLASS_NAME = "th105_106";

        private static readonly IntPtr P_BATTLE_MANAGER = new IntPtr(0x006E6244);
        private const int OFFSET_1P = 0x0c;
        private const int OFFSET_2P = 0x10;
        private const int OFFSET_BATTLE_MODE = 0x88;
        private const int OFFSET_WINCOUNT = 0x4E8;

        private static readonly IntPtr P_NETWORK = new IntPtr(0x006E62FC);
        private const int PROFILE_SIZE = 0x20;
        private const int OFFSET_1P_PLOFILE = 0x04;
        private const int OFFSET_2P_PLOFILE = 0x24;

        private static readonly IntPtr P_SCENE = new IntPtr(0x006ECE7C);
        private static readonly IntPtr P_1P_CHARACTER = new IntPtr(0x006E6FF0);
        private static readonly IntPtr P_2P_CHARACTER = new IntPtr(0x006E7010);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ReadProcessMemoryFailedException"></exception>
        public byte GetScene()
        {
            if (!CanRead())
                throw new ReadProcessMemoryFailedException();

            return Kernel32.ReadProcessMemory(_process.Handle, P_SCENE);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetBattleMode()
        {
            if (!CanRead())
                throw new ReadProcessMemoryFailedException();

            var bbuffer = new byte[4];
            Kernel32.ReadProcessMemory(_process.Handle, P_BATTLE_MANAGER, ref bbuffer);
            var battleManager = BitConverter.ToInt32(bbuffer, 0);

            var battleMode = new byte[4];
            Kernel32.ReadProcessMemory(_process.Handle, new IntPtr(battleManager + OFFSET_BATTLE_MODE), ref battleMode);

            return BitConverter.ToInt32(battleMode, 0);
        }


        #region
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ReadProcessMemoryFailedException"></exception>
        public Th105Characters GetCharacter1P()
        {
            if (!CanRead())
                throw new ReadProcessMemoryFailedException();

            return (Th105Characters)Kernel32.ReadProcessMemory(_process.Handle, P_1P_CHARACTER);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ReadProcessMemoryFailedException"></exception>
        public Th105Characters GetCharacter2P()
        {
            if (!CanRead())
                throw new ReadProcessMemoryFailedException();

            return (Th105Characters)Kernel32.ReadProcessMemory(_process.Handle, P_2P_CHARACTER);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ReadProcessMemoryFailedException"></exception>
        public int GetWinCount1P()
        {
            if (!CanRead())
                throw new ReadProcessMemoryFailedException();

            var bbuffer = new byte[4];
            Kernel32.ReadProcessMemory(_process.Handle, P_BATTLE_MANAGER, ref bbuffer);
            var battleManager = BitConverter.ToInt32(bbuffer, 0);

            var p1 = new byte[4];
            Kernel32.ReadProcessMemory(_process.Handle, new IntPtr(battleManager + OFFSET_1P), ref p1);
            return Kernel32.ReadProcessMemory(_process.Handle, new IntPtr(BitConverter.ToInt32(p1, 0) + OFFSET_WINCOUNT));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ReadProcessMemoryFailedException"></exception>
        public int GetWinCount2P()
        {
            if (!CanRead())
                throw new ReadProcessMemoryFailedException();

            var bbuffer = new byte[4];
            Kernel32.ReadProcessMemory(_process.Handle, P_BATTLE_MANAGER, ref bbuffer);
            var battleManager = BitConverter.ToInt32(bbuffer, 0);

            var p2 = new byte[4];
            Kernel32.ReadProcessMemory(_process.Handle, new IntPtr(battleManager + OFFSET_2P), ref p2);
            return Kernel32.ReadProcessMemory(_process.Handle, new IntPtr(BitConverter.ToInt32(p2, 0) + OFFSET_WINCOUNT));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ReadProcessMemoryFailedException"></exception>
        public string GetProfileName1P()
        {
            if (!CanRead())
                throw new ReadProcessMemoryFailedException();

            var nbuffer = new byte[4];
            Kernel32.ReadProcessMemory(_process.Handle, P_NETWORK, ref nbuffer);
            var network = BitConverter.ToInt32(nbuffer, 0);

            var p1p = new byte[PROFILE_SIZE];
            Kernel32.ReadProcessMemory(_process.Handle, new IntPtr(network + OFFSET_1P_PLOFILE), ref p1p);
            var p1pBytes = new List<byte>();
            foreach (var b in p1p)
                if (b != 0) p1pBytes.Add(b); else break;

            return Encoding.GetEncoding("shift_jis").GetString(p1pBytes.ToArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ReadProcessMemoryFailedException"></exception>
        public string GetProfileName2P()
        {
            if (!CanRead())
                throw new ReadProcessMemoryFailedException();

            var nbuffer = new byte[4];
            Kernel32.ReadProcessMemory(_process.Handle, P_NETWORK, ref nbuffer);
            var network = BitConverter.ToInt32(nbuffer, 0);

            var p2p = new byte[PROFILE_SIZE];
            Kernel32.ReadProcessMemory(_process.Handle, new IntPtr(network + OFFSET_2P_PLOFILE), ref p2p);
            var p2pBytes = new List<byte>();
            foreach (var b in p2p)
                if (b != 0) p2pBytes.Add(b); else break;

            return Encoding.GetEncoding("shift_jis").GetString(p2pBytes.ToArray());
        }
        #endregion


        #region CanRead
        private bool CanRead()
        {
            // プロセス応答確認
            if (_process != null)
            {
                try
                {
                    if (!_process.Responding)
                    {
                        _process = null;
                        return false;
                    }
                }
                catch (PlatformNotSupportedException) { return false; }
                catch (InvalidOperationException) { return false; }
                catch (NotSupportedException) { return false; }
            }

            // 緋想天のプロセス取得
            if (_process == null)
            {
                // ウィンドウハンドル取得
                var handle = User32.FindWindow(CLASS_NAME, CAPTION);
                if (handle == IntPtr.Zero)
                    return false;

                // プロセスID取得
                uint hisoutenProcessId;
                User32.GetWindowThreadProcessId(handle, out hisoutenProcessId);
                if (hisoutenProcessId == 0)
                    return false;

                try
                {
                    _process = Process.GetProcessById((int)hisoutenProcessId);
                }
                catch (ArgumentException) { return false; }
            }

            if (_process == null)
                return false;

            return true;
        }
        #endregion
    }
}
