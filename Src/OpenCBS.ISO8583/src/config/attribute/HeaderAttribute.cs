using System;

namespace Free.iso8583.config.attribute
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public abstract class BaseHeaderAttribute : Attribute
    {
        public int Seq { get; set; }
    }

    public class HeaderAttribute : BaseHeaderAttribute
    {
        public string Name { get; set; }
        public byte[] Value { get; set; }
        public Type DelegateClass { get; set; }
        public string DelegateMethod { get; set; }

        public int Length
        {
            get
            {
                if (_length <= 0 && Value != null) _length = Value.Length;
                return _length;
            }
            set
            {
                _length = value;
            }
        }
        private int _length = 0;
    }

    public class MessageTypeAttribute : BaseHeaderAttribute
    {
        public byte[] Value { get; set; }
    }

    public class BitMapAttribute : BaseHeaderAttribute
    {
        public int Length { get; set; }
    }
}
