using OpenCBS.ArchitectureV2.CommandData;
using OpenCBS.ArchitectureV2.Interface;
using OpenCBS.ArchitectureV2.Interface.Presenter;

namespace OpenCBS.ArchitectureV2.Command
{
    public class EditVillageBankCommand : Command, ICommand<EditVillageBankCommandData>
    {
        private readonly IVillageBankPresenter _presenter;

        public EditVillageBankCommand(IApplicationController applicationController, IVillageBankPresenter presenter) : base(applicationController)
        {
            _presenter = presenter;
        }

        public void Execute(EditVillageBankCommandData commandData)
        {
            if (ActivateViewIfExists("VillageBankView" + commandData.VillageBankId))
            {
                return;
            }
            _presenter.Run(commandData.VillageBankId);
        }
    }
}
