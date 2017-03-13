using Free.iso8583.config.attribute;
using Free.iso8583.example.model;
using Free.iso8583.example.process;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Free.iso8583.example
{
    public class MessageToModelMapping
    {
        [MessageToModel(Model = typeof(Request0100), ProcessClass = typeof(Message0100Process), ProcessMethod = "Execute")]
        public static IMask Request0100()
        {
            return new Mask
            {
                StartByte = 8,
                Value = new byte[] { 0x01, 0x00 }
            };
        }
        [MessageToModel(Model = typeof(ReqBalanceInquiry0100), ProcessClass = typeof(Message0100Process), ProcessMethod = "Execute")]
        public static IMask RequestBalanceInquiry0100()
        {
            return Request0100();
        }
        [MessageToModel(Model = typeof(ReqPinChange0100), ProcessClass = typeof(Message0100Process), ProcessMethod = "Execute")]
        public static IMask RequestPinChange0100()
        {
            return Request0100();
        }
        [MessageToModel(Model = typeof(ReqPaymentInquiry0100), ProcessClass = typeof(Message0100Process), ProcessMethod = "Execute")]
        public static IMask RequestPaymentInquiry0100()
        {
            return Request0100();
        }
        [MessageToModel(Model = typeof(ReqTransferInquiry0100), ProcessClass = typeof(Message0100Process), ProcessMethod = "Execute")]
        public static IMask RequestTransferInquiry0100()
        {
            return Request0100();
        }

        [MessageToModel(Model = typeof(RespTransferInquiry0110))]
        public static IMask Response0110()
        {
            return new Mask
            {
                StartByte = 8,
                Value = new byte[] { 0x01, 0x10 }
            };
        }
        [MessageToModel(Model = typeof(RespPinChange0110))]
        public static IMask ResponsePinChange0110()
        {
            return Response0110();
        }
        [MessageToModel(Model = typeof(RespPaymentInquiry0110))]
        public static IMask ResponsePaymentInquiry0110()
        {
            return Response0110();
        }
        [MessageToModel(Model = typeof(RespBalanceInquiry0110))]
        public static IMask ResponseBalanceInquiry0110()
        {
            return Response0110();
        }

        [MessageToModel(Model = typeof(Request0200TB), ProcessClass = typeof(Message0200ProcessSB), ProcessMethod = "Execute")]
        public static IMask Request0200TB()
        {
            return new MaskAnd
            {
                Children = new List<IMask>()
                {
                    new Mask
                    {
                        StartByte = 8,
                        Value = new byte[] { 0x02, 0x00 }
                    },
                    new Mask
                    {
                        StartByte = 10,
                        MaskBytes = new byte[] { 0x80 },
                        Result = MaskResult.NotZero
                    },
                    new Mask
                    {
                        StartByte = 18,
                        MaskBytes = new byte[] { 0x80 },
                        Result = MaskResult.NotZero
                    },
                }
            };
        }

        [MessageToModel(Model = typeof(Response0210TB))]
        public static IMask Response0210TB()
        {
            return new MaskAnd
            {
                Children = new List<IMask>()
                {
                    new Mask
                    {
                        StartByte = 8,
                        Value = new byte[] { 0x02, 0x10 }
                    },
                    new Mask
                    {
                        StartByte = 10,
                        MaskBytes = new byte[] { 0x80 },
                        Result = MaskResult.NotZero
                    },
                    new Mask
                    {
                        StartByte = 18,
                        MaskBytes = new byte[] { 0x80 },
                        Result = MaskResult.NotZero
                    },
                }
            };
        }

        [MessageToModel(Model = typeof(Request0200SB), ProcessClass = typeof(Message0200ProcessSB), ProcessMethod = "Execute")]
        public static IMask Request0200SB()
        {
            return new MaskAnd
            {
                Children = new List<IMask>()
                {
                    new Mask
                    {
                        StartByte = 8,
                        Value = new byte[] { 0x02, 0x00 }
                    },
                    new Mask
                    {
                        StartByte = 10,
                        MaskBytes = new byte[] { 0x80 },
                        Result = MaskResult.NotZero
                    },
                }
            };
        }

        [MessageToModel(Model = typeof(Response0210SB))]
        public static IMask Response0210SB()
        {
            return new MaskAnd
            {
                Children = new List<IMask>()
                {
                    new Mask
                    {
                        StartByte = 8,
                        Value = new byte[] { 0x02, 0x10 }
                    },
                    new Mask
                    {
                        StartByte = 10,
                        MaskBytes = new byte[] { 0x80 },
                        Result = MaskResult.NotZero
                    },
                }
            };
        }

        [MessageToModel(Model = typeof(Request0200), ProcessClass = typeof(Message0200Process), ProcessMethod = "Execute")]
        public static IMask Request0200()
        {
            return new MaskAnd
            {
                Children = new List<IMask>()
                {
                    new Mask
                    {
                        StartByte = 8,
                        Value = new byte[] { 0x02, 0x00 }
                    },
                    new Mask
                    {
                        StartByte = 10,
                        MaskBytes = new byte[] { 0x80 },
                        Result = MaskResult.Zero
                    },
                }
            };
        }
        [MessageToModel(Model = typeof(ReqCashWithdrawal0200), ProcessClass = typeof(Message0200Process), ProcessMethod = "Execute")]
        public static IMask RequestCashWithdrawal0200()
        {
            return Request0200();
        }
        [MessageToModel(Model = typeof(ReqPayment0200), ProcessClass = typeof(Message0200Process), ProcessMethod = "Execute")]
        public static IMask RequestPayment0200()
        {
            return Request0200();
        }
        [MessageToModel(Model = typeof(ReqTransfer0200), ProcessClass = typeof(Message0200Process), ProcessMethod = "Execute")]
        public static IMask RequestTransfer0200()
        {
            return Request0200();
        }

        [MessageToModel(Model = typeof(RespTransfer0210))]
        public static IMask Response0210()
        {
            return new MaskAnd
            {
                Children = new List<IMask>()
                {
                    new Mask
                    {
                        StartByte = 8,
                        Value = new byte[] { 0x02, 0x10 }
                    },
                    new Mask
                    {
                        StartByte = 10,
                        MaskBytes = new byte[] { 0x80 },
                        Result = MaskResult.Zero
                    },
                }
            };
        }
        [MessageToModel(Model = typeof(RespCashWithdrawal0210))]
        public static IMask ResponseCashWithdrawal0210()
        {
            return Response0210();
        }
        [MessageToModel(Model = typeof(RespPayment0210))]
        public static IMask ResponsePayment0210()
        {
            return Response0210();
        }

        [MessageToModel(Model = typeof(Request0800), ProcessClass = typeof(Message0800Process), ProcessMethod = "Execute")]
        public static IMask Request0800()
        {
            return new Mask
            {
                StartByte = 8,
                Value = new byte[] { 0x08, 0x00 }
            };
        }
        [MessageToModel(Model = typeof(ReqLogon0800), ProcessClass = typeof(Message0800Process), ProcessMethod = "Execute")]
        public static IMask RequestLogon0800()
        {
            return Request0800();
        }

        [MessageToModel(Model = typeof(RespLogon0810))]
        public static IMask Response0810()
        {
            return new Mask
            {
                StartByte = 8,
                Value = new byte[] { 0x08, 0x10 }
            };
        }
    }
}
