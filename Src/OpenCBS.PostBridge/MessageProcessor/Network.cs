using Free.iso8583;
using Free.iso8583.example;
using Free.iso8583.example.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCBS.PostBridge.MessageProcessor
{
    public class Network
    {
        public static void Logon(ReqLogon0800 requestModel, RespLogon0810 responseModel)
        {
            try
            {
                bool isLoginOk = true; //Not so simple, must do some checking process here
                if (isLoginOk)
                    responseModel.ResponseCode = ResponseCode.COMPLETED_SUCCESSFULLY;
                else
                    responseModel.ResponseCode = ResponseCode.TERMINAL_NOT_PERMITTED;

                //Must be replaced by true query
                byte[] pin = new byte[] { 0x2e, 0xde, 0xa3, 0xb4, 0x09, 0x07, 0x64, 0x54 };
                byte[] macAddress = new byte[] { 0xfe, 0xfe, 0xfe, 0xfe, 0xfe, 0xfe, 0xfe, 0xfe };
                byte[] merchant = MessageUtility.StringToAsciiArray("MerchantNameInSubang");
                responseModel.PrivateUse = pin.Concat(macAddress).Concat(merchant).ToArray();
            }
            catch
            {
                responseModel.ResponseCode = ResponseCode.SYSTEM_ERROR;
            }
        }

    }
}
