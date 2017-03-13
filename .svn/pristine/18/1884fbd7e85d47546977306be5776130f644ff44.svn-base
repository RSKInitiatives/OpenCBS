using System.Reflection;
using System.Windows.Forms;
using OpenCBS.ArchitectureV2.CommandData;
using OpenCBS.ArchitectureV2.Interface;

namespace OpenCBS.ArchitectureV2.Command
{
    public class SearchLoanCommand : ICommand<SearchLoanCommandData>
    {
        public void Execute(SearchLoanCommandData commandData)
        {
            var assembly = Assembly.Load("OpenCBS.GUI");
            var viewType = assembly.GetType("OpenCBS.GUI.SearchCreditContractForm", true);
            var form = (Form) viewType.GetMethod("GetInstance", new[] { typeof(Control) }).Invoke(null, new object[] { null });
            form.BringToFront();
            form.WindowState = FormWindowState.Normal;
            form.Show();
        }
    }
}
