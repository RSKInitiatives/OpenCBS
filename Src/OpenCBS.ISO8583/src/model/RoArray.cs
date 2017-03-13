using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Free.iso8583.model
{
    public class RoArray<T> : ReadOnlyCollection<T>
    {
        public RoArray(T[] list) : base(list) { }

        public bool IsFixedSize { get { return true; } }
        public bool IsReadOnly { get { return true; } }
        public bool IsSynchronized { get { return (Items as ICollection).IsSynchronized; } }
        public int Length { get { return (Items as T[]).Length; } }
        public long LongLength { get { return (Items as T[]).LongLength; } }
        public int Rank { get { return (Items as T[]).Rank; } }
        public Object SyncRoot { get { return (Items as ICollection).SyncRoot; } }
        
        public static implicit operator T[](RoArray<T> array)
        {
            return array.ToArray();
        }

        public static implicit operator RoArray<T>(T[] array)
        {
            return new RoArray<T>(array);
        }
    }
}
