using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using HisoutenSupportTools.AddressUpdater.Lib.Model.Config;
using HisoutenSupportTools.AddressUpdater.Lib.Network;
using HisoutenSupportTools.AddressUpdater.Lib.Tenco;
using HisoutenSupportTools.AddressUpdater.Lib.ViewModel;
using HisoutenSupportTools.AddressUpdater.Lib.ViewModel.AutoMatching;
using HisoutenSupportTools.AddressUpdater.Lib.Watcher;

namespace HisoutenSupportTools.AddressUpdater.Lib.View.AutoMatching
{
    /// <summary>
    /// 
    /// </summary>
    public partial class AutoMatchingView : UserControl
    {
        private Th123TencoClient _tencoClient;
        private DateTime _lastGetTime = DateTime.MinValue;

        /// <summary>
        /// 
        /// </summary>
        public AutoMatchingViewModel ViewModel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public MultiWatcher Watcher
        {
            get { return _watcher; }
        }
        private MultiWatcher _watcher = new MultiWatcher();

        /// <summary>
        /// 
        /// </summary>
        public AutoMatchingView()
        {
            InitializeComponent();

            _watcher.StatusChanged += new EventHandler<EventArgs<bool>>(_watcher_StatusChanged);
        }

        void _watcher_StatusChanged(object sender, EventArgs<bool> e)
        {
            var isFighting = e.Field;

            // マッチング相手と対戦可能状態になってない
            if (ViewModel.Phase == MatchingPhase.Empty ||
                ViewModel.Phase == MatchingPhase.Matching ||
                ViewModel.Phase == MatchingPhase.HostGettingReady ||
                ViewModel.Phase == MatchingPhase.WaitingHostGettingReady)
            {
                // にもかかわらず対戦しちゃってる場合
                if (isFighting)
                {
                    // 登録解除
                    ViewModel.Unregister();
                    registerCheckBox.Checked = false;
                    return;
                }
            }

            switch (ViewModel.Phase)
            {
                case MatchingPhase.Empty:
                    break;
                case MatchingPhase.Matching:
                    break;
                case MatchingPhase.HostGettingReady:
                    break;
                case MatchingPhase.WaitingClientConnecting:
                    if (isFighting)
                    {
                        messageLabel.Text = "対戦中";
                        copyIpButton.Visible = false;
                        skipButton.Visible = false;
                        receiveTimer.Enabled = false;
                    }
                    else
                    {
                        //messageLabel.Text = "対戦終了";
                        ViewModel.AddHistory();
                        new Action(delegate
                        {
                            System.Threading.Thread.Sleep(200);
                            Invoke(new MethodInvoker(delegate
                            {
                                Unregister();
                            }));
                        }).BeginInvoke(null, null);
                    }
                    break;
                case MatchingPhase.WaitingHostGettingReady:
                    break;
                case MatchingPhase.WaitingFightStart:
                    if (isFighting)
                    {
                        messageLabel.Text = "対戦中";
                        copyIpButton.Visible = false;
                        skipButton.Visible = false;
                        receiveTimer.Enabled = false;
                    }
                    else
                    {
                        //messageLabel.Text = "対戦終了";
                        ViewModel.AddHistory();
                        new Action(delegate
                        {
                            System.Threading.Thread.Sleep(200);
                            Invoke(new MethodInvoker(delegate
                            {
                                Unregister();
                            }));
                        }).BeginInvoke(null, null);
                    }
                    break;
                default:
                    break;
            }
        }

        private void AutoMatchingView_Load(object sender, EventArgs e)
        {
            SetBindings();
        }

        private void SetBindings()
        {
            if (ViewModel == null)
                return;

            accountNameInput.DataBindings.Add("Text", ViewModel, "AccountName", false, DataSourceUpdateMode.OnPropertyChanged);
            characterSelect.DataSource = ViewModel.Characters;
            characterSelect.DataBindings.Add("SelectedValue", ViewModel, "Character", false, DataSourceUpdateMode.OnPropertyChanged);
            matchingSpanInput.DataBindings.Add("Value", ViewModel, "MatchingSpan", false, DataSourceUpdateMode.OnPropertyChanged);
            isHostableSelect.DataBindings.Add("Checked", ViewModel, "IsHostable", false, DataSourceUpdateMode.OnPropertyChanged);
            isRoomOnlySelect.DataBindings.Add("Checked", ViewModel, "IsRoomOnly", false, DataSourceUpdateMode.OnPropertyChanged);
            ipInput.DataBindings.Add("Text", ViewModel, "Ip", false, DataSourceUpdateMode.OnPropertyChanged);
            portInput.DataBindings.Add("Value", ViewModel, "Port", false, DataSourceUpdateMode.OnPropertyChanged);
            commentInput.DataBindings.Add("Text", ViewModel, "Comment", false, DataSourceUpdateMode.OnPropertyChanged);
            ViewModel.PropertyChanged += new PropertyChangedEventHandler(ViewModel_PropertyChanged);
        }

        void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Oponent":
                    if (ViewModel.Oponent == null)
                    {
                        Invoke(new MethodInvoker(delegate
                        {
                            messageLabel.Text = "対戦相手を探しています...";
                            copyIpButton.Visible = false;
                            skipButton.Visible = false;
                            oponentAccountNameOutput.Visible = false;
                            oponentRatingLabel.Visible = false;
                            oponentIsRoomOnlyLabel.Visible = false;
                            oponentCommentOutput.Visible = false;
                        }));
                        return;
                    }
                    else
                    {
                        Invoke(new MethodInvoker(delegate
                        {
                            try
                            {
                                var oponet = ViewModel.Oponent;
                                oponentAccountNameOutput.Text = oponet.AccountName;
                                oponentAccountNameOutput.Visible = true;
                                oponentRatingLabel.Text = string.Format(
                                    "{0}:{1}±{2}",
                                    EnumTextAttribute.GetText((Th123Characters)oponet.Rating.Character.Value),
                                    oponet.Rating.Value,
                                    oponet.Rating.Deviation);
                                oponentRatingLabel.Visible = true;
                                oponentIsRoomOnlyLabel.Visible = ViewModel.Oponent.IsRoomOnly;
                                oponentCommentOutput.Text = oponet.Comment;
                                oponentCommentOutput.Visible = true;
                            }
                            catch (Exception ex)
                            {
                                ViewModel.Skip();
                                MessageBox.Show(ParentForm,
                                    "予期しないエラーが発生したので強制スキップしました。" + Environment.NewLine + Environment.NewLine + ex.Message,
                                    Application.ProductName,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                            }
                        }));
                    }
                    break;

                case "Phase":
                    Invoke(new MethodInvoker(delegate
                    {
                        switch (ViewModel.Phase)
                        {
                            case MatchingPhase.Matching:
                                messageLabel.Text = "対戦相手を探しています...";
                                copyIpButton.Visible = false;
                                skipButton.Visible = false;
                                hostCheckTimer.Enabled = false;
                                hostCheckTimer.Stop();
                                break;
                            case MatchingPhase.HostGettingReady:
                                messageLabel.Text = "ホストを立てて下さい。";
                                copyIpButton.Visible = false;
                                skipButton.Visible = true;
                                hostCheckTimer.Enabled = true;
                                hostCheckTimer.Start();
                                break;
                            case MatchingPhase.WaitingClientConnecting:
                                messageLabel.Text = "クライアントの接続を待っています。";
                                copyIpButton.Visible = false;
                                skipButton.Visible = true;
                                hostCheckTimer.Enabled = false;
                                hostCheckTimer.Stop();
                                break;
                            case MatchingPhase.WaitingHostGettingReady:
                                messageLabel.Text = "ホスト側がサーバーを立てるのを待っています。";
                                copyIpButton.Visible = false;
                                skipButton.Visible = true;
                                hostCheckTimer.Enabled = false;
                                hostCheckTimer.Stop();
                                break;
                            case MatchingPhase.WaitingFightStart:
                                messageLabel.Text = "ホストに接続して下さい。";
                                copyIpButton.Visible = true;
                                skipButton.Visible = true;
                                hostCheckTimer.Enabled = false;
                                hostCheckTimer.Stop();
                                break;
                            default:
                                break;
                        }
                    }));
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Close()
        {
            receiveTimer.Enabled = false;
            receiveTimer.Stop();
            hostCheckTimer.Enabled = false;
            hostCheckTimer.Stop();
            if (_watcher != null)
            {
                _watcher.StatusChanged -= _watcher_StatusChanged;
                _watcher.Stop();
            }
            ViewModel.Unregister();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="theme"></param>
        public void ReflectTheme(Theme theme)
        {
            BackColor = theme.ToolBackColor.ToColor();

            copyIpButton.BackColor = SystemColors.Control;
            copyIpButton.UseVisualStyleBackColor = true;
            skipButton.BackColor = SystemColors.Control;
            skipButton.UseVisualStyleBackColor = true;

            accountNameLabel.ForeColor = theme.GeneralTextColor.ToColor();
            accountNameInput.ForeColor = theme.ChatForeColor.ToColor();
            accountNameInput.BackColor = theme.ChatBackColor.ToColor();
            characterLabel.ForeColor = theme.GeneralTextColor.ToColor();
            characterSelect.ForeColor = theme.ChatForeColor.ToColor();
            characterSelect.BackColor = theme.ChatBackColor.ToColor();
            matchingSpanLabel.ForeColor = theme.GeneralTextColor.ToColor();
            matchingSpanInput.ForeColor = theme.ChatForeColor.ToColor();
            matchingSpanInput.BackColor = theme.ChatBackColor.ToColor();
            isHostableSelect.ForeColor = theme.GeneralTextColor.ToColor();
            isRoomOnlySelect.ForeColor = theme.GeneralTextColor.ToColor();
            ip_portLabel.ForeColor = theme.GeneralTextColor.ToColor();
            ipInput.ForeColor = theme.ChatForeColor.ToColor();
            ipInput.BackColor = theme.ChatBackColor.ToColor();
            portInput.ForeColor = theme.ChatForeColor.ToColor();
            portInput.BackColor = theme.ChatBackColor.ToColor();
            commentLabel.ForeColor = theme.GeneralTextColor.ToColor();
            commentInput.ForeColor = theme.ChatForeColor.ToColor();
            commentInput.BackColor = theme.ChatBackColor.ToColor();

            messageLabel.ForeColor = theme.GeneralTextColor.ToColor();
            oponentAccountNameOutput.ForeColor = theme.ChatForeColor.ToColor();
            oponentAccountNameOutput.BackColor = theme.ChatBackColor.ToColor();
            oponentRatingLabel.ForeColor = theme.GeneralTextColor.ToColor();
            oponentCommentOutput.ForeColor = theme.ChatForeColor.ToColor();
            oponentCommentOutput.BackColor = theme.ChatBackColor.ToColor();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Unregister()
        {
            registerCheckBox.Checked = false;
        }

        private void registerCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            informationPanel.Visible = !registerCheckBox.Checked;
            resultPanel.Visible = registerCheckBox.Checked;

            if (registerCheckBox.Checked)
            {
                if (string.IsNullOrEmpty(accountNameInput.Text))
                {
                    registerCheckBox.Checked = false;
                    MessageBox.Show(ParentForm,
                        "アカウント名は空欄にできません。", Application.ProductName,
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (_tencoClient == null)
                    _tencoClient = new Th123TencoClient(accountNameInput.Text);
                else
                {
                    if (_tencoClient.AccountName != accountNameInput.Text)
                    {
                        _lastGetTime = DateTime.MinValue;
                        _tencoClient = new Th123TencoClient(accountNameInput.Text);
                    }
                }

                if (_lastGetTime.AddHours(1) < DateTime.Now)
                {
                    try
                    {
                        _tencoClient.UpdateRating();
                        _lastGetTime = DateTime.Now;
                    }
                    catch (Exception ex) { System.Diagnostics.Debug.WriteLine(ex.Message); }
                }

                ViewModel.Ratings = _tencoClient.GetRatings();

                ViewModel.Register();
                _watcher.Start();
                new Action(delegate
                {
                    System.Threading.Thread.Sleep(200);
                    ViewModel.GetMatchingResult();
                }).BeginInvoke(null, null);
                receiveTimer.Enabled = true;
                receiveTimer.Start();
            }
            else
            {
                ViewModel.Unregister();
                _watcher.Stop();
                receiveTimer.Enabled = false;
                receiveTimer.Stop();
            }
        }

        private void receiveTimer_Tick(object sender, EventArgs e)
        {
            ViewModel.GetMatchingResult();
        }

        private void copyIpButton_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(string.Format("{0}:{1}", ViewModel.Oponent.Ip, ViewModel.Oponent.Port));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private void finishedButton_Click(object sender, EventArgs e)
        {
            ViewModel.AddHistory();
            new Action(delegate
            {
                System.Threading.Thread.Sleep(200);
                Invoke(new MethodInvoker(delegate
                {
                    Unregister();
                }));
            }).BeginInvoke(null, null);
        }

        private void skipButton_Click(object sender, EventArgs e)
        {
            ViewModel.Skip();
            new Action(delegate
            {
                System.Threading.Thread.Sleep(200);
                ViewModel.GetMatchingResult();
            }).BeginInvoke(null, null);
        }

        private void hostCheckTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                // ポート開いてるかチェック
                if (UdpPort.GetIsListening(ViewModel.Port))
                {
                    // 開いてたらホスト側準備OKを送信
                    ViewModel.SetPrepared();
                    new Action(delegate
                    {
                        System.Threading.Thread.Sleep(200);
                        ViewModel.GetMatchingResult();
                    }).BeginInvoke(null, null);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private void isHostableSelect_CheckedChanged(object sender, EventArgs e)
        {
            ip_portLabel.Visible = isHostableSelect.Checked;
            ipInput.Visible = isHostableSelect.Checked;
            portInput.Visible = isHostableSelect.Checked;
        }
    }
}
