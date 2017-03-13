using Free.iso8583;
using Free.iso8583.example;
using Free.iso8583.example.model;
using Free.iso8583.example.process;
using OpenCBS.PostBridge.Encryption;
using OpenCBS.PostBridge.MessageProcessor;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace OpenCBS.PostBridge
{
    public class Program
    {                 
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
            System.Console.Out.WriteLine("Certificate Issuer = " + (certificate == null ? "null" : certificate.Issuer));
            System.Console.Out.WriteLine("Certificate Error = " + sslPolicyErrors.ToString());
            return true;
        }

        private static void OnStartListeningEvent(Object sender, ListeningEventArgs e)
        {
            Console.Out.WriteLine("*****************************************************************************");
            Console.Out.WriteLine("****************** OpenCBS Postilion PostBridge Server Console **************");
            Console.Out.WriteLine(" ");
            Console.Out.WriteLine(" ");
            Console.Out.WriteLine("*****************************************************************************");
            Console.Out.WriteLine("Starts listening port " + e.Port
                + (e.IPAddress.ToString() != "0.0.0.0" ? " on address " + e.IPAddress : "")
                + " ...");
        }

        public static void Main2(string[] args)
        {
            String thisFileDir = Util.GetAssemblyDir(new Program());
            //Environment.CurrentDirectory = thisFileDir;

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
            try
            {
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

            Message0100Process.ProcessBalanceInquiry = Authorization.BalanceInquiry;
            Message0200Process.ProcessTransfer = Financial.Transfer;
            Message0800Process.ProcessLogon = Network.Logon;


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

        public static void Main(string[] args)
        {
            byte[] key256 = new byte[32];
            for (int i = 0; i < 32; i++)
                key256[i] = Convert.ToByte(i % 256);

            string message = "Hello World";
            string password = "560A18CD-6346-4CF0-A2E8-671F9B6B9EA9";

            byte[] nonSecretOrg = Encoding.UTF8.GetBytes("Pay Bob Zero Dollars");
            byte[] nonSecretMod = Encoding.UTF8.GetBytes("Pay Bob $ 1,000,000.");

            // Encrypt with associated data 
            //string encrypted = AESGCM.SimpleEncrypt(message, key256, nonSecretOrg);
            string encrypted = AESThenHMAC.SimpleEncryptWithPassword(message, password, nonSecretOrg);
            Console.WriteLine("AESThenHMAC Encrypted: {0}", encrypted);

            // Decrypt with original associated data
            //string decrypted = AESGCM.SimpleDecrypt(encrypted, key256, nonSecretOrg.Length);
            string decrypted = AESThenHMAC.SimpleDecryptWithPassword(encrypted, password, nonSecretOrg.Length);

            Console.WriteLine("AESThenHMAC Decrypted: {0}", decrypted);
            //Console.WriteLine("Auth cleartext: {0}", Encoding.UTF8.GetString(nonSecretOrg));

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();


            var secret = AESCBC.EncryptString(password, message);
            //Console.WriteLine("AESCBC Encrypted: {0}", BitConverter.ToString(secret));
            Console.WriteLine("AESCBC Encrypted: {0}", Convert.ToBase64String(secret));

            var recovered = AESCBC.DecryptString(password, secret);
            Console.WriteLine("AESCBC Decrypted: {0}", recovered);

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            Rijndael.Inputkey = password;

            encrypted = Rijndael.EncryptRijndael(message, "12345678");
            //Console.WriteLine("AESCBC Encrypted: {0}", BitConverter.ToString(secret));
            Console.WriteLine("Rijndael Encrypted: {0}", encrypted);

            decrypted = Rijndael.DecryptRijndael(encrypted, "12345678");
            Console.WriteLine("Rijndael Decrypted: {0}", decrypted);


            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            
            string passPhrase = "Pas5pr@se";        // can be any string
            string saltValue = "12345678";//"s@1tValue";        // can be any string
            string hashAlgorithm = "SHA1";             // can be "MD5"
            int passwordIterations = 10000;                // can be any number
            string initVector = "@1B2c3D4e5F6g7H8"; // must be 16 bytes
            int keySize = 128;// 256;                // can be 192 or 128
            
            string cipherText = RijndaelSimple.Encrypt
            (
                message,
                passPhrase,
                saltValue,
                hashAlgorithm,
                passwordIterations,
                initVector,
                keySize
            );

            Console.WriteLine(String.Format("RijndaelSimple Encrypted : {0}", cipherText));

            message = RijndaelSimple.Decrypt
            (
                cipherText,
                passPhrase,
                saltValue,
                hashAlgorithm,
                passwordIterations,
                initVector,
                keySize
            );

            Console.WriteLine(String.Format("RijndaelSimple Decrypted : {0}", message));

            Console.ReadLine();
        }
    }
}
