using OpenCBS.ArchitectureV2.CommandData;
using OpenCBS.ArchitectureV2.Interface;
using OpenCBS.ArchitectureV2.Message;

namespace OpenCBS.ArchitectureV2.Command
{
    public class EditClientCommand : ICommand<EditClientCommandData>
    {
        private readonly IApplicationController _applicationController;

        public EditClientCommand(IApplicationController applicationController)
        {
            _applicationController = applicationController;
        }

        public void Execute(EditClientCommandData commandData)
        {
            _applicationController.Publish(new EditClientMessage(this, commandData.Id));
        }
    }
}
