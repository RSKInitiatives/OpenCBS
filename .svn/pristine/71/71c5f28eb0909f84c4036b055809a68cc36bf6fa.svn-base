using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeScales.Http.Common;
using OpenCBS.Messaging.Interfaces;
using OpenCBS.Messaging.Custom;

namespace OpenCBS.Messaging.SMS
{
    public class EstoreSms : ISms
    {
        public EstoreSms()
        {
            Url = "http://www.estoresms.com/smsapi.php?";
        }

        public override string GetBalance()
        {
            PostData = String.Format("username={0}&password={1}&balance={2}&", UserName, Password, true);
            return "";
        }

        public override string SendSms()
        {
            List<NameValuePair> nameValuePairList = new List<NameValuePair>();
            nameValuePairList.Add(new NameValuePair("username", UserName));
            nameValuePairList.Add(new NameValuePair("password", Password));
            nameValuePairList.Add(new NameValuePair("sender", MessageFrom));
            nameValuePairList.Add(new NameValuePair("recipient", MessageTo));
            nameValuePairList.Add(new NameValuePair("message", Message));

            return HttpUtil.sendPost(Url, nameValuePairList);
        }
    }
}