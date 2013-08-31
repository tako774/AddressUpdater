using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using HisoutenSupportTools.AddressUpdater.Lib.Api;
using HisoutenSupportTools.AddressUpdater.Lib.ViewModel;
using System.Collections.ObjectModel;

namespace HisoutenSupportTools.AddressUpdater.Lib.View
{
    /// <summary>
    /// チャット表示コントロール
    /// </summary>
    public partial class ChatView : UserControl
    {
        #region property
        /// <summary>
        /// ChatViewModelの取得・設定
        /// </summary>
        public ChatViewModel ViewModel { get; set; }

        /// <summary>テキスト色</summary>
        public Color TextForeColor
        {
            get { return richTextBox.ForeColor; }
            set
            {
                if (richTextBox.ForeColor == value)
                    return;
                richTextBox.ForeColor = value;
            }
        }
        /// <summary>テキスト背景色</summary>
        public Color TextBackColor
        {
            get { return richTextBox.BackColor; }
            set
            {
                if (richTextBox.BackColor == value)
                    return;

                richTextBox.BackColor = value;
            }
        }

        /// <summary>ハイライト表示するキーワード一覧</summary>
        public Collection<string> HighlightKeywords { get; set; }

        /// <summary></summary>
        public override ContextMenuStrip ContextMenuStrip
        {
            get { return richTextBox.ContextMenuStrip; }
            set { richTextBox.ContextMenuStrip = value; }
        }

        /// <summary>選択されているテキストの取得</summary>
        public string SelectedText
        {
            get { return richTextBox.SelectedText; }
        }
        #endregion


        #region 初期化
        /// <summary>
        /// インスタンスの生成
        /// </summary>
        public ChatView()
        {
            InitializeComponent();
        }
        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChatView_Load(object sender, EventArgs e)
        {
            SetBindings();

            if (ViewModel == null)
                return;

            ViewModel.PropertyChanged += new PropertyChangedEventHandler(ViewModel_PropertyChanged);
        }

        void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                default:
                    break;
            }
        }

        private void SetBindings()
        {
            if (ViewModel == null)
                return;

            richTextBox.DataBindings.Add("Text", ViewModel, "Text", false, DataSourceUpdateMode.OnPropertyChanged);
        }

        /// <summary>
        /// 選択中のテキストをクリップボードにコピーします。
        /// </summary>
        public void Copy()
        {
            richTextBox.Copy();
        }

        /// <summary>
        /// テキストをセット
        /// </summary>
        /// <param name="text">テキスト</param>
        public void SetText(string text)
        {
            richTextBox.Text = text;
        }

        /// <summary>
        /// 最新位置へスクロール
        /// </summary>
        private void ScrollToLatest()
        {
            if (!ViewModel.IsReverse)
            {
                try
                {
                    richTextBox.SelectionStart = richTextBox.Text.Length;
                    richTextBox.ScrollToCaret();
                    // VistaだとRichTextBoxがあと一歩言うこと聞かないのでWindowsメッセージで1行スクロール
                    User32.SendMessage(richTextBox.Handle, User32.EM_LINESCROLL, 0, 1);

                    // 横スクロール位置を一番左に

                    var maxCount = short.MaxValue;
                    var count = 0;
                    while (User32.GetScrollPos(richTextBox.Handle, User32.SB_HORZ) != 0 || maxCount < count)
                    {
                        //User32.SendMessage(richTextBox.Handle, User32.WM_HSCROLL, User32.SB_LINELEFT, 0);
                        User32.SendMessage(richTextBox.Handle, User32.WM_HSCROLL, User32.SB_PAGELEFT, 0);
                        count++;
                    }
                    User32.SetScrollPos(richTextBox.Handle, User32.SB_HORZ, 0, true);
                }
                catch (Exception) { }
            }
            else
            {
                richTextBox.SelectionStart = 0;
                richTextBox.ScrollToCaret();
            }
        }

        /// <summary>
        /// チャット欄URLクリック時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void richTextBox_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            try { Process.Start(e.LinkText); }
            catch (ObjectDisposedException ex) { System.Diagnostics.Debug.WriteLine(ex); }
            catch (InvalidOperationException ex) { System.Diagnostics.Debug.WriteLine(ex); }
            catch (Win32Exception ex) { System.Diagnostics.Debug.WriteLine(ex); }
        }

        /// <summary>
        /// フォント変更時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChatView_FontChanged(object sender, EventArgs e)
        {
            richTextBox.Font = Font;
        }

        private void richTextBox_TextChanged(object sender, EventArgs e)
        {
            richTextBox.SuspendLayout();
            try
            {
                if (HighlightKeywords != null)
                {
                    try
                    {
                        foreach (var keyword in HighlightKeywords)
                        {
                            int startIndex = 0;
                            while (true)
                            {
                                var index = richTextBox.Text.IndexOf(keyword, startIndex);
                                if (index < 0)
                                    break;

                                startIndex = index + 1;
                                richTextBox.SelectionStart = index;
                                richTextBox.SelectionLength = keyword.Length;
                                richTextBox.SelectionColor = System.Drawing.Color.Red;
                            }
                        }
                    }
                    catch (Exception ex) { System.Diagnostics.Debug.WriteLine(ex.Message); }
                }
                ScrollToLatest();
            }
            finally { richTextBox.ResumeLayout(); }
        }
    }
}
