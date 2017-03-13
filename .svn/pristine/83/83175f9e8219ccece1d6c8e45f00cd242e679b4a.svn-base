using OpenCBS.ArchitectureV2.CommandData;
using OpenCBS.ArchitectureV2.Interface;
using OpenCBS.ArchitectureV2.Message;

namespace OpenCBS.ArchitectureV2.Command
{
    public class EditSavingCommand : ICommand<EditSavingCommandData>
    {
        private readonly IApplicationController _applicationController;

        public EditSavingCommand(IApplicationController applicationController)
        {
            _applicationController = applicationController;
        }

        public void Execute(EditSavingCommandData commandData)
        {
            _applicationController.Publish(new EditSavingMessage(this, commandData.Id));
        }
    }
}
