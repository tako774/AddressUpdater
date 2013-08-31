using System;

namespace HisoutenSupportTools.AddressUpdater.Lib.Watcher
{
    /// <summary>
    /// 対戦状態監視インターフェース
    /// </summary>
    public interface IWatcher
    {
        /// <summary>対戦状態が変化した時に発生します。</summary>
        event EventHandler<EventArgs<bool>> StatusChanged;

        /// <summary>
        /// 通信対戦中かどうか
        /// </summary>
        bool IsFighting { get; }
        /// <summary>
        /// 開始
        /// </summary>
        void Start();
        /// <summary>
        /// 停止
        /// </summary>
        void Stop();
    }
}
