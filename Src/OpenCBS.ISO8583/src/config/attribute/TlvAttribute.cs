using System;

namespace Free.iso8583.config.attribute
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class TlvAttribute : Attribute
    {
        public TlvAttribute()
        {
            LengthBytes = 3;
        }

        public int LengthBytes { get; set; }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class TlvTagAttribute : Attribute
    {
        public string Splitter { get; set; }
        public TlvTagType Type { get; set; }
    }

    public enum TlvTagType
    {
        Default = 0,
        Array = 1,
        BitContent = 2
    }
}
