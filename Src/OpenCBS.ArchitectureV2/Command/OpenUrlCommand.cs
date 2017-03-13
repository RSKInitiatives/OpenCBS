using System.Diagnostics;
using OpenCBS.ArchitectureV2.CommandData;
using OpenCBS.ArchitectureV2.Interface;

namespace OpenCBS.ArchitectureV2.Command
{
    public class OpenUrlCommand : ICommand<OpenUrlCommandData>
    {
        public void Execute(OpenUrlCommandData commandData)
        {
            Process.Start(commandData.Url);
        }
    }
}
