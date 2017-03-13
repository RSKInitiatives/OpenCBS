/**
 *  Distributed as part of Free.iso8583
 *  
 *  Free.iso8583 is ISO 8583 Message Processor library that makes message parsing/compiling esier.
 *  It will convert ISO 8583 message to a model object and vice versa. So, the other parts of
 *  application will only do the rest effort to make business process done.
 *  
 *  This library is free software; you can redistribute it and/or
 *  modify it under the terms of the GNU Lesser General Public
 *  License as published by the Free Software Foundation; either
 *  version 2.1 of the License or (at your option) any later version. 
 *  See http://gnu.org/licenses/lgpl.html
 *
 *  Developed by AT Mulyana (atmulyana@yahoo.com) 2009-2015
 *  The latest update can be found at sourceforge.net
 **/
using System;
using System.Collections.Generic;
using System.Text;

namespace Free.iso8583
{
    public enum LogLevel
    {
        Notice = 0,
        Warning = 1,
        Error = 2,
        MuteLog = 99
    }

    public interface ILoggerOutput
    {
        void WriteLine(String log, LogLevel level);
        void WriteLine(String log);
        void Write(String log, LogLevel level);
        void Write(String log);
    }

    public class ConsoleLoggerOutput : ILoggerOutput
    {
        public void WriteLine(String log, LogLevel level)
        {
            Console.WriteLine(log);
        }

        public void WriteLine(String log)
        {
            Console.WriteLine(log);
        }
        
        public void Write(String log, LogLevel level)
        {
            Console.Write(log);
        }

        public void Write(String log)
        {
            Console.Write(log);
        }
    }

    public class LoggerOutputList : ILoggerOutput
    {
        private IList<ILoggerOutput> _list = new List<ILoggerOutput>();
        public IList<ILoggerOutput> List { get { return _list; } }
        
        public void WriteLine(String log, LogLevel level)
        {
            foreach (ILoggerOutput logger in _list) logger.WriteLine(log, level);
        }

        public void WriteLine(String log)
        {
            WriteLine(log, LogLevel.Notice);
        }

        public void Write(String log, LogLevel level)
        {
            foreach (ILoggerOutput logger in _list) logger.Write(log, level);
        }

        public void Write(String log)
        {
            Write(log, LogLevel.Notice);
        }
    }
    
    public class Logger
    {
        private ILoggerOutput _output = new ConsoleLoggerOutput();
        
        private Logger()
        {
            this.Level = LogLevel.Error;
            this.IsStackTraceShown = true;
        }

        private static Logger instance = new Logger();
        public static Logger GetInstance() { return instance; }

        public LogLevel Level { get; set; }
        public bool IsStackTraceShown { get; set; }

        private void WriteTime(LogLevel level)
        {
            _output.Write(DateTime.Now + "   ", level);
        }

        public void WriteLine(String log, LogLevel level)
        {
            if (Level > level || level == LogLevel.MuteLog) return;
            lock (instance)
            {
                WriteTime(level);
                _output.WriteLine(log, level);
            }
        }

        public void WriteLine(String log)
        {
            WriteLine(log, LogLevel.Notice);
        }

        public void Write(String log, LogLevel level)
        {
            if (Level > level || level == LogLevel.MuteLog) return;
            lock (instance)
            {
                WriteTime(level);
                _output.Write(log, level);
            }
        }

        public void Write(String log)
        {
            Write(log, LogLevel.Notice);
        }

        public void Write(Exception ex, String log)
        {
            if (Level == LogLevel.MuteLog) return;
            LogLevel level = LogLevel.Error;
            lock (instance)
            {
                WriteTime(level);
                if (!String.IsNullOrEmpty(log)) _output.WriteLine(log, level);
                _output.WriteLine("Error Message: " + ex.Message, level);
                _output.WriteLine("Thrown: " + ex.GetType().Name, level);
                if (IsStackTraceShown)
                {
                    _output.WriteLine("Source: " + ex.Source, level);
                    if (ex.InnerException != null) _output.WriteLine("Inner Source: " + ex.InnerException.Source, level);
                    _output.WriteLine("StackTrace:", level);
                    _output.WriteLine(ex.StackTrace, level);
                    if (ex.InnerException != null)
                    {
                        _output.WriteLine("Inner StackTrace:", level);
                        _output.WriteLine(ex.InnerException.StackTrace, level);
                    }
                    _output.WriteLine("TargetSite: " + ex.TargetSite, level);
                }
                _output.WriteLine("", level);
            }
        }

        public void Write(Exception ex)
        {
            Write(ex, null);
        }

        public void AddOutput(ILoggerOutput output)
        {
            if (output == null) throw new ArgumentNullException();

            lock (instance)
            {
                if (_output is LoggerOutputList)
                {
                    ((LoggerOutputList)_output).List.Add(output);
                }
                else
                {
                    LoggerOutputList outList = new LoggerOutputList();
                    if (_output != null) outList.List.Add(_output);
                    outList.List.Add(output);
                    _output = outList;
                }
            }
        }

        public void ReplaceOutput(ILoggerOutput output)
        {
            if (output == null) throw new ArgumentNullException();
            lock (instance)
            {
                _output = output;
            }
        }

        public ILoggerOutput Output { get { return _output; } }
    }
}
