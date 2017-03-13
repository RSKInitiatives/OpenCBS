using System.Collections.Generic;
using System.ComponentModel;
using OpenCBS.ArchitectureV2.CommandData;
using OpenCBS.ArchitectureV2.Interface;
using OpenCBS.ArchitectureV2.Interface.Presenter;
using OpenCBS.ArchitectureV2.Interface.Repository;
using OpenCBS.ArchitectureV2.Interface.View;
using OpenCBS.ArchitectureV2.Message;
using OpenCBS.ArchitectureV2.Model;

namespace OpenCBS.ArchitectureV2.Presenter
{
    public class SearchPresenter : ISearchPresenter, ISearchPresenterCallbacks
    {
        private readonly ISearchView _view;
        private readonly IApplicationController _applicationController;
        private readonly ISearchRepository _searchRepository;

        public SearchPresenter(
            ISearchView view,
            IApplicationController applicationController,
            ISearchRepository searchRepository)
        {
            _view = view;
            _applicationController = applicationController;
            _searchRepository = searchRepository;
        }

        public void Run()
        {
            _view.Attach(this);
            _applicationController.Publish(new ShowViewMessage(this, _view));
            _applicationController.Publish(new SearchShownMessage(this));
        }

        public object View
        {
            get { return _view; }
        }

        public void Search()
        {
            _view.StartProgress();
            var bw = new BackgroundWorker();
            bw.DoWork += (sender, e) =>
            {
                e.Result = _searchRepository.Search(_view.Keywords);
            };
            bw.RunWorkerCompleted += (sender, e) =>
            {
               _view.StopProgress();
                if (e.Error != null)
                {
                    return;
                }
                _view.SetSearchResults((List<SearchResult>) e.Result);
            };
            bw.RunWorkerAsync();
        }

        public void DetachView()
        {
            _applicationController.Publish(new SearchHiddenMessage(this));
        }

        public void OpenSearchResult()
        {
            var selectedItem = _view.SelectedItem;
            if (selectedItem == null)
            {
                return;
            }

            _applicationController.Execute(new EditVillageBankCommandData { VillageBankId = selectedItem.Id });
        }
    }
}
