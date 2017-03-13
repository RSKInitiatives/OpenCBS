/**
 * ISO 8583 Message Server
 * The example of the use of Free.iso8583
 * Developed by AT Mulyana (atmulyana@yahoo.com)
 * CopyLeft (ᴐ) 2009-2015
 **/
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading;
using Free.iso8583;
using Free.iso8583.example;
using Free.iso8583.example.model;
using Free.iso8583.example.process;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace Iso8583Server
{
    public class Program
    {
        public static void DoBalanceInquiryProcess(
            ReqBalanceInquiry0100 requestModel,
            RespBalanceInquiry0110 responseModel)
        {
            try
            {
                //It should be queried from database
                responseModel.TransactionAmount = 82000000m;
                responseModel.AdditionalData = "ID010Balance OK";
                if (responseModel.TransactionAmount > 0)
                    responseModel.AdditionalAmount = responseModel.TransactionAmount;
                else
                    responseModel.AdditionalAmount = 0;
            }
            catch
            {
                responseModel.ResponseCode = ResponseCode.SYSTEM_ERROR; //Or another error code
            }
        }

        public static void DoTransferProcess(ReqTransfer0200 requestModel, RespTransfer0210 responseModel)
        {
            //It should be queried from database
            ResponseDataEntry48 data = new ResponseDataEntry48();
            decimal fee = 0;
            if (requestModel.AdditionalData is RequestDataEntry48)
                fee = ((RequestDataEntry48)requestModel.AdditionalData).Fee;
            if (requestModel.AdditionalData is RequestTlvDataEntry48)
                fee = ((RequestTlvDataEntry48)requestModel.AdditionalData).TD.Fee;
            data.ID = "Transfer Succesful";
            data.IP = new String[] {
                "Transfer Amount: " + requestModel.TransactionAmount,
                "Fee            : " + fee,
                "Total          : " + (requestModel.TransactionAmount + fee)
            };
            responseModel.AdditionalData = data;
        }

        public static void DoLogonProcess(ReqLogon0800 requestModel, RespLogon0810 responseModel)
        {
            try
            {
                bool isLoginOk = true; //Not so simple, must do some checking process here
                if (isLoginOk)
                    responseModel.ResponseCode = ResponseCode.COMPLETED_SUCCESSFULLY;
                else
                    responseModel.ResponseCode = ResponseCode.TERMINAL_NOT_PERMITTED;

                //Must be replaced by true query
                byte[] pin = new byte[] { 0x2e, 0xde, 0xa3, 0xb4, 0x09, 0x07, 0x64, 0x54 };
                byte[] macAddress = new byte[] { 0xfe, 0xfe, 0xfe, 0xfe, 0xfe, 0xfe, 0xfe, 0xfe };
                byte[] merchant = MessageUtility.StringToAsciiArray("MerchantNameInSubang");
                responseModel.PrivateUse = pin.Concat(macAddress).Concat(merchant).ToArray();
            }
            catch
            {
                responseModel.ResponseCode = ResponseCode.SYSTEM_ERROR;
            }
        }

        private static String GetAbsolutePath(String path)
        {
            try
            {
                if (Path.IsPathRooted(path)) return path;
                return Path.GetFullPath(path);
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("There is an error. The application will halt!");
                Logger.GetInstance().Write(ex);
                Environment.Exit(1);
            }
            return null;
        }

        private static bool RemoteCertificateValidationCallback(Object sender, X509Certificate certificate, X509Chain chain,
            SslPolicyErrors sslPolicyErrors)
        {
            System.Console.Out.WriteLine("Certificate Issuer = " + (certificate==null?"null":certificate.Issuer));
            System.Console.Out.WriteLine("Certificate Error = " + sslPolicyErrors.ToString());
            return true;
        }

        private static void OnStartListeningEvent(Object sender, ListeningEventArgs e)
        {
            Console.Out.WriteLine("*****************************************************************************");
            Console.Out.WriteLine("If you feel   Free.iso8583   is useful, please donate via PayPal by visiting:");
            Console.Out.WriteLine(" ");
            Console.Out.WriteLine("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=4ZKPC3URPZ24U");
            Console.Out.WriteLine(" ");
            Console.Out.WriteLine("*****************************************************************************");
            Console.Out.WriteLine("Starts listening port " + e.Port
                + (e.IPAddress.ToString() != "0.0.0.0" ? " on address " + e.IPAddress : "")
                + " ...");
        }
        
        public static void Main(string[] args)
        {
            String thisFileDir = Util.GetAssemblyDir(new Program());
            Environment.CurrentDirectory = thisFileDir;

            log4net.Config.XmlConfigurator.Configure();
            Logger.GetInstance().AddOutput(new Log4NetLogger());
            //Logger.GetInstance().ReplaceOutput(new Log4NetLogger());
            //Logger.GetInstance().Level = LogLevel.Notice;

            Properties.Settings settings = Properties.Settings.Default;

            String sslCertificateFile = null;
            System.Security.SecureString sslCertificatePwd = null;
            try { sslCertificateFile = (String)settings["sslCertificateFile"]; } catch (SettingsPropertyNotFoundException) { }
            if (sslCertificateFile != null)
            {
                sslCertificateFile = sslCertificateFile.Trim();
                sslCertificateFile = sslCertificateFile == "" ? null : GetAbsolutePath(sslCertificateFile);
            }
            try {
                String pwd = (String)settings["sslCertificatePwd"];
                pwd = pwd.Trim();
                if (!String.IsNullOrEmpty(pwd))
                {
                    sslCertificatePwd = new System.Security.SecureString();
                    Char[] pwdChars = pwd.ToCharArray();
                    for (int i = 0; i < pwdChars.Length; i++) sslCertificatePwd.AppendChar(pwdChars[i]);
                }
            }
            catch (SettingsPropertyNotFoundException)
            {
            }
            
            Message0100Process.ProcessBalanceInquiry = DoBalanceInquiryProcess;
            Message0200Process.ProcessTransfer = DoTransferProcess;
            Message0800Process.ProcessLogon = DoLogonProcess;

            
            //***** 1: Example of single instance of listener
            MessageListener messageListener = new MessageListener(sslCertificateFile, sslCertificatePwd);
            //messageListener.IPAddress = settings.ipAddress;
            messageListener.Port = settings.port;
            messageListener.MaxConnections = settings.maxConnections;

            //messageListener.IsClientCertificateRequired = true;
            //messageListener.CertificateValidationCallback = RemoteCertificateValidationCallback;

            try
            {
                Type mappingType = Type.GetType(settings.confPath);
                if (mappingType != null)
                {
                    //Console.WriteLine("Using Attribute configuration...");
                    MessageListener.SetConfig(mappingType);
                }
                else
                {
                    //Console.WriteLine("Using XML configuration...");
                    MessageListener.SetConfig(settings.confPath, thisFileDir);
                    //MessageListener.SetConfigPath(settings.confPath, thisFileDir);
                }
                messageListener.StartListeningEvent += OnStartListeningEvent;
                messageListener.Start();
            }
            finally
            {
                messageListener.Stop();
            }
            //***** End 1:



            /*//***** 2: Example of two instances of listener, one listens to non SSL and another listens to SSL
            MessageListener messageListener = new MessageListener(), messageListener2 = null;
            //messageListener.IPAddress = settings.ipAddress;
            messageListener.Port = settings.port;
            messageListener.MaxConnections = settings.maxConnections;
            if (sslCertificateFile != null)
            {
                messageListener2 = new MessageListener(sslCertificateFile, sslCertificatePwd);
                //messageListener2.IPAddress = settings.ipAddress;
                messageListener2.Port = settings.port + 1;
                messageListener.MaxConnections = settings.maxConnections/2;
                messageListener2.MaxConnections = settings.maxConnections/2;
            }

            Thread t = null, t2 = null;
            try
            {
                Type mappingType = Type.GetType(settings.confPath);
                if (mappingType != null) MessageListener.SetConfig(mappingType);
                else MessageListener.SetConfig(settings.confPath, thisFileDir);
                messageListener.StartListeningEvent += OnStartListeningEvent;
                t = new Thread(messageListener.Start);
                t.Start();
                
                if (messageListener2 != null)
                {
                    messageListener2.StartListeningEvent += OnStartListeningEvent;
                    t2 = new Thread(messageListener2.Start);
                    t2.Start();
                }
                
                while(true) Thread.Sleep(int.MaxValue);
            }
            finally
            {
                messageListener.Stop();
                if (messageListener2 != null) messageListener2.Stop();

                if (t != null && t.IsAlive) t.Join();
                if (t2 != null && t2.IsAlive) t2.Join();
            }
            //***** End 2:
            */
        }
    }
}
