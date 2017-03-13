using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Free.iso8583;

namespace Iso8583WebClient
{
    public class ByteArrayConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, Object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            writer.WriteValue(MessageUtility.HexToString((byte[])value));
        }

        public override Object ReadJson(JsonReader reader, Type objectType, Object existingValue, JsonSerializer serializer)
        {
            return MessageUtility.StringToHex(existingValue.ToString(), true);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(byte[]);
        }
    }
}