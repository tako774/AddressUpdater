using System;
using System.Collections.Generic;
using System.Diagnostics;
using HisoutenSupportTools.AddressUpdater.Lib.Api;
using HisoutenSupportTools.AddressUpdater.Lib.Util;

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

            // ゲームプロセス取得
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

                bool _ret;
                int _readSize;

                Byte[] _ptrBytes = new Byte[4];
                Byte[] _intBytes = new Byte[4];
                IntPtr _vmPointer;
                IntPtr _rootTablePointer;
                IntPtr _tableItemsPointer;
                int _tableItemNum;

                int _itemKeyType;
                IntPtr _itemKeyPtr;
                int _itemKeyLen;
                String _itemKey;
                
                int _itemValType;
                int _itemValVal;
                
                String[] targetKeys = new String[] {"game", "network_inst", "network_is_watch"};
                Dictionary<String, Dictionary<String, int>> _sceneParams = new Dictionary<string, Dictionary<string, int>>(targetKeys.Length);
                
                // メインモジュールのベースアドレス取得
                IntPtr _mainModulePtr = _process.MainModule.BaseAddress;

                // SQVM のインスタンスアドレス取得
                _ret = Kernel32.ReadProcessMemory(_process.Handle, _mainModulePtr + _pointer.ToInt32(), _ptrBytes, _ptrBytes.Length, out _readSize);
                if (!_ret) { return false; }
                _vmPointer = PtrUtil.BytesToIntPtr(_ptrBytes);
                
                // SQRootTable のインスタンスアドレス取得
                _ret = Kernel32.ReadProcessMemory(_process.Handle, _vmPointer + 0x34, _ptrBytes, _ptrBytes.Length, out _readSize);
                if (!_ret) { return false; }
                _rootTablePointer = PtrUtil.BytesToIntPtr(_ptrBytes);
                
                // SQRootTable のアイテムのアドレス取得
                _ret = Kernel32.ReadProcessMemory(_process.Handle, _rootTablePointer + 0x20, _ptrBytes, _ptrBytes.Length, out _readSize);
                if (!_ret) { return false; }
                _tableItemsPointer = PtrUtil.BytesToIntPtr(_ptrBytes);

                // SQRootTable のアイテム数取得
                _ret = Kernel32.ReadProcessMemory(_process.Handle, _rootTablePointer + 0x24, _intBytes, _intBytes.Length, out _readSize);
                if (!_ret) { return false; }
                _tableItemNum = BitConverter.ToInt32(_intBytes, 0);

                // SQRootTable から、ゲームシーン状況に関する情報を取得
                for (int i = 0; i < _tableItemNum; i++)
                {
                    // キーの型を取得
                    _ret = Kernel32.ReadProcessMemory(_process.Handle, _tableItemsPointer + 0x14 * i + 0x08, _intBytes, _intBytes.Length, out _readSize);
                    if (!_ret) { return false; }
                    _itemKeyType = BitConverter.ToInt32(_intBytes, 0) & 0x000FFFFF;

                    // キーの文字列型のアイテムのみ処理
                    if (_itemKeyType == 0x10)
                    {
                        // キー文字列のアドレス取得
                        _ret = Kernel32.ReadProcessMemory(_process.Handle, _tableItemsPointer + 0x14 * i + 0x0C, _ptrBytes, _ptrBytes.Length, out _readSize);
                        if (!_ret) { return false; }
                        _itemKeyPtr = PtrUtil.BytesToIntPtr(_ptrBytes);

                        // キー文字列の長さ取得
                        _ret = Kernel32.ReadProcessMemory(_process.Handle, _itemKeyPtr + 0x14, _intBytes, _intBytes.Length, out _readSize);
                        if (!_ret) { return false; }
                        _itemKeyLen = BitConverter.ToInt32(_intBytes, 0);

                        // キー文字列を取得
                        Byte[] _keyBytes = new Byte[_itemKeyLen];
                        _ret = Kernel32.ReadProcessMemory(_process.Handle, _itemKeyPtr + 0x1C, _keyBytes, _keyBytes.Length, out _readSize);
                        if (!_ret) { return false; }
                        _itemKey = System.Text.Encoding.ASCII.GetString(_keyBytes);

                        // キー文字列が、ゲームシーン情報に関わるものであったら、値を取得
                        foreach (String targetKey in targetKeys)
                        {
                            if (_itemKey == targetKey)
                            {
                                // 値の型を取得
                                _ret = Kernel32.ReadProcessMemory(_process.Handle, _tableItemsPointer + 0x14 * i + 0x00, _intBytes, _intBytes.Length, out _readSize);
                                if (!_ret) { return false; }
                                _itemValType = BitConverter.ToInt32(_intBytes, 0);

                                // 値を取得
                                _ret = Kernel32.ReadProcessMemory(_process.Handle, _tableItemsPointer + 0x14 * i + 0x04, _intBytes, _intBytes.Length, out _readSize);
                                if (!_ret) { return false; }
                                _itemValVal = BitConverter.ToInt32(_intBytes, 0);

                                // 結果をシーンパラメータ情報へと格納
                                Dictionary<String,int> item = new Dictionary<string,int>();
                                item.Add("type", _itemValType);
                                item.Add("val", _itemValVal);
                                _sceneParams.Add(targetKey, item);
                            }
                        }

                    }
                }
                
                return IsFightingScene(_sceneParams);
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
        /// <param name="sceneParams"></param>
        /// <returns>true:対戦中 / false:対戦中じゃない</returns>
        private bool IsFightingScene(Dictionary<String, Dictionary<String, int>> sceneParams)
        {
            if (sceneParams == null)
                return false;

             try
            {
                if (
                    ((sceneParams["network_inst"]["type"] & 0x8000) != 0) &&
                    ((sceneParams["network_is_watch"]["type"] & 0x8) != 0) &&
                    (sceneParams["network_is_watch"]["val"] == 0)
                   )
                {
                    return true;
                }
            }
            catch (KeyNotFoundException)
            {
                return false;
            }


            return false;
        }
    }
}
