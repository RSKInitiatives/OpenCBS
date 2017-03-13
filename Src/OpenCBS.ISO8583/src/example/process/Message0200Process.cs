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
    public delegate void ProcessCashWithdrawalDelegate(ReqCashWithdrawal0200 requestModel, RespCashWithdrawal0210 responseModel);
    public delegate void ProcessPaymentDelegate(ReqPayment0200 requestModel, RespPayment0210 responseModel);
    public delegate void ProcessTransferDelegate(ReqTransfer0200 requestModel, RespTransfer0210 responseModel);

    public class Message0200Process
    {
        private static Message0200Process instance = new Message0200Process();
        
        public static ProcessCashWithdrawalDelegate ProcessCashWithdrawal = null;
        public static ProcessPaymentDelegate ProcessPayment = null;
        public static ProcessTransferDelegate ProcessTransfer = null;
        public static ProcessErrorNotificationDelegate ProcessErrorNotification = null;
        
        public static Message0200Process GetInstance()
        {
            return instance;
        }

        public Response0210 Execute(Request0200 request)
        {
            if (request == null)
            {
                ProcessErrorNotification?.Invoke();
                return null;
            }
            
            DateTime now = DateTime.Now;

            Response0210 response = new Response0210();
            if (request.ProcessingCode.TransactionType == TransactionTypeCodeBytes.CASH_WITHDRAWAL)
            {
                response = new RespCashWithdrawal0210();
            }
            else if (request.ProcessingCode.TransactionType == TransactionTypeCodeBytes.PAYMENT)
            {
                response = new RespPayment0210();
            }
            else if (request.ProcessingCode.TransactionType == TransactionTypeCodeBytes.TRANSFER)
            {
                response = new RespTransfer0210();
            }
            response.TransactionDate = now;
            response.TransactionTime = now;
            response.RetrievalReferenceNumber = MessageUtility.HexToString(request.SystemAuditTraceNumber);
            response.ResponseCode = ResponseCode.COMPLETED_SUCCESSFULLY;
            response.MessageAuthenticationCode = new byte[8];

            if (request.TransferData != null)
            {
                response.TransferData = request.TransferData;
                //It's just default value, it must be queried from db in the delegates defined above
                response.TransferData.BeneficiaryName = "BENEFICIARY NAME GENERATED";
                response.TransferData.IssuerInstitutionId = "4442";
                response.TransferData.CardholderAccountNumber = "1234567890123456";
                response.TransferData.CardholderName = "CARD HOLDER NAME GENERATED";
            }

            if (request.ProcessingCode.TransactionType == TransactionTypeCodeBytes.CASH_WITHDRAWAL)
            {
                ProcessCashWithdrawal?.Invoke(new ReqCashWithdrawal0200(request), (RespCashWithdrawal0210)response);
            }
            else if (request.ProcessingCode.TransactionType == TransactionTypeCodeBytes.PAYMENT)
            {
                ProcessPayment?.Invoke(new ReqPayment0200(request), (RespPayment0210)response);
            }
            else if (request.ProcessingCode.TransactionType == TransactionTypeCodeBytes.TRANSFER)
            {
                ProcessTransfer?.Invoke(new ReqTransfer0200(request), (RespTransfer0210)response);
            }

            //ProcessCashWithdrawal = null;
            //ProcessPayment = null;
            //ProcessTransfer = null;

            return response;
        }
    }
}
