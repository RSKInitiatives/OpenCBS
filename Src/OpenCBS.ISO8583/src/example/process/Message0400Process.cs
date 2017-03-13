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
using Free.iso8583.example.model;

namespace Free.iso8583.example.process
{
    public delegate void ProcessReversalDelegate(ReqReversal0400 requestModel, RespReversal0410 responseModel);
    
    public class Message0400Process
    {
        private static Message0400Process instance = new Message0400Process();

        public static ProcessReversalDelegate ProcessReversal = null;
        public static ProcessErrorNotificationDelegate ProcessErrorNotification = null;
        
        public static Message0400Process GetInstance()
        {
            return instance;
        }

        public Response0410 Execute(Request0400 request)
        {
            if (request == null)
            {
                if (ProcessErrorNotification != null) ProcessErrorNotification();
                return null;
            }

            DateTime now = DateTime.Now;

            Response0410 response = new RespReversal0410();
            
            response.TransactionDate = now;
            response.TransactionTime = now;
            response.RetrievalReferenceNumber = MessageUtility.HexToString(request.SystemAuditTraceNumber);
            response.ResponseCode = ResponseCode.COMPLETED_SUCCESSFULLY;
            
            if (ProcessReversal != null)
                ProcessReversal(new ReqReversal0400(request), (RespReversal0410)response);
            
            //ProcessReversal = null;

            return response;
        }
    }
}
