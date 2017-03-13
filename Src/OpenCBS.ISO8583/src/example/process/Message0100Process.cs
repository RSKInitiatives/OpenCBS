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
    public delegate void ProcessBalanceInquiryDelegate(ReqBalanceInquiry0100 requestModel, RespBalanceInquiry0110 responseModel);
    public delegate void ProcessPinChangeDelegate(ReqPinChange0100 requestModel, RespPinChange0110 responseModel);
    public delegate void ProcessPaymentInquiryDelegate(ReqPaymentInquiry0100 requestModel, RespPaymentInquiry0110 responseModel);
    public delegate void ProcessTransferInquiryDelegate(ReqTransferInquiry0100 requestModel, RespTransferInquiry0110 responseModel);

    public class Message0100Process
    {
        private static Message0100Process instance = new Message0100Process();

        public static ProcessBalanceInquiryDelegate ProcessBalanceInquiry = null;
        public static ProcessPinChangeDelegate ProcessPinChange = null;
        public static ProcessPaymentInquiryDelegate ProcessPaymentInquiry = null;
        public static ProcessTransferInquiryDelegate ProcessTransferInquiry = null;
        public static ProcessErrorNotificationDelegate ProcessErrorNotification = null;
        
        public static Message0100Process GetInstance()
        {
            return instance;
        }

        public Response0110 Execute(Request0100 request)
        {
            if (request == null)
            {
                if (ProcessErrorNotification != null) ProcessErrorNotification();
                return null;
            }

            DateTime now = DateTime.Now;

            Response0110 response = new Response0110();
            switch (request.ProcessingCode.TransactionType)
            {
                case TransactionTypeCodeBytes.BALANCE_INQUIRY : response = new RespBalanceInquiry0110(); break;
                case TransactionTypeCodeBytes.PIN_CHANGE      : response = new RespPinChange0110(); break;
                case TransactionTypeCodeBytes.PAYMENT_INQUIRY : response = new RespPaymentInquiry0110(); break;
                case TransactionTypeCodeBytes.TRANSFER_INQUIRY: response = new RespTransferInquiry0110(); break;
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

            switch (request.ProcessingCode.TransactionType)
            {
                case TransactionTypeCodeBytes.BALANCE_INQUIRY :
                    if (ProcessBalanceInquiry != null)
                        ProcessBalanceInquiry(new ReqBalanceInquiry0100(request), (RespBalanceInquiry0110)response);
                    break;
                case TransactionTypeCodeBytes.PIN_CHANGE:
                    if (ProcessPinChange != null)
                        ProcessPinChange(new ReqPinChange0100(request), (RespPinChange0110)response);
                    break;
                case TransactionTypeCodeBytes.PAYMENT_INQUIRY:
                    if (ProcessPaymentInquiry != null)
                        ProcessPaymentInquiry(new ReqPaymentInquiry0100(request), (RespPaymentInquiry0110)response);
                    break;
                case TransactionTypeCodeBytes.TRANSFER_INQUIRY:
                    if (ProcessTransferInquiry != null)
                        ProcessTransferInquiry(new ReqTransferInquiry0100(request), (RespTransferInquiry0110)response);
                    break;
            }

            //ProcessBalanceInquiry = null;
            //ProcessPinChange = null;
            //ProcessPaymentInquiry = null;
            //ProcessTransferInquiry = null;

            return response;
        }
    }
}
