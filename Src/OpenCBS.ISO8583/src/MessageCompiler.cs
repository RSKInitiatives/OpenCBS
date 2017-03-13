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
using Free.iso8583.model;

namespace Free.iso8583
{
    public interface ICompiledMessage
    {
        CompiledMessageContainer CompiledMessage { get; }
        Object Model { get; }
    }

    public class MessageCompiler : ICompiledMessage
    {
        private Object _model;
        private IParsedMessage _reqMsg = null;
        private CompiledMessageContainer _struct = new CompiledMessageContainer();
        private ModelConfig _modelCfg;
        private MessageConfig _msgCfg;
        
        protected MessageCompiler()
        {
        }

        public MessageCompiler(Object model, IParsedMessage reqMsg)
        {
            _model = model;
            _reqMsg = reqMsg;
        }

        public MessageCompiler(Object model) : this(model, null)
        {
        }

        public void Compile()
        {
            Type modelType = _model.GetType();
            if (!MessageConfigs.ClassToModels.ContainsKey(modelType))
            {
                throw new MessageCompilerException("Cannot compile model " + modelType.FullName
                    + ". Check configuration for message elements. ");
            }

            _modelCfg = MessageConfigs.ClassToModels[modelType];
            _msgCfg = _modelCfg.MessageCfg;

            _struct.LengthHeader = _msgCfg.LengthHeader;
            CompileHeaders();
            CompileFields();
        }

        private void CompileHeaders()
        {
            foreach (IMessageHeaderConfig hdrCfg in _msgCfg.Headers)
            {
                MessageElement hdr = hdrCfg.GetNewHeader();
                _struct.AddHeader(hdr);
            }
        }

        private void CompileFields()
        {
            String[] locMsgs = new String[] { "model element (id=" + (_modelCfg.Id==null?"":_modelCfg.Id) + ", class="
                + (_modelCfg.ClassType==null?"":_modelCfg.ClassType.FullName) + ") and property (name=", null, ")" };
            
            foreach (KeyValuePair<int, MessageFieldConfig> kvpFldCfg in _msgCfg.Fields)
            {
                MessageFieldConfig fldCfg = kvpFldCfg.Value;
                ModelPropertyConfig propCfg = _modelCfg.Properties.ContainsKey(fldCfg.Seq) ? _modelCfg.Properties[fldCfg.Seq] : null;
                MessageField fld = (MessageField)Activator.CreateInstance(fldCfg.FieldType);
                Object propVal = propCfg == null ? null : propCfg.PropertyInfo.GetValue(_model, null);
                byte[] fldVal = null;

                if (fldCfg.FieldType != typeof(NullMessageField) && fldCfg.FieldType != typeof(MessageBitMap))
                {
                    if (/*propCfg==null ||*/ propVal == null)
                    {
                        if (_reqMsg != null && fldCfg.FromRequest)
                        {
                            IDictionary<int, MessageField> fields = _reqMsg.ParsedMessage.Fields;
                            fldVal = fields.ContainsKey(fldCfg.Seq) ? fields[fldCfg.Seq].BytesValue : null;
                        }
                    }
                    else if (propCfg.Tlv != null || !String.IsNullOrEmpty(propCfg.TlvTagName))
                    {
                        locMsgs[1] = propCfg.PropertyInfo.Name;
                        fldVal = CompileTlv(propCfg.Tlv, propCfg.TlvTagName, propCfg.TlvLengthBytes, propVal,
                            String.Join("", locMsgs));
                    }
                    else if (propCfg.BitContent != null)
                    {
                        fldVal = CompileBitContent(propCfg.BitContent, propVal);
                    }
                    else if (fldCfg.GetFieldBytesFunc != null)
                    {
                        fldVal = (byte[])fldCfg.GetFieldBytesFunc.DynamicInvoke(propVal);
                    }

                    byte[] header = null;
                    if (fldVal != null || (propVal != null && propCfg.SetValueFromProperty != null)) //propCfg != null ==> propVal != null
                    {
                        if (fldCfg.Length >= 0)
                        {
                            if (fldVal != null && fldVal.Length != fld.GetBytesLengthFromActualLength(fldCfg.Length))
                            {
                                throw new MessageCompilerException(
                                    "Incompatible length of bytes between compiled bytes length "
                                    + "and the defined one in configuration. Check the config for model (class="
                                    + _modelCfg.ClassType.FullName
                                    + ") and property (name=" + (propCfg != null ? propCfg.PropertyInfo.Name : "")
                                    + ") and also its mapped field in message (id="
                                    + _msgCfg.Id + ")");
                            }
                            fld.Length = fldCfg.Length;
                        }
                        if (propVal is NibbleList) fld.IsOdd = ((NibbleList)propVal).IsOdd;
                        if (propCfg != null) fld.FracDigits = propCfg.FracDigits;
                        fld.VarLength = (fldCfg.Length < 0);

                        if (fldVal != null) fld.SetValue(fldVal);
                        else //if (propVal != null /* && propCfg != null */ && propCfg.SetValueFromProperty != null)
                        {
                            try
                            {
                                Type paramType = propCfg.SetValueFromProperty.GetParameters()[0].ParameterType;
                                propVal = Util.GetAssignableValue(paramType, propVal);
                                propCfg.SetValueFromProperty.Invoke(fld, new Object[] { propVal });
                            }
                            catch (Exception ex)
                            {
                                throw new MessageCompilerException(
                                    "Cannot convert a property value to the coresponding message field. "
                                    + "Check the config for model (class=" + _modelCfg.ClassType.FullName
                                    + ") and property (name=" + propCfg.PropertyInfo.Name
                                    + ") and also its mapped field in message (id="
                                    + _msgCfg.Id + ")", ex);
                            }
                        }

                        if (fldCfg.LengthHeader >= 0) //It must be fld.VarLength == true (See XmlConfigParser)
                            header = MessageUtility.IntToHex((ulong)fld.Length, fldCfg.LengthHeader);

                        CompiledMessageField cmf = new CompiledMessageField();
                        cmf.Header = header;
                        cmf.Content = fld;
                        _struct.AddField(fldCfg.Seq, cmf);
                    }
                }
                else //field is NULL type or BitMap
                {
                    CompiledMessageField cmf = new CompiledMessageField();
                    cmf.Header = null;
                    fld.Length = fldCfg.Length;
                    cmf.Content = fld;
                    _struct.AddField(fldCfg.Seq, cmf);
                }
            }
        }

        private byte[] CompileBitContent(BitContentConfig cfg, Object obj)
        {
            if (cfg.ClassType != obj.GetType())
            {
                throw new MessageCompilerException("Cannot compile a BitContent field. Not match target type of object. The passed "
                    + "object has type of " + obj.GetType().FullName + " whereas the BitContent requires type of "
                    + cfg.ClassType.FullName + ". Please check configuration for BitContent (id=" + cfg.Id);
            }

            String[] locMsgs = new String[] { "BitContent element (id=" + cfg.Id + ") and field (name=",
                null, ")" };
            List<byte> bytes = new List<byte>();
            foreach (BitContentFieldConfig ccfg in cfg.Fields)
            {
                Object propVal = ccfg.PropertyInfo.GetValue(obj, null);
                if (propVal == null)
                {
                    if (!ccfg.IsOptional)
                        for (int i = 0; i < ccfg.Length; i++) bytes.Add(ccfg.NullChar);
                }
                else if (propVal.GetType() == typeof(byte[]))
                {
                    bytes.AddRange((byte[])propVal);
                }
                else if (ccfg.Tlv != null || !String.IsNullOrEmpty(ccfg.TlvTagName))
                {
                    locMsgs[1] = ccfg.PropertyInfo.Name;
                    byte[] val = CompileTlv(ccfg.Tlv, ccfg.TlvTagName, ccfg.TlvLengthBytes, propVal,
                        String.Join("", locMsgs));
                    bytes.AddRange(val);
                }
                else
                {
                    byte[] value = MessageUtility.StringToAsciiArray(propVal.ToString());
                    if (value.Length > ccfg.Length)
                    {
                        locMsgs[1] = ccfg.PropertyInfo.Name;
                        throw new MessageCompilerException(
                            "Cannot compile a BitContent field. The length of field's value is too big more than "
                            + ccfg.Length + " chars. Check configuration " + String.Join("",locMsgs));
                    }
                    int i = value.Length;
                    if (ccfg.Align == "right")
                    {
                        for (; i < ccfg.Length; i++) bytes.Add(ccfg.PadChar);
                    }
                    bytes.AddRange(value);
                    if (ccfg.Align != "right")
                    {
                        for (; i < ccfg.Length; i++) bytes.Add(ccfg.PadChar);
                    }
                }
            }
            return bytes.ToArray();
        }

        private byte[] CompileTlv(TlvConfig cfg, String tagName, int lengthBytes, Object obj, String locMsg)
        {
            int tagLength = (cfg != null && cfg.TagsCount > 0) ? cfg.GetTag(0).Name.Length
                : (tagName.Length > 0 ? tagName.Length : 2);
            if (cfg != null && cfg.LengthBytes > 0) lengthBytes = cfg.LengthBytes;

            if (String.IsNullOrEmpty(tagName) && (cfg == null || cfg.TagsCount <= 0))
            {
                throw new MessageCompilerException(
                    "Cannot compile a tlv field. It cannot determine the tag ID. Check configuration "
                    + locMsg);
            }

            IList<TlvTagConfig> tagCfgs = null;
            if (cfg != null) tagCfgs = cfg.Tags;
            if (tagCfgs == null) tagCfgs = new List<TlvTagConfig>();
            if (tagCfgs.Count <= 0)
            {
                TlvTagConfig tagCfg = new TlvTagConfig();
                tagCfg.Name = tagName;
                tagCfgs.Add(tagCfg);
            }

            IDictionary<String, Object> tagMapVal = new Dictionary<String, Object>();
            if (obj is IDictionary<String, Object>)
            {
                IDictionary<String, Object> map = (IDictionary<String, Object>)obj;
                foreach (TlvTagConfig tagCfg in tagCfgs)
                {
                    if (map.ContainsKey(tagCfg.Name))
                        tagMapVal[tagCfg.Name] = map[tagCfg.Name];
                }
            }
            else if (obj != null)
            {
                foreach (TlvTagConfig tagCfg in tagCfgs)
                {
                    PropertyInfo property = obj.GetType().GetProperty(tagCfg.Name);
                    if (property != null) tagMapVal[tagCfg.Name] = property.GetValue(obj,null);
                }
            }
            if (tagMapVal.Count <= 0 && tagCfgs.Count == 1)
            {
                tagMapVal[tagCfgs[0].Name] = obj;
            }
            
            StringBuilder sb = new StringBuilder();
            foreach (TlvTagConfig tagCfg in tagCfgs)
            {
                sb.Append(tagCfg.Name);
                String len = "0";
                String strVal = "";
                if (tagMapVal.ContainsKey(tagCfg.Name) && tagMapVal[tagCfg.Name] != null)
                {
                    Object value = tagMapVal[tagCfg.Name];
                    if (tagCfg.Splitter != null)
                    {
                        if (value is IEnumerable<Object>)
                        {
                            strVal = Util.Join((IEnumerable<Object>)value, tagCfg.Splitter);
                        }
                        else
                        {
                            throw new MessageCompilerException(
                                "Cannot compile a tlv field. The passed value is not an array. Check configuration "
                                + locMsg + " for tag " + tagCfg.Name);
                        }
                    }
                    else if (tagCfg.BitContent != null)
                    {
                        byte[] val = CompileBitContent(tagCfg.BitContent, value);
                        strVal = System.Text.Encoding.ASCII.GetString(val);
                    }
                    else
                    {
                        strVal = value.ToString();
                    }
                }

                len = strVal.Length.ToString();
                if (len.Length > lengthBytes)
                {
                    throw new MessageCompilerException(
                        "Cannot compile a tlv field. The length's value is too big more than " + lengthBytes
                        + " digits. Check configuration " + locMsg + " for tag " + tagCfg.Name);
                }
                else if (len.Length < lengthBytes)
                {
                    len = len.PadLeft(lengthBytes, '0');
                }
                sb.Append(len);
                sb.Append(strVal);
            }

            return MessageUtility.StringToAsciiArray(sb.ToString());
        }

        #region ICompiledMessage Members
        public CompiledMessageContainer CompiledMessage
        {
            get { return _struct; }
        }

        public object Model
        {
            get { return _model; }
        }
        #endregion

        internal ModelConfig ModelCfg
        {
            get { return _modelCfg; }
        }
    }
}
