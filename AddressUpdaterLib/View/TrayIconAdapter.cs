using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace HisoutenSupportTools.AddressUpdater.Lib.View
{
    /// <summary>
    /// ウィンドウにトレイアイコン機能追加
    /// </summary>
    public partial class TrayIconAdapter : Component
    {
        private FormWindowState _lastWindowState;
        private Form _target;
        /// <summary></summary>
        public Form Target
        {
            get { return _target; }
            set
            {
                _target = value;
                if (_target != null)
                {
                    _target.Shown += new EventHandler(_target_Shown);
                    _target.SizeChanged += new EventHandler(_target_SizeChanged);
                }
            }
        }

        private bool _hideFormWhenFormMinimized = false;
        /// <summary></summary>
        [DefaultValue(false)]
        public bool HideFormWhenMinimized
        {
            get { return _hideFormWhenFormMinimized; }
            set { _hideFormWhenFormMinimized = value; }
        }

        private bool _hideIconWhenFormActived = false;
        /// <summary></summary>
        [DefaultValue(false)]
        public bool HideIconWhenFormActived
        {
            get { return _hideIconWhenFormActived; }
            set { _hideIconWhenFormActived = value; }
        }

        private NotifyIcon _notifyIcon;
        /// <summary></summary>
        public NotifyIcon NotifyIcon
        {
            get { return _notifyIcon; }
            set
            {
                _notifyIcon = value;
                if (_notifyIcon != null)
                {
                    _notifyIcon.MouseDoubleClick += new MouseEventHandler(_notifyIcon_MouseDoubleClick);
                }
            }
        }

        /// <summary>
        /// インスタンスの生成
        /// </summary>
        public TrayIconAdapter()
        {
            InitializeComponent();
        }

        /// <summary>
        /// インスタンスの生成
        /// </summary>
        /// <param name="container"></param>
        public TrayIconAdapter(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        void _target_Shown(object sender, EventArgs e)
        {
            _lastWindowState = _target.WindowState;
        }

        void _target_SizeChanged(object sender, EventArgs e)
        {
            if (_notifyIcon == null)
                return;

            if (_target.WindowState == FormWindowState.Minimized)
            {
                _notifyIcon.Visible = true;
                _target.Visible = !_hideFormWhenFormMinimized;
            }
            else
            {
                _lastWindowState = _target.WindowState;
                _notifyIcon.Visible = !_hideIconWhenFormActived;
            }
        }

        void _notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            _target.Show();
            _target.WindowState = _lastWindowState;

            bool topMostBackup = _target.TopMost;

            _target.TopMost = true;
            _target.TopMost = topMostBackup;

            _notifyIcon.Visible = !_hideIconWhenFormActived;
        }
    }
}
