using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyMessenger;

namespace OpenCBS.HRM.Message
{
    public class DesignationShownMessage : ITinyMessage
    {
        public DesignationShownMessage(object sender)
        {
            Sender = sender;
        }
        public object Sender { get; private set; }
    }
}
