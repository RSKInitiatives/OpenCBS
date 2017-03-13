namespace OpenCBS.ArchitectureV2.Interface.Presenter
{
    public interface IStartPagePresenterCallbacks
    {
        void AddPerson();
        void AddGroup();
        void AddVillageBank();
        void AddCompany();
        void SearchClient();
        void SearchLoan();
        void ChangeLanguage(string language);
        void OpenUrl(string url);
        void OpenEmail(string to, string subject);
        void DetachView();
    }
}
