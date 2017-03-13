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
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Free.iso8583.config;
using Free.iso8583.model;

namespace Free.iso8583
{
    public abstract class MessageElement
    {
        protected byte[] _bytes = null;

        public virtual int Length { get; internal set; }
        internal virtual byte[] BytesValue { get { return _bytes; } }
        internal virtual void SetValue(byte[] value) { _bytes = value; }
        public virtual byte[] RoBytesValue { get { return (byte[])BytesValue.Clone(); } }
    }

    public class MessageTypeHeader : MessageElement
    {
        public MessageTypeHeader(byte[] bytes)
        {
            if (bytes == null || bytes.Length != 2)
                throw new MessageProcessorException("Invalid value for MessageTypeHeader.");
            _bytes = bytes;
            StringValue = MessageUtility.HexToString(_bytes);
            this.MessageVersion = (MessageVersion)(_bytes[0] >> 4);
            this.MessageClass = (MessageClass)(_bytes[0] & 0x0f);
            this.MessageFunction = (MessageFunction)(_bytes[1] >> 4);
            this.MessageOrigin = (MessageOrigin)(_bytes[1] & 0x0f);
        }

        #region MessageElement Members
        public override int Length
        {
            get { return _bytes.Length; }
            internal set { }
        }

        internal override void SetValue(byte[] value) { }
        #endregion

        public String StringValue { get; private set; }
        public MessageVersion MessageVersion { get; private set; }
        public MessageClass MessageClass { get; private set; }
        public MessageFunction MessageFunction { get; private set; }
        public MessageOrigin MessageOrigin { get; private set; }

        public static implicit operator byte[](MessageTypeHeader mti)
        {
            return mti.RoBytesValue;
        }

        public static implicit operator MessageTypeHeader(byte[] bytes)
        {
            return new MessageTypeHeader(bytes);
        }
    }

    public class MessageHeader : MessageElement
    {
        #region MessageElement Members
        public override int Length
        {
            get { return _bytes == null ? 0 : _bytes.Length; }
            internal set { if (value >= 0) Array.Resize<byte>(ref _bytes, value < 0 ? 0 : value); else _bytes = null; }
        }

        internal override byte[] BytesValue
        { 
            get
            {
                if (GetFieldBytesFunc != null) return GetFieldBytesFunc();
                if (_bytes == null) throw new MessageProcessorException("This message header has not been set.");
                return _bytes;
            }
        }
        #endregion

        public GetHeaderBytes GetFieldBytesFunc { get; internal set; }
    }

    public class MessageField : MessageElement
    {
        protected String _string = null;
        protected decimal? _decimal = null;
        private int _fracDigits = 0;

        public MessageField()
        {
            this.Length = -1;
        }
        
        public bool VarLength { get; internal set; }
        public int BytesLength { get { return _bytes == null ? 0 : _bytes.Length; } }
        internal bool IsOdd { get; set; }

        public static int GetBytesLengthFromActualLength(int actualLength, int divider)
        {
            if (actualLength <= 0 || divider <= 0) return 0;
            return (actualLength - 1) / divider + 1;
        }
        
        public virtual int GetBytesLengthFromActualLength(int actualLength)
        {
            return GetBytesLengthFromActualLength(actualLength, 8);
        } 
        
        public int FracDigits
        {
            get
            {
                return _fracDigits;
            }
            internal set
            {
                if (value < 0)
                    throw new MessageProcessorException("Fractional length cannot be negative value.");
                if (_decimal != null && _fracDigits != value)
                {
                    String str = _string.Replace(".", "");
                    if (value > str.Length)
                        throw new MessageProcessorException("Fractional length assigned is greater than actual number of digits.");
                    //_bytes is kept intact
                    _string = str.Insert(str.Length - value, ".");
                    _decimal = _decimal * (decimal)Math.Pow(10, _fracDigits - value);
                }
                _fracDigits = value;
            }
        }

        internal override void SetValue(byte[] value)
        {
            if (VarLength && Length < 0) Length = value.Length * 8;
            if (Length < 0)
                throw new MessageProcessorException("The Length property is not yet set (still negative).");
            if (value.Length*8 != Length)
                throw new MessageProcessorException("The length of the assigned value is incompatible with Length property.");
            
            _bytes = value;
            _string = null;
            _decimal = null;
        }
        internal virtual void SetValue(String value) { throw new NotImplementedException(); }
        internal virtual void SetValue(decimal value) { throw new NotImplementedException(); }
        internal virtual void SetValue(int value) { SetValue((decimal)value); }

        public String StringValue { get { return _string;  } }
        public decimal? DecimalValue { get { return _decimal; } }
        public int? IntValue { get { return (int?)_decimal; } }
    }

    public class NMessageField : MessageField
    {
        public override int GetBytesLengthFromActualLength(int actualLength)
        {
            return GetBytesLengthFromActualLength(actualLength, 2);
        }

        internal override void SetValue(byte[] value)
        {
            if (VarLength && Length < 0)
            {
                Length = value.Length * 2;
                if (IsOdd && Length > 0) Length = Length - 1;
            }
            if (Length < 0)
                throw new MessageProcessorException("The Length property is not yet set (still negative).");
            if (value.Length > (Length+1)/2)
                throw new MessageProcessorException("The length of the assigned value is greater than defined length (Length property).");

            String str = MessageUtility.HexToString(value);
            foreach (char ch in str)
            {
                if (ch < '0' || ch > '9')
                    throw new MessageProcessorException("Invalid assigned value, cannot converted to numeric.");
            }
            if (str.Length < Length)
                str = str.PadLeft(Length, '0');
            else if (str.Length > Length) // str.Length == Length + 1 //odd effect
            {
                if (VarLength) str = str.Substring(0, str.Length-1);
                else str = str.Substring(1);
            }

            if (value.Length * 2 < Length)
            {
                byte[] val = new byte[(Length + 1) / 2 - value.Length]; //padded zero bytes
                value = val.Concat(value).ToArray();
            }
            _bytes = value;
            
            _string = str;
            if (FracDigits > 0) _string = _string.Insert(_string.Length - FracDigits, ".");
            
            _decimal = MessageUtility.HexToDecimal(value, FracDigits);
        }

        internal override void SetValue(String value)
        {
            SetValue(MessageUtility.StringToHex(value));
        }

        internal override void SetValue(decimal value)
        {
            SetValue(MessageUtility.DecimalToHex(value, FracDigits));
            if (_decimal == null)
            {
                _decimal = value;
                if (FracDigits > 0) _string = _string.Insert(_string.Length - FracDigits, ".");
            }
        }
    }

    public class NsMessageField : NMessageField
    {
        internal override void SetValue(byte[] value)
        {
            if (VarLength && Length < 0)
            {
                Length = value.Length * 2;
                if (IsOdd && Length > 0) Length = Length - 1;
            }
            if (Length < 0)
                throw new MessageProcessorException("The Length property is not yet set (still negative).");
            if (value.Length > (Length + 1) / 2)
                throw new MessageProcessorException("The length of the assigned value is greater than defined length (Length property).");

            String str = MessageUtility.HexToString(value);
            if (str.Length < Length)
                str = str.PadLeft(Length, '0');
            else if (str.Length > Length) // str.Length == Length + 1 //odd effect
            {
                if (VarLength) str = str.Substring(0, str.Length - 1); //left aligned
                else str = str.Substring(1); //right aligned
            }

            if (value.Length * 2 < Length)
            {
                byte[] val = new byte[(Length + 1) / 2 - value.Length]; //padded zero bytes
                value = val.Concat(value).ToArray();
            }
            
            _bytes = value;
            _string = str;
            _decimal = null;
        }
    }

    public class AnMessageField : MessageField
    {
        public override int GetBytesLengthFromActualLength(int actualLength)
        {
            return GetBytesLengthFromActualLength(actualLength, 1);
        }

        internal override void SetValue(byte[] value)
        {
            for (int i = 0; i < value.Length; i++)
            {
                if (value[i] < 0x30 || value[i] > 0x39)
                    throw new MessageProcessorException("Invalid assigned value, cannot converted to numeric");
            }
            if (VarLength && Length < 0) Length = value.Length;
            if (Length < 0)
                throw new MessageProcessorException("The Length property is not yet set (still negative).");
            else if (value.Length > Length)
                throw new MessageProcessorException("The length of the assigned value is greater than defined length (Length property).");
            else if (value.Length < Length)
            {
                byte[] val = new byte[Length - value.Length];
                for (int i = 0; i < val.Length; i++) val[i] = (byte)0x30; //'0'
                value = val.Concat(value).ToArray();
            }
            _bytes = value;
            _string = MessageUtility.AsciiArrayToString(value);
            if (FracDigits > 0) _string = _string.Insert(_string.Length - FracDigits, ".");
            _decimal = decimal.Parse(_string);
        }

        internal override void SetValue(String value)
        {
            decimal? val = MessageUtility.StringToDecimal(value);
            if (val == null) throw new MessageProcessorException("Invalid string, cannot converted to numeric."); 
            SetValue(MessageUtility.DecimalToAsciiArray((decimal)val, FracDigits));
        }

        internal override void SetValue(decimal value)
        {
            SetValue(MessageUtility.DecimalToAsciiArray(value, FracDigits));
        }
    }

    public class AnsMessageField : MessageField
    {
        public override int GetBytesLengthFromActualLength(int actualLength)
        {
            return GetBytesLengthFromActualLength(actualLength, 1);
        }

        internal override void SetValue(byte[] value)
        {
            if (VarLength && Length < 0) Length = value.Length;
            if (Length < 0)
                throw new MessageProcessorException("The Length property is not yet set (still negative).");
            else if (value.Length > Length)
                throw new MessageProcessorException("The length of the assigned value is greater than defined length (Length property).");
            else if (value.Length < Length)
            {
                byte[] val = new byte[Length - value.Length];
                for (int i = 0; i < val.Length; i++) val[i] = (byte)0x20; //Space
                value = value.Concat(val).ToArray();
            }
            _bytes = value;
            _string = MessageUtility.AsciiArrayToString(value);
            _decimal = null;
        }

        internal override void SetValue(String value)
        {
            SetValue(MessageUtility.StringToAsciiArray(value));
        }

        internal override void SetValue(decimal value)
        {
            SetValue(MessageUtility.DecimalToAsciiArray(value, FracDigits));
            _decimal = value;
            if (FracDigits > 0) _string.Insert(_string.Length - FracDigits, ".");
        }
    }

    public class NullMessageField : MessageField
    {
        public override int GetBytesLengthFromActualLength(int actualLength)
        {
            return 0;
        }

        private void SetValue()
        {
            _bytes = null;
            _decimal = null;
            _string = null;
        }

        internal override void SetValue(byte[] value) { SetValue(); }
        internal override void SetValue(String value) { SetValue(); }
        internal override void SetValue(decimal value) { SetValue(); }
        public override byte[] RoBytesValue { get { return null; } }
    }

    public class MessageBitMap : MessageField
    {
        private int _startBit = 1;

        public override int Length
        {
            get { return _bytes == null ? 0 : _bytes.Length; }
            internal set { if (value >= 0) Array.Resize<byte>(ref _bytes, value); else _bytes = null; }
        }

        public int StartBit
        {
            get { return _startBit; }
            internal set { _startBit = value; }
        }

        public int FieldSeq
        {
            get;
            internal set;
        }

        public override int GetBytesLengthFromActualLength(int actualLength)
        {
            return GetBytesLengthFromActualLength(actualLength, 1);
        }

        internal override void SetValue(byte[] value)
        {
            _bytes = value;
        }

        private static void _SetBit(int bitPos, byte[] bitMap, int startBit, bool isOn)
        {
            int bitPos2 = bitPos - startBit;
            int bytePos = bitPos2 / 8;
            int subBitPos = bitPos2 % 8;
            if (isOn)
            {
                bitMap[bytePos] = (byte)(bitMap[bytePos] | (0x80 >> subBitPos));
            }
            else
            {
                int mask1 = 0xff >> (subBitPos + 1);
                int mask2 = (0xff << (8 - subBitPos)) & 0xff;
                int mask = mask1 | mask2; //will produce bits like '11101111' with position of zero on (subBitPos+1)th position
                bitMap[bytePos] = (byte)(bitMap[bytePos] & mask);
            }
        }

        public static void SetBit(int bitPos, byte[] bitMap, int startBit = 1, bool isOn = true)
        {
            if (startBit % 8 != 1 || bitMap == null) return;
            int maxPos = 8 * bitMap.Length + startBit - 1;
            if (bitPos < startBit || bitPos > maxPos) return;
            _SetBit(bitPos, bitMap, startBit, isOn);
        }

        internal void SetBit(int bitPos, bool isOn = true)
        {
            SetBit(bitPos, _bytes, _startBit, isOn);
        }

        public static void SetBitOn(ICollection<int> bitOnList, byte[] bitMap, int startBit = 1)
        {
            if (startBit % 8 != 1 || bitMap == null) return;
            for (int i = 0; i < bitMap.Length; i++) bitMap[i] = 0;
            int maxPos = 8 * bitMap.Length + startBit - 1;
            foreach (int bitPos in bitOnList)
            {
                if (bitPos < startBit || bitPos > maxPos) continue;
                _SetBit(bitPos, bitMap, startBit, true);
            }
        }

        internal void SetBitOn(ICollection<int> bitOnList)
        {
            SetBitOn(bitOnList, _bytes, _startBit);
        }

        public static bool IsBitOn(int bitPos, byte[] bitMap, int startBit = 1)
        {
            if (startBit % 8 != 1 || bitMap == null) return false;
            int endBit = bitMap.Length * 8 + startBit - 1;
            if (bitPos < startBit || bitPos > endBit) return false;
            int bitPos2 = bitPos - startBit;
            int bytePos = bitPos2 / 8;
            int subBitPos = bitPos2 % 8;
            return (bitMap[bytePos] & (0x80 >> subBitPos)) != 0;
        }

        public bool IsBitOn(int bitPos)
        {
            return IsBitOn(bitPos, _bytes, _startBit);
        }

        public bool IsOutOfRange(int bitPos)
        {
            return (_bytes == null || bitPos < _startBit || bitPos >= _bytes.Length * 8 + _startBit);
        }

        public bool IsNull
        {
            get
            {
                bool isNull = true;
                if (_bytes != null) foreach (byte b in _bytes) isNull = isNull && b == 0;
                return isNull;
            }
        }
        
        public static implicit operator byte[](MessageBitMap bitMap)
        {
            return bitMap.RoBytesValue;
        }

        public static implicit operator MessageBitMap(byte[] bytes)
        {
            MessageBitMap bitMap = new MessageBitMap();
            bitMap.SetValue(bytes);
            return bitMap;
        }
    }


    public class MessageBitMapCollection
    {
        private List<MessageBitMap> _bitMaps = new List<MessageBitMap>();
        
        internal void Add(MessageBitMap bitMap)
        {
            bitMap.StartBit = _bitMaps.Count > 0 ? _bitMaps.Last().StartBit + _bitMaps.Last().Length * 8 : 1;
            _bitMaps.Add(bitMap);
        }

        internal void SetBit(int bitPos, bool isOn = true)
        {
            foreach (MessageBitMap bitMap in _bitMaps) bitMap.SetBit(bitPos, isOn);
        }

        internal void SetBitOn(ICollection<int> bitOnList)
        {
            foreach (MessageBitMap bitMap in _bitMaps) bitMap.SetBitOn(bitOnList);
        }

        public bool IsBitOn(int bitPos)
        {
            foreach (MessageBitMap bitMap in _bitMaps) if (bitMap.IsBitOn(bitPos)) return true;
            return false;
        }

        public List<MessageBitMap> ToList()
        {
            return _bitMaps;
        }

        public byte[] BytesValue
        {
            get
            {
                if (_bitMaps.Count == 0) return new byte[0];
                IEnumerable<byte> bytes = _bitMaps[0].BytesValue;
                for (int i = 1; i < _bitMaps.Count; i++) bytes = bytes.Concat(_bitMaps[i].BytesValue);
                return bytes.ToArray(); //new copy
            }
        }

        public int Length
        {
            get
            {
                int length = 0;
                foreach (byte[] bitmap in _bitMaps) length += bitmap.Length;
                return length;
            }
        }

        public static implicit operator byte[](MessageBitMapCollection bitMapCollection)
        {
            return bitMapCollection.BytesValue; //new copy
        }
    }


    public class ParsedMessageContainer
    {
        private List<MessageElement> _headers = new List<MessageElement>();
        private Dictionary<int,MessageField> _fields = new Dictionary<int,MessageField>();
        
        internal IList<MessageElement> Headers { get { return _headers; } }
        internal IDictionary<int, MessageField> Fields { get { return _fields; } }
        public IList<MessageElement> RoHeaders { get { return _headers.AsReadOnly(); } }
        public IDictionary<int, MessageField> RoFields { get { return new RoDictionary<int, MessageField>(_fields); } }
    }


    public class CompiledMessageField
    {
        internal byte[] Header { get; set; }
        public byte[] RoHeader { get { return (byte[])Header.Clone();  } }
        public MessageField Content { get; internal set;  }
        public int Length
        {
            get { return HeaderLength + (Content == null || Content.BytesValue == null ? 0 : Content.BytesValue.Length); }
        }
        public int HeaderLength { get { return Header == null ? 0 : Header.Length; } }
    }
    
    public class CompiledMessageContainer
    {
        private List<MessageElement> _headers = new List<MessageElement>();
        private Dictionary<int, CompiledMessageField> _fields = new Dictionary<int, CompiledMessageField>();
        private MessageBitMapCollection _bitMap = new MessageBitMapCollection();

        public int LengthHeader { get; internal set; }

        public MessageTypeHeader MessageType { get; private set; }
        public MessageBitMapCollection BitMap { get { return _bitMap; } }
        
        internal IList<MessageElement> Headers { get { return _headers; } }
        internal IDictionary<int, CompiledMessageField> Fields { get { return _fields; } }
        public IList<MessageElement> RoHeaders { get { return _headers.AsReadOnly(); } }
        public IDictionary<int, CompiledMessageField> RoFields { get { return new RoDictionary<int, CompiledMessageField>(_fields); } }

        internal void AddHeader(MessageElement hdr)
        {
            _headers.Add(hdr);
            if (hdr is MessageBitMap)
            {
                ((MessageBitMap)hdr).FieldSeq = 0;
                _bitMap.Add((MessageBitMap)hdr);
            }
            if (hdr is MessageTypeHeader) this.MessageType = (MessageTypeHeader)hdr;
        }

        internal void AddField(int seq, CompiledMessageField cmf)
        {
            _fields.Add(seq, cmf);
            if (cmf.Content is MessageBitMap)
            {
                ((MessageBitMap)cmf.Content).FieldSeq = seq;
                _bitMap.Add((MessageBitMap)cmf.Content);
            }
        }

        public byte[] GetAllBytes()
        {
            ICollection<int> bitOnList = _fields.Keys;
            _bitMap.SetBitOn(bitOnList);

            //Don't include null BitMap (no bit on).
            List<MessageBitMap> bitMapList = _bitMap.ToList();
            for (int x = bitMapList.Count - 1; x >= 1; x--) //Excludes primary BitMap when checking
            {
                if (bitMapList[x].IsNull) _bitMap.SetBit(bitMapList[x].FieldSeq, false);
            }

            int length = 0;
            foreach (MessageElement hdr in _headers) length += hdr.Length;
            foreach (CompiledMessageField fld in _fields.Values)
            {
                if (fld.Content is MessageBitMap && ((MessageBitMap)fld.Content).IsNull) continue; //null BitMap not included
                length += fld.Length;
            }
            
            byte[] bytes = new byte[LengthHeader + length];
            byte[] lenHdr = MessageUtility.IntToBytes((ulong)length, LengthHeader);
            Array.Copy(lenHdr, 0, bytes, 0, LengthHeader);

            int i = LengthHeader;
            foreach (MessageElement hdr in _headers)
            {
                if (hdr.BytesValue == null) continue;
                Array.Copy(hdr.BytesValue, 0, bytes, i, hdr.Length);
                i += hdr.Length;
            }
            
            //int[] bits = bitOnList.ToArray();
            //Array.Sort(bits); //Has been ordered by XmlConfigParser
            ICollection<int> bits = bitOnList;
            foreach (int j in bits)
            {
                CompiledMessageField fld = _fields[j];
                if (fld.Content.BytesValue == null) continue;
                if (fld.Content is MessageBitMap && ((MessageBitMap)fld.Content).IsNull) continue;
                if (fld.Header != null) Array.Copy(fld.Header, 0, bytes, i, fld.HeaderLength);
                Array.Copy(fld.Content.BytesValue, 0, bytes, i + fld.HeaderLength, fld.Length - fld.HeaderLength);
                i += fld.Length;
            }
            return bytes;
        }
    }
}
