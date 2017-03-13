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
    public class DateTimeNibble
    {
        public static DateTime GetMMDD(byte[] bytes)
        {
            if (bytes.Length < 2)
                throw new MessageProcessorException("Invalid argument.");

            int m1 = ((int)bytes[0] & 0xf0) >> 4;
            int m2 = (int)bytes[0] & 0x0f;
            if (m1 >= 10 || m2 >= 10)
                throw new MessageProcessorException("Invalid argument.");

            int d1 = ((int)bytes[1] & 0xf0) >> 4;
            int d2 = (int)bytes[1] & 0x0f;
            if (d1 >= 10 || d2 >= 10)
                throw new MessageProcessorException("Invalid argument.");

            return new DateTime(DateTime.Now.Year, m1 * 10 + m2, d1 * 10 + d2);
        }

        public static byte[] GetBytesFromMMDD(DateTime dt)
        {
            byte[] bytes = new byte[2];
            int m1 = dt.Month / 10;
            int m2 = dt.Month % 10;
            int d1 = dt.Day / 10;
            int d2 = dt.Day % 10;
            bytes[0] = (byte)((m1 << 4) | m2);
            bytes[1] = (byte)((d1 << 4) | d2);
            return bytes;
        }

        public static DateTime GetYYMM(byte[] bytes)
        {
            if (bytes.Length < 2)
                throw new MessageProcessorException("Invalid argument.");

            int y1 = ((int)bytes[0] & 0xf0) >> 4;
            int y2 = (int)bytes[0] & 0x0f;
            if (y1 >= 10 || y2 >= 10)
                throw new MessageProcessorException("Invalid argument.");

            int m1 = ((int)bytes[1] & 0xf0) >> 4;
            int m2 = (int)bytes[1] & 0x0f;
            if (m1 >= 10 || m2 >= 10)
                throw new MessageProcessorException("Invalid argument.");

            int year = (DateTime.Now.Year / 100) * 100 + y1 * 10 + y2;
            if (year < 1970) year += 100;

            return new DateTime(year, m1 * 10 + m2, 1);
        }

        public static byte[] GetBytesFromYYMM(DateTime dt)
        {
            byte[] bytes = new byte[2];
            int y1 = (dt.Year % 100) / 10;
            int y2 = dt.Year % 10;
            int m1 = dt.Month / 10;
            int m2 = dt.Month % 10;
            bytes[0] = (byte)((y1 << 4) | y2);
            bytes[1] = (byte)((m1 << 4) | m2);
            return bytes;
        }

        public static DateTime GetHHMMSS(byte[] bytes)
        {
            if (bytes.Length < 3)
                throw new MessageProcessorException("Invalid argument.");

            int h1 = ((int)bytes[0] & 0xf0) >> 4;
            int h2 = (int)bytes[0] & 0x0f;
            if (h1 >= 10 || h2 >= 10)
                throw new MessageProcessorException("Invalid argument.");

            int m1 = ((int)bytes[1] & 0xf0) >> 4;
            int m2 = (int)bytes[1] & 0x0f;
            if (m1 >= 10 || m2 >= 10)
                throw new MessageProcessorException("Invalid argument.");

            int s1 = ((int)bytes[2] & 0xf0) >> 4;
            int s2 = (int)bytes[2] & 0x0f;
            if (s1 >= 10 || s2 >= 10)
                throw new MessageProcessorException("Invalid argument.");

            return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
                h1 * 10 + h2, m1 * 10 + m2, s1 * 10 + s2);
        }

        public static byte[] GetBytesFromHHMMSS(DateTime dt)
        {
            byte[] bytes = new byte[3];
            int h1 = dt.Hour / 10;
            int h2 = dt.Hour % 10;
            int m1 = dt.Minute / 10;
            int m2 = dt.Minute % 10;
            int s1 = dt.Second / 10;
            int s2 = dt.Second % 10;
            bytes[0] = (byte)((h1 << 4) | h2);
            bytes[1] = (byte)((m1 << 4) | m2);
            bytes[2] = (byte)((s1 << 4) | s2);
            return bytes;
        }

        public static DateTime GetMMDDHHMMSS(byte[] bytes)
        {
            if (bytes.Length < 5)
                throw new MessageProcessorException("Invalid argument.");

            int m1 = ((int)bytes[0] & 0xf0) >> 4;
            int m2 = (int)bytes[0] & 0x0f;
            if (m1 >= 10 || m2 >= 10)
                throw new MessageProcessorException("Invalid argument.");

            int d1 = ((int)bytes[1] & 0xf0) >> 4;
            int d2 = (int)bytes[1] & 0x0f;
            if (d1 >= 10 || d2 >= 10)
                throw new MessageProcessorException("Invalid argument.");
            
            int h1 = ((int)bytes[2] & 0xf0) >> 4;
            int h2 = (int)bytes[2] & 0x0f;
            if (h1 >= 10 || h2 >= 10)
                throw new MessageProcessorException("Invalid argument.");

            int mi1 = ((int)bytes[3] & 0xf0) >> 4;
            int mi2 = (int)bytes[3] & 0x0f;
            if (mi1 >= 10 || mi2 >= 10)
                throw new MessageProcessorException("Invalid argument.");

            int s1 = ((int)bytes[4] & 0xf0) >> 4;
            int s2 = (int)bytes[4] & 0x0f;
            if (s1 >= 10 || s2 >= 10)
                throw new MessageProcessorException("Invalid argument.");

            return new DateTime(DateTime.Now.Year, m1 * 10 + m2, d1 * 10 + d2,
                h1 * 10 + h2, mi1 * 10 + mi2, s1 * 10 + s2);
        }

        public static byte[] GetBytesFromMMDDHHMMSS(DateTime dt)
        {
            byte[] bytes = new byte[5];
            int m1 = dt.Month / 10;
            int m2 = dt.Month % 10;
            int d1 = dt.Day / 10;
            int d2 = dt.Day % 10;
            int h1 = dt.Hour / 10;
            int h2 = dt.Hour % 10;
            int mi1 = dt.Minute / 10;
            int mi2 = dt.Minute % 10;
            int s1 = dt.Second / 10;
            int s2 = dt.Second % 10;
            bytes[0] = (byte)((m1 << 4) | m2);
            bytes[1] = (byte)((d1 << 4) | d2);
            bytes[2] = (byte)((h1 << 4) | h2);
            bytes[3] = (byte)((mi1 << 4) | mi2);
            bytes[4] = (byte)((s1 << 4) | s2);
            return bytes;
        }
    }
}
