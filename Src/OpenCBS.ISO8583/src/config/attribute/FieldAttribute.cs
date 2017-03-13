using System;
using System.Collections.Generic;
using System.Linq;

namespace Free.iso8583.config.attribute
{
    public abstract class FieldAttribute : Attribute
    {
        public int Seq { get; set; }
        public virtual FieldType FieldType { get; set; }

        private static IDictionary<FieldType, Type> _fieldClasses = new Dictionary<FieldType, Type>()
        {
            {FieldType.B, typeof(MessageField)},
            {FieldType.N, typeof(NMessageField)},
            {FieldType.NS, typeof(NsMessageField)},
            {FieldType.AN, typeof(AnMessageField)},
            {FieldType.ANS, typeof(AnsMessageField)},
            {FieldType.Null, typeof(NullMessageField)},
            {FieldType.BitMap, typeof(MessageBitMap)},
        };
        public virtual Type FieldClass
        {
            get { return _fieldClasses[FieldType]; }
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class NullFieldAttribute : FieldAttribute
    {
        public override FieldType FieldType
        {
            get { return FieldType.Null; }
            set { }
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class BitMapFieldAttribute : FieldAttribute
    {
        public override FieldType FieldType
        {
            get { return FieldType.BitMap; }
            set { }
        }

        public int Length { get; set; }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class PropertyFieldAttribute : FieldAttribute
    {
        public PropertyFieldAttribute()
        {
            TlvLengthBytes = 3;
        }
        
        private static HashSet<FieldType> _validTypes = new HashSet<FieldType>() {
            FieldType.B, FieldType.N, FieldType.NS, FieldType.AN, FieldType.ANS
        };
        public override FieldType FieldType
        {
            get { return _fieldType; }
            set
            {
                if (!_validTypes.Contains(value))
                {
                    throw new ConfigParserException("Invalid value for FieldType property. Use only: "
                        + string.Join(", ", (from item in _validTypes select item.ToString()).ToArray())
                    );
                }
                _fieldType = value;
            }
        }
        private FieldType _fieldType;

        public int LengthHeader { get; set; }
        public int Length { get; set; }
        public PropertyType PropertyType { get; set; }
        public bool FromRequest { get; set; }
        public Type FieldDelegateClass { get; set; }
        public string FieldDelegateMethod { get; set; }
        public int FracDigits { get; set; }
        public Type PropertyDelegateClass { get; set; }
        public string PropertyDelegateMethod { get; set; }
        public Type Tlv { get; set; }
        public String TlvTagName { get; set; }
        public int TlvLengthBytes { get; set; }
    }

    public enum FieldType
    {
        B = 0,
        N = 1,
        NS = 2,
        AN = 3,
        ANS = 4,
        Null = 5,
        BitMap = 6
    }

    public enum PropertyType
    {
        String = 1,
        Int = 2,
        Decimal = 3,
        Bytes = 4,
        BitContent = 5
    }
}
