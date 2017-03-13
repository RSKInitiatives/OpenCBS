using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenCBS.HRM.Interface.View
{
    public interface IfrmAdvancePayment
    {
        bool Enabled { get; set; }
        FormWindowState WindowState { get; set; }

        void CallFromAdvanceRegister(decimal decGrid, frmAdvanceRegister frmAdvanceRegister);
        void ReturnFromEmployeeCreation(decimal decIdForOtherForms);
    }
}
