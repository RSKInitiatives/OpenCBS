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
    [Header(Seq = 1, Name = "ISO TPDU", Value = new byte[] { 0x60, 0x00, 0x05, 0x80, 0x53 })]
    [MessageType(Seq = 2, Value = new byte[] { 0x02, 0x00 })]
    [BitMap(Seq = 3, Length = 8)]
    public class Request0200 : BaseModel
    {
        public Request0200()
        {
        }
        public Request0200(Object aModel) : base(aModel)
        {
        }

        [PropertyField(Seq = 2, FieldType = FieldType.N, LengthHeader = 1, PropertyType = PropertyType.String)]
        public String PrimaryAccountNumber { get; set; }

        [PropertyField(Seq = 3, FieldType = FieldType.N, Length = 6, PropertyType = PropertyType.Bytes)]
        public ProcessingCode ProcessingCode { get; set; }

        [PropertyField(Seq = 4, FieldType = FieldType.N, Length = 12, PropertyType = PropertyType.Decimal, FracDigits = 2)]
        public decimal? TransactionAmount { get; set; }

        [PropertyField(Seq = 11, FieldType = FieldType.N, Length = 6, PropertyType = PropertyType.Bytes)]
        public byte[] SystemAuditTraceNumber { get; set; }

        [PropertyField(Seq = 14, FieldType = FieldType.N, Length = 4,
            FieldDelegateClass = typeof(DateTimeNibble), FieldDelegateMethod = "GetBytesFromYYMM",
            PropertyDelegateClass = typeof(DateTimeNibble), PropertyDelegateMethod = "GetYYMM")]
        public DateTime? ExpirationDate { get; set; }

        [PropertyField(Seq = 22, FieldType = FieldType.N, Length = 3, PropertyType = PropertyType.Bytes)]
        public POSEntryMode PosEntryMode { get; set; }

        [PropertyField(Seq = 24, FieldType = FieldType.N, Length = 3, PropertyType = PropertyType.String)]
        public String NetworkInternationalId { get; set; }

        [PropertyField(Seq = 25, FieldType = FieldType.N, Length = 2, PropertyType = PropertyType.Bytes)]
        public POSCondition PosConditionCode { get; set; }

        [PropertyField(Seq = 35, FieldType = FieldType.NS, LengthHeader = 1, PropertyType = PropertyType.Bytes)]
        public byte[] Track2Data { get; set; }

        [PropertyField(Seq = 41, FieldType = FieldType.ANS, Length = 8, PropertyType = PropertyType.String)]
        public String TerminalId { get; set; }

        [PropertyField(Seq = 42, FieldType = FieldType.ANS, Length = 15, PropertyType = PropertyType.String)]
        public String MerchantId { get; set; }

        [PropertyField(Seq = 48, FieldType = FieldType.ANS, LengthHeader = 2, Tlv = typeof(RequestTlvDataEntry48))]
        public Object AdditionalData { get; set; }

        [PropertyField(Seq = 52, FieldType = FieldType.B, Length = 64, PropertyType = PropertyType.Bytes)]
        public byte[] CardholderPinBlock { get; set; }

        [PropertyField(Seq = 62, FieldType = FieldType.ANS, LengthHeader = 2, PropertyType = PropertyType.String)]
        public String InvoiceNumber { get; set; }

        [PropertyField(Seq = 63, FieldType = FieldType.ANS, LengthHeader = 2, PropertyType = PropertyType.BitContent)]
        public Bit63Content TransferData { get; set; }

        [PropertyField(Seq = 64, FieldType = FieldType.B, Length = 64, PropertyType = PropertyType.Bytes)]
        public byte[] MessageAuthenticationCode { get; set; }
    }

    public class ReqCashWithdrawal0200 : Request0200
    {
        public ReqCashWithdrawal0200()
        {
        }
        public ReqCashWithdrawal0200(Object aModel) : base(aModel)
        {
        }
    }
    public class ReqPayment0200 : Request0200
    {
        public ReqPayment0200()
        {
        }
        public ReqPayment0200(Object aModel) : base(aModel)
        {
        }
    }
    public class ReqTransfer0200 : Request0200
    {
        public ReqTransfer0200()
        {
        }
        public ReqTransfer0200(Object aModel) : base(aModel)
        {
        }
    }
}
