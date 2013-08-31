using System.ComponentModel;

namespace HisoutenSupportTools.AddressUpdater.Lib.ViewModel
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ViewModelBase : Component, INotifyPropertyChanged
    {
        /// <summary>
        /// 
        /// </summary>
        public ViewModelBase()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        public ViewModelBase(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }


        #region INotifyPropertyChanged メンバ
        /// <summary>
        /// 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
