/**
 * ISO 8583 Message Client
 * The example of the use of Free.iso8583
 * Developed by AT Mulyana (atmulyana@yahoo.com)
 * CopyLeft (ᴐ) 2009-2015
 **/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using Free.iso8583;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Diagnostics;
using Free.iso8583.config;
using Free.iso8583.example;

namespace Iso8583Client
{
    public partial class MainForm : Form, ILoggerOutput, IModelParsingReceiver
    {
        private MessageClient _client = null;
        X509CertificateCollection _certificates = new X509CertificateCollection();
        private Object _reqModel = null;
        private bool _isAttributeConfig;
        
        public MainForm()
        {
            InitializeComponent();
        }

        public Object PrintModel(Object model)
        {
            if (this.InvokeRequired)
            {
                return this.Invoke(new MessageCallback(this.PrintModel), model);
            }

            if (model == null)
            {
                MessageBox.Show("An error occured. Try to check/uncheck 'Use SSL/TLS' option.");
            }
            else
            {
                //WriteLog("Print response bytes..");
                txtResponse.Text = MessageUtility.HexToReadableString(ParsedMessage.AllBytes)
                    + Environment.NewLine + Environment.NewLine;
                //WriteLog("End Print response bytes..");

                //WriteLog("Print response model..");
                txtResponse.Text += Util.GetReadableStringFromModel(model);
                //WriteLog("End Print model..");
            }
            return null;
        }

        private void WriteLog(String log)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<string>(this.WriteLog), log);
                return;
            }
            int addedLength = log.Length + Environment.NewLine.Length;
            if (txtLog.Text.Length + addedLength > txtLog.MaxLength)
                txtLog.Text = (txtLog.Text.Length > addedLength ? txtLog.Text.Substring(addedLength) : "")
                    + log + Environment.NewLine;
            else
                txtLog.Text += log + Environment.NewLine;
        }

        private bool LoadConfig()
        {
            if (chkConfig.Checked)
            {
                try
                {
                    MessageProcessor.GetInstance().Load(typeof(MessageToModelMapping));
                    WriteLog("Config loaded..");
                }
                catch (Exception ex)
                {
                    Logger.GetInstance().Write(ex);
                    return false;
                }
            }
            else
            {
                String configPath = Util.GetAssemblyDir(this) + "/../../../../src/messagemap-config.xml";
                Stream fileConfig = null;
                try
                {
                    fileConfig = File.Open(configPath, FileMode.Open);
                }
                catch (FileNotFoundException ex)
                {
                    Logger.GetInstance().Write(ex);
                }
                if (fileConfig != null)
                {
                    using (fileConfig) MessageProcessor.GetInstance().Load(fileConfig);
                    WriteLog("Config loaded..");
                }
                else return false;
            }

            _isAttributeConfig = chkConfig.Checked;
            return true;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            X509Certificate2 cert = new X509Certificate2();
            cert.Import(Util.GetAssemblyDir(this) + "/../../../sslcerts/client.p12", "", X509KeyStorageFlags.MachineKeySet);
            _certificates.Add(cert);
            
            Logger.GetInstance().ReplaceOutput(this);

            txtServerHost.Text = "localhost";
            txtServerPort.Text = MessageListener.PORT+"";

            if (!LoadConfig()) return;

            _exampleMessages.Clear();
            foreach (KeyValuePair<String, EditedItem> kvp in EditedItems.Item)
            {
                MessageCompiler compiler = new MessageCompiler(kvp.Value.DefaultModel);
                compiler.Compile();
                _exampleMessages[kvp.Key] = MessageUtility.HexToReadableString(compiler.CompiledMessage.GetAllBytes(), 100); 
            }

            cmbExampleMessages.Items.Clear();
            foreach (String msgName in _exampleMessages.Keys) cmbExampleMessages.Items.Add(msgName);
            cmbExampleMessages.SelectedIndex = 0;
        }

        #region ILoggerOutput Members
        public void WriteLine(string log, LogLevel level)
        {
            WriteLine(log);
        }

        public void WriteLine(string log)
        {
            WriteLog(log);
        }

        public void Write(string log, LogLevel level)
        {
            Write(log);
        }

        public void Write(string log)
        {
            WriteLog(log);
        }
        #endregion

        #region IModelParsingReceiver Members

        public IParsedMessage ParsedMessage
        {
            get; set;
        }

        #endregion

        private X509Certificate LocalCertificateSelectionCallback(Object sender, string targetHost,
            X509CertificateCollection localCertificates, X509Certificate remoteCertificate, string[] acceptableIssuers)
        {
            WriteLog("[Select Certificate] targetHost: " + targetHost
                + "; Local Issuer: " + (localCertificates.Count > 0 ? localCertificates[0].Issuer : "null")
                + "; Issuer: " + (remoteCertificate==null?"null":remoteCertificate.Issuer)
            );
            return localCertificates.Count > 0 ? localCertificates[0] : null;
        }

        private bool RemoteCertificateValidationCallback(Object sender, X509Certificate certificate, X509Chain chain,
            SslPolicyErrors sslPolicyErrors)
        {
            StringBuilder str = new StringBuilder();
            foreach (X509ChainStatus stat in chain.ChainStatus)
            {
                str.Append(stat.Status.ToString()).Append(",");
            }
            WriteLog("[Validate Certificate] Issuer = " + (certificate == null ? "null" : certificate.Issuer)
                + "; Error = " + sslPolicyErrors.ToString()
                + "; ChainStatus = " + str.ToString()
            );
            return true;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (_isAttributeConfig != chkConfig.Checked) if (!LoadConfig()) return;

            byte[] reqMsg = null;
            if (_reqModel == null)
            {
                String sReq = txtRequest.Text.Trim();
                if (String.IsNullOrEmpty(sReq)) return;
                Regex ws = new Regex(@"\s+");
                sReq = ws.Replace(sReq, "");
                reqMsg = MessageUtility.StringToHex(sReq, true);
                if (reqMsg == null)
                {
                    MessageBox.Show("Invalid hexadecimal string.");
                }
            }
            
            try
            {
                _client = new MessageClient(txtServerHost.Text, int.Parse(txtServerPort.Text), null, this.PrintModel);
                _client.IsSslEnabled = chkSsl.Checked;
                /*if (_client.IsSslEnabled)
                {
                    _client.ClientCertificates = _certificates;
                    _client.CertificateSelectionCallback = this.LocalCertificateSelectionCallback;
                    _client.CertificateValidationCallback = this.RemoteCertificateValidationCallback;
                }*/

                if (_reqModel == null)
                {
                    Thread thread = new Thread(_client.SendBytes);
                    thread.Start(reqMsg);
                }
                else
                {
                    _client.Model = _reqModel;
                    _client.SendModel();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot connect to the server." + Environment.NewLine + ex.Message);
            }
            finally
            {
                btnStop_Click(sender, e);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {

            if (_client != null) _client.Close();
            _client = null;
        }

        private void ClearMessage()
        {
            txtRequest.Text = "";
            txtResponse.Text = "";
            txtRequest.ReadOnly = false;
            _reqModel = null;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtLog.Text = "";
            ClearMessage();
        }

        private static Dictionary<String, String> _exampleMessages = new Dictionary<string, string>()
        {
//            { "Transfer Inquiry",
//@"01 01 60 00 05 80 53 01 00 30 20 05 80 20 C0 10 07 39 10 00 00 07 25 00 00 00 00 31 07 00 21 00 05 00
//37 49 91 87 02 73 00 27 3C D6 2B 27 1A 0A 38 08 00 80 12 40 31 32 33 34 31 32 33 34 31 32 33 34 35 31 32
//33 34 35 31 32 33 34 35 77 BB AA 66 78 3B D7 CC 00 06 30 30 33 31 30 37 01 56 37 38 30 30 30 30 30 30 30
//32 33 31 34 30 31 32 33 34 35 36 37 38 39 30 31 32 33 34 35 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20
//20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 31 32 33 20 20 20 20 20
//20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20
//20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20
//20 20 20 20 20 20 20 00 00 00 00 00 00 00 00"
//            },
//            { "Transfer",
//@"01 01 60 00 05 80 53 02 00 30 20 05 80 20 C0 10 07 40 10 00 00 07 25 00 00 00 00 31 07 00 21 00 05 00
//37 49 91 87 02 73 00 27 3C D6 2B 27 1A 0A 38 08 00 80 12 40 31 32 33 34 31 32 33 34 31 32 33 34 35 31 32
//33 34 35 31 32 33 34 35 77 BB AA 66 78 3B D7 CC 00 06 30 30 33 31 30 37 01 56 37 38 30 30 30 30 30 30 30
//32 33 31 34 30 31 32 33 34 35 36 37 38 39 30 31 32 33 34 35 20 20 20 20 20 20 20 20 20 20 20 20 44 45 53
//54 49 4E 41 54 49 4F 4E 20 4E 41 4D 45 20 47 45 4E 45 52 41 54 45 44 20 20 20 20 31 32 33 20 20 20 20 20
//20 20 20 20 20 20 20 20 30 30 30 30 30 30 30 32 33 31 34 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20
//20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20
//20 20 20 20 20 20 20 00 00 00 00 00 00 00 00"
//            },
//            { "TransferSB",
//@"01 1B 60 00 05 80 53 02 00 B0 20 05 80 20 C0 10 07 00 00 00 00 01 00 00 00 40 10 00 00 07 25 00 00 00
//00 31 07 00 21 00 05 00 37 49 91 87 02 73 00 27 3C D6 2B 27 1A 0A 38 08 00 80 12 40 31 32 33 34 31 32 33
//34 31 32 33 34 35 31 32 33 34 35 31 32 33 34 35 77 BB AA 66 78 3B D7 CC 00 06 30 30 33 31 30 37 01 56 37
//38 30 30 30 30 30 30 30 32 33 31 34 30 31 32 33 34 35 36 37 38 39 30 31 32 33 34 35 20 20 20 20 20 20 20
//20 20 20 20 20 44 45 53 54 49 4E 41 54 49 4F 4E 20 4E 41 4D 45 20 47 45 4E 45 52 41 54 45 44 20 20 20 20
//31 32 33 20 20 20 20 20 20 20 20 20 20 20 20 20 30 30 30 30 30 30 30 32 33 31 34 20 20 20 20 20 20 20 20
//20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20
//20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 00 00 00 00 00 00 00 00 00 16 54 52 41 4E 53 41 43 54 49 4F
//4E 20 4E 4F 54 45"
//            },
//            { "Balance Inquiry",
//@"00 5D 60 00 05 80 53 01 00 20 20 05 80 20 C0 10 05 31 10 00 00 31 07 00 21 00 05 00 37 49 91 87 02 73
//00 27 3C D6 2B 27 1A 0A 38 08 00 80 12 40 31 32 33 34 31 32 33 34 31 32 33 34 35 31 32 33 34 35 31 32 33
//34 35 77 BB AA 66 78 3B D7 CC 00 06 30 30 33 31 30 37 00 00 00 00 00 00 00 00"
//            },
//            { "Logon",
//@"00 1F 60 00 05 80 53 08 00 20 20 01 00 00 80 00 00 92 00 00 00 31 07 00 05 31 32 33 34 31 32 33 34"
//            }
        };
        private void btnChoose_Click(object sender, EventArgs e)
        {
            ClearMessage();
            String messageType = cmbExampleMessages.Text;
            try
            {
                txtRequest.Text = _exampleMessages[messageType];
            }
            catch (Exception ex)
            {
                MessageBox.Show(messageType + " : " + ex.Message);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            ClearMessage();
            MessageEditor editor = new MessageEditor();
            if (!editor.SetEditedItem(cmbExampleMessages.Text)) return;
            if (editor.ShowDialog(this) == DialogResult.OK)
            {
                _reqModel = editor.ResultModel;
                txtRequest.Text = Util.GetReadableStringFromModel(_reqModel);
                txtRequest.ReadOnly = true;
            }
            editor.Close();
            editor.Dispose();
        }

        private void linkPaypal_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=4ZKPC3URPZ24U"));
        }
    }
}
