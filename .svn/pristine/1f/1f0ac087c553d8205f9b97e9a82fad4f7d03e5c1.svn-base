// Copyright © 2013 Open Octopus Ltd.
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License along
// with this program; if not, write to the Free Software Foundation, Inc.,
// 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//
// Website: http://www.opencbs.com
// Contact: contact@opencbs.com

using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;
using OpenCBS.CoreDomain;
using System.Collections.Generic;
using System;
using System.Linq;
using System.ComponentModel.Composition.Primitives;

namespace OpenCBS.Extensions
{
    public class MefContainer
    {
        #region Fields
        private CompositionContainer _container;
        private static bool modified;

        private static MefContainer _instance;
        #endregion        
        
        public static MefContainer Current
        {
            get { return _instance ?? (_instance = new MefContainer()); }
        }        

        private MefContainer()
        {
            if (modified || _container == null)
            {
                if (_container != null)
                    _container.Dispose();


                var extensionsFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;
                extensionsFolder = Path.Combine(extensionsFolder, "Extensions");
                var catalog = new AggregateCatalog(
                        new AssemblyCatalog(Assembly.GetExecutingAssembly()),
                        new AssemblyCatalog(Assembly.GetAssembly(typeof(DatabaseConnection)))
                    );

                if (Directory.Exists(extensionsFolder))
                    catalog.Catalogs.Add(new DirectoryCatalog(extensionsFolder));
                if (Directory.Exists(extensionsFolder + "/HRM"))
                    catalog.Catalogs.Add(new DirectoryCatalog(extensionsFolder + "/HRM"));

#if SAMPLE_EXTENSIONS
            var samples = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;
            samples = Path.Combine(samples, "OpenCBS.Extensions.Samples.dll");
            if (File.Exists(samples)) 
                catalog.Catalogs.Add(new AssemblyCatalog(samples));
#endif
                _container = new CompositionContainer(catalog, ExportProviders.ToArray());
                modified = false;
            }
        }

        /// <summary>
        /// Adds the specific export provider to the composer.
        /// </summary>
        /// <param name="provider">The export provider add to the composer.</param>
        /// <param name="postContainerModifier">A modifier action called after the container has been created.</param>

        public static IList<ExportProvider> ExportProviders = new List<ExportProvider>();
        public static void AddExportProvider(ExportProvider provider)
        {
            if (provider == null)
                throw new ArgumentNullException("provider");

            ExportProviders.Add(provider);
            modified = true;            
        }

        public void Bind(object host)
        {
            _container.SatisfyImportsOnce(host);
        }
    }
}
