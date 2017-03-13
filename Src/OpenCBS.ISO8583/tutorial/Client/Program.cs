using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Free.iso8583;
using Models;

namespace Client
{
    public class Program : IModelParsingReceiver
    {
        static void Main(string[] args)
        {
            new Program().Run();
        }

        public void Run()
        {
            Console.WriteLine(this.GetType().AssemblyQualifiedName);
            String configPath = Util.GetAssemblyDir(this) + "/../../client-conf.xml";
            try
            {
                MessageProcessor.GetInstance().Load(File.Open(configPath, FileMode.Open));
            }
            catch (FileNotFoundException ex)
            {
                Logger.GetInstance().Write(ex);
                return;
            }

            Request0800 req = new Request0800
            {
                TransmissionDateTime = DateTime.Now,
                SystemAuditTraceNumber = 3456,
                AdditionalData = "Additional Data",
                NetworkManagementInformationCode = "301"
            };

            Console.WriteLine("==== Begin: Request ====");
            Console.WriteLine(Util.GetReadableStringFromModel(req));
            Console.WriteLine("==== End: Request ====");

            try
            {
                MessageClient client = new MessageClient("localhost", 3107, req);
                client.SendModel();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot process the request." + Environment.NewLine + ex.Message);
            }
        }

        public Object ProcessResponse(Object model)
        {
            if (model == null)
            {
                Console.WriteLine("An error occured!! Cannot get response...");
            }
            else
            {
                Console.WriteLine("==== Begin: Response ====");
                Console.WriteLine(MessageUtility.HexToReadableString(ParsedMessage.AllBytes));
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine(Util.GetReadableStringFromModel(model));
                Console.WriteLine("==== End: Response ====");
            }
            return null;
        }

        public IParsedMessage ParsedMessage
        {
            get;
            set;
        }
    }
}
