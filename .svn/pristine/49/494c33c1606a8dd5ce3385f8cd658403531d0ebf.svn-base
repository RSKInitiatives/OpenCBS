using System;
using OpenCBS.ArchitectureV2.CommandData;
using OpenCBS.ArchitectureV2.Interface;
using OpenCBS.ArchitectureV2.Interface.Presenter;

namespace OpenCBS.ArchitectureV2.Command
{
    public class ShowStartPageCommand : Command, ICommand<ShowStartPageCommandData>
    {
        private readonly Lazy<IStartPagePresenter> _presenter;

        public ShowStartPageCommand(IApplicationController applicationController, Lazy<IStartPagePresenter> presenter) : base(applicationController)
        {
            _presenter = presenter;
        }

        public void Execute(ShowStartPageCommandData commandData)
        {
            if (ActivateViewIfExists("StartPageView"))
            {
                return;
            }
            _presenter.Value.Run();
        }
    }
}
