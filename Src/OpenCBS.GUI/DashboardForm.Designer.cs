

namespace OpenCBS.GUI
{
    partial class DashboardForm
    {
        private System.ComponentModel.IContainer components;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DashboardForm));
            this.generalInfoPanel = new System.Windows.Forms.Panel();
            this.infoPanel = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.disbursementsPanel = new System.Windows.Forms.Panel();
            this.olbTrendPanel = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.riskTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.parListView = new BrightIdeasSoftware.ObjectListView();
            this.parNameColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.parAmountColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.parQuantityColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.portfolioPanel = new System.Windows.Forms.Panel();
            this.parPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this._filterPanel = new System.Windows.Forms.Panel();
            this._refreshButton = new System.Windows.Forms.Button();
            this._loanProductFilterComboBox = new System.Windows.Forms.ComboBox();
            this._loanProductFilterLabel = new System.Windows.Forms.Label();
            this._userFilterComboBox = new System.Windows.Forms.ComboBox();
            this._userFilterLabel = new System.Windows.Forms.Label();
            this._branchFilterComboBox = new System.Windows.Forms.ComboBox();
            this._branchFilterLabel = new System.Windows.Forms.Label();
            this.topBarPanel = new System.Windows.Forms.TableLayoutPanel();
            this.smallLogoPictureBox = new System.Windows.Forms.PictureBox();
            this.generalInfoPanel.SuspendLayout();
            this.infoPanel.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.riskTableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.parListView)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this._filterPanel.SuspendLayout();
            this.topBarPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smallLogoPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // generalInfoPanel
            // 
            resources.ApplyResources(this.generalInfoPanel, "generalInfoPanel");
            this.generalInfoPanel.Controls.Add(this.infoPanel);
            this.generalInfoPanel.Name = "generalInfoPanel";
            // 
            // infoPanel
            // 
            resources.ApplyResources(this.infoPanel, "infoPanel");
            this.infoPanel.BackColor = System.Drawing.Color.White;
            this.infoPanel.Controls.Add(this.tableLayoutPanel1);
            this.infoPanel.Controls.Add(this.label2);
            this.infoPanel.Controls.Add(this.riskTableLayoutPanel);
            this.infoPanel.Controls.Add(this.label1);
            this.infoPanel.Controls.Add(this._filterPanel);
            this.infoPanel.Controls.Add(this.topBarPanel);
            this.infoPanel.Name = "infoPanel";
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // tableLayoutPanel3
            // 
            resources.ApplyResources(this.tableLayoutPanel3, "tableLayoutPanel3");
            this.tableLayoutPanel3.Controls.Add(this.disbursementsPanel, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.olbTrendPanel, 1, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            // 
            // disbursementsPanel
            // 
            resources.ApplyResources(this.disbursementsPanel, "disbursementsPanel");
            this.disbursementsPanel.Name = "disbursementsPanel";
            // 
            // olbTrendPanel
            // 
            resources.ApplyResources(this.olbTrendPanel, "olbTrendPanel");
            this.olbTrendPanel.Name = "olbTrendPanel";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.label2.Name = "label2";
            // 
            // riskTableLayoutPanel
            // 
            resources.ApplyResources(this.riskTableLayoutPanel, "riskTableLayoutPanel");
            this.riskTableLayoutPanel.Controls.Add(this.parListView, 1, 0);
            this.riskTableLayoutPanel.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.riskTableLayoutPanel.Name = "riskTableLayoutPanel";
            // 
            // parListView
            // 
            resources.ApplyResources(this.parListView, "parListView");
            this.parListView.AllColumns.Add(this.parNameColumn);
            this.parListView.AllColumns.Add(this.parAmountColumn);
            this.parListView.AllColumns.Add(this.parQuantityColumn);
            this.parListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.parNameColumn,
            this.parAmountColumn,
            this.parQuantityColumn});
            this.parListView.FullRowSelect = true;
            this.parListView.GridLines = true;
            this.parListView.HasCollapsibleGroups = false;
            this.parListView.Name = "parListView";
            this.parListView.OverlayText.Text = resources.GetString("resource.Text");
            this.parListView.ShowGroups = false;
            this.parListView.UseCompatibleStateImageBehavior = false;
            this.parListView.View = System.Windows.Forms.View.Details;
            // 
            // parNameColumn
            // 
            this.parNameColumn.AspectName = "Name";
            resources.ApplyResources(this.parNameColumn, "parNameColumn");
            // 
            // parAmountColumn
            // 
            this.parAmountColumn.AspectName = "Amount";
            resources.ApplyResources(this.parAmountColumn, "parAmountColumn");
            this.parAmountColumn.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // parQuantityColumn
            // 
            this.parQuantityColumn.AspectName = "Quantity";
            resources.ApplyResources(this.parQuantityColumn, "parQuantityColumn");
            this.parQuantityColumn.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.portfolioPanel, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.parPanel, 1, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // portfolioPanel
            // 
            resources.ApplyResources(this.portfolioPanel, "portfolioPanel");
            this.portfolioPanel.Name = "portfolioPanel";
            // 
            // parPanel
            // 
            resources.ApplyResources(this.parPanel, "parPanel");
            this.parPanel.Name = "parPanel";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.label1.Name = "label1";
            // 
            // _filterPanel
            // 
            resources.ApplyResources(this._filterPanel, "_filterPanel");
            this._filterPanel.Controls.Add(this._refreshButton);
            this._filterPanel.Controls.Add(this._loanProductFilterComboBox);
            this._filterPanel.Controls.Add(this._loanProductFilterLabel);
            this._filterPanel.Controls.Add(this._userFilterComboBox);
            this._filterPanel.Controls.Add(this._userFilterLabel);
            this._filterPanel.Controls.Add(this._branchFilterComboBox);
            this._filterPanel.Controls.Add(this._branchFilterLabel);
            this._filterPanel.Name = "_filterPanel";
            // 
            // _refreshButton
            // 
            resources.ApplyResources(this._refreshButton, "_refreshButton");
            this._refreshButton.Name = "_refreshButton";
            this._refreshButton.UseVisualStyleBackColor = true;
            // 
            // _loanProductFilterComboBox
            // 
            resources.ApplyResources(this._loanProductFilterComboBox, "_loanProductFilterComboBox");
            this._loanProductFilterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._loanProductFilterComboBox.FormattingEnabled = true;
            this._loanProductFilterComboBox.Name = "_loanProductFilterComboBox";
            // 
            // _loanProductFilterLabel
            // 
            resources.ApplyResources(this._loanProductFilterLabel, "_loanProductFilterLabel");
            this._loanProductFilterLabel.Name = "_loanProductFilterLabel";
            // 
            // _userFilterComboBox
            // 
            resources.ApplyResources(this._userFilterComboBox, "_userFilterComboBox");
            this._userFilterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._userFilterComboBox.FormattingEnabled = true;
            this._userFilterComboBox.Name = "_userFilterComboBox";
            // 
            // _userFilterLabel
            // 
            resources.ApplyResources(this._userFilterLabel, "_userFilterLabel");
            this._userFilterLabel.Name = "_userFilterLabel";
            // 
            // _branchFilterComboBox
            // 
            resources.ApplyResources(this._branchFilterComboBox, "_branchFilterComboBox");
            this._branchFilterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._branchFilterComboBox.FormattingEnabled = true;
            this._branchFilterComboBox.Name = "_branchFilterComboBox";
            // 
            // _branchFilterLabel
            // 
            resources.ApplyResources(this._branchFilterLabel, "_branchFilterLabel");
            this._branchFilterLabel.Name = "_branchFilterLabel";
            // 
            // topBarPanel
            // 
            resources.ApplyResources(this.topBarPanel, "topBarPanel");
            this.topBarPanel.Controls.Add(this.smallLogoPictureBox, 0, 0);
            this.topBarPanel.Name = "topBarPanel";
            // 
            // smallLogoPictureBox
            // 
            resources.ApplyResources(this.smallLogoPictureBox, "smallLogoPictureBox");
            this.smallLogoPictureBox.Image = global::OpenCBS.GUI.Properties.Resources.LOGO;
            this.smallLogoPictureBox.Name = "smallLogoPictureBox";
            this.smallLogoPictureBox.TabStop = false;
            // 
            // DashboardForm
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.generalInfoPanel);
            this.Name = "DashboardForm";
            this.ShowInTaskbar = false;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.OnLoad);
            this.generalInfoPanel.ResumeLayout(false);
            this.infoPanel.ResumeLayout(false);
            this.infoPanel.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.riskTableLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.parListView)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this._filterPanel.ResumeLayout(false);
            this._filterPanel.PerformLayout();
            this.topBarPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smallLogoPictureBox)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.Panel generalInfoPanel;
        private System.Windows.Forms.Panel infoPanel;
        private System.Windows.Forms.TableLayoutPanel riskTableLayoutPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel portfolioPanel;
        private System.Windows.Forms.Panel parPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Panel disbursementsPanel;
        private System.Windows.Forms.Panel olbTrendPanel;
        private System.Windows.Forms.TableLayoutPanel topBarPanel;
        private System.Windows.Forms.PictureBox smallLogoPictureBox;
        private System.Windows.Forms.Panel _filterPanel;
        private System.Windows.Forms.Label _branchFilterLabel;
        private System.Windows.Forms.ComboBox _branchFilterComboBox;
        private System.Windows.Forms.ComboBox _userFilterComboBox;
        private System.Windows.Forms.Label _userFilterLabel;
        private System.Windows.Forms.ComboBox _loanProductFilterComboBox;
        private System.Windows.Forms.Label _loanProductFilterLabel;
        private System.Windows.Forms.Button _refreshButton;
        private BrightIdeasSoftware.ObjectListView parListView;
        private BrightIdeasSoftware.OLVColumn parNameColumn;
        private BrightIdeasSoftware.OLVColumn parAmountColumn;
        private BrightIdeasSoftware.OLVColumn parQuantityColumn;
    }
}
