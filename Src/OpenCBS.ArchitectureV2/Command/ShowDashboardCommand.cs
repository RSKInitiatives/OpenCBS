using System;
using System.Reflection;
using OpenCBS.ArchitectureV2.CommandData;
using OpenCBS.ArchitectureV2.Interface;
using OpenCBS.ArchitectureV2.Message;

namespace OpenCBS.ArchitectureV2.Command
{
    public class ShowDashboardCommand : Command, ICommand<ShowDashboardCommandData>
    {
        public ShowDashboardCommand(IApplicationController applicationController) : base(applicationController)
        {
        }

        public void Execute(ShowDashboardCommandData commandData)
        {
            if (ActivateViewIfExists("DashboardForm"))
            {
                return;
            }

            var assembly = Assembly.Load("OpenCBS.GUI");
            var viewType = assembly.GetType("OpenCBS.GUI.DashboardForm", true);
            var view = Activator.CreateInstance(viewType, ApplicationController);
            ApplicationController.Publish(new ShowViewMessage(this, view));
            ApplicationController.Publish(new DashboardShownMessage(this));
        }
    }
}
