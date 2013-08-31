using System;
using System.Collections.Generic;
using System.Diagnostics;
using HisoutenSupportTools.AddressUpdater.Lib.Api;

namespace HisoutenSupportTools.AddressUpdater.Lib.Model
{
    /// <summary>
    /// ゲーム情報
    /// </summary>
    public class GameInformation
    {
        /// <summary>キャプション</summary>
        public readonly string Caption;
        /// <summary>クラス名</summary>
        public readonly string ClassName;
        /// <summary>状態アドレス</summary>
        public readonly string Address;
        /// <summary>対戦中を指すあたい</summary>
        public readonly byte[] FightingValues;

        /// <summary>プロセス</summary>
        private Process _process;

        /// <summary>
        /// インスタンスの生成
        /// </summary>
        /// <param name="caption">キャプション</param>
        /// <param name="className">クラス名</param>
        /// <param name="address">状態アドレス</param>
        /// <param name="fightingValues">対戦中を指すあたい</param>
        public GameInformation(string caption, string className, string address, byte[] fightingValues)
        {
            Caption = caption;
            ClassName = className;
            Address = address;
            FightingValues = fightingValues;
        }

        private IntPtr _pointer;
        /// <summary>
        /// 対戦中かどうかを取得
        /// </summary>
        /// <returns>true:対戦中 / false:対戦中じゃない</returns>
        public bool GetIsFighting()
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
                if (string.IsNullOrEmpty(Caption))
                    return false;

                if (string.IsNullOrEmpty(ClassName))
                    return false;

                // ウィンドウハンドル取得
                var handle = User32.FindWindow(ClassName, Caption);
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

            // シーン検出
            try
            {
                if (_pointer == IntPtr.Zero)
                    _pointer = GetPointer();
                if (_pointer == IntPtr.Zero)
                    return false;

                var scene = Kernel32.ReadProcessMemory(_process.Handle, _pointer);
                return IsFightingScene(scene);
            }
            catch (InvalidOperationException) { return false; }
            catch (NotSupportedException) { return false; }
            catch (ReadProcessMemoryFailedException) { return false; }
        }

        /// <summary>
        /// (主にサーバーの)文字列から生成
        /// </summary>
        /// <param name="informationString">ゲーム情報文字列</param>
        /// <returns>ゲーム情報</returns>
        /// <remarks>(caption),(classname),(address),(scene:scene:scene:...)</remarks>
        public static GameInformation FromString(string informationString)
        {
            var texts = informationString.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            if (texts == null || texts.Length != 4)
                return null;


            var valueStrings = texts[3].Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
            var values = new List<byte>();
            foreach (var valueString in valueStrings)
            {
                try { values.Add(byte.Parse(valueString)); }
                catch (Exception) { }
            }

            return new GameInformation(
                texts[0].Trim(),
                texts[1].Trim(),
                texts[2].Trim(),
                values.ToArray());
        }


        /// <summary>
        /// アドレスをポインターに
        /// </summary>
        /// <returns></returns>
        private IntPtr GetPointer()
        {
            if (string.IsNullOrEmpty(Address))
                return IntPtr.Zero;

            try
            {
                if (IntPtr.Size == sizeof(long))
                    return new IntPtr(Convert.ToInt64(Address, 16));
                else
                    return new IntPtr(Convert.ToInt32(Address, 16));
            }
            catch (Exception) { return IntPtr.Zero; }
        }

        /// <summary>
        /// 対戦中のあたいかどうか
        /// </summary>
        /// <param name="sceneId"></param>
        /// <returns>true:対戦中 / false:対戦中じゃない</returns>
        private bool IsFightingScene(byte sceneId)
        {
            if (FightingValues == null)
                return false;

            foreach (var fightingValue in FightingValues)
            {
                if (sceneId == fightingValue)
                    return true;
            }

            return false;
        }
    }
}
