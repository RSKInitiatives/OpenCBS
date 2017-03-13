using System.Collections.Generic;
using OpenCBS.ArchitectureV2.Interface;
using OpenCBS.ArchitectureV2.Interface.Presenter;
using OpenCBS.ArchitectureV2.Interface.Service;
using OpenCBS.ArchitectureV2.Interface.View;
using OpenCBS.CoreDomain;
using OpenCBS.Services;

namespace OpenCBS.ArchitectureV2.Presenter
{

    public class LoginVerificationPresenter : ILoginVerificationPresenter, ILoginPresenterCallbacks
    {
        private readonly ILoginVerificationView _view;
        private readonly IErrorView _errorView;
        private readonly IDatabaseService _databaseService;
        private readonly IAuthService _authService;
        private readonly ISettingsService _settingsService;
        private readonly IBackgroundTaskFactory _backgroundTaskFactory;

        public LoginVerificationPresenter(
            ILoginVerificationView view,
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
            var user = _authService.Verify(_view.Username, _view.Password/*, _view.SecretQuestionAnswer*/);
            if (user != null && user.UserName != User.CurrentUser.UserName && 
                (
                (user.IsExpired.HasValue && !user.IsExpired.Value)
                ||
                (!user.IsExpired.HasValue)
                ))
            {
                User.CurrentUser.TimedOut = false;
                User.CurrentUser.IsExpired = false;
                User.CurrentUser.LoginAttempt = 0;
                ServicesProvider.GetInstance().GetUserServices().UpdateUserAccess(User.CurrentUser);
                _view.Stop();
            }
            else if (user != null && user.UserName != User.CurrentUser.UserName && 
                (
                    (user.IsExpired.HasValue && user.IsExpired.Value)
                    ||
                    (user.TimedOut.HasValue && user.TimedOut.Value)
                )
                )
            {
                _errorView.Run("Verification login details expired or timed out");
            }
            else if (user != null && user.UserName == User.CurrentUser.UserName)
            {
                _errorView.Run("Different login details required for verification");
            }
            else
            {
                _errorView.Run("Invalid verification username or password.");
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
