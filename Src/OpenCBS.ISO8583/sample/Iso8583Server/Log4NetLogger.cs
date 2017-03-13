/**
 * The example of the use of Free.iso8583, especially how to create a logger for this engine
 * Developed by AT Mulyana (atmulyana@yahoo.com)
 * CopyLeft (ᴐ) 2009
 **/
using System;
using log4net;
using log4net.Config;

namespace Iso8583Server
{
    public class Log4NetLogger : Free.iso8583.ILoggerOutput
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(Log4NetLogger));
        
        #region ILoggerOutput Members
        public void WriteLine(string log, Free.iso8583.LogLevel level)
        {
            WriteLine(log);
        }
        
        public void WriteLine(string log)
        {
            logger.Info(log + Environment.NewLine);
        }

        public void Write(string log, Free.iso8583.LogLevel level)
        {
            Write(log);
        }

        public void Write(string log)
        {
            logger.Info(log);
        }
        #endregion
    }
}
