using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Free.iso8583;

namespace Iso8583WebClient
{
    public class DateTimeConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, Object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            DateTime dt = (DateTime)value;
            int i = dt.Year.ToString().Length - 2;
            if (i < 0) i = 0;
            writer.WriteValue(dt.Year.ToString().Substring(i).PadLeft(2, '0') + "/" + dt.Month.ToString().PadLeft(2, '0'));
        }

        public override Object ReadJson(JsonReader reader, Type objectType, Object existingValue, JsonSerializer serializer)
        {
            if (existingValue == null) return null;
            return GetDateTime(existingValue.ToString());
        }

        public static DateTime GetDateTime(String dateString)
        {
            String[] dt = dateString.Split('/');
            int year, month;
            if (dt.Length < 2 || !int.TryParse(dt[0], out year) || !int.TryParse(dt[1], out month))
            {
                return new DateTime(0);
            }
            return new DateTime((DateTime.Now.Year / 100) * 100 + year, month, 1);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime?) || objectType == typeof(DateTime);
        }
    }
}