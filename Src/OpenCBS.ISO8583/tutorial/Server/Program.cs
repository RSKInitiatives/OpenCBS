using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Free.iso8583;
using Models;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            MessageListener messageListener = new MessageListener();

            try
            {
                MessageListener.SetConfigPath("../../server-conf.xml", Util.GetAssemblyDir(new Program()));
                messageListener.StartListeningEvent += OnStartListeningEvent;
                messageListener.Start();
            }
            finally
            {
                messageListener.Stop();
            }
        }
        
        private static void OnStartListeningEvent(Object sender, ListeningEventArgs e)
        {
            Console.Out.WriteLine("Starts listening port " + e.Port
                + (e.IPAddress.ToString() != "0.0.0.0" ? " on address " + e.IPAddress : "")
                + " ...");
        }
        
        public static Response0810 ProcessRequest(Request0800 request)
        {
            Console.WriteLine("==== Begin: Request ====");
            Console.WriteLine(Util.GetReadableStringFromModel(request));
            Console.WriteLine("==== End: Request ====");

            Response0810 resp = new Response0810
            {
                TransmissionDateTime = DateTime.Now,
                /*SystemAuditTraceNumber = request.SystemAuditTraceNumber,
                AdditionalData = request.AdditionalData,
                NetworkManagementInformationCode = request.NetworkManagementInformationCode,*/
                ResponseCode = "00",
                MessageAuthenticationCode = MessageUtility.StringToHex("0102030405060708")
            };
            return resp;
        }
    }
}
