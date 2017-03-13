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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Open_Miracle
{
    public partial class frmBudget : Form
    {
        #region Variables
        /// <summary>
        /// Public variable declaration part
        /// </summary>
        ArrayList lstArrOfRemove = new ArrayList();
        BudgetMasterSP spBudgetMaster = new BudgetMasterSP();
        BudgetMasterInfo infoBudgetMaster = new BudgetMasterInfo();
        BudgetDetailsSP spBudgetDetails = new BudgetDetailsSP();
        BudgetDetailsInfo infoBudgetDetails = new BudgetDetailsInfo();
        decimal decBudgetMasterIdentity = 0;
        decimal decBudgetId;
        decimal decTxtTotalDebit = 0;
        decimal decTxtTotalCredit = 0;
        decimal decAmount = 0;
        int inKeyPressCount = 0;
        bool isValueChanged = false;
        int inUpdatingRowIndexForPartyRemove = -1;
        decimal decUpdatingLedgerForPartyremove = 0;
        #endregion
        #region Function
        /// <summary>
        /// Creates instance of frmBudget class
        /// </summary>
        public frmBudget()
        {
            InitializeComponent();
        }
        /// <summary>
        /// function to clear
        /// </summary>
        public void Clear()
        {
            try
            {
                dtpFromDate.Value = PublicVariables._dtCurrentDate;
                dtpFromDate.MinDate = PublicVariables._dtFromDate;
                dtpFromDate.MaxDate = PublicVariables._dtToDate;
                dtpToDate.Value = PublicVariables._dtCurrentDate;
                dtpToDate.MinDate = PublicVariables._dtFromDate;
                dtpToDate.MaxDate = PublicVariables._dtToDate;
                txtFromDate.Text = PublicVariables._dtCurrentDate.ToString("dd-MMM-yyyy");
                txtToDate.Text = PublicVariables._dtCurrentDate.ToString("dd-MMM-yyyy");
                cmbType.SelectedIndex = 0;
                btnSave.Text = "Save";
                btnClear.Text = "Clear";
                btnDelete.Enabled = false;
                txtBudgetName.Clear();
                txtNarration.Clear();
                txtTotalCr.Clear();
                txtTotalDr.Clear();
                dgvBudget.Rows.Clear();
                txtBudgetName.Focus();
            }
            catch (Exception ex)
            {
                formMDI.infoError.ErrorString = "BU1" + ex.Message;
            }
        }
        /// <summary>
        /// function to fill particular combo in datagridview
        /// </summary>
        public void AccountLedgerComboFill()
        {
            try
            {
                DataTable dtbl = new DataTable();
                AccountLedgerSP spaccountledger = new AccountLedgerSP();
                dtbl = spaccountledger.AccountLedgerViewAllForComboBox();
                dgvcmbParticular.DataSource = dtbl;
                dgvcmbParticular.ValueMember = "ledgerId";
                dgvcmbParticular.DisplayMember = "ledgerName";
            }
            catch (Exception ex)
            {
                formMDI.infoError.ErrorString = "BU2" + ex.Message;
            }
        }
        /// <summary>
        /// function to fill particulars combo
        /// </summary>
        public void GroupNameViewForComboFill()
        {
            try
            {
                DataTable dtbl = new DataTable();
                AccountGroupSP spAccountGroup = new AccountGroupSP();
                dtbl = spAccountGroup.GroupNameViewForComboFill();
                dgvcmbParticular.DataSource = dtbl;
                dgvcmbParticular.ValueMember = "accountGroupId";
                dgvcmbParticular.DisplayMember = "accountGroupName";
            }
            catch (Exception ex)
            {
                formMDI.infoError.ErrorString = "BU3" + ex.Message;
            }
        }
        /// <summary>
        /// function to fill type combo
        /// </summary>
        public void AccountLedgerSearchFill()
        {
            try
            {
                DataTable dtbl = new DataTable();
                AccountLedgerSP spaccountledger = new AccountLedgerSP();
                dtbl = spaccountledger.AccountLedgerViewAllForComboBox();
                DataRow dr = dtbl.NewRow();
                dr[1] = "All";
                dtbl.Rows.InsertAt(dr, 0);
                cmbTypeSearch.DataSource = dtbl;
                cmbTypeSearch.ValueMember = "ledgerId";
                cmbTypeSearch.DisplayMember = "ledgerName";
            }
            catch (Exception ex)
            {
                formMDI.infoError.ErrorString = "BU4" + ex.Message;
            }
        }
        /// <summary>
        /// function to fill Dr/Cr combo
        /// </summary>
        public void DrOrCrComboFill()
        {
            try
            {
                dgvcmbDrOrCr.Items.AddRange("Dr", "Cr");
            }
            catch (Exception ex)
            {
                formMDI.infoError.ErrorString = "BU5" + ex.Message;
            }
        }
        /// <summary>
        /// function to generate serial no in datagridview
        /// </summary>
        public void SerialNo()
        {
            try
            {
                int inCount = 1;
                foreach (DataGridViewRow row in dgvBudget.Rows)
                {
                    row.Cells["dgvtxtSlNo"].Value = inCount.ToString();
                    inCount++;
                }
            }
            catch (Exception ex)
            {
                formMDI.infoError.ErrorString = "BU6" + ex.Message;
            }
        }
        /// <summary>
        /// function to fill datagridview
        /// </summary>
        public void GridFill()
        {
            try
            {
                DataTable dtbl = new DataTable();
                dtbl = spBudgetMaster.BudgetSearchGridFill(txtBudgetNameSearch.Text.Trim(), cmbTypeSearch.Text.ToString());
                dgvRegister.DataSource = dtbl;
            }
            catch (Exception ex)
            {
                formMDI.infoError.ErrorString = "BU7" + ex.Message;
            }
        }
        /// <summary>
        /// function to calculate the total debit and credit
        /// </summary>
        public void DebitAndCreditTotal()
        {
            int inRowCount = dgvBudget.RowCount;
            decimal decTxtTotalDebit = 0;
            decimal decTxtTotalCredit = 0;
            try
            {
                for (int inI = 0; inI < inRowCount; inI++)
                {
                    if (dgvBudget.Rows[inI].Cells["dgvcmbDrOrCr"].Value != null && dgvBudget.Rows[inI].Cells["dgvcmbDrOrCr"].Value.ToString() != string.Empty)
                    {
                        if (dgvBudget.Rows[inI].Cells["dgvtxtAmount"].Value != null && dgvBudget.Rows[inI].Cells["dgvtxtAmount"].Value.ToString() != string.Empty)
                        {
                            if (dgvBudget.Rows[inI].Cells["dgvcmbDrOrCr"].Value.ToString() == "Dr")
                            {
                                decAmount = Convert.ToDecimal(dgvBudget.Rows[inI].Cells["dgvtxtAmount"].Value.ToString());
                                decTxtTotalDebit = decTxtTotalDebit + decAmount;
                            }
                            else
                            {
                                decAmount = Convert.ToDecimal(dgvBudget.Rows[inI].Cells["dgvtxtAmount"].Value.ToString());
                                decTxtTotalCredit = decTxtTotalCredit + decAmount;
                            }
                        }
                    }
                    txtTotalDr.Text = Math.Round(decTxtTotalDebit, PublicVariables._inNoOfDecimalPlaces).ToString();
                    txtTotalCr.Text = Math.Round(decTxtTotalCredit, PublicVariables._inNoOfDecimalPlaces).ToString(); ;
                }
            }
            catch (Exception ex)
            {
                formMDI.infoError.ErrorString = "BU8" + ex.Message;
            }
        }
        /// <summary>
        /// function to fill controls for update
        /// </summary>
        public void FillControls()
        {
            try
            {
                dgvBudget.Rows.Clear();
                infoBudgetMaster = spBudgetMaster.BudgetMasterView(decBudgetMasterIdentity);
                txtBudgetName.Text = infoBudgetMaster.BudgetName;
                cmbType.Text = infoBudgetMaster.Type;
                txtTotalCr.Text = infoBudgetMaster.TotalCr.ToString();
                txtTotalDr.Text = infoBudgetMaster.TotalDr.ToString();
                txtFromDate.Text = infoBudgetMaster.FromDate.ToString("dd-MMM-yyyy");
                txtToDate.Text = infoBudgetMaster.ToDate.ToString("dd-MMM-yyyy");
                txtNarration.Text = infoBudgetMaster.Narration;
                decBudgetId = infoBudgetMaster.BudgetMasterId;
                DataTable dtbl = new DataTable();
                dtbl = spBudgetDetails.BudgetDetailsViewByMasterId(decBudgetMasterIdentity);
                for (int i = 0; i < dtbl.Rows.Count; i++)
                {
                    dgvBudget.Rows.Add();
                    dgvBudget.Rows[i].HeaderCell.Value = string.Empty;
                    dgvBudget.Rows[i].Cells["budgetDetailsId"].Value = Convert.ToDecimal(dtbl.Rows[i]["budgetDetailsId"].ToString());
                    if (dgvBudget.Rows[i].Cells["dgvcmbParticular"].Value == null)
                    {
                        dgvBudget.Rows[i].Cells["dgvcmbParticular"].Value = Convert.ToDecimal(dtbl.Rows[i]["ledgerId"].ToString());
                    }
                    if (Convert.ToDecimal(dtbl.Rows[i]["debit"].ToString()) == 0)
                    {
                        dgvBudget.Rows[i].Cells["dgvcmbDrOrCr"].Value = "Cr";
                        dgvBudget.Rows[i].Cells["dgvtxtAmount"].Value = Convert.ToDecimal(dtbl.Rows[i]["credit"].ToString());
                    }
                    else
                    {
                        dgvBudget.Rows[i].Cells["dgvcmbDrOrCr"].Value = "Dr";
                        dgvBudget.Rows[i].Cells["dgvtxtAmount"].Value = Convert.ToDecimal(dtbl.Rows[i]["debit"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                formMDI.infoError.ErrorString = "BU9" + ex.Message;
            }
        }
        /// <summary>
        /// function to save
        /// </summary>
        public void SaveFunction()
        {
            try
            {
                decimal decTotalDebit = 0;
                decimal decTotalCredit = 0;
                decTotalDebit = Convert.ToDecimal(txtTotalDr.Text.Trim());
                decTotalCredit = Convert.ToDecimal(txtTotalCr.Text.Trim());
                infoBudgetMaster.BudgetName = txtBudgetName.Text.Trim();
                if (cmbType.Text == "Account Ledger")
                {
                    infoBudgetMaster.Type = cmbType.Text;
                }
                if (cmbType.Text == "Account Group")
                {
                    infoBudgetMaster.Type = cmbType.Text;
                }
                infoBudgetMaster.FromDate = Convert.ToDateTime(txtFromDate.Text);
                infoBudgetMaster.ToDate = Convert.ToDateTime(txtToDate.Text);
                infoBudgetMaster.TotalCr = decTotalCredit;
                infoBudgetMaster.TotalDr = decTotalDebit;
                infoBudgetMaster.Narration = txtNarration.Text.Trim();
                infoBudgetMaster.Extra1 = string.Empty;
                infoBudgetMaster.Extra2 = string.Empty;
                {
                    decBudgetMasterIdentity = spBudgetMaster.BudgetMasterAdd(infoBudgetMaster);
                }
                infoBudgetDetails.BudgetMasterId = decBudgetMasterIdentity;
                infoBudgetDetails.Extra1 = string.Empty;
                infoBudgetDetails.Extra2 = string.Empty;
                decimal decDebit = 0;
                decimal decCredit = 0;
                int inRowCount = dgvBudget.RowCount;
                for (int inI = 0; inI < inRowCount - 1; inI++)
                {
                    if (dgvBudget.Rows[inI].Cells["dgvcmbParticular"].Value != null && dgvBudget.Rows[inI].Cells["dgvcmbParticular"].Value.ToString() != string.Empty)
                    {
                        infoBudgetDetails.Particular = Convert.ToString(dgvBudget.Rows[inI].Cells["dgvcmbParticular"].FormattedValue.ToString());
                    }
                    if (dgvBudget.Rows[inI].Cells["dgvcmbDrOrCr"].Value != null && dgvBudget.Rows[inI].Cells["dgvcmbDrOrCr"].Value.ToString() != string.Empty)
                    {
                        if (dgvBudget.Rows[inI].Cells["dgvtxtAmount"].Value != null && dgvBudget.Rows[inI].Cells["dgvtxtAmount"].Value.ToString() != string.Empty)
                        {
                            decAmount = Convert.ToDecimal(dgvBudget.Rows[inI].Cells["dgvtxtAmount"].Value.ToString());
                            decTxtTotalCredit = decTxtTotalCredit + decAmount;
                            if (dgvBudget.Rows[inI].Cells["dgvcmbDrOrCr"].Value.ToString() == "Dr")
                            {
                                infoBudgetDetails.Debit = Convert.ToDecimal(dgvBudget.Rows[inI].Cells["dgvtxtAmount"].Value.ToString());
                                infoBudgetDetails.Credit = 0;
                                decCredit = infoBudgetDetails.Credit;
                            }
                            else
                            {
                                infoBudgetDetails.Credit = Convert.ToDecimal(dgvBudget.Rows[inI].Cells["dgvtxtAmount"].Value.ToString());
                                infoBudgetDetails.Debit = 0;
                                decDebit = infoBudgetDetails.Debit;
                            }
                            spBudgetDetails.BudgetDetailsAdd(infoBudgetDetails);
                        }
                    }
                }
                Messages.SavedMessage();
                Clear();
                GridFill();
            }
            catch (Exception ex)
            {
                formMDI.infoError.ErrorString = "BU10" + ex.Message;
            }
        }
        /// <summary>
        /// function to edit
        /// </summary>
        public void EditFunction()
        {
            try
            {
                BudgetDetailsDeleteByBudgetDetailsId();
                infoBudgetMaster.BudgetMasterId = decBudgetMasterIdentity;
                infoBudgetMaster.BudgetName = txtBudgetName.Text.Trim();
                infoBudgetMaster.Type = cmbType.Text;
                infoBudgetMaster.FromDate = Convert.ToDateTime(txtFromDate.Text.ToString());
                infoBudgetMaster.ToDate = Convert.ToDateTime(txtToDate.Text.ToString());
                infoBudgetMaster.TotalCr = Convert.ToDecimal(txtTotalCr.Text.ToString());
                infoBudgetMaster.TotalDr = Convert.ToDecimal(txtTotalDr.Text.ToString());
                infoBudgetMaster.Narration = txtNarration.Text.Trim();
                infoBudgetMaster.Extra1 = string.Empty;
                infoBudgetMaster.Extra2 = string.Empty;
                spBudgetMaster.BudgetMasterEdit(infoBudgetMaster);
                decimal decDebit = 0;
                decimal decCredit = 0;
                int inRowCount = dgvBudget.RowCount;
                for (int inI = 0; inI < inRowCount - 1; inI++)
                {
                    if (dgvBudget.Rows[inI].Cells["budgetDetailsId"].Value == null || dgvBudget.Rows[inI].Cells["budgetDetailsId"].Value.ToString() == string.Empty)
                    {
                        infoBudgetDetails.BudgetMasterId = decBudgetMasterIdentity;
                        if (dgvBudget.Rows[inI].Cells["dgvcmbParticular"].Value != null && dgvBudget.Rows[inI].Cells["dgvcmbParticular"].Value.ToString() != string.Empty)
                        {
                            infoBudgetDetails.Particular = Convert.ToString(dgvBudget.Rows[inI].Cells["dgvcmbParticular"].FormattedValue.ToString());
                        }
                        if (dgvBudget.Rows[inI].Cells["dgvcmbDrOrCr"].Value != null && dgvBudget.Rows[inI].Cells["dgvcmbDrOrCr"].Value.ToString() != string.Empty)
                        {
                            decAmount = Convert.ToDecimal(dgvBudget.Rows[inI].Cells["dgvtxtAmount"].Value.ToString());
                            if (dgvBudget.Rows[inI].Cells["dgvcmbDrOrCr"].Value.ToString() == "Dr")
                            {
                                infoBudgetDetails.Debit = Convert.ToDecimal(dgvBudget.Rows[inI].Cells["dgvtxtAmount"].Value.ToString());
                                infoBudgetDetails.Credit = 0;
                                decDebit = decTxtTotalDebit + decAmount;
                                decCredit = infoBudgetDetails.Credit;
                            }
                            else
                            {
                                infoBudgetDetails.Credit = Convert.ToDecimal(dgvBudget.Rows[inI].Cells["dgvtxtAmount"].Value.ToString());
                                infoBudgetDetails.Debit = 0;
                                decCredit = decTxtTotalCredit + decAmount;
                                decDebit = infoBudgetDetails.Debit;
                            }
                        }
                        infoBudgetDetails.Extra1 = string.Empty;
                        infoBudgetDetails.Extra2 = string.Empty;
                        spBudgetDetails.BudgetDetailsAdd(infoBudgetDetails);
                    }
                    else
                    {
                        infoBudgetDetails.BudgetMasterId = decBudgetMasterIdentity;
                        infoBudgetDetails.BudgetDetailsId = Convert.ToDecimal(dgvBudget.Rows[inI].Cells["budgetDetailsId"].Value);
                        if (dgvBudget.Rows[inI].Cells["dgvcmbParticular"].Value != null && dgvBudget.Rows[inI].Cells["dgvcmbParticular"].Value.ToString() != string.Empty)
                        {
                            infoBudgetDetails.Particular = Convert.ToString(dgvBudget.Rows[inI].Cells["dgvcmbParticular"].FormattedValue.ToString());
                        }
                        if (dgvBudget.Rows[inI].Cells["dgvcmbDrOrCr"].Value != null && dgvBudget.Rows[inI].Cells["dgvcmbDrOrCr"].Value.ToString() != string.Empty)
                        {
                            decAmount = Convert.ToDecimal(dgvBudget.Rows[inI].Cells["dgvtxtAmount"].Value.ToString());
                            if (dgvBudget.Rows[inI].Cells["dgvcmbDrOrCr"].Value.ToString() == "Dr")
                            {
                                infoBudgetDetails.Debit = Convert.ToDecimal(dgvBudget.Rows[inI].Cells["dgvtxtAmount"].Value.ToString());
                                infoBudgetDetails.Credit = 0;
                                decDebit = decTxtTotalDebit + decAmount;
                                decCredit = infoBudgetDetails.Credit;
                            }
                            else
                            {
                                infoBudgetDetails.Credit = Convert.ToDecimal(dgvBudget.Rows[inI].Cells["dgvtxtAmount"].Value.ToString());
                                infoBudgetDetails.Debit = 0;
                                decCredit = decTxtTotalCredit + decAmount;
                                decDebit = infoBudgetDetails.Debit;
                            }
                        }
                        infoBudgetDetails.Extra1 = string.Empty;
                        infoBudgetDetails.Extra2 = string.Empty;
                        spBudgetDetails.BudgetDetailsEdit(infoBudgetDetails);
                    }
                }
                Messages.UpdatedMessage();
                Clear();
                GridFill();
            }
            catch (Exception ex)
            {
                formMDI.infoError.ErrorString = "BU11" + ex.Message;
            }
        }
        /// <summary>
        /// function to remove incompleted rows from the datagridview
        /// </summary>
        /// <returns></returns>
        public bool RemoveIncompleteRowsFromGrid()
        {
            bool isOk = true;
            try
            {
                string strMessage = "Rows";
                int inC = 0, inForFirst = 0;
                int inRowcount = dgvBudget.RowCount;
                int inLastRow = 1;//To eliminate last row from checking
                foreach (DataGridViewRow dgvrowCur in dgvBudget.Rows)
                {
                    if (inLastRow < inRowcount)
                    {
                        if (dgvrowCur.HeaderCell.Value != null)
                        {
                            if (dgvrowCur.HeaderCell.Value.ToString() == "X" || dgvrowCur.Cells["dgvcmbParticular"].Value == null)
                            {
                                isOk = false;
                                if (inC == 0)
                                {
                                    strMessage = strMessage + Convert.ToString(dgvrowCur.Index + 1);
                                    inForFirst = dgvrowCur.Index;
                                    inC++;
                                }
                                else
                                {
                                    strMessage = strMessage + ", " + Convert.ToString(dgvrowCur.Index + 1);
                                }
                            }
                        }
                    }
                    inLastRow++;
                }
                inLastRow = 1;
                if (!isOk)
                {
                    strMessage = strMessage + " contains invalid entries. Do you want to continue?";
                    if (MessageBox.Show(strMessage, "Openmiracle", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        isOk = true;
                        for (int inK = 0; inK < dgvBudget.Rows.Count; inK++)
                        {
                            if (dgvBudget.Rows[inK].HeaderCell.Value != null && dgvBudget.Rows[inK].HeaderCell.Value.ToString() == "X")
                            {
                                if (!dgvBudget.Rows[inK].IsNewRow)
                                {
                                    dgvBudget.Rows.RemoveAt(inK);
                                    inK--;
                                }
                            }
                        }
                    }
                    else
                    {
                        dgvBudget.Rows[inForFirst].Cells["dgvcmbParticular"].Selected = true;
                        dgvBudget.CurrentCell = dgvBudget.Rows[inForFirst].Cells["dgvcmbParticular"];
                        dgvBudget.Focus();
                    }
                }
                SerialNo();
            }
            catch (Exception ex)
            {
                formMDI.infoError.ErrorString = "BU12" + ex.Message;
            }
            return isOk;
        }
        /// <summary>
        /// function to check invalid entries in datagridview
        /// </summary>
        /// <param name="e"></param>
        public void CheckInvalidEntries(DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvBudget.CurrentRow != null)
                {
                    if (!isValueChanged)
                    {
                        if (dgvBudget.CurrentRow.Cells["dgvcmbParticular"].Value == null || dgvBudget.CurrentRow.Cells["dgvcmbParticular"].Value.ToString().Trim() == "")
                        {
                            isValueChanged = true;
                            dgvBudget.CurrentRow.HeaderCell.Value = "X";
                            dgvBudget.CurrentRow.HeaderCell.Style.ForeColor = Color.Red;
                        }
                        else if (dgvBudget.CurrentRow.Cells["dgvcmbDrOrCr"].Value == null || dgvBudget.CurrentRow.Cells["dgvcmbDrOrCr"].Value.ToString().Trim() == "")
                        {
                            isValueChanged = true;
                            dgvBudget.CurrentRow.HeaderCell.Value = "X";
                            dgvBudget.CurrentRow.HeaderCell.Style.ForeColor = Color.Red;
                        }
                        else
                        {
                            isValueChanged = true;
                            dgvBudget.CurrentRow.HeaderCell.Value = String.Empty;
                        }
                    }
                    isValueChanged = false;
                }
            }
            catch (Exception ex)
            {
                formMDI.infoError.ErrorString = "BU13" + ex.Message;
            }
        }
        /// <summary>
        /// function to call save
        /// </summary>
        public void SaveOrEdit()
        {
            try
            {
                dgvBudget.ClearSelection();
                int inRow = dgvBudget.RowCount;
                if (txtBudgetName.Text.Trim() == string.Empty)
                {
                    Messages.InformationMessage("Enter budget name");
                    txtBudgetName.Focus();
                }
                else if (spBudgetMaster.BudgetCheckExistanceOfName(txtBudgetName.Text.Trim(), 0) == true && btnSave.Text == "Save")
                {
                    Messages.InformationMessage("Budget name already exist");
                    txtBudgetName.Focus();
                }
                else if (txtFromDate.Text.Trim() == string.Empty)
                {
                    Messages.InformationMessage("Select a date in between financial year");
                    txtFromDate.Focus();
                }
                else if (txtToDate.Text.Trim() == string.Empty)
                {
                    Messages.InformationMessage("Select a date in between financial year");
                    txtToDate.Focus();
                }
                else if (DateTime.Parse(txtToDate.Text) < DateTime.Parse(txtFromDate.Text))
                {
                    Messages.InformationMessage("From date should not greater than to date ");
                    txtToDate.Focus();
                }
                else if (inRow - 1 == 0)
                {
                    Messages.InformationMessage("Can't save budget without atleast one account ledger with complete details");
                }
                else
                    if (inRow > 1)
                    {
                        if (dgvBudget.Rows[0].Cells["dgvtxtAmount"].Value == null && dgvBudget.Rows[0].Cells["dgvtxtAmount"].Value == null)
                        {
                            MessageBox.Show("Can't save budget without atleast one particular with complete details", "Openmiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            if (RemoveIncompleteRowsFromGrid())
                            {
                                if (dgvBudget.Rows[0].Cells["dgvcmbParticular"].Value == null && dgvBudget.Rows[0].Cells["dgvcmbParticular"].Value == null)
                                {
                                    MessageBox.Show("Can't save budget without atleast one particular with complete details", "Openmiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    dgvBudget.ClearSelection();
                                    dgvBudget.Focus();
                                }
                                if (btnSave.Text == "Save")
                                {
                                    if (dgvBudget.Rows[0].Cells["dgvcmbParticular"].Value == null)
                                    {
                                        MessageBox.Show("Can't save budget without atleast one particular with complete details", "Openmiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        dgvBudget.ClearSelection();
                                        dgvBudget.Focus();
                                    }
                                    else
                                    {
                                        if (PublicVariables.isMessageAdd)
                                        {
                                            if (Messages.SaveMessage())
                                            {
                                                SaveFunction();
                                            }
                                        }
                                        else
                                        {
                                            SaveFunction();
                                        }
                                    }
                                }
                                else
                                {
                                    if (PublicVariables.isMessageEdit)
                                    {
                                        if (Messages.UpdateMessage())
                                        {
                                            EditFunction();
                                        }
                                    }
                                    else
                                    {
                                        EditFunction();
                                    }
                                }
                            }
                        }
                    }
            }
            catch (Exception ex)
            {
                formMDI.infoError.ErrorString = "BU14" + ex.Message;
            }
        }
        /// <summary>
        /// function to call delete
        /// </summary>
        public void Delete()
        {
            try
            {
                if (PublicVariables.isMessageDelete)
                {
                    if (Messages.DeleteMessage())
                    {
                        DeleteFunction();
                    }
                }
                else
                {
                    DeleteFunction();
                }
            }
            catch (Exception ex)
            {
                formMDI.infoError.ErrorString = "BU15" + ex.Message;
            }
        }
        /// <summary>
        /// function to delete
        /// </summary>
        public void DeleteFunction()
        {
            try
            {
                if (spBudgetMaster.BudgetMasterDelete(decBudgetMasterIdentity) == -1)
                {
                    Messages.ReferenceExistsMessage();
                }
                else
                {
                    Messages.DeletedMessage();
                    Clear();
                    GridFill();
                }
            }
            catch (Exception ex)
            {
                formMDI.infoError.ErrorString = "BU16" + ex.Message;
            }
        }
        /// <summary>
        /// function to remove a row from the datagridview
        /// </summary>
        public void RemoveFunction()
        {
            try
            {
                int inRowCount = dgvBudget.RowCount;
                int inAddIndex = dgvBudget.CurrentRow.Index;
                if (inRowCount > 0)
                {
                    dgvBudget.Rows.RemoveAt(inAddIndex);
                }
                SerialNo();
            }
            catch (Exception ex)
            {
                formMDI.infoError.ErrorString = "BU17" + ex.Message;
            }
        }
        /// <summary>
        /// function to delete 
        /// </summary>
        public void BudgetDetailsDeleteByBudgetDetailsId()
        {
            try
            {
                foreach (var strId in lstArrOfRemove)
                {
                    decimal decDeleteId = Convert.ToDecimal(strId);
                    spBudgetDetails.BudgetDetailsDelete(decDeleteId);
                }
            }
            catch (Exception ex)
            {
                formMDI.infoError.ErrorString = "BU18" + ex.Message;
            }
        }
        #endregion
        #region Events
        /// <summary>
        /// load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmBudget_Load(object sender, EventArgs e)
        {
            try
            {
                Clear();
                DrOrCrComboFill();
                cmbTypeSearch.SelectedIndex = 0;
                GridFill();
            }
            catch (Exception ex)
            {
                formMDI.infoError.ErrorString = "BU19" + ex.Message;
            }
        }
        /// <summary>
        /// 'date' textbox leave
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtToDate_Leave(object sender, EventArgs e)
        {
            try
            {
                DateValidation objVal = new DateValidation();
                bool isInvalid = objVal.DateValidationFunction(txtToDate);
                if (!isInvalid)
                {
                    txtToDate.Text = PublicVariables._dtCurrentDate.ToString("dd-MMM-yyyy");
                }
                dtpToDate.Value = Convert.ToDateTime(txtToDate.Text);
            }
            catch (Exception ex)
            {
                formMDI.infoError.ErrorString = "BU20" + ex.Message;
            }
        }
        /// <summary>
        /// On 'close' button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFrmClose_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                formMDI.infoError.ErrorString = "BU21" + ex.Message;
            }
        }
        /// <summary>
        /// On 'From date' datetime picker value changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtpFromDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime datetime = this.dtpFromDate.Value;
                txtFromDate.Text = datetime.ToString("dd-MMMM-yyyy");
                txtFromDate.Focus();
            }
            catch (Exception ex)
            {
                formMDI.infoError.ErrorString = "BU22" + ex.Message;
            }
        }
        /// <summary>
        /// On 'Todate' datetime picker value changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtpToDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime date = this.dtpToDate.Value;
                this.txtToDate.Text = date.ToString("dd-MMM-yyyy");
                txtToDate.Focus();
            }
            catch (Exception ex)
            {
                formMDI.infoError.ErrorString = "BU23" + ex.Message;
            }
        }
        /// <summary>
        /// Serial number generation On datagridview rowws added
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvBudget_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                SerialNo();
            }
            catch (Exception ex)
            {
                formMDI.infoError.ErrorString = "BU24" + ex.Message;
            }
        }
        /// <summary>
        ///Calculating total On 'Budget' datagridview's cell value changed 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvBudget_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                {
                    if (e.RowIndex != -1 && e.ColumnIndex != -1)
                    {
                        DebitAndCreditTotal();
                    }
                    CheckInvalidEntries(e);
                }
            }
            catch (Exception ex)
            {
                formMDI.infoError.ErrorString = "BU25" + ex.Message;
            }
        }
        /// <summary>
        /// On 'Save' button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckUserPrivilege.PrivilegeCheck(PublicVariables._decCurrentUserId, this.Name, btnSave.Text))
                {
                    {
                        SaveOrEdit();
                    }
                }
                else
                {
                    Messages.NoPrivillageMessage();
                }
            }
            catch (Exception ex)
            {
                formMDI.infoError.ErrorString = "BU26" + ex.Message;
            }
        }
        /// <summary>
        /// On 'Clear' button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
            }
            catch (Exception ex)
            {
                formMDI.infoError.ErrorString = "BU27" + ex.Message;
            }
        }
        /// <summary>
        /// On 'Close' button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                formMDI.infoError.ErrorString = "BU28" + ex.Message;
            }
        }
        /// <summary>
        /// On 'Search' button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                GridFill();
            }
            catch (Exception ex)
            {
                formMDI.infoError.ErrorString = "BU29" + ex.Message;
            }
        }
        /// <summary>
        /// On 'Clear' button in Search click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            try
            {
                txtBudgetNameSearch.Clear();
                cmbTypeSearch.SelectedIndex = 0;
                GridFill();
                txtBudgetNameSearch.Focus();
            }
            catch (Exception ex)
            {
                formMDI.infoError.ErrorString = "BU30" + ex.Message;
            }
        }
        /// <summary>
        /// Fill controls on cell double click in datagridview for update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvRegister_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                    decBudgetMasterIdentity = Convert.ToDecimal(dgvRegister.CurrentRow.Cells["budgetMasterId"].Value.ToString());
                    FillControls();
                    btnSave.Text = "Update";
                    btnDelete.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                formMDI.infoError.ErrorString = "BU31" + ex.Message;
            }
        }
        /// <summary>
        /// On 'delete' button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckUserPrivilege.PrivilegeCheck(PublicVariables._decCurrentUserId, this.Name, "Delete"))
                {
                    Delete();
                }
                else
                {
                    Messages.NoPrivillageMessage();
                }
            }
            catch (Exception ex)
            {
                formMDI.infoError.ErrorString = "BU32" + ex.Message;
            }
        }
        /// <summary>
        ///filling particulars combo On'Type' Combo selected value change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbType_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbType.Text == "Account Ledger")
                {
                    AccountLedgerComboFill();
                }
                if (cmbType.Text == "Account Group")
                {
                    GroupNameViewForComboFill();
                }
            }
            catch (Exception ex)
            {
                formMDI.infoError.ErrorString = "BU33" + ex.Message;
            }
        }
        /// <summary>
        /// decimal validation on 'Amount' textbox keypresss 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvtxtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (dgvBudget.CurrentCell != null)
                {
                    if (dgvBudget.Columns[dgvBudget.CurrentCell.ColumnIndex].Name == "dgvtxtAmount")
                    {
                        Common.DecimalValidation(sender, e, false);
                    }
                }
            }
            catch (Exception ex)
            {
                formMDI.infoError.ErrorString = "BU34" + ex.Message;
            }
        }
        /// <summary>
        /// Datagridview editing controlshowing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvBudget_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                DataGridViewTextBoxEditingControl txt = e.Control as DataGridViewTextBoxEditingControl;
                if (dgvBudget.CurrentCell.ColumnIndex == dgvBudget.Columns["dgvtxtAmount"].Index)
                {
                    txt.KeyPress += dgvtxtAmount_KeyPress;
                }
            }
            catch (Exception ex)
            {
                formMDI.infoError.ErrorString = "BU35" + ex.Message;
            }
        }
        /// <summary>
        /// data eroor for datagridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvBudget_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
                if (e.Exception.Message == "DataGridViewComboBoxCell value is not valid.")
                {
                    object value = dgvBudget.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    if (!((DataGridViewComboBoxColumn)dgvBudget.Columns[e.ColumnIndex]).Items.Contains(value))
                    {
                        e.ThrowException = false;
                    }
                }
            }
            catch (Exception ex)
            {
                formMDI.infoError.ErrorString = "BU36" + ex.Message;
            }
        }
        /// <summary>
        /// datagridview cell beginend edit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvBudget_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                inUpdatingRowIndexForPartyRemove = -1;
                decUpdatingLedgerForPartyremove = 0;
                DataTable dtbl = new DataTable();
                AccountLedgerSP SpAccountLedger = new AccountLedgerSP();
                if (cmbType.SelectedIndex == 0)
                {
                    if (dgvBudget.CurrentCell.ColumnIndex == dgvBudget.Columns["dgvcmbParticular"].Index)
                    {
                        dtbl = SpAccountLedger.AccountLedgerViewAll();
                        DataRow dr = dtbl.NewRow();
                        dr[0] = 0;
                        dr[2] = string.Empty;
                        dtbl.Rows.InsertAt(dr, 0);
                        if (dtbl.Rows.Count > 0)
                        {
                            if (dgvBudget.RowCount > 1)
                            {
                                int inGridRowCount = dgvBudget.RowCount;
                                for (int inI = 0; inI < inGridRowCount - 1; inI++)
                                {
                                    if (inI != e.RowIndex)
                                    {
                                        int inTableRowcount = dtbl.Rows.Count;
                                        for (int inJ = 0; inJ < inTableRowcount; inJ++)
                                        {
                                            if (dgvBudget.Rows[inI].Cells["dgvcmbParticular"].Value != null && dgvBudget.Rows[inI].Cells["dgvcmbParticular"].Value.ToString() != string.Empty)
                                            {
                                                if (dtbl.Rows[inJ]["ledgerId"].ToString() == dgvBudget.Rows[inI].Cells["dgvcmbParticular"].Value.ToString())
                                                {
                                                    dtbl.Rows.RemoveAt(inJ);
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            DataGridViewComboBoxCell dgvccVoucherType = (DataGridViewComboBoxCell)dgvBudget[dgvBudget.Columns["dgvcmbParticular"].Index, e.RowIndex];
                            dgvccVoucherType.DataSource = dtbl;
                            dgvccVoucherType.ValueMember = "ledgerId";
                            dgvccVoucherType.DisplayMember = "ledgerName";
                        }
                    }
                    if (dgvBudget.Rows[e.RowIndex].Cells[e.ColumnIndex].OwningColumn.Name == "dgvcmbParticular")
                    {
                        if (dgvBudget.Rows[e.RowIndex].Cells["dgvcmbParticular"].Value != null && dgvBudget.Rows[e.RowIndex].Cells["dgvcmbParticular"].Value.ToString() != string.Empty)
                        {
                            AccountLedgerSP spAccountLedger = new AccountLedgerSP();
                            if (spAccountLedger.AccountGroupIdCheck(dgvBudget.Rows[e.RowIndex].Cells["dgvcmbParticular"].FormattedValue.ToString()))
                            {
                                inUpdatingRowIndexForPartyRemove = e.RowIndex;
                                decUpdatingLedgerForPartyremove = Convert.ToDecimal(dgvBudget.Rows[e.RowIndex].Cells["dgvcmbParticular"].Value.ToString());
                            }
                        }
                    }
                    if (dgvBudget.Rows[e.RowIndex].Cells[e.ColumnIndex].OwningColumn.Name == "dgvcmbDrOrCr")
                    {
                        if (dgvBudget.Rows[e.RowIndex].Cells["dgvcmbParticular"].Value != null && dgvBudget.Rows[e.RowIndex].Cells["dgvcmbParticular"].Value.ToString() != string.Empty)
                        {
                            AccountLedgerSP spAccountLedger = new AccountLedgerSP();
                            if (spAccountLedger.AccountGroupIdCheck(dgvBudget.Rows[e.RowIndex].Cells["dgvcmbParticular"].FormattedValue.ToString()))
                            {
                                inUpdatingRowIndexForPartyRemove = e.RowIndex;
                                decUpdatingLedgerForPartyremove = Convert.ToDecimal(dgvBudget.Rows[e.RowIndex].Cells["dgvcmbParticular"].Value.ToString());
                            }
                        }
                    }
                }
                else
                {
                    if (dgvBudget.CurrentCell.ColumnIndex == dgvBudget.Columns["dgvcmbParticular"].Index)
                    {
                        AccountGroupSP spAccountGroup = new AccountGroupSP();
                        dtbl = spAccountGroup.GroupNameViewForComboFill();
                        DataRow dr = dtbl.NewRow();
                        dr[0] = 0;
                        dtbl.Rows.InsertAt(dr, 0);
                        if (dtbl.Rows.Count > 0)
                        {
                            if (dgvBudget.RowCount > 1)
                            {
                                int inGridRowCount = dgvBudget.RowCount;
                                for (int inI = 0; inI < inGridRowCount - 1; inI++)
                                {
                                    if (inI != e.RowIndex)
                                    {
                                        int inTableRowcount = dtbl.Rows.Count;
                                        for (int inJ = 0; inJ < inTableRowcount; inJ++)
                                        {
                                            if (dgvBudget.Rows[inI].Cells["dgvcmbParticular"].Value != null && dgvBudget.Rows[inI].Cells["dgvcmbParticular"].Value.ToString() != string.Empty)
                                            {
                                                if (dtbl.Rows[inJ]["accountGroupName"].ToString() == dgvBudget.Rows[inI].Cells["dgvcmbParticular"].Value.ToString())
                                                {
                                                    dtbl.Rows.RemoveAt(inJ);
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            DataGridViewComboBoxCell dgvccVoucherType = (DataGridViewComboBoxCell)dgvBudget[dgvBudget.Columns["dgvcmbParticular"].Index, e.RowIndex];
                            dgvccVoucherType.DataSource = dtbl;
                            dgvccVoucherType.ValueMember = "accountGroupId";
                            dgvccVoucherType.DisplayMember = "accountGroupName";
                        }
                    }
                    if (dgvBudget.Rows[e.RowIndex].Cells[e.ColumnIndex].OwningColumn.Name == "dgvcmbParticular")
                    {
                        if (dgvBudget.Rows[e.RowIndex].Cells["dgvcmbParticular"].Value != null && dgvBudget.Rows[e.RowIndex].Cells["dgvcmbParticular"].Value.ToString() != string.Empty)
                        {
                            AccountLedgerSP spAccountLedger = new AccountLedgerSP();
                            if (spAccountLedger.AccountGroupIdCheck(dgvBudget.Rows[e.RowIndex].Cells["dgvcmbParticular"].FormattedValue.ToString()))
                            {
                                inUpdatingRowIndexForPartyRemove = e.RowIndex;
                                decUpdatingLedgerForPartyremove = Convert.ToDecimal(dgvBudget.Rows[e.RowIndex].Cells["dgvcmbParticular"].Value.ToString());
                            }
                        }
                    }
                    if (dgvBudget.Rows[e.RowIndex].Cells[e.ColumnIndex].OwningColumn.Name == "dgvcmbDrOrCr")
                    {
                        if (dgvBudget.Rows[e.RowIndex].Cells["dgvcmbParticular"].Value != null && dgvBudget.Rows[e.RowIndex].Cells["dgvcmbParticular"].Value.ToString() != string.Empty)
                        {
                            AccountLedgerSP spAccountLedger = new AccountLedgerSP();
                            if (spAccountLedger.AccountGroupIdCheck(dgvBudget.Rows[e.RowIndex].Cells["dgvcmbParticular"].FormattedValue.ToString()))
                            {
                                inUpdatingRowIndexForPartyRemove = e.RowIndex;
                                decUpdatingLedgerForPartyremove = Convert.ToDecimal(dgvBudget.Rows[e.RowIndex].Cells["dgvcmbParticular"].Value.ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                formMDI.infoError.ErrorString = "BU37" + ex.Message;
            }
        }
        /// <summary>
        /// Commit on datagridview cell dirtystate changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvBudget_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvBudget.IsCurrentCellDirty)
                {
                    dgvBudget.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
            }
            catch (Exception ex)
            {
                formMDI.infoError.ErrorString = "BU38" + ex.Message;
            }
        }
        /// <summary>
        /// On 'Remove' link click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnklblRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (dgvBudget.SelectedCells.Count > 0 && dgvBudget.CurrentRow != null)
                {
                    if (!dgvBudget.Rows[dgvBudget.CurrentRow.Index].IsNewRow)
                    {
                        if (MessageBox.Show("Do you want to remove current row ?", "Openmiracle", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            if (btnSave.Text == "Update")
                            {
                                if (dgvBudget.CurrentRow.Cells["budgetDetailsId"].Value != null && dgvBudget.CurrentRow.Cells["budgetDetailsId"].Value.ToString() != "")
                                {
                                    lstArrOfRemove.Add(dgvBudget.CurrentRow.Cells["budgetDetailsId"].Value.ToString());
                                    RemoveFunction();
                                    DebitAndCreditTotal();
                                }
                                else
                                {
                                    RemoveFunction();
                                    DebitAndCreditTotal();
                                }
                            }
                            else
                            {
                                RemoveFunction();
                                DebitAndCreditTotal();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                formMDI.infoError.ErrorString = "BU39" + ex.Message;
            }
        }
        /// <summary>
        /// datagridview cell enter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvBudget_CellEnter_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvBudget.Rows[e.RowIndex].Cells[e.ColumnIndex] is DataGridViewComboBoxCell)
                {
                    dgvBudget.EditMode = DataGridViewEditMode.EditOnEnter;
                }
                else
                {
                    dgvBudget.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
                }
            }
            catch (Exception ex)
            {
                formMDI.infoError.ErrorString = "BU40" + ex.Message;
            }
        }
        #endregion
        #region Navigation
        /// <summary>
        /// Form keydown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmBudget_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Escape)
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
                if (e.Control && e.KeyCode == Keys.S)
                {
                    btnSave.PerformClick();
                }
                if (e.Control && e.KeyCode == Keys.D)
                {
                    if (btnDelete.Enabled)
                    {
                        btnDelete.PerformClick();
                    }
                }
            }
            catch (Exception ex)
            {
                formMDI.infoError.ErrorString = "BU41" + ex.Message;
            }
        }
        /// <summary>
        /// 'BudgetName' TextBox keydown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBudgetName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    cmbType.Focus();
                }
            }
            catch (Exception ex)
            {
                formMDI.infoError.ErrorString = "BU42" + ex.Message;
            }
        }
        /// <summary>
        /// 'Type' combo keydown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbType_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtFromDate.SelectionStart = txtFromDate.Text.Length;
                    txtFromDate.Focus();
                }
                if (e.KeyCode == Keys.Back)
                {
                    txtBudgetName.Focus();
                }
            }
            catch (Exception ex)
            {
                formMDI.infoError.ErrorString = "BU43" + ex.Message;
            }
        }
        /// <summary>
        /// 'fromdate' textbox keydown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtFromDate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtToDate.SelectionStart = txtToDate.Text.Length;
                    txtToDate.Focus();
                }
                if (e.KeyCode == Keys.Back)
                {
                    if (txtFromDate.SelectionLength != 11)
                    {
                        if (txtFromDate.Text == string.Empty || txtFromDate.SelectionStart == 0)
                        {
                            cmbType.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                formMDI.infoError.ErrorString = "BU44" + ex.Message;
            }
        }
        /// <summary>
        /// 'Todate' textbox keydown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtToDate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (dgvBudget.Rows.Count > 0)
                    {
                        dgvBudget.Focus();
                        dgvBudget.CurrentCell = dgvBudget.Rows[0].Cells["dgvtxtSlNo"];
                    }
                }
                if (e.KeyCode == Keys.Back)
                {
                    if (txtToDate.SelectionLength != 11)
                    {
                        if (txtToDate.Text == string.Empty || txtToDate.SelectionStart == 0)
                        {
                            txtFromDate.Focus();
                            txtFromDate.SelectionStart = 0;
                            txtFromDate.SelectionLength = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                formMDI.infoError.ErrorString = "BU45" + ex.Message;
            }
        }
        /// <summary>
        /// datagridview keydown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvBudget_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                int inDgvProductRowCount = dgvBudget.Rows.Count;
                if (e.KeyCode == Keys.Enter)
                {
                    if (dgvBudget.CurrentCell == dgvBudget.Rows[inDgvProductRowCount - 1].Cells["dgvtxtAmount"])
                    {
                        txtNarration.Focus();
                        txtNarration.SelectionStart = txtNarration.TextLength;
                        dgvBudget.ClearSelection();
                    }
                }
                if (e.KeyCode == Keys.Back)
                {
                    if (dgvBudget.CurrentCell == dgvBudget.Rows[0].Cells["dgvtxtSlNo"])
                    {
                        txtToDate.SelectionStart = 0;
                        txtToDate.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                formMDI.infoError.ErrorString = "BU46" + ex.Message;
            }
        }
        /// <summary>
        /// 'narration' textbox key press
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNarration_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    inKeyPressCount++;
                    if (inKeyPressCount == 2)
                    {
                        inKeyPressCount = 0;
                        btnSave.Focus();
                    }
                }
                else
                {
                    inKeyPressCount = 0;
                }
            }
            catch (Exception ex)
            {
                formMDI.infoError.ErrorString = "BU47" + ex.Message;
            }
        }
        /// <summary>
        /// 'Narration' textbox keydown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNarration_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Back)
                {
                    if (txtNarration.Text == string.Empty || txtNarration.SelectionStart == 0)
                    {
                        dgvBudget.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                formMDI.infoError.ErrorString = "BU48" + ex.Message;
            }
        }
        /// <summary>
        /// 'save' button key up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Back)
                {
                    txtNarration.SelectionStart = 0;
                    txtNarration.Focus();
                }
            }
            catch (Exception ex)
            {
                formMDI.infoError.ErrorString = "BU49" + ex.Message;
            }
        }
        /// <summary>
        /// 'FromDate' textbox key leave
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtFromDate_Leave(object sender, EventArgs e)
        {
            try
            {
                DateValidation obj = new DateValidation();
                obj.DateValidationFunction(txtFromDate);
                if (txtFromDate.Text == string.Empty)
                {
                    txtFromDate.Text = PublicVariables._dtCurrentDate.ToString("dd-MMM-yyyy");
                }
                //---for change date in Date time picker----//
                string strdate = txtFromDate.Text;
                dtpFromDate.Value = Convert.ToDateTime(strdate.ToString());
                //------------------//
            }
            catch (Exception ex)
            {
                formMDI.infoError.ErrorString = "BU50" + ex.Message;
            }
        }
        /// <summary>
        /// 'Budgetname' textbox keydown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBudgetNameSearch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (cmbTypeSearch.Enabled == true)
                    {
                        cmbTypeSearch.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                formMDI.infoError.ErrorString = "BU51" + ex.Message;
            }
        }
        /// <summary>
        /// 'Type' combo in search keydown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbTypeSearch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (btnSearch.Enabled == true)
                    {
                        btnSearch.Focus();
                    }
                }
                if (e.KeyCode == Keys.Back)
                {
                    txtBudgetNameSearch.Focus();
                }
            }
            catch (Exception ex)
            {
                formMDI.infoError.ErrorString = "BU52" + ex.Message;
            }
        }
        #endregion
    }
}
