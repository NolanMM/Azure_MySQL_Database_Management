using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cloud_Database_Management_System
{
    public class DateOnlyConverter : JsonConverter<DateOnly>
    {
        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Parse the DateOnly value from the JSON string
            return DateOnly.Parse(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        {
            // Write the DateOnly value as a JSON string
            writer.WriteStringValue(value.ToString());
        }
    }
}
