using System.ComponentModel;
using System.Windows.Forms;
using OpenCBS.GUI.UserControl;

namespace OpenCBS.GUI.Products
{
    public partial class FrmAvalaibleLoanProducts
    {
        private System.Windows.Forms.Button buttonDeletePackage;
        private System.Windows.Forms.Button buttonAddPackage;
        private System.Windows.Forms.Button buttonEditProduct;

        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private Container components = null;
        private Panel pnlLoanProducts;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAvalaibleLoanProducts));
            this.buttonEditProduct = new System.Windows.Forms.Button();
            this.checkBoxShowDeletedProduct = new System.Windows.Forms.CheckBox();
            this.buttonAddPackage = new System.Windows.Forms.Button();
            this.buttonDeletePackage = new System.Windows.Forms.Button();
            this.pnlLoanProducts = new System.Windows.Forms.Panel();
            this.descriptionListView = new System.Windows.Forms.ListView();
            this.nameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.installmentTypeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.interestRateHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.frequencyHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlLoanProducts.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
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
            // buttonAddPackage
            // 
            resources.ApplyResources(this.buttonAddPackage, "buttonAddPackage");
            this.buttonAddPackage.Name = "buttonAddPackage";
            this.buttonAddPackage.Click += new System.EventHandler(this.buttonAddPackage_Click);
            // 
            // buttonDeletePackage
            // 
            resources.ApplyResources(this.buttonDeletePackage, "buttonDeletePackage");
            this.buttonDeletePackage.Name = "buttonDeletePackage";
            this.buttonDeletePackage.Click += new System.EventHandler(this.buttonDeletePackage_Click);
            // 
            // pnlLoanProducts
            // 
            this.pnlLoanProducts.Controls.Add(this.descriptionListView);
            this.pnlLoanProducts.Controls.Add(this.flowLayoutPanel1);
            resources.ApplyResources(this.pnlLoanProducts, "pnlLoanProducts");
            this.pnlLoanProducts.Name = "pnlLoanProducts";
            // 
            // descriptionListView
            // 
            this.descriptionListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameHeader,
            this.installmentTypeHeader,
            this.interestRateHeader,
            this.frequencyHeader});
            resources.ApplyResources(this.descriptionListView, "descriptionListView");
            this.descriptionListView.FullRowSelect = true;
            this.descriptionListView.GridLines = true;
            this.descriptionListView.MultiSelect = false;
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
            // installmentTypeHeader
            // 
            resources.ApplyResources(this.installmentTypeHeader, "installmentTypeHeader");
            // 
            // interestRateHeader
            // 
            resources.ApplyResources(this.interestRateHeader, "interestRateHeader");
            // 
            // frequencyHeader
            // 
            resources.ApplyResources(this.frequencyHeader, "frequencyHeader");
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
            this.flowLayoutPanel1.Controls.Add(this.checkBoxShowDeletedProduct);
            this.flowLayoutPanel1.Controls.Add(this.buttonAddPackage);
            this.flowLayoutPanel1.Controls.Add(this.buttonEditProduct);
            this.flowLayoutPanel1.Controls.Add(this.buttonDeletePackage);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // FrmAvalaibleLoanProducts
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.pnlLoanProducts);
            this.Name = "FrmAvalaibleLoanProducts";
            this.Load += new System.EventHandler(this.PackagesForm_Load);
            this.Controls.SetChildIndex(this.pnlLoanProducts, 0);
            this.pnlLoanProducts.ResumeLayout(false);
            this.pnlLoanProducts.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private FlowLayoutPanel flowLayoutPanel1;
        private ListView descriptionListView;
        private ColumnHeader nameHeader;
        private ColumnHeader installmentTypeHeader;
        private ColumnHeader interestRateHeader;
        private ColumnHeader frequencyHeader;
    }
}
