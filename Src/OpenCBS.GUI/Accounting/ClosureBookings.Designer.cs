namespace OpenCBS.GUI.Accounting
{
    partial class ClosureBookings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClosureBookings));
            this.olvBookings = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn_Id = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn_Date = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn_Amount = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn_DebitAccount = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn_CreditAccount = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn_EventId = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn_EventType = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn_Currency = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn_ExchangeRate = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn_Description = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn_Branch = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            ((System.ComponentModel.ISupportInitialize)(this.olvBookings)).BeginInit();
            this.SuspendLayout();
            // 
            // olvBookings
            // 
            this.olvBookings.AllColumns.Add(this.olvColumn_Id);
            this.olvBookings.AllColumns.Add(this.olvColumn_Date);
            this.olvBookings.AllColumns.Add(this.olvColumn_Amount);
            this.olvBookings.AllColumns.Add(this.olvColumn_DebitAccount);
            this.olvBookings.AllColumns.Add(this.olvColumn_CreditAccount);
            this.olvBookings.AllColumns.Add(this.olvColumn_EventId);
            this.olvBookings.AllColumns.Add(this.olvColumn_EventType);
            this.olvBookings.AllColumns.Add(this.olvColumn_Currency);
            this.olvBookings.AllColumns.Add(this.olvColumn_ExchangeRate);
            this.olvBookings.AllColumns.Add(this.olvColumn_Description);
            this.olvBookings.AllColumns.Add(this.olvColumn_Branch);
            this.olvBookings.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn_Id,
            this.olvColumn_Date,
            this.olvColumn_Amount,
            this.olvColumn_DebitAccount,
            this.olvColumn_CreditAccount,
            this.olvColumn_EventId,
            this.olvColumn_EventType,
            this.olvColumn_Currency,
            this.olvColumn_ExchangeRate,
            this.olvColumn_Description,
            this.olvColumn_Branch});
            resources.ApplyResources(this.olvBookings, "olvBookings");
            this.olvBookings.FullRowSelect = true;
            this.olvBookings.GridLines = true;
            this.olvBookings.HasCollapsibleGroups = false;
            this.olvBookings.Name = "olvBookings";
            this.olvBookings.ShowGroups = false;
            this.olvBookings.UseCompatibleStateImageBehavior = false;
            this.olvBookings.View = System.Windows.Forms.View.Details;
            // 
            // olvColumn_Id
            // 
            this.olvColumn_Id.AspectName = "Id";
            resources.ApplyResources(this.olvColumn_Id, "olvColumn_Id");
            // 
            // olvColumn_Date
            // 
            this.olvColumn_Date.AspectName = "Date";
            resources.ApplyResources(this.olvColumn_Date, "olvColumn_Date");
            // 
            // olvColumn_Amount
            // 
            this.olvColumn_Amount.AspectName = "Amount";
            resources.ApplyResources(this.olvColumn_Amount, "olvColumn_Amount");
            // 
            // olvColumn_DebitAccount
            // 
            this.olvColumn_DebitAccount.AspectName = "DebitAccount";
            resources.ApplyResources(this.olvColumn_DebitAccount, "olvColumn_DebitAccount");
            // 
            // olvColumn_CreditAccount
            // 
            this.olvColumn_CreditAccount.AspectName = "CreditAccount";
            resources.ApplyResources(this.olvColumn_CreditAccount, "olvColumn_CreditAccount");
            // 
            // olvColumn_EventId
            // 
            this.olvColumn_EventId.AspectName = "EventId";
            resources.ApplyResources(this.olvColumn_EventId, "olvColumn_EventId");
            // 
            // olvColumn_EventType
            // 
            this.olvColumn_EventType.AspectName = "EventType";
            resources.ApplyResources(this.olvColumn_EventType, "olvColumn_EventType");
            // 
            // olvColumn_Currency
            // 
            this.olvColumn_Currency.AspectName = "Currency";
            resources.ApplyResources(this.olvColumn_Currency, "olvColumn_Currency");
            // 
            // olvColumn_ExchangeRate
            // 
            this.olvColumn_ExchangeRate.AspectName = "ExchangeRate";
            resources.ApplyResources(this.olvColumn_ExchangeRate, "olvColumn_ExchangeRate");
            // 
            // olvColumn_Description
            // 
            this.olvColumn_Description.AspectName = "Description";
            resources.ApplyResources(this.olvColumn_Description, "olvColumn_Description");
            // 
            // olvColumn_Branch
            // 
            this.olvColumn_Branch.AspectName = "Branch";
            resources.ApplyResources(this.olvColumn_Branch, "olvColumn_Branch");
            // 
            // ClosureBookings
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.olvBookings);
            this.Name = "ClosureBookings";
            ((System.ComponentModel.ISupportInitialize)(this.olvBookings)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private BrightIdeasSoftware.ObjectListView olvBookings;
        private BrightIdeasSoftware.OLVColumn olvColumn_Id;
        private BrightIdeasSoftware.OLVColumn olvColumn_Date;
        private BrightIdeasSoftware.OLVColumn olvColumn_Amount;
        private BrightIdeasSoftware.OLVColumn olvColumn_DebitAccount;
        private BrightIdeasSoftware.OLVColumn olvColumn_CreditAccount;
        private BrightIdeasSoftware.OLVColumn olvColumn_EventId;
        private BrightIdeasSoftware.OLVColumn olvColumn_EventType;
        private BrightIdeasSoftware.OLVColumn olvColumn_Currency;
        private BrightIdeasSoftware.OLVColumn olvColumn_ExchangeRate;
        private BrightIdeasSoftware.OLVColumn olvColumn_Description;
        private BrightIdeasSoftware.OLVColumn olvColumn_Branch;
    }
}