using System;
using OpenCBS.ArchitectureV2.CommandData;
using OpenCBS.ArchitectureV2.Interface;
using OpenCBS.ArchitectureV2.Interface.Presenter;

namespace OpenCBS.ArchitectureV2.Command
{
    public class ShowSearchCommand : Command, ICommand<ShowSearchCommandData>
    {
        private readonly Lazy<ISearchPresenter> _presenter;

        public ShowSearchCommand(IApplicationController applicationController, Lazy<ISearchPresenter> presenter) : base(applicationController)
        {
            _presenter = presenter;
        }

        public void Execute(ShowSearchCommandData commandData)
        {
            if (ActivateViewIfExists("SearchView"))
            {
                return;
            }
            _presenter.Value.Run();
        }
    }
}
