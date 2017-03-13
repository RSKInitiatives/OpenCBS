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
    public delegate void ProcessErrorNotificationDelegate();
    public delegate void ProcessLogonDelegate(ReqLogon0800 requestModel, RespLogon0810 responseModel);

    public class Message0800Process
    {
        private static Message0800Process instance = new Message0800Process();

        public static ProcessLogonDelegate ProcessLogon = null;
        public static ProcessErrorNotificationDelegate ProcessErrorNotification = null;

        public static Message0800Process GetInstance()
        {
            return instance;
        }

        public Response0810 Execute(Request0800 request)
        {
            if (request == null)
            {
                if (ProcessErrorNotification != null) ProcessErrorNotification();
                return null;
            }
            
            Response0810 response = new Response0810();
            if (request.ProcessingCode.TransactionType == TransactionTypeCodeBytes.LOGON)
            {
                if (ProcessLogon != null)
                {
                    response = new RespLogon0810();
                    ProcessLogon(new ReqLogon0800(request), (RespLogon0810)response);
                }
            }
            //ProcessLogon = null;
            return response;
        }
    }
}
