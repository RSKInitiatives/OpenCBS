using System;
using OpenCBS.ArchitectureV2.CommandData;
using OpenCBS.ArchitectureV2.Interface;
using OpenCBS.ArchitectureV2.Interface.Presenter;
using OpenCBS.Payroll.CommandData;
using OpenCBS.Payroll.Interface.Presenter;

namespace OpenCBS.Payroll.Command
{
    public class ShowLeaveApplicationCommand : OpenCBS.ArchitectureV2.Command.Command, ICommand<ShowLeaveApplicationCommandData>
    {
        private readonly Lazy<ILeaveApplicationPresenter> _presenter;

        public ShowLeaveApplicationCommand(IApplicationController applicationController, Lazy<ILeaveApplicationPresenter> presenter) : base(applicationController)
        {
            _presenter = presenter;
        }

        public void Execute(ShowLeaveApplicationCommandData commandData)
        {
            if (ActivateViewIfExists("LeaveApplicationView"))
            {
                return;
            }
            _presenter.Value.Run();
        }
    }
}
