using System;
using Free.iso8583;
using System.IO;

namespace Iso8583WebClient
{
    public class FreeIso8583Logger : ILoggerOutput
    {
        private StreamWriter _logFile;
        public String LogFilePath { get; private set; }
        public Object Sync = new Object();

        protected FreeIso8583Logger() { }
        public FreeIso8583Logger(String rootDir)
        {
            LogFilePath = rootDir + "/App_Data/Free.iso8583.log";
            Open();
        }

        private void Open()
        {
            _logFile = new StreamWriter(new FileStream(LogFilePath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite));
            _logFile.AutoFlush = true;
        }
        
        public void WriteLine(String log, LogLevel level)
        {
            WriteLine(log);
        }
        public void WriteLine(String log)
        {
            lock(Sync) _logFile.WriteLine(log);
        }
        public void Write(String log, LogLevel level)
        {
            Write(log);
        }
        public void Write(String log)
        {
            lock (Sync) _logFile.Write(log);
        }

        public void Reset()
        {
            lock (Sync)
            {
                _logFile.Close();
                Open();
            }
        }
    }
}