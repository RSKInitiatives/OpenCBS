using OpenCBS.Messaging.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenCBS.Messaging.SMS
{
    public class BetaSms : ISms
    {
        public BetaSms()
        {
            Url = "http://sms.betasms.com:8080/bulksms/bulksms?";
        }

        public override string GetBalance()
        {
            PostData = String.Format("username={0}&password={1}&balance={2}&", UserName, Password, true);
            return "";
        }

        public override string SendSms()
        {
            PostData = String.Format("username={0}&password={1}&source={2}&type=0&dlr=1&destination={3}&message={4}",
            UserName, Password, MessageFrom, MessageTo, Message);
            return "";
        }
    }
}