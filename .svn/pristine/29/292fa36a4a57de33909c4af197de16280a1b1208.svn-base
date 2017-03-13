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
using System.Diagnostics;
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.ExceptionsHandler;
using OpenCBS.GUI.UserControl;
using System.Windows.Forms;
using OpenCBS.Services;
using OpenCBS.CoreDomain.Messaging;
using OpenCBS.Shared;

namespace OpenCBS.GUI.Configuration
{
    public partial class MessagingForm : SweetForm
    {        
        public MessagingForm()
        {
            InitializeComponent();
        }

        private void MessagingForm_Load(object sender, System.EventArgs e)
        {            
            LoadEmailAccounts();
            LoadMessageTemplates();
            LoadMessagesStatus();
        }
       
        #region Email Accounts
        public void LoadEmailAccounts()
        {
            lvEmailAccounts.Items.Clear();
            List<EmailAccount> emailAccounts =
                ServicesProvider.GetInstance().GetEmailAccountServices().LoadAll();
            foreach (EmailAccount emailAccount in emailAccounts)
            {
                ListViewItem lvi = new ListViewItem { Tag = emailAccount };              
                lvi.UseItemStyleForSubItems = false;
                lvi.SubItems.Add(emailAccount.Email);
                lvi.SubItems.Add(emailAccount.DisplayName);
                lvi.SubItems.Add(emailAccount.Host);
                lvi.SubItems.Add(emailAccount.Port.ToString());
                //lvi.SubItems.Add(emailAccount.Username);
                //lvi.SubItems.Add(emailAccount.Password);
                //lvi.SubItems.Add(emailAccount.EnableSsl);
                //lvi.SubItems.Add(emailAccount.UseDefaultCredentials);
                //lvi.SubItems.Add(emailAccount.IsDefaultEmailAccount);
                lvEmailAccounts.Items.Add(lvi);
            }
        }

        private void btnAddEmailAccount_Click(object sender, EventArgs e)
        {
            AddEmailAccountForm frm = new AddEmailAccountForm();
            if (DialogResult.OK != frm.ShowDialog()) return;
            try
            {
                ServicesProvider.GetInstance().GetEmailAccountServices().Add(frm.EmailAccount);
            }
            catch (Exception ex)
            {
                new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
            }
            LoadEmailAccounts();
        }

        private void btnDeleteEmailAccount_Click(object sender, EventArgs e)
        {
            if (lvEmailAccounts.SelectedItems.Count == 0)
                return;
            EmailAccount emailAccount = lvEmailAccounts.SelectedItems[0].Tag as EmailAccount;
            Debug.Assert(emailAccount != null, "Email account not selected!");

            if (emailAccount.IsDefaultEmailAccount.HasValue && emailAccount.IsDefaultEmailAccount.Value)
            {
                Fail("Cannot delete default email account");
                return;
            }
            if (!Confirm("confirmDelete")) 
                return;
            ServicesProvider.GetInstance().GetEmailAccountServices().Delete(emailAccount);

            LoadEmailAccounts();
        }

        private void btnEditEmailAccount_Click(object sender, EventArgs e)
        {
            if (lvEmailAccounts.SelectedItems.Count == 0)
                return;
            EmailAccount emailAccount = (EmailAccount)lvEmailAccounts.SelectedItems[0].Tag;
            Debug.Assert(emailAccount != null, "Email account not selected!");
            AddEmailAccountForm frm = new AddEmailAccountForm(emailAccount);
            if (DialogResult.OK != frm.ShowDialog()) return;
            try
            {
                ServicesProvider.GetInstance().GetEmailAccountServices().Update(frm.EmailAccount);
            }
            catch (Exception ex)
            {
                new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
            }
            LoadEmailAccounts();
        }            

        private void tbCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        #endregion

        #region Message Templates
        private void btnAddMessageTemplate_Click(object sender, EventArgs e)
        {
            AddMessageTemplateForm frm = new AddMessageTemplateForm();
            if (DialogResult.OK != frm.ShowDialog()) return;
            try
            {
                ServicesProvider.GetInstance().GetMessageTemplateServices().Add(frm.MessageTemplate);
            }
            catch (Exception ex)
            {
                new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
            }
            LoadMessageTemplates();
        }

        private void btnDeleteMessageTemplate_Click(object sender, EventArgs e)
        {
            if (lvMessageTemplates.SelectedItems.Count == 0)
                return;
            MessageTemplate messageTemplate = lvMessageTemplates.SelectedItems[0].Tag as MessageTemplate;
            Debug.Assert(messageTemplate != null, "Message template not selected!");

            if (messageTemplate.IsDefault.HasValue && messageTemplate.IsDefault.Value)
            {
                Fail("Cannot delete default message template");
                return;
            }
            if (!Confirm("confirmDelete")) 
                return;
            
            ServicesProvider.GetInstance().GetMessageTemplateServices().Delete(messageTemplate);

            LoadMessageTemplates();
        }

        private void btnEditMessageTemplate_Click(object sender, EventArgs e)
        {
            if (lvMessageTemplates.SelectedItems.Count == 0)
                return;
            MessageTemplate messageTemplate = (MessageTemplate)lvMessageTemplates.SelectedItems[0].Tag;
            Debug.Assert(messageTemplate != null, "Message template not selected!");
            AddMessageTemplateForm frm = new AddMessageTemplateForm(messageTemplate);
            if (DialogResult.OK != frm.ShowDialog()) return;
            try
            {
                ServicesProvider.GetInstance().GetMessageTemplateServices().Update(frm.MessageTemplate);
            }
            catch (Exception ex)
            {
                new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
            }
            LoadMessageTemplates();
        }            

        public void LoadMessageTemplates()
        {
            lvMessageTemplates.Items.Clear();
            List<MessageTemplate> messageTemplates =
                ServicesProvider.GetInstance().GetMessageTemplateServices().LoadAll();
            foreach (MessageTemplate messageTemplate in messageTemplates)
            {
                ListViewItem lvi = new ListViewItem { Tag = messageTemplate };
                lvi.UseItemStyleForSubItems = false;
                lvi.SubItems.Add(messageTemplate.Name);
                lvi.SubItems.Add(messageTemplate.BccEmailAddresses);
                lvi.SubItems.Add(messageTemplate.EmailAccount == null ? "" : messageTemplate.EmailAccount.Email);                
                lvi.SubItems.Add(messageTemplate.Subject);
                lvi.SubItems.Add(messageTemplate.IsActive.ToString());
                lvi.SubItems.Add(messageTemplate.IsDefault.ToString());
                lvMessageTemplates.Items.Add(lvi);
            }
        }

        #endregion

        public void LoadMessagesStatus()
        {            
            var _queuedEmailService = ServicesProvider.GetInstance().GetQueuedEmailServices();
            var pendingEmails = _queuedEmailService.SearchEmails(null, null, null, null, true, 0, OpenCBSConstants.MessagingMaxSentTries, false, 0, 10000).Count;
            slPendingMessages.Text = pendingEmails.ToString();

            var failedEmails = _queuedEmailService.SearchEmails(null, null, null, null, true, OpenCBSConstants.MessagingMaxSentTries, OpenCBSConstants.MessagingMaxSentTries + 10, false, 0, 10000).Count;
            slFailedMessages.Text = failedEmails.ToString();
        }

        private void tsbReset_ButtonClick(object sender, EventArgs e)
        {
            var _queuedEmailService = ServicesProvider.GetInstance().GetQueuedEmailServices();
            var failedEmails = _queuedEmailService.LoadAll().FindAll(qe => qe.SentTries >= OpenCBSConstants.MessagingMaxSentTries);

            foreach (var failedEmail in failedEmails) 
            {
                failedEmail.SentTries = 0;
                _queuedEmailService.Update(failedEmail);
            }
            LoadMessagesStatus();
        }

        private void lvMessageTemplates_DoubleClick(object sender, EventArgs e)
        {
            //MessageTemplate messageTemplate = lvMessageTemplates.SelectedItems[0].Tag as MessageTemplate;
            btnEditMessageTemplate_Click(sender, e);
        }

        private void lvEmailAccounts_DoubleClick(object sender, EventArgs e)
        {
            btnEditEmailAccount_Click(sender, e);
        }
        
    }
}
