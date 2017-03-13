 //Octopus MFS is an integrated suite for managing a Micro Finance Institution: 
 //clients, contracts, accounting, reporting and risk
 //Copyright © 2006,2007 OCTO Technology & OXUS Development Network

 //This program is free software; you can redistribute it and/or modify
 //it under the terms of the GNU Lesser General Public License as published by
 //the Free Software Foundation; either version 2 of the License, or
 //(at your option) any later version.

 //This program is distributed in the hope that it will be useful,
 //but WITHOUT ANY WARRANTY; without even the implied warranty of
 //MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 //GNU Lesser General Public License for more details.

 //You should have received a copy of the GNU Lesser General Public License along
 //with this program; if not, write to the Free Software Foundation, Inc.,
 //51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.

 //Website: http://www.opencbs.com
 //Contact: contact@opencbs.com

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using OpenCBS.CoreDomain.Products;
using OpenCBS.Enums;
using OpenCBS.ExceptionsHandler;
using OpenCBS.Services;
using System.Security.Permissions;
using OpenCBS.MultiLanguageRessources;
using OpenCBS.Shared.Settings;
using OpenCBS.GUI.UserControl;

namespace OpenCBS.GUI.Products
{
     //<summary>
     //Description r�sum�e de PackagesForm.
     //</summary>
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public partial class FrmAvalaibleLoanProducts : SweetForm
    {
        private LoanProduct _package;
        private LoanProduct _selectedPackage;
        private bool _showDeletedPackage = false;
        
        public FrmAvalaibleLoanProducts()
        {
            InitializeComponent();
            InitializePackages();
            _package = new LoanProduct();
            _selectedPackage = new LoanProduct();
        }

        private void InitializePackages()
        {
            List<LoanProduct> packageList = ServicesProvider.GetInstance().GetProductServices().FindAllPackages(_showDeletedPackage, OClientTypes.All);
            packageList.Sort(new ProductComparer<LoanProduct>());

            foreach (LoanProduct p in packageList)
            {
                ListViewItem lvi = new ListViewItem(p.Name);
                lvi.SubItems.Add(Convert.ToString(p.LoanType));
                lvi.SubItems.Add(p.InterestRate == null ? String.Format("{0:N2}% - {1:N2}%", p.InterestRateMin * 100, p.InterestRateMax * 100) : String.Format("{0:N2}%", p.InterestRate * 100));
                if (p.InstallmentType.Id == 0)
                {
                    lvi.SubItems.Add(GetString("FrmAddLoanProduct", "allInstallmentTypes"));
                }
                else
                {
                    lvi.SubItems.Add(Convert.ToString(p.InstallmentType));
                }
                lvi.Tag = p;
                determineRowColor(p, lvi);
                descriptionListView.Items.Add(lvi);
            } 
        }

        private void determineRowColor(LoanProduct loanProduct, ListViewItem listViewItem)
        {
            if (loanProduct.Delete == true)
                listViewItem.BackColor = System.Drawing.Color.LightGray;
            else
                listViewItem.BackColor = System.Drawing.Color.White;
        }

        private void buttonAddPackage_Click(object sender, EventArgs e)
        {
            addPackage();
        }

        private void addPackage()
        {
            var addPackageForm = new FrmAddLoanProduct();
            addPackageForm.ShowDialog();
            refreshListView();
            ((MainView)MdiParent).SetInfoMessage("Product saved");
        }

        private void refreshListView()
        {
            descriptionListView.Items.Clear();
            InitializePackages();
        }

        private void buttonDeletePackage_Click(object sender, EventArgs e)
        {
            if (descriptionListView.SelectedItems.Count != 0)
            {
                string msg = GetString("DeleteConfirmation.Text");
                if (DialogResult.Yes == MessageBox.Show(msg, "", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
                    DeletePackage();
            }
            else
            {
                MessageBox.Show(GetString("messageSelection.Text"),
                                GetString("title.Text"),
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DeletePackage()
        {
            try
            {
                ServicesProvider.GetInstance().GetProductServices().DeletePackage(retrieveSelectedPackage());
                refreshListView();
                _selectedPackage = null;
                buttonDeletePackage.Enabled = false;
                buttonEditProduct.Enabled = false;
            }
            catch (Exception ex)
            {
                new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog(); //do u wanna build a snowman? :)
            }
        }

        private void buttonEditProduct_Click(object sender, EventArgs e)
        {
            if (descriptionListView.SelectedItems.Count != 0)
            {
                var addPackageForm = new FrmAddLoanProduct(retrieveSelectedPackage());
                addPackageForm.ShowDialog();
                refreshListView();
            }
            else
            {
                MessageBox.Show(GetString("messageSelection.Text"),
                                GetString("title.Text"),
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void checkBoxShowDeletedProduct_CheckedChanged(object sender, EventArgs e)
        {
            _showDeletedPackage = checkBoxShowDeletedProduct.Checked;
            refreshListView();
        }

        private void descriptionListView_Click(object sender, EventArgs e)
        {
            buttonDeletePackage.Enabled = true;
            buttonEditProduct.Enabled = true;
        }
        
        private void descriptionListView_DoubleClick(object sender, EventArgs e)
        {
            var addPackageForm = new FrmAddLoanProduct(retrieveSelectedPackage());
            addPackageForm.ShowDialog();
        }

        private LoanProduct retrieveSelectedPackage()
        {
            _package = (LoanProduct)descriptionListView.SelectedItems[0].Tag;
            _selectedPackage = ServicesProvider.GetInstance().GetProductServices().FindPackage(_package.Id);
            return _selectedPackage;
        }

        private void PackagesForm_Load(object sender, EventArgs e)
        {
            buttonDeletePackage.Enabled = true;
            buttonEditProduct.Enabled = true;
        }
    }
}