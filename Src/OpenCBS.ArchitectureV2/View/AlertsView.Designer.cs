namespace OpenCBS.ArchitectureV2.View
{
    partial class AlertsView
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AlertsView));
            this._alertsListView = new BrightIdeasSoftware.FastObjectListView();
            this._clientNameColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.clientStatusColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this._phoneColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this._addressColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this._cityColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this._branchColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this._contractCodeColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this._statusColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this._dateColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this._lateDaysColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this._amountColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this._loanOfficerColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this._toolStrip = new System.Windows.Forms.ToolStrip();
            this._reloadButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this._clientsDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this._newClientsItem = new System.Windows.Forms.ToolStripMenuItem();
            this._updatedClientsItem = new System.Windows.Forms.ToolStripMenuItem();
            this._loansDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this._performingLoansItemToday = new System.Windows.Forms.ToolStripMenuItem();
            this._performingLoansItemAll = new System.Windows.Forms.ToolStripMenuItem();
            this._lateLoansItem = new System.Windows.Forms.ToolStripMenuItem();
            this._pendingLoansItem = new System.Windows.Forms.ToolStripMenuItem();
            this._validatedLoansItem = new System.Windows.Forms.ToolStripMenuItem();
            this._postponedLoansItem = new System.Windows.Forms.ToolStripMenuItem();
            this._savingsDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this._pendingSavingsItem = new System.Windows.Forms.ToolStripMenuItem();
            this._overdraftSavingsItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this._searchTextBox = new System.Windows.Forms.ToolStripTextBox();
            this._clearSearchButton = new System.Windows.Forms.ToolStripButton();
            this._alertTabControl = new System.Windows.Forms.TabControl();
            this.loansAndAccountsTabPage = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.miniToolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            ((System.ComponentModel.ISupportInitialize)(this._alertsListView)).BeginInit();
            this._toolStrip.SuspendLayout();
            this._alertTabControl.SuspendLayout();
            this.loansAndAccountsTabPage.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _alertsListView
            // 
            this._alertsListView.AllColumns.Add(this._clientNameColumn);
            this._alertsListView.AllColumns.Add(this.clientStatusColumn);
            this._alertsListView.AllColumns.Add(this._phoneColumn);
            this._alertsListView.AllColumns.Add(this._addressColumn);
            this._alertsListView.AllColumns.Add(this._cityColumn);
            this._alertsListView.AllColumns.Add(this._branchColumn);
            this._alertsListView.AllColumns.Add(this._contractCodeColumn);
            this._alertsListView.AllColumns.Add(this._statusColumn);
            this._alertsListView.AllColumns.Add(this._dateColumn);
            this._alertsListView.AllColumns.Add(this._lateDaysColumn);
            this._alertsListView.AllColumns.Add(this._amountColumn);
            this._alertsListView.AllColumns.Add(this._loanOfficerColumn);
            this._alertsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this._clientNameColumn,
            this.clientStatusColumn,
            this._phoneColumn,
            this._branchColumn,
            this._contractCodeColumn,
            this._statusColumn,
            this._dateColumn,
            this._lateDaysColumn,
            this._amountColumn,
            this._loanOfficerColumn});
            resources.ApplyResources(this._alertsListView, "_alertsListView");
            this._alertsListView.FullRowSelect = true;
            this._alertsListView.GridLines = true;
            this._alertsListView.MultiSelect = false;
            this._alertsListView.Name = "_alertsListView";
            this._alertsListView.ShowGroups = false;
            this._alertsListView.UseCompatibleStateImageBehavior = false;
            this._alertsListView.View = System.Windows.Forms.View.Details;
            this._alertsListView.VirtualMode = true;
            // 
            // _clientNameColumn
            // 
            this._clientNameColumn.AspectName = "ClientName";
            resources.ApplyResources(this._clientNameColumn, "_clientNameColumn");
            // 
            // clientStatusColumn
            // 
            this.clientStatusColumn.AspectName = "ClientStatus";
            resources.ApplyResources(this.clientStatusColumn, "clientStatusColumn");
            // 
            // _phoneColumn
            // 
            this._phoneColumn.AspectName = "Phone";
            resources.ApplyResources(this._phoneColumn, "_phoneColumn");
            // 
            // _addressColumn
            // 
            this._addressColumn.AspectName = "Address";
            resources.ApplyResources(this._addressColumn, "_addressColumn");
            this._addressColumn.IsVisible = false;
            // 
            // _cityColumn
            // 
            this._cityColumn.AspectName = "City";
            resources.ApplyResources(this._cityColumn, "_cityColumn");
            this._cityColumn.IsVisible = false;
            // 
            // _branchColumn
            // 
            this._branchColumn.AspectName = "BranchName";
            resources.ApplyResources(this._branchColumn, "_branchColumn");
            // 
            // _contractCodeColumn
            // 
            this._contractCodeColumn.AspectName = "ContractCode";
            resources.ApplyResources(this._contractCodeColumn, "_contractCodeColumn");
            // 
            // _statusColumn
            // 
            this._statusColumn.AspectName = "Status";
            this._statusColumn.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            resources.ApplyResources(this._statusColumn, "_statusColumn");
            // 
            // _dateColumn
            // 
            this._dateColumn.AspectName = "Date";
            this._dateColumn.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            resources.ApplyResources(this._dateColumn, "_dateColumn");
            // 
            // _lateDaysColumn
            // 
            this._lateDaysColumn.AspectName = "LateDays";
            this._lateDaysColumn.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            resources.ApplyResources(this._lateDaysColumn, "_lateDaysColumn");
            // 
            // _amountColumn
            // 
            this._amountColumn.AspectName = "Amount";
            this._amountColumn.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            resources.ApplyResources(this._amountColumn, "_amountColumn");
            // 
            // _loanOfficerColumn
            // 
            this._loanOfficerColumn.AspectName = "LoanOfficer";
            resources.ApplyResources(this._loanOfficerColumn, "_loanOfficerColumn");
            // 
            // _toolStrip
            // 
            this._toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._reloadButton,
            this.toolStripSeparator2,
            this._clientsDropDownButton,
            this._loansDropDownButton,
            this._savingsDropDownButton,
            this.toolStripSeparator3,
            this._searchTextBox,
            this._clearSearchButton});
            resources.ApplyResources(this._toolStrip, "_toolStrip");
            this._toolStrip.Name = "_toolStrip";
            // 
            // _reloadButton
            // 
            this._reloadButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this._reloadButton, "_reloadButton");
            this._reloadButton.Name = "_reloadButton";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // _clientsDropDownButton
            // 
            this._clientsDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._clientsDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._newClientsItem,
            this._updatedClientsItem});
            resources.ApplyResources(this._clientsDropDownButton, "_clientsDropDownButton");
            this._clientsDropDownButton.Name = "_clientsDropDownButton";
            // 
            // _newClientsItem
            // 
            this._newClientsItem.Name = "_newClientsItem";
            resources.ApplyResources(this._newClientsItem, "_newClientsItem");
            // 
            // _updatedClientsItem
            // 
            this._updatedClientsItem.Name = "_updatedClientsItem";
            resources.ApplyResources(this._updatedClientsItem, "_updatedClientsItem");
            // 
            // _loansDropDownButton
            // 
            this._loansDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._loansDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._performingLoansItemToday,
            this._performingLoansItemAll,
            this._lateLoansItem,
            this._pendingLoansItem,
            this._validatedLoansItem,
            this._postponedLoansItem});
            resources.ApplyResources(this._loansDropDownButton, "_loansDropDownButton");
            this._loansDropDownButton.Name = "_loansDropDownButton";
            // 
            // _performingLoansItemToday
            // 
            this._performingLoansItemToday.Name = "_performingLoansItemToday";
            resources.ApplyResources(this._performingLoansItemToday, "_performingLoansItemToday");
            // 
            // _performingLoansItemAll
            // 
            this._performingLoansItemAll.Name = "_performingLoansItemAll";
            resources.ApplyResources(this._performingLoansItemAll, "_performingLoansItemAll");
            // 
            // _lateLoansItem
            // 
            this._lateLoansItem.Name = "_lateLoansItem";
            resources.ApplyResources(this._lateLoansItem, "_lateLoansItem");
            // 
            // _pendingLoansItem
            // 
            this._pendingLoansItem.Name = "_pendingLoansItem";
            resources.ApplyResources(this._pendingLoansItem, "_pendingLoansItem");
            // 
            // _validatedLoansItem
            // 
            this._validatedLoansItem.Name = "_validatedLoansItem";
            resources.ApplyResources(this._validatedLoansItem, "_validatedLoansItem");
            // 
            // _postponedLoansItem
            // 
            this._postponedLoansItem.Name = "_postponedLoansItem";
            resources.ApplyResources(this._postponedLoansItem, "_postponedLoansItem");
            // 
            // _savingsDropDownButton
            // 
            this._savingsDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._savingsDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._pendingSavingsItem,
            this._overdraftSavingsItem});
            resources.ApplyResources(this._savingsDropDownButton, "_savingsDropDownButton");
            this._savingsDropDownButton.Name = "_savingsDropDownButton";
            // 
            // _pendingSavingsItem
            // 
            this._pendingSavingsItem.Name = "_pendingSavingsItem";
            resources.ApplyResources(this._pendingSavingsItem, "_pendingSavingsItem");
            // 
            // _overdraftSavingsItem
            // 
            this._overdraftSavingsItem.Name = "_overdraftSavingsItem";
            resources.ApplyResources(this._overdraftSavingsItem, "_overdraftSavingsItem");
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // _searchTextBox
            // 
            this._searchTextBox.Name = "_searchTextBox";
            resources.ApplyResources(this._searchTextBox, "_searchTextBox");
            // 
            // _clearSearchButton
            // 
            this._clearSearchButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this._clearSearchButton, "_clearSearchButton");
            this._clearSearchButton.Name = "_clearSearchButton";
            // 
            // _alertTabControl
            // 
            this._alertTabControl.Controls.Add(this.loansAndAccountsTabPage);
            resources.ApplyResources(this._alertTabControl, "_alertTabControl");
            this._alertTabControl.Name = "_alertTabControl";
            this._alertTabControl.SelectedIndex = 0;
            // 
            // loansAndAccountsTabPage
            // 
            this.loansAndAccountsTabPage.Controls.Add(this.panel1);
            this.loansAndAccountsTabPage.Controls.Add(this._toolStrip);
            resources.ApplyResources(this.loansAndAccountsTabPage, "loansAndAccountsTabPage");
            this.loansAndAccountsTabPage.Name = "loansAndAccountsTabPage";
            this.loansAndAccountsTabPage.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this._alertsListView);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // miniToolStrip
            // 
            this.miniToolStrip.CanOverflow = false;
            resources.ApplyResources(this.miniToolStrip, "miniToolStrip");
            this.miniToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.miniToolStrip.Name = "miniToolStrip";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
            // 
            // AlertsView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._alertTabControl);
            this.Name = "AlertsView";
            this.Load += new System.EventHandler(this.AlertsView_Load);
            ((System.ComponentModel.ISupportInitialize)(this._alertsListView)).EndInit();
            this._toolStrip.ResumeLayout(false);
            this._toolStrip.PerformLayout();
            this._alertTabControl.ResumeLayout(false);
            this.loansAndAccountsTabPage.ResumeLayout(false);
            this.loansAndAccountsTabPage.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private BrightIdeasSoftware.FastObjectListView _alertsListView;
        private BrightIdeasSoftware.OLVColumn _contractCodeColumn;
        private BrightIdeasSoftware.OLVColumn _statusColumn;
        private BrightIdeasSoftware.OLVColumn _clientNameColumn;
        private BrightIdeasSoftware.OLVColumn _loanOfficerColumn;
        private BrightIdeasSoftware.OLVColumn _dateColumn;
        private BrightIdeasSoftware.OLVColumn _amountColumn;
        private BrightIdeasSoftware.OLVColumn _addressColumn;
        private BrightIdeasSoftware.OLVColumn _cityColumn;
        private BrightIdeasSoftware.OLVColumn _phoneColumn;
        private BrightIdeasSoftware.OLVColumn _branchColumn;
        private BrightIdeasSoftware.OLVColumn _lateDaysColumn;
        private System.Windows.Forms.ToolStrip _toolStrip;
        private System.Windows.Forms.ToolStripDropDownButton _loansDropDownButton;
        private System.Windows.Forms.ToolStripMenuItem _performingLoansItemToday;
        private System.Windows.Forms.ToolStripMenuItem _lateLoansItem;
        private System.Windows.Forms.ToolStripMenuItem _pendingLoansItem;
        private System.Windows.Forms.ToolStripMenuItem _validatedLoansItem;
        private System.Windows.Forms.ToolStripMenuItem _postponedLoansItem;
        private System.Windows.Forms.ToolStripDropDownButton _savingsDropDownButton;
        private System.Windows.Forms.ToolStripMenuItem _pendingSavingsItem;
        private System.Windows.Forms.ToolStripMenuItem _overdraftSavingsItem;
        private System.Windows.Forms.ToolStripTextBox _searchTextBox;
        private System.Windows.Forms.ToolStripButton _reloadButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton _clearSearchButton;
        private System.Windows.Forms.ToolStripMenuItem _performingLoansItemAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.TabControl _alertTabControl;
        private System.Windows.Forms.TabPage loansAndAccountsTabPage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip miniToolStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripDropDownButton _clientsDropDownButton;
        private System.Windows.Forms.ToolStripMenuItem _newClientsItem;
        private System.Windows.Forms.ToolStripMenuItem _updatedClientsItem;
        private BrightIdeasSoftware.OLVColumn clientStatusColumn;
    }
}