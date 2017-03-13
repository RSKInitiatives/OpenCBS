// Octopus MFS is an integrated suite for managing a Micro Finance Institution: 
// clients, contracts, accounting, reporting and risk
// Copyright © 2006,2007 OCTO Technology & OXUS Development Network
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License along
// with this program; if not, write to the Free Software Foundation, Inc.,
// 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//
// Website: http://www.opencbs.com
// Contact: contact@opencbs.com

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
using OpenCBS.GUI.Configuration;
using OpenCBS.Shared.Settings;
using OpenCBS.GUI.UserControl;

namespace OpenCBS.GUI.Products
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public partial class FrmAvailableSavingProducts : SweetForm
    {
        private ISavingProduct _package;
        private ISavingProduct _selectedPackage;
        private bool _showDeletedPackage = false;

        public FrmAvailableSavingProducts()
        {
            InitializeComponent();
            InitializePackages();
        }

        private void InitializePackages()
        {
            List<ISavingProduct> packageList = ServicesProvider.GetInstance().GetSavingProductServices().
                FindAllSavingsProducts(_showDeletedPackage, OClientTypes.All);
            packageList.Sort((p1, p2) => p1.Name.CompareTo(p2.Name));

            foreach (ISavingProduct s in packageList)
            {
                ListViewItem lvi = new ListViewItem(s.Name);
                lvi.SubItems.Add(Convert.ToString(s.ClientType));
                lvi.SubItems.Add(s.InterestRate == null ? String.Format("{0:N2}% - {1:N2}%", s.InterestRateMin * 100, s.InterestRateMax * 100) : String.Format("{0:N2}%", s.InterestRate * 100));
                lvi.SubItems.Add(s.Periodicity == null ? "No" : "Yes"); //TermDeposit
                lvi.Tag = s;
                determineRowColor(s, lvi);
                descriptionListView.Items.Add(lvi);
            }
        }

        private void determineRowColor(ISavingProduct savingsProduct, ListViewItem listViewItem)
        {
            if (savingsProduct.Delete == true)
                listViewItem.BackColor = System.Drawing.Color.LightGray;
            else
                listViewItem.BackColor = System.Drawing.Color.White;
        }

        private void buttonAddProduct_Click(object sender, EventArgs e)
        {
            AddSavingBookProduct();
        }

        private void AddSavingBookProduct()
        {
            FrmAddSavingBookProduct addPackageForm = new FrmAddSavingBookProduct();
            if (addPackageForm.ShowDialog() == DialogResult.OK)
            {
                refreshListView();
                ((MainView)MdiParent).SetInfoMessage(GetString("SaveSavingProduct.Text"));
            }
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
                {
                    try
                    {
                        ServicesProvider.GetInstance().GetSavingProductServices().DeleteSavingProduct(retrieveSelectedPackage().Id);
                        refreshListView();
                        _selectedPackage = null;
                        buttonDeleteProduct.Enabled = false;
                        buttonEditProduct.Enabled = false;
                    }
                    catch (Exception ex)
                    {
                        new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
                    }
                }
            }
            else
            {
                MessageBox.Show(MultiLanguageStrings.GetString(Ressource.PackagesForm, "messageSelection.Text"),
                                MultiLanguageStrings.GetString(Ressource.PackagesForm, "title.Text"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void buttonEditProduct_Click(object sender, EventArgs e)
        {
            EditProduct();
        }

        private void EditProduct()
        {
            if (descriptionListView.SelectedItems.Count != 0)
            {
                ISavingProduct product = retrieveSelectedPackage();

                if (product is SavingsBookProduct)
                    EditSavingBookProduct((SavingsBookProduct)product);
            }
            else
            {
                MessageBox.Show(MultiLanguageStrings.GetString(Ressource.PackagesForm, "messageSelection.Text"),
                                MultiLanguageStrings.GetString(Ressource.PackagesForm, "title.Text"), MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void EditSavingBookProduct(SavingsBookProduct sProduct)
        {
            FrmAddSavingBookProduct editProductForm = new FrmAddSavingBookProduct(sProduct);
            if (editProductForm.ShowDialog() == DialogResult.OK)
            {
                refreshListView();
                ((MainView)MdiParent).SetInfoMessage(GetString("ModifiSavingProduct.Text"));
            }
        }

        private void checkBoxShowDeletedProduct_CheckedChanged(object sender, EventArgs e)
        {
            _showDeletedPackage = checkBoxShowDeletedProduct.Checked;
            refreshListView();
        }

        private void descriptionListView_Click(object sender, EventArgs e)
        {
            buttonDeleteProduct.Enabled = true;
            buttonEditProduct.Enabled = true;
        }

        private void descriptionListView_DoubleClick(object sender, EventArgs e)
        {
            EditProduct();
        }

        private ISavingProduct retrieveSelectedPackage()
        {
            _package = (ISavingProduct)descriptionListView.SelectedItems[0].Tag;
            _selectedPackage = ServicesProvider.GetInstance().GetSavingProductServices().FindSavingProductById(_package.Id);
            return _selectedPackage;
        }

        private void FrmAvailableSavingProducts_Load(object sender, EventArgs e)
        {
            lblTitle.Text = "Account Products";
        }
    }
}