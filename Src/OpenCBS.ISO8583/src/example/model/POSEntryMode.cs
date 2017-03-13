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
using Free.iso8583;

namespace Free.iso8583.example.model
{
    public class POSEntryMode
    {
        public static readonly IDictionary<PosEntryMediaModeBytes, String> MEDIA = new Dictionary<PosEntryMediaModeBytes, String>() {
            {PosEntryMediaModeBytes.UNSPECIFIED, "Unspecified"},
            {PosEntryMediaModeBytes.MANUAL, "Manual"},
            {PosEntryMediaModeBytes.MAGNETIC_STRIPE, "Magnetic Stripe"},
        };
        public static readonly IDictionary<PosEntryMediaModeBytes, String> MEDIA_CODE_STRING = new Dictionary<PosEntryMediaModeBytes, String>() {
            {PosEntryMediaModeBytes.UNSPECIFIED, PosEntryMediaMode.UNSPECIFIED},
            {PosEntryMediaModeBytes.MANUAL, PosEntryMediaMode.MANUAL},
            {PosEntryMediaModeBytes.MAGNETIC_STRIPE, PosEntryMediaMode.MAGNETIC_STRIPE},
        };
        public static readonly IDictionary<PosEntryPinModeBytes, String> PIN_ENTRY = new Dictionary<PosEntryPinModeBytes, String>() {
            {PosEntryPinModeBytes.UNSPECIFIED, "Unspecified"},
            {PosEntryPinModeBytes.PIN_ENTRY_CAPABILITY, "PIN Entry Capability"},
            {PosEntryPinModeBytes.NO_PIN_ENTRY_CAPABILITY, "No PIN Entry Capability"},
        };
        public static readonly IDictionary<PosEntryPinModeBytes, String> PIN_ENTRY_CODE_STRING = new Dictionary<PosEntryPinModeBytes, String>() {
            {PosEntryPinModeBytes.UNSPECIFIED, PosEntryPinMode.UNSPECIFIED},
            {PosEntryPinModeBytes.PIN_ENTRY_CAPABILITY, PosEntryPinMode.PIN_ENTRY_CAPABILITY},
            {PosEntryPinModeBytes.NO_PIN_ENTRY_CAPABILITY, PosEntryPinMode.NO_PIN_ENTRY_CAPABILITY},
        };

        private PosEntryMediaModeBytes _mediaCode;
        private PosEntryPinModeBytes _pinEntryCode;
        private String _mediaCodeString;
        private String _pinEntryCodeString;
        private String _mediaDescription;
        private String _pinEntryDescription;
        private byte[] _bytes = new byte[2];

        public POSEntryMode()
        {
            //Using default values
            MediaCode = _mediaCode;
            PinEntryCode = _pinEntryCode;
        }

        public POSEntryMode(byte[] bytes)
        {
            this.BytesValue = bytes;
        }

        private void SetMediaCode(Object value)
        {
            byte val = 0;
            if (value is byte[])
            {
                _bytes = (byte[])value;
                val = (byte)((_bytes[0] << 4) | (_bytes[1] >> 4));
            }
            else
            {
                if (value is PosEntryMediaModeBytes) val = (byte)value;
                else if (value is String)
                {
                    byte[] vals = MessageUtility.StringToHex(value.ToString());
                    if (vals == null || vals.Length != 1)
                    {
                        throw new MessageProcessorException("POSEntryMode.MediaCodeString: Invalid value.");
                    }
                    val = vals[0];
                }
                else return;
                _bytes[0] = (byte)(val >> 4);
                _bytes[1] = (byte)((_bytes[1] & 0x0f) | (val << 4));
            }
            _mediaCode = (PosEntryMediaModeBytes)val;
            _mediaCodeString = MessageUtility.HexToString(val);
            _mediaDescription = MEDIA.ContainsKey(_mediaCode) ? MEDIA[_mediaCode] : null;
        }
        public PosEntryMediaModeBytes MediaCode
        { 
            get { return _mediaCode; }
            set { SetMediaCode(value); }
        }
        public String MediaCodeString
        { 
            get { return _mediaCodeString; }
            set { SetMediaCode(value); }
        }
        public String MediaDescription
        {
            get { return _mediaDescription; }
        }

        private void SetPinEntryCode(Object value)
        {
            byte val = 0;
            if (value is byte[])
            {
                _bytes = (byte[])value;
                val = (byte)(_bytes[1] & 0x0f);
            }
            else
            {
                if (value is PosEntryPinModeBytes) val = (byte)value;
                else if (value is String)
                {
                    byte[] vals = MessageUtility.StringToHex(value.ToString());
                    if (vals == null || vals.Length != 1 || ((vals[0] & 0xf0) != 0))
                    {
                        throw new MessageProcessorException("POSEntryMode.PinEntryCodeString: Invalid value.");
                    }
                    val = vals[0];
                }
                else return;
                _bytes[1] = (byte)((_bytes[1] & 0xf0) | val);
            }
            _pinEntryCode = (PosEntryPinModeBytes)val;
            _pinEntryCodeString = MessageUtility.HexToString(val).Substring(1);
            _pinEntryDescription = PIN_ENTRY.ContainsKey(_pinEntryCode) ? PIN_ENTRY[_pinEntryCode] : null;
        }
        public PosEntryPinModeBytes PinEntryCode
        {
            get { return _pinEntryCode; }
            set { SetPinEntryCode(value); }
        }
        public String PinEntryCodeString
        {
            get { return _pinEntryCodeString; }
            set { SetPinEntryCode(value); }
        }
        public String PinEntryDescription
        {
            get { return _pinEntryDescription; }
        }

        public byte[] BytesValue
        {
            get
            {
                return _bytes;
            }
            set
            {
                if (value == null || value.Length < 2)
                    throw new MessageProcessorException("POSEntryMode.BytesValue: Bytes array length must be 2.");
                Array.Copy(value, 0, _bytes, 0, 2);
                SetMediaCode(_bytes);
                SetPinEntryCode(_bytes);
            }
        }

        public override string ToString()
        {
            return _mediaCodeString + _pinEntryCodeString;
        }

        public static implicit operator byte[](POSEntryMode pem)
        {
            return pem.BytesValue;
        }

        public static implicit operator POSEntryMode(byte[] bytes)
        {
            return new POSEntryMode(bytes);
        }
    }
}
