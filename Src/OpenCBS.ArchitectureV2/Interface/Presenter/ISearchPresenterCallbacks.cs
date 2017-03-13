namespace OpenCBS.ArchitectureV2.Interface.Presenter
{
    public interface ISearchPresenterCallbacks
    {
        void Search();
        void OpenSearchResult();
        void DetachView();
    }
}
