
using System.Windows.Forms;
using OpenCBS.GUI.UserControl;
using OpenCBS.Enums;
using OpenCBS.Services;
using OpenCBS.CoreDomain;

namespace OpenCBS.GUI
{
    partial class MainView
    {
        private System.Windows.Forms.ToolStripMenuItem mnuClients;
        private System.Windows.Forms.ToolStripMenuItem mnuAccounting;
        private System.Windows.Forms.ToolStripMenuItem mnuNewClient;
        private System.Windows.Forms.ToolStripMenuItem mnuSearchClient;
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuWindow;
        private System.Windows.Forms.ToolStripMenuItem mnuNewGroup;
        private System.Windows.Forms.ToolStripMenuItem mnuConfiguration;
        private System.Windows.Forms.ToolStripMenuItem mnuDomainOfApplication;
        private System.Windows.Forms.ToolStripMenuItem menuItemExportTransaction;
        private System.Windows.Forms.ToolStripMenuItem menuItemExchangeRate;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private ToolStripSeparator toolStripSeparatorConfig1;
        private ToolStripSeparator toolStripSeparatorConfig2;
        private ToolStripSeparator toolStripSeparatorConfig3;
        private StatusStrip mainStatusBar;
        private ToolStripLabel toolBarLblVersion;
        private ToolStripStatusLabel mainStatusBarLblUserName;
        private ToolStripStatusLabel mainStatusBarLblDate;
        private ToolStripStatusLabel mainStatusBarLblUpdateVersion;
        private ToolStripStatusLabel toolStripStatusLblBranchCode;
        private ToolStripMenuItem toolStripMenuItemAccountView;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem menuItemLocations;
        private ToolStripMenuItem toolStripMenuItemFundingLines;
        private ToolStripMenuItem toolStripMenuItemInstallmentTypes;


        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form
        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainView));
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.toolBarLblVersion = new System.Windows.Forms.ToolStripLabel();
            this.nIUpdateAvailable = new System.Windows.Forms.NotifyIcon(this.components);
            this.openCustomizableFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.colAlerts_Address = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.colAlerts_City = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.colAlerts_Phone = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.colAlerts_BranchName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.bwUserInformation = new System.ComponentModel.BackgroundWorker();
            this.mnuClients = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNewPerson = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNewClient = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNewGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNewVillage = new System.Windows.Forms.ToolStripMenuItem();
            this.newCorporateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuSearchClient = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSearchContract = new System.Windows.Forms.ToolStripMenuItem();
            this.reasignToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAccounting = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuChartOfAccounts = new System.Windows.Forms.ToolStripMenuItem();
            this.accountingRulesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trialBalanceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemAccountView = new System.Windows.Forms.ToolStripMenuItem();
            this.manualEntriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.standardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemExportTransaction = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNewclosure = new System.Windows.Forms.ToolStripMenuItem();
            this.fiscalYearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuConfiguration = new System.Windows.Forms.ToolStripMenuItem();
            this.branchesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tellersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorConfig1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuDomainOfApplication = new System.Windows.Forms.ToolStripMenuItem();
            this.loanPurposeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemLocations = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemFundingLines = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemInstallmentTypes = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorConfig2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemExchangeRate = new System.Windows.Forms.ToolStripMenuItem();
            this.currenciesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorConfig3 = new System.Windows.Forms.ToolStripSeparator();
            this.miAccountNumber = new System.Windows.Forms.ToolStripMenuItem();
            this.miContractCode = new System.Windows.Forms.ToolStripMenuItem();
            this.collateralProductsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.miMessaging = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.userGuideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contactMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getHelpFromForumToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.visitOpenCBScomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.mnuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDatabaseControlPanel = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemApplicationDate = new System.Windows.Forms.ToolStripMenuItem();
            this.languagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.frenchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.englishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.russianToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spanishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.portugueseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSecurity = new System.Windows.Forms.ToolStripMenuItem();
            this.rolesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemAddUser = new System.Windows.Forms.ToolStripMenuItem();
            this.miAuditTrail = new System.Windows.Forms.ToolStripMenuItem();
            this.changePasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuProducts = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPackages = new System.Windows.Forms.ToolStripMenuItem();
            this.savingProductsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTeller = new System.Windows.Forms.ToolStripMenuItem();
            this.miVaultTransfer = new System.Windows.Forms.ToolStripMenuItem();
            this.miTellerHistory = new System.Windows.Forms.ToolStripMenuItem();
            this._viewItem = new System.Windows.Forms.ToolStripMenuItem();
            this._startPageItem = new System.Windows.Forms.ToolStripMenuItem();
            this._alertsItem = new System.Windows.Forms.ToolStripMenuItem();
            this._dashboardItem = new System.Windows.Forms.ToolStripMenuItem();
            this._searchItem = new System.Windows.Forms.ToolStripMenuItem();
            this._modulesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._aboutModulesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPayroll = new System.Windows.Forms.ToolStripMenuItem();
            this.payrollToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.reportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainStatusBar = new System.Windows.Forms.StatusStrip();
            this.mainStatusBarLblUpdateVersion = new System.Windows.Forms.ToolStripStatusLabel();
            this.mainStatusBarLblUserName = new System.Windows.Forms.ToolStripStatusLabel();
            this.mainStatusBarLblCashPosition = new System.Windows.Forms.ToolStripStatusLabel();
            this.mainStatusBarLblDate = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLblBranchCode = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLblDB = new System.Windows.Forms.ToolStripStatusLabel();
            this.mainStatusBarLabelAutoLogoutTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.mainMenu.SuspendLayout();
            this.mainStatusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // toolBarLblVersion
            // 
            this.toolBarLblVersion.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            resources.ApplyResources(this.toolBarLblVersion, "toolBarLblVersion");
            this.toolBarLblVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(156)))));
            this.toolBarLblVersion.Name = "toolBarLblVersion";
            // 
            // nIUpdateAvailable
            // 
            this.nIUpdateAvailable.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            resources.ApplyResources(this.nIUpdateAvailable, "nIUpdateAvailable");
            this.nIUpdateAvailable.BalloonTipClicked += new System.EventHandler(this.nIUpdateAvailable_BalloonTipClicked);
            // 
            // openCustomizableFileDialog
            // 
            resources.ApplyResources(this.openCustomizableFileDialog, "openCustomizableFileDialog");
            // 
            // colAlerts_Address
            // 
            this.colAlerts_Address.AspectName = "Address";
            resources.ApplyResources(this.colAlerts_Address, "colAlerts_Address");
            this.colAlerts_Address.IsEditable = false;
            this.colAlerts_Address.IsVisible = false;
            // 
            // colAlerts_City
            // 
            this.colAlerts_City.AspectName = "City";
            resources.ApplyResources(this.colAlerts_City, "colAlerts_City");
            this.colAlerts_City.IsEditable = false;
            this.colAlerts_City.IsVisible = false;
            // 
            // colAlerts_Phone
            // 
            this.colAlerts_Phone.AspectName = "Phone";
            resources.ApplyResources(this.colAlerts_Phone, "colAlerts_Phone");
            this.colAlerts_Phone.IsEditable = false;
            this.colAlerts_Phone.IsVisible = false;
            // 
            // colAlerts_BranchName
            // 
            this.colAlerts_BranchName.AspectName = "BranchName";
            resources.ApplyResources(this.colAlerts_BranchName, "colAlerts_BranchName");
            this.colAlerts_BranchName.IsEditable = false;
            this.colAlerts_BranchName.IsVisible = false;
            // 
            // mnuClients
            // 
            this.mnuClients.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuNewPerson,
            this.mnuNewClient,
            this.toolStripMenuItem1,
            this.mnuSearchClient,
            this.mnuSearchContract,
            this.reasignToolStripMenuItem});
            this.mnuClients.Name = "mnuClients";
            resources.ApplyResources(this.mnuClients, "mnuClients");
            // 
            // mnuNewPerson
            // 
            resources.ApplyResources(this.mnuNewPerson, "mnuNewPerson");
            this.mnuNewPerson.Name = "mnuNewPerson";
            this.mnuNewPerson.Click += new System.EventHandler(this.mnuNewPerson_Click);
            // 
            // mnuNewClient
            // 
            this.mnuNewClient.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuNewGroup,
            this.mnuNewVillage,
            this.newCorporateToolStripMenuItem});
            resources.ApplyResources(this.mnuNewClient, "mnuNewClient");
            this.mnuNewClient.Name = "mnuNewClient";
            // 
            // mnuNewGroup
            // 
            resources.ApplyResources(this.mnuNewGroup, "mnuNewGroup");
            this.mnuNewGroup.Name = "mnuNewGroup";
            this.mnuNewGroup.Click += new System.EventHandler(this.mnuNewGroup_Click);
            // 
            // mnuNewVillage
            // 
            this.mnuNewVillage.Name = "mnuNewVillage";
            resources.ApplyResources(this.mnuNewVillage, "mnuNewVillage");
            this.mnuNewVillage.Click += new System.EventHandler(this.mnuNewVillage_Click);
            // 
            // newCorporateToolStripMenuItem
            // 
            this.newCorporateToolStripMenuItem.Name = "newCorporateToolStripMenuItem";
            resources.ApplyResources(this.newCorporateToolStripMenuItem, "newCorporateToolStripMenuItem");
            this.newCorporateToolStripMenuItem.Click += new System.EventHandler(this.newCorporateToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            // 
            // mnuSearchClient
            // 
            this.mnuSearchClient.Image = global::OpenCBS.GUI.Properties.Resources.find;
            resources.ApplyResources(this.mnuSearchClient, "mnuSearchClient");
            this.mnuSearchClient.Name = "mnuSearchClient";
            this.mnuSearchClient.Click += new System.EventHandler(this.mnuSearchClient_Click);
            // 
            // mnuSearchContract
            // 
            this.mnuSearchContract.Image = global::OpenCBS.GUI.Properties.Resources.find;
            resources.ApplyResources(this.mnuSearchContract, "mnuSearchContract");
            this.mnuSearchContract.Name = "mnuSearchContract";
            this.mnuSearchContract.Click += new System.EventHandler(this.mnuSearchContract_Click);
            // 
            // reasignToolStripMenuItem
            // 
            this.reasignToolStripMenuItem.Name = "reasignToolStripMenuItem";
            resources.ApplyResources(this.reasignToolStripMenuItem, "reasignToolStripMenuItem");
            this.reasignToolStripMenuItem.Click += new System.EventHandler(this.reasignToolStripMenuItem_Click);
            // 
            // mnuAccounting
            // 
            this.mnuAccounting.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuChartOfAccounts,
            this.accountingRulesToolStripMenuItem,
            this.trialBalanceToolStripMenuItem,
            this.toolStripMenuItemAccountView,
            this.manualEntriesToolStripMenuItem,
            this.standardToolStripMenuItem,
            this.toolStripSeparator2,
            this.menuItemExportTransaction,
            this.mnuNewclosure,
            this.fiscalYearToolStripMenuItem,
            this.toolStripSeparator3});
            this.mnuAccounting.Name = "mnuAccounting";
            resources.ApplyResources(this.mnuAccounting, "mnuAccounting");
            // 
            // mnuChartOfAccounts
            // 
            this.mnuChartOfAccounts.Image = global::OpenCBS.GUI.Properties.Resources.page;
            this.mnuChartOfAccounts.Name = "mnuChartOfAccounts";
            resources.ApplyResources(this.mnuChartOfAccounts, "mnuChartOfAccounts");
            // 
            // accountingRulesToolStripMenuItem
            // 
            this.accountingRulesToolStripMenuItem.Name = "accountingRulesToolStripMenuItem";
            resources.ApplyResources(this.accountingRulesToolStripMenuItem, "accountingRulesToolStripMenuItem");
            this.accountingRulesToolStripMenuItem.Click += new System.EventHandler(this.accountingRulesToolStripMenuItem_Click);
            // 
            // trialBalanceToolStripMenuItem
            // 
            this.trialBalanceToolStripMenuItem.Name = "trialBalanceToolStripMenuItem";
            resources.ApplyResources(this.trialBalanceToolStripMenuItem, "trialBalanceToolStripMenuItem");
            this.trialBalanceToolStripMenuItem.Click += new System.EventHandler(this.trialBalanceToolStripMenuItem_Click);
            // 
            // toolStripMenuItemAccountView
            // 
            this.toolStripMenuItemAccountView.Image = global::OpenCBS.GUI.Properties.Resources.book;
            resources.ApplyResources(this.toolStripMenuItemAccountView, "toolStripMenuItemAccountView");
            this.toolStripMenuItemAccountView.Name = "toolStripMenuItemAccountView";
            this.toolStripMenuItemAccountView.Click += new System.EventHandler(this.toolStripMenuItemAccountView_Click);
            // 
            // manualEntriesToolStripMenuItem
            // 
            this.manualEntriesToolStripMenuItem.Name = "manualEntriesToolStripMenuItem";
            resources.ApplyResources(this.manualEntriesToolStripMenuItem, "manualEntriesToolStripMenuItem");
            this.manualEntriesToolStripMenuItem.Click += new System.EventHandler(this.manualEntriesToolStripMenuItem_Click);
            // 
            // standardToolStripMenuItem
            // 
            this.standardToolStripMenuItem.Name = "standardToolStripMenuItem";
            resources.ApplyResources(this.standardToolStripMenuItem, "standardToolStripMenuItem");
            this.standardToolStripMenuItem.Click += new System.EventHandler(this.standardToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // menuItemExportTransaction
            // 
            resources.ApplyResources(this.menuItemExportTransaction, "menuItemExportTransaction");
            this.menuItemExportTransaction.Name = "menuItemExportTransaction";
            this.menuItemExportTransaction.Click += new System.EventHandler(this.menuItemExportTransaction_Click);
            // 
            // mnuNewclosure
            // 
            this.mnuNewclosure.Name = "mnuNewclosure";
            resources.ApplyResources(this.mnuNewclosure, "mnuNewclosure");
            this.mnuNewclosure.Click += new System.EventHandler(this.newClosureToolStripMenuItem_Click_1);
            // 
            // fiscalYearToolStripMenuItem
            // 
            this.fiscalYearToolStripMenuItem.Name = "fiscalYearToolStripMenuItem";
            resources.ApplyResources(this.fiscalYearToolStripMenuItem, "fiscalYearToolStripMenuItem");
            this.fiscalYearToolStripMenuItem.Click += new System.EventHandler(this.fiscalYearToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // mnuConfiguration
            // 
            this.mnuConfiguration.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.branchesToolStripMenuItem,
            this.tellersToolStripMenuItem,
            this.toolStripSeparatorConfig1,
            this.mnuDomainOfApplication,
            this.loanPurposeToolStripMenuItem,
            this.menuItemLocations,
            this.toolStripMenuItemFundingLines,
            this.toolStripMenuItemInstallmentTypes,
            this.toolStripSeparatorConfig2,
            this.menuItemExchangeRate,
            this.currenciesToolStripMenuItem,
            this.toolStripSeparatorConfig3,
            this.miAccountNumber,
            this.miContractCode,
            this.collateralProductsToolStripMenuItem,
            this.toolStripSeparator1,
            this.miMessaging});
            this.mnuConfiguration.Name = "mnuConfiguration";
            resources.ApplyResources(this.mnuConfiguration, "mnuConfiguration");
            // 
            // branchesToolStripMenuItem
            // 
            this.branchesToolStripMenuItem.Name = "branchesToolStripMenuItem";
            resources.ApplyResources(this.branchesToolStripMenuItem, "branchesToolStripMenuItem");
            this.branchesToolStripMenuItem.Click += new System.EventHandler(this.branchesToolStripMenuItem_Click);
            // 
            // tellersToolStripMenuItem
            // 
            this.tellersToolStripMenuItem.Name = "tellersToolStripMenuItem";
            resources.ApplyResources(this.tellersToolStripMenuItem, "tellersToolStripMenuItem");
            this.tellersToolStripMenuItem.Click += new System.EventHandler(this.tellersToolStripMenuItem_Click);
            // 
            // toolStripSeparatorConfig1
            // 
            this.toolStripSeparatorConfig1.Name = "toolStripSeparatorConfig1";
            resources.ApplyResources(this.toolStripSeparatorConfig1, "toolStripSeparatorConfig1");
            // 
            // mnuDomainOfApplication
            // 
            resources.ApplyResources(this.mnuDomainOfApplication, "mnuDomainOfApplication");
            this.mnuDomainOfApplication.Name = "mnuDomainOfApplication";
            this.mnuDomainOfApplication.Click += new System.EventHandler(this.mnuDomainOfApplication_Click);
            // 
            // loanPurposeToolStripMenuItem
            // 
            this.loanPurposeToolStripMenuItem.Name = "loanPurposeToolStripMenuItem";
            resources.ApplyResources(this.loanPurposeToolStripMenuItem, "loanPurposeToolStripMenuItem");
            this.loanPurposeToolStripMenuItem.Click += new System.EventHandler(this.loanPurposeToolStripMenuItem_Click);
            // 
            // menuItemLocations
            // 
            this.menuItemLocations.Name = "menuItemLocations";
            resources.ApplyResources(this.menuItemLocations, "menuItemLocations");
            this.menuItemLocations.Click += new System.EventHandler(this.menuItemLocations_Click);
            // 
            // toolStripMenuItemFundingLines
            // 
            this.toolStripMenuItemFundingLines.Name = "toolStripMenuItemFundingLines";
            resources.ApplyResources(this.toolStripMenuItemFundingLines, "toolStripMenuItemFundingLines");
            this.toolStripMenuItemFundingLines.Click += new System.EventHandler(this.toolStripMenuItemFundingLines_Click);
            // 
            // toolStripMenuItemInstallmentTypes
            // 
            this.toolStripMenuItemInstallmentTypes.Name = "toolStripMenuItemInstallmentTypes";
            resources.ApplyResources(this.toolStripMenuItemInstallmentTypes, "toolStripMenuItemInstallmentTypes");
            this.toolStripMenuItemInstallmentTypes.Click += new System.EventHandler(this.toolStripMenuItemInstallmentTypes_Click);
            // 
            // toolStripSeparatorConfig2
            // 
            this.toolStripSeparatorConfig2.Name = "toolStripSeparatorConfig2";
            resources.ApplyResources(this.toolStripSeparatorConfig2, "toolStripSeparatorConfig2");
            // 
            // menuItemExchangeRate
            // 
            resources.ApplyResources(this.menuItemExchangeRate, "menuItemExchangeRate");
            this.menuItemExchangeRate.Name = "menuItemExchangeRate";
            this.menuItemExchangeRate.Click += new System.EventHandler(this.menuItemExchangeRate_Click);
            // 
            // currenciesToolStripMenuItem
            // 
            this.currenciesToolStripMenuItem.Image = global::OpenCBS.GUI.Properties.Resources.money;
            this.currenciesToolStripMenuItem.Name = "currenciesToolStripMenuItem";
            resources.ApplyResources(this.currenciesToolStripMenuItem, "currenciesToolStripMenuItem");
            this.currenciesToolStripMenuItem.Click += new System.EventHandler(this.currenciesToolStripMenuItem_Click);
            // 
            // toolStripSeparatorConfig3
            // 
            this.toolStripSeparatorConfig3.Name = "toolStripSeparatorConfig3";
            resources.ApplyResources(this.toolStripSeparatorConfig3, "toolStripSeparatorConfig3");
            // 
            // miAccountNumber
            // 
            this.miAccountNumber.Name = "miAccountNumber";
            resources.ApplyResources(this.miAccountNumber, "miAccountNumber");
            this.miAccountNumber.Click += new System.EventHandler(this.accountNumberToolStripMenuItem_Click);
            // 
            // miContractCode
            // 
            this.miContractCode.Name = "miContractCode";
            resources.ApplyResources(this.miContractCode, "miContractCode");
            this.miContractCode.Click += new System.EventHandler(this.miContractCode_Click);
            // 
            // collateralProductsToolStripMenuItem
            // 
            this.collateralProductsToolStripMenuItem.Name = "collateralProductsToolStripMenuItem";
            resources.ApplyResources(this.collateralProductsToolStripMenuItem, "collateralProductsToolStripMenuItem");
            this.collateralProductsToolStripMenuItem.Click += new System.EventHandler(this.collateralProductsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // miMessaging
            // 
            this.miMessaging.Name = "miMessaging";
            resources.ApplyResources(this.miMessaging, "miMessaging");
            this.miMessaging.Click += new System.EventHandler(this.miMessaging_Click);
            // 
            // mnuWindow
            // 
            this.mnuWindow.Name = "mnuWindow";
            resources.ApplyResources(this.mnuWindow, "mnuWindow");
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.userGuideToolStripMenuItem,
            this.contactMenuItem,
            this.aboutMenuItem,
            this.getHelpFromForumToolStripMenuItem,
            this.visitOpenCBScomToolStripMenuItem,
            this.helpToolStripSeparator});
            this.mnuHelp.Name = "mnuHelp";
            resources.ApplyResources(this.mnuHelp, "mnuHelp");
            // 
            // userGuideToolStripMenuItem
            // 
            this.userGuideToolStripMenuItem.Name = "userGuideToolStripMenuItem";
            resources.ApplyResources(this.userGuideToolStripMenuItem, "userGuideToolStripMenuItem");
            this.userGuideToolStripMenuItem.Click += new System.EventHandler(this.OpenUserGuid);
            // 
            // contactMenuItem
            // 
            this.contactMenuItem.Name = "contactMenuItem";
            resources.ApplyResources(this.contactMenuItem, "contactMenuItem");
            this.contactMenuItem.Click += new System.EventHandler(this.contactMenuItem_Click);
            // 
            // aboutMenuItem
            // 
            this.aboutMenuItem.Name = "aboutMenuItem";
            resources.ApplyResources(this.aboutMenuItem, "aboutMenuItem");
            this.aboutMenuItem.Click += new System.EventHandler(this.OnAboutMenuItemClick);
            // 
            // getHelpFromForumToolStripMenuItem
            // 
            this.getHelpFromForumToolStripMenuItem.Name = "getHelpFromForumToolStripMenuItem";
            resources.ApplyResources(this.getHelpFromForumToolStripMenuItem, "getHelpFromForumToolStripMenuItem");
            this.getHelpFromForumToolStripMenuItem.Click += new System.EventHandler(this.getHelpFromForumToolStripMenuItem_Click);
            // 
            // visitOpenCBScomToolStripMenuItem
            // 
            this.visitOpenCBScomToolStripMenuItem.Name = "visitOpenCBScomToolStripMenuItem";
            resources.ApplyResources(this.visitOpenCBScomToolStripMenuItem, "visitOpenCBScomToolStripMenuItem");
            this.visitOpenCBScomToolStripMenuItem.Click += new System.EventHandler(this.visitOpenCBScomToolStripMenuItem_Click);
            // 
            // helpToolStripSeparator
            // 
            this.helpToolStripSeparator.Name = "helpToolStripSeparator";
            resources.ApplyResources(this.helpToolStripSeparator, "helpToolStripSeparator");
            // 
            // mainMenu
            // 
            this.mainMenu.BackColor = System.Drawing.SystemColors.Control;
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSettings,
            this.mnuConfiguration,
            this.mnuSecurity,
            this.mnuProducts,
            this.mnuClients,
            this.mnuTeller,
            this._viewItem,
            this._modulesMenuItem,
            this.mnuPayroll,
            this.mnuAccounting,
            this.reportsToolStripMenuItem,
            this.mnuHelp,
            this.mnuWindow,
            this.logoutToolStripMenuItem});
            resources.ApplyResources(this.mainMenu, "mainMenu");
            this.mainMenu.MdiWindowListItem = this.mnuWindow;
            this.mainMenu.Name = "mainMenu";
            // 
            // mnuSettings
            // 
            this.mnuSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemSetting,
            this.menuItemDatabaseControlPanel,
            this.menuItemApplicationDate,
            this.languagesToolStripMenuItem});
            this.mnuSettings.Name = "mnuSettings";
            resources.ApplyResources(this.mnuSettings, "mnuSettings");
            // 
            // menuItemSetting
            // 
            this.menuItemSetting.Image = global::OpenCBS.GUI.Properties.Resources.cog;
            resources.ApplyResources(this.menuItemSetting, "menuItemSetting");
            this.menuItemSetting.Name = "menuItemSetting";
            this.menuItemSetting.Click += new System.EventHandler(this.menuItemSetting_Click);
            // 
            // menuItemDatabaseControlPanel
            // 
            this.menuItemDatabaseControlPanel.Image = global::OpenCBS.GUI.Properties.Resources.database_gear;
            this.menuItemDatabaseControlPanel.Name = "menuItemDatabaseControlPanel";
            resources.ApplyResources(this.menuItemDatabaseControlPanel, "menuItemDatabaseControlPanel");
            this.menuItemDatabaseControlPanel.Click += new System.EventHandler(this.menuItemBackupData_Click);
            // 
            // menuItemApplicationDate
            // 
            this.menuItemApplicationDate.Image = global::OpenCBS.GUI.Properties.Resources.calendar;
            resources.ApplyResources(this.menuItemApplicationDate, "menuItemApplicationDate");
            this.menuItemApplicationDate.Name = "menuItemApplicationDate";
            this.menuItemApplicationDate.Click += new System.EventHandler(this.OnChangeApplicationDateClick);
            // 
            // languagesToolStripMenuItem
            // 
            this.languagesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.frenchToolStripMenuItem,
            this.englishToolStripMenuItem,
            this.russianToolStripMenuItem,
            this.spanishToolStripMenuItem,
            this.portugueseToolStripMenuItem});
            this.languagesToolStripMenuItem.Name = "languagesToolStripMenuItem";
            resources.ApplyResources(this.languagesToolStripMenuItem, "languagesToolStripMenuItem");
            // 
            // frenchToolStripMenuItem
            // 
            this.frenchToolStripMenuItem.Image = global::OpenCBS.GUI.Properties.Resources.fr;
            resources.ApplyResources(this.frenchToolStripMenuItem, "frenchToolStripMenuItem");
            this.frenchToolStripMenuItem.Name = "frenchToolStripMenuItem";
            this.frenchToolStripMenuItem.Tag = "fr";
            this.frenchToolStripMenuItem.Click += new System.EventHandler(this.LanguageToolStripMenuItem_Click);
            // 
            // englishToolStripMenuItem
            // 
            this.englishToolStripMenuItem.Image = global::OpenCBS.GUI.Properties.Resources.gb;
            resources.ApplyResources(this.englishToolStripMenuItem, "englishToolStripMenuItem");
            this.englishToolStripMenuItem.Name = "englishToolStripMenuItem";
            this.englishToolStripMenuItem.Tag = "en-US";
            this.englishToolStripMenuItem.Click += new System.EventHandler(this.LanguageToolStripMenuItem_Click);
            // 
            // russianToolStripMenuItem
            // 
            this.russianToolStripMenuItem.Image = global::OpenCBS.GUI.Properties.Resources.ru;
            resources.ApplyResources(this.russianToolStripMenuItem, "russianToolStripMenuItem");
            this.russianToolStripMenuItem.Name = "russianToolStripMenuItem";
            this.russianToolStripMenuItem.Tag = "ru-RU";
            this.russianToolStripMenuItem.Click += new System.EventHandler(this.LanguageToolStripMenuItem_Click);
            // 
            // spanishToolStripMenuItem
            // 
            this.spanishToolStripMenuItem.Image = global::OpenCBS.GUI.Properties.Resources.es;
            resources.ApplyResources(this.spanishToolStripMenuItem, "spanishToolStripMenuItem");
            this.spanishToolStripMenuItem.Name = "spanishToolStripMenuItem";
            this.spanishToolStripMenuItem.Tag = "es-ES";
            this.spanishToolStripMenuItem.Click += new System.EventHandler(this.LanguageToolStripMenuItem_Click);
            // 
            // portugueseToolStripMenuItem
            // 
            this.portugueseToolStripMenuItem.Image = global::OpenCBS.GUI.Properties.Resources.pt;
            resources.ApplyResources(this.portugueseToolStripMenuItem, "portugueseToolStripMenuItem");
            this.portugueseToolStripMenuItem.Name = "portugueseToolStripMenuItem";
            this.portugueseToolStripMenuItem.Click += new System.EventHandler(this.LanguageToolStripMenuItem_Click);
            // 
            // mnuSecurity
            // 
            this.mnuSecurity.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rolesToolStripMenuItem,
            this.menuItemAddUser,
            this.miAuditTrail,
            this.changePasswordToolStripMenuItem});
            this.mnuSecurity.Name = "mnuSecurity";
            resources.ApplyResources(this.mnuSecurity, "mnuSecurity");
            // 
            // rolesToolStripMenuItem
            // 
            this.rolesToolStripMenuItem.Name = "rolesToolStripMenuItem";
            resources.ApplyResources(this.rolesToolStripMenuItem, "rolesToolStripMenuItem");
            this.rolesToolStripMenuItem.Click += new System.EventHandler(this.rolesToolStripMenuItem_Click);
            // 
            // menuItemAddUser
            // 
            this.menuItemAddUser.Image = global::OpenCBS.GUI.Properties.Resources.group;
            resources.ApplyResources(this.menuItemAddUser, "menuItemAddUser");
            this.menuItemAddUser.Name = "menuItemAddUser";
            this.menuItemAddUser.Click += new System.EventHandler(this.menuItemAddUser_Click);
            // 
            // miAuditTrail
            // 
            this.miAuditTrail.Name = "miAuditTrail";
            resources.ApplyResources(this.miAuditTrail, "miAuditTrail");
            this.miAuditTrail.Click += new System.EventHandler(this.eventsToolStripMenuItem_Click);
            // 
            // changePasswordToolStripMenuItem
            // 
            this.changePasswordToolStripMenuItem.Name = "changePasswordToolStripMenuItem";
            resources.ApplyResources(this.changePasswordToolStripMenuItem, "changePasswordToolStripMenuItem");
            this.changePasswordToolStripMenuItem.Click += new System.EventHandler(this.changePasswordToolStripMenuItem_Click);
            // 
            // mnuProducts
            // 
            this.mnuProducts.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuPackages,
            this.savingProductsToolStripMenuItem});
            this.mnuProducts.Name = "mnuProducts";
            resources.ApplyResources(this.mnuProducts, "mnuProducts");
            // 
            // mnuPackages
            // 
            this.mnuPackages.Image = global::OpenCBS.GUI.Properties.Resources.package;
            resources.ApplyResources(this.mnuPackages, "mnuPackages");
            this.mnuPackages.Name = "mnuPackages";
            this.mnuPackages.Click += new System.EventHandler(this.menuItemPackages_Click);
            // 
            // savingProductsToolStripMenuItem
            // 
            this.savingProductsToolStripMenuItem.Image = global::OpenCBS.GUI.Properties.Resources.package;
            this.savingProductsToolStripMenuItem.Name = "savingProductsToolStripMenuItem";
            resources.ApplyResources(this.savingProductsToolStripMenuItem, "savingProductsToolStripMenuItem");
            this.savingProductsToolStripMenuItem.Click += new System.EventHandler(this.savingProductsToolStripMenuItem_Click);
            // 
            // mnuTeller
            // 
            this.mnuTeller.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miVaultTransfer,
            this.miTellerHistory});
            this.mnuTeller.Name = "mnuTeller";
            resources.ApplyResources(this.mnuTeller, "mnuTeller");
            // 
            // miVaultTransfer
            // 
            this.miVaultTransfer.Name = "miVaultTransfer";
            resources.ApplyResources(this.miVaultTransfer, "miVaultTransfer");
            this.miVaultTransfer.Click += new System.EventHandler(this.miVaultTransfer_Click);
            // 
            // miTellerHistory
            // 
            this.miTellerHistory.Name = "miTellerHistory";
            resources.ApplyResources(this.miTellerHistory, "miTellerHistory");
            this.miTellerHistory.Click += new System.EventHandler(this.miTellerHistory_Click);
            // 
            // _viewItem
            // 
            this._viewItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._startPageItem,
            this._alertsItem,
            this._dashboardItem,
            this._searchItem});
            this._viewItem.Name = "_viewItem";
            resources.ApplyResources(this._viewItem, "_viewItem");
            // 
            // _startPageItem
            // 
            this._startPageItem.Name = "_startPageItem";
            resources.ApplyResources(this._startPageItem, "_startPageItem");
            // 
            // _alertsItem
            // 
            this._alertsItem.Name = "_alertsItem";
            resources.ApplyResources(this._alertsItem, "_alertsItem");
            // 
            // _dashboardItem
            // 
            this._dashboardItem.Name = "_dashboardItem";
            resources.ApplyResources(this._dashboardItem, "_dashboardItem");
            // 
            // _searchItem
            // 
            this._searchItem.Name = "_searchItem";
            resources.ApplyResources(this._searchItem, "_searchItem");
            // 
            // _modulesMenuItem
            // 
            this._modulesMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._aboutModulesMenuItem});
            this._modulesMenuItem.Name = "_modulesMenuItem";
            resources.ApplyResources(this._modulesMenuItem, "_modulesMenuItem");
            // 
            // _aboutModulesMenuItem
            // 
            this._aboutModulesMenuItem.Name = "_aboutModulesMenuItem";
            resources.ApplyResources(this._aboutModulesMenuItem, "_aboutModulesMenuItem");
            this._aboutModulesMenuItem.Click += new System.EventHandler(this._aboutModulesMenuItem_Click);
            // 
            // mnuPayroll
            // 
            this.mnuPayroll.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.payrollToolStripSeparator});
            this.mnuPayroll.Name = "mnuPayroll";
            resources.ApplyResources(this.mnuPayroll, "mnuPayroll");
            // 
            // payrollToolStripSeparator
            // 
            this.payrollToolStripSeparator.Name = "payrollToolStripSeparator";
            resources.ApplyResources(this.payrollToolStripSeparator, "payrollToolStripSeparator");
            // 
            // reportsToolStripMenuItem
            // 
            this.reportsToolStripMenuItem.Name = "reportsToolStripMenuItem";
            resources.ApplyResources(this.reportsToolStripMenuItem, "reportsToolStripMenuItem");
            // 
            // logoutToolStripMenuItem
            // 
            this.logoutToolStripMenuItem.Name = "logoutToolStripMenuItem";
            resources.ApplyResources(this.logoutToolStripMenuItem, "logoutToolStripMenuItem");
            this.logoutToolStripMenuItem.Click += new System.EventHandler(this.logoutToolStripMenuItem_Click);
            // 
            // mainStatusBar
            // 
            resources.ApplyResources(this.mainStatusBar, "mainStatusBar");
            this.mainStatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainStatusBarLblUpdateVersion,
            this.mainStatusBarLblUserName,
            this.mainStatusBarLblCashPosition,
            this.mainStatusBarLblDate,
            this.toolStripStatusLblBranchCode,
            this.toolStripStatusLblDB,
            this.mainStatusBarLabelAutoLogoutTime});
            this.mainStatusBar.Name = "mainStatusBar";
            this.mainStatusBar.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
            this.mainStatusBar.ShowItemToolTips = true;
            this.mainStatusBar.SizingGrip = false;
            // 
            // mainStatusBarLblUpdateVersion
            // 
            resources.ApplyResources(this.mainStatusBarLblUpdateVersion, "mainStatusBarLblUpdateVersion");
            this.mainStatusBarLblUpdateVersion.Name = "mainStatusBarLblUpdateVersion";
            this.mainStatusBarLblUpdateVersion.Spring = true;
            // 
            // mainStatusBarLblUserName
            // 
            this.mainStatusBarLblUserName.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.mainStatusBarLblUserName.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.mainStatusBarLblUserName.Image = global::OpenCBS.GUI.Properties.Resources.user_gray;
            this.mainStatusBarLblUserName.Name = "mainStatusBarLblUserName";
            resources.ApplyResources(this.mainStatusBarLblUserName, "mainStatusBarLblUserName");
            // 
            // mainStatusBarLblCashPosition
            // 
            this.mainStatusBarLblCashPosition.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.mainStatusBarLblCashPosition.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.mainStatusBarLblCashPosition.ForeColor = System.Drawing.Color.Red;
            this.mainStatusBarLblCashPosition.Name = "mainStatusBarLblCashPosition";
            resources.ApplyResources(this.mainStatusBarLblCashPosition, "mainStatusBarLblCashPosition");
            // 
            // mainStatusBarLblDate
            // 
            this.mainStatusBarLblDate.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.mainStatusBarLblDate.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.mainStatusBarLblDate.Image = global::OpenCBS.GUI.Properties.Resources.calendar;
            this.mainStatusBarLblDate.Name = "mainStatusBarLblDate";
            resources.ApplyResources(this.mainStatusBarLblDate, "mainStatusBarLblDate");
            // 
            // toolStripStatusLblBranchCode
            // 
            this.toolStripStatusLblBranchCode.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLblBranchCode.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.toolStripStatusLblBranchCode.Name = "toolStripStatusLblBranchCode";
            resources.ApplyResources(this.toolStripStatusLblBranchCode, "toolStripStatusLblBranchCode");
            // 
            // toolStripStatusLblDB
            // 
            this.toolStripStatusLblDB.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLblDB.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.toolStripStatusLblDB.Image = global::OpenCBS.GUI.Properties.Resources.database;
            this.toolStripStatusLblDB.Name = "toolStripStatusLblDB";
            resources.ApplyResources(this.toolStripStatusLblDB, "toolStripStatusLblDB");
            // 
            // mainStatusBarLabelAutoLogoutTime
            // 
            this.mainStatusBarLabelAutoLogoutTime.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.mainStatusBarLabelAutoLogoutTime.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.mainStatusBarLabelAutoLogoutTime.Image = global::OpenCBS.GUI.Properties.Resources.calendar;
            this.mainStatusBarLabelAutoLogoutTime.Name = "mainStatusBarLabelAutoLogoutTime";
            resources.ApplyResources(this.mainStatusBarLabelAutoLogoutTime, "mainStatusBarLabelAutoLogoutTime");
            // 
            // MainView
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.mainStatusBar);
            this.Controls.Add(this.mainMenu);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.mainMenu;
            this.Name = "MainView";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LotrasmicMainWindowForm_FormClosing);
            this.Load += new System.EventHandler(this.LotrasmicMainWindowForm_Load);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.mainStatusBar.ResumeLayout(false);
            this.mainStatusBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private ToolStripMenuItem newCorporateToolStripMenuItem;
        private ToolStripMenuItem mnuNewVillage;
        private ToolStripMenuItem miContractCode;
        private ToolStripMenuItem standardToolStripMenuItem;
        private ToolStripMenuItem currenciesToolStripMenuItem;
        private ToolStripMenuItem accountingRulesToolStripMenuItem;
        private NotifyIcon nIUpdateAvailable;
        private OpenFileDialog openCustomizableFileDialog;
        private ToolStripMenuItem trialBalanceToolStripMenuItem;
        private BrightIdeasSoftware.OLVColumn colAlerts_Address;
        private BrightIdeasSoftware.OLVColumn colAlerts_City;
        private BrightIdeasSoftware.OLVColumn colAlerts_Phone;
        private ToolStripMenuItem manualEntriesToolStripMenuItem;
        private ToolStripMenuItem branchesToolStripMenuItem;
        private ToolStripStatusLabel toolStripStatusLblDB;
        private BrightIdeasSoftware.OLVColumn colAlerts_BranchName;
        private ToolStripMenuItem mnuNewclosure;
        private System.ComponentModel.BackgroundWorker bwUserInformation;
        private ToolStripMenuItem fiscalYearToolStripMenuItem;
        private ToolStripMenuItem tellersToolStripMenuItem;
        private ToolStripMenuItem aboutMenuItem;
        private ToolStripMenuItem reportsToolStripMenuItem;
        private ToolStripMenuItem mnuSettings;
        private ToolStripMenuItem menuItemSetting;
        private ToolStripMenuItem menuItemDatabaseControlPanel;
        private ToolStripMenuItem menuItemApplicationDate;
        private ToolStripMenuItem mnuSecurity;
        private ToolStripMenuItem rolesToolStripMenuItem;
        private ToolStripMenuItem menuItemAddUser;
        private ToolStripMenuItem miAuditTrail;
        private ToolStripMenuItem mnuNewPerson;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem mnuSearchContract;
        private ToolStripMenuItem reasignToolStripMenuItem;
        private ToolStripMenuItem mnuProducts;
        private ToolStripMenuItem mnuPackages;
        private ToolStripMenuItem savingProductsToolStripMenuItem;
        private ToolStripMenuItem languagesToolStripMenuItem;
        private ToolStripMenuItem frenchToolStripMenuItem;
        private ToolStripMenuItem englishToolStripMenuItem;
        private ToolStripMenuItem russianToolStripMenuItem;
        private ToolStripMenuItem spanishToolStripMenuItem;
        private ToolStripMenuItem portugueseToolStripMenuItem;
        private ToolStripMenuItem changePasswordToolStripMenuItem;
        private ToolStripMenuItem _modulesMenuItem;
        private ToolStripMenuItem _aboutModulesMenuItem;
        private ToolStripMenuItem contactMenuItem;
        private ToolStripMenuItem mnuChartOfAccounts;
        private ToolStripMenuItem userGuideToolStripMenuItem;
        private ToolStripMenuItem getHelpFromForumToolStripMenuItem;
        private ToolStripMenuItem visitOpenCBScomToolStripMenuItem;
        private ToolStripMenuItem collateralProductsToolStripMenuItem;
        private ToolStripMenuItem _viewItem;
        private ToolStripMenuItem _startPageItem;
        private ToolStripMenuItem _alertsItem;
        private ToolStripMenuItem _dashboardItem;
        private ToolStripMenuItem loanPurposeToolStripMenuItem;
        private ToolStripMenuItem _searchItem;
        private ToolStripMenuItem miAccountNumber;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem miMessaging;
        private ToolStripSeparator helpToolStripSeparator;
        private ToolStripMenuItem mnuPayroll;
        private ToolStripSeparator payrollToolStripSeparator;
        private ToolStripMenuItem logoutToolStripMenuItem;
        private ToolStripStatusLabel mainStatusBarLabelAutoLogoutTime;
        private ToolStripStatusLabel mainStatusBarLblCashPosition;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem mnuTeller;
        private ToolStripMenuItem miVaultTransfer;
        private ToolStripMenuItem miTellerHistory;
    }
}
