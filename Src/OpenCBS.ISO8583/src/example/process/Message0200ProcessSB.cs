using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Free.iso8583.example.model;

namespace Free.iso8583.example.process
{
    public delegate void ProcessMessage0200SBDelegate(Request0200SB requestModel, Response0210SB responseModel);
    
    public class Message0200ProcessSB
    {
        public static ProcessErrorNotificationDelegate ProcessErrorNotification = null;
        public static ProcessMessage0200SBDelegate ProcessMessage0200SB = null;
        
        public Response0210SB Execute(Request0200SB request)
        {
            if (request == null)
            {
                if (ProcessErrorNotification != null) ProcessErrorNotification();
                return null;
            }

            Response0210SB response = new Response0210SB();
            if (request is Request0200TB) response = new Response0210TB();
            
            DateTime now = DateTime.Now;
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

            if (ProcessMessage0200SB != null) ProcessMessage0200SB(request, response);

            return response;
        }
    }
}
