/**
 * The example of the use of Free.iso8583
 * ISO 8583 Message Client in the purpose for doing stress test of message server
 * It will flood the server with a lot of request (number of request is specified by argument)
 * Developed by AT Mulyana (atmulyana@yahoo.com)
 * CopyLeft (ᴐ) 2009, 2012, 2013
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using Free.iso8583;
using Free.iso8583.example.model;

namespace StressTester
{
    class NullLogger : ILoggerOutput
    {
        public void Write(string log, LogLevel level)
        {
            Write(log);
        }

        public void Write(string log)
        {
        }

        public void WriteLine(string log)
        {
        }


        public void WriteLine(string log, LogLevel level)
        {
            WriteLine(log);
        }
    }

    class MessageClientWorker
    {
        public static String ServerAddress = "127.0.0.1";
        public static int ServerPort = 3107;
        public static bool IsSslEnabled = false;

        private Object _model;
        private int _id;
        private DateTime _startTime;
        private ManualResetEvent _responseEvent = new ManualResetEvent(false);

        private MessageClientWorker()
        {
        }

        public MessageClientWorker(Object model, int id, ManualResetEvent responseEvent)
        {
            _model = model;
            _id = id;
            _responseEvent = responseEvent;
        }

        private Object Callback(Object responseMode)
        {
            DateTime endTime = DateTime.Now;
            TimeSpan duration = endTime.Subtract(_startTime);
            System.Console.Out.WriteLine("Process {0}: End time at {1}; Duration = {2}; Status = {3}",
                _id, endTime.ToString("HH:mm:ss") + "." + endTime.Millisecond, duration.ToString(),
                responseMode != null ? "Success" : "Failed");
            _responseEvent.Set();
            lock (Program.sync)
            {
                Program.numOfRequest--;
                if (responseMode != null) Program.numOfSuccess++; else Program.numOfFailed++;
                if (Program.numOfRequest <= 0) Program.endTime = DateTime.Now;
            }
            return null;
        }

        public void StartProcess(Object param)
        {
            _responseEvent.Reset();
            Program.startProcessEvent.WaitOne(); //Tries to start all process exactly at the same time
            _startTime = DateTime.Now;
            System.Console.Out.WriteLine("Process {0}: Start time at {1}.{2}",
                _id, _startTime.ToString("HH:mm:ss"), _startTime.Millisecond);
            MessageClient client = new MessageClient(ServerAddress, ServerPort, _model, this.Callback);
            client.IsSslEnabled = IsSslEnabled;
            client.SendModel();
            //_responseEvent.WaitOne();
        }
    }

    class Program
    {
        internal static Object sync = new Object();
        internal static int numOfRequest;
        internal static int numOfSuccess = 0;
        internal static int numOfFailed = 0;
        internal static ManualResetEvent startProcessEvent = new ManualResetEvent(false);
        internal static DateTime startTime, endTime;

        private static ReqTransferInquiry0100 model = new ReqTransferInquiry0100()
        {
            PrimaryAccountNumber = null,
            ProcessingCode = new byte[] { 0x39, 0x10, 0x00 },
            TransactionAmount = 7250000.00m,
            SystemAuditTraceNumber = new byte[] { 0x00, 0x31, 0x07 },
            ExpirationDate = null,
            PosEntryMode = new byte[] { 0x00, 0x21 },
            NetworkInternationalId = "005",
            PosConditionCode = new byte[] { 0x00 },
            Track2Data = new byte[] { 0x49, 0x91, 0x87, 0x02, 0x73, 0x00, 0x27, 0x3C,
                0xD6, 0x2B, 0x27, 0x1A, 0x0A, 0x38, 0x08, 0x00, 0x80, 0x12, 0x40 },
            TerminalId = "12341234",
            MerchantId = "123451234512345",
            AdditionalData = null,
            CardholderPinBlock = new byte[] { 0x77, 0xBB, 0xAA, 0x66, 0x78, 0x3B, 0xD7, 0xCC },
            InvoiceNumber = "003107",
            TransferData = new Bit63Content
            {
                TableId = "78",
                BeneficiaryInstitutionId = "00000002314",
                BeneficiaryAccountNumber = "0123456789012345",
                BeneficiaryName = null,
                CustomerReferenceNumber = null,
                IssuerInstitutionId = null,
                CardholderAccountNumber = null,
                CardholderName = null,
                InformationData = null
            },
            MessageAuthenticationCode = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00 }
        };

        static void Main(string[] args)
        {
            try
            {
                numOfRequest = int.Parse(args[0]);
                if (args.Length > 1) MessageClientWorker.ServerAddress = args[1];
                if (args.Length > 2) MessageClientWorker.ServerPort = int.Parse(args[2]);
                if (args.Length > 3) MessageClientWorker.IsSslEnabled = args[3].ToLower() == "ssl";
            }
            catch
            {
                System.Console.Out.WriteLine("Invalid argument! Format:" + Environment.NewLine
                    + typeof(Program).Namespace
                    + " numberOfRequest [ServerAddress] [ServerPort]");
                return;
            }

            Logger.GetInstance().ReplaceOutput(new NullLogger()); //Don't show any error message.

            String configPath = Util.GetAssemblyDir(typeof(StressTester.Program)) + "/../../../../src/messagemap-config.xml";
            Stream fileConfig = null;
            try
            {
                fileConfig = File.Open(configPath, FileMode.Open);
            }
            catch (FileNotFoundException ex)
            {
                Logger.GetInstance().Write(ex);
            }
            if (fileConfig != null) MessageProcessor.GetInstance().Load(fileConfig);
            else
            {
                System.Console.WriteLine("Cannot load the configuration");
                return;
            }

            System.Console.Out.WriteLine("Starts processing {0} request", numOfRequest);
            //ThreadPool.SetMaxThreads(numOfProcess, numOfProcess);
            ManualResetEvent[] responseEvents = new ManualResetEvent[numOfRequest];
            Thread[] workers = new Thread[numOfRequest];
            for (int i = 0; i < numOfRequest; i++)
            {
                responseEvents[i] = new ManualResetEvent(false);
                MessageClientWorker client = new MessageClientWorker(model, i + 1, responseEvents[i]);
                //ThreadPool.QueueUserWorkItem(client.StartProcess);
                workers[i] = new Thread(client.StartProcess);
            }
            startProcessEvent.Reset();
            for (int i = 0; i < numOfRequest; i++) workers[i].Start(null);
            startTime = DateTime.Now;
            startProcessEvent.Set();

            //WaitHandle.WaitAll(responseEvents); //Only supports 64 threads at max
            while (numOfRequest > 0)
            {
                Thread.Sleep(1000);
            }
            TimeSpan duration = endTime.Subtract(startTime);
            System.Console.Out.WriteLine("Done. Success: " + numOfSuccess + ", Failed: " + numOfFailed
                + ", Total Time = " + duration.ToString());
        }
    }
}
