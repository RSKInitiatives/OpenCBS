using System.Linq;
using System.Windows.Forms;
using OpenCBS.ArchitectureV2.Interface;
using OpenCBS.ArchitectureV2.Message;

namespace OpenCBS.ArchitectureV2.Command
{
    public abstract class Command
    {
        protected readonly IApplicationController ApplicationController;

        protected Command(IApplicationController applicationController)
        {
            ApplicationController = applicationController;
        }

        protected bool ActivateViewIfExists(string name)
        {
            var view = Application.OpenForms.Cast<Form>().FirstOrDefault(x => x.Name == name);
            if (view == null) return false;
            ApplicationController.Publish(new ActivateViewMessage(this, view));
            return true;
        }
    }
}
