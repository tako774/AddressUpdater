using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using HisoutenSupportTools.AddressUpdater.Lib.Api;
using HisoutenSupportTools.AddressUpdater.Lib.Model.Config;

namespace HisoutenSupportTools.AddressUpdater.Lib.ViewModel
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ExtraViewModel : ViewModelBase
    {
        #region property
        /// <summary>ウィンドウ情報(キャプション, クラス名)</summary>
        public Dictionary<string, string> WindowInformations { get; set; }

        /// <summary>ウィンドウ情報(位置, サイズ)</summary>
        public ExtraWindowInformation WindowInformation { get; set; }

        /// <summary></summary>
        public string Caption
        {
            get { return WindowInformation.Caption; }
            set
            {
                if (WindowInformation.Caption == value)
                    return;

                WindowInformation.Caption = value;
                OnPropertyChanged("Caption");
            }
        }

        /// <summary></summary>
        public bool IsMoveSameTime
        {
            get { return _isMoveSameTime; }
            set
            {
                if (_isMoveSameTime == value)
                    return;

                _isMoveSameTime = value;
                OnPropertyChanged("IsMoveSameTime");
            }
        }
        private bool _isMoveSameTime;

        /// <summary></summary>
        public int X
        {
            get { return WindowInformation.X; }
            set
            {
                if (WindowInformation.X == value)
                    return;

                WindowInformation.X = value;
                OnPropertyChanged("X");
                if (IsMoveSameTime)
                    SetWindowPos();
            }
        }

        /// <summary></summary>
        public int Y
        {
            get { return WindowInformation.Y; }
            set
            {
                if (WindowInformation.Y == value)
                    return;

                WindowInformation.Y = value;
                OnPropertyChanged("Y");
                if (IsMoveSameTime)
                    SetWindowPos();
            }
        }

        /// <summary></summary>
        public int Width
        {
            get { return WindowInformation.Width; }
            set
            {
                if (WindowInformation.Width == value)
                    return;

                WindowInformation.Width = value;
                OnPropertyChanged("Width");
                if (IsMoveSameTime)
                    SetWindowPos();
            }
        }

        /// <summary></summary>
        public int Height
        {
            get { return WindowInformation.Height; }
            set
            {
                if (WindowInformation.Height == value)
                    return;

                WindowInformation.Height = value;
                OnPropertyChanged("Height");
                if (IsMoveSameTime)
                    SetWindowPos();
            }
        }
        #endregion


        /// <summary>
        /// 
        /// </summary>
        public ExtraViewModel()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        public ExtraViewModel(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }


        #region GetWindowRect
        /// <summary>
        /// ウィンドウ位置取得
        /// </summary>
        /// <exception cref="HisoutenSupportTools.AddressUpdater.Lib.Api.WindowNotFoundException"></exception>
        /// <exception cref="HisoutenSupportTools.AddressUpdater.Lib.Api.GetWindowRectFailedException"></exception>
        /// <exception cref="HisoutenSupportTools.AddressUpdater.Lib.Api.AdjustWindowRectFailedException"></exception>
        public void GetWindowRect()
        {
            if (string.IsNullOrEmpty(Caption))
                return;

            Win32Window window = null;
            try
            {
                window = new Win32Window(WindowInformations[Caption], Caption);
            }
            catch (KeyNotFoundException)
            {
                window = new Win32Window(Caption);
            }

            Rect = window.GetWindowRect(WindowStyles.WS_CAPTION, false);
        }
        #endregion

        #region SetWindowPos
        /// <summary>
        /// ウィンドウ位置設定
        /// </summary>
        /// <exception cref="HisoutenSupportTools.AddressUpdater.Lib.Api.WindowNotFoundException"></exception>
        /// <exception cref="HisoutenSupportTools.AddressUpdater.Lib.Api.AdjustWindowRectFailedException"></exception>
        public void SetWindowPos()
        {
            if (string.IsNullOrEmpty(Caption))
                return;

            Win32Window window = null;
            try
            {
                window = new Win32Window(WindowInformations[Caption], Caption);
            }
            catch (KeyNotFoundException)
            {
                window = new Win32Window(Caption);
            }

            window.SetWindowPos(
                HWND.HWND_TOP,
                WindowStyles.WS_CAPTION,
                false,
                SWP.SWP_NOZORDER | SWP.SWP_NOACTIVATE,
                X, Y, Width, Height);
        }
        #endregion


        private Rectangle Rect
        {
            get { return new Rectangle(X, Y, Width, Height); }
            set
            {
                X = value.X;
                Y = value.Y;
                Width = value.Width;
                Height = value.Height;
            }
        }
    }
}
