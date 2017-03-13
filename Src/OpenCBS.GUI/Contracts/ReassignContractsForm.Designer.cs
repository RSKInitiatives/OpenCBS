using OpenCBS.GUI.UserControl;

namespace OpenCBS.GUI.Contracts
{
    partial class ReassignContractsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReassignContractsForm));
            this.filterLabel = new System.Windows.Forms.Label();
            this.filterTextbox = new System.Windows.Forms.TextBox();
            this.selectAllCheckbox = new System.Windows.Forms.CheckBox();
            this.assignButton = new System.Windows.Forms.Button();
            this.toLabel = new System.Windows.Forms.Label();
            this.fromLabel = new System.Windows.Forms.Label();
            this.toCombobox = new System.Windows.Forms.ComboBox();
            this.fromCombobox = new System.Windows.Forms.ComboBox();
            this.optionsPanel = new System.Windows.Forms.Panel();
            this._incudeDeletedUsersCheckBox = new System.Windows.Forms.CheckBox();
            this.listPanel = new System.Windows.Forms.Panel();
            this.contractsObjectListView = new BrightIdeasSoftware.FastObjectListView();
            this.clientLastNameColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.clientFirstNameColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.clientFatherNameColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.districtNameColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.contractCodeColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.contractStatusColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.amountColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olbColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.startDateColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.closeDateColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.numberOfInstallmentsColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.interestRateColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.loanProductNameColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.installmentTypeColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.optionsPanel.SuspendLayout();
            this.listPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.contractsObjectListView)).BeginInit();
            this.SuspendLayout();
            // 
            // filterLabel
            // 
            resources.ApplyResources(this.filterLabel, "filterLabel");
            this.filterLabel.Name = "filterLabel";
            // 
            // filterTextbox
            // 
            resources.ApplyResources(this.filterTextbox, "filterTextbox");
            this.filterTextbox.Name = "filterTextbox";
            // 
            // selectAllCheckbox
            // 
            resources.ApplyResources(this.selectAllCheckbox, "selectAllCheckbox");
            this.selectAllCheckbox.Name = "selectAllCheckbox";
            // 
            // assignButton
            // 
            resources.ApplyResources(this.assignButton, "assignButton");
            this.assignButton.Name = "assignButton";
            // 
            // toLabel
            // 
            resources.ApplyResources(this.toLabel, "toLabel");
            this.toLabel.Name = "toLabel";
            // 
            // fromLabel
            // 
            resources.ApplyResources(this.fromLabel, "fromLabel");
            this.fromLabel.Name = "fromLabel";
            // 
            // toCombobox
            // 
            this.toCombobox.BackColor = System.Drawing.SystemColors.Window;
            this.toCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toCombobox.FormattingEnabled = true;
            resources.ApplyResources(this.toCombobox, "toCombobox");
            this.toCombobox.Name = "toCombobox";
            // 
            // fromCombobox
            // 
            this.fromCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fromCombobox.FormattingEnabled = true;
            resources.ApplyResources(this.fromCombobox, "fromCombobox");
            this.fromCombobox.Name = "fromCombobox";
            // 
            // optionsPanel
            // 
            this.optionsPanel.Controls.Add(this._incudeDeletedUsersCheckBox);
            this.optionsPanel.Controls.Add(this.filterTextbox);
            this.optionsPanel.Controls.Add(this.fromCombobox);
            this.optionsPanel.Controls.Add(this.filterLabel);
            this.optionsPanel.Controls.Add(this.fromLabel);
            this.optionsPanel.Controls.Add(this.toLabel);
            this.optionsPanel.Controls.Add(this.toCombobox);
            this.optionsPanel.Controls.Add(this.selectAllCheckbox);
            this.optionsPanel.Controls.Add(this.assignButton);
            resources.ApplyResources(this.optionsPanel, "optionsPanel");
            this.optionsPanel.Name = "optionsPanel";
            // 
            // _incudeDeletedUsersCheckBox
            // 
            resources.ApplyResources(this._incudeDeletedUsersCheckBox, "_incudeDeletedUsersCheckBox");
            this._incudeDeletedUsersCheckBox.Checked = true;
            this._incudeDeletedUsersCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this._incudeDeletedUsersCheckBox.Name = "_incudeDeletedUsersCheckBox";
            this._incudeDeletedUsersCheckBox.UseVisualStyleBackColor = true;
            // 
            // listPanel
            // 
            this.listPanel.Controls.Add(this.contractsObjectListView);
            resources.ApplyResources(this.listPanel, "listPanel");
            this.listPanel.Name = "listPanel";
            // 
            // contractsObjectListView
            // 
            this.contractsObjectListView.AllColumns.Add(this.clientLastNameColumn);
            this.contractsObjectListView.AllColumns.Add(this.clientFirstNameColumn);
            this.contractsObjectListView.AllColumns.Add(this.clientFatherNameColumn);
            this.contractsObjectListView.AllColumns.Add(this.districtNameColumn);
            this.contractsObjectListView.AllColumns.Add(this.contractCodeColumn);
            this.contractsObjectListView.AllColumns.Add(this.contractStatusColumn);
            this.contractsObjectListView.AllColumns.Add(this.amountColumn);
            this.contractsObjectListView.AllColumns.Add(this.olbColumn);
            this.contractsObjectListView.AllColumns.Add(this.startDateColumn);
            this.contractsObjectListView.AllColumns.Add(this.closeDateColumn);
            this.contractsObjectListView.AllColumns.Add(this.numberOfInstallmentsColumn);
            this.contractsObjectListView.AllColumns.Add(this.interestRateColumn);
            this.contractsObjectListView.AllColumns.Add(this.loanProductNameColumn);
            this.contractsObjectListView.AllColumns.Add(this.installmentTypeColumn);
            this.contractsObjectListView.CheckBoxes = false;
            this.contractsObjectListView.CheckedAspectName = "CanReassign";
            this.contractsObjectListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clientLastNameColumn,
            this.clientFirstNameColumn,
            this.clientFatherNameColumn,
            this.districtNameColumn,
            this.contractCodeColumn,
            this.contractStatusColumn,
            this.amountColumn,
            this.olbColumn,
            this.startDateColumn,
            this.closeDateColumn,
            this.numberOfInstallmentsColumn,
            this.interestRateColumn,
            this.loanProductNameColumn,
            this.installmentTypeColumn});
            resources.ApplyResources(this.contractsObjectListView, "contractsObjectListView");
            this.contractsObjectListView.GridLines = true;
            this.contractsObjectListView.HeaderWordWrap = true;
            this.contractsObjectListView.Name = "contractsObjectListView";
            this.contractsObjectListView.ShowGroups = false;
            this.contractsObjectListView.ShowImagesOnSubItems = true;
            this.contractsObjectListView.UseCompatibleStateImageBehavior = false;
            this.contractsObjectListView.UseFiltering = true;
            this.contractsObjectListView.View = System.Windows.Forms.View.Details;
            this.contractsObjectListView.VirtualMode = true;
            // 
            // clientLastNameColumn
            // 
            this.clientLastNameColumn.AspectName = "ClientLastName";
            resources.ApplyResources(this.clientLastNameColumn, "clientLastNameColumn");
            // 
            // clientFirstNameColumn
            // 
            this.clientFirstNameColumn.AspectName = "ClientFirstName";
            resources.ApplyResources(this.clientFirstNameColumn, "clientFirstNameColumn");
            // 
            // clientFatherNameColumn
            // 
            this.clientFatherNameColumn.AspectName = "ClientFatherName";
            resources.ApplyResources(this.clientFatherNameColumn, "clientFatherNameColumn");
            // 
            // districtNameColumn
            // 
            this.districtNameColumn.AspectName = "DistrictName";
            resources.ApplyResources(this.districtNameColumn, "districtNameColumn");
            // 
            // contractCodeColumn
            // 
            this.contractCodeColumn.AspectName = "ContractCode";
            resources.ApplyResources(this.contractCodeColumn, "contractCodeColumn");
            // 
            // contractStatusColumn
            // 
            this.contractStatusColumn.AspectName = "StatusCode";
            this.contractStatusColumn.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            resources.ApplyResources(this.contractStatusColumn, "contractStatusColumn");
            // 
            // amountColumn
            // 
            this.amountColumn.AspectName = "Amount";
            this.amountColumn.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            resources.ApplyResources(this.amountColumn, "amountColumn");
            // 
            // olbColumn
            // 
            this.olbColumn.AspectName = "Olb";
            this.olbColumn.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            resources.ApplyResources(this.olbColumn, "olbColumn");
            // 
            // startDateColumn
            // 
            this.startDateColumn.AspectName = "StartDate";
            this.startDateColumn.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            resources.ApplyResources(this.startDateColumn, "startDateColumn");
            // 
            // closeDateColumn
            // 
            this.closeDateColumn.AspectName = "CloseDate";
            this.closeDateColumn.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            resources.ApplyResources(this.closeDateColumn, "closeDateColumn");
            // 
            // numberOfInstallmentsColumn
            // 
            this.numberOfInstallmentsColumn.AspectName = "NumberOfInstallments";
            this.numberOfInstallmentsColumn.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            resources.ApplyResources(this.numberOfInstallmentsColumn, "numberOfInstallmentsColumn");
            // 
            // interestRateColumn
            // 
            this.interestRateColumn.AspectName = "InterestRate";
            this.interestRateColumn.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            resources.ApplyResources(this.interestRateColumn, "interestRateColumn");
            // 
            // loanProductNameColumn
            // 
            this.loanProductNameColumn.AspectName = "LoanProductName";
            resources.ApplyResources(this.loanProductNameColumn, "loanProductNameColumn");
            // 
            // installmentTypeColumn
            // 
            this.installmentTypeColumn.AspectName = "InstallmentType";
            resources.ApplyResources(this.installmentTypeColumn, "installmentTypeColumn");
            // 
            // ReassignContractsForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listPanel);
            this.Controls.Add(this.optionsPanel);
            this.Name = "ReassignContractsForm";
            this.Controls.SetChildIndex(this.optionsPanel, 0);
            this.Controls.SetChildIndex(this.listPanel, 0);
            this.optionsPanel.ResumeLayout(false);
            this.optionsPanel.PerformLayout();
            this.listPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.contractsObjectListView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button assignButton;
        private System.Windows.Forms.Label toLabel;
        private System.Windows.Forms.Label fromLabel;
        private System.Windows.Forms.ComboBox toCombobox;
        private System.Windows.Forms.ComboBox fromCombobox;
        private System.Windows.Forms.CheckBox selectAllCheckbox;
        private System.Windows.Forms.Label filterLabel;
        private System.Windows.Forms.TextBox filterTextbox;
        private System.Windows.Forms.Panel optionsPanel;
        private System.Windows.Forms.Panel listPanel;
        private BrightIdeasSoftware.FastObjectListView contractsObjectListView;
        private BrightIdeasSoftware.OLVColumn contractCodeColumn;
        private BrightIdeasSoftware.OLVColumn clientFirstNameColumn;
        private BrightIdeasSoftware.OLVColumn clientLastNameColumn;
        private BrightIdeasSoftware.OLVColumn contractStatusColumn;
        private BrightIdeasSoftware.OLVColumn olbColumn;
        private BrightIdeasSoftware.OLVColumn amountColumn;
        private BrightIdeasSoftware.OLVColumn clientFatherNameColumn;
        private BrightIdeasSoftware.OLVColumn startDateColumn;
        private BrightIdeasSoftware.OLVColumn closeDateColumn;
        private BrightIdeasSoftware.OLVColumn numberOfInstallmentsColumn;
        private BrightIdeasSoftware.OLVColumn loanProductNameColumn;
        private BrightIdeasSoftware.OLVColumn installmentTypeColumn;
        private BrightIdeasSoftware.OLVColumn districtNameColumn;
        private BrightIdeasSoftware.OLVColumn interestRateColumn;
        private System.Windows.Forms.CheckBox _incudeDeletedUsersCheckBox;

    }
}