using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Iso8583WebClient.Models
{
    public class Iso8583Request
    {
        public String ServerHost { get; set; }
        public int ServerPort { get; set; }
        public bool IsSSL { get; set; }
        public String RequestString { get; set; }
    }
}