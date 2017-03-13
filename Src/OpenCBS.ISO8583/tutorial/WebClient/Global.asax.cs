using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Free.iso8583;
using System.IO;

namespace WebClient
{
    public class Global : System.Web.HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            String rootDir = Server.MapPath("~/");
            Logger.GetInstance().ReplaceOutput(new FreeIso8583Logger(rootDir + "/App_Data/Free.iso8583.log"));
            Logger.GetInstance().Level = LogLevel.Notice;
            MessageProcessor.GetInstance().Load(File.Open(rootDir + "/../Client/client-conf.xml", FileMode.Open));
			Logger.GetInstance().WriteLine("Config loaded...");
        }

        void Application_End(object sender, EventArgs e)
        {
        }

        void Application_Error(object sender, EventArgs e)
        {
        }

        void Session_Start(object sender, EventArgs e)
        {
        }

        void Session_End(object sender, EventArgs e)
        {
        }
    }

    public class FreeIso8583Logger : ILoggerOutput
    {
        private StreamWriter _logFile;

        public FreeIso8583Logger(String logFilePath)
        {
            _logFile = new StreamWriter(new FileStream(logFilePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite));
            _logFile.AutoFlush = true;
        }

        public void WriteLine(String log, LogLevel level)
        {
            _logFile.WriteLine(log);
        }
        public void WriteLine(String log)
        {
            _logFile.WriteLine(log);
        }
        public void Write(String log, LogLevel level)
        {
            _logFile.Write(log);
        }
        public void Write(String log)
        {
            _logFile.Write(log);
        }
    }
}
