using OpenCBS.GUI.UserControl;
using OpenCBS.Shared.Settings;

namespace OpenCBS.GUI.Contracts
{
    partial class VillageAddLoanForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VillageAddLoanForm));
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.tbAmount = new System.Windows.Forms.TextBox();
            this.tbInterest = new System.Windows.Forms.TextBox();
            this.udGracePeriod = new System.Windows.Forms.NumericUpDown();
            this.udInstallments = new System.Windows.Forms.NumericUpDown();
            this.cbLoanOfficer = new System.Windows.Forms.ComboBox();
            this.tbEntryFee = new System.Windows.Forms.TextBox();
            this.cbFundingLine = new System.Windows.Forms.ComboBox();
            this.cbDonor = new System.Windows.Forms.ComboBox();
            this.lvMembers = new OpenCBS.GUI.UserControl.ListViewEx();
            this.chName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chPassport = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chAmount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chCurrency = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chInterest = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chGracePeriod = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chInstallments = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chLoanOfficer = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chCreationDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chFundingLine = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chAvailableFunds = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chCompulsorySavings = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chCompulsoryPercentage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cbCompulsorySavings = new System.Windows.Forms.ComboBox();
            this.udCompulsoryPercentage = new System.Windows.Forms.NumericUpDown();
            this.dtCreationDate = new System.Windows.Forms.DateTimePicker();
            this.pnlButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udGracePeriod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udInstallments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udCompulsoryPercentage)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlButtons
            // 
            this.pnlButtons.Controls.Add(this.btnCancel);
            this.pnlButtons.Controls.Add(this.btnSave);
            resources.ApplyResources(this.pnlButtons, "pnlButtons");
            this.pnlButtons.Name = "pnlButtons";
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tbAmount
            // 
            resources.ApplyResources(this.tbAmount, "tbAmount");
            this.tbAmount.Name = "tbAmount";
            // 
            // tbInterest
            // 
            resources.ApplyResources(this.tbInterest, "tbInterest");
            this.tbInterest.Name = "tbInterest";
            // 
            // udGracePeriod
            // 
            resources.ApplyResources(this.udGracePeriod, "udGracePeriod");
            this.udGracePeriod.Name = "udGracePeriod";
            // 
            // udInstallments
            // 
            resources.ApplyResources(this.udInstallments, "udInstallments");
            this.udInstallments.Name = "udInstallments";
            // 
            // cbLoanOfficer
            // 
            this.cbLoanOfficer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLoanOfficer.FormattingEnabled = true;
            resources.ApplyResources(this.cbLoanOfficer, "cbLoanOfficer");
            this.cbLoanOfficer.Name = "cbLoanOfficer";
            // 
            // tbEntryFee
            // 
            resources.ApplyResources(this.tbEntryFee, "tbEntryFee");
            this.tbEntryFee.Name = "tbEntryFee";
            // 
            // cbFundingLine
            // 
            this.cbFundingLine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFundingLine.FormattingEnabled = true;
            resources.ApplyResources(this.cbFundingLine, "cbFundingLine");
            this.cbFundingLine.Name = "cbFundingLine";
            // 
            // cbDonor
            // 
            this.cbDonor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDonor.FormattingEnabled = true;
            resources.ApplyResources(this.cbDonor, "cbDonor");
            this.cbDonor.Name = "cbDonor";
            // 
            // lvMembers
            // 
            this.lvMembers.CheckBoxes = true;
            this.lvMembers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chName,
            this.chPassport,
            this.chAmount,
            this.chCurrency,
            this.chInterest,
            this.chGracePeriod,
            this.chInstallments,
            this.chLoanOfficer,
            this.chCreationDate,
            this.chFundingLine,
            this.chAvailableFunds,
            this.chCompulsorySavings,
            this.chCompulsoryPercentage});
            resources.ApplyResources(this.lvMembers, "lvMembers");
            this.lvMembers.DoubleClickActivation = false;
            this.lvMembers.FullRowSelect = true;
            this.lvMembers.GridLines = true;
            this.lvMembers.Name = "lvMembers";
            this.lvMembers.UseCompatibleStateImageBehavior = false;
            this.lvMembers.View = System.Windows.Forms.View.Details;
            this.lvMembers.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvMembers_ItemCheck);
            this.lvMembers.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvMembers_ItemChecked);
            this.lvMembers.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvMembers_MouseDown);
            // 
            // chName
            // 
            resources.ApplyResources(this.chName, "chName");
            // 
            // chPassport
            // 
            resources.ApplyResources(this.chPassport, "chPassport");
            // 
            // chAmount
            // 
            resources.ApplyResources(this.chAmount, "chAmount");
            // 
            // chCurrency
            // 
            resources.ApplyResources(this.chCurrency, "chCurrency");
            // 
            // chInterest
            // 
            resources.ApplyResources(this.chInterest, "chInterest");
            // 
            // chGracePeriod
            // 
            resources.ApplyResources(this.chGracePeriod, "chGracePeriod");
            // 
            // chInstallments
            // 
            resources.ApplyResources(this.chInstallments, "chInstallments");
            // 
            // chLoanOfficer
            // 
            resources.ApplyResources(this.chLoanOfficer, "chLoanOfficer");
            // 
            // chCreationDate
            // 
            resources.ApplyResources(this.chCreationDate, "chCreationDate");
            // 
            // chFundingLine
            // 
            resources.ApplyResources(this.chFundingLine, "chFundingLine");
            // 
            // chAvailableFunds
            // 
            resources.ApplyResources(this.chAvailableFunds, "chAvailableFunds");
            // 
            // chCompulsorySavings
            // 
            resources.ApplyResources(this.chCompulsorySavings, "chCompulsorySavings");
            // 
            // chCompulsoryPercentage
            // 
            resources.ApplyResources(this.chCompulsoryPercentage, "chCompulsoryPercentage");
            // 
            // cbCompulsorySavings
            // 
            this.cbCompulsorySavings.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCompulsorySavings.FormattingEnabled = true;
            resources.ApplyResources(this.cbCompulsorySavings, "cbCompulsorySavings");
            this.cbCompulsorySavings.Name = "cbCompulsorySavings";
            // 
            // udCompulsoryPercentage
            // 
            resources.ApplyResources(this.udCompulsoryPercentage, "udCompulsoryPercentage");
            this.udCompulsoryPercentage.Name = "udCompulsoryPercentage";
            // 
            // dtCreationDate
            // 
            this.dtCreationDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            resources.ApplyResources(this.dtCreationDate, "dtCreationDate");
            this.dtCreationDate.Name = "dtCreationDate";
            this.dtCreationDate.ValueChanged += new System.EventHandler(this.dtCreationDate_ValueChanged);
            // 
            // VillageAddLoanForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dtCreationDate);
            this.Controls.Add(this.udCompulsoryPercentage);
            this.Controls.Add(this.cbCompulsorySavings);
            this.Controls.Add(this.cbDonor);
            this.Controls.Add(this.cbFundingLine);
            this.Controls.Add(this.tbEntryFee);
            this.Controls.Add(this.cbLoanOfficer);
            this.Controls.Add(this.udInstallments);
            this.Controls.Add(this.udGracePeriod);
            this.Controls.Add(this.tbInterest);
            this.Controls.Add(this.tbAmount);
            this.Controls.Add(this.lvMembers);
            this.Controls.Add(this.pnlButtons);
            this.Name = "VillageAddLoanForm";
            this.pnlButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.udGracePeriod)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udInstallments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udCompulsoryPercentage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlButtons;
        private OpenCBS.GUI.UserControl.ListViewEx lvMembers;
        private System.Windows.Forms.ColumnHeader chName;
        private System.Windows.Forms.ColumnHeader chPassport;
        private System.Windows.Forms.ColumnHeader chAmount;
        private System.Windows.Forms.TextBox tbAmount;
        private System.Windows.Forms.ColumnHeader chInterest;
        private System.Windows.Forms.TextBox tbInterest;
        private System.Windows.Forms.ColumnHeader chGracePeriod;
        private System.Windows.Forms.NumericUpDown udGracePeriod;
        private System.Windows.Forms.ColumnHeader chInstallments;
        private System.Windows.Forms.NumericUpDown udInstallments;
        private System.Windows.Forms.ColumnHeader chLoanOfficer;
        private System.Windows.Forms.ComboBox cbLoanOfficer;
        private System.Windows.Forms.TextBox tbEntryFee;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cbFundingLine;
        private System.Windows.Forms.ColumnHeader chFundingLine;
        private System.Windows.Forms.ComboBox cbDonor;
        private System.Windows.Forms.ColumnHeader chAvailableFunds;
        private System.Windows.Forms.ColumnHeader chCompulsorySavings;
        private System.Windows.Forms.ColumnHeader chCompulsoryPercentage;
        private System.Windows.Forms.ComboBox cbCompulsorySavings;
        private System.Windows.Forms.NumericUpDown udCompulsoryPercentage;
        private System.Windows.Forms.ColumnHeader chCurrency;
        private System.Windows.Forms.ColumnHeader chCreationDate;
        private System.Windows.Forms.DateTimePicker dtCreationDate;
    }
}