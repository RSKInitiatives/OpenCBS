using System.ComponentModel;
using System.Windows.Forms;
using OpenCBS.GUI.UserControl;

namespace OpenCBS.GUI.Products
{
    public partial class FrmAvailableSavingProducts
    {
        private System.Windows.Forms.Button buttonDeleteProduct;
        private System.Windows.Forms.Button buttonAddProduct;

        private GroupBox groupBox1;
        private Panel pnlSavingsProducts;
        private CheckBox checkBoxShowDeletedProduct;
        private System.Windows.Forms.Button buttonEditProduct;

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

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAvailableSavingProducts));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonEditProduct = new System.Windows.Forms.Button();
            this.checkBoxShowDeletedProduct = new System.Windows.Forms.CheckBox();
            this.buttonAddProduct = new System.Windows.Forms.Button();
            this.buttonDeleteProduct = new System.Windows.Forms.Button();
            this.pnlSavingsProducts = new System.Windows.Forms.Panel();
            this.descriptionListView = new OpenCBS.GUI.UserControl.ListViewEx();
            this.columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderClientType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderInterestRate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderTermDeposit = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuBtnAddProduct = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.savingBookProductToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.pnlSavingsProducts.SuspendLayout();
            this.menuBtnAddProduct.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonEditProduct);
            this.groupBox1.Controls.Add(this.checkBoxShowDeletedProduct);
            this.groupBox1.Controls.Add(this.buttonAddProduct);
            this.groupBox1.Controls.Add(this.buttonDeleteProduct);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // buttonEditProduct
            // 
            resources.ApplyResources(this.buttonEditProduct, "buttonEditProduct");
            this.buttonEditProduct.Name = "buttonEditProduct";
            this.buttonEditProduct.Click += new System.EventHandler(this.buttonEditProduct_Click);
            // 
            // checkBoxShowDeletedProduct
            // 
            resources.ApplyResources(this.checkBoxShowDeletedProduct, "checkBoxShowDeletedProduct");
            this.checkBoxShowDeletedProduct.Name = "checkBoxShowDeletedProduct";
            this.checkBoxShowDeletedProduct.CheckedChanged += new System.EventHandler(this.checkBoxShowDeletedProduct_CheckedChanged);
            // 
            // buttonAddProduct
            // 
            resources.ApplyResources(this.buttonAddProduct, "buttonAddProduct");
            this.buttonAddProduct.Name = "buttonAddProduct";
            this.buttonAddProduct.Click += new System.EventHandler(this.buttonAddProduct_Click);
            // 
            // buttonDeleteProduct
            // 
            resources.ApplyResources(this.buttonDeleteProduct, "buttonDeleteProduct");
            this.buttonDeleteProduct.Name = "buttonDeleteProduct";
            this.buttonDeleteProduct.Click += new System.EventHandler(this.buttonDeletePackage_Click);
            // 
            // pnlSavingsProducts
            // 
            this.pnlSavingsProducts.Controls.Add(this.descriptionListView);
            this.pnlSavingsProducts.Controls.Add(this.groupBox1);
            resources.ApplyResources(this.pnlSavingsProducts, "pnlSavingsProducts");
            this.pnlSavingsProducts.Name = "pnlSavingsProducts";
            // 
            // descriptionListView
            // 
            this.descriptionListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName,
            this.columnHeaderClientType,
            this.columnHeaderInterestRate,
            this.columnHeaderTermDeposit});
            resources.ApplyResources(this.descriptionListView, "descriptionListView");
            this.descriptionListView.DoubleClickActivation = false;
            this.descriptionListView.FullRowSelect = true;
            this.descriptionListView.GridLines = true;
            this.descriptionListView.MultiSelect = false;
            this.descriptionListView.Name = "descriptionListView";
            this.descriptionListView.UseCompatibleStateImageBehavior = false;
            this.descriptionListView.View = System.Windows.Forms.View.Details;
            this.descriptionListView.Click += new System.EventHandler(this.descriptionListView_Click);
            this.descriptionListView.DoubleClick += new System.EventHandler(this.descriptionListView_DoubleClick);
            // 
            // columnHeaderName
            // 
            resources.ApplyResources(this.columnHeaderName, "columnHeaderName");
            // 
            // columnHeaderClientType
            // 
            resources.ApplyResources(this.columnHeaderClientType, "columnHeaderClientType");
            // 
            // columnHeaderInterestRate
            // 
            resources.ApplyResources(this.columnHeaderInterestRate, "columnHeaderInterestRate");
            // 
            // columnHeaderTermDeposit
            // 
            resources.ApplyResources(this.columnHeaderTermDeposit, "columnHeaderTermDeposit");
            // 
            // menuBtnAddProduct
            // 
            this.menuBtnAddProduct.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.savingBookProductToolStripMenuItem});
            this.menuBtnAddProduct.Name = "contextMenuStrip1";
            resources.ApplyResources(this.menuBtnAddProduct, "menuBtnAddProduct");
            // 
            // savingBookProductToolStripMenuItem
            // 
            this.savingBookProductToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.savingBookProductToolStripMenuItem.Name = "savingBookProductToolStripMenuItem";
            resources.ApplyResources(this.savingBookProductToolStripMenuItem, "savingBookProductToolStripMenuItem");
            // 
            // FrmAvailableSavingProducts
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.pnlSavingsProducts);
            this.Name = "FrmAvailableSavingProducts";
            this.Load += new System.EventHandler(this.FrmAvailableSavingProducts_Load);
            this.Controls.SetChildIndex(this.pnlSavingsProducts, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pnlSavingsProducts.ResumeLayout(false);
            this.menuBtnAddProduct.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private IContainer components;
        private ContextMenuStrip menuBtnAddProduct;
        private ToolStripMenuItem savingBookProductToolStripMenuItem;
        private ListViewEx descriptionListView;
        private ColumnHeader columnHeaderName;
        private ColumnHeader columnHeaderClientType;
        private ColumnHeader columnHeaderInterestRate;
        private ColumnHeader columnHeaderTermDeposit;
    }
}
