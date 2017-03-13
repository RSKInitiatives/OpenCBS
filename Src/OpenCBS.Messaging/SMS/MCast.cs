using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeScales.Http;
using CodeScales.Http.Methods;
using CodeScales.Http.Entity;
using CodeScales.Http.Common;
using OpenCBS.Messaging.Interfaces;
using OpenCBS.Messaging.Custom;

namespace OpenCBS.Messaging.SMS
{


    public class MCast : ISms
    {
        
        public MCast() 
        {
            this.Url = "http://107.20.195.151/mcast_ws_v2/index.php?";
        }
        
        public override string GetBalance()
        {
            PostData = String.Format("cmd={0}&user={1}&password={2}",
                "querybalance", UserName, Password);
            return "";
        }

        public override string SendSms()
        {
            List<NameValuePair> nameValuePairList = new List<NameValuePair>();
            nameValuePairList.Add(new NameValuePair("user", UserName));
            nameValuePairList.Add(new NameValuePair("password", Password));
            nameValuePairList.Add(new NameValuePair("from", MessageFrom));
            nameValuePairList.Add(new NameValuePair("to", MessageTo));
            nameValuePairList.Add(new NameValuePair("message", Message));

            return HttpUtil.sendPost(Url, nameValuePairList);
        }        
    }
}
