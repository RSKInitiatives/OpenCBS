using OpenCBS.ArchitectureV2.Interface;
using OpenCBS.ArchitectureV2.Interface.Service;
using OpenCBS.ArchitectureV2.Interface.View;
using OpenCBS.ArchitectureV2.Presenter;
using OpenCBS.ArchitectureV2.Service;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using StructureMap.Graph.Scanning;
using System.Diagnostics;
using TinyMessenger;

namespace OpenCBS.ArchitectureV2
{
    public class DefaultRegistry : Registry
    {
        public DefaultRegistry()
        {
            try
            {
                Scan(scanner =>
                    {
                        scanner.Assembly("OpenCBS.GUI");
                        scanner.TheCallingAssembly();
                        scanner.WithDefaultConventions();
                        scanner.ConnectImplementationsToTypesClosing(typeof(ICommand<>));
                        scanner.AssembliesFromPath("Extensions");                        
                        scanner.LookForRegistries();
                        scanner.AddAllTypesOf<IMainView>();
                    });
            }
            catch (System.Exception)
            {
                Scan(scanner =>
                {
                    scanner.Assembly("OpenCBS.GUI");
                    scanner.TheCallingAssembly();
                    scanner.WithDefaultConventions();
                    scanner.ConnectImplementationsToTypesClosing(typeof(ICommand<>));
                    scanner.LookForRegistries();
                    scanner.AddAllTypesOf<IMainView>();
                });
            }

            For<ITranslationService>().Singleton().Use<TranslationService>();
            For<IApplicationController>().Singleton().Use<ApplicationController>();
            For<ITinyMessengerHub>().Singleton().Use<TinyMessengerHub>();
            For<IConnectionProvider>().Singleton().Use<ConnectionProvider>();

            TypeRepository.AssertNoTypeScanningFailures();           
        }
    }
}
