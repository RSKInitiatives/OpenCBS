using OpenCBS.ArchitectureV2.CommandData;
using OpenCBS.ArchitectureV2.Interface;

namespace OpenCBS.ArchitectureV2.Command
{
    public class FastRepaymentCommand : ICommand<FastRepaymentCommandData>
    {
        public void Execute(FastRepaymentCommandData commandData)
        {
            commandData.DefaultAction();
        }
    }
}
