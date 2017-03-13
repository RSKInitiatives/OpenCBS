using OpenCBS.ArchitectureV2.CommandData;
using OpenCBS.ArchitectureV2.Interface;
using OpenCBS.ArchitectureV2.Interface.Presenter;
using OpenCBS.ArchitectureV2.Interface.View;
using OpenCBS.ArchitectureV2.Message;

namespace OpenCBS.ArchitectureV2.Presenter
{
    public class StartPagePresenter : IStartPagePresenter, IStartPagePresenterCallbacks
    {
        private readonly IStartPageView _view;
        private readonly IApplicationController _applicationController;

        public StartPagePresenter(IStartPageView view, IApplicationController applicationController)
        {
            _view = view;
            _applicationController = applicationController;
        }

        public void Run()
        {
            _view.Attach(this);
            _applicationController.Publish(new ShowViewMessage(this, _view));
            _applicationController.Publish(new StartPageShownMessage(this));
        }

        public object View
        {
            get { return _view; }
        }

        public void AddPerson()
        {
            _applicationController.Execute(new AddPersonCommandData());
        }

        public void AddGroup()
        {
            _applicationController.Execute(new AddGroupCommandData());
        }

        public void AddVillageBank()
        {
            _applicationController.Execute(new AddVillageBankCommandData());
        }

        public void AddCompany()
        {
            _applicationController.Execute(new AddCompanyCommandData());
        }

        public void SearchClient()
        {
            _applicationController.Execute(new SearchClientCommandData());
        }

        public void SearchLoan()
        {
            _applicationController.Execute(new SearchLoanCommandData());
        }

        public void ChangeLanguage(string language)
        {
            _applicationController.Publish(new RestartApplicationMessage(this, language));
        }

        public void OpenUrl(string url)
        {
            _applicationController.Execute(new OpenUrlCommandData { Url = url });
        }

        public void OpenEmail(string to, string subject)
        {
            _applicationController.Execute(new OpenEmailCommandData { To = to, Subject = subject });
        }

        public void DetachView()
        {
            _applicationController.Publish(new StartPageHiddenMessage(this));
        }
    }
}
