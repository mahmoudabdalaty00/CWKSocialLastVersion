using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Service
{
    public class CustomDateTimeConverter : JsonConverter<DateTime>
    {
        private readonly string _format;

        public CustomDateTimeConverter() : this("yyyy-MM-dd") { }

        public CustomDateTimeConverter(string format)
        {
            _format = format;
        }

        public override DateTime ReadJson(JsonReader reader, Type objectType, DateTime existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.Value == null || reader.TokenType == JsonToken.Null)
            {
                return GetDefaultValue(objectType);
            }

            if (reader.Value is DateTime dt)
            {
                return dt;
            }

            var value = reader.Value.ToString()?.Trim();

            if (string.IsNullOrEmpty(value))
            {
                return GetDefaultValue(objectType);
            }

            if (DateTime.TryParse(value, out var parsed))
            {
                return parsed;
            }

            if (DateTime.TryParseExact(value, _format, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsed))
            {
                return parsed;
            }

            var extraFormats = new[] { "dd/MM/yyyy", "MM/dd/yyyy", "yyyy-MM-ddTHH:mm:ss" };
            if (DateTime.TryParseExact(value, extraFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsed))
            {
                return parsed;
            }

            return GetDefaultValue(objectType);
        }

        public override void WriteJson(JsonWriter writer, DateTime value, JsonSerializer serializer)
        {
            writer.WriteValue(value == default ? null : value.ToString(_format, CultureInfo.InvariantCulture));
        }

        private static DateTime GetDefaultValue(Type objectType)
        {
            return Nullable.GetUnderlyingType(objectType) != null
                ? default(DateTime) 
                : default;          
        }
    }

}
