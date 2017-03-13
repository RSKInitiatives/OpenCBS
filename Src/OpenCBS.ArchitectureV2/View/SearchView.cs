using System.Collections.Generic;
using System.Windows.Forms;
using OpenCBS.ArchitectureV2.Interface.Presenter;
using OpenCBS.ArchitectureV2.Interface.View;
using OpenCBS.ArchitectureV2.Model;

namespace OpenCBS.ArchitectureV2.View
{
    public partial class SearchView : Form, ISearchView
    {
        public SearchView()
        {
            InitializeComponent();
            _activeColumn.AspectToStringConverter = v => ((bool) v) ? "Yes" : "No";
        }

        public void Attach(ISearchPresenterCallbacks presenterCallbacks)
        {
            FormClosing += (sender, e) => presenterCallbacks.DetachView();
            _searchTextBox.KeyDown += (sender, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    presenterCallbacks.Search();
                }
            };
            _searchResultsListView.DoubleClick += (sender, e) => presenterCallbacks.OpenSearchResult();
        }

        public string Keywords
        {
            get { return _searchTextBox.Text; }
        }

        public void SetSearchResults(List<SearchResult> searchResults)
        {
            _searchResultsListView.SetObjects(searchResults);
            Text = string.Format("Search - {0}", searchResults.Count);
        }

        public void StartProgress()
        {
            Cursor = Cursors.WaitCursor;
        }

        public void StopProgress()
        {
            Cursor = Cursors.Default;
        }

        public SearchResult SelectedItem
        {
            get { return (SearchResult) _searchResultsListView.SelectedObject; }
        }
    }
}
