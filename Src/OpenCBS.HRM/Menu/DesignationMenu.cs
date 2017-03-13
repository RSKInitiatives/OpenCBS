using Microsoft.Practices.ServiceLocation;
using OpenCBS.ArchitectureV2.Interface;
using OpenCBS.Extensions;
using OpenCBS.HRM.CommandData;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenCBS.HRM
{
    [PartCreationPolicy(CreationPolicy.Shared)]
    [Export(typeof(IMenu))]
    public class DesignationMenu : IMenu
    {        
        public DesignationMenu()
        {
            MefContainer.Current.Bind(this);
        }

        public string InsertAfter
        {
            get { return "payrollToolStripSeparator"; }
        }

        public string Parent
        {
            get { return "mnuPayroll"; }
        }
        
        public int Index
        {
            get
            {
                return 0;
            }
        }

        public ToolStripMenuItem GetItem()
        {
            var result = new ToolStripMenuItem { Name = "mnuDesignation", Text = "Designation" };
            result.Click += (sender, args) =>
            {
                var appController = ServiceLocator.Current.GetInstance<IApplicationController>();
                if (appController != null)
                {
                    //appController.Subscribe<AlertsHiddenMessage>(this, OnAlertsHidden);
                    appController.Execute(new ShowDesignationCommandData());
                }
            };
            return result;
        }        
    }
}
