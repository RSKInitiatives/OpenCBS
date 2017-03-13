using System;
using System.Reflection;
using OpenCBS.ArchitectureV2.CommandData;
using OpenCBS.ArchitectureV2.Interface;
using OpenCBS.ArchitectureV2.Message;

namespace OpenCBS.ArchitectureV2.Command
{
    public class AddVillageBankCommand : ICommand<AddVillageBankCommandData>
    {
        private readonly IApplicationController _applicationController;

        public AddVillageBankCommand(IApplicationController applicationController)
        {
            _applicationController = applicationController;
        }

        public void Execute(AddVillageBankCommandData commandData)
        {
            var assembly = Assembly.Load("OpenCBS.GUI");
            var viewType = assembly.GetType("OpenCBS.GUI.Clients.NonSolidaryGroupForm", true);
            var view = Activator.CreateInstance(viewType, _applicationController);
            _applicationController.Publish(new ShowViewMessage(this, view));
        }
    }
}
