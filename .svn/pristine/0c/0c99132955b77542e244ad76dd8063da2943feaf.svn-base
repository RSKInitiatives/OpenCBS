using System;
using System.Reflection;
using OpenCBS.ArchitectureV2.CommandData;
using OpenCBS.ArchitectureV2.Interface;
using OpenCBS.ArchitectureV2.Message;
using OpenCBS.Enums;

namespace OpenCBS.ArchitectureV2.Command
{
    public class AddCompanyCommand : ICommand<AddCompanyCommandData>
    {
        private readonly IApplicationController _applicationController;

        public AddCompanyCommand(IApplicationController applicationController)
        {
            _applicationController = applicationController;
        }

        public void Execute(AddCompanyCommandData commandData)
        {
            var assembly = Assembly.Load("OpenCBS.GUI");
            var viewType = assembly.GetType("OpenCBS.GUI.Clients.ClientForm", true);
            var view = Activator.CreateInstance(viewType, OClientTypes.Corporate, null, false, _applicationController);
            _applicationController.Publish(new ShowViewMessage(this, view));
        }
    }
}
