using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeScales.Http;
using CodeScales.Http.Common;
using CodeScales.Http.Methods;
using CodeScales.Http.Entity;
using System.Net;
using System.IO;

namespace OpenCBS.Messaging.Custom
{
    public class HttpUtil
    {
        public static string sendPost(String Url, List<NameValuePair> nameValuePairList)
        {
            HttpClient client = new HttpClient();
            HttpPost postMethod = new HttpPost(new Uri(Url));

            UrlEncodedFormEntity formEntity = new UrlEncodedFormEntity(nameValuePairList, Encoding.UTF8);
            postMethod.Entity = formEntity;

            HttpResponse response = client.Execute(postMethod);

            Console.WriteLine("Response Code: " + response.ResponseCode);
            Console.WriteLine("Response Content: " + EntityUtils.ToString(response.Entity));


            return EntityUtils.ToString(response.Entity);
        }

        public static string sendGet(String Url)
        {
            HttpClient httpClient = new HttpClient();

            HttpGet httpGet = new HttpGet(new Uri(Url));

            HttpResponse httpResponse = httpClient.Execute(httpGet);

            Console.WriteLine("Response Code: " + httpResponse.ResponseCode);

            Console.WriteLine("Response Content: " + EntityUtils.ToString(httpResponse.Entity));

            return EntityUtils.ToString(httpResponse.Entity);
        }

    }     
}
