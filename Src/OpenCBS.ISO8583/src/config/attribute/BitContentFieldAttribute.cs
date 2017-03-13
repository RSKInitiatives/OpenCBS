using System;

namespace Free.iso8583.config.attribute
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class BitContentFieldAttribute : Attribute
    {
        public BitContentFieldAttribute()
        {
            PadChar = ' ';
            NullChar = ' ';
            IsTrim = true;
            TlvLengthBytes = 3;
        }

        public int Length { get; set; }
        public char PadChar { get; set; }
        public FieldAlignment Align { get; set; }
        public char NullChar { get; set; }
        public bool IsTrim { get; set; }
        public bool IsOptional { get; set; }
        public Type Tlv { get; set; }
        public String TlvTagName { get; set; }
        public int TlvLengthBytes { get; set; }
    }

    public enum FieldAlignment
    {
        Left = 0,
        Right = 1
    }
}
