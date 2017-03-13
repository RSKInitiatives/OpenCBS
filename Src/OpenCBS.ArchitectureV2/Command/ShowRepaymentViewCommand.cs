using OpenCBS.ArchitectureV2.CommandData;
using OpenCBS.ArchitectureV2.Interface;

namespace OpenCBS.ArchitectureV2.Command
{
    public class ShowRepaymentViewCommand : ICommand<ShowRepaymentViewCommandData>
    {
        public void Execute(ShowRepaymentViewCommandData commandData)
        {
            commandData.DefaultAction();
        }
    }
}
