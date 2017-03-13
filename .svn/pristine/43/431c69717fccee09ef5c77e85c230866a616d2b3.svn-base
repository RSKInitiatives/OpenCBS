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

using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using OpenCBS.Services;
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.GUI.UserControl;
using OpenCBS.CoreDomain.Messaging;
using System;

namespace OpenCBS.GUI.Configuration
{
    public partial class AddEmailAccountForm : SweetOkCancelForm
    {
        private bool _isNew;
        private EmailAccount _emailAccount;
        private Branch branch;

        public EmailAccount EmailAccount
        {
            get
            {
                return _emailAccount;
            }
        }

        public AddEmailAccountForm()
        {
            InitializeComponent();
            Initialize(null);
        }

        public AddEmailAccountForm(EmailAccount emailAccount)
        {
            InitializeComponent();
            Initialize(emailAccount);
        }

        public AddEmailAccountForm(Branch tBranch)
        {
            InitializeComponent();
            Initialize(null);
            branch = tBranch;
        }

        private void Initialize(EmailAccount emailAccount)
        {
            _isNew = emailAccount == null;
            _emailAccount = emailAccount;

            if (!_isNew)
            {
                tbEmail.Text = emailAccount.Email;
                tbDisplayName.Text = emailAccount.DisplayName;
                tbHost.Text = emailAccount.Host;
                tbPort.Text = emailAccount.Port.ToString();
                tbUserName.Text = emailAccount.Username;
                tbPassword.Text = emailAccount.Password;
                cbSSL.Checked = emailAccount.EnableSsl;
                cbDefault.Checked = emailAccount.IsDefaultEmailAccount.HasValue ? emailAccount.IsDefaultEmailAccount.Value : false;
                cbDefaultCred.Checked = emailAccount.UseDefaultCredentials;
            }
        }

        private void AddEmailAccountForm_Load(object sender, System.EventArgs e)
        {
            Text = _isNew ? GetString("add") : GetString("edit");
            if (!_isNew)
            {
                //cmbPaymentMethod.SelectedItem = _emailAccount.Id;
                //cmbAccount.SelectedItem = _emailAccount.Account;
            }
        }

        private void AddEmailAccountForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult.OK != DialogResult) return;
            //EmailAccount ea = cmbPaymentMethod.SelectedItem as EmailAccount;
            _emailAccount = new EmailAccount
                                                   {
                                                       Id =_emailAccount != null ? _emailAccount.Id : 0,
                                                       Email = tbEmail.Text,
                                                       DisplayName = tbDisplayName.Text,
                                                       Host = tbHost.Text,
                                                       Port = Convert.ToInt32(tbPort.Text),
                                                       Username = tbUserName.Text,
                                                       Password = tbPassword.Text,
                                                       EnableSsl = cbSSL.Checked,
                                                       IsDefaultEmailAccount = cbDefault.Checked,
                                                       UseDefaultCredentials = cbDefaultCred.Checked
                                                   };
            if (string.IsNullOrEmpty(_emailAccount.Email))
            {
                Fail(GetString("NameIsEmpty.Text"));
                e.Cancel = true;
                return;
            }

            if (string.IsNullOrEmpty(_emailAccount.Email))
            {
                Fail(GetString("EmailIsEmpty.Text"));
                e.Cancel = true;
            }
        }

        private void tbPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
