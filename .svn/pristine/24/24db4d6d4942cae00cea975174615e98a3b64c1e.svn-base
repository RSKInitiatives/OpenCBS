using System.Diagnostics;
using OpenCBS.ArchitectureV2.CommandData;
using OpenCBS.ArchitectureV2.Interface;

namespace OpenCBS.ArchitectureV2.Command
{
    public class OpenEmailCommand : ICommand<OpenEmailCommandData>
    {
        public void Execute(OpenEmailCommandData commandData)
        {
            var url = string.Format("mailto:{0}?subject={1}", commandData.To, commandData.Subject);
            Process.Start(url);
        }
    }
}
