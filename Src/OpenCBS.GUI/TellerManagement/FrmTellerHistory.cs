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
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using OpenCBS.CoreDomain;
using OpenCBS.GUI.UserControl;
using OpenCBS.Services;
using OpenCBS.Shared;
using OpenCBS.CoreDomain.Events.Saving;
using OpenCBS.Shared.Settings;
using System.Diagnostics;
using OpenCBS.Services.Events;
using OpenCBS.Reports;
using OpenCBS.Reports.Forms;

namespace OpenCBS.GUI.TellerManagement
{
    public partial class FrmTellerHistory : SweetForm
    {
        public FrmTellerHistory()
        {
            InitializeComponent();
            InitDates();
            InitUsers();
            InitBranches();
            InitTellers();
        }

        private void OnLoad(object sender, EventArgs e)
        {
            if (User.CurrentUser.HasAdminRole)
            {
                cbUser.Enabled = cbBranch.Enabled = cbTeller.Enabled = true;
            }
            else
            {
                cbUser.Enabled = cbBranch.Enabled = cbTeller.Enabled = false;
                if (Teller.CurrentTeller != null)
                {
                    cbUser.SelectedItem = User.CurrentUser;
                    cbBranch.SelectedItem = Teller.CurrentTeller.Branch;
                    cbTeller.SelectedItem = Teller.CurrentTeller;
                }
            }
        }

        private void InitUsers()
        {
            cbUser.Items.Clear();

            List<User> users = ServicesProvider.GetInstance().GetUserServices().FindAll(false);

            cbUser.Items.Add(GetString("allUsers"));

            foreach (User user in users)
            {
                cbUser.Items.Add(user);
            }
            cbUser.SelectedIndex = 0;
        }

        private void InitBranches()
        {
            cbBranch.Items.Clear();
            cbBranch.Items.Add(GetString("allBranches"));
            List<Branch> branches = ServicesProvider.GetInstance().GetBranchService().FindAllNonDeleted();
            foreach (Branch branch in branches)
            {
                cbBranch.Items.Add(branch);
            }
            cbBranch.SelectedIndex = 0;
        }

        private void InitTellers()
        {
            cbTeller.Items.Clear();
            cbTeller.Items.Add(GetString("allTellers"));
            List<Teller> tellers = ServicesProvider.GetInstance().GetTellerServices().FindAllNonDeleted();
            foreach (Teller teller in tellers)
            {
                cbTeller.Items.Add(teller);
            }
            cbTeller.SelectedIndex = 0;
        }

        private void InitDates()
        {
            dtTo.Value = TimeProvider.Today;
            dtFrom.Value = TimeProvider.Today;// dtTo.Value.AddMonths(-1);

            dtTo.Format = DateTimePickerFormat.Custom;
            dtTo.CustomFormat = ApplicationSettings.GetInstance("").SHORT_DATE_FORMAT;
            dtFrom.Format = DateTimePickerFormat.Custom;
            dtFrom.CustomFormat = ApplicationSettings.GetInstance("").SHORT_DATE_FORMAT;
        }

        private void OnPrintClick(object sender, EventArgs e)
        {
            User u = cbUser.SelectedItem as User;
            Branch b = cbBranch.SelectedItem as Branch;
            Teller t = cbTeller.SelectedItem as Teller;
            TellerSavingEventsFilter filter;
            filter.From = dtFrom.Value;
            filter.To = dtTo.Value;
            filter.UserId = null == u ? 0 : u.Id;
            filter.BranchId = null == b ? 0 : b.Id;
            filter.TellerId = null == t ? 0 : t.Id.Value;
            filter.IncludeDeleted = chkIncludeDeleted.Checked;
            string userName = null == u ? GetString("allUsers") : u.Name;
            string tellerName = null == t ? GetString("allTellers") : t.Name;
            string branchName = null == b ? GetString("allBranches") : b.Name;
            bwReport.RunWorkerAsync(new object[] { filter, userName, tellerName, branchName });
        }

        private void OnRefreshClick(object sender, EventArgs e)
        {
            //btnRefresh.StartProgress();

            User u = cbUser.SelectedItem as User;
            Branch b = cbBranch.SelectedItem as Branch;
            Teller t = cbTeller.SelectedItem as Teller;
            TellerSavingEventsFilter filter;
            filter.From = dtFrom.Value;
            filter.To = dtTo.Value;
            filter.UserId = null == u ? 0 : u.Id;
            filter.BranchId = null == b ? 0 : b.Id;
            filter.TellerId = null == t ? 0 : t.Id.Value;
            filter.IncludeDeleted = chkIncludeDeleted.Checked;

            bwRefresh.RunWorkerAsync(filter);
        }

        private void OnPrintDoWork(object sender, DoWorkEventArgs e)
        {
            ReportService rs = ReportService.GetInstance();
            Debug.Assert(rs != null, "Report service not found");
            Report r = rs.GetReportByName("Teller_Events.zip");
            Debug.Assert(r != null, "Report not found");
            Debug.Assert(r.IsLoaded, "Report not loaded");

            object[] arr = (object[])e.Argument;
            TellerSavingEventsFilter filter = (TellerSavingEventsFilter)arr[0];
            string userName = arr[1].ToString();
            string tellerName = arr[2].ToString();
            string branchName = arr[3].ToString();
            r.RemoveParams();
            r.AddParam("from", filter.From);
            r.AddParam("to", filter.To);
            r.AddParam("user", filter.UserId);
            r.AddParam("user_name", userName);
            r.AddParam("teller", filter.TellerId);
            r.AddParam("teller_name", tellerName);
            r.AddParam("branch", filter.BranchId);
            r.AddParam("branch_name", branchName);
            r.AddParam("include_deleted", filter.IncludeDeleted);
            rs.LoadReport(r);
            e.Result = r;
        }

        private void OnPrintCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                Report r = e.Result as Report;
                Debug.Assert(r != null, "Report not loaded");
                ReportViewerForm frm = new ReportViewerForm(r);
                frm.Show();

            }
            catch (Exception)
            {
                MessageBox.Show("Error generating teller report");
            }
            //btnPrint.StopProgress();
        }

        private void OnRefreshDoWork(object sender, DoWorkEventArgs e)
        {
            Debug.Assert(e.Argument != null, "Argument is null");
            TellerSavingEventsFilter filter = (TellerSavingEventsFilter)e.Argument;
            EventProcessorServices s = ServicesProvider.GetInstance().GetEventProcessorServices();
            List<TellerSavingEvent> events = s.SelectTellerSavingEvents(filter);
            UpdateEvents(events);
        }

        private void UpdateTitle()
        {
            int count = lvTellerEvent.Items.Count - 1;
            Text = string.Format(GetString("title"), count);
        }

        private delegate void UpdateListViewDelegate(List<TellerSavingEvent> list);

        ListViewItem totalItem = new ListViewItem("Total: ");
        private void UpdateEvents(IEnumerable<TellerSavingEvent> events)
        {
            if (InvokeRequired)
            {
                Invoke(new UpdateListViewDelegate(UpdateEvents), new object[] { events });
                return;
            }

            totalItem = new ListViewItem("Total: ");
            lvTellerEvent.Items.Clear();

            OCurrency totalBalance = 0;
            OCurrency totalFeeBalance = 0;

            foreach (TellerSavingEvent ev in events)
            {
                ListViewItem item = new ListViewItem(ev.Date.ToString("dd/MM/yyyy HH:mm:ss"));
                item.SubItems.Add(ev.Fee.GetFormatedValue(true));
                string amt = ev.Amount.GetFormatedValue(true);
                item.SubItems.Add(amt);
                item.SubItems.Add(ev.Code);
                item.SubItems.Add(ev.Account);
                item.SubItems.Add(ev.ReferenceNumber);
                item.SubItems.Add(ev.Description);
                item.SubItems.Add(ev.CancelDate.HasValue ? ev.CancelDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : string.Empty);

                if (ev.IsPending)
                {
                    item.BackColor = Color.Orange;
                    item.ForeColor = Color.White;
                }

                if (ev.Deleted)
                {
                    item.BackColor = Color.FromArgb(188, 209, 199);
                    item.ForeColor = Color.White;
                }

                totalBalance += ev.GetAmountForBalance();                
                totalFeeBalance += ev.Fee;
                item.Tag = ev;
                lvTellerEvent.Items.Add(item);
            }

            totalItem.SubItems.Add(totalFeeBalance.GetFormatedValue(true));
            totalItem.Font = new Font(totalItem.Font, FontStyle.Bold);
            //totalItem.SubItems.Add("");
            //totalItem.SubItems.Add("");
            totalItem.SubItems.Add(totalBalance.GetFormatedValue(true));
            lvTellerEvent.Items.Add(totalItem);

            UpdateTitle();
        }

    }
}
