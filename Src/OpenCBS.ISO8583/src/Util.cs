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
using System.Reflection;
using System.Linq;

namespace Free.iso8583
{
    public static class Util
    {
        public static String[] PrintableClassPrefixes = null;

        public static String GetAssemblyDir(Object obj)
        {
            String dir = ((obj is Type) ? (Type)obj : obj.GetType()).Assembly.CodeBase;
            if (dir.StartsWith("file:"))
            {
                Uri uri = new Uri(dir);
                dir = uri.AbsolutePath;
            }
            int i = dir.LastIndexOf("/");
            if (i == -1) i = dir.LastIndexOf("\\");
            if (i != -1) dir = dir.Substring(0, i);
            return dir;
        }

        public static String Join(IEnumerable<Object> list, String separator)
        {
            return String.Join(separator, (from item in list select item.ToString()).ToArray());
        }

        public static String GetReadableStringFromModel(Object model)
        {
            return GetReadableStringFromModel(model, null);
        }

        public static String GetReadableStringFromModel(Object model, String[] printableClassPrefixes)
        {
            if (printableClassPrefixes == null) printableClassPrefixes = PrintableClassPrefixes;
            if (printableClassPrefixes == null) printableClassPrefixes = new String[] { };
            if (model == null) return "null";
            StringBuilder str = new StringBuilder();
            PrintModel(model, str, "", printableClassPrefixes);
            return str.ToString();
        }

        private static void PrintModel(Object model, StringBuilder str, String indent, String[] printableClassPrefixes)
        {
            String newLine = Environment.NewLine;
            str.Append(model.GetType().FullName).Append("()").Append(newLine);
            str.Append(indent).Append("{").Append(newLine);
            PropertyInfo[] Properties = model.GetType().GetProperties();
            foreach (PropertyInfo property in Properties)
            {
                Object value = property.CanRead ? property.GetValue(model, null) : "null";
                str.Append(indent).Append(property.Name).Append(" = ");
                if (value != null)
                {
                    if (property.PropertyType.IsEnum)
                    {
                        str.Append(property.PropertyType.Name).Append(".")
                            .Append(Enum.Format(property.PropertyType, value, "G"));
                    }
                    else if (IsPrintableClass(value.GetType(), printableClassPrefixes))
                    {
                        PrintModel(value, str, indent + "\t", printableClassPrefixes);
                    }
                    else if (value is byte[])
                    {
                        byte[] val = (byte[])value;
                        str.Append(MessageUtility.HexToReadableString(val));
                    }
                    else if (value is string[])
                    {
                        string[] val = (string[])value;
                        str.Append("[");
                        if (val.Length > 0) str.Append(String.Join(", ", val));
                        str.Append("]");
                    }
                    else
                        str.Append(value.ToString());
                }
                else
                    str.Append("null");
                str.Append(newLine);
            }
            str.Append(indent).Append("}");//.Append(newLine);
        }

        private static bool IsPrintableClass(Type type, String[] printableClassPrefixes)
        {
            String className = type.FullName;
            if (className.StartsWith("Free.iso8583.")) return true;
            for (int i = 0; i < printableClassPrefixes.Length; i++)
            {
                if (className.StartsWith(printableClassPrefixes[i])) return true;
            }
            return false;
        }

        public static MethodInfo GetConvertOperator(Type hostType, Type guestType, bool guestToHost)
        {
            String[] memberNames = new String[] { "op_Implicit", "op_Explicit" };
            Type fromType, toType;
            if (guestToHost)
            {
                fromType = guestType;
                toType = hostType;
            }
            else
            {
                fromType = hostType;
                toType = guestType;
            }
            for (int i = 0; i < memberNames.Length; i++)
            {
                MemberInfo[] members = hostType.GetMember(memberNames[i], MemberTypes.Method,
                    BindingFlags.Public | BindingFlags.Static);
                foreach (MemberInfo member in members)
                {
                    MethodInfo method = (MethodInfo)member;
                    if ((method.Attributes & MethodAttributes.SpecialName) != MethodAttributes.SpecialName)
                        continue; //SpecialName indicates it's really operator
                    ParameterInfo[] parameters = method.GetParameters();
                    if (parameters.Length != 1) continue;
                    if (parameters[0].ParameterType != fromType) continue;
                    if (method.ReturnType != toType) continue;
                    return method;
                }
            }
            return null;
        }

        public static Object GetAssignableValue(Type type, Object value)
        {
            if (value == null) return null;
            if (type.IsAssignableFrom(value.GetType())) return value;

            try
            {

                MethodInfo convertOperator = Util.GetConvertOperator(type, value.GetType(), true);
                if (convertOperator == null)
                    convertOperator = Util.GetConvertOperator(value.GetType(), type, false);
                if (convertOperator != null)
                {
                    return convertOperator.Invoke(null, new Object[] { value });
                }

                ConstructorInfo constructor = type.GetConstructor(new Type[] { value.GetType() });
                if (constructor != null)
                {
                    return constructor.Invoke(new Object[] { value });
                }

                if (value.GetType() == typeof(String))
                {
                    MethodInfo parseMethod = type.GetMethod("Parse", BindingFlags.Public | BindingFlags.Static, null,
                        new Type[] { typeof(String) }, null);
                    if (parseMethod != null)
                    {
                        return parseMethod.Invoke(null, new Object[] { value });
                    }

                }

                if (type == typeof(String))
                {
                    MethodInfo toStringMethod = value.GetType().GetMethod("ToString", new Type[] { });
                    if (toStringMethod != null && toStringMethod.DeclaringType != typeof(Object)) //ToString method has been overridden
                    {
                        return toStringMethod.Invoke(value, new Object[] { });
                    }
                }
            }
            catch
            {
                return null;
            }
            return null;
        }
    }
}
