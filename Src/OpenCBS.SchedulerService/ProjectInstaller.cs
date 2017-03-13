using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace OpenCBS.SchedulerService
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }

        private void OpenCBSService_AfterInstall(object sender, InstallEventArgs e)
        {
            new ServiceController(OpenCBSService.ServiceName).Start();
        }

        private void OpenCBSService_BeforeUninstall(object sender, InstallEventArgs e)
        {
            /*var serviceController = new ServiceController(OpenCBSService.ServiceName);
            if (serviceController.CanStop)
                serviceController.Stop();
            */
            try
            {
                ServiceController controller = ServiceController.GetServices().Where
                (s => s.ServiceName == OpenCBSService.ServiceName).FirstOrDefault();
                if (controller != null)
                {
                    if ((controller.Status != ServiceControllerStatus.Stopped) &&
                    (controller.Status != ServiceControllerStatus.StopPending))
                    {
                        controller.Stop();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new System.Configuration.Install.InstallException
                    (ex.Message.ToString());
            }
        }

        private void OpenCBSService_BeforeInstall(object sender, InstallEventArgs e)
        {
            try
            {
                ServiceController controller = ServiceController.GetServices().Where
                (s => s.ServiceName == OpenCBSService.ServiceName).FirstOrDefault();
                if (controller != null)
                {
                    if ((controller.Status != ServiceControllerStatus.Stopped) &&
                    (controller.Status != ServiceControllerStatus.StopPending))
                    {
                        controller.Stop();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new System.Configuration.Install.InstallException
                    (ex.Message.ToString());
            }
        }
    }
}
