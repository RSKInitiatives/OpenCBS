using System.Collections.Generic;
using OpenCBS.ArchitectureV2.Interface.Presenter;

namespace OpenCBS.ArchitectureV2.Interface.View
{
    public interface IPasswordView : IView<ILoginPresenterCallbacks>
    {
        void Run();
        void Stop();
        string Username { get; }
        string Password { get; }
        string ConfirmPassword { get; }
        string SecretQuestion { get; }
        string SecretQuestionAnswer { get; }        
    }
}
