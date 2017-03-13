using OpenCBS.ArchitectureV2.Interface;
using OpenCBS.ArchitectureV2.Interface.Service;
using OpenCBS.ArchitectureV2.Presenter;
using OpenCBS.ArchitectureV2.Service;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using TinyMessenger;

namespace OpenCBS.GUI
{
    public class DefaultRegistry : Registry
    {
        public DefaultRegistry()
        {
            try
            {
                Scan(scanner =>
                    {
                        scanner.TheCallingAssembly();
                        scanner.WithDefaultConventions();                        
                    });
            }
            catch (System.Exception)
            {
                
            }            
        }
    }
}
