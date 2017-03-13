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
using System.Linq;
using System.Text;
using System.Reflection;

namespace Free.iso8583
{
    public static class MessageUtility
    {
        private static char[] hexDigits = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
        private static IDictionary<char, int> hexDigitsToInt = new Dictionary<char, int>() {
            {'0', 0}, {'1', 1}, {'2', 2}, {'3', 3}, {'4', 4}, {'5', 5}, {'6', 6}, {'7', 7}, {'8', 8}, {'9', 9},
            {'A', 10}, {'B', 11}, {'C', 12}, {'D', 13}, {'E', 14}, {'F', 15},
            {'a', 10}, {'b', 11}, {'c', 12}, {'d', 13}, {'e', 14}, {'f', 15}
        };

        /// <summary>
        /// Converts a byte value contains two digits hexadecimal to integer. 
        /// Example: val parameter is "15" hexadecimal then this method must return "15" integer, 
        /// "15" hexadecimal is not translated to "21" decimal. If the either digit is greater than or equal "A" hexadecimal
        /// then it will be translated as is, example "A5" will be "165" integer.
        /// </summary>
        public static uint HexToInt(byte val)
        {
            int lower = val & 0x0f;
            int upper = val >> 4;
            if (upper >= 10 || lower >= 10) return val; 
            return (uint)(upper*10 + lower);
        }

        /// <summary>
        /// The reverse of HexToInt(byte val) method. Assumes the hex value lower than 100
        /// and no hexadimal digit between "A" and "F"
        /// </summary>
        public static byte IntToHex2Digit(uint hex)
        {
            byte hex2 = (byte)(hex % 100); // between 0 - 99
            byte retVal = (byte)(hex2 % 10);// between 0 - 9
            hex2 = (byte)(hex2 / 10); //tens value, between 0 - 9 (must be in lower four bits)
            retVal = (byte)((hex2 << 4) //shift the tens value to upper four bits
                   + retVal); //replace the unit value in the lower nibble
            return retVal;
        }

        public static int AsciiArrayToInt(byte[] val)
        {
            if (val == null) return 0;
            int value = 0;
            int exp = 1;
            for (int i = val.Length-1; i>=0; i--)
            {
                int digit = val[i] - 48;
                if (digit < 0 || digit > 9) return -1;
                value = value + digit * exp;
                exp *= 10;
            }
            return value;
        }

        /// <summary>
        /// The same as calling repeatedly HexToInt method and calculate their result
        /// as if all entries in the array are a consecutive digits. If there is a digit greater than
        /// or equal "A" hexadecimal then it will calculate from the true value of each byte.
        /// </summary>
        public static uint HexNDigitsToInt(byte[] val)
        {
            if (val == null) return 0;
            bool isTrueHex = false;
            uint retVal = 0;
            ulong denom = 1;
            for (int i = val.Length-1; i >= 0; i--)
            {
                uint lower = (uint)(val[i] & 0x0f);
                uint upper = (uint)(val[i] >> 4);
                isTrueHex = isTrueHex || (upper >= 10 || lower >= 10); 
                lower = lower*(uint)denom;
                denom = denom*10;
                retVal += upper*(uint)denom + lower;
                denom = denom*10;
            }
            if (isTrueHex)
            {
                retVal = 0;
                for (int i = 0; i < val.Length; i++)
                {
                    retVal = retVal << 8;
                    retVal = retVal | val[i];
                }
            }
            return retVal;
        }

        /// <summary>
        /// Converts an array of byte values to ulong value as if they are the consecutive bytes for the ulong type.
        /// </summary>
        public static ulong BytesToInt(byte[] val)
        {
            if (val == null) return 0;
            ulong retVal = 0;
            for (int i = 0; i < val.Length; i++)
            {
                retVal = retVal << 8;
                retVal = retVal | val[i];
            }
            return retVal;
        }

        public static byte[] IntToBytes(ulong val, int length)
        {
            byte[] bytes = new byte[length];
            for (int i = length - 1; i >= 0; i--)
            {
                bytes[i] = (byte)(val & 0xff);
                val = val >> 8;
            }
            return bytes;
        }

        public static byte CharToByte(char ch)
        {
            return System.Text.Encoding.ASCII.GetBytes(ch + "")[0];
        }

        /// <summary>
        /// Converts a hexadecimal in a bytes array to a decimal value. One byte contains two hexadecimal digits.
        /// Hexadecimal "10" means "10" decimal, not "16". All zero values on the left side will be removed.
        /// The fracDigits parameter determines how many digits after decimal point.
        /// No decimal point will be included in the bytes array, ex. "10.11" contains two digits (one byte)
        /// after decimal point, so the last byte in hex parameter contains 17 value (11 hexadecimal)
        /// and the other bytes will contain the mantisa.
        /// </summary>
        public static decimal HexToDecimal(byte[] hex, int fracDigits)
        {
            if (hex == null) return 0;
            decimal retVal = 0;
            decimal denom = 1;
            if (hex.Length > 0) retVal = HexToInt(hex[0]);
            for (int i = 1; i < hex.Length; i++)
            {
                denom *= 100;
                //Assumes no hex digit is between "A" and "F"
                retVal = retVal * 100 + HexToInt(hex[i]);
            }
            retVal = retVal / (decimal)Math.Pow(10, fracDigits);
            return retVal;
        }

        /// <summary>
        /// The same as HexToDecimal but return integer value.
        /// </summary>
        public static ulong HexToInt(byte[] hex)
        {
            if (hex == null) return 0;
            ulong retVal = 0;
            ulong denom = 1;
            if (hex.Length > 0) retVal = (uint)HexToInt(hex[0]);
            for (int i = 1; i < hex.Length; i++)
            {
                denom *= 100;
                //Assumes no hex digit is between "A" and "F"
                retVal += (ulong)HexToInt(hex[i]) * denom;
            }
            return retVal;
        }

        private static void HexToChars(byte hex, char[] chars, int idx)
        {
            int lower = (hex & 0x0f);
            int upper = (hex >> 4);
            chars[idx] = hexDigits[upper];
            chars[idx+1] = hexDigits[lower];
        }

        /// <summary>
        /// Converts a hexadecimal in a byte to be a string. One byte will produce two characters.
        /// </summary>
        public static String HexToString(byte hex)
        {
            char[] chars = new char[2];
            HexToChars(hex, chars, 0);
            return new String(chars);
        }

        /// <summary>
        /// Converts a hexadecimal in a bytes array to be a string. One byte will produce two characters.
        /// </summary>
        public static String HexToString(byte[] hex)
        {
            if (hex == null) return null;
            char[] chars = new char[hex.Length*2];
            for (int i = 0, j = 0; i < hex.Length; i++, j+=2)
            {
               HexToChars(hex[i], chars, j);
            }
            return new String(chars);
        }

        public static String HexToReadableString(byte[] hex, int maxCharsPerLine = 0)
        {
            if (hex == null || hex.Length <= 0) return "";

            //3x - 1 >= maxCharsPerLine  :::  x >= (maxCharsPerLine + 1) / 3  ::: x ~= max items in hex which fulfil maxCharsPerLine 

            StringBuilder ret = new StringBuilder();
            char[] chars;
            int x = maxCharsPerLine > 0 ? (maxCharsPerLine + 1) / 3 : hex.Length;
            int i = 0, maxLine = x;
            while (true)
            {
                int k = maxLine - 1;
                if (k >= hex.Length) k = hex.Length - 1;
                for (; i < k; i++)
                {
                    chars = new char[3];
                    HexToChars(hex[i], chars, 0);
                    chars[2] = ' ';
                    ret.Append(chars);
                }
                chars = new char[2];
                HexToChars(hex[i++], chars, 0);
                ret.Append(chars);
                if (i < hex.Length) ret.Append(Environment.NewLine); else break;
                maxLine += x;
            }

            return ret.ToString();
        }

        /// <summary>
        /// Converts a hexadecimal in a bytes array to be a string. One byte will produce two characters.
        /// If there should be an odd number of characters then the first character will be removed.
        /// </summary>
        public static String HexToString(byte[] hex, bool isOdd)
        {
            String retVal = HexToString(hex);
            if (isOdd && retVal != null) return retVal.Substring(1);
            return retVal;
        }

        /// <summary>
        /// Converts a hexadecimal in a bytes array to be a string. One byte will produce two characters.
        /// If there should be an odd number of characters then the first/last character will be removed.
        /// </summary>
        public static String HexToString(byte[] hex, bool isOdd, bool isLeftAligned)
        {
            String retVal = HexToString(hex);
            if (isOdd && retVal != null)
            {
                if (isLeftAligned) return retVal.Substring(0, retVal.Length - 1);
                return retVal.Substring(1);
            }
            return retVal;
        }

        /// <summary>
        /// The reverse of HexToDecimal method. The length parameter determines how many bytes to be returned,
        /// will be padded by zero on left side.
        /// </summary>
        public static byte[] DecimalToHex(decimal val, int fracDigits, int length)
        {
            if (length % 2 == 1) length++;
            length = length / 2;
            byte[] retVal = new byte[length];
            decimal val2 = Math.Round(val * (decimal)Math.Pow(10, fracDigits));
            for (int i = length - 1; i >= 0; i--)
            {
                uint byt = (uint)(val2 % 100);
                retVal[i] = IntToHex2Digit(byt);
                val2 = (val2 - byt) / 100;
                if (val2 <= 0) break;
            }
            return retVal;
        }

        /// <summary>
        /// The same as the above method but returns the bytes as many as needed.
        /// </summary>
        public static byte[] DecimalToHex(decimal val, int fracDigits)
        {
            IList<byte> retVal = new List<byte>();
            decimal val2 = Math.Round(val * (decimal)Math.Pow(10, fracDigits));
            while (true)
            {
                uint byt = (uint)(val2 % 100);
                retVal.Add(IntToHex2Digit(byt));
                val2 = (val2 - byt) / 100;
                if (val2 < 1) break;
            }
            if (fracDigits % 2 == 0 && retVal.Count == fracDigits / 2) //If the result is only the fraction part
                retVal.Add(0); //Place 0 for the integer part
            return retVal.Reverse().ToArray();
        }

        /// <summary>
        /// The reverse of HexToInt method. The length parameter determines how many bytes to be returned,
        /// will be padded by zero on left side.
        /// </summary>
        public static byte[] IntToHex(ulong val, int length)
        {
            byte[] retVal = new byte[length];
            for (int i = length - 1; i >= 0; i--)
            {
                uint byt = (uint)(val % 100);
                retVal[i] = IntToHex2Digit(byt);
                val = (val - byt) / 100;
                if (val <= 0) break;
            }
            if (val > 0)
            {
                throw new MessageProcessorException("Too big value to convert to nibble digits. Value = " + val
                    + ", bytes length = " + length);
            }
            return retVal;
        }

        /// <summary>
        /// The reverse of HexToInt method. The length parameter determines how many bytes to be returned,
        /// will be padded by zero on right side (left justified).
        /// </summary>
        public static byte[] IntToHexLeftAligned(ulong val, int length)
        {
            byte[] retVal = new byte[length];
            ulong val2 = val;
            int digits = 0;
            while (val2 > 0)
            {
                val2 = val2 / 10;
                digits++;
            }
            if (digits % 2 == 1) { val = val * 10; digits++; }
            int bytes = digits / 2;
            byte[] retVal2 = IntToHex(val, bytes);
            retVal2.CopyTo(retVal, 0);
            return retVal;
        }

        /// <summary>
        /// The same as the above IntToHex method but returns the bytes as many as needed.
        /// </summary>
        public static byte[] IntToHex(ulong val)
        {
            ulong val2 = val;
            int digits = 0;
            while (val2 > 0)
            {
                val2 = val2 / 10;
                digits++;
            }
            if (digits % 2 == 1) digits++;
            int bytes = digits / 2;
            return IntToHex(val, bytes);
        }

        /// <summary>
        /// The reverse of HexToString method.
        /// </summary>
        public static byte[] StringToHex(String val, bool isLeftAligned)
        {
            if (val == null) return null;
            int length = val.Length;
            if (length % 2 == 1)
            {
                val = isLeftAligned ? val + "0" : "0" + val;
                length++;
            }
            length = length / 2;
            byte[] retVal = new byte[length];
            char[] chars = val.ToUpper().ToCharArray();
            for (int i = 0; i < length; i++)
            {
                char ch = chars[2 * i];
                if (!hexDigitsToInt.ContainsKey(ch)) return null;
                int upper = hexDigitsToInt[ch];
                ch = chars[2 * i + 1];
                if (!hexDigitsToInt.ContainsKey(ch)) return null;
                int lower = hexDigitsToInt[ch];
                retVal[i] = (byte)((upper << 4) | lower);
            }
            return retVal;
        }
        
        public static byte[] StringToHex(String val)
        {
            return StringToHex(val, false);
        }

        public static byte[] StringToHex(String val, int length, bool isLeftAligned)
        {
            if (val != null)
            {
                if (val.Length < length)
                {
                    val = val.PadLeft(length, '0');
                }
                else if (val.Length > length)
                {
                    val = val.Substring(val.Length - length, length);
                }
            }
            return StringToHex(val, isLeftAligned);
        }

        public static byte[] StringToHex(String val, int length)
        {
            return StringToHex(val, length, false);
        }

        public static byte[] DecimalToAsciiArray(decimal val, int fracDigits)
        {
            if (fracDigits < 0) return null;
            val = (decimal)Math.Round(val * (decimal)Math.Pow(10, fracDigits));
            IList<byte> ascii = new List<byte>();
            while (true) {
                byte digit = (byte)(val % 10);
                ascii.Insert(0, (byte)(0x30 + digit));
                val = (val-digit)/10;
                if (val < 1) break;
            }
            if (ascii.Count == fracDigits) //If the result is only the fraction part
                ascii.Add(0x30); //Place 0 for the integer part
            return ascii.ToArray();
        }

        public static decimal? StringToDecimal(String val)
        {
            if (String.IsNullOrEmpty(val)) return null;
            val = val.Trim();
            int pointPos = val.IndexOf(".");
            if (pointPos != val.LastIndexOf(".")) return null;
            if (pointPos >= 0) val = val.Substring(0, pointPos) + val.Substring(pointPos + 1, val.Length - pointPos - 1);
            int pow = pointPos < 0 ? val.Length - 1 : pointPos - 1;
            double n = 0;
            char[] chars = val.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                char c = chars[i];
                if (!hexDigitsToInt.ContainsKey(c)) return null;
                int digit = hexDigitsToInt[c];
                if (digit > 9) return null;
                n += digit * Math.Pow(10, pow);
                pow--;
            }
            return (decimal?)n;
        }

        /// <summary>
        /// Converts a bytes array to be a string. One byte represents an ANSI character code, one character.
        /// </summary>
        public static String AsciiArrayToString(byte[] ansiChars)
        {
            if (ansiChars == null) return null;
            if (ansiChars.Length <= 0) return "";
            for (int i = 0; i < ansiChars.Length; i++)
            {
                if (ansiChars[i] < 32 || ansiChars[i] > 126) //non typeable characters
                    return null;
            }
            return System.Text.Encoding.ASCII.GetString(ansiChars);
        }

        /// <summary>
        /// The reverse of AsciiArrayToString method. The length parameter determines how many bytes to be returned,
        /// will be padded by space on the right side.
        /// </summary>
        public static byte[] StringToAsciiArray(String val, int length)
        {
            if (val == null) return null;
            byte[] retVal = new byte[length];
            byte[] ansi = System.Text.Encoding.ASCII.GetBytes(val);
            if (ansi.Length > length) {
                Array.Copy(ansi, retVal, length);
            } else {
                ansi.CopyTo(retVal, 0);
                for (int i=ansi.Length; i<length; i++) retVal[i] = 0x20; //space
            }
            return retVal;
        }

        /// <summary>
        /// The same as the above method but returns the bytes as many as needed.
        /// </summary>
        public static byte[] StringToAsciiArray(String val)
        {
            if (val == null) return null;
            return System.Text.Encoding.ASCII.GetBytes(val);
        }

        /// <summary>
        /// Returns a string represents the value of each bit, 0 or 1.
        /// </summary>
        public static String GetBitString(byte[] bytes)
        {
            if (bytes == null) return null;
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                byte byt = bytes[i];
                for (int j=0; j<8; j++) {
                    str.Append((byt & 0x80) == 0 ? "0" : "1");
                    byt = (byte)(byt << 1);
                }
            }
            return str.ToString();
        }

        /// <summary>
        /// Returns a string representing the hexadecimal value inside the hex parameter. The length of the hex array is variable.
        /// To determine how many entries inside this array, there are some bytes in the beginning to note the length after.
        /// The count of how many these beginning bytes is passed via headerLength parameter.
        /// Example: headerLength parameter is 1 and the first byte in the hex parameter is "37" hexadecimal,
        /// then there are 37 digits hexadecimal (19 bytes) after the first byte. Thus, hex  parameter contains 20 bytes,
        /// 1 byte of header and 19 bytes of content. Notice in the example before, "37" hexadecimal is not translated
        /// to "55" decimal, except there is a digit greater than or equal "A" hexadecimal.
        /// Note, if the content has the odd number of digits, it must be padded by zero at the right side (left justified).
        /// </summary>
        public static String HexVarlenToString(byte[] hex, int headerLength)
        {
            if (hex == null || headerLength > hex.Length) return null;
            byte[] header = new byte[headerLength];
            Array.Copy(hex, header, headerLength);
            int contentLength = (int)HexNDigitsToInt(header);
            if (contentLength == 0) return "";

            bool isOdd = (contentLength % 2 == 1);
            //There are contentLength (4-bits) in the content
            if (isOdd)
                contentLength = (contentLength + 1) / 2; //so the actual count of bytes
            else
                contentLength = contentLength / 2;
            
            int length = hex.Length - headerLength;
            if (contentLength > length) { contentLength = length; isOdd = false; }
            byte[] content = new byte[contentLength];
            Array.Copy(hex, headerLength, content, 0, contentLength);
            if (isOdd) content[contentLength - 1] = (byte)(content[contentLength - 1] & 0xf0); //The last four-bits has no digit (filled by 0) because of the odd number of digits
            return HexToString(content);
        }

        /// <summary>
        /// Similar with HexVarlenToString method but for ansi code (each code 1 byte).
        /// The headerLength parameter is still in hexadecimal digit.
        /// Example: the headerLength parameter is 2 and two first bytes of ansi array contains "0156"
        /// then ansi array has 158 entries (2 bytes of header and 156 bytes of content).
        /// </summary>
        public static String AsciiVarlenToString(byte[] ansi, int headerLength)
        {
            if (ansi == null || headerLength > ansi.Length) return null;
            byte[] header = new byte[headerLength];
            Array.Copy(ansi, header, headerLength);
            int contentLength = (int)HexNDigitsToInt(header);
            if (contentLength == 0) return "";

            int length = ansi.Length - headerLength;
            if (contentLength > length) contentLength = length;
            byte[] content = new byte[contentLength];
            Array.Copy(ansi, headerLength, content, 0, contentLength);
            return System.Text.Encoding.ASCII.GetString(content);
        }

        /// <summary>
        /// The reverse of HexVarlenToString method.
        /// </summary>
        public static byte[] StringToHexVarlen(String val, int headerLength)
        {
            if (val == null) return null;
            byte[] header = IntToHex((ulong)val.Length, headerLength);
            
            int contentLength = val.Length / 2; //The actual number of bytes if even length of string
            if (val.Length % 2 == 1) contentLength++; //If odd number of chars then the actual number of byte plus 1
            
            byte[] content = StringToHex(val, true);
            return (byte[])header.Concat(content);
        }

        /// <summary>
        /// The reverse of AsciiVarlenToString method.
        /// </summary>
        public static byte[] StringToAsciiVarlen(String val, int headerLength)
        {
            if (val == null) return null;
            byte[] header = IntToHex((ulong)val.Length, headerLength);
            byte[] content = StringToAsciiArray(val);
            return (byte[])header.Concat(content);
        }
    }
}
