using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.IO;
using Free.iso8583;
using Iso8583WebClient.Models;

namespace Iso8583WebClient.Controllers
{
    public class Iso8583Controller : ApiController, IModelParsingReceiver
    {
        private String _outputData = "";
        private System.Threading.AutoResetEvent _iso8583ResponseEvent = new System.Threading.AutoResetEvent(false);

        public String PostRequest(Iso8583Request req)
        {
            byte[] reqMsg = null;
            if (req.RequestString != null)
            {
                String sReq = req.RequestString.Trim();
                if (String.IsNullOrEmpty(sReq)) throw new HttpResponseException(HttpStatusCode.NoContent);
                System.Text.RegularExpressions.Regex ws = new System.Text.RegularExpressions.Regex(@"\s+");
                sReq = ws.Replace(sReq, "");
                reqMsg = MessageUtility.StringToHex(sReq, true);
                if (reqMsg == null)
                {
                    //throw new HttpResponseException(HttpStatusCode.InternalServerError);
                    _outputData = "Error: Invalid request message string (not valid hexadecimal)";
                }
            }

            try
            {
                MessageClient client = new MessageClient(req.ServerHost, req.ServerPort, null, this.PrintModel);
                client.IsSslEnabled = req.IsSSL;

                if (req.RequestString != null)
                {
                    if (reqMsg != null) client.SendBytes(reqMsg);
                }
                else
                {
                    client.Model = HttpContext.Current.Session != null ? HttpContext.Current.Session["model"] : null;
                    if (client.Model != null)
                    {
                        _iso8583ResponseEvent.Reset();
                        client.SendModel();
                        _iso8583ResponseEvent.WaitOne();
                    }
                    else
                        _outputData = "Error: Invalid request message (cannot be null value)";
                }
            }
            catch
            {
                //throw new HttpResponseException(HttpStatusCode.InternalServerError); 
                _outputData = "Error: Cannot send request message to ISO 8583 server.";
            }
            
            if (_outputData.IndexOf("Error: ") == 0) Logger.GetInstance().WriteLine(_outputData);
            
            return _outputData;
        }

        private Object PrintModel(Object model)
        {
            if (model == null)
            {
                //throw new HttpResponseException(HttpStatusCode.InternalServerError);
                _outputData = "Error: An error occured. Try to check/uncheck 'Use SSL/TLS' option.";
            }
            else
            {
                _outputData = MessageUtility.HexToReadableString(ParsedMessage.AllBytes)
                    + Environment.NewLine + Environment.NewLine
                    + Util.GetReadableStringFromModel(model);
            }
            _iso8583ResponseEvent.Set();
            return null;
        }

        public String GetLogs(int startReadLog = 0)
        {
            StreamReader sr = new StreamReader(
                new FileStream((Logger.GetInstance().Output as FreeIso8583Logger).LogFilePath,
                    FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
            );
            String log = "";
            lock ((Logger.GetInstance().Output as FreeIso8583Logger).Sync)
            {
                log = sr.ReadToEnd();
                int i = startReadLog;
                startReadLog = log.Length;
                log = i < log.Length ? log.Substring(i) : "";
            }
            sr.Close();
            return log;
        }

        public String DeleteLogs()
        {
            (Logger.GetInstance().Output as FreeIso8583Logger).Reset();
            if (HttpContext.Current.Session != null) HttpContext.Current.Session.Remove("model");
            return "";
        }

        public IParsedMessage ParsedMessage
        {
            set;
            get;
        }
    }
}
