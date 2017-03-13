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
using OpenCBS.Messaging.Messages;
using LiveSwitch.TextControl;
using System.IO;

namespace OpenCBS.GUI.Configuration
{
    public partial class AddMessageTemplateForm : SweetOkCancelForm
    {
        private bool _isNew;
        private MessageTemplate _messageTemplate;
        private Branch branch;

        public MessageTemplate MessageTemplate
        {
            get
            {
                return _messageTemplate;
            }
        }

        public AddMessageTemplateForm()
        {
            InitializeComponent();
            Initialize(null);
        }

        public AddMessageTemplateForm(MessageTemplate messageTemplate)
        {
            InitializeComponent();
            Initialize(messageTemplate);
        }

        public AddMessageTemplateForm(Branch tBranch)
        {
            InitializeComponent();
            Initialize(null);
            branch = tBranch;
        }

        private void Initialize(MessageTemplate messageTemplate)
        {
            tbEmailBody.Tick += new Editor.TickDelegate(emailEditor_Tick);

            _isNew = messageTemplate == null;
            _messageTemplate = messageTemplate;

            List<EmailAccount> emailAccounts = ServicesProvider.GetInstance().GetEmailAccountServices().LoadAll();
            cmbEmailAccount.Items.Clear();
            cmbEmailAccount.ValueMember = "Id";
            cmbEmailAccount.DisplayMember = "Email";
            cmbEmailAccount.DataSource = emailAccounts;

            if (!_isNew)
            {
                tbName.Enabled = messageTemplate.IsDefault.HasValue && !messageTemplate.IsDefault.Value;
                tbName.Text = messageTemplate.Name;
                tbBccEmail.Text = messageTemplate.BccEmailAddresses;
                tbSubject.Text = messageTemplate.Subject;
                tbBody.Text = messageTemplate.Body.ToString();
                cbSendSMS.Checked = messageTemplate.SendSMS.HasValue ? messageTemplate.SendSMS.Value : false;
                cbSendEmail.Checked = messageTemplate.SendEmail.HasValue ? messageTemplate.SendEmail.Value : true;

                tbEmailBody.Html = messageTemplate.EmailBody;

                if (messageTemplate != null && messageTemplate.EmailAccount != null)
                    cmbEmailAccount.SelectedValue = messageTemplate.EmailAccount.Id;


                cbDefault.Checked = messageTemplate.IsDefault.HasValue ? messageTemplate.IsDefault.Value : false;
                cbActive.Checked = messageTemplate.IsActive;
            }
            cmbTokens.DataSource = MessageTokenProvider.GetListOfAllowedTokens();
        }

        private void AddMessageTemplateForm_Load(object sender, System.EventArgs e)
        {
            Text = _isNew ? GetString("add") : GetString("edit");
            if (!_isNew)
            {
                cmbEmailAccount.SelectedItem = _messageTemplate.EmailAccount;

                using (TextInsertForm form = new TextInsertForm(tbEmailBody.BodyHtml))
                {
                    form.ShowDialog(this);
                    if (form.Accepted)
                    {
                        tbEmailBody.BodyHtml = _messageTemplate.EmailBody;// form.HTML;
                    }
                }
            }
        }

        private void AddMessageTemplateForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult.OK != DialogResult) return;
            EmailAccount ea = cmbEmailAccount.SelectedItem as EmailAccount;

            _messageTemplate = new MessageTemplate
            {
                Id = _messageTemplate != null ? _messageTemplate.Id : 0,
                Name = tbName.Text,
                BccEmailAddresses = tbBccEmail.Text,
                Subject = tbSubject.Text,
                Body = tbBody.Text,
                EmailBody = tbEmailBody.Html,//.DocumentText,//.ToMailMessage().Body,
                EmailAccountId = ea.Id,
                EmailAccount = ea,
                SendEmail = cbSendEmail.Checked,
                SendSMS = cbSendSMS.Checked,
                IsDefault = cbDefault.Checked,
                IsActive = cbActive.Checked
            };

            if (string.IsNullOrEmpty(_messageTemplate.Name))
            {
                Fail(GetString("NameIsEmpty.Text"));
                e.Cancel = true;
                return;
            }

            if (_messageTemplate.EmailAccount == null)
            {
                Fail(GetString("EmailAccountIsEmpty.Text"));
                e.Cancel = true;
            }

            if (_messageTemplate.SendEmail.HasValue && _messageTemplate.SendEmail.Value && String.IsNullOrEmpty(_messageTemplate.EmailBody))
            {
                Fail(GetString("EmailBody.Text"));
                e.Cancel = true;
                return;
            }
            if (_messageTemplate.SendSMS.HasValue && _messageTemplate.SendSMS.Value && String.IsNullOrEmpty(_messageTemplate.Body))
            {
                Fail(GetString("SMSBody.Text"));
                e.Cancel = true;
                return;
            }

        }

        private void cmbTokens_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTokens.SelectedItem.ToString() != String.Empty && cmbTokens.SelectedItem.ToString() != MessageTokenProvider.GetListOfAllowedTokens()[0])
                tbBody.Text = tbBody.Text.Insert(tbBody.SelectionStart, cmbTokens.SelectedItem.ToString());

            cmbTokens.SelectedIndex = 0;
        }

        private void emailEditor_Tick()
        {
            undoToolStripMenuItem.Enabled = tbEmailBody.CanUndo();
            redoToolStripMenuItem.Enabled = tbEmailBody.CanRedo();
            cutToolStripMenuItem.Enabled = tbEmailBody.CanCut();
            copyToolStripMenuItem.Enabled = tbEmailBody.CanCopy();
            pasteToolStripMenuItem.Enabled = tbEmailBody.CanPaste();
            imageToolStripMenuItem.Enabled = tbEmailBody.CanInsertLink();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _filename = null;
            Text = null;
            tbEmailBody.BodyHtml = string.Empty;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_filename == null)
            {
                if (!SaveFileDialog())
                    return;
            }
            SaveFile(_filename);
        }

        private bool SaveFileDialog()
        {
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Title = "Save Email Template File";
                dlg.AddExtension = true;
                dlg.DefaultExt = "htm";
                dlg.Filter = "HTML files (*.html;*.htm)|*.html;*.htm";
                DialogResult res = dlg.ShowDialog(this);
                if (res == DialogResult.OK)
                {
                    _filename = dlg.FileName;
                    return true;
                }
                else
                    return false;
            }
        }

        private void SaveFile(string filename)
        {
            using (StreamWriter writer = File.CreateText(filename))
            {
                writer.Write(tbEmailBody.DocumentText);
                writer.Flush();
                writer.Close();
            }
        }

        private string _filename = null;

        private void LoadFile(string filename)
        {
            using (StreamReader reader = File.OpenText(filename))
            {
                tbEmailBody.Html = reader.ReadToEnd();
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "HTML files (*.html;*.htm)|*.html;*.htm";
                DialogResult res = dlg.ShowDialog(this);
                if (res == DialogResult.OK)
                {
                    _filename = dlg.FileName;
                }
                else
                    return;
            }
            LoadFile(_filename);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SearchDialog dlg = new SearchDialog(tbEmailBody))
            {
                dlg.ShowDialog(this);
            }
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tbEmailBody.SelectAll();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tbEmailBody.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tbEmailBody.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tbEmailBody.Paste();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tbEmailBody.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tbEmailBody.Redo();
        }

        private void textToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, tbEmailBody.BodyText);
        }

        private void htmlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, tbEmailBody.BodyHtml);
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tbEmailBody.Print();
        }

        private void breakToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tbEmailBody.InsertBreak();
        }

        private void textToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (TextInsertForm form = new TextInsertForm(tbEmailBody.BodyHtml))
            {
                form.ShowDialog(this);
                if (form.Accepted)
                {
                    tbEmailBody.BodyHtml = form.HTML;
                }
            }
        }

        private void paragraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tbEmailBody.InsertParagraph();
        }

        private void imageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tbEmailBody.InsertImage();
        }

        private void emailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new ConfigureSMTPForm(null, 25, SMTPAuthenticationType.UsernamePassword, null, null, true, tbEmailBody.ToMailMessage());
            form.ShowDialog();
        }

        private void tbEmailBody_Load(object sender, EventArgs e)
        {

        }
    }
}
