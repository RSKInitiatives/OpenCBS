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
    public class MessageProcessorException : Exception
    {
        public MessageProcessorException(String msg) : base(msg)
        {
        }

        public MessageProcessorException(String msg, Exception innerExc) : base(msg, innerExc)
        {
        }

        public int ErrorCode { get; set; }
    }

    public class ConfigParserException : MessageProcessorException
    {
        public ConfigParserException(String msg) : base("Failed to load configuration. " + msg)
        {
        }

        public ConfigParserException(String msg, Exception innerExc) : base("Failed to load configuration. " + msg, innerExc)
        {
        }
    }

    public class MessageParserException : MessageProcessorException
    {
        public MessageParserException(String msg) : base("Failed to parse incoming message. " + msg)
        {
        }

        public MessageParserException(String msg, Exception innerExc)
            : base("Failed to parse incoming message. " + msg, innerExc)
        {
        }
    }

    public class MessageCompilerException : MessageProcessorException
    {
        public MessageCompilerException(String msg)
            : base("Failed to compile outgoing message. " + msg)
        {
        }

        public MessageCompilerException(String msg, Exception innerExc)
            : base("Failed to compile outgoing message. " + msg, innerExc)
        {
        }
    }

    public class MessageListenerException : MessageProcessorException
    {
        public MessageListenerException(String msg) : base(msg)
        {
        }

        public MessageListenerException(String msg, Exception innerExc) : base(msg, innerExc)
        {
        }
    }
}
