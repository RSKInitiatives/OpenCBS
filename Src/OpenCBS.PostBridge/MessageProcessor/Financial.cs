using Free.iso8583.example.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCBS.PostBridge.MessageProcessor
{
    public class Financial
    {
        public static void Transfer(ReqTransfer0200 requestModel, RespTransfer0210 responseModel)
        {
            //It should be queried from database
            ResponseDataEntry48 data = new ResponseDataEntry48();
            decimal fee = 0;
            if (requestModel.AdditionalData is RequestDataEntry48)
                fee = ((RequestDataEntry48)requestModel.AdditionalData).Fee;
            if (requestModel.AdditionalData is RequestTlvDataEntry48)
                fee = ((RequestTlvDataEntry48)requestModel.AdditionalData).TD.Fee;
            data.ID = "Transfer Succesful";
            data.IP = new String[] {
                "Transfer Amount: " + requestModel.TransactionAmount,
                "Fee            : " + fee,
                "Total          : " + (requestModel.TransactionAmount + fee)
            };
            responseModel.AdditionalData = data;
        }

    }
}
