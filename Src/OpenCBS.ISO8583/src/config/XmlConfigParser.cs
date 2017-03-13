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
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Reflection;

namespace Free.iso8583.config
{
    internal class XmlConfigParser : ConfigParser
    {
        private static readonly HashSet<String> _propertyTypes = new HashSet<String> { "string", "int", "decimal", "bytes" };
        
        private IDictionary<String, XmlElement> _elementIds = new Dictionary<String, XmlElement>();
        private XmlDocument _docs = new XmlDocument();
        private IList<XmlElement> _types = new List<XmlElement>();
        private IList<XmlElement> _delegates = new List<XmlElement>();
        private IList<XmlElement> _bitContents = new List<XmlElement>();
        private IList<BitContentFieldConfig> _bitContentFieldsReferTlv = new List<BitContentFieldConfig>();
        private IList<String> _referredTlvIds = new List<String>();
        private IList<XmlElement> _tlvs = new List<XmlElement>();
        private HashSet<String> _tlvIds = new HashSet<String>();
        private IList<XmlElement> _messages = new List<XmlElement>();
        private IList<XmlElement> _models = new List<XmlElement>();
        private HashSet<String> _modelIds = new HashSet<String>();
        private IDictionary<String, XmlElement> _childModels = new Dictionary<String, XmlElement>();
        private IList<XmlElement> _messageToModels = new List<XmlElement>();
        private IList<XmlElement> _hpdhEdcTransactionTypes = new List<XmlElement>();
        

        private XmlConfigParser()
        {
        }
        public XmlConfigParser(Stream fileConfig)
        {
            _docs.Load(fileConfig);
        }

        private XmlElement GetElementById(String id)
        {
            return _elementIds.ContainsKey(id) ? _elementIds[id] : null;
        }

        public override void Parse()
        {
            if (_docs.DocumentElement.Name != "MessageMap")
            {
                throw new ConfigParserException("The root element must be MessageMap");
            }
            foreach (XmlNode node in _docs.DocumentElement.ChildNodes)
            {
                if (node.NodeType != XmlNodeType.Element) continue;

                XmlElement elm = (XmlElement)node;
                String id = elm.GetAttribute("id");
                if (!String.IsNullOrEmpty(id)) id = id.Trim(); else id = null;

                switch (node.Name)
                {
                    case "type":
                        _types.Add(elm);
                        break;
                    case "delegate":
                        _delegates.Add(elm);
                        break;
                    case "tlv":
                        if (id != null) _tlvIds.Add(id);
                        _tlvs.Add(elm);
                        break;
                    case "BitContent":
                        _bitContents.Add(elm);
                        break;
                    case "message":
                        _messages.Add(elm);
                        break;
                    case "model":
                        if (id != null) _modelIds.Add(id);
                        _models.Add(elm);
                        break;
                    case "message-to-model":
                        _messageToModels.Add(elm);
                        break;
                    case "hpdh-edc-transaction-type":
                        _hpdhEdcTransactionTypes.Add(elm);
                        break;
                    default:
                        throw new ConfigParserException("Unknown tag " + node.Name + " or it's invalid to be direct child of root element");
                }

                if (id != null)
                {
                    if (_elementIds.ContainsKey(id))
                        throw new ConfigParserException("Duplicate element with the same id="+id);
                    _elementIds[id] = elm;
                }
            }

            ParseTypes();
            ParseDelegates();
            ParseBitContents();
            ParseTlvs();
            ParseMessages();
            ParseModels();
            ParseMessageToModels();
        }

        private static String GetRequiredAttribute(XmlElement elm, String attrName, String elmId)
        {
            String attrVal = elm.GetAttribute(attrName);
            if (attrVal != null) attrVal = attrVal.Trim();
            if (String.IsNullOrEmpty(attrVal))
                throw new ConfigParserException("There is a " + elm.Name + " element "
                    + (elmId != null ? "(id=" + elmId + ")" : "")
                    + " that doesn't have " + attrName + " or specifies blank value.");
            return attrVal;
        }

        private static int GetNonNegativeIntValue(String str, String attrName, String elmName)
        {
            int integer = 0;
            bool isExc = false;
            try
            {
                integer = int.Parse(str);
            }
            catch
            {
                isExc = true;
            }
            if (integer < 0 || isExc)
            {
                throw new ConfigParserException(attrName + " value of " + elmName + " element must be non-negative integer value.");
            }
            return integer;
        }

        private static int GetNonNegativeIntValue(XmlElement elm, String attrName, int? defaultVal)
        {
            XmlAttribute attr = elm.GetAttributeNode(attrName);
            if (attr == null)
            {
                if (defaultVal != null) return (int)defaultVal;
                return -1;
            }
            else
            {
                return GetNonNegativeIntValue(attr.Value, attrName, elm.Name);
            }
        }

        private static bool GetBoolValue(XmlElement elm, String attrName, bool defaultVal)
        {
            bool isTrue = false;
            XmlAttribute attr = elm.GetAttributeNode(attrName);
            if (attr != null)
            {
                String val = attr.Value;
                if (val != "true" && val != "false")
                {
                    throw new ConfigParserException(attrName + " value of " + elm.Name + " element must be 'true' or 'false'.");
                }
                isTrue = val.Equals("true");
            }
            else
            {
                isTrue = defaultVal;
            }
            return isTrue;
        }

        private static String GetStringValue(XmlElement elm, String attrName, String defaultVal)
        {
            XmlAttribute attr = elm.GetAttributeNode(attrName);
            if (attr != null)
            {
                return attr.Value;
            }
            else
            {
                return defaultVal;
            }
        }

        private void ParseTypes()
        {
            foreach (XmlElement elm in _types)
            {
                String id = GetRequiredAttribute(elm, "id", null);
                String className = GetRequiredAttribute(elm, "class", id);
                Type classType = Type.GetType(className);
                if (MessageConfigs.Types.ContainsKey(id))
                    throw new ConfigParserException("There are more than one type element whose id "+id);
                MessageConfigs.Types.Add(id, classType);
            }
        }

        private static Type GetTypeFromName(String name)
        {
            if (MessageConfigs.Types.ContainsKey(name)) return MessageConfigs.Types[name];
            Type type = Type.GetType(name, true);
            if (type == null)
            {
                throw new ConfigParserException("There is no type named '"+name+"'");
            }
            return type;
        }

        private static PropertyInfo GetProperty(Type type, String propName, String locMsg)
        {
            PropertyInfo prop = type.GetProperty(propName);
            if (prop == null)
                throw new ConfigParserException("The type " + type.FullName + " doesn't have public non-static property named "
                    + propName + ". " + (locMsg==null ? "" : locMsg));
            return prop;
        }

        private void ParseDelegates()
        {
            foreach (XmlElement elm in _delegates)
            {
                String id = GetRequiredAttribute(elm, "id", null);

                String className = GetRequiredAttribute(elm, "class", id);
                Type classType = GetTypeFromName(className);

                Type paramType = null;
                String paramName = elm.GetAttribute("param-type");
                if (!String.IsNullOrEmpty(paramName)) paramType = GetTypeFromName(paramName);

                String methodName = GetRequiredAttribute(elm, "method", id);
                MethodInfo method = paramType == null ? classType.GetMethod(methodName, Type.EmptyTypes)
                    : classType.GetMethod(methodName, new Type[] { paramType });

                Object classInstance = null;
                if (!method.IsStatic) classInstance = GetInstanceOf(classType);
                
                if (MessageConfigs.HeaderDelegates.ContainsKey(id)
                        || MessageConfigs.FieldDelegates.ContainsKey(id)
                        || MessageConfigs.PropertyDelegates.ContainsKey(id)
                        || MessageConfigs.ProcessDelegates.ContainsKey(id)
                )
                    throw new ConfigParserException("There are more than one delegate element whose id '" + id + "'.");

                Type delegateType = null;
                IDictionary<object, Delegate> delegates = null;
                if (paramType == null && method.ReturnType == typeof(byte[]))
                {
                    delegateType = typeof(GetHeaderBytes);
                    delegates = MessageConfigs.HeaderDelegates;
                }
                else if (paramType != null && method.ReturnType == typeof(byte[]))
                {
                    delegateType = typeof(GetFieldBytes<>).MakeGenericType(paramType);
                    delegates = MessageConfigs.FieldDelegates;
                }
                else if (paramType == typeof(byte[]) && method.ReturnType != typeof(void))
                {
                    delegateType = typeof(GetPropertyValue<>).MakeGenericType(method.ReturnType);
                    delegates = MessageConfigs.PropertyDelegates;
                }
                else if (paramType != null && !paramType.IsArray && method.ReturnType != typeof(void) && !method.ReturnType.IsArray)
                {
                    delegateType = typeof(ProcessModel<,>).MakeGenericType(method.ReturnType, paramType);
                    delegates = MessageConfigs.ProcessDelegates;
                }

                if (delegateType == null)
                {
                    throw new ConfigParserException("The delegate (id=" + id + ") has invalid signature.");
                }
                else
                {
                    delegates[id] = Delegate.CreateDelegate(delegateType, classInstance, method);
                }
            }
        }

        private void ParseBitContents()
        {
            foreach (XmlElement elm in _bitContents)
            {
                String id = GetRequiredAttribute(elm, "id", null);
                String className = GetRequiredAttribute(elm, "class", id);
                Type classType = GetTypeFromName(className);

                BitContentConfig cfg = new BitContentConfig();
                cfg.Id = id;
                cfg.ClassType = classType;
                ParseBitContentFields(elm, cfg);

                if (MessageConfigs.BitContents.ContainsKey(id))
                    throw new ConfigParserException("There are more than one BitContent element whose id " + id);
                MessageConfigs.BitContents.Add(id, cfg);
            }
        }

        private void ParseBitContentFields(XmlElement elm, BitContentConfig cfg)
        {
            IList<XmlElement> fields = new List<XmlElement>();
            foreach (XmlNode node in elm.ChildNodes)
            {
                if (node.NodeType != XmlNodeType.Element) continue;
                if (node.Name != "field")
                    throw new ConfigParserException("There is unknown tag " + node.Name
                        + " inside BitContent element (id=" + cfg.Id + ")");
                fields.Add((XmlElement)node);
            }
            int fieldCount = fields.Count;
            for (int i=0; i<fieldCount; i++)
            {
                BitContentFieldConfig ccfg = new BitContentFieldConfig();
                
                XmlElement field = fields[i];
                String name = GetRequiredAttribute(field, "name", null);
                int length = GetNonNegativeIntValue(field, "length", null); //GetRequiredAttribute(field, "length", null);
                String pad = GetStringValue(field, "pad", " ");
                String align = GetStringValue(field, "align", "left");
                String nullCh = GetStringValue(field, "null", " ");
                String optional = field.GetAttribute("optional");
                String tlvId = field.GetAttribute("tlv");
                String tlvTagName = field.GetAttribute("tlv-tag-name");
                tlvTagName = String.IsNullOrEmpty(tlvTagName) ? "" : tlvTagName.Trim().ToUpper();
                String tlvClassName = GetStringValue(field, "tlv-class", "").Trim();
                Type tlvClassType = String.IsNullOrEmpty(tlvClassName) ? null : GetTypeFromName(tlvClassName);

                String commonMsg = "Check BitContent element (id="+cfg.Id+") and field named " + name;
                PropertyInfo propInfo = GetProperty(cfg.ClassType, name, commonMsg);
                
                if (length <= 0 && String.IsNullOrEmpty(tlvId) && String.IsNullOrEmpty(tlvTagName))
                {
                    throw new ConfigParserException("length value must be set to a positive value if tlv and tlv-tag-name "
                        + "are not specified. " + commonMsg);
                }

                if (pad.Length != 1 || Encoding.UTF8.GetByteCount(pad) != 1)
                    throw new ConfigParserException("pad value of field element must be one character and valid ASCII. "
                        + commonMsg);
                
                if (align != "left" && align != "right")
                {
                    throw new ConfigParserException("align value of field element must be 'left' or 'right'. "
                        + commonMsg);
                }

                if (nullCh.Length != 1 || Encoding.UTF8.GetByteCount(nullCh) != 1)
                    throw new ConfigParserException("null value of field element must be one character and valid ASCII. "
                        + commonMsg);
                
                bool isOptional = false;
                if (!String.IsNullOrEmpty(optional))
                {
                    isOptional = GetBoolValue(field, "optional", false);
                    if (isOptional && i < fieldCount - 1)
                    {
                        throw new ConfigParserException("optional attribute is only available for the last field element inside "
                            + "a BitContent elemet. Check BitContent element (id="+cfg.Id+").");
                    }
                }

                if (!String.IsNullOrEmpty(tlvId))
                {
                    if (!_tlvIds.Contains(tlvId))
                    {
                        throw new ConfigParserException("There is no tlv (id=" + tlvId + "). " + commonMsg);
                    }
                    else
                    {
                        _bitContentFieldsReferTlv.Add(ccfg);
                        _referredTlvIds.Add(tlvId);
                    }
                }

                ccfg.PropertyInfo = propInfo;
                ccfg.Length = length;
                ccfg.PadChar = MessageUtility.CharToByte(pad.ToCharArray()[0]);
                ccfg.Align = align;
                ccfg.NullChar = MessageUtility.CharToByte(nullCh.ToCharArray()[0]);
                ccfg.IsTrim = GetBoolValue(field, "trim", true);
                ccfg.IsOptional = isOptional;
                ccfg.Tlv = null;
                ccfg.TlvTagName = tlvTagName;
                ccfg.TlvLengthBytes = GetNonNegativeIntValue(field, "tlv-length-bytes", 3);
                ccfg.TlvClassType = tlvClassType;
                cfg.Fields.Add(ccfg);
            }
        }

        private void ParseTlvs()
        {
            foreach (XmlElement elm in _tlvs)
            {
                String id = GetRequiredAttribute(elm, "id", null);
                int lengthBytes = GetNonNegativeIntValue(elm, "length-bytes", null);
                String className = GetStringValue(elm, "class", "").Trim();
                Type classType = String.IsNullOrEmpty(className) ? null : GetTypeFromName(className);

                TlvConfig cfg = new TlvConfig();
                cfg.Id = id;
                cfg.LengthBytes = lengthBytes;
                cfg.ClassType = classType;
                ParseTlvTags(elm, cfg);

                if (MessageConfigs.Tlvs.ContainsKey(id))
                    throw new ConfigParserException("There are more than one tlv element whose id " + id);
                MessageConfigs.Tlvs.Add(id, cfg);
            }

            SetTlvForBitContentFields();
        }

        private void ParseTlvTags(XmlElement elm, TlvConfig cfg)
        {
            IList<XmlElement> tags = new List<XmlElement>();
            foreach (XmlNode node in elm.ChildNodes)
            {
                if (node.NodeType != XmlNodeType.Element) continue;
                if (node.Name != "tag")
                    throw new ConfigParserException("There is unknown tag " + node.Name
                        + " inside tlv element (id=" + cfg.Id + ")");
                tags.Add((XmlElement)node);
            }

            int fieldCount = tags.Count;
            int tagLength = -1;
            for (int i = 0; i < fieldCount; i++)
            {
                XmlElement tag = tags[i];
                String name = GetRequiredAttribute(tag, "name", null).ToUpper();
                if (tagLength <= 0) tagLength = name.Length;
                if (tagLength != name.Length)
                {
                    throw new ConfigParserException("Not match length of tag for tag " + name
                        + " inside tlv element (id=" + cfg.Id + ")");
                }
                String type = tag.GetAttribute("type");
                if (type != null) type = type.Trim().ToLower();
                String splitter = tag.GetAttribute("splitter");
                if (type == "array")
                {
                    if (splitter != null) splitter = splitter.Trim();
                    if (String.IsNullOrEmpty(splitter)) splitter = ";";
                }
                else
                {
                    splitter = null;
                }
                
                String bitContentId = tag.GetAttribute("bitcontent");
                BitContentConfig bitContent = null;
                if (type == "bitcontent")
                {
                    if (String.IsNullOrEmpty(bitContentId))
                    {
                        throw new ConfigParserException(
                            "bitcontent attribute of tag element must be defined if type is 'bitcontent'. Check tag element (name="
                            + name + ") inside tlv element (id=" + cfg.Id + ")");
                    }
                    else if (!MessageConfigs.BitContents.ContainsKey(bitContentId))
                    {
                        throw new ConfigParserException("There is no BitContent element (id=" + bitContentId
                            + ") referred by tag element (name=" + name + ") inside tlv element (id=" + cfg.Id + ")");
                    }
                    bitContent = MessageConfigs.BitContents[bitContentId];
                }

                TlvTagConfig ccfg = new TlvTagConfig();
                ccfg.Name = name;
                ccfg.Splitter = splitter;
                ccfg.BitContent = bitContent;
                cfg.AddTag(ccfg);
            }
        }

        private void SetTlvForBitContentFields()
        {
            int count = _bitContentFieldsReferTlv.Count;
            for (int i=0; i<count; i++)
            {
                _bitContentFieldsReferTlv[i].Tlv = MessageConfigs.Tlvs[_referredTlvIds[i]];
            }
        }

        private void ParseMessages()
        {
            foreach (XmlElement elm in _messages)
            {
                String id = GetRequiredAttribute(elm, "id", null);
                String lengthHdr = GetRequiredAttribute(elm, "length-header", null);
                int lenHdr = GetNonNegativeIntValue(lengthHdr, "length-header", "message");

                MessageConfig cfg = new MessageConfig();
                cfg.Id = id;
                cfg.LengthHeader = lenHdr;

                IList<XmlElement> headers = new List<XmlElement>();
                IList<int> headerType = new List<int>();
                IList<XmlElement> fields = new List<XmlElement>();
                foreach (XmlNode node in elm.ChildNodes)
                {
                    if (node.NodeType != XmlNodeType.Element) continue;
                    switch (node.Name)
                    {
                        case "header":
                            headers.Add((XmlElement)node);
                            headerType.Add(0);
                            break;
                        case "message-type":
                            headers.Add((XmlElement)node);
                            headerType.Add(1);
                            break;
                        case "bitmap":
                            headers.Add((XmlElement)node);
                            headerType.Add(2);
                            break;
                        case "bit":
                            fields.Add((XmlElement)node);
                            break;
                        default:
                            throw new ConfigParserException("Unknown tag " + node.Name + " inside message element (id=" + id + ").");
                    }
                }

                ParseMessageHeaders(headers, headerType, cfg);
                ParseMessageFields(fields, cfg);

                if (MessageConfigs.Messages.ContainsKey(id))
                    throw new ConfigParserException("There are more than one message element whose id " + id);
                MessageConfigs.Messages.Add(id, cfg);
            }
        }

        private void ParseMessageHeaders(IList<XmlElement> headers, IList<int> headerType, MessageConfig cfg)
        {
            for (int i = 0; i < headers.Count; i++)
            {
                XmlElement elm = headers.ElementAt(i);
                IMessageHeaderConfig ccfg;
                if (headerType.ElementAt(i) == 2)
                {
                    String length = GetRequiredAttribute(elm, "length", null);
                    int len = GetNonNegativeIntValue(length, "length", elm.Name);
                    MessageBitMapConfig ccfg2 = new MessageBitMapConfig();
                    ccfg2.Length = len;
                    ccfg = ccfg2;
                }
                else if (headerType.ElementAt(i) == 1)
                {
                    String length = GetRequiredAttribute(elm, "length", null);
                    int len = GetNonNegativeIntValue(length, "length", elm.Name);
                    String value = GetRequiredAttribute(elm, "value", null);
                    byte[] bytesVal = MessageUtility.StringToHex(value);
                    if (bytesVal == null)
                        throw new ConfigParserException("Invalid value for " + elm.Name + " element inside message element (id="
                            + cfg.Id + ")");
                    if (bytesVal.Length != len)
                        throw new ConfigParserException("Incompatible length of value attribute with defined one by length "
                            + "attribute inside message element (id=" + cfg.Id + ")");
                    MessageTypeConfig ccfg2 = new MessageTypeConfig();
                    ccfg2.Value = bytesVal;
                    ccfg2.Length = len;
                    ccfg = ccfg2;
                }
                else
                {
                    String name = elm.GetAttribute("name");
                    if (name == null) name = "";
                    String length = GetRequiredAttribute(elm, "length", null);
                    int len = GetNonNegativeIntValue(length, "length", elm.Name);

                    String value = elm.GetAttribute("value");
                    String delId = elm.GetAttribute("delegate");
                    if (String.IsNullOrEmpty(value) && String.IsNullOrEmpty(delId))
                    {
                        throw new ConfigParserException("Either value or delegate attribute of header element must be defined.");
                    }

                    MessageHeaderConfig ccfg2 = new MessageHeaderConfig();
                    ccfg2.Name = name;
                    ccfg2.Length = len;

                    if (!String.IsNullOrEmpty(delId))
                    {
                        if (!MessageConfigs.HeaderDelegates.ContainsKey(delId))
                        {
                            throw new ConfigParserException("The delegate (id=" + delId + ") referred by header element (name="
                                + name + ") was not found or its signature is invalid.");
                        }
                        ccfg2.GetFieldBytesFunc = (GetHeaderBytes)MessageConfigs.HeaderDelegates[delId];
                    }
                    else
                    {
                        ccfg2.StringValue = value;
                    }

                    ccfg = ccfg2;
                }

                cfg.Headers.Add(ccfg);
            }
        }

        private void ParseMessageFields(IList<XmlElement> fields, MessageConfig cfg)
        {
            IDictionary<int, MessageFieldConfig> flds = new Dictionary<int, MessageFieldConfig>();
            foreach (XmlElement elm in fields)
            {
                String seq = GetRequiredAttribute(elm, "seq", null);
                int iSeq = GetNonNegativeIntValue(seq, "seq", elm.Name);

                String type = GetRequiredAttribute(elm, "type", null).ToUpper();
                Type fieldType;
                switch (type)
                {
                    case "B": fieldType = typeof(MessageField);  break;
                    case "N": fieldType = typeof(NMessageField); break;
                    case "NS": fieldType = typeof(NsMessageField); break;
                    case "AN": fieldType = typeof(AnMessageField); break;
                    case "ANS": fieldType = typeof(AnsMessageField); break;
                    case "NULL": fieldType = typeof(NullMessageField); break;
                    case "BITMAP": fieldType = typeof(MessageBitMap); break;
                    default:
                        throw new ConfigParserException("Invalid value of type attribute of bit element (seq=" + seq
                            + ") inside message element (id=" + cfg.Id + ")");
                }

                int len = 0, lenHdr = -1;
                bool fromRequest = false;
                String delId = null;
                if (type == "NULL")
                {
                    if (elm.GetAttributeNode("length") != null || elm.GetAttributeNode("length-header") != null
                        || elm.GetAttributeNode("from-request") != null || elm.GetAttributeNode("delegate") != null)
                    {
                        throw new ConfigParserException("If type is 'NULL' then only seq and type attribute are valid. " +
                            "Check bit element (seq=" + iSeq+ ") inside message element (id=" + cfg.Id + ").");
                    }
                }
                else if (type == "BITMAP")
                {
                    if (elm.GetAttributeNode("length-header") != null || elm.GetAttributeNode("from-request") != null
                        || elm.GetAttributeNode("delegate") != null)
                    {
                        throw new ConfigParserException("If type is 'BitMap' then only seq, type and length attribute are valid. "
                            + "Check bit element (seq=" + iSeq + ") inside message element (id=" + cfg.Id + ").");
                    }

                    XmlAttribute length = elm.GetAttributeNode("length");
                    if (length != null)
                    {
                        len = GetNonNegativeIntValue(length.Value, "length", elm.Name);
                    }
                    if (length == null || len == 0)
                    {
                        throw new ConfigParserException(
                            "If type is 'BitMap' then length attribute must be defined and greater than 0. "
                            + "Check bit element (seq=" + iSeq + ") inside message element (id=" + cfg.Id + ").");
                    }
                }
                else
                {
                    XmlAttribute length = elm.GetAttributeNode("length");
                    XmlAttribute lengthHeader = elm.GetAttributeNode("length-header");
                    if (!(length != null ^ lengthHeader != null))
                    {
                        throw new ConfigParserException("Either length or length-header attribute must be defined but NOT BOTH. "
                            + "Check bit element (seq=" + seq + ") inside message element (id=" + cfg.Id + ")");
                    }
                    if (length != null)
                    {
                        len = GetNonNegativeIntValue(length.Value, "length", elm.Name);
                        lenHdr = -1;
                    }
                    else
                    {
                        len = -1;
                        lenHdr = GetNonNegativeIntValue(lengthHeader.Value, "length-header", elm.Name);
                    }

                    fromRequest = GetBoolValue(elm, "from-request", false);

                    delId = elm.GetAttribute("delegate");
                    if (!String.IsNullOrEmpty(delId) && !MessageConfigs.FieldDelegates.ContainsKey(delId))
                    {
                        throw new ConfigParserException("The delegate (id=" + delId + ") referred by bit element (seq=" + iSeq
                            + ") inside message element (id=" + cfg.Id + ") was not found or its signature is invalid.");
                    }
                }
                
                MessageFieldConfig ccfg = new MessageFieldConfig();
                ccfg.Seq = iSeq;
                ccfg.FieldType = fieldType;
                ccfg.Length = len;
                ccfg.LengthHeader = lenHdr;
                ccfg.FromRequest = fromRequest;
                ccfg.GetFieldBytesFunc = String.IsNullOrEmpty(delId) ? null : MessageConfigs.FieldDelegates[delId];
                if (flds.ContainsKey(iSeq))
                    throw new ConfigParserException("There are more than one message field (bit element) inside message element (id="
                        + cfg.Id + ") whose seq attribute " + iSeq);
                flds.Add(iSeq, ccfg);
            }

            /*** Sorts the fields based on their sequence number. It ensures we get the field
             *   from the lowest sequence until the highest one consecutively when they are iterated. ***/
            List<int> keys = flds.Keys.ToList();
            keys.Sort();
            for (int i = 0; i < keys.Count; i++)
            {
                int iSeq = keys[i];
                cfg.Fields.Add(iSeq, flds[iSeq]);
            }
        }

        private void ParseModels()
        {
            foreach (XmlElement elm in _models)
            {
                String extend = elm.GetAttribute("extend");
                if (!String.IsNullOrEmpty(extend)) //It's a child model, defers to create ModelConfig
                {
                    String id = elm.GetAttribute("id");
                    if (!_modelIds.Contains(extend))
                    {
                        throw new ConfigParserException("model (id=" + extend + ") extended by model element (id=" + id
                            + ", class=" + elm.GetAttribute("class") + ") doesn't exist");
                    }
                    String genId = id;
                    if (String.IsNullOrEmpty(genId))
                    {
                        genId = extend + "Child";
                        int i = 0;
                        while (GetElementById(genId + ++i) != null || _childModels.ContainsKey(genId + i)) { }
                        genId += i;
                    }
                    _childModels[genId] = elm;
                }
                else
                {
                    ModelConfig cfg = ParseModel(elm);
                    ParseModelProperties(elm, cfg, false);
                    RegisterModel(cfg);
                }
            }

            ParseChildModels();
        }

        private void ParseChildModels()
        {
            while (_childModels.Count > 0)
            {
                String id = _childModels.First().Key;
                String lastId = id;
                String locMsg = "Check model (id=" + id + ") and its chain for model extension (check extend attribute).";

                HashSet<String> chainIds = new HashSet<String>();
                while (!MessageConfigs.Models.ContainsKey(id)) //as long as it's not root model
                {
                    chainIds.Add(id);
                    lastId = id;
                    id = _childModels[id].GetAttribute("extend");
                    if (chainIds.Contains(id))
                    {
                        throw new ConfigParserException("Circular reference. " + locMsg);
                    }
                }
                
                ModelConfig cfg = MessageConfigs.Models[id].Clone();
                XmlElement elm = _childModels[lastId];
                ModelConfig newCfg = ParseModel(elm);
                cfg.Id = newCfg.Id;
                Type oldClassType = cfg.ClassType;
                cfg.ClassType = (newCfg.ClassType != null ? newCfg.ClassType : cfg.ClassType);
                MessageConfig oldMsgCfg = cfg.MessageCfg;
                cfg.MessageCfg = (newCfg.MessageCfg != null ? newCfg.MessageCfg : cfg.MessageCfg);
                ParseModelProperties(elm, cfg, true);

                if (!oldClassType.Equals(newCfg.ClassType))
                {
                    foreach (KeyValuePair<int, ModelPropertyConfig> propCfg in cfg.Properties)
                    {
                        propCfg.Value.PropertyInfo = GetProperty(cfg.ClassType, propCfg.Value.PropertyInfo.Name, locMsg); //Change PropertyInfo based on the new ClassType
                    }
                }

                if (newCfg.MessageCfg != null && oldMsgCfg.Id != newCfg.MessageCfg.Id)
                {
                    IDictionary<int, ModelPropertyConfig> propCfgs = new Dictionary<int, ModelPropertyConfig>();
                    IDictionary<int, String> types = new Dictionary<int, String>();
                    foreach (KeyValuePair<int, ModelPropertyConfig> propCfg in cfg.Properties)
                    {
                        propCfg.Value.PropertyInfo = GetProperty(cfg.ClassType, propCfg.Value.PropertyInfo.Name, locMsg); //Change PropertyInfo based on the new ClassType
                        propCfgs.Add(propCfg.Value.FieldBit.Seq, propCfg.Value);
                        if (propCfg.Value.GetValueFromBytes == null) types.Add(propCfg.Value.FieldBit.Seq, null);
                        else
                            switch (propCfg.Value.GetValueFromBytes.Name)
                            {
                                case "StringValue": types.Add(propCfg.Value.FieldBit.Seq, "string"); break;
                                case "IntValue": types.Add(propCfg.Value.FieldBit.Seq, "int"); break;
                                case "DecimalValue": types.Add(propCfg.Value.FieldBit.Seq, "decimal"); break;
                                case "BytesValue": types.Add(propCfg.Value.FieldBit.Seq, "bytes"); break;
                            }
                    }
                    ParseMappedMessageField(propCfgs, types, true, cfg); //Change mapped message field based on the new mapped message for this model
                }

                RegisterModel(cfg);
                _childModels.Remove(lastId);
            }
        }

        private ModelConfig ParseModel(XmlElement elm)
        {
            bool isChild = !String.IsNullOrEmpty(elm.GetAttribute("extend"));
            String id = elm.GetAttribute("id");
            String className = isChild ? elm.GetAttribute("class") : GetRequiredAttribute(elm, "class", id == "" ? null : id);
            String msgId = isChild ? elm.GetAttribute("message") : GetRequiredAttribute(elm, "message", id == "" ? null : id);
            Type classType = String.IsNullOrEmpty(className) ? null : GetTypeFromName(className); //className is empty only if isChild==true

            if (!String.IsNullOrEmpty(msgId) && !MessageConfigs.Messages.ContainsKey(msgId))
            {
                throw new ConfigParserException("message id=" + msgId + " referred by model element (id=" + id
                    + ", class=" + (className==null?"":className) + ") doesn't exist");
            }

            ModelConfig cfg = new ModelConfig();
            cfg.Id = (id == "" ? null : id);
            cfg.ClassType = classType;
            cfg.MessageCfg = String.IsNullOrEmpty(msgId) ? null : MessageConfigs.Messages[msgId];
            return cfg;
        }


        private void RegisterModel(ModelConfig cfg)
        {
            if (!MessageConfigs.ClassToModels.ContainsKey(cfg.ClassType)) MessageConfigs.ClassToModels[cfg.ClassType] = cfg; //Child model whose the same type as parent's will be ignored
            if (!String.IsNullOrEmpty(cfg.Id)) MessageConfigs.Models[cfg.Id] = cfg;
        }

        private void ParseModelProperties(XmlElement elm, ModelConfig cfg, bool subtitute)
        {
            String modelLocMsg = "model element (id=" + (cfg.Id == null ? "" : cfg.Id)
                + ", class=" + (cfg.ClassType == null ? "" : cfg.ClassType.FullName) + ")";
            IList<XmlElement> props = new List<XmlElement>();
            foreach (XmlNode node in elm.ChildNodes)
            {
                if (node.NodeType != XmlNodeType.Element) continue;
                if (node.Name != "property")
                    throw new ConfigParserException("Unknown tag " + node.Name + " inside " + modelLocMsg);
                props.Add((XmlElement)node);
            }

            String locMsg = "Check " + modelLocMsg;
            IDictionary<int, ModelPropertyConfig> propCfgs = new Dictionary<int, ModelPropertyConfig>();
            IDictionary<int, String> types = new Dictionary<int, String>();
            foreach (XmlElement property in props)
            {
                String name = GetRequiredAttribute(property, "name", null);
                PropertyInfo propInfo = GetProperty(cfg.ClassType, name, locMsg);

                String bit = GetRequiredAttribute(property, "bit", null);
                int iBit = GetNonNegativeIntValue(bit, "bit", property.Name);
                
                String type = property.GetAttribute("type");
                types.Add(iBit, type);
                
                String delId = property.GetAttribute("delegate");
                String bitcontentId = property.GetAttribute("bitcontent");
                String tlvId = property.GetAttribute("tlv");
                String tlvTagName = property.GetAttribute("tlv-tag-name");
                int iTlvLengthBytes = GetNonNegativeIntValue(property, "tlv-length-bytes", 3);
                String tlvClassName = GetStringValue(property, "tlv-class", "").Trim();
                Type tlvClassType = String.IsNullOrEmpty(tlvClassName) ? null : GetTypeFromName(tlvClassName);
                
                if (String.IsNullOrEmpty(type) && String.IsNullOrEmpty(delId) && String.IsNullOrEmpty(bitcontentId)
                        && String.IsNullOrEmpty(tlvId) && String.IsNullOrEmpty(tlvTagName))
                {
                    throw new ConfigParserException("Either type or delegate or bitcontent or tlv or tlv-tag-name "
                        + "attribute must be defined. Check property element (name=" + name + ") inside " + modelLocMsg);
                }
                if (!String.IsNullOrEmpty(bitcontentId) && !MessageConfigs.BitContents.ContainsKey(bitcontentId))
                {
                    throw new ConfigParserException("There is no BitContent (id=" + bitcontentId
                        + ") referred by property element (name=" + name + ") inside " + modelLocMsg);
                }
                if (!String.IsNullOrEmpty(tlvId) && !MessageConfigs.Tlvs.ContainsKey(tlvId))
                {
                    throw new ConfigParserException("There is no tlv (id=" + tlvId
                        + ") referred by property element (name=" + name + ") inside " + modelLocMsg);
                }
                if (!String.IsNullOrEmpty(delId) && !MessageConfigs.PropertyDelegates.ContainsKey(delId))
                {
                    throw new ConfigParserException("The delegate referred by property element (name=" + name
                        + ") inside " + modelLocMsg + " is not found or its signature is invalid.");
                }

                if (!String.IsNullOrEmpty(type) && !_propertyTypes.Contains(type))
                {
                    throw new ConfigParserException("Invalid type value of property element (name=" + name
                        + ") inside " + modelLocMsg + " is not found or its signature is invalid.");
                }

                int iFracDigits = 0;
                String fracDigits = property.GetAttribute("frac-digits");
                if (!String.IsNullOrEmpty(fracDigits)) iFracDigits = GetNonNegativeIntValue(fracDigits, "frac-digits", property.Name);

                ModelPropertyConfig ccfg = new ModelPropertyConfig();
                ccfg.PropertyInfo = propInfo;
                ccfg.FracDigits = iFracDigits;
                ccfg.GetPropertyValueFunc = String.IsNullOrEmpty(delId) ? null : MessageConfigs.PropertyDelegates[delId];
                ccfg.BitContent = String.IsNullOrEmpty(bitcontentId) ? null : MessageConfigs.BitContents[bitcontentId];
                ccfg.Tlv = String.IsNullOrEmpty(tlvId) ? null : MessageConfigs.Tlvs[tlvId];
                ccfg.TlvTagName = String.IsNullOrEmpty(tlvTagName) ? "" : tlvTagName.Trim().ToUpper();
                ccfg.TlvLengthBytes = iTlvLengthBytes;
                ccfg.TlvClassType = tlvClassType;

                propCfgs.Add(iBit, ccfg);
            }
            ParseMappedMessageField(propCfgs, types, subtitute, cfg);
        }

        private void ParseMappedMessageField(IDictionary<int, ModelPropertyConfig> propCfgs, IDictionary<int, String> types,
            bool subtitute, ModelConfig cfg)
        {
            String modelLocMsg = "model element (id=" + (cfg.Id ?? "")
                + ", class=" + (cfg.ClassType == null ? "" : cfg.ClassType.FullName) + ")";
            foreach (KeyValuePair<int, ModelPropertyConfig> propCfg in propCfgs)
            {
                int iBit = propCfg.Key;
                ModelPropertyConfig ccfg = propCfg.Value;
                if (!cfg.MessageCfg.Fields.ContainsKey(iBit))
                    throw new ConfigParserException("message (id=" + cfg.MessageCfg.Id + ") doesn't have bit #" + iBit
                        + ". It's referred by property element (name=" + ccfg.PropertyInfo.Name + ") inside " + modelLocMsg);
                MessageFieldConfig fieldBit = cfg.MessageCfg.Fields[iBit];
                if (fieldBit.FieldType == typeof(MessageBitMap))
                {
                    throw new ConfigParserException("Cannot map a bit element whose type='BitMap' to a model property. "
                        + "It's referred by property element (name=" + ccfg.PropertyInfo.Name + ") inside " + modelLocMsg);
                }

                //if delegate attribute is not ignored (because bitcontent, tlv, tlv-tag-name attribute are not specified) then
                //the mapped field element must specify delegate attribute
                if (ccfg.BitContent == null && ccfg.Tlv == null && String.IsNullOrEmpty(ccfg.TlvTagName)
                    && ccfg.GetPropertyValueFunc != null && fieldBit.GetFieldBytesFunc == null)
                {
                    throw new ConfigParserException("The property element (name=" + ccfg.PropertyInfo.Name
                        + ") inside " + modelLocMsg + " defines a delegate but its mapped message field doesn't.");
                }

                HookPropertyToField(ccfg, fieldBit, types[iBit]);

                if (subtitute)
                    cfg.AddOrSubtituteProperty(ccfg);
                else
                    cfg.Properties.Add(iBit, ccfg);
            }
        }

        private void ParseMessageToModels()
        {
            foreach (XmlElement elm in _messageToModels)
            {
                MessageToModelConfig cfg = new MessageToModelConfig();

                String model = GetRequiredAttribute(elm, "model", null);
                if (!MessageConfigs.Models.ContainsKey(model))
                    throw new ConfigParserException("No model whose id=" + model + ". It's referred by a message-to-model element.");
                cfg.ModelCfg = MessageConfigs.Models[model];

                String delId = elm.GetAttribute("delegate");
                if (String.IsNullOrEmpty(delId))
                {
                    cfg.ProcessModel = null;
                }
                else
                {
                    if (!MessageConfigs.ProcessDelegates.ContainsKey(delId))
                        throw new ConfigParserException("No delegate whose id=" + delId
                            + " or its signature is invalid. It's referred by a message-to-model element.");
                    cfg.ProcessModel = MessageConfigs.ProcessDelegates[delId];
                }

                ParseMasks(elm, cfg, model, delId);

                MessageConfigs.MessageToModels.Add(cfg);
            }
        }

        private void ParseMasks(XmlElement elm, IMaskListConfig cfg, String modelId, String delId)
        {
            IList<XmlElement> masks = new List<XmlElement>();
            foreach (XmlNode node in elm.ChildNodes)
            {
                if (node.NodeType != XmlNodeType.Element) continue;
                if (node.Name == "mask" || node.Name == "and" || node.Name == "or")
                    masks.Add((XmlElement)node);
                else
                    throw new ConfigParserException("Unknown tag " + node.Name + " inside message-to-model element (model id="
                        + modelId + ", delegate id=" + delId + ")");
            }

            foreach (XmlElement mask in masks)
            {
                if (mask.Name == "mask")
                {
                    MaskConfig ccfg = new MaskConfig();
                    String startByte = GetRequiredAttribute(mask, "start-byte", null);
                    ccfg.StartByte = GetNonNegativeIntValue(startByte, "start-byte", mask.Name);
                    String length = GetRequiredAttribute(mask, "length", null);
                    ccfg.Length = GetNonNegativeIntValue(length, "length", mask.Name);

                    bool isValueExist = mask.GetAttributeNode("value") != null;
                    bool isMaskExist = mask.GetAttributeNode("mask") != null;
                    if (!(isValueExist ^ isMaskExist))
                    {
                        throw new ConfigParserException("Either value or mask attribute must be defined but not both. "
                            + "Check mask element inside message-to-model element (model id=" + modelId
                            + ", delegate id=" + delId + ")");
                    }
                    String value = mask.GetAttribute("value");
                    String maskVal = mask.GetAttribute("mask");
                    String maskResult = mask.GetAttribute("result");
                    if (isValueExist)
                    {
                        ccfg.SetValue(value, modelId, delId);
                    }
                    else
                    {
                        ccfg.SetValue(maskVal, modelId, delId);
                        if (String.IsNullOrEmpty(maskResult))
                        {
                            throw new ConfigParserException("If mask attribute is defined then result attribute must be defined. "
                                + "Check mask element inside message-to-model element (model id=" + modelId
                                + ", delegate id=" + delId + ")");
                        }
                        if (maskResult != "equals" && maskResult != "notEquals" && maskResult != "zero" && maskResult != "notZero")
                        {
                            throw new ConfigParserException(
                                "The result attribute must be 'equals' or 'notEquals' or 'zero' or 'notZero'. "
                                + "Check mask element inside message-to-model element (model id=" + modelId
                                + ", delegate id=" + delId + ")");
                        }
                        ccfg.MaskResult = maskResult;
                    }
                    
                    cfg.MaskList.Add(ccfg);
                }
                else
                {
                    if (mask.Name == "or")
                    {
                        MaskOrConfig ccfg = new MaskOrConfig();
                        cfg.MaskList.Add(ccfg);
                        ParseMasks(mask, ccfg, modelId, delId);
                    }
                    else //if (mask.Name == "and")
                    {
                        MaskAndConfig ccfg = new MaskAndConfig();
                        cfg.MaskList.Add(ccfg);
                        ParseMasks(mask, ccfg, modelId, delId);
                    }
                }
            }
        }
    }
}
