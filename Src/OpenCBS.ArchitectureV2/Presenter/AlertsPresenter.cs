using System.Collections.Generic;
using System.ComponentModel;
using OpenCBS.ArchitectureV2.CommandData;
using OpenCBS.ArchitectureV2.Interface;
using OpenCBS.ArchitectureV2.Interface.Presenter;
using OpenCBS.ArchitectureV2.Interface.Repository;
using OpenCBS.ArchitectureV2.Interface.View;
using OpenCBS.ArchitectureV2.Message;
using OpenCBS.CoreDomain;
using OpenCBS.Enums;
using OpenCBS.Shared;
using OpenCBS.CoreDomain.Clients;

namespace OpenCBS.ArchitectureV2.Presenter
{
    public class AlertsPresenter : IAlertsPresenter, IAlertsPresenterCallbacks
    {
        private readonly IAlertsView _view;
        private readonly IErrorView _errorView;
        private readonly IApplicationController _applicationController;
        private readonly ILoanRepository _loanRepository;
        private readonly IClientRepository _clientRepository;

        private List<Model.Alert> _alerts;
        private List<Client> _clients; 

        public AlertsPresenter(
            IAlertsView view,
            IErrorView errorView,
            IApplicationController applicationController,
            ILoanRepository loanRepository,
            IClientRepository clientRepository)
        {
            _view = view;
            _errorView = errorView;
            _applicationController = applicationController;
            _loanRepository = loanRepository;
            _clientRepository = clientRepository;
        }

        public void Run()
        {
            _view.Attach(this);
            _applicationController.Publish(new ShowViewMessage(this, _view));
            _applicationController.Publish(new AlertsShownMessage(this));
            Reload();
        }

        public object View
        {
            get { return _view; }
        }

        public void DetachView()
        {
            _applicationController.Publish(new AlertsHiddenMessage(this));
        }

        public void Refresh()
        {
            var alerts = _alerts.FindAll(x =>
                _view.ShowPerformingLoansToday && x.IsLoan && x.Status == OContractStatus.Active && x.LateDays == 0 && x.Date.Date == TimeProvider.Today ||
                _view.ShowPerformingLoansAll && x.IsLoan && x.Status == OContractStatus.Active && x.LateDays == 0 && x.Date.Date > TimeProvider.Today ||
                _view.ShowLateLoans && x.IsLoan && x.Status == OContractStatus.Active && x.LateDays > 0 ||
                _view.ShowPendingLoans && x.IsLoan && x.Status == OContractStatus.Pending ||
                _view.ShowValidatedLoans && x.IsLoan && x.Status == OContractStatus.Validated ||
                _view.ShowPostponedLoans && x.IsLoan && x.Status == OContractStatus.Postponed ||
                _view.ShowPendingSavings && x.IsSaving && x.Status == OContractStatus.Pending ||
                _view.ShowOverdraftSavings && x.IsSaving && x.Amount < 0 ||
                _view.ShowNewClients && x.IsClient && x.ClientStatus == OClientStatus.New ||
                _view.ShowUpdatedClients && x.IsClient && x.ClientStatus == OClientStatus.Updated);
            _view.SetAlerts(alerts);           
        }

        public void Reload()
        {
            var bw = new BackgroundWorker();
            bw.DoWork += (sender, e) =>
            {
                e.Result = _loanRepository.GetAlerts(TimeProvider.Now, User.CurrentUser.Id);
            };
            bw.RunWorkerCompleted += (sender, e) =>
            {
                _view.StopProgress();
                if (e.Error != null)
                {
                    _errorView.Run(e.Error.Message);
                    return;
                }
                _alerts = (List<Model.Alert>) e.Result;
                Refresh();
            };
            _view.StartProgress();
            bw.RunWorkerAsync();

            //slycode
            var bw1 = new BackgroundWorker();
            bw1.DoWork += (sender, e) =>
            {
                e.Result = _clientRepository.GetAlerts(TimeProvider.Now, User.CurrentUser.Id);
            };
            bw1.RunWorkerCompleted += (sender, e) =>
            {
                _view.StopProgress();
                if (e.Error != null)
                {
                    _errorView.Run(e.Error.Message);
                    return;
                }
                var result = (List<Model.Alert>)e.Result;
                foreach (Model.Alert alert in result) 
                {
                    if (_alerts.Contains(alert))
                    {
                        Model.Alert eAlert = _alerts.Find(a => a.Id == alert.Id);
                        eAlert.Id = alert.Id;
                        //eAlert.Kind = OpenCBS.ArchitectureV2.Model.Alert.AlertKind.Client;
                        eAlert.ClientStatus = alert.ClientStatus;
                        eAlert.Date = alert.Date;
                        eAlert.ClientName = alert.ClientName;
                        eAlert.Address = alert.Address;
                        eAlert.Phone = alert.Phone;
                        eAlert.BranchName = alert.BranchName;
                    }
                    else
                        _alerts.Add(alert);
                }                
                Refresh();
            };
            _view.StartProgress();
            bw1.RunWorkerAsync();
        }

        public void ActivateAlert()
        {
            var id = _view.SelectedLoanId;
            if (id != null)
            {
                _applicationController.Execute(new EditLoanCommandData { Id = id.Value });
                return;
            }

            id = _view.SelectedSavingsId;
            if (id != null)
            {
                _applicationController.Execute(new EditSavingCommandData { Id = id.Value });
            }

            id = _view.SelectedClientId;
            if (id != null)
            {
                _applicationController.Execute(new EditClientCommandData { Id = id.Value });
            }
        }
    }
}
