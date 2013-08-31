using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows.Forms;
using HisoutenSupportTools.AddressUpdater.Lib.AddressService;
using HisoutenSupportTools.AddressUpdater.Lib.Model;
using HisoutenSupportTools.AddressUpdater.Lib.Model.Config;
using HisoutenSupportTools.AddressUpdater.Lib.Model.Tournament;

namespace HisoutenSupportTools.AddressUpdater.Lib.View.Tournament
{
    /// <summary>
    /// 試合状況表示クラス
    /// </summary>
    public abstract class MatchStatusViewer
    {
        /// <summary>初回表示かどうか</summary>
        protected bool IsFirstShow = true;
        /// <summary>最後に表示したプレイヤー情報</summary>
        private List<player> _lastPlayers;
        /// <summary>最後に表示したコントロール情報</summary>
        protected Collection<ControlInformation> LastControls;
        /// <summary></summary>
        protected ToolTip ToolTip = new ToolTip();

        #region プロパティ
        /// <summary>
        /// 表示するテーブルレイアウトパネルの取得・設定
        /// </summary>
        public TableLayoutPanel TPanel { get; set; }

        /// <summary>
        /// テーマの取得・設定
        /// </summary>
        public Theme Theme { get; set; }

        /// <summary>
        /// Idの取得・設定
        /// </summary>
        public id Id { get; set; }

        /// <summary>
        /// 大会種別の取得・設定
        /// </summary>
        public TournamentTypes TournamentType { get; set; }

        /// <summary>
        /// 役割の取得・設定
        /// </summary>
        public int? Roles
        {
            get { return _roles; }
            set
            {
                if (_roles == value)
                    return;

                _roles = value;
                if (_lastPlayers != null)
                    SetView(new Collection<player>(_lastPlayers));
            }
        }
        private int? _roles;
        #endregion


        /// <summary>
        /// 状況編集時に発生します
        /// </summary>
        public event EventHandler<EventArgs<player>> ResultEdited;


        /// <summary></summary>
        protected static readonly string NONE_TEXT = EnumTextAttribute.GetText(MatchResults.None);
        /// <summary>負</summary>
        protected static readonly string LOSE_TEXT = EnumTextAttribute.GetText(MatchResults.Lose);
        /// <summary>勝</summary>
        protected static readonly string WIN_TEXT = EnumTextAttribute.GetText(MatchResults.Win);
        /// <summary>状態テキストリスト</summary>
        protected static readonly string[] STATUS_TEXTS = new string[]
        {
            NONE_TEXT,
            LOSE_TEXT,
            WIN_TEXT,
        };

        /// <summary>
        /// 表示されている中から自分自身を取得
        /// </summary>
        /// <returns></returns>
        public player GetMyself()
        {
            foreach (var player in _lastPlayers)
            {
                if (player.Id.Equals(Id))
                    return player;
            }
            return null;
        }

        /// <summary>
        /// テーマの反映
        /// </summary>
        public void ReflectTheme()
        {
            if (Theme == null)
                return;

            foreach (var controlInformation in LastControls)
            {
                var control = controlInformation.Control;
                if (control is Label)
                {
                    if (!(control is PlayerLinkLabel))
                    {
                        controlInformation.Control.ForeColor = Theme.GeneralTextColor.ToColor();
                    }
                    else
                    {
                        if (((PlayerLinkLabel)control).Player.fighting)
                            controlInformation.Control.BackColor = Theme.FightingHostBackColor.ToColor();
                    }
                }
                else if (control is ResultEditor)
                {
                    ((ResultEditor)control).ReflectTheme();
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(control.ToString());
                }
            }
        }

        /// <summary>
        /// 表示する
        /// </summary>
        /// <param name="players">プレイヤー情報</param>
        public void SetView(Collection<player> players)
        {
            if (TPanel == null)
                return;

            if (Theme == null)
                return;

            if (Id == null)
                return;

            if (!Roles.HasValue)
                return;

            // ルートパネル初期化
            if (IsFirstShow)
            {
                TPanel.SuspendLayout();
                TPanel.Controls.Clear();
                InitializeRootPanel(players.Count);
                TPanel.ResumeLayout();
                IsFirstShow = false;
            }

            // ソート
            var playerList = new List<player>(players);
            playerList.Sort(new Comparison<player>(ComparePlayerTo));

            // コントロール情報生成
            var controlInformations = CreateControlInformations(playerList);

            // コントロールを設置
            SetControls(controlInformations);

            _lastPlayers = playerList;
        }

        int ComparePlayerTo(player a, player b)
        {
            return a.entryNo.CompareTo(b.entryNo);
        }

        /// <summary>
        /// ルートパネル初期化
        /// </summary>
        /// <param name="playerCount">プレイヤー数</param>
        protected abstract void InitializeRootPanel(int playerCount);

        /// <summary>
        /// コントロール情報生成
        /// </summary>
        /// <param name="players"></param>
        /// <returns></returns>
        protected abstract Collection<ControlInformation> CreateControlInformations(List<player> players);

        /// <summary>
        /// コントロールを設置
        /// </summary>
        /// <param name="controlInformations">コントロール情報</param>
        private void SetControls(Collection<ControlInformation> controlInformations)
        {
            var sameControls = new Collection<ControlInformation>();
            var removeControls = new Collection<ControlInformation>();
            var addControls = new Collection<ControlInformation>();

            if (LastControls == null)
            {
                addControls = controlInformations;
            }
            else
            {
                foreach (var oldControl in LastControls)
                {
                    if (controlInformations.Contains(oldControl))
                        sameControls.Add(oldControl);
                    else
                        removeControls.Add(oldControl);
                }

                foreach (var sameControl in sameControls)
                    controlInformations.Remove(sameControl);

                addControls = controlInformations;
            }

            var measure = new TimeMeasure("status viewer control setting");
            TPanel.SuspendLayout();
            ToolTip.RemoveAll();
            foreach (var rem in removeControls)
                TPanel.Controls.Remove(rem.Control);
            foreach (var add in addControls)
            {
                TPanel.Controls.Add(add.Control, add.X, add.Y);
                if (add.ColumnSpan != 0)
                    TPanel.SetColumnSpan(add.Control, add.ColumnSpan);
                if (add.RowSpan != 0)
                    TPanel.SetRowSpan(add.Control, add.RowSpan);
                if(add.Control is PlayerLinkLabel)
                {
                    var player =((PlayerLinkLabel)add.Control).Player;
                    ToolTip.SetToolTip(
                        add.Control,
                        string.Format("{0}:{1}", player.ip, player.port));
                }
            }
            TPanel.ResumeLayout();
            measure.WriteFinishTime();

            if (LastControls == null)
                LastControls = controlInformations;
            else
            {
                var lastControls = new Collection<ControlInformation>();
                foreach (var same in sameControls)
                    lastControls.Add(same);
                foreach (var add in addControls)
                    lastControls.Add(add);

                LastControls = lastControls;
            }
        }

        /// <summary>
        /// ResultEditedイベント起こす
        /// </summary>
        protected virtual void RaiseResultEdited(player player)
        {
            if (ResultEdited != null)
                ResultEdited(this, new EventArgs<player>(player));
        }

        /// <summary>
        /// ラベル生成
        /// </summary>
        /// <param name="text">ラベルテキスト</param>
        /// <returns></returns>
        protected Label CreateLabel(string text)
        {
            return CreateLabel(text, Color.Transparent, BorderStyle.None);
        }

        /// <summary>
        /// ラベル生成
        /// </summary>
        /// <param name="text">ラベルテキスト</param>
        /// <param name="backColor">背景色</param>
        /// <returns></returns>
        protected Label CreateLabel(string text, Color backColor)
        {
            return CreateLabel(text, backColor, BorderStyle.None);
        }

        /// <summary>
        /// ラベル生成
        /// </summary>
        /// <param name="text">ラベルテキスト</param>
        /// <param name="borderStyle">境界線</param>
        /// <returns></returns>
        protected Label CreateLabel(string text, BorderStyle borderStyle)
        {
            return CreateLabel(text, Color.Transparent, borderStyle);
        }

        /// <summary>
        /// ラベル生成
        /// </summary>
        /// <param name="text">ラベルテキスト</param>
        /// <param name="backColor">背景色</param>
        /// <param name="borderStyle">境界線</param>
        /// <returns></returns>
        protected Label CreateLabel(string text, Color backColor, BorderStyle borderStyle)
        {
            return new Label()
            {
                Text = text,
                ForeColor = Theme.GeneralTextColor.ToColor(),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                Margin = Padding.Empty,
                BorderStyle = borderStyle,
            };
        }

        /// <summary>
        ///プレイヤーリンクラベル生成
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        private PlayerLinkLabel CreatePlayerLinkLabel(player player)
        {
            var playerLinkLabel = new PlayerLinkLabel()
            {
                Player = player,
                Text = player.entryName,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                Margin = Padding.Empty,
            };
            if (IsMyself(player))
                playerLinkLabel.BorderStyle = BorderStyle.Fixed3D;
            playerLinkLabel.LinkClicked += new LinkLabelLinkClickedEventHandler(playerLinkLabel_LinkClicked);
            return playerLinkLabel;
        }
        void playerLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var playerLinkLabel = (PlayerLinkLabel)sender;
            try { Clipboard.SetText(string.Format("{0}:{1}", playerLinkLabel.Player.ip, playerLinkLabel.Player.port)); }
            catch (Exception) { }
        }

        /// <summary>
        /// プレイヤーラベル生成
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        protected Label CreatePlayerLabel(player player)
        {
            Label label;

            if (player.retired)
                label = CreateLabel(player.entryName);
            else
            {
                if ((IsManager || IsPlayer || IsSpectator) && player.waiting)
                    label = CreatePlayerLinkLabel(player);
                else
                    label = CreateLabel(player.entryName);
            }


            if (player.fighting)
                label.BackColor = Theme.FightingHostBackColor.ToColor();

            if (IsMyself(player))
                label.BorderStyle = BorderStyle.Fixed3D;

            return label;
        }

        /// <summary>
        /// 状況編集コントロール生成
        /// </summary>
        /// <param name="player">プレイヤー</param>
        /// <param name="matchIndex">試合のindex</param>
        /// <returns></returns>
        protected ResultEditor CreateResultEditor(player player, int matchIndex)
        {
            var resultEditor = new ResultEditor()
            {
                Player = player,
                MatchIndex = matchIndex,
                TournamentType = TournamentType,
                Theme = Theme,
                Result = (MatchResults)player.MatchResults[matchIndex].Value,
                Dock = DockStyle.Fill,
                Margin = Padding.Empty,
                Tag = matchIndex,
            };
            resultEditor.ResultEdited += new EventHandler<EventArgs<player>>(resultEditor_ResultChanged);
            return resultEditor;
        }

        void resultEditor_ResultChanged(object sender, EventArgs<player> e)
        {
            var editor = (ResultEditor)sender;
            if (editor.TournamentType == TournamentTypes.トーナメント)
            {
                var matchIndex = (int)editor.Tag;
                var result = (MatchResults)editor.Player.MatchResults[matchIndex];
                if (result == MatchResults.None || result == MatchResults.Lose)
                {
                    for (var i = matchIndex + 1; i < editor.Player.MatchResults.Length; i++)
                    {
                        editor.Player.MatchResults[i] = (int)MatchResults.None;
                    }
                }
            }
            RaiseResultEdited(e.Field);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        protected bool IsMyself(player player)
        {
            return player.Id.Equals(Id);
        }

        /// <summary></summary>
        protected bool IsManager
        {
            get { return (Roles & (int)TournamentRoles.Manager) != 0; }
        }

        /// <summary></summary>
        protected bool IsPlayer
        {
            get { return (Roles & (int)TournamentRoles.Player) != 0; }
        }

        /// <summary></summary>
        protected bool IsSpectator
        {
            get { return (Roles & (int)TournamentRoles.Spectator) != 0; }
        }
    }
}
