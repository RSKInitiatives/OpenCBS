namespace OpenCBS.ArchitectureV2.View
{
    partial class BatchRepaymentView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BatchRepaymentView));
            this._loansListView = new BrightIdeasSoftware.FastObjectListView();
            this._firstNameColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this._lastNameColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this._contractCodeColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this._principalColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this._interestColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this._totalColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this._buttonsPanel = new System.Windows.Forms.Panel();
            this._cancelButton = new System.Windows.Forms.Button();
            this._okButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this._loansListView)).BeginInit();
            this._buttonsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _loansListView
            // 
            this._loansListView.AllColumns.Add(this._firstNameColumn);
            this._loansListView.AllColumns.Add(this._lastNameColumn);
            this._loansListView.AllColumns.Add(this._contractCodeColumn);
            this._loansListView.AllColumns.Add(this._principalColumn);
            this._loansListView.AllColumns.Add(this._interestColumn);
            this._loansListView.AllColumns.Add(this._totalColumn);
            this._loansListView.AlternateRowBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this._loansListView.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.DoubleClick;
            this._loansListView.CheckedAspectName = "Selected";
            this._loansListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this._firstNameColumn,
            this._lastNameColumn,
            this._contractCodeColumn,
            this._principalColumn,
            this._interestColumn,
            this._totalColumn});
            this._loansListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._loansListView.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._loansListView.FullRowSelect = true;
            this._loansListView.GridLines = true;
            this._loansListView.Location = new System.Drawing.Point(0, 0);
            this._loansListView.MultiSelect = false;
            this._loansListView.Name = "_loansListView";
            this._loansListView.ShowGroups = false;
            this._loansListView.ShowImagesOnSubItems = true;
            this._loansListView.Size = new System.Drawing.Size(910, 440);
            this._loansListView.TabIndex = 5;
            this._loansListView.UseCompatibleStateImageBehavior = false;
            this._loansListView.View = System.Windows.Forms.View.Details;
            this._loansListView.VirtualMode = true;
            // 
            // _firstNameColumn
            // 
            this._firstNameColumn.AspectName = "FirstName";
            this._firstNameColumn.IsEditable = false;
            this._firstNameColumn.Sortable = false;
            this._firstNameColumn.Text = "First Name";
            this._firstNameColumn.Width = 150;
            // 
            // _lastNameColumn
            // 
            this._lastNameColumn.AspectName = "LastName";
            this._lastNameColumn.IsEditable = false;
            this._lastNameColumn.Sortable = false;
            this._lastNameColumn.Text = "Last Name";
            this._lastNameColumn.Width = 150;
            // 
            // _contractCodeColumn
            // 
            this._contractCodeColumn.AspectName = "ContractCode";
            this._contractCodeColumn.IsEditable = false;
            this._contractCodeColumn.Sortable = false;
            this._contractCodeColumn.Text = "Contract Code";
            this._contractCodeColumn.Width = 250;
            // 
            // _principalColumn
            // 
            this._principalColumn.AspectName = "Principal";
            this._principalColumn.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this._principalColumn.IsEditable = false;
            this._principalColumn.Sortable = false;
            this._principalColumn.Text = "Principal";
            this._principalColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this._principalColumn.Width = 100;
            // 
            // _interestColumn
            // 
            this._interestColumn.AspectName = "Interest";
            this._interestColumn.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this._interestColumn.IsEditable = false;
            this._interestColumn.Sortable = false;
            this._interestColumn.Text = "Interest";
            this._interestColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this._interestColumn.Width = 100;
            // 
            // _totalColumn
            // 
            this._totalColumn.AspectName = "Total";
            this._totalColumn.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this._totalColumn.Sortable = false;
            this._totalColumn.Text = "Total";
            this._totalColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this._totalColumn.Width = 100;
            // 
            // _buttonsPanel
            // 
            this._buttonsPanel.Controls.Add(this._cancelButton);
            this._buttonsPanel.Controls.Add(this._okButton);
            this._buttonsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._buttonsPanel.Location = new System.Drawing.Point(0, 440);
            this._buttonsPanel.Name = "_buttonsPanel";
            this._buttonsPanel.Size = new System.Drawing.Size(910, 51);
            this._buttonsPanel.TabIndex = 6;
            // 
            // _cancelButton
            // 
            this._cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cancelButton.Location = new System.Drawing.Point(458, 14);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(75, 23);
            this._cancelButton.TabIndex = 1;
            this._cancelButton.Text = "Cancel";
            this._cancelButton.UseVisualStyleBackColor = true;
            // 
            // _okButton
            // 
            this._okButton.Location = new System.Drawing.Point(377, 14);
            this._okButton.Name = "_okButton";
            this._okButton.Size = new System.Drawing.Size(75, 23);
            this._okButton.TabIndex = 0;
            this._okButton.Text = "OK";
            this._okButton.UseVisualStyleBackColor = true;
            // 
            // BatchRepaymentView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._cancelButton;
            this.ClientSize = new System.Drawing.Size(910, 491);
            this.Controls.Add(this._loansListView);
            this.Controls.Add(this._buttonsPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BatchRepaymentView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Repayment";
            ((System.ComponentModel.ISupportInitialize)(this._loansListView)).EndInit();
            this._buttonsPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private BrightIdeasSoftware.FastObjectListView _loansListView;
        private BrightIdeasSoftware.OLVColumn _firstNameColumn;
        private BrightIdeasSoftware.OLVColumn _lastNameColumn;
        private BrightIdeasSoftware.OLVColumn _contractCodeColumn;
        private BrightIdeasSoftware.OLVColumn _principalColumn;
        private BrightIdeasSoftware.OLVColumn _interestColumn;
        private BrightIdeasSoftware.OLVColumn _totalColumn;
        private System.Windows.Forms.Panel _buttonsPanel;
        private System.Windows.Forms.Button _cancelButton;
        private System.Windows.Forms.Button _okButton;
    }
}