using OpenCBS.Update.Interface.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Deployment.Application;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenCBS.Update.Service
{
    enum UpdateStatuses
    {
        NoUpdateAvailable,
        UpdateAvailable,
        UpdateRequired,
        UpdateDownloaded,
        NotDeployedViaClickOnce,
        DeploymentDownloadException,
        InvalidDeploymentException,
        InvalidOperationException,        
    }

    public class ClientOnceUpdateService : IUpdateService
    {
        private void SyncWithInfo()
        {
            UpdateCheckInfo info = null;

            if (ApplicationDeployment.IsNetworkDeployed)
            {
                ApplicationDeployment ad = ApplicationDeployment.CurrentDeployment;

                try
                {
                    info = ad.CheckForDetailedUpdate();

                }
                catch (DeploymentDownloadException dde)
                {
                    MessageBox.Show("The new version of the application cannot be downloaded at this time. \n\nPlease check your network connection, or try again later. Error: " + dde.Message);
                    return;
                }
                catch (InvalidDeploymentException ide)
                {
                    MessageBox.Show("Cannot check for a new version of the application. The ClickOnce deployment is corrupt. Please redeploy the application and try again. Error: " + ide.Message);
                    return;
                }
                catch (InvalidOperationException ioe)
                {
                    MessageBox.Show("This application cannot be updated. It is likely not a ClickOnce application. Error: " + ioe.Message);
                    return;
                }

                if (info.UpdateAvailable)
                {
                    Boolean doUpdate = true;

                    if (!info.IsUpdateRequired)
                    {
                        DialogResult dr = MessageBox.Show("An update is available. Would you like to update the application now?", "Update Available", MessageBoxButtons.OKCancel);
                        if (!(DialogResult.OK == dr))
                        {
                            doUpdate = false;
                        }
                    }
                    else
                    {
                        // Display a message that the app MUST reboot. Display the minimum required version.
                        MessageBox.Show("This application has detected a mandatory update from your current " +
                            "version to version " + info.MinimumRequiredVersion.ToString() +
                            ". The application will now install the update and restart.",
                            "Update Available", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }

                    if (doUpdate)
                    {
                        try
                        {
                            ad.Update();
                            MessageBox.Show("The application has been upgraded, and will now restart.");
                            Application.Restart();
                        }
                        catch (DeploymentDownloadException dde)
                        {
                            MessageBox.Show("Cannot install the latest version of the application. \n\nPlease check your network connection, or try again later. Error: " + dde);
                            return;
                        }
                    }
                }
            }
        }

        public void CheckForUpdate()
        {
            BackgroundWorker bgWorker = new BackgroundWorker();
            bgWorker.DoWork += new DoWorkEventHandler(bgWorker_DoWork);
            bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgWorder_RunWorkerCompleted);
            bgWorker.RunWorkerAsync();            
        }

        private static void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            UpdateCheckInfo info = null;

            // Check if the application was deployed via ClickOnce.
            if (!ApplicationDeployment.IsNetworkDeployed)
            {
                e.Result = UpdateStatuses.NotDeployedViaClickOnce;
                return;
            }

            ApplicationDeployment updateCheck = ApplicationDeployment.CurrentDeployment;

            try
            {
                info = updateCheck.CheckForDetailedUpdate();
            }
            catch (DeploymentDownloadException dde)
            {
                e.Result = UpdateStatuses.DeploymentDownloadException;
                return;
            }
            catch (InvalidDeploymentException ide)
            {
                e.Result = UpdateStatuses.InvalidDeploymentException;
                return;
            }
            catch (InvalidOperationException ioe)
            {
                e.Result = UpdateStatuses.InvalidOperationException;
                return;
            }

            if (info.UpdateAvailable)
                if (info.IsUpdateRequired)
                    e.Result = UpdateStatuses.UpdateRequired;
                else
                    e.Result = UpdateStatuses.UpdateAvailable;
            else
                e.Result = UpdateStatuses.NoUpdateAvailable;
        }

        /// <summary>
        /// Will be executed once it's complete...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void bgWorder_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            switch ((UpdateStatuses)e.Result)
            {
                case UpdateStatuses.NoUpdateAvailable:
                    // No update available, do nothing
                    //MessageBox.Show("There's no update, thanks...");
                    break;
                case UpdateStatuses.UpdateAvailable:
                    DialogResult dialogResult = MessageBox.Show("An update is available. Would you like to update the application now?", "Update Available", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (dialogResult == DialogResult.OK)
                        UpdateApplication();
                    break;
                case UpdateStatuses.UpdateRequired:
                    MessageBox.Show("A required update is available, which will be installed now", "Update Available", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    UpdateApplication();
                    break;
                case UpdateStatuses.NotDeployedViaClickOnce:
                    MessageBox.Show("Is this deployed via ClickOnce?");
                    break;
                case UpdateStatuses.DeploymentDownloadException:
                    MessageBox.Show("Whoops, couldn't retrieve info on this app...");
                    break;
                case UpdateStatuses.InvalidDeploymentException:
                    MessageBox.Show("Cannot check for a new version. ClickOnce deployment is corrupt!");
                    break;
                case UpdateStatuses.InvalidOperationException:
                    MessageBox.Show("This application cannot be updated. It is likely not a ClickOnce application.");
                    break;
                default:
                    MessageBox.Show("Huh?");
                    break;
            }
        }

        private static void UpdateApplication()
        {
            try
            {
                ApplicationDeployment updateCheck = ApplicationDeployment.CurrentDeployment;
                updateCheck.Update();
                MessageBox.Show("The application has been upgraded, and will now restart.");
                Application.Restart();
            }
            catch (DeploymentDownloadException dde)
            {
                MessageBox.Show("Cannot install the latest version of the application. nnPlease check your network connection, or try again later. Error: " + dde);
                return;
            }
        }
    }

}
