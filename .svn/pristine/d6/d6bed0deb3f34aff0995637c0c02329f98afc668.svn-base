using System.Collections.Generic;
using OpenCBS.ArchitectureV2.Interface;
using OpenCBS.ArchitectureV2.Interface.Presenter;
using OpenCBS.ArchitectureV2.Interface.Service;
using OpenCBS.ArchitectureV2.Interface.View;
using OpenCBS.CoreDomain;
using OpenCBS.Services;
using System.Windows.Forms;

namespace OpenCBS.ArchitectureV2.Presenter
{

    public class ChangePasswordPresenter : IChangePasswordPresenter, ILoginPresenterCallbacks
    {
        private readonly IPasswordView _view;
        private readonly IErrorView _errorView;
        private readonly IDatabaseService _databaseService;
        private readonly IAuthService _authService;
        private readonly ISettingsService _settingsService;
        private readonly IBackgroundTaskFactory _backgroundTaskFactory;

        public ChangePasswordPresenter(
            IPasswordView view,
            IErrorView errorView,
            IDatabaseService databaseService,
            IAuthService authService,
            ISettingsService settingsService,
            IBackgroundTaskFactory backgroundTaskFactory)
        {
            _view = view;
            _errorView = errorView;
            _databaseService = databaseService;
            _authService = authService;
            _settingsService = settingsService;
            _backgroundTaskFactory = backgroundTaskFactory;
        }

        public void Ok()
        {
            if (!string.Equals(_view.Password, _view.ConfirmPassword))
            {
                MessageBox.Show("Passwords do not match", "Change Password Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var userService = ServicesProvider.GetInstance().GetUserServices();
            User user = userService.FindByUserName(_view.Username);
            if (user != null && user.UserName == User.CurrentUser.UserName && 
                (user.IsReset.HasValue && user.IsReset.Value))
            {
                User.CurrentUser.Secret.Question = _view.SecretQuestion;
                User.CurrentUser.Secret.Answer = _view.SecretQuestionAnswer;
                User.CurrentUser.Password = _view.Password;
                var userErrors = userService.SaveUser(User.CurrentUser, true);
                if (userErrors.FindError)
                {
                    _errorView.Run(userErrors.ResultMessage);
                }
                else
                {
                    User.CurrentUser.TimedOut = false;
                    User.CurrentUser.IsExpired = false;
                    User.CurrentUser.LoginAttempt = 0;
                    User.CurrentUser.IsReset = false;
                    userService.UpdateUserAccess(User.CurrentUser);
                    _view.Stop();
                }                
            }
            else if (user != null && user.UserName != User.CurrentUser.UserName)
            {
                _errorView.Run("Login credential change request denied");
            }
            else
            {
                _errorView.Run(@"An unknown error has occurred. 
                \n\n Your account may not have been reset properly.");
            }
        }

        public void Run()
        {
            _view.Attach(this);
            _view.Run();
        }

        public object View
        {
            get { return _view; }
        }
    }
}
