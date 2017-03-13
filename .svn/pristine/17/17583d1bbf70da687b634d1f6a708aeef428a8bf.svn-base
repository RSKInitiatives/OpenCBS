using System.Collections.Generic;
using OpenCBS.ArchitectureV2.Interface.Presenter;
using OpenCBS.ArchitectureV2.Model;

namespace OpenCBS.ArchitectureV2.Interface.View
{
    public interface ISearchView : IView<ISearchPresenterCallbacks>
    {
        string Keywords { get; }
        SearchResult SelectedItem { get; }
        void SetSearchResults(List<SearchResult> searchResults);
        void StartProgress();
        void StopProgress();
    }
}
