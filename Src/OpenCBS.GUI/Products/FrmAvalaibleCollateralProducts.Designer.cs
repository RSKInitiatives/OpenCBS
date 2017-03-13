using System.ComponentModel;
using System.Windows.Forms;
using OpenCBS.GUI.UserControl;

namespace OpenCBS.GUI.Products
{
    public partial class FrmAvalaibleCollateralProducts
    {
        private System.Windows.Forms.Button buttonDeletePackage;
        private System.Windows.Forms.Button buttonAddProduct;
        private System.Windows.Forms.Button buttonEditProduct;

        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private Container components = null;

        private GroupBox groupBox;
        private Panel pnlCollateralProducts;
        private CheckBox checkBoxShowDeletedProduct;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAvalaibleCollateralProducts));
            this.pnlCollateralProducts = new System.Windows.Forms.Panel();
            this.descriptionListView = new System.Windows.Forms.ListView();
            this.nameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.descriptionHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.buttonEditProduct = new System.Windows.Forms.Button();
            this.checkBoxShowDeletedProduct = new System.Windows.Forms.CheckBox();
            this.buttonAddProduct = new System.Windows.Forms.Button();
            this.buttonDeletePackage = new System.Windows.Forms.Button();
            this.pnlCollateralProducts.SuspendLayout();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCollateralProducts
            // 
            this.pnlCollateralProducts.Controls.Add(this.descriptionListView);
            this.pnlCollateralProducts.Controls.Add(this.groupBox);
            resources.ApplyResources(this.pnlCollateralProducts, "pnlCollateralProducts");
            this.pnlCollateralProducts.Name = "pnlCollateralProducts";
            // 
            // descriptionListView
            // 
            this.descriptionListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameHeader,
            this.descriptionHeader});
            resources.ApplyResources(this.descriptionListView, "descriptionListView");
            this.descriptionListView.FullRowSelect = true;
            this.descriptionListView.GridLines = true;
            this.descriptionListView.Name = "descriptionListView";
            this.descriptionListView.UseCompatibleStateImageBehavior = false;
            this.descriptionListView.View = System.Windows.Forms.View.Details;
            this.descriptionListView.Click += new System.EventHandler(this.descriptionListView_Click);
            this.descriptionListView.DoubleClick += new System.EventHandler(this.descriptionListView_DoubleClick);
            // 
            // nameHeader
            // 
            resources.ApplyResources(this.nameHeader, "nameHeader");
            // 
            // descriptionHeader
            // 
            resources.ApplyResources(this.descriptionHeader, "descriptionHeader");
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.buttonEditProduct);
            this.groupBox.Controls.Add(this.checkBoxShowDeletedProduct);
            this.groupBox.Controls.Add(this.buttonAddProduct);
            this.groupBox.Controls.Add(this.buttonDeletePackage);
            resources.ApplyResources(this.groupBox, "groupBox");
            this.groupBox.Name = "groupBox";
            this.groupBox.TabStop = false;
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
            // buttonDeletePackage
            // 
            resources.ApplyResources(this.buttonDeletePackage, "buttonDeletePackage");
            this.buttonDeletePackage.Name = "buttonDeletePackage";
            this.buttonDeletePackage.Click += new System.EventHandler(this.buttonDeletePackage_Click);
            // 
            // FrmAvalaibleCollateralProducts
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.pnlCollateralProducts);
            this.Name = "FrmAvalaibleCollateralProducts";
            this.Load += new System.EventHandler(this.PackagesForm_Load);
            this.Controls.SetChildIndex(this.pnlCollateralProducts, 0);
            this.pnlCollateralProducts.ResumeLayout(false);
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private ListView descriptionListView;
        private ColumnHeader nameHeader;
        private ColumnHeader descriptionHeader;
    }
}
