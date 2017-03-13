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
using Free.iso8583.config.attribute;

namespace Free.iso8583.example.model
{
    [Message(LengthHeader = 2)]
    [Header(Seq = 1, Name = "ISO TPDU", Value = new byte[] { 0x60, 0x80, 0x53, 0x00, 0x05 })]
    [MessageType(Seq = 2, Value = new byte[] { 0x01, 0x10 })]
    [BitMap(Seq = 3, Length = 8)]
    public class Response0110 : BaseModel
    {
        public Response0110()
        {
        }
        public Response0110(Object aModel) : base(aModel)
        {
        }

        [PropertyField(Seq = 2, FieldType = FieldType.N, LengthHeader = 1, PropertyType = PropertyType.String, FromRequest = true)]
        public String PrimaryAccountNumber { get; set; }

        [PropertyField(Seq = 3, FieldType = FieldType.N, Length = 6, PropertyType = PropertyType.Bytes, FromRequest = true)]
        public ProcessingCode ProcessingCode { get; set; }

        [PropertyField(Seq = 4, FieldType = FieldType.N, Length = 12, PropertyType = PropertyType.Decimal, FracDigits = 2,
            FromRequest = true)]
        public decimal? TransactionAmount { get; set; }

        [PropertyField(Seq = 11, FieldType = FieldType.N, Length = 6, PropertyType = PropertyType.Bytes, FromRequest = true)]
        public byte[] SystemAuditTraceNumber { get; set; }

        [PropertyField(Seq = 12, FieldType = FieldType.N, Length = 6,
            FieldDelegateClass = typeof(DateTimeNibble), FieldDelegateMethod = "GetBytesFromHHMMSS",
            PropertyDelegateClass = typeof(DateTimeNibble), PropertyDelegateMethod = "GetHHMMSS")]
        public DateTime? TransactionTime { get; set; }

        [PropertyField(Seq = 13, FieldType = FieldType.N, Length = 4,
            FieldDelegateClass = typeof(DateTimeNibble), FieldDelegateMethod = "GetBytesFromMMDD",
            PropertyDelegateClass = typeof(DateTimeNibble), PropertyDelegateMethod = "GetMMDD")]
        public DateTime? TransactionDate { get; set; }

        [PropertyField(Seq = 24, FieldType = FieldType.N, Length = 3, PropertyType = PropertyType.String, FromRequest = true)]
        public String NetworkInternationalId { get; set; }

        [PropertyField(Seq = 37, FieldType = FieldType.AN, Length = 12, PropertyType = PropertyType.String)]
        public String RetrievalReferenceNumber { get; set; }

        [PropertyField(Seq = 38, FieldType = FieldType.AN, Length = 6, PropertyType = PropertyType.String)]
        public String AuthorizationIdResponse { get; set; }

        [PropertyField(Seq = 39, FieldType = FieldType.AN, Length = 2, PropertyType = PropertyType.String)]
        public String ResponseCode { get; set; }

        [PropertyField(Seq = 41, FieldType = FieldType.ANS, Length = 8, PropertyType = PropertyType.String, FromRequest = true)]
        public String TerminalId { get; set; }

        [PropertyField(Seq = 42, FieldType = FieldType.ANS, Length = 15, PropertyType = PropertyType.String, FromRequest = true)]
        public String MerchantId { get; set; }

        [PropertyField(Seq = 48, FieldType = FieldType.ANS, LengthHeader = 2, PropertyType = PropertyType.String)]
        public Object AdditionalData { get; set; }

        [PropertyField(Seq = 54, FieldType = FieldType.AN, LengthHeader = 2, PropertyType = PropertyType.Decimal, FracDigits = 2)]
        public decimal? AdditionalAmount { get; set; }

        [PropertyField(Seq = 62, FieldType = FieldType.ANS, LengthHeader = 2, PropertyType = PropertyType.String,
            FromRequest = true)]
        public String InvoiceNumber { get; set; }

        [PropertyField(Seq = 63, FieldType = FieldType.ANS, LengthHeader = 2, PropertyType = PropertyType.BitContent)]
        public Bit63Content TransferData { get; set; }

        [PropertyField(Seq = 64, FieldType = FieldType.B, Length = 64, PropertyType = PropertyType.Bytes)]
        public byte[] MessageAuthenticationCode { get; set; }
    }

    public class RespBalanceInquiry0110 : Response0110
    {
        public RespBalanceInquiry0110()
        {
        }
        public RespBalanceInquiry0110(Object aModel) : base(aModel)
        {
        }
    }

    public class RespPinChange0110 : Response0110
    {
        public RespPinChange0110()
        {
        }
        public RespPinChange0110(Object aModel) : base(aModel)
        {
        }
    }

    public class RespPaymentInquiry0110 : Response0110
    {
        public RespPaymentInquiry0110()
        {
        }
        public RespPaymentInquiry0110(Object aModel) : base(aModel)
        {
        }
    }

    public class RespTransferInquiry0110 : Response0110
    {
        public RespTransferInquiry0110()
        {
        }
        public RespTransferInquiry0110(Object aModel) : base(aModel)
        {
        }
    }
}
