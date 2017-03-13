using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Free.iso8583.model
{
    public class ReadOnlyException : NotSupportedException
    {
        public ReadOnlyException() : base("Read-only collection.") { }

        public ReadOnlyException(String message) : base(message)
        {
        }

        public ReadOnlyException(String message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
