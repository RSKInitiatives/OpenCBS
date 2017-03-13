//This is a source code or part of Openmiracle project
//Copyright (C) 2013  Openmiracle
//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.
//You should have received a copy of the GNU General Public License
//along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace Open_Miracle
{
    public partial class frmBudgetVariance : Form
    {
        #region VARIABLES
        /// <summary>
        /// Public variable declaration part
        /// </summary>
        #endregion
        #region FUNCTIONS
        /// <summary>
        /// Creates an instance of frmbudgetvariance class
        /// </summary>
        public frmBudgetVariance()
        {
            InitializeComponent();
        }
        /// <summary>
        /// function to fill datagridview
        /// </summary>
        public void GridFill()
        {
            try
            {
                DataTable dtbl = new DataTable();
                BudgetMasterSP spbudgetmaster = new BudgetMasterSP();
                dgvBudgetVariance.Rows.Clear();
                decimal decId = Convert.ToDecimal(cmbBudget.SelectedValue.ToString());
                dtbl = spbudgetmaster.BudgetVariance(decId);
                decimal decrindx = dgvBudgetVariance.Rows.Count;
                int index = 0;
                foreach (DataRow rw in dtbl.Rows)
                {
                    dgvBudgetVariance.Rows.Add();
                    dgvBudgetVariance.Rows[index].Cells["dgvtxtSlNo"].Value = rw["Sl NO"].ToString();
                    dgvBudgetVariance.Rows[index].Cells["dgvtxtParticulars"].Value = rw["Particular"].ToString();
                    dgvBudgetVariance.Rows[index].Cells["dgvtxtBudgetDr"].Value = rw["BudgetDr"].ToString();
                    dgvBudgetVariance.Rows[index].Cells["dgvtxtActualDr"].Value = rw["ActualDr"].ToString();
                    dgvBudgetVariance.Rows[index].Cells["dgvtxtVarianceDr"].Value = rw["VarianceDr"].ToString();
                    dgvBudgetVariance.Rows[index].Cells["dgvtxtBudgetCr"].Value = rw["BudgetCr"].ToString();
                    dgvBudgetVariance.Rows[index].Cells["dgvtxtActualCr"].Value = rw["ActualCr"].ToString();
                    dgvBudgetVariance.Rows[index].Cells["dgvtxtVarianceCr"].Value = rw["VarianceCr"].ToString();
                    index++;
                }
            }
            catch (Exception ex)
            {
                formMDI.infoError.ErrorString = "BV:01" + ex.Message;
            }
        }
        /// <summary>
        /// function to fill budget combo
        /// </summary>
        public void BudgetComboFill()
        {
            try
            {
                BudgetMasterSP spBudgetMaster = new BudgetMasterSP();
                DataTable dtbl = new DataTable();
                dtbl = spBudgetMaster.BudgetViewAll();
                DataRow drow = dtbl.NewRow();
                drow[0] = 0;
                drow[1] = "Select";
                drow[2] = 0;
                drow[3] = 0;
                dtbl.Rows.InsertAt(drow, 0);
                cmbBudget.ValueMember = "budgetMasterId";
                cmbBudget.DisplayMember = "budgetName";
                cmbBudget.DataSource = dtbl;
            }
            catch (Exception ex)
            {
                formMDI.infoError.ErrorString = "BV:02" + ex.Message;
            }
        }
        /// <summary>
        /// function to convert datagridview data to a datatable
        /// </summary>
        /// <returns></returns>
        public DataTable GetDataTable()
        {
            DataTable dtbl = new DataTable();
            try
            {
                dtbl.Columns.Add("SlNo");
                dtbl.Columns.Add("Particulars");
                dtbl.Columns.Add("BudgetDr");
                dtbl.Columns.Add("ActualDr");
                dtbl.Columns.Add("VarianceDr");
                dtbl.Columns.Add("BudgetCr");
                dtbl.Columns.Add("ActualCr");
                dtbl.Columns.Add("VarianceCr");
                DataRow drow = null;
                foreach (DataGridViewRow dr in dgvBudgetVariance.Rows)
                {
                    drow = dtbl.NewRow();
                    drow["SlNo"] = dr.Cells["dgvtxtSlNo"].Value;
                    drow["Particulars"] = dr.Cells["dgvtxtParticulars"].Value;
                    drow["BudgetDr"] = dr.Cells["dgvtxtBudgetDr"].Value;
                    drow["ActualDr"] = dr.Cells["dgvtxtActualDr"].Value;
                    drow["VarianceDr"] = dr.Cells["dgvtxtVarianceDr"].Value;
                    drow["BudgetCr"] = dr.Cells["dgvtxtBudgetCr"].Value;
                    drow["ActualCr"] = dr.Cells["dgvtxtActualCr"].Value;
                    drow["VarianceCr"] = dr.Cells["dgvtxtVarianceCr"].Value;
                    dtbl.Rows.Add(drow);
                }
            }
            catch (Exception ex)
            {
                formMDI.infoError.ErrorString = "BV:03" + ex.Message;
            }
            return dtbl;
        }
        /// <summary>
        /// Function to create datasets
        /// </summary>
        /// <returns></returns>
        public DataSet getdataset()
        {
            FinancialStatementSP spfinancial = new FinancialStatementSP();
            DataSet dsBudget = new DataSet();
            try
            {
                DataTable dtblbudget = GetDataTable();
                DataTable dtblCompany = new DataTable();
                dtblCompany = spfinancial.FundFlowReportPrintCompany(1);
                dsBudget.Tables.Add(dtblbudget);
                dsBudget.Tables.Add(dtblCompany);
            }
            catch (Exception ex)
            {
                formMDI.infoError.ErrorString = "BV:04" + ex.Message;
            }
            return dsBudget;
        }
        /// <summary>
        /// function to print the data
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        public void Print(DateTime fromDate, DateTime toDate)
        {
            try
            {
                DataSet dsBudget = getdataset();
                frmReport frmReport = new frmReport();
                frmReport.MdiParent = formMDI.MDIObj;
                frmReport.Budget(dsBudget);
            }
            catch (Exception ex)
            {
                formMDI.infoError.ErrorString = "BV:05" + ex.Message;
            }
        }
        #endregion
        #region EVENTS
        /// <summary>
        /// Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmBudgetVariance_Load(object sender, EventArgs e)
        {
            try
            {
                BudgetComboFill();
                GridFill();
            }
            catch (Exception ex)
            {
                formMDI.infoError.ErrorString = "BV:06" + ex.Message;
            }
        }
        /// <summary>
        /// Function to fill datagridview on 'Budget' combo selected index change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbBudget_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridFill();
            }
            catch (Exception ex)
            {
                formMDI.infoError.ErrorString = "BV:07" + ex.Message;
            }
        }
        /// <summary>
        /// On 'print' button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (dgvBudgetVariance.Rows.Count > 0)
                {
                    Print(PublicVariables._dtFromDate, PublicVariables._dtToDate);
                }
                else
                {
                    MessageBox.Show("No data found", "Openmiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                formMDI.infoError.ErrorString = "BV:08" + ex.Message;
            }
        }
        #endregion
        #region Navigation
        /// <summary>
        /// On form keypress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmBudgetVariance_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 27)
                {
                    if (PublicVariables.isMessageClose)
                    {
                        Messages.CloseMessage(this);
                    }
                    else
                    {
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                formMDI.infoError.ErrorString = "BV:09" + ex.Message;
            }
        }
        #endregion
    }
}
