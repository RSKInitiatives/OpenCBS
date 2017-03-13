using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCBS.HRM.Interface.View
{
    public interface IfrmEmployeePopup
    {
        void Close();
        void CallFromAdvancePayment(IfrmAdvancePayment frmAdvancePayment, decimal v);
    }
}
