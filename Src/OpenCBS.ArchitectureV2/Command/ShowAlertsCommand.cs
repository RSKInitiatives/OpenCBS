using System;
using OpenCBS.ArchitectureV2.CommandData;
using OpenCBS.ArchitectureV2.Interface;
using OpenCBS.ArchitectureV2.Interface.Presenter;

namespace OpenCBS.ArchitectureV2.Command
{
    public class ShowAlertsCommand : Command, ICommand<ShowAlertsCommandData>
    {
        private readonly Lazy<IAlertsPresenter> _presenter;

        public ShowAlertsCommand(IApplicationController applicationController, Lazy<IAlertsPresenter> presenter)
            : base(applicationController)
        {
            _presenter = presenter;
        }

        public void Execute(ShowAlertsCommandData commandData)
        {
            if (ActivateViewIfExists("AlertsView"))
            {
                return;
            }
            _presenter.Value.Run();
        }
    }
}
