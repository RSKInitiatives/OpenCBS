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
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenCBS.CoreDomain;
using OpenCBS.Services;
using OpenCBS.ArchitectureV2.View;
using OpenCBS.ArchitectureV2.Interface.View;
using OpenCBS.ArchitectureV2.Interface.Presenter;

namespace OpenCBS.ArchitectureV2.View
{
    public partial class PasswordView : BaseView, IPasswordView
    {        
        public string Username
        {
            get { return textBoxUserName.Text; }
            set { textBoxUserName.Text = value; }
        }

        public string Password
        {
            get { return textBoxNewPswd.Text; }
            set { textBoxNewPswd.Text = value; }
        }

        public string ConfirmPassword
        {
            get { return textBoxConfirmNewPswd.Text; }
            set { textBoxConfirmNewPswd.Text = value; }
        }

        public string SecretQuestion
        {
            get { return textBoxSecretQuestion.Text; }
            set { textBoxSecretQuestion.Text = value; }
        }

        public string SecretQuestionAnswer
        {
            get { return textBoxSecretQuestionAnswer.Text; }
            set { textBoxSecretQuestionAnswer.Text = value; }
        }
        
        public PasswordView()
        {
            InitializeComponent();
            Username = User.CurrentUser.UserName;
        }
        
        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape) Close();
            return base.ProcessCmdKey(ref msg, keyData);
        }        

        public void Run()
        {
            Show();
        }

        public void Stop()
        {
            Close();
        }

        public void Attach(ILoginPresenterCallbacks presenterCallbacks)
        {
            btnOk.Click += (sender, e) => presenterCallbacks.Ok();
        }        
    }
}
