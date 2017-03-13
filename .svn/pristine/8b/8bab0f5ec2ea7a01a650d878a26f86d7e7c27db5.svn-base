using System.Reflection;
using System.Windows.Forms;
using OpenCBS.ArchitectureV2.CommandData;
using OpenCBS.ArchitectureV2.Interface;

namespace OpenCBS.ArchitectureV2.Command
{
    public class SearchClientCommand : ICommand<SearchClientCommandData>
    {
        public void Execute(SearchClientCommandData commandData)
        {
            var assembly = Assembly.Load("OpenCBS.GUI");
            var viewType = assembly.GetType("OpenCBS.GUI.SearchClientForm", true);
            var form = (Form) viewType.GetMethod("GetInstance", new[] { typeof(Control) }).Invoke(null, new object[] { null });
            form.BringToFront();
            form.WindowState = FormWindowState.Normal;
            form.Show();
        }
    }
}
