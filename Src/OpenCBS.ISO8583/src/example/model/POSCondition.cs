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

namespace Free.iso8583.example.model
{
    public class POSCondition
    {
        public static readonly IDictionary<PosConditionCodeBytes, String> DESCRIPTION = new Dictionary<PosConditionCodeBytes, String>() {
            {PosConditionCodeBytes.NORMAL_PRESENTMENT, "Normal Presentment"},
            {PosConditionCodeBytes.CUSTOMER_NOT_PRESENT, "Customer Not Present"},
            {PosConditionCodeBytes.MAIL_OR_PHONE_ORDER, "Mail or Phone Order"},
            {PosConditionCodeBytes.UNATTENDED_TERMINAL, "Unattended Terminal, Unable To Retain Card"},
        };
        public static readonly IDictionary<PosConditionCodeBytes, String> CODE_STRING = new Dictionary<PosConditionCodeBytes, String>() {
            {PosConditionCodeBytes.NORMAL_PRESENTMENT, PosConditionCode.NORMAL_PRESENTMENT},
            {PosConditionCodeBytes.CUSTOMER_NOT_PRESENT, PosConditionCode.CUSTOMER_NOT_PRESENT},
            {PosConditionCodeBytes.MAIL_OR_PHONE_ORDER, PosConditionCode.MAIL_OR_PHONE_ORDER},
            {PosConditionCodeBytes.UNATTENDED_TERMINAL, PosConditionCode.UNATTENDED_TERMINAL},
        };

        private PosConditionCodeBytes _code;
        private String _codeString;
        private String _description;

        public POSCondition()
        {
            //Using default value
            Code = _code;
        }

        public POSCondition(PosConditionCodeBytes code)
        {
            this.Code = code;
        }

        public POSCondition(byte code)
        {
            this.Code = (PosConditionCodeBytes)code;
        }

        public POSCondition(byte[] code)
        {
            this.Code = (PosConditionCodeBytes)code[0];
        }

        public PosConditionCodeBytes Code
        {
            get { return _code; }
            set
            {
                _code = value;
                _codeString = MessageUtility.HexToString((byte)_code);
                Description = null; //null is dummy value. The real value depends on _code
            }
        }
        public String CodeString
        {
            get { return _codeString; }
            set
            {
                byte[] vals = MessageUtility.StringToHex(value);
                if (vals == null || vals.Length != 1)
                {
                    throw new MessageProcessorException("POSCondition.CodeString: Invalid value.");
                }
                _code = (PosConditionCodeBytes)vals[0];
                _codeString = value.PadLeft(2, '0');
                Description = null; //null is dummy value. The real value depends on _code
            }
        }
        public String Description
        {
            get { return _description; }
            private set
            {
                _description = DESCRIPTION.ContainsKey(_code) ? DESCRIPTION[_code] : null;
            }
        }

        public static implicit operator byte(POSCondition pc)
        {
            return (byte)pc.Code;
        }

        public static implicit operator POSCondition(byte code)
        {
            return new POSCondition(code);
        }

        public static implicit operator byte[](POSCondition pc)
        {
            return new byte[] { (byte)pc.Code };
        }

        public static implicit operator POSCondition(byte[] bytes)
        {
            if (bytes == null || bytes.Length < 1) throw new MessageProcessorException("POSCondition: Bytes array length must be 1.");
            return new POSCondition(bytes[0]);
        }

        public static implicit operator string(POSCondition pc)
        {
            return pc.Description;
        }

        public override string ToString()
        {
            return _description;
        }
    }
}
