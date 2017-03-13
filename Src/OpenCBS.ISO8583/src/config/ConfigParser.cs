using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Free.iso8583.config
{
    internal interface IConfigParser
    {
        void Parse();
    }

    internal abstract class ConfigParser : IConfigParser
    {
        public abstract void Parse();

        protected static Object GetInstanceOf(Type type)
        {
            //ConstructorInfo constructor = type.GetConstructor(new Type[] { });
            //if (constructor == null)
            //    throw new ConfigParserException("Type " + type.FullName + " doesn't have the constructor with no parameter.");
            MethodInfo getInstance = type.GetMethod("GetInstance", Type.EmptyTypes);
            if (getInstance != null && getInstance.IsStatic) return getInstance.Invoke(null, new Object[] { });
            return Activator.CreateInstance(type);
        }

        protected static void HookPropertyToField(ModelPropertyConfig propertyConfig, MessageFieldConfig fieldConfig,
            string fieldType)
        {
            propertyConfig.FieldBit = fieldConfig;
            ParameterModifier[] pm = null;// new ParameterModifier[] { };
            switch (fieldType.ToLower())
            {
                case "string":
                    propertyConfig.GetValueFromBytes = fieldConfig.FieldType.GetProperty("StringValue");
                    propertyConfig.SetValueFromProperty = fieldConfig.FieldType.GetMethod("SetValue",
                        BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance,
                        null, new Type[] { typeof(String) }, pm);
                    //propertyConfig.SetValueFromProperty = fieldConfig.FieldType.GetMethod("SetValue", new Type[] { typeof(String) });
                    break;
                case "int":
                    propertyConfig.GetValueFromBytes = fieldConfig.FieldType.GetProperty("IntValue");
                    propertyConfig.SetValueFromProperty = fieldConfig.FieldType.GetMethod("SetValue",
                        BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance,
                        null, new Type[] { typeof(int) }, pm);
                    //propertyConfig.SetValueFromProperty = fieldConfig.FieldType.GetMethod("SetValue", new Type[] { typeof(int) });
                    break;
                case "decimal":
                    propertyConfig.GetValueFromBytes = fieldConfig.FieldType.GetProperty("DecimalValue");
                    propertyConfig.SetValueFromProperty = fieldConfig.FieldType.GetMethod("SetValue",
                        BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance,
                        null, new Type[] { typeof(decimal) }, pm);
                    //propertyConfig.SetValueFromProperty = fieldConfig.FieldType.GetMethod("SetValue", new Type[] { typeof(decimal) });
                    break;
                case "bytes":
                    propertyConfig.GetValueFromBytes = fieldConfig.FieldType.GetProperty("BytesValue",
                        BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                    propertyConfig.SetValueFromProperty = fieldConfig.FieldType.GetMethod("SetValue",
                        BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance,
                        null, new Type[] { typeof(byte[]) }, pm);
                    //propertyConfig.SetValueFromProperty = fieldConfig.FieldType.GetMethod("SetValue", new Type[] { typeof(byte[]) });
                    break;
            }
        }
    }
}
