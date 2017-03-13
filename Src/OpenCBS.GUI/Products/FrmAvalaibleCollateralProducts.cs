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
using System.Security.Permissions;
using OpenCBS.CoreDomain.Products.Collaterals;
using OpenCBS.ExceptionsHandler;
using OpenCBS.Services;
using OpenCBS.MultiLanguageRessources;
using OpenCBS.Shared.Settings;
using OpenCBS.GUI.UserControl;

namespace OpenCBS.GUI.Products
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public partial class FrmAvalaibleCollateralProducts : SweetForm
    {
        private int idPackage;
        private CollateralProduct product;
        private bool _showDeletedPackage = false;
        private CollateralProduct _package;
        private CollateralProduct _selectedPackage;

        public FrmAvalaibleCollateralProducts()
        {
            InitializeComponent();
            product = new CollateralProduct();
            refreshListView();
            _package = new CollateralProduct();
            _selectedPackage = new CollateralProduct();
        }

        private int PackageFormId
        {
            set { idPackage = value; }
            get { return idPackage; }
        }

        private void InitializePackages()
        {
            List<CollateralProduct> productList = ServicesProvider.GetInstance().GetCollateralProductServices().SelectAllCollateralProducts(_showDeletedPackage);
            productList.Sort(new CollateralProductComparer<CollateralProduct>());
            foreach (CollateralProduct cP in productList)
            {
                ListViewItem lvi = new ListViewItem(cP.Name);
                lvi.SubItems.Add(cP.Description);
                lvi.Tag = cP;
                determineRowColor(cP, lvi);
                descriptionListView.Items.Add(lvi);
            }            
        }

        private void determineRowColor(CollateralProduct cP, ListViewItem lvi)
        {
            if (cP.Delete == true)
                lvi.BackColor = System.Drawing.Color.LightGray;
            else
                lvi.BackColor = System.Drawing.Color.White;
        }

        private void buttonAddProduct_Click(object sender, EventArgs e)
        {
            FrmAddCollateralProduct addCollateralProductForm = new FrmAddCollateralProduct();
            addCollateralProductForm.ShowDialog();
            refreshListView();
            ((MainView)MdiParent).SetInfoMessage("Collateral type saved");
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
                ServicesProvider.GetInstance().GetCollateralProductServices().DeleteCollateralProduct(retrieveSelectedPackage().Id);
                refreshListView();
                product = null;
                buttonDeletePackage.Enabled = false;
                buttonEditProduct.Enabled = false;
            }
            catch (Exception ex)
            {
                new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
            }
        }

        private void buttonEditProduct_Click(object sender, EventArgs e)
        {
            if (descriptionListView.SelectedItems.Count != 0)
            {
                FrmAddCollateralProduct addCollateralProduct = new FrmAddCollateralProduct(retrieveSelectedPackage());
                addCollateralProduct.ShowDialog();
                refreshListView();
            }
            else
            {
                MessageBox.Show(MultiLanguageStrings.GetString(Ressource.PackagesForm, "messageSelection.Text"),
                    MultiLanguageStrings.GetString(Ressource.PackagesForm, "title.Text"), MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            FrmAddCollateralProduct addCollateralProduct = new FrmAddCollateralProduct(retrieveSelectedPackage());
            addCollateralProduct.ShowDialog();
        }      

        private CollateralProduct retrieveSelectedPackage()
        {
            _package = (CollateralProduct)descriptionListView.SelectedItems[0].Tag;
            _selectedPackage = ServicesProvider.GetInstance().GetCollateralProductServices().SelectCollateralProduct(_package.Id);
            return _selectedPackage;
        }

        private void PackagesForm_Load(object sender, EventArgs e)
        {
            buttonDeletePackage.Enabled = true;
            buttonEditProduct.Enabled = true;
        }






    }
}
