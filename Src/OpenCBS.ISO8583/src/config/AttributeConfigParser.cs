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
using Free.iso8583.config.attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Free.iso8583.config
{
    internal class AttributeConfigParser : ConfigParser
    {
        private Type _mapping;
        
        private AttributeConfigParser()
        {
        }

        public AttributeConfigParser(Type messageToModelMapping)
        {
            _mapping = messageToModelMapping;
        }

        private string Trim(string str)
        {
            return (str ?? "").Trim();
        }

        private AttributeType GetAttribute<AttributeType>(MemberInfo mi) where AttributeType : Attribute
        {
            return Attribute.GetCustomAttribute(mi, typeof(AttributeType)) as AttributeType;
        }

        private AttributeType[] GetAttributes<AttributeType>(MemberInfo mi) where AttributeType : Attribute
        {
            return Attribute.GetCustomAttributes(mi, typeof(AttributeType)) as AttributeType[];
        }

        private KeyValuePair<PropertyInfo, TReturn> GetValue<TReturn>(Object obj, String propertyName)
        {
            var propInfo = obj.GetType().GetProperty(propertyName);
            return new KeyValuePair<PropertyInfo, TReturn>(propInfo, (TReturn)propInfo.GetValue(obj, null));
        }

        private void CheckPositiveValue(Object obj, String propertyName, string sourceMsg)
        {
            var kvp = GetValue<int>(obj, propertyName);
            if (kvp.Value <= 0)
            {
                throw new MessageParserException(kvp.Key.Name + " property of " + kvp.Key.ReflectedType.Name
                    + " class must be greater than 0. " + sourceMsg);
            }
        }

        private bool IsEmpty(object value)
        {
            bool isEmpty = value == null;
            if (!isEmpty) 
            {
                if (value is System.Collections.IEnumerable)
                {
                    isEmpty = (value as System.Collections.IEnumerable).Cast<object>().Count() <= 0;
                }
                else if (value is Enum)
                {
                    isEmpty = !Enum.IsDefined(value.GetType(), value);
                }
            }
            return isEmpty;
        }

        private void CheckNonNegativeValue(Object obj, string propertyName, string sourceMsg)
        {
            var kvp = GetValue<int>(obj, propertyName);
            if (kvp.Value < 0)
            {
                throw new MessageParserException(kvp.Key.Name + " property of " + kvp.Key.ReflectedType.Name
                    + " class must be greater than or equal to 0. " + sourceMsg);
            }
        }

        private void CheckRequired(Object obj, string propertyName, string sourceMsg)
        {
            var kvp = GetValue<Object>(obj, propertyName);
            if (IsEmpty(kvp.Value))
            {
                throw new MessageParserException(kvp.Key.Name + " property of " + kvp.Key.ReflectedType.Name + " must be defined. "
                    + sourceMsg);
            }
        }

        public override void Parse()
        {
            Type IMaskType = typeof(IMask);
            MethodInfo[] methods = _mapping.GetMethods(BindingFlags.Public | BindingFlags.Static);
            foreach (var m in methods)
            {
                if (!IMaskType.IsAssignableFrom(m.ReturnType) || m.GetParameters().Length > 0) continue;
                var attr = GetAttribute<MessageToModelAttribute>(m);
                if (attr == null) continue;
                
                MessageToModelConfig cfg = new MessageToModelConfig();
                string sourceMsg = "Check mapping defined by " + m.Name + " method of " + _mapping.FullName + " class.";

                if (attr.Model == null) throw new ConfigParserException("Model must be defined. " + sourceMsg);
                cfg.ModelCfg = ParseModel(attr.Model, sourceMsg);

                MethodInfo processMethod = null;
                if (attr.ProcessClass == null)
                {
                    cfg.ProcessModel = null;
                }
                else
                {
                    CheckRequired(attr, "ProcessMethod", sourceMsg);
                    cfg.ProcessModel = ParseDelegate(typeof(ProcessModel<,>), attr.ProcessClass, attr.ProcessMethod, sourceMsg);
                    processMethod = cfg.ProcessModel.Method;
                }

                cfg.MaskList.Add((m.Invoke(null, null) as IMask).GetConfig(attr.Model, processMethod));
                MessageConfigs.MessageToModels.Add(cfg);
            }
        }

        private ModelConfig ParseModel(Type modelType, string sourceMsg)
        {
            if (MessageConfigs.ClassToModels.ContainsKey(modelType))
            {
                throw new ConfigParserException(modelType.FullName + " class model has been used by another mapping. " + sourceMsg);
            }
            ModelConfig cfg = new ModelConfig();
            cfg.Id = modelType.FullName;
            cfg.ClassType = modelType;
            cfg.MessageCfg = ParseMessage(modelType);

            ParseModelProperties(modelType, cfg);

            MessageConfigs.ClassToModels[cfg.ClassType] = cfg;
            if (!String.IsNullOrEmpty(cfg.Id)) MessageConfigs.Models[cfg.Id] = cfg;
            return cfg;
        }

        private void ParseModelProperties(Type modelType, ModelConfig cfg)
        {
            string sourceMsg = "Check attribute for {0} property of " + modelType.FullName + " class.";
            foreach (var propInfo in modelType.GetProperties())
            {
                var attr = GetAttribute<PropertyFieldAttribute>(propInfo);
                if (attr == null) continue;
                
                string srcMsg = string.Format(sourceMsg, propInfo.Name);
                CheckPositiveValue(attr, "Seq", srcMsg);
                CheckNonNegativeValue(attr, "FracDigits", srcMsg);
                attr.TlvTagName = Trim(attr.TlvTagName);
                CheckPositiveValue(attr, "TlvLengthBytes", srcMsg);

                if (IsEmpty(attr.PropertyType) && IsEmpty(attr.PropertyDelegateClass) && IsEmpty(attr.Tlv)
                    && IsEmpty(attr.TlvTagName))
                {
                    throw new ConfigParserException(
                        "Either PropertyType or PropertyDelegateClass or Tlv or TlvTagName property must be defined. " + srcMsg);
                }
                if (!IsEmpty(attr.PropertyDelegateClass))
                {
                    CheckRequired(attr, "PropertyDelegateMethod", srcMsg);
                }

                ModelPropertyConfig ccfg = new ModelPropertyConfig();
                ccfg.PropertyInfo = propInfo;
                ccfg.FracDigits = attr.FracDigits;
                ccfg.GetPropertyValueFunc = ParseDelegate(typeof(GetPropertyValue<>), attr.PropertyDelegateClass,
                    attr.PropertyDelegateMethod, srcMsg);
                ccfg.BitContent = attr.PropertyType != PropertyType.BitContent ? null
                    : ParseBitContent(propInfo.PropertyType, srcMsg);
                ccfg.Tlv = attr.Tlv == null ? null : ParseTlv(attr.Tlv, srcMsg);
                ccfg.TlvTagName = attr.TlvTagName.ToUpper();
                ccfg.TlvLengthBytes = attr.TlvLengthBytes;
                ccfg.TlvClassType = ccfg.Tlv == null ? attr.Tlv : null;

                var fieldBit = cfg.MessageCfg.Fields[attr.Seq];
                //if property delegate is not ignored (because it's not bitcontent or Tlv and TlvTagName are not specified) then
                //the mapped field must also specify field delegate
                if (ccfg.BitContent == null && ccfg.Tlv == null && IsEmpty(ccfg.TlvTagName)
                    && ccfg.GetPropertyValueFunc != null && fieldBit.GetFieldBytesFunc == null)
                {
                    throw new ConfigParserException("There is a property defines PropertyDelegateClass but not define "
                        + "FieldDelegateClass. " + srcMsg);
                }
                HookPropertyToField(ccfg, fieldBit, attr.PropertyType.ToString());

                cfg.Properties.Add(attr.Seq, ccfg);
            }
        }

        private MessageConfig ParseMessage(Type modelType)
        {
            string sourceMsg = "Check attribute for " + modelType.FullName + " model class.";
            var messageAttr = GetAttribute<MessageAttribute>(modelType);
            CheckPositiveValue(messageAttr, "LengthHeader", sourceMsg);

            MessageConfig cfg = new MessageConfig();
            cfg.Id = modelType.FullName;
            cfg.LengthHeader = messageAttr.LengthHeader;

            ParseMessageHeaders(modelType, cfg, sourceMsg);
            ParseMessageFields(modelType, cfg, sourceMsg);

            MessageConfigs.Messages.Add(cfg.Id, cfg);
            return cfg;
        }

        private void ParseMessageHeaders(Type modelType, MessageConfig cfg, string sourceMsg)
        {
            var headerAttrs = GetAttributes<BaseHeaderAttribute>(modelType).OrderBy(item => item.Seq);
            foreach (var attr in headerAttrs)
            {
                IMessageHeaderConfig ccfg;
                if (attr is BitMapAttribute)
                {
                    var attr2 = attr as BitMapAttribute;
                    CheckPositiveValue(attr2, "Length", sourceMsg);
                    
                    var ccfg2 = new MessageBitMapConfig();
                    ccfg2.Length = attr2.Length;
                    ccfg = ccfg2;
                }
                else if (attr is MessageTypeAttribute)
                {
                    var attr2 = attr as MessageTypeAttribute;
                    CheckRequired(attr2, "Value", sourceMsg);
                    
                    var ccfg2 = new MessageTypeConfig();
                    ccfg2.Value = attr2.Value;
                    ccfg2.Length = attr2.Value.Length;
                    ccfg = ccfg2;
                }
                else
                {
                    var attr2 = attr as HeaderAttribute;
                    attr2.Name = Trim(attr2.Name);
                    GetHeaderBytes func = null;

                    if (IsEmpty(attr2.Value))
                    {
                        if (IsEmpty(attr2.DelegateClass))
                        {
                            throw new ConfigParserException("Either Value or delegate property of HeaderAttribute must be defined. "
                                + sourceMsg);
                        }
                        else
                        {
                            CheckRequired(attr2, "DelegateMethod", sourceMsg);
                            CheckPositiveValue(attr2, "Length", sourceMsg);
                            func = (GetHeaderBytes)ParseDelegate(typeof(GetHeaderBytes), attr2.DelegateClass, attr2.DelegateMethod,
                                sourceMsg);
                        }
                    }

                    var ccfg2 = new MessageHeaderConfig();
                    ccfg2.Name = attr2.Name;
                    ccfg2.Length = attr2.Length;

                    if (func != null)
                    {
                        ccfg2.GetFieldBytesFunc = func;
                    }
                    else
                    {
                        ccfg2.Value = attr2.Value;
                    }

                    ccfg = ccfg2;
                }

                cfg.Headers.Add(ccfg);
            }
        }

        private IList<FieldAttribute> GetClassFieldAttributes(Type modelType)
        {
            List<FieldAttribute> attrs = new List<FieldAttribute>();
            do
            {
                var attrs2 = Attribute.GetCustomAttributes(modelType, typeof(FieldAttribute), false) as FieldAttribute[];
                foreach (var attr in attrs2)
                {
                    if (!attrs.Any(at => at.Seq == attr.Seq)) attrs.Add(attr);
                }
                modelType = modelType.BaseType;
            }
            while (modelType != typeof(Object) || modelType == null);
            return attrs;
        }

        private void ParseMessageFields(Type modelType, MessageConfig cfg, string sourceMsg)
        {
            IDictionary<int, MessageFieldConfig> configs = new Dictionary<int, MessageFieldConfig>();
            List<FieldAttribute> attrs = new List<FieldAttribute>();
            attrs.AddRange(GetClassFieldAttributes(modelType));
            List<PropertyInfo> propInfos = new List<PropertyInfo>();
            for (int i = 0; i < attrs.Count; i++) propInfos.Add(null);
            foreach (var propInfo in modelType.GetProperties())
            {
                var attr = GetAttribute<FieldAttribute>(propInfo);
                if (attr == null) continue;
                attrs.Add(attr);
                propInfos.Add(propInfo);
            }

            for (int i =0; i < attrs.Count; i++)
            {
                var attr = attrs[i];
                string srcMsg = sourceMsg;
                if (propInfos[i] != null) {
                    srcMsg = "Check attribute for " + propInfos[i].Name + " property of " + modelType.FullName + " class.";
                }
                
                MessageFieldConfig ccfg = new MessageFieldConfig();
                ccfg.FieldType = attr.FieldClass;
                CheckPositiveValue(attr, "Seq", srcMsg);
                ccfg.Seq = attr.Seq;

                if (attr.FieldType == FieldType.Null)
                {
                    ccfg.Length = 0;
                }
                else if (attr.FieldType == FieldType.BitMap)
                {
                    CheckPositiveValue(attr, "Length", srcMsg);
                    ccfg.Length = ((BitMapFieldAttribute)attr).Length;
                }
                else
                {
                    var attr2 = (PropertyFieldAttribute)attr;
                    
                    if (!(attr2.Length > 0 ^ attr2.LengthHeader > 0))
                    {
                        throw new ConfigParserException(
                            "Either Length or LengthHeader property of PropertyFieldAttribute must be greater than 0 but NOT BOTH. "
                            + srcMsg);
                    }
                    if (attr2.Length > 0)
                    {
                        ccfg.Length = attr2.Length;
                        ccfg.LengthHeader = -1;
                    }
                    else
                    {
                        ccfg.Length = -1;
                        ccfg.LengthHeader = attr2.LengthHeader;
                    }

                    ccfg.FromRequest = attr2.FromRequest;

                    if (attr2.FieldDelegateClass != null)
                    {
                        CheckRequired(attr2, "FieldDelegateMethod", srcMsg);
                        ccfg.GetFieldBytesFunc = ParseDelegate(typeof(GetFieldBytes<>), attr2.FieldDelegateClass,
                            attr2.FieldDelegateMethod, srcMsg);
                    }
                }

                if (configs.ContainsKey(attr.Seq))
                    throw new ConfigParserException("There are more than one message field whose the same Seq value (Seq=" + attr.Seq
                        + "). " + sourceMsg);
                configs.Add(attr.Seq, ccfg);
            }

            /*** Sorts the fields based on their sequence number. It ensures we get the field
             *   from the lowest sequence until the highest one consecutively when they are iterated. ***/
            List<int> keys = configs.Keys.ToList();
            keys.Sort();
            for (int i = 0; i < keys.Count; i++)
            {
                int iSeq = keys[i];
                cfg.Fields.Add(iSeq, configs[iSeq]);
            }
        }

        private BitContentConfig ParseBitContent(Type bitContentType, string sourceMsg)
        {
            if (MessageConfigs.BitContents.ContainsKey(bitContentType.FullName))
            {
                if (MessageConfigs.BitContents[bitContentType.FullName].ClassType != bitContentType)
                    throw new ConfigParserException("There is already BitContent whose type of " + bitContentType.FullName + ". "
                        + sourceMsg);
                return MessageConfigs.BitContents[bitContentType.FullName];
            }

            BitContentConfig cfg = new BitContentConfig();
            cfg.Id = bitContentType.FullName;
            cfg.ClassType = bitContentType;
            ParseBitContentFields(bitContentType, cfg, sourceMsg);

            MessageConfigs.BitContents.Add(cfg.Id, cfg);
            return cfg;
        }

        private void ParseBitContentFields(Type bitContentType, BitContentConfig cfg, string sourceMsg)
        {
            IList<BitContentFieldAttribute> fields = new List<BitContentFieldAttribute>();
            IList<PropertyInfo> properties = new List<PropertyInfo>();
            foreach (var propInfo in bitContentType.GetProperties())
            {
                var attr = GetAttribute<BitContentFieldAttribute>(propInfo);
                if (attr == null) continue;
                fields.Add(attr);
                properties.Add(propInfo);
            }

            int fieldCount = fields.Count;
            if (fieldCount <= 0)
                throw new ConfigParserException("There is no property which defines BitContentFieldAttribute in "
                    + bitContentType.FullName + " class. " + sourceMsg);
            sourceMsg = "Check attribute for {0} property of " + bitContentType.FullName + " class.";
            for (int i = 0; i < fieldCount; i++)
            {
                var ccfg = new BitContentFieldConfig();
                var attr = fields[i];
                var propInfo = properties[i];
                string srcMsg = String.Format(sourceMsg, propInfo.Name);

                CheckNonNegativeValue(attr, "Length", srcMsg);

                attr.TlvTagName = Trim(attr.TlvTagName);
                if (attr.Length <= 0 && IsEmpty(attr.Tlv) && IsEmpty(attr.TlvTagName))
                {
                    throw new ConfigParserException(
                        "Length must be set to a positive value if Tlv and TlvTagName are not specified. " + srcMsg);
                }

                if (Encoding.UTF8.GetByteCount(attr.PadChar.ToString()) != 1)
                    throw new ConfigParserException("PadChar must be a valid ASCII character. " + srcMsg);

                if (IsEmpty(attr.Align))
                {
                    throw new ConfigParserException("Invalid Align value, it must be 'Left' or 'Right'. " + srcMsg);
                }

                if (Encoding.UTF8.GetByteCount(attr.NullChar.ToString()) != 1)
                    throw new ConfigParserException("NullChar must be a valid ASCII character. " + srcMsg);

                if (attr.IsOptional && i < fieldCount - 1)
                {
                    throw new ConfigParserException("IsOptional may only be true for the last field of a BitContent class. "
                            + srcMsg);
                }

                if (!IsEmpty(attr.Tlv))
                {
                    ccfg.Tlv = ParseTlv(attr.Tlv, srcMsg);
                }

                ccfg.PropertyInfo = propInfo;
                ccfg.Length = attr.Length;
                ccfg.PadChar = MessageUtility.CharToByte(attr.PadChar);
                ccfg.Align = attr.Align.ToString().ToLower();
                ccfg.NullChar = MessageUtility.CharToByte(attr.NullChar);
                ccfg.IsTrim = attr.IsTrim;
                ccfg.IsOptional = attr.IsOptional;
                ccfg.TlvTagName = attr.TlvTagName;
                ccfg.TlvLengthBytes = attr.TlvLengthBytes;
                ccfg.TlvClassType = attr.Tlv;
                cfg.Fields.Add(ccfg);
            }
        }

        private TlvConfig ParseTlv(Type tlvType, string sourceMsg)
        {
            if (MessageConfigs.Tlvs.ContainsKey(tlvType.FullName))
            {
                if (MessageConfigs.Tlvs[tlvType.FullName].ClassType != tlvType)
                    throw new ConfigParserException("There is already Tlv whose type of " + tlvType.FullName + ". " + sourceMsg);
                return MessageConfigs.Tlvs[tlvType.FullName];
            }

            var attr = GetAttribute<TlvAttribute>(tlvType);
            if (attr == null)
                throw new ConfigParserException(tlvType.FullName + " class doesn't have TlvAttribute. " + sourceMsg);
            CheckPositiveValue(attr, "LengthBytes", "Check attribute for " + tlvType.FullName + " class.");

            TlvConfig cfg = new TlvConfig();
            cfg.Id = tlvType.FullName;
            cfg.LengthBytes = attr.LengthBytes;
            cfg.ClassType = tlvType;
            ParseTlvTags(tlvType, cfg);

            MessageConfigs.Tlvs.Add(cfg.Id, cfg);
            return cfg;
        }

        private void ParseTlvTags(Type tlvType, TlvConfig cfg)
        {
            int fieldCount = 0;
            foreach (var propInfo in tlvType.GetProperties())
            {
                var attr = GetAttribute<TlvTagAttribute>(propInfo);
                if (attr == null) continue;
                fieldCount++;

                TlvTagConfig ccfg = new TlvTagConfig();
                ccfg.Name = propInfo.Name;

                ccfg.Splitter = null;
                if (attr.Type == TlvTagType.Array)
                {
                    ccfg.Splitter = Trim(attr.Splitter);
                    if (ccfg.Splitter == String.Empty) ccfg.Splitter = ";";
                }

                if (attr.Type == TlvTagType.BitContent)
                {
                    ccfg.BitContent = ParseBitContent(propInfo.PropertyType, "Check attribute for " + propInfo.Name + " of "
                        + tlvType.FullName + " class.");
                }

                cfg.AddTag(ccfg);
            }

            if (fieldCount <= 0)
                throw new ConfigParserException(tlvType.FullName + " class defines TlvAttribute but there is no property which "
                    + "defines TlvTagAttribute");
        }

        private Delegate ParseDelegate(Type delegateType, Type targetType, string methodName, string sourceMsg)
        {
            if (targetType == null) return null;
            MethodInfo targetMethod = targetType.GetMethod(methodName);
            if (targetMethod == null)
            {
                throw new ConfigParserException("No " + methodName + " method of " + targetType.Name
                    + " type that will be bound to " + delegateType.Name + " delegate. " + sourceMsg);
            }

            IDictionary<Object, Delegate> delegates = null;
            Type[] argTypes = new Type[0];
            var paramTypes = targetMethod.GetParameters();
            if (delegateType == typeof(ProcessModel<,>))
            {
                if (targetMethod.ReturnType == null || targetMethod.ReturnType == typeof(void)
                    || paramTypes == null || paramTypes.Length != 1)
                {
                }
                else
                {
                    argTypes = new Type[] { targetMethod.ReturnType, paramTypes[0].ParameterType };
                    delegates = MessageConfigs.HeaderDelegates;
                }
            }
            else if (delegateType == typeof(GetFieldBytes<>))
            {
                if (targetMethod.ReturnType == typeof(byte[]) && paramTypes != null && paramTypes.Length == 1)
                {
                    argTypes = new Type[] { paramTypes[0].ParameterType };
                    delegates = MessageConfigs.FieldDelegates;
                }
            }
            else if (delegateType == typeof(GetPropertyValue<>))
            {
                if (paramTypes != null && paramTypes.Length == 1 && paramTypes[0].ParameterType == typeof(byte[])
                    && targetMethod.ReturnType != null && targetMethod.ReturnType != typeof(void))
                {
                    argTypes = new Type[] { targetMethod.ReturnType };
                    delegates = MessageConfigs.PropertyDelegates;
                }
            }
            else if (delegateType == typeof(GetHeaderBytes))
            {
                if (targetMethod.ReturnType == typeof(byte[]) && (paramTypes == null || paramTypes.Length == 0))
                {
                    delegates = MessageConfigs.HeaderDelegates;
                }
            }

            if (delegates == null)
                throw new ConfigParserException(methodName + " method of " + targetType.Name + " or " + delegateType.Name
                    + " delegate cannot be used. " + sourceMsg);
            
            if (argTypes.Length > 0) delegateType = delegateType.MakeGenericType(argTypes);
            
            var func = MessageConfigs.HeaderDelegates.ContainsKey(targetMethod) ? (Delegate)MessageConfigs.HeaderDelegates[targetMethod]
                : MessageConfigs.FieldDelegates.ContainsKey(targetMethod) ? (Delegate)MessageConfigs.FieldDelegates[targetMethod]
                : MessageConfigs.PropertyDelegates.ContainsKey(targetMethod) ? (Delegate)MessageConfigs.PropertyDelegates[targetMethod]
                : MessageConfigs.ProcessDelegates.ContainsKey(targetMethod) ? (Delegate)MessageConfigs.ProcessDelegates[targetMethod]
                : null;
            if (func != null) return func;

            try
            {
                if (targetMethod.IsStatic)
                {
                    func = Delegate.CreateDelegate(delegateType, targetMethod);
                }
                else
                {
                    func = Delegate.CreateDelegate(delegateType, GetInstanceOf(targetType), targetMethod);
                }
            }
            catch
            {
                throw new ConfigParserException("Cannot create " + delegateType.Name + " delegate. " + sourceMsg);
            }

            delegates[targetMethod] = func;
            return func;
        }
    }
}
