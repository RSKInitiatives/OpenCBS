// Copyright © 2013 Open Octopus Ltd.
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
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BrightIdeasSoftware;
using OpenCBS.CoreDomain.Contracts.Loans;
using OpenCBS.CoreDomain.Contracts.Loans.Installments;
using OpenCBS.Shared;

namespace OpenCBS.Controls
{
    public partial class ScheduleControl : UserControl
    {
        private string _amountFormatString;

        public ScheduleControl()
        {
            InitializeComponent();
            Setup();
            scheduleObjectListView.RowFormatter = FormatRow;
        }

        public void SetScheduleFor(Loan loan)
        {
            _amountFormatString = loan.UseCents ? "N2" : "N0";
            scheduleObjectListView.SetObjects(loan.InstallmentList);
        }

        private void Setup()
        {
            dateColumn.AspectToStringConverter =
            paymentDateColumn.AspectToStringConverter = value =>
            {
                var date = (DateTime?)value;
                return date.HasValue ? date.Value.ToShortDateString() : string.Empty;
            };
            principalColumn.AspectToStringConverter =
            interestColumn.AspectToStringConverter =
            extraColumn.AspectToStringConverter =
            paidPrincipalColumn.AspectToStringConverter =
            paidInterestColumn.AspectToStringConverter =
            paidExtraColumn.AspectToStringConverter =
            olbColumn.AspectToStringConverter =
            totalColumn.AspectToStringConverter = value =>
            {
                var amount = (OCurrency)value;
                return amount.Value.ToString(_amountFormatString);
            };
            _scheduleContextMenuStrip.Click += (sender, e) => _CopyData();
            extraColumn.IsVisible = false;
            paidExtraColumn.IsVisible = false;
            scheduleObjectListView.RebuildColumns();
        }

        private static void FormatRow(OLVListItem item)
        {
            var installment = (Installment) item.RowObject;
            if (installment == null) return;
            if (installment.IsPending) item.BackColor = Color.Orange;
            if (installment.IsRepaid) item.BackColor = Color.FromArgb(61, 153, 57);
            if (installment.IsPending || installment.IsRepaid) item.ForeColor = Color.White;
            if (installment.LateDays > 0 && !(installment.IsPending || installment.IsRepaid))
                item.ForeColor = Color.Red;
        }

        public void ShowExtraColumn()
        {
            extraColumn.IsVisible = true;
            paidExtraColumn.IsVisible = true;
            scheduleObjectListView.RebuildColumns();
        }

        private void _CopyData()
        {
            var buffer = new StringBuilder();
            for (var i = 0; i < scheduleObjectListView.Columns.Count; i++)
            {
                buffer.Append(scheduleObjectListView.Columns[i].Text);
                buffer.Append("\t");
            }
            buffer.Append("\n");

            for (int i = 0; i < scheduleObjectListView.Items.Count; i++)
            {
                for (int j = 0; j < scheduleObjectListView.Items[i].SubItems.Count; j++)
                {
                    buffer.Append(scheduleObjectListView.Items[i].SubItems[j].Text);
                    buffer.Append("\t");
                }
                buffer.Append("\n");
            }

            Clipboard.SetText(buffer.ToString());
            _scheduleContextMenuStrip.Visible = false;
        }
    }
}
