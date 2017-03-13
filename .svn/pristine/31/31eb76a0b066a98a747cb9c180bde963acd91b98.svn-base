using System.Collections.Generic;
using OpenCBS.ArchitectureV2.Interface.Presenter;

namespace OpenCBS.ArchitectureV2.Interface.View
{
    public interface ILoginVerificationView : IView<ILoginPresenterCallbacks>
    {
        void Run();
        void Stop();
        string Username { get; }
        string Password { get; }
        string SecretQuestionAnswer { get; }        
    }
}
