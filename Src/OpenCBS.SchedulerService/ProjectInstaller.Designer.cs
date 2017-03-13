namespace OpenCBS.SchedulerService
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.OpenCBSServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.OpenCBSService = new System.ServiceProcess.ServiceInstaller();
            // 
            // OpenCBSServiceProcessInstaller
            // 
            this.OpenCBSServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.OpenCBSServiceProcessInstaller.Password = null;
            this.OpenCBSServiceProcessInstaller.Username = null;
            // 
            // OpenCBSService
            // 
            this.OpenCBSService.ServiceName = "OpenCBS Scheduler Service";
            this.OpenCBSService.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            this.OpenCBSService.AfterInstall += new System.Configuration.Install.InstallEventHandler(this.OpenCBSService_AfterInstall);
            this.OpenCBSService.BeforeInstall += new System.Configuration.Install.InstallEventHandler(this.OpenCBSService_BeforeInstall);
            this.OpenCBSService.BeforeUninstall += new System.Configuration.Install.InstallEventHandler(this.OpenCBSService_BeforeUninstall);
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.OpenCBSServiceProcessInstaller,
            this.OpenCBSService});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller OpenCBSServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller OpenCBSService;
    }
}