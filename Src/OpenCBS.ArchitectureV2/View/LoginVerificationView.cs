using OpenCBS.ArchitectureV2.Interface.Presenter;
using OpenCBS.ArchitectureV2.Interface.Service;
using OpenCBS.ArchitectureV2.Interface.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenCBS.ArchitectureV2.View
{
    public partial class LoginVerificationView :  BaseView, ILoginVerificationView
    {
        public LoginVerificationView(ITranslationService translationService) : base(translationService)
        {
            InitializeComponent();
        }

        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape) Close();
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public string Username
        {
            get { return _usernameTextBox.Text; }
            set { _usernameTextBox.Text = value; }
        }

        public string Password
        {
            get { return _passwordTextBox.Text; }
            set { _passwordTextBox.Text = value; }
        }

        public string SecretQuestionAnswer
        {
            get
            {
                return "";// throw new NotImplementedException();
            }
        }

        public void Attach(ILoginPresenterCallbacks presenterCallbacks)
        {
            _loginButton.Click += (sender, e) => presenterCallbacks.Ok();
        }

        public void Run()
        {
            Show();
            //ShowDialog();
        }

        public void Stop()
        {
            Close();
        }
    }
}
