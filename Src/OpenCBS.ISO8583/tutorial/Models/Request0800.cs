using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class Request0800
    {
        public DateTime TransmissionDateTime { get; set; }
        public int SystemAuditTraceNumber { get; set; }
        public String AdditionalData { get; set; }
        public String NetworkManagementInformationCode { get; set; }
    }
}
