namespace OpenCBS.GUI.TellerManagement
{
    partial class FrmTellerHistory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTellerHistory));
            this.pnlRight = new System.Windows.Forms.Panel();
            this.chkIncludeDeleted = new System.Windows.Forms.CheckBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.cbTeller = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbUser = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbBranch = new System.Windows.Forms.ComboBox();
            this.lblBranch = new System.Windows.Forms.Label();
            this.dtFrom = new System.Windows.Forms.DateTimePicker();
            this.dtTo = new System.Windows.Forms.DateTimePicker();
            this.lblBeginDate = new System.Windows.Forms.Label();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.lvTellerEvent = new System.Windows.Forms.ListView();
            this.columnHeader21 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader26 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader22 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader29 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.refColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader24 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCancelDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.bwReport = new System.ComponentModel.BackgroundWorker();
            this.bwRefresh = new System.ComponentModel.BackgroundWorker();
            this.pnlRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlRight
            // 
            this.pnlRight.Controls.Add(this.chkIncludeDeleted);
            this.pnlRight.Controls.Add(this.btnRefresh);
            this.pnlRight.Controls.Add(this.btnPrint);
            this.pnlRight.Controls.Add(this.cbTeller);
            this.pnlRight.Controls.Add(this.label2);
            this.pnlRight.Controls.Add(this.cbUser);
            this.pnlRight.Controls.Add(this.label1);
            this.pnlRight.Controls.Add(this.cbBranch);
            this.pnlRight.Controls.Add(this.lblBranch);
            this.pnlRight.Controls.Add(this.dtFrom);
            this.pnlRight.Controls.Add(this.dtTo);
            this.pnlRight.Controls.Add(this.lblBeginDate);
            this.pnlRight.Controls.Add(this.lblEndDate);
            resources.ApplyResources(this.pnlRight, "pnlRight");
            this.pnlRight.Name = "pnlRight";
            // 
            // chkIncludeDeleted
            // 
            resources.ApplyResources(this.chkIncludeDeleted, "chkIncludeDeleted");
            this.chkIncludeDeleted.Name = "chkIncludeDeleted";
            // 
            // btnRefresh
            // 
            resources.ApplyResources(this.btnRefresh, "btnRefresh");
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Click += new System.EventHandler(this.OnRefreshClick);
            // 
            // btnPrint
            // 
            resources.ApplyResources(this.btnPrint, "btnPrint");
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Click += new System.EventHandler(this.OnPrintClick);
            // 
            // cbTeller
            // 
            resources.ApplyResources(this.cbTeller, "cbTeller");
            this.cbTeller.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTeller.FormattingEnabled = true;
            this.cbTeller.Name = "cbTeller";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // cbUser
            // 
            resources.ApplyResources(this.cbUser, "cbUser");
            this.cbUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbUser.FormattingEnabled = true;
            this.cbUser.Name = "cbUser";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // cbBranch
            // 
            resources.ApplyResources(this.cbBranch, "cbBranch");
            this.cbBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBranch.FormattingEnabled = true;
            this.cbBranch.Name = "cbBranch";
            // 
            // lblBranch
            // 
            resources.ApplyResources(this.lblBranch, "lblBranch");
            this.lblBranch.Name = "lblBranch";
            // 
            // dtFrom
            // 
            resources.ApplyResources(this.dtFrom, "dtFrom");
            this.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtFrom.Name = "dtFrom";
            // 
            // dtTo
            // 
            resources.ApplyResources(this.dtTo, "dtTo");
            this.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtTo.Name = "dtTo";
            // 
            // lblBeginDate
            // 
            resources.ApplyResources(this.lblBeginDate, "lblBeginDate");
            this.lblBeginDate.Name = "lblBeginDate";
            // 
            // lblEndDate
            // 
            resources.ApplyResources(this.lblEndDate, "lblEndDate");
            this.lblEndDate.Name = "lblEndDate";
            // 
            // lvTellerEvent
            // 
            this.lvTellerEvent.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader21,
            this.columnHeader26,
            this.columnHeader22,
            this.columnHeader15,
            this.columnHeader29,
            this.refColumnHeader,
            this.columnHeader24,
            this.colCancelDate});
            resources.ApplyResources(this.lvTellerEvent, "lvTellerEvent");
            this.lvTellerEvent.FullRowSelect = true;
            this.lvTellerEvent.GridLines = true;
            this.lvTellerEvent.Name = "lvTellerEvent";
            this.lvTellerEvent.UseCompatibleStateImageBehavior = false;
            this.lvTellerEvent.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader21
            // 
            resources.ApplyResources(this.columnHeader21, "columnHeader21");
            // 
            // columnHeader26
            // 
            resources.ApplyResources(this.columnHeader26, "columnHeader26");
            // 
            // columnHeader22
            // 
            resources.ApplyResources(this.columnHeader22, "columnHeader22");
            // 
            // columnHeader15
            // 
            resources.ApplyResources(this.columnHeader15, "columnHeader15");
            // 
            // columnHeader29
            // 
            resources.ApplyResources(this.columnHeader29, "columnHeader29");
            // 
            // refColumnHeader
            // 
            resources.ApplyResources(this.refColumnHeader, "refColumnHeader");
            // 
            // columnHeader24
            // 
            resources.ApplyResources(this.columnHeader24, "columnHeader24");
            // 
            // colCancelDate
            // 
            resources.ApplyResources(this.colCancelDate, "colCancelDate");
            // 
            // bwReport
            // 
            this.bwReport.DoWork += new System.ComponentModel.DoWorkEventHandler(this.OnPrintDoWork);
            this.bwReport.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.OnPrintCompleted);
            // 
            // bwRefresh
            // 
            this.bwRefresh.DoWork += new System.ComponentModel.DoWorkEventHandler(this.OnRefreshDoWork);
            // 
            // FrmTellerHistory
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lvTellerEvent);
            this.Controls.Add(this.pnlRight);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmTellerHistory";
            this.Load += new System.EventHandler(this.OnLoad);
            this.Controls.SetChildIndex(this.pnlRight, 0);
            this.Controls.SetChildIndex(this.lvTellerEvent, 0);
            this.pnlRight.ResumeLayout(false);
            this.pnlRight.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.ComboBox cbBranch;
        private System.Windows.Forms.Label lblBranch;
        private System.Windows.Forms.DateTimePicker dtFrom;
        private System.Windows.Forms.DateTimePicker dtTo;
        private System.Windows.Forms.Label lblBeginDate;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.ListView lvTellerEvent;
        private System.Windows.Forms.ColumnHeader columnHeader21;
        private System.Windows.Forms.ColumnHeader columnHeader26;
        private System.Windows.Forms.ColumnHeader columnHeader22;
        private System.Windows.Forms.ColumnHeader columnHeader15;
        private System.Windows.Forms.ColumnHeader columnHeader29;
        private System.Windows.Forms.ColumnHeader refColumnHeader;
        private System.Windows.Forms.ColumnHeader columnHeader24;
        private System.Windows.Forms.ColumnHeader colCancelDate;
        private System.Windows.Forms.ComboBox cbUser;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbTeller;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnPrint;
        private System.ComponentModel.BackgroundWorker bwReport;
        private System.ComponentModel.BackgroundWorker bwRefresh;
        private System.Windows.Forms.CheckBox chkIncludeDeleted;
    }
}