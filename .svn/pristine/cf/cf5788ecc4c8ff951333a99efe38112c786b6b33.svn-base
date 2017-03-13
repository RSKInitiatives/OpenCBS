using System.Collections.Generic;
using OpenCBS.ArchitectureV2.Interface.Presenter;
using OpenCBS.ArchitectureV2.Model;
using OpenCBS.CoreDomain.Clients;

namespace OpenCBS.ArchitectureV2.Interface.View
{
    public interface IAlertsView : IView<IAlertsPresenterCallbacks>
    {
        void SetAlerts(List<Alert> alerts);
        void StartProgress();
        void StopProgress();

        bool ShowPerformingLoansToday { get; }
        bool ShowPerformingLoansAll { get; }
        bool ShowLateLoans { get; }
        bool ShowPendingLoans { get; }
        bool ShowValidatedLoans { get; }
        bool ShowPostponedLoans { get; }
        bool ShowPendingSavings { get; }
        bool ShowOverdraftSavings { get; }


        //slycode
        bool ShowNewClients { get; }
        bool ShowUpdatedClients { get; }
        bool ShowPendingClients { get; }

        int? SelectedLoanId { get; }
        int? SelectedSavingsId { get; }
        int? SelectedClientId { get; }
    }
}
