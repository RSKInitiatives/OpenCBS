using System.Windows.Forms;
using OpenCBS.ArchitectureV2.Interface.Service;
using StructureMap;
using System.ComponentModel.Composition;
using OpenCBS.ArchitectureV2.Interface;
using OpenCBS.Extensions;
using CommonServiceLocator.StructureMapAdapter.Unofficial;
using Microsoft.Practices.ServiceLocation;
using System.Linq;
using System.Diagnostics;

namespace OpenCBS.ArchitectureV2
{
    public class Bootstrapper
    {
        private CSLExportProvider _exportProvider;

        private readonly IContainer _container;

        public Bootstrapper(IContainer container)
        {
            _container = container;
        }

        public ApplicationContext GetAppContext()
        {
            _container.Configure(c => c.AddRegistry<DefaultRegistry>());
            Debug.WriteLine(_container.WhatDidIScan());
            _container.Inject<ApplicationContext>(new AppContext(_container));
            _container.GetInstance<ITranslationService>().Reload();            

            var locator = new StructureMapServiceLocator(_container);
            ServiceLocator.SetLocatorProvider(() => locator);
            
            _exportProvider = new CSLExportProvider(locator);
            var appController = _container.GetInstance<IApplicationController>();
            _exportProvider.RegisterType(appController.GetType());
            
            //var bindings = _container.GetAllInstances<IApplicationController>().ToList();
            //bindings.ForEach(m => _exportProvider.RegisterType(m.GetType()));

            MefContainer.AddExportProvider(_exportProvider);

            return _container.GetInstance<ApplicationContext>();
        }        
    }
}
