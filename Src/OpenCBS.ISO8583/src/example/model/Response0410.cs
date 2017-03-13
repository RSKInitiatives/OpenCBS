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
using Free.iso8583.model;

namespace Free.iso8583.example.model
{
    public class Response0410 : BaseModel
    {
        public Response0410()
        {
        }
        public Response0410(Object aModel) : base(aModel)
        {
        }

        public ProcessingCode ProcessingCode { get; set; }
        public byte[] SystemAuditTraceNumber { get; set; }
        public DateTime? TransactionTime { get; set; }
        public DateTime? TransactionDate { get; set; }
        public String NetworkInternationalId { get; set; }
        public String RetrievalReferenceNumber { get; set; }
        public String ResponseCode { get; set; }
        public String TerminalId { get; set; }
    }

    public class RespReversal0410 : Response0410
    {
        public RespReversal0410()
        {
        }
        public RespReversal0410(Object aModel) : base(aModel)
        {
        }
    }
}
