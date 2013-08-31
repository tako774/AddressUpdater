using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using HisoutenSupportTools.AddressUpdater.Lib.ViewModel;

namespace HisoutenSupportTools.AddressUpdater.Lib.View
{
    /// <summary>
    /// 
    /// </summary>
    public partial class CommandButton : Component
    {
        /// <summary>
        /// 
        /// </summary>
        public string Command { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public object[] CommandParameters { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ButtonBase Target
        {
            get { return _target; }
            set
            {
                _target = value;
                if (_target != null)
                    _target.Click += new EventHandler(_target_Click);
            }
        }
        private ButtonBase _target;

        /// <summary>
        /// 
        /// </summary>
        public ViewModelBase ViewModel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public CommandButton()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        public CommandButton(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }


        void _target_Click(object sender, EventArgs e)
        {
            if (ViewModel == null)
                return;

            if (string.IsNullOrEmpty(Command))
                return;

            var type = ViewModel.GetType();
            var methodInfo = type.GetMethod(Command);

            if (methodInfo == null)
                return;

            methodInfo.Invoke(this, CommandParameters);
        }
    }
}
