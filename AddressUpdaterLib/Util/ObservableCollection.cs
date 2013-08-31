using System;
using System.Collections.ObjectModel;

namespace HisoutenSupportTools.AddressUpdater.Lib.Util
{
    /// <summary>
    /// 変更通知イベント付きのコレクション
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ObservableCollection<T> : Collection<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual event EventHandler CollectionChanged;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        protected override void SetItem(int index, T item)
        {
            base.SetItem(index, item);

            if (CollectionChanged != null)
                CollectionChanged(this, EventArgs.Empty);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        protected override void InsertItem(int index, T item)
        {
            base.InsertItem(index, item);

            if (CollectionChanged != null)
                CollectionChanged(this, EventArgs.Empty);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        protected override void RemoveItem(int index)
        {
            base.RemoveItem(index);

            if (CollectionChanged != null)
                CollectionChanged(this, EventArgs.Empty);
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void ClearItems()
        {
            base.ClearItems();

            if (CollectionChanged != null)
                CollectionChanged(this, EventArgs.Empty);
        }
    }
}
