using OpenCBS.CoreDomain;

namespace OpenCBS.ArchitectureV2.Interface.Service
{
    public interface IAuthService
    {
        User Verify(string username, string password);
        User Login(string username, string password);
        User Login(string username, string password, string sqAnswer);
        bool LoggedIn { get; }
        bool AnswerCorrect { get; }
        bool RequiresVerification { get; }
        bool LoginExpired { get; }
        bool LoginAttemptExceeded { get; }
        bool LoginTimedOut { get; }
        bool LoginReset { get; }
        bool AllowAccess { get; }
    }
}
