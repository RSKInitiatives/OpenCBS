using OpenCBS.ArchitectureV2.CommandData;
using OpenCBS.ArchitectureV2.Interface;
using OpenCBS.ArchitectureV2.Message;

namespace OpenCBS.ArchitectureV2.Command
{
    public class EditLoanCommand : ICommand<EditLoanCommandData>
    {
        private readonly IApplicationController _applicationController;

        public EditLoanCommand(IApplicationController applicationController)
        {
            _applicationController = applicationController;
        }

        public void Execute(EditLoanCommandData commandData)
        {
            _applicationController.Publish(new EditLoanMessage(this, commandData.Id));
        }
    }
}
