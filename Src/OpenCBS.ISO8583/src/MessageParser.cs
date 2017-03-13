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
using System.Reflection;
using Free.iso8583.config;

namespace Free.iso8583
{
    public interface IParsedMessage
    {
        ParsedMessageContainer ParsedMessage { get; }
        Object Model { get; }
        MessageTypeHeader MessageType { get; }
        MessageBitMapCollection BitMap { get; }
        byte[] GetHeader(String name);
        byte[] AllBytes { get; }
    }

    public class MessageParser : IParsedMessage
    {
        private MessageProcessorWorker _workerThread = null;
        private int _token;
        private byte[] _bytes;
        private Object _model;
        private ParsedMessageContainer _struct = new ParsedMessageContainer();
        private ModelConfig _modelCfg;
        private MessageConfig _msgCfg;
        private MessageBitMapCollection _bitMap = new MessageBitMapCollection();
        private byte[] _messageType;
        private IDictionary<String, byte[]> _headers = new Dictionary<String,byte[]>();

        protected MessageParser()
        {
        }

        public MessageParser(byte[] bytes) : this(bytes, null)
        {
        }

        internal MessageParser(byte[] bytes, MessageProcessorWorker workerThread)
        {
            _bytes = bytes;
            _workerThread = workerThread;
        }

        public void Parse()
        {
            _token = 0;

            MessageToModelConfig cfg = GetMessageToModelConfig();
            _modelCfg = cfg.ModelCfg;
            _msgCfg = _modelCfg.MessageCfg;
            ProcessModel = cfg.ProcessModel;

            CheckMessageLength();
            ParseHeaders();
            ParseFields();
            ConvertToModel();
        }

        private MessageToModelConfig GetMessageToModelConfig()
        {
            MessageToModelConfig cfg = (_workerThread != null && _workerThread.MessageToModelConfig != null)
                ? _workerThread.MessageToModelConfig
                : MessageConfigs.GetQulifiedMessageToModel(_bytes);
            if (cfg == null)
            {
                byte[] bytes2 = new byte[config.MaskConfig.MinBytesCountToCheck < _bytes.Length
                    ? config.MaskConfig.MinBytesCountToCheck : _bytes.Length];
                Array.Copy(_bytes, bytes2, bytes2.Length);
                throw new MessageParserException("No matching config for this message. Check message-to-model elements in configuration file."
                    + Environment.NewLine
                    + bytes2.Length + " first bytes of message: " + MessageUtility.HexToReadableString(bytes2));
            }
            return cfg;
        }

        private void CheckToken(int nextFieldLength, IMessageHeaderConfig curHeader, MessageFieldConfig curField)
        {
            if (_token + nextFieldLength > _bytes.Length)
            {
                String msg = "Invalid configuration. Check xml configuration pertaining message id="
                    + _msgCfg.Id + " and its corresponding message-to-model. ";
                if (curHeader != null)
                {
                    msg += "Parsing failed at header ";
                    if (curHeader is MessageHeaderConfig) msg += "'" + ((MessageHeaderConfig)curHeader).Name + "'.";
                    else if (curHeader is MessageBitMapConfig) msg += "BitMap.";
                    else msg += "Message Type.";
                }
                if (curField != null)
                {
                    msg += "Parsing failed at field bit#" + curField.Seq;
                }
                throw new MessageParserException(msg);
            }
        }

        private void CheckMessageLength()
        {
            byte[] lenHdr = new byte[_msgCfg.LengthHeader];
            Array.Copy(_bytes, 0, lenHdr, 0, _msgCfg.LengthHeader);
            int length = (int)MessageUtility.BytesToInt(lenHdr);
            if (_bytes.Length - _msgCfg.LengthHeader != length)
                throw new MessageParserException("Incompatible length header value with the actual message length.");
            _token += _msgCfg.LengthHeader;
        }

        private void ParseHeaders()
        {
            foreach (IMessageHeaderConfig hdrCfg in _msgCfg.Headers)
            {
                CheckToken(hdrCfg.Length, hdrCfg, null);
                MessageElement hdr = hdrCfg.GetNewHeader();
                byte[] bytes = new byte[hdrCfg.Length];
                Array.Copy(_bytes, _token, bytes, 0, hdrCfg.Length);
                hdr.SetValue(bytes);
                _struct.Headers.Add(hdr);

                if (hdrCfg is MessageBitMapConfig)
                {
                    MessageBitMap bitMap = bytes;
                    bitMap.FieldSeq = 0;
                    _bitMap.Add(bitMap);
                }
                else if (hdrCfg is MessageTypeConfig) _messageType = bytes;
                else
                {
                    MessageHeaderConfig cfg = (MessageHeaderConfig)hdrCfg;
                    if (!String.IsNullOrEmpty(cfg.Name)) _headers[cfg.Name] = bytes;
                }
                
                _token += hdrCfg.Length;
            }
        }

        private void ParseFields()
        {
            foreach (KeyValuePair<int,MessageFieldConfig> kvp in _msgCfg.Fields)
            {
                MessageFieldConfig cfg = kvp.Value;

                /* Because message fields configuration has been ordered based on their sequence number. The statement
                 * below never misses secondary or tertiary BitMap (secondary and/or tertiary BitMap have been included
                 * in _bitMap collection if cfg.Seq must be identified by those BitMaps) */
                if (!_bitMap.IsBitOn(cfg.Seq)) continue;

                int length;
                if (cfg.Length >= 0)
                {
                    length = cfg.Length;
                }
                else
                {
                    int hdrLen = cfg.LengthHeader;
                    CheckToken(hdrLen, null, cfg);
                    byte[] hdr = new byte[hdrLen];
                    Array.Copy(_bytes, _token, hdr, 0, hdrLen);
                    _token += hdrLen;
                    
                    length = (int)MessageUtility.HexNDigitsToInt(hdr);
                }

                MessageField msgFld = (MessageField)Activator.CreateInstance(cfg.FieldType);
                int bytesLength = msgFld.GetBytesLengthFromActualLength(length);
                CheckToken(bytesLength, null, cfg);
                byte[] bytesVal = new byte[bytesLength];
                Array.Copy(_bytes, _token, bytesVal, 0, bytesLength);
                _token += bytesLength;

                msgFld.Length = length;
                msgFld.VarLength = (cfg.Length < 0);
                msgFld.SetValue(bytesVal);
                _struct.Fields.Add(cfg.Seq, msgFld);

                if (msgFld is MessageBitMap)
                {
                    ((MessageBitMap)msgFld).FieldSeq = cfg.Seq;
                    _bitMap.Add((MessageBitMap)msgFld);
                }
            }
        }

        private void ConvertToModel()
        {
            _model = Activator.CreateInstance(_modelCfg.ClassType);
            String[] locMsgs = new String[] {
                "model element (id=" + _modelCfg.Id + ", class=" + _modelCfg.ClassType.FullName + ") and property (name=",
                null, ")" };
            
            foreach (KeyValuePair<int, ModelPropertyConfig> kvp in _modelCfg.Properties)
            {
                ModelPropertyConfig propCfg = kvp.Value;
                Object propVal = null;
                if (_struct.Fields.ContainsKey(propCfg.FieldBit.Seq))
                {
                    MessageField fld = _struct.Fields[propCfg.FieldBit.Seq];
                    if (fld.BytesValue == null)
                    {
                        //propVal = null;
                    }
                    else if (propCfg.Tlv != null || !String.IsNullOrEmpty(propCfg.TlvTagName))
                    {
                        locMsgs[1] = propCfg.PropertyInfo.Name;
                        int tlvLength = 0;
                        propVal = ParseTlv(propCfg.PropertyInfo.PropertyType, propCfg.Tlv, propCfg.TlvTagName,
                            propCfg.TlvLengthBytes, propCfg.TlvClassType, fld.BytesValue, String.Join("", locMsgs),
                            false, ref tlvLength);
                    }
                    else if (propCfg.BitContent != null)
                    {
                        propVal = ParseBitContent(propCfg, fld);
                    }
                    else if (propCfg.GetPropertyValueFunc != null)
                    {
                        propVal = propCfg.GetPropertyValueFunc.DynamicInvoke(fld.BytesValue);
                    }
                    else
                    {
                        fld.FracDigits = propCfg.FracDigits;
                        propVal = propCfg.GetValueFromBytes.GetValue(fld, null);
                    }
                }

                Object propVal2 = propVal;
                if (propVal2 != null)
                {
                    propVal2 = Util.GetAssignableValue(propCfg.PropertyInfo.PropertyType, propVal);
                    if (propVal2 == null)
                    {
                        throw new MessageParserException("Cannot convert " + propVal.GetType().FullName + " to "
                            + propCfg.PropertyInfo.PropertyType.FullName + ". Failed to convert incoming message to model "
                            + _modelCfg.ClassType.FullName + " at property " + propCfg.PropertyInfo.Name);
                    }
                }

                propCfg.PropertyInfo.SetValue(_model, propVal2, null);
            }
        }

        private Object ParseBitContent(ModelPropertyConfig cfg, MessageField fld)
        {
            return ParseBitContent(cfg.BitContent, fld.BytesValue);
        }

        private Object ParseBitContent(BitContentConfig bcc, byte[] bytesValue)
        {
            Object bitContent = Activator.CreateInstance(bcc.ClassType);
            int i = 0;
            String[] locMsgs = new String[] { "BitContent element (id="+bcc.Id+") and field (name=", null, ")"};
            foreach (BitContentFieldConfig bcfc in bcc.Fields)
            {
                Object propValue = null;

                if (bcfc.Tlv != null || !String.IsNullOrEmpty(bcfc.TlvTagName))
                {
                    int len = bytesValue.Length - i;
                    byte[] bytes = new byte[len];
                    Array.Copy(bytesValue, i, bytes, 0, len);
                    locMsgs[1] = bcfc.PropertyInfo.Name;
                    int tlvLength = 0;
                    propValue = ParseTlv(bcfc.PropertyInfo.PropertyType, bcfc.Tlv, bcfc.TlvTagName, bcfc.TlvLengthBytes,
                        bcfc.TlvClassType, bytes, String.Join("", locMsgs), true, ref tlvLength);
                    i += tlvLength;
                }
                else
                {
                    int len = (i + bcfc.Length > bytesValue.Length) ? (bytesValue.Length - i) : bcfc.Length;
                    if (len <= 0) break;
                    byte[] bytes = new byte[len];
                    Array.Copy(bytesValue, i, bytes, 0, len);
                    String val = MessageUtility.AsciiArrayToString(bytes);
                    if (val != null)
                    {
                        if (bcfc.IsTrim) val = val.Trim();
                        propValue = val;
                    }
                    else
                    {
                        Type bcfType = bcfc.PropertyInfo.PropertyType;
                        propValue = Util.GetAssignableValue(bcfType, bytes);
                        if (propValue == null)
                        {
                            throw new MessageParserException("Cannot parse BitContent field named " + bcfc.PropertyInfo.Name
                                + ". Check configuration for BitContent element (id=" + bcc.Id + ")");
                        }
                    }
                    i += bcfc.Length;
                }

                bcfc.PropertyInfo.SetValue(bitContent, propValue, null);
                if (i >= bytesValue.Length) break;
            }
            return bitContent;
        }

        private Object ParseTlv(Type propType, TlvConfig cfg, String tagName, int lengthBytes, Type tlvType,
                byte[] bytesValue, String locMsg, bool isAlwaysOneTag, ref int tlvLength)
        {
            int tagLength = (cfg != null && cfg.TagsCount > 0) ? cfg.GetTag(0).Name.Length
                : (tagName.Length > 0 ? tagName.Length : 2);
            if (cfg != null && cfg.LengthBytes > 0) lengthBytes = cfg.LengthBytes;
            if (cfg != null && cfg.ClassType != null) tlvType = cfg.ClassType;
            
            IDictionary<String, Object> tagValMap = new Dictionary<String, Object>();
            int i = 0;
            byte[] bytes;
            while (i < bytesValue.Length)
            {
                CheckTlvToken(i, tagLength, bytesValue, locMsg);
                bytes = new byte[tagLength];
                Array.Copy(bytesValue, i, bytes, 0, tagLength);
                i += tagLength;
                String tag = MessageUtility.AsciiArrayToString(bytes);
                if (tag == null) throw new MessageParserException("Invalid tag value for a tlv field. Check configuration " + locMsg);

                CheckTlvToken(i, lengthBytes, bytesValue, locMsg);
                bytes = new byte[lengthBytes];
                Array.Copy(bytesValue, i, bytes, 0, lengthBytes);
                i += lengthBytes;
                int len = MessageUtility.AsciiArrayToInt(bytes);
                if (len < 0) throw new MessageParserException("Invalid length value for a tlv field. Check configuration " + locMsg);

                if (len > 0)
                {
                    CheckTlvToken(i, len, bytesValue, locMsg);
                    bytes = new byte[len];
                    Array.Copy(bytesValue, i, bytes, 0, len);
                    i += len;
                    
                    TlvTagConfig tagCfg = cfg != null ? cfg.GetTag(tag) : null;
                    if (tagCfg != null && tagCfg.BitContent != null)
                    {
                        tagValMap[tag] = ParseBitContent(tagCfg.BitContent, bytes);
                    }
                    else
                    {
                        String value = System.Text.Encoding.ASCII.GetString(bytes);
                        if (tagCfg != null && tagCfg.Splitter != null)
                            tagValMap[tag] = value.Split(new String[] { tagCfg.Splitter }, StringSplitOptions.None);
                        else
                            tagValMap[tag] = value;
                    }
                }
                
                if (isAlwaysOneTag) break;
            }

            tlvLength = i;
            if (tagValMap.Count <= 0) return null;

            String convertFailedMsg = "Cannot convert a tlv field into the mapped property. Check " + locMsg;
            if (tlvType != null && propType.IsAssignableFrom(tlvType))
            {
                Object propValue = Activator.CreateInstance(tlvType);
                foreach (KeyValuePair<String, Object> kvp in tagValMap)
                {
                    PropertyInfo property = tlvType.GetProperty(kvp.Key);
                    if (property == null)
                    {
                        throw new MessageParserException(convertFailedMsg);
                    }

                    Object propValue2 = kvp.Value;
                    if (propValue2 != null)
                    {
                        propValue2 = Util.GetAssignableValue(property.PropertyType, kvp.Value);
                        if (propValue2 == null)
                        {
                            throw new MessageParserException(convertFailedMsg);
                        }
                    }
                    property.SetValue(propValue, propValue2, null);
                }
                return propValue;
            }
            else 
            {
                if (tagValMap.Count == 1)
                {
                    foreach (Object value in tagValMap.Values)
                    {
                        if (value == null) return null;
                        Object val = Util.GetAssignableValue(propType, value);
                        if (val != null)
                        {
                            return val;
                        }
                    }
                }
                if (!propType.IsAssignableFrom(tagValMap.GetType()))
                {
                    Object propValue = Activator.CreateInstance(propType);
                    foreach (KeyValuePair<String, Object> kvp in tagValMap)
                    {
                        PropertyInfo property = propType.GetProperty(kvp.Key);
                        if (property == null)
                        {
                            throw new MessageParserException(convertFailedMsg);
                        }

                        Object propValue2 = kvp.Value;
                        if (propValue2 != null)
                        {
                            propValue2 = Util.GetAssignableValue(property.PropertyType, kvp.Value);
                            if (propValue2 == null)
                            {
                                throw new MessageParserException(convertFailedMsg);
                            }
                        }
                        property.SetValue(propValue, propValue2, null);
                    }
                    return propValue;
                }
            }

            return tagValMap;
        }

        private void CheckTlvToken(int token, int length, byte[] bytes, String locMsg)
        {
            if (token + length > bytes.Length)
            {
                throw new MessageParserException("Failed to parse tlv field. Check " + locMsg);
            }
        }

        #region IParsedMessage Members
        public ParsedMessageContainer ParsedMessage
        {
            get { return _struct; }
        }

        public object Model
        {
            get { return _model; }
        }

        public MessageBitMapCollection BitMap
        {
            get { return _bitMap; }
        }

        public MessageTypeHeader MessageType
        {
            get { return _messageType; }
        }

        public byte[] GetHeader(string name)
        {
            return (byte[])_headers[name].Clone();
        }
        
        public byte[] AllBytes
        {
            get { return (byte[])_bytes.Clone(); }
        }
        #endregion

        public Delegate ProcessModel
        {
            get; private set;
        }

        internal ModelConfig ModelCfg
        {
            get { return _modelCfg; }
        }
    }
}
