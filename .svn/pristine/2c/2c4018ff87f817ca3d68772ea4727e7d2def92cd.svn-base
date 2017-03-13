using System.Windows.Forms;
using OpenCBS.ArchitectureV2.Interface.Presenter;
using OpenCBS.ArchitectureV2.Interface.View;
using OpenCBS.ArchitectureV2.Model;

namespace OpenCBS.ArchitectureV2.View
{
    public partial class VillageBankView : Form, IVillageBankView
    {
        public VillageBankView()
        {
            InitializeComponent();
            _memberActiveColumn.AspectToStringConverter = v => ((bool) v) ? "Yes" : "No";
            _loanAmountColumn.AspectToStringConverter = v => ((decimal) v).ToString("N0");
            _loanOlbColumn.AspectToStringConverter = v => ((decimal) v).ToString("N0");
        }

        public void Attach(IVillageBankPresenterCallbacks presenterCallbacks)
        {
            _repayButton.Click += (sender, e) => presenterCallbacks.Repay();
        }

        public void SetVillageBank(VillageBank villageBank)
        {
            Name = "VillageBankView" + villageBank.Id;
            Text = "Village Bank - " + villageBank.Name;
            _membersListView.SetObjects(villageBank.Members);
            _loansListView.SetObjects(villageBank.Loans);
            _nameTextBox.Text = villageBank.Name;
            _repayButton.Enabled = villageBank.Loans.Count > 0;
        }
    }
}
