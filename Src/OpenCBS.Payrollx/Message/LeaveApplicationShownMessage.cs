using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyMessenger;

namespace OpenCBS.Payroll.Message
{
    public class LeaveApplicationShownMessage : ITinyMessage
    {
        public LeaveApplicationShownMessage(object sender)
        {
            Sender = sender;
        }
        public object Sender { get; private set; }
    }
}
