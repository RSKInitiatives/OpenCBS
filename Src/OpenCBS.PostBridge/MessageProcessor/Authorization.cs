using Free.iso8583.example;
using Free.iso8583.example.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCBS.PostBridge.MessageProcessor
{
    public class Authorization
    {
        public static void BalanceInquiry(ReqBalanceInquiry0100 requestModel, RespBalanceInquiry0110 responseModel)
        {
            try
            {
                //It should be queried from database
                responseModel.TransactionAmount = 82000000m;
                responseModel.AdditionalData = "ID010Balance OK";
                if (responseModel.TransactionAmount > 0)
                    responseModel.AdditionalAmount = responseModel.TransactionAmount;
                else
                    responseModel.AdditionalAmount = 0;
            }
            catch
            {
                responseModel.ResponseCode = ResponseCode.SYSTEM_ERROR; //Or another error code
            }
        }

    }
}
