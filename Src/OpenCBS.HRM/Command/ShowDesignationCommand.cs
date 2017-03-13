using System;
using OpenCBS.ArchitectureV2.CommandData;
using OpenCBS.ArchitectureV2.Interface;
using OpenCBS.ArchitectureV2.Interface.Presenter;
using OpenCBS.HRM.CommandData;
using OpenCBS.HRM.Interface.Presenter;

namespace OpenCBS.HRM.Command
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
            if (ActivateViewIfExists("frmDesignation"))
            {
                return;
            }
            _presenter.Value.Run();
        }
    }
}
