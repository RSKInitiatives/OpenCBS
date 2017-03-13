using System;
using OpenCBS.ArchitectureV2.CommandData;
using OpenCBS.ArchitectureV2.Interface;
using OpenCBS.ArchitectureV2.Interface.Presenter;
using OpenCBS.HRM.CommandData;
using OpenCBS.HRM.Interface.Presenter;

namespace OpenCBS.HRM.Command
{
    public class ShowPayHeadCommand : OpenCBS.ArchitectureV2.Command.Command, ICommand<ShowPayHeadCommandData>
    {
        private readonly Lazy<IPayHeadPresenter> _presenter;

        public ShowPayHeadCommand(IApplicationController applicationController, Lazy<IPayHeadPresenter> presenter) : base(applicationController)
        {
            _presenter = presenter;
        }

        public void Execute(ShowPayHeadCommandData commandData)
        {
            if (ActivateViewIfExists("PayheadView"))
            {
                return;
            }
            _presenter.Value.Run();
        }
    }
}
