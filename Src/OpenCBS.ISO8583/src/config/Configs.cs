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

namespace Free.iso8583.config
{
    public class BitContentFieldConfig
    {
        public PropertyInfo PropertyInfo { get; set; } //Name
        public int Length { get; set; }
        public byte PadChar { get; set; }
        public String Align { get; set; }
        public byte NullChar { get; set; }
        public bool IsTrim { get; set; }
        public bool IsOptional { get; set; }
        public TlvConfig Tlv { get; set; }
        public String TlvTagName { get; set; }
        public int TlvLengthBytes { get; set; }
        public Type TlvClassType { get; set; }
    }

    public class BitContentConfig
    {
        private IList<BitContentFieldConfig> _fields = new List<BitContentFieldConfig>();

        public String Id { get; set; }
        public Type ClassType { get; set; }
        public IList<BitContentFieldConfig> Fields { get { return _fields; } }
    }

    public class TlvTagConfig
    {
        public String Name { get; set; }
        public String Splitter { get; set; }
        public BitContentConfig BitContent { get; set; }
    }

    public class TlvConfig
    {
        private IList<TlvTagConfig> _tags = new List<TlvTagConfig>();
        private IDictionary<String, TlvTagConfig> _tagMap = new Dictionary<String, TlvTagConfig>();

        public String Id { get; set; }
        public int LengthBytes { get; set; }
        public Type ClassType { get; set; }
        public IList<TlvTagConfig> Tags { get { return ((List<TlvTagConfig>)_tags).AsReadOnly(); } }
        
        public void AddTag(TlvTagConfig tagConfig)
        {
            if (_tagMap.ContainsKey(tagConfig.Name))
                throw new ConfigParserException("Duplicate tag name (name=" + tagConfig.Name
                    + ") inside tlv element (id=" + this.Id + ")");
            _tagMap[tagConfig.Name] = tagConfig;
            _tags.Add(tagConfig);
        }

        public TlvTagConfig GetTag(String tag)
        {
            return _tagMap.ContainsKey(tag) ? _tagMap[tag] : null;
        }

        public TlvTagConfig GetTag(int index)
        {
            return _tags.Count > index && index >= 0 ? _tags[index] : null;
        }

        public int TagsCount
        {
            get { return _tags.Count; }
        }
    }

    public interface IMessageHeaderConfig
    {
        int Length { get; }
        MessageElement GetNewHeader();
    }

    public class MessageTypeConfig : IMessageHeaderConfig
    {
        public byte[] Value { get; set; }

        #region AbstractMessageHeaderConfig Members
        public int Length
        {
            get { return Value == null ? 0 : Value.Length; }
            set { }
        }
        
        public MessageElement GetNewHeader()
        {
            return new MessageTypeHeader(Value);
        }
        #endregion
    }

    public class MessageHeaderConfig : IMessageHeaderConfig
    {
        private byte[] _value;

        public String Name { get; set; }
        public String StringValue
        {
            get
            {
                return MessageUtility.HexToString(_value);
            }
            set
            {
                Value = MessageUtility.StringToHex(value);
            }
        }
        public byte[] Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                if (_value == null) throw new ConfigParserException("Invalid value of message header (name=" + Name + ")");
                if (_value.Length != Length)
                    throw new ConfigParserException("Provided value incompatible with provided length in message header (name="
                        + Name + ")");
            }
        }
        public GetHeaderBytes GetFieldBytesFunc { get; set; }

        #region AbstractMessageHeaderConfig Members
        public int Length { get; set; }
        
        public MessageElement GetNewHeader()
        {
            MessageHeader header = new MessageHeader();
            header.SetValue(_value);
            header.GetFieldBytesFunc = GetFieldBytesFunc;
            return header;
        }
        #endregion
    }

    public class MessageFieldConfig
    {
        public MessageFieldConfig()
        {
            this.Length = -1;
            this.LengthHeader = -1;
        }

        public int Seq { get; set; }
        public Type FieldType { get; set; }
        public int Length { get; set; }
        public int LengthHeader { get; set; }
        public bool FromRequest { get; set; }
        public Delegate GetFieldBytesFunc { get; set; }
    }

    public class MessageBitMapConfig : MessageFieldConfig, IMessageHeaderConfig
    {
        #region IMessageHeaderConfig Members
        public MessageElement GetNewHeader()
        {
            MessageBitMap bitMap = new MessageBitMap();
            bitMap.Length = Length;
            return bitMap;
        }
        #endregion
    }

    public class MessageConfig
    {
        private List<IMessageHeaderConfig> _headers = new List<IMessageHeaderConfig>();
        private Dictionary<int, MessageFieldConfig> _fields = new Dictionary<int, MessageFieldConfig>();

        public String Id { get; set; }
        public int LengthHeader { get; set; }
        internal IList<IMessageHeaderConfig> Headers { get { return _headers; } }
        public IList<IMessageHeaderConfig> RoHeaders { get { return _headers.AsReadOnly(); } }
        internal IDictionary<int, MessageFieldConfig> Fields { get { return _fields; } }
        public ICollection<MessageFieldConfig> RoFields { get { return _fields.Values; } }
    }

    public class ModelPropertyConfig
    {
        public PropertyInfo PropertyInfo { get; set; }
        public MessageFieldConfig FieldBit { get; set; }
        public PropertyInfo GetValueFromBytes { get; set; }
        public MethodInfo SetValueFromProperty { get; set; }
        public int FracDigits { get; set; }
        public Delegate GetPropertyValueFunc { get; set; }
        public BitContentConfig BitContent { get; set; }
        public TlvConfig Tlv { get; set; }
        public String TlvTagName { get; set; }
        public int TlvLengthBytes { get; set; }
        public Type TlvClassType { get; set; }
    }

    public class ModelConfig
    {
        private Dictionary<int, ModelPropertyConfig> _props = new Dictionary<int, ModelPropertyConfig>();

        public String Id { get; set; }
        public Type ClassType { get; set; }
        public MessageConfig MessageCfg { get; set; }
        internal Dictionary<int, ModelPropertyConfig> Properties { get { return _props; } }
        public ICollection<ModelPropertyConfig> RoProperties { get { return _props.Values; } }
        
        public ModelConfig Clone()
        {
            ModelConfig cfg = new ModelConfig();
            cfg.Id = Id;
            cfg.ClassType = ClassType;
            cfg.MessageCfg = MessageCfg;
            foreach (KeyValuePair<int, ModelPropertyConfig> propCfg in Properties) cfg.Properties.Add(propCfg.Key, propCfg.Value);
            return cfg;
        }

        public void AddOrSubtituteProperty(ModelPropertyConfig propCfg)
        {
            int i = -1;
            String propName = propCfg.PropertyInfo.Name;
            foreach (KeyValuePair<int, ModelPropertyConfig> kvp in Properties)
            {
                if (kvp.Value.PropertyInfo.Name == propName)
                {
                    i = kvp.Key;
                    break;
                }
            }
            if (i == -1)
                Properties.Add(propCfg.FieldBit.Seq, propCfg);
            else
            {
                Properties.Remove(i);
                Properties[propCfg.FieldBit.Seq] = propCfg;
            }
        }

        public ModelPropertyConfig GetPropertyMappedToField(int seq)
        {
            return _props.ContainsKey(seq) ? _props[seq] : null;
        }
    }

    public interface IMaskConfig
    {
        bool IsQualified(byte[] message);
    }

    public interface IMaskListConfig
    {
        IList<IMaskConfig> MaskList { get; }
    }

    public class MaskConfig : IMaskConfig
    {
        protected delegate bool QualifyDelegate(byte[] message);
        protected QualifyDelegate IsQualifiedFunc;
        
        private byte[] _value;
        private String _maskResult;
        private String _strValue;
        private String _msg = "";
        private int _startByte = 0;
        private int _length = 0;

        public MaskConfig()
        {
            IsQualifiedFunc = ValueEquals;
        }

        public static int MinBytesCountToCheck { get; internal set; }

        public int StartByte
        {
            get { return _startByte;  }
            set
            {
                _startByte = value;
                if (_startByte + _length - 1 > MinBytesCountToCheck)  MinBytesCountToCheck = _startByte + _length - 1;
            }
        }
        public int Length
        {
            get { return _length; }
            set
            {
                _length = value;
                if (_startByte + _length - 1 > MinBytesCountToCheck) MinBytesCountToCheck = _startByte + _length - 1;
            }
        }

        public bool ValueEquals(byte[] message)
        {
            bool isTrue = true;
            int j = StartByte - 1;
            if (j + Length > message.Length) return false;
            for (int i = 0; i < Length; i++, j++)
            {
                isTrue = isTrue && (message[j] == _value[i]);
            }
            return isTrue;
        }

        public bool MaskEquals(byte[] message)
        {
            bool isTrue = true;
            int j = StartByte - 1;
            if (j + Length > message.Length) return false;
            for (int i = 0; i < Length; i++, j++)
            {
                isTrue = isTrue && ((message[j] & _value[i]) == _value[i]);
            }
            return isTrue;
        }

        public bool MaskNotEquals(byte[] message)
        {
            int j = StartByte - 1;
            if (j + Length > message.Length) return true;
            for (int i = 0; i < Length; i++, j++)
            {
                if ((message[j] & _value[i]) != _value[i]) return true;
            }
            return false;
        }

        public bool MaskZero(byte[] message)
        {
            return !MaskNotZero(message);
        }

        public bool MaskNotZero(byte[] message)
        {
            bool isTrue = false;
            int j = StartByte - 1;
            if (j + Length > message.Length) return false;
            for (int i = 0; i < Length; i++, j++)
            {
                isTrue = isTrue || ((message[j] & _value[i]) != 0);
            }
            return isTrue;
        }

        public String StringValue
        {
            get
            {
                return _strValue;
            }
            set
            {
                _strValue = value;
                int len = Length * 2;
                while (_strValue.Length < len) _strValue = "0" + _strValue;
                if (len < _strValue.Length)
                    throw new ConfigParserException("Incompatible provided value/mask with provided length in mask element. " + _msg);

                _value = MessageUtility.StringToHex(value);
                if (_value == null)
                    throw new ConfigParserException("Invalid value/mask provided in mask element. " + _msg);

                _msg = "";
            }
        }

        public byte[] Value
        {
            get { return _value; }
            set
            {
                _value = value;
                Length = _value.Length;
                _strValue = MessageUtility.HexToString(_value);
            }
        }

        public void SetValue(String value, String modelId, String delegateId)
        {
            _msg = "Check mask element inside message-to-model element (model id="+modelId+", delegate id="+delegateId+")";
            this.StringValue = value;
        }

        public String MaskResult
        {
            get { return _maskResult; }
            set
            {
                _maskResult = value;
                if (value == "equals")
                {
                    IsQualifiedFunc = MaskEquals;
                }
                else if (value == "notEquals")
                {
                    IsQualifiedFunc = MaskNotEquals;
                }
                else if (value == "zero")
                {
                    IsQualifiedFunc = MaskZero;
                }
                else if (value == "notZero")
                {
                    IsQualifiedFunc = MaskNotZero;
                }
                else
                {
                    IsQualifiedFunc = ValueEquals;
                    _maskResult = null;
                }
            }
        }

        #region IMaskConfig Members
        public bool IsQualified(byte[] message)
        {
            return IsQualifiedFunc(message);
        }
        #endregion
    }

    public class MaskAndConfig : IMaskConfig, IMaskListConfig
    {
        private IList<IMaskConfig> _list = new List<IMaskConfig>();

        #region IMaskListConfig Members
        public IList<IMaskConfig> MaskList
        {
            get { return _list; }
        }
        #endregion
        
        #region IMaskConfig Members
        public bool IsQualified(byte[] message)
        {
            bool isTrue = true;
            foreach (IMaskConfig cfg in _list) {
                isTrue = isTrue && cfg.IsQualified(message);
            }
            return isTrue;
        }
        #endregion
    }

    public class MaskOrConfig : IMaskListConfig, IMaskConfig
    {
        private IList<IMaskConfig> _list = new List<IMaskConfig>();

        #region IMaskListConfig Members
        public IList<IMaskConfig> MaskList
        {
            get { return _list; }
        }
        #endregion

        #region IMaskConfig Members
        public bool IsQualified(byte[] message)
        {
            bool isTrue = false;
            foreach (IMaskConfig cfg in _list)
            {
                isTrue = isTrue || cfg.IsQualified(message);
            }
            return isTrue;
        }
        #endregion
    }


    public class MessageToModelConfig : MaskOrConfig
    {
        public ModelConfig ModelCfg { get; set; }
        public Delegate ProcessModel { get; set; }
    }

    internal static class MessageConfigs
    {
        private static IDictionary<String, Type> _types = new Dictionary<String, Type>();
        private static IDictionary<Object, Delegate> _headerDelegates = new Dictionary<Object, Delegate>();
        private static IDictionary<Object, Delegate> _fieldDelegates = new Dictionary<Object, Delegate>();
        private static IDictionary<Object, Delegate> _propertyDelegates = new Dictionary<Object, Delegate>();
        private static IDictionary<Object, Delegate> _processDelegates = new Dictionary<Object, Delegate>();
        private static IDictionary<String, BitContentConfig> _bitContents = new Dictionary<String, BitContentConfig>();
        private static IDictionary<String, TlvConfig> _tlvs = new Dictionary<String, TlvConfig>();
        private static IDictionary<String, MessageConfig> _messages = new Dictionary<String, MessageConfig>();
        private static IDictionary<String, ModelConfig> _models = new Dictionary<String, ModelConfig>();
        private static IDictionary<Type, ModelConfig> _classToModels = new Dictionary<Type, ModelConfig>();
        private static IList<MessageToModelConfig> _messageToModels = new List<MessageToModelConfig>();
        
        public static IDictionary<String, Type> Types { get { return _types; } }
        public static IDictionary<Object, Delegate> HeaderDelegates { get { return _headerDelegates; } }
        public static IDictionary<Object, Delegate> FieldDelegates { get { return _fieldDelegates; } }
        public static IDictionary<Object, Delegate> PropertyDelegates { get { return _propertyDelegates; } }
        public static IDictionary<Object, Delegate> ProcessDelegates { get { return _processDelegates; } }
        public static IDictionary<String, BitContentConfig> BitContents { get { return _bitContents; } }
        public static IDictionary<String, TlvConfig> Tlvs { get { return _tlvs; } }
        public static IDictionary<String, MessageConfig> Messages { get { return _messages; } }
        public static IDictionary<String, ModelConfig> Models { get { return _models; } }
        
        public static IDictionary<Type, ModelConfig> ClassToModels { get { return _classToModels;  } }
        public static IList<MessageToModelConfig> MessageToModels { get { return _messageToModels;  } }
        
        public static MessageToModelConfig GetQulifiedMessageToModel(byte[] message)
        {
            foreach (MessageToModelConfig cfg in _messageToModels)
            {
                if (cfg.IsQualified(message)) return cfg;
            }
            return null;
        }
        
        public static void Clear()
        {
            PropertyInfo[] props = typeof(MessageConfigs).GetProperties(
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            foreach (PropertyInfo prop in props)
            {
                Object propVal = prop.GetValue(null, null);
                var clearMethod = propVal.GetType().GetMethod("Clear", Type.EmptyTypes);
                if (clearMethod != null) clearMethod.Invoke(propVal, new object[] {});
            }
            MaskConfig.MinBytesCountToCheck = 0;
        }
    }
}
