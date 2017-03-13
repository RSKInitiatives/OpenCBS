using OpenCBS.Update.Interface.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace OpenCBS.Update.Service
{
    public struct DownloadedVersionInfo
    {
        public bool Error { get; set; }
        public Version Version { get; set; }
        public string HomeUrl { get; set; }
        public string InstallerUrl { get; set; }
        public bool IsUpdateRequired { get; set; }
        public bool UpdateAvailable {
            get
            {
                var name = Assembly.GetExecutingAssembly().GetName();
                Version applicationVersion = Assembly.GetExecutingAssembly().GetName().Version;
                var compare = applicationVersion.CompareTo(Version);
                return compare < 0;
                
            }
            internal set
            {

            }
        }
    }

    public struct DownloadInstallerInfo
    {
        public bool Error { get; set; }
        public string Path { get; set; }
    }

    public delegate bool DelegateCheckForUpdateFinished(DownloadedVersionInfo versionInfo);
    public delegate void DelegateDownloadInstallerFinished(DownloadInstallerInfo info);

    [PartCreationPolicy(CreationPolicy.Shared)]
    [Export(typeof(IUpdateService))]
    public class DefaultUpdateService : IUpdateService
    {
        private static DownloadedVersionInfo versionInfo = new DownloadedVersionInfo();
        private static DownloadInstallerInfo installerInfo = new DownloadInstallerInfo();

        public void CheckForUpdate()
        {
            BackgroundWorker checkForUpdateBgWorker = new BackgroundWorker();
            checkForUpdateBgWorker.DoWork += new DoWorkEventHandler(CheckForUpdateBgWorker_DoWork);
            checkForUpdateBgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(CheckForUpdateBgWorker_RunWorkerCompleted);
            checkForUpdateBgWorker.RunWorkerAsync();
        }

        public void CheckForUpdateBgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string xmlUrl = "http://opencbs.maxifundonline.com/appupdate.xml";
            XmlTextReader reader = null;
            try
            {
                reader = new XmlTextReader(xmlUrl);
                reader.MoveToContent();
                string elementName = "";
                if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "opencbs"))
                {
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element)
                            elementName = reader.Name;
                        else
                        {
                            if ((reader.NodeType == XmlNodeType.Text) && (reader.HasValue))
                            {
                                switch (elementName)
                                {
                                    case "version":
                                        versionInfo.Version = new Version(reader.Value);
                                        break;
                                    case "homeurl":
                                        versionInfo.HomeUrl = reader.Value;
                                        break;
                                    case "installerurl":
                                        versionInfo.InstallerUrl = reader.Value;
                                        break;
                                    case "required":
                                        versionInfo.IsUpdateRequired = Convert.ToBoolean(reader.Value);
                                        break;
                                }
                            }
                        }
                    }
                    versionInfo.Error = false;
                }
            }
            catch (Exception exc)
            {
                versionInfo.Error = true;
                e.Result = UpdateStatuses.InvalidDeploymentException;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            if (versionInfo.UpdateAvailable)
                if (versionInfo.IsUpdateRequired)
                    e.Result = UpdateStatuses.UpdateRequired;
                else
                    e.Result = UpdateStatuses.UpdateAvailable;
            else
                e.Result = UpdateStatuses.NoUpdateAvailable;
        }

        private void CheckForUpdateBgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            switch ((UpdateStatuses)e.Result)
            {
                case UpdateStatuses.NoUpdateAvailable:
                    // No update available, do nothing
                    MessageBox.Show("No update available");
                    break;
                case UpdateStatuses.UpdateAvailable:                    
                    DialogResult dialogResult = MessageBox.Show("Version " + versionInfo.Version.Major + "." + versionInfo.Version.Minor + "." + versionInfo.Version.Build + " is available for download. Would you like to update the application now?", "OpenCBS Update Available", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.OK)
                        UpdateApplication();
                    break;
                case UpdateStatuses.UpdateRequired:
                    MessageBox.Show("A required update v" + versionInfo.Version.Major + "." + versionInfo.Version.Minor + "." + versionInfo.Version.Build + " is available, which will be installed now", "OpenCBS Update Available", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    UpdateApplication();
                    break;
                case UpdateStatuses.DeploymentDownloadException:
                    MessageBox.Show("Whoops, couldn't retrieve OpenCBS version, try again later", "OpenCBS Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case UpdateStatuses.InvalidDeploymentException:
                    MessageBox.Show("Cannot check for a new version. Deployment is corrupt!", "OpenCBS Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case UpdateStatuses.InvalidOperationException:
                    MessageBox.Show("This application cannot be updated.", "OpenCBS Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                default:
                    MessageBox.Show("Huh?");
                    break;
            }            
        }
        
        private void UpdateApplication()
        {
            try
            {
                BackgroundWorker downloadUpdateBgWorker = new BackgroundWorker();
                downloadUpdateBgWorker.DoWork += new DoWorkEventHandler(DownloadUpdateBgWorker_DoWork);
                downloadUpdateBgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(DownloadUpdateBgWorker_RunWorkerCompleted);
                downloadUpdateBgWorker.RunWorkerAsync();                                                
            }
            catch (Exception dde)
            {
                MessageBox.Show("Cannot install the latest version of the application. nnPlease check your network connection, or try again later. Error: " + dde);
                return;
            }
        }

        public void DownloadUpdateBgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // download and let the main thread know
            installerInfo.Error = true;
            string filepath = "";
            try
            {
                WebRequest request = WebRequest.Create(versionInfo.InstallerUrl);
                WebResponse response = request.GetResponse();
                string filename = "";
                int contentLength = 0;
                for (int a = 0; a < response.Headers.Count; a++)
                {
                    try
                    {
                        string val = response.Headers.Get(a);

                        switch (response.Headers.GetKey(a).ToLower())
                        {
                            case "content-length":
                                contentLength = Convert.ToInt32(val);
                                break;
                            case "content-disposition":
                                string[] v2 = val.Split(';');
                                foreach (string s2 in v2)
                                {
                                    string[] v3 = s2.Split('=');
                                    if (v3.Length == 2)
                                    {
                                        if (v3[0].Trim().ToLower() == "filename")
                                        {
                                            char[] sss = { ' ', '"' };
                                            filename = v3[1].Trim(sss);
                                        }
                                    }
                                }
                                break;
                        }
                    }
                    catch (Exception) { };
                }

                if (filename.Length == 0)
                    filename = "OpenCBS_Installer.msi";

                filepath = Path.Combine(Path.GetTempPath(), filename);

                if (File.Exists(filepath))
                {
                    try
                    {
                        File.Delete(filepath);
                    }
                    catch
                    {
                    }
                    if (File.Exists(filepath))
                    {
                        string rname = Path.GetRandomFileName();
                        rname.Replace('.', '_');
                        rname += ".msi";
                        filepath = Path.Combine(Path.GetTempPath(), rname);
                    }
                }
                Stream stream = response.GetResponseStream();
                int pos = 0;
                byte[] buf2 = new byte[8192];
                FileStream fs = new FileStream(filepath, FileMode.CreateNew);
                while ((0 == contentLength) || (pos < contentLength))
                {
                    int maxBytes = 8192;
                    if ((0 != contentLength) && ((pos + maxBytes) > contentLength)) maxBytes = contentLength - pos;
                    int bytesRead = stream.Read(buf2, 0, maxBytes);
                    if (bytesRead <= 0) break;
                    fs.Write(buf2, 0, bytesRead);                    
                    pos += bytesRead;
                }
                fs.Close();
                stream.Close();
                installerInfo.Error = false;
                installerInfo.Path = filepath;

                e.Result = UpdateStatuses.UpdateDownloaded;
            }
            catch
            {
                e.Result = UpdateStatuses.DeploymentDownloadException;
                // when something goes wrong - at least do the cleanup :)
                if (filepath.Length > 0)
                {
                    try
                    {
                        File.Delete(filepath);
                    }
                    catch
                    {
                    }
                }
            }
        }

        private void DownloadUpdateBgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            switch ((UpdateStatuses)e.Result)
            {
                case UpdateStatuses.UpdateDownloaded:                    
                    // run the installer and exit the app
                    try
                    {
                        Process.Start(installerInfo.Path);
                        MessageBox.Show("The application has been upgraded, and will now restart.");
                        Application.Restart();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Error while running the installer.", "OpenCBS Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        try
                        {
                            File.Delete(installerInfo.Path);
                        }
                        catch { }
                        return;
                    }
                    break;
                case UpdateStatuses.DeploymentDownloadException:
                    MessageBox.Show("Error while downloading the installer", "OpenCBS Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }            
        }
        
    }
}
