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

namespace Free.iso8583.model
{
    public class NibbleList : List<byte>
    {
        private bool _isOdd = false;
        private bool _isLeftAligned = false;

        public NibbleList(byte[] bytes) : base(bytes)
        {
        }

        public static implicit operator byte[](NibbleList fbl)
        {
            return fbl.ToArray();
        }
        
        public static implicit operator NibbleList(byte[] bytes)
        {
            return new NibbleList(bytes);
        }

        public bool IsOdd
        {
            get { return _isOdd; }
            set { _isOdd = value; }
        }
        public bool IsLeftAligned
        {
            get { return _isLeftAligned; }
            set { _isLeftAligned = value; }
        }

        public int Length
        {
            get
            {
                int length = this.Count * 2;
                if (IsOdd) length--;
                return length;
            }
        }

        public new byte this[int i]
        {
            get
            {
                int length = Length;
                if (i < 0 || i >= length) throw new ArgumentOutOfRangeException();
                if (!IsLeftAligned) i++;
                int idx = i / 2;
                bool isUpper = (i % 2 == 0);
                if (isUpper)
                {
                    return (byte)(((int)base[idx] & 0xf0) >> 4);
                }
                else
                {
                    return (byte)((int)base[idx] & 0x0f); 
                }
            }
            set
            {
                int length = Length;
                if (i < 0 || i >= length) throw new ArgumentOutOfRangeException();
                if (!IsLeftAligned) i++;
                int idx = i / 2;
                bool isUpper = (i % 2 == 0);

                int val = (int)value & 0x0f;
                int curVal = base[idx];
                if (isUpper)
                {
                    curVal = curVal & 0x0f;
                    val = val << 4;
                    curVal = curVal | val;
                }
                else
                {
                    curVal = curVal & 0xf0;
                    curVal = curVal | val;
                }
                base[idx] = (byte)curVal;
            }
        }

        public override string ToString()
        {
            return MessageUtility.HexToString(this.ToArray());
        }
    }
}
