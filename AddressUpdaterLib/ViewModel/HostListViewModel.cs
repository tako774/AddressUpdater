using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using HisoutenSupportTools.AddressUpdater.Lib.AddressService;
using HisoutenSupportTools.AddressUpdater.Lib.Controller;
using HisoutenSupportTools.AddressUpdater.Lib.Model;
using HisoutenSupportTools.AddressUpdater.Lib.Model.Tournament;

namespace HisoutenSupportTools.AddressUpdater.Lib.ViewModel
{
    /// <summary>
    /// 
    /// </summary>
    public partial class HostListViewModel : ViewModelBase
    {
        /// <summary>通信クライアント</summary>
        private IClient _client;
        /// <summary>ホスト一覧</summary>
        private readonly AddressUpdaterDataSet.HostsDataTable _hosts = new AddressUpdaterDataSet.HostsDataTable();

        /// <summary>ホスト一覧ビュー</summary>
        public readonly DataView HostsDataView;

        #region property
        /// <summary>
        /// ランクフィルターの取得・設定
        /// </summary>
        public string RankFilter
        {
            get { return _rankFilter; }
            set
            {
                if (_rankFilter == value)
                    return;

                _rankFilter = value;
                OnPropertyChanged("RankFilter");
                UpdateDataView();
            }
        }
        private string _rankFilter;
        #endregion


        #region 初期化
        /// <summary>
        /// 
        /// </summary>
        public HostListViewModel()
        {
            InitializeComponent();


            HostsDataView = new DataView(_hosts);
            HostsDataView.Sort = "IsFighting, LastUpdate DESC";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        public HostListViewModel(IContainer container)
        {
            container.Add(this);

            InitializeComponent();


            HostsDataView = new DataView(_hosts);
            HostsDataView.Sort = "IsFighting, LastUpdate DESC";
        }
        #endregion


        #region BindClient, UnBindClient
        /// <summary>
        /// 通信クライアントのバインド
        /// </summary>
        /// <param name="client"></param>
        public void BindClient(IClient client)
        {
            if (_isBinded || client == null)
                return;

            _client = client;

            _client.NewHostReceived += new EventHandler<EventArgs<Collection<host>>>(_client_NewHostReceived);
            _client.HostStatusChanged += new EventHandler<EventArgs<Collection<host>>>(_client_HostStatusChanged);
            _client.HostDeleted += new EventHandler<EventArgs<Collection<host>>>(_client_HostDeleted);

            _client.NewTournamentReceived += new EventHandler<EventArgs<Collection<tournamentInformation>>>(_client_NewTournamentReceived);
            _client.TournamentStatusChanged += new EventHandler<EventArgs<Collection<tournamentInformation>>>(_client_TournamentStatusChanged);
            _client.TournamentDeleted += new EventHandler<EventArgs<Collection<tournamentInformation>>>(_client_TournamentDeleted);

            _isBinded = true;
        }
        private bool _isBinded = false;
        /// <summary>
        /// 通信クライアントのバインド解除
        /// </summary>
        public void UnBindClient()
        {
            if (!_isBinded || _client == null)
                return;

            _client.NewHostReceived -= _client_NewHostReceived;
            _client.HostStatusChanged -= _client_HostStatusChanged;
            _client.HostDeleted -= _client_HostDeleted;

            _client.NewTournamentReceived -= _client_NewTournamentReceived;
            _client.TournamentStatusChanged -= _client_TournamentStatusChanged;
            _client.TournamentDeleted -= _client_TournamentDeleted;

            _client = null;

            _isBinded = false;
        }
        #endregion


        void UpdateDataView()
        {
            HostsDataView.AllowNew = false;
            HostsDataView.AllowEdit = false;
            HostsDataView.AllowDelete = false;

            if (string.IsNullOrEmpty(RankFilter))
                HostsDataView.RowFilter = string.Empty;
            else
                HostsDataView.RowFilter = string.Format("Rank LIKE '*{0}*'", RankFilter);
        }

        void _client_NewHostReceived(object sender, EventArgs<Collection<host>> e)
        {
            foreach (var host in e.Field)
                _hosts.AddHostsRow(host.ToHostsRow(_hosts));
        }

        void _client_HostStatusChanged(object sender, EventArgs<Collection<host>> e)
        {
            foreach (var host in e.Field)
            {
                var row = _hosts.FindByIdTime(host.Id.value, host.Time);
                if (row == null)
                    continue;

                row.BeginEdit();
                row.LastUpdate = host.lastUpdate;
                row.IpPort = host.Ip + ":" + host.Port.ToString();
                row.Rank = host.Rank;
                row.Comment = host.Comment;
                row.IsFighting = host.IsFighting;
                row.EndEdit();
            };
            _hosts.AcceptChanges();
        }

        void _client_HostDeleted(object sender, EventArgs<Collection<host>> e)
        {
            foreach (var host in e.Field)
            {
                var row = _hosts.FindByIdTime(host.Id.value, host.Time);
                if (row == null)
                    continue;

                _hosts.RemoveHostsRow(row);
            }
            _hosts.AcceptChanges();
        }

        void _client_NewTournamentReceived(object sender, EventArgs<Collection<tournamentInformation>> e)
        {
            foreach (var tournamentInformation in e.Field)
                _hosts.AddHostsRow(tournamentInformation.ToHostsRow(_hosts));

            _hosts.AcceptChanges();
        }

        void _client_TournamentStatusChanged(object sender, EventArgs<Collection<tournamentInformation>> e)
        {
            foreach (var tournamentInformation in e.Field)
            {
                var row = _hosts.FindByIdTime(tournamentInformation.Id.value, tournamentInformation.Time);
                if (row == null)
                    continue;

                row.BeginEdit();
                row.LastUpdate = tournamentInformation.LastUpdate;
                row.IpPort = Enum.GetName(typeof(TournamentTypes), tournamentInformation.Type) + ":" + tournamentInformation.PlayersCount + "/" + tournamentInformation.UserCount.ToString();
                row.Rank = tournamentInformation.Rank;
                row.Comment = tournamentInformation.Comment;
                row.IsFighting = tournamentInformation.Started;
                row.EndEdit();
            }

            _hosts.AcceptChanges();
        }

        void _client_TournamentDeleted(object sender, EventArgs<Collection<tournamentInformation>> e)
        {
            foreach (var tournamentInformation in e.Field)
            {
                var row = _hosts.FindByIdTime(tournamentInformation.Id.value, tournamentInformation.Time);
                if (row == null)
                    continue;

                _hosts.RemoveHostsRow(row);
            }

            _hosts.AcceptChanges();
        }
    }
}
