using System;
using OpenCBS.ArchitectureV2.Interface.Service;
using OpenCBS.CoreDomain;
using OpenCBS.Services;

namespace OpenCBS.ArchitectureV2.Service
{
    public class AuthService : IAuthService
    {
        public User Verify(string username, string password)
        {
            User user = ServicesProvider.GetInstance().GetUserServices().Find(username, password);
            if (user == null)
                return null;

            return user;
        }

        public User Find(string username)
        {
            User user = ServicesProvider.GetInstance().GetUserServices().Find(username);
            if (user == null)
                return null;

            return user;
        }

        public User Login(string username, string password)
        {
            User user = ServicesProvider.GetInstance().GetUserServices().Find(username, password);
            if (user == null)
                return null;

            return (User.CurrentUser = user);
        }

        public User Login(string username, string password, string sqAnswer)
        {
            User user = ServicesProvider.GetInstance().GetUserServices().Find(username, password, sqAnswer);
            if (user == null)
                return null;
            return (User.CurrentUser = user);
        }

        public bool Logout()
        {
            User.CurrentUser = null;
            return true;
        }

        public bool LoggedIn
        {
            get { return User.CurrentUser != null && User.CurrentUser.Id > 0 && !LoginExpired; }
        }

        public bool AnswerCorrect
        {
            get { return User.CurrentUser != null && User.CurrentUser.Id > 0 && User.CurrentUser.Secret.Valid; }
        }

        public bool AllowAccess
        {
            get
            {
                return LoggedIn && !LoginReset && !RequiresVerification;
            }
        }

        public bool RequiresVerification
        {
            get { return User.CurrentUser != null && User.CurrentUser.Id > 0 
                    && 
                    (
                        LoginTimedOut
                        ||
                        LoginExpired
                        ||
                        LoginAttemptExceeded 
                    );
            }
        }

        public bool LoginTimedOut
        {
            get
            {
                return User.CurrentUser != null && User.CurrentUser.Id > 0
                  && User.CurrentUser.TimedOut.HasValue && User.CurrentUser.TimedOut.Value;
            }
        }

        public bool LoginAttemptExceeded
        {
            get
            {
                return User.CurrentUser != null && User.CurrentUser.Id > 0
                  && User.CurrentUser.LoginAttempt.HasValue && User.CurrentUser.LoginAttempt >= 3;
            }
        }

        public bool LoginExpired
        {
            get
            {
                return User.CurrentUser != null && User.CurrentUser.Id > 0
                  && User.CurrentUser.IsExpired.HasValue && User.CurrentUser.IsExpired.Value;
            }
        }

        public bool LoginReset
        {
            get
            {
                return User.CurrentUser != null && User.CurrentUser.Id > 0
                  && User.CurrentUser.IsReset.HasValue && User.CurrentUser.IsReset.Value;
            }
        }
    }
}
