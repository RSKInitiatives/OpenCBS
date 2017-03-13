using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BrightIdeasSoftware;
using OpenCBS.ArchitectureV2.Interface.Presenter;
using OpenCBS.ArchitectureV2.Interface.Service;
using OpenCBS.ArchitectureV2.Interface.View;
using OpenCBS.ArchitectureV2.Model;
using OpenCBS.Enums;
using StructureMap;
using OpenCBS.CoreDomain.Clients;

namespace OpenCBS.ArchitectureV2.View
{
    public partial class AlertsView : BaseView, IAlertsView
    {
        public AlertsView()
        {
            InitializeComponent();
        }

        [DefaultConstructor]
        public AlertsView(ITranslationService translationService)
            : base(translationService)
        {
            InitializeComponent();
            ShowLateLoans = true;
            ShowPerformingLoansToday = true;
            _lateLoansItem.Checked = true;
            _performingLoansItemToday.Checked = true;
            _clearSearchButton.Visible = false;
            SetUp();
        }

        public void Attach(IAlertsPresenterCallbacks presenterCallbacks)
        {
            FormClosing += (sender, e) => presenterCallbacks.DetachView();

            _reloadButton.Click += (sender, e) => presenterCallbacks.Reload();
            _alertsListView.DoubleClick += (sender, e) => presenterCallbacks.ActivateAlert();
            
            _performingLoansItemToday.Click += (sender, e) =>
            {
                ShowPerformingLoansToday = !ShowPerformingLoansToday;
                _performingLoansItemToday.Checked = ShowPerformingLoansToday;
                presenterCallbacks.Refresh();
            };

            _performingLoansItemAll.Click += (sender, e) =>
            {
                ShowPerformingLoansAll = !ShowPerformingLoansAll;
                _performingLoansItemAll.Checked = ShowPerformingLoansAll;
                presenterCallbacks.Refresh();
            };

            _lateLoansItem.Click += (sender, e) =>
            {
                ShowLateLoans = !ShowLateLoans;
                _lateLoansItem.Checked = ShowLateLoans;
                presenterCallbacks.Refresh();
            };

            _pendingLoansItem.Click += (sender, e) =>
            {
                ShowPendingLoans = !ShowPendingLoans;
                _pendingLoansItem.Checked = ShowPendingLoans;
                presenterCallbacks.Refresh();
            };

            _validatedLoansItem.Click += (sender, e) =>
            {
                ShowValidatedLoans = !ShowValidatedLoans;
                _validatedLoansItem.Checked = ShowValidatedLoans;
                presenterCallbacks.Refresh();
            };

            _postponedLoansItem.Click += (sender, e) =>
            {
                ShowPostponedLoans = !ShowPostponedLoans;
                _postponedLoansItem.Checked = ShowPostponedLoans;
                presenterCallbacks.Refresh();
            };

            _pendingSavingsItem.Click += (sender, e) =>
            {
                ShowPendingSavings = !ShowPendingSavings;
                _pendingSavingsItem.Checked = ShowPendingSavings;
                presenterCallbacks.Refresh();
            };

            _overdraftSavingsItem.Click += (sender, e) =>
            {
                ShowOverdraftSavings = !ShowOverdraftSavings;
                _overdraftSavingsItem.Checked = ShowOverdraftSavings;
                presenterCallbacks.Refresh();
            };


            //slycode //client alerts
            _newClientsItem.Click += (sender, e) =>
            {
                ShowNewClients = !ShowNewClients;
                _newClientsItem.Checked = ShowNewClients;
                presenterCallbacks.Refresh();
            };

            _updatedClientsItem.Click += (sender, e) =>
            {
                ShowUpdatedClients = !ShowUpdatedClients;
                _updatedClientsItem.Checked = ShowUpdatedClients;
                presenterCallbacks.Refresh();
            };            
        }

        private void UpdateTitle()
        {
            Text = string.Format(TranslationService.Translate("Alerts ({0})"), _alertsListView.Items.Count);            
        }

        public void SetAlerts(List<Alert> alerts)
        {
            try
            {
                _alertsListView.SetObjects(alerts);
                _alertsListView.Sort(_lateDaysColumn, SortOrder.Descending);
                UpdateTitle();
            }
            catch (Exception exc) 
            {

            }
        }
        
        private static void OnFormatAlertRow(object sender, FormatRowEventArgs e)
        {
            var alert = (Alert) e.Model;
            e.Item.BackColor = alert.BackColor;
            e.Item.ForeColor = alert.ForeColor;
        }

        private void SetUp()
        {
            _dateColumn.AspectToStringConverter = delegate(object value)
            {
                var date = (DateTime) value;
                return date.ToShortDateString();
            };
            _amountColumn.AspectToStringConverter = delegate(object value)
            {
                var amount = (decimal) value;
                return amount.ToString("N2");
            };
            _statusColumn.AspectToStringConverter = delegate(object value)
            {
                if (value == null)
                    return "";
                var status = (OContractStatus) value;
                return TranslationService.Translate(status.ToString());
            };
            clientStatusColumn.AspectToStringConverter = delegate(object value)
            {
                if (value == null)
                    return "";
                var status = (OClientStatus)value;
                return TranslationService.Translate(status.ToString());
            };            
            _alertsListView.FormatRow += OnFormatAlertRow;
            
            _searchTextBox.TextChanged += (sender, e) => OnSearchTextChanged();
            (_searchTextBox.Control as TextBox).SetHint(TranslationService.Translate("Search"));
            _clearSearchButton.Click += (sender, e) => _searchTextBox.Text = string.Empty;
        }

        public void StartProgress()
        {
            Cursor = Cursors.WaitCursor;
           _toolStrip.Enabled = false;
           Text = TranslationService.Translate("Loans and Account Alerts (loading...)");
        }

        public void StopProgress()
        {
            Cursor = Cursors.Default;            
            _toolStrip.Enabled = true;                                        
        }

        public bool ShowPerformingLoansToday { get; private set; }

        public bool ShowPerformingLoansAll { get; private set; }

        public bool ShowLateLoans { get; private set; }

        public bool ShowPendingLoans { get; private set; }

        public bool ShowValidatedLoans { get; private set; }

        public bool ShowPostponedLoans { get; private set; }

        public bool ShowPendingSavings { get; private set; }

        public bool ShowOverdraftSavings { get; private set; }

        public bool ShowNewClients { get; private set; }

        public bool ShowUpdatedClients { get; private set; }

        public bool ShowPendingClients { get; private set; }


        private void OnSearchTextChanged()
        {
            var filter = _searchTextBox.Text.ToLower();
            if (string.IsNullOrEmpty(filter))
            {
                _alertsListView.UseFiltering = false;
                _clearSearchButton.Visible = false;
                UpdateTitle();
                return;
            }

            _alertsListView.UseFiltering = true;
            _alertsListView.ModelFilter = new ModelFilter(delegate(object x)
            {
                var alert = (Alert) x;
                return alert.ContractCode.ToLower().Contains(filter)
                       || alert.ClientName.ToLower().Contains(filter)
                       || alert.LoanOfficer.ToLower().Contains(filter);
            });
            _clearSearchButton.Visible = true;
            UpdateTitle();
        }        

        public int? SelectedLoanId
        {
            get
            {
                var alert = (Alert) _alertsListView.SelectedObject;
                if (alert == null)
                {
                    return null;
                }
                return alert.IsLoan ? alert.Id : (int?) null;
            }
        }

        public int? SelectedSavingsId
        {
            get
            {
                var alert = (Alert) _alertsListView.SelectedObject;
                if (alert == null)
                {
                    return null;
                }
                return alert.IsSaving ? alert.Id : (int?) null;
            }
        }

        public int? SelectedClientId
        {
            get
            {
                var alert = (Alert)_alertsListView.SelectedObject;
                if (alert == null)
                {
                    return null;
                }
                return alert.IsClient ? alert.Id : (int?)null;
            }
        }

        private void AlertsView_Load(object sender, EventArgs e)
        {

        }
    }
}
