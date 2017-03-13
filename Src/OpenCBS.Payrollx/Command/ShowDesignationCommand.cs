using System;
using OpenCBS.ArchitectureV2.CommandData;
using OpenCBS.ArchitectureV2.Interface;
using OpenCBS.ArchitectureV2.Interface.Presenter;
using OpenCBS.Payroll.CommandData;
using OpenCBS.Payroll.Interface.Presenter;

namespace OpenCBS.Payroll.Command
{
    public class ShowDesignationCommand : OpenCBS.ArchitectureV2.Command.Command, ICommand<ShowDesignationCommandData>
    {
        private readonly Lazy<IDesignationPresenter> _presenter;

        public ShowDesignationCommand(IApplicationController applicationController, Lazy<IDesignationPresenter> presenter) : base(applicationController)
        {
            _presenter = presenter;
        }

        public void Execute(ShowDesignationCommandData commandData)
        {
            if (ActivateViewIfExists("DesignationView"))
            {
                return;
            }
            _presenter.Value.Run();
        }
    }
}
