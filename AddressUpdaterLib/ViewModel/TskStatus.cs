using System;

namespace HisoutenSupportTools.AddressUpdater.Lib.ViewModel
{
    /// <summary>
    /// 対戦記録ツールの状態
    /// </summary>
    public class TskStatus
    {
        private bool _status = false;
        /// <summary>
        /// 状態の取得・設定
        /// </summary>
        public bool Status
        {
            get { return _status; }
            set
            {
                //if (_status == value)
                //    return;

                _status = value;
                if (StatusChanged != null)
                    StatusChanged(this, new EventArgs<bool>(value));
            }
        }

        /// <summary>状態変更時に発生します</summary>
        public event EventHandler<EventArgs<bool>> StatusChanged;

        /// <summary>
        /// インスタンスの生成
        /// </summary>
        public TskStatus() { }
    }
}
