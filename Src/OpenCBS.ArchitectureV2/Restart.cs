using OpenCBS.Shared.Settings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenCBS.ArchitectureV2
{
    public static class Restart
    {
        private const string RESTARTER = "OpenCBS.GUI.Restarter.exe";
        private const string ONLINE = " -online";

        /// <summary>
        /// Launches the application restarter.<br/>
        /// </summary>
        public static void LaunchRestarter()
        {
            string path = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            path = Path.Combine(path, RESTARTER);

            if (TechnicalSettings.UseOnlineMode)
                Process.Start(path, ONLINE);
            else
                Process.Start(path);

            Application.Exit();
        }
    }
}
