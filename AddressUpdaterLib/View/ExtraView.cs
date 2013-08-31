using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using HisoutenSupportTools.AddressUpdater.Lib.Api;
using HisoutenSupportTools.AddressUpdater.Lib.Model.Config;
using HisoutenSupportTools.AddressUpdater.Lib.ViewModel;
using System.Drawing;

namespace HisoutenSupportTools.AddressUpdater.Lib.View
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ExtraView : UserControl
    {
        /// <summary>VersionViewModelの取得・設定</summary>
        public ExtraViewModel ViewModel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ExtraView()
        {
            InitializeComponent();
        }

        private void ExtraView_Load(object sender, EventArgs e)
        {
            try
            {
                SetBindings();
            }
            catch (NullReferenceException) { }
        }

        private void SetBindings()
        {
            if (ViewModel == null)
                return;

            ViewModel.PropertyChanged += new PropertyChangedEventHandler(ViewModel_PropertyChanged);
            windowCaptionInput.DataBindings.Add("Text", ViewModel, "Caption", false, DataSourceUpdateMode.OnPropertyChanged);
            xInput.DataBindings.Add("Value", ViewModel, "X", false, DataSourceUpdateMode.OnPropertyChanged);
            yInput.DataBindings.Add("Value", ViewModel, "Y", false, DataSourceUpdateMode.OnPropertyChanged);
            widthInput.DataBindings.Add("Value", ViewModel, "Width", false, DataSourceUpdateMode.OnPropertyChanged);
            heightInput.DataBindings.Add("Value", ViewModel, "Height", false, DataSourceUpdateMode.OnPropertyChanged);
            sameTimeChangeCheckBox.DataBindings.Add("Checked", ViewModel, "IsMoveSameTime", false, DataSourceUpdateMode.OnPropertyChanged);
        }

        void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                default:
                    break;
            }
        }

        /// <summary>
        /// 表示言語の反映
        /// </summary>
        /// <param name="language"></param>
        public void ReflectLanguage(Language language)
        {
            try { sameTimeChangeCheckBox.Text = language["VersionTab_ChangeSameTime"]; }
            catch (KeyNotFoundException) { }
            try { xLabel.Text = language["VersionTab_X"]; }
            catch (KeyNotFoundException) { }
            try { yLabel.Text = language["VersionTab_Y"]; }
            catch (KeyNotFoundException) { }
            try { widthLabel.Text = language["VersionTab_Width"]; }
            catch (KeyNotFoundException) { }
            try { heightLabel.Text = language["VersionTab_Height"]; }
            catch (KeyNotFoundException) { }
            try { getWindowRectButton.Text = language["VersionTab_GetPosition"]; }
            catch (KeyNotFoundException) { }
            try { setWindowPosButton.Text = language["VersionTab_SetPosition"]; }
            catch (KeyNotFoundException) { }
        }

        /// <summary>
        /// テーマの反映
        /// </summary>
        /// <param name="theme"></param>
        public void ReflectTheme(Theme theme)
        {
            BackColor = theme.ToolBackColor.ToColor();
            getWindowRectButton.BackColor = SystemColors.Control;
            getWindowRectButton.UseVisualStyleBackColor = true;
            setWindowPosButton.BackColor = SystemColors.Control;
            setWindowPosButton.UseVisualStyleBackColor = true;

            sameTimeChangeCheckBox.ForeColor = theme.GeneralTextColor.ToColor();
            xLabel.ForeColor = theme.GeneralTextColor.ToColor();
            yLabel.ForeColor = theme.GeneralTextColor.ToColor();
            widthLabel.ForeColor = theme.GeneralTextColor.ToColor();
            heightLabel.ForeColor = theme.GeneralTextColor.ToColor();
            getWindowRectButton.ForeColor = SystemColors.ControlText;
            setWindowPosButton.ForeColor = SystemColors.ControlText;

            windowCaptionInput.ForeColor = theme.ChatForeColor.ToColor();
            windowCaptionInput.BackColor = theme.ChatBackColor.ToColor();
            xInput.ForeColor = theme.ChatForeColor.ToColor();
            xInput.BackColor = theme.ChatBackColor.ToColor();
            yInput.ForeColor = theme.ChatForeColor.ToColor();
            yInput.BackColor = theme.ChatBackColor.ToColor();
            widthInput.ForeColor = theme.ChatForeColor.ToColor();
            widthInput.BackColor = theme.ChatBackColor.ToColor();
            heightInput.ForeColor = theme.ChatForeColor.ToColor();
            heightInput.BackColor = theme.ChatBackColor.ToColor();
        }

        /// <summary>
        /// 現在位置取得ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void getWindowRectButton_Click(object sender, EventArgs e)
        {
            try { ViewModel.GetWindowRect(); }
            catch (WindowNotFoundException) { }
            catch (GetWindowRectFailedException) { }
            catch (AdjustWindowRectFailedException) { }
        }

        /// <summary>
        /// 移動ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void setWindowPosButton_Click(object sender, EventArgs e)
        {
            SetWindowPos();
        }

        /// <summary>
        /// ウィンドウ位置・サイズを設定
        /// </summary>
        private void SetWindowPos()
        {
            try { ViewModel.SetWindowPos(); }
            catch (WindowNotFoundException) { }
            catch (AdjustWindowRectFailedException) { }
        }
    }
}
