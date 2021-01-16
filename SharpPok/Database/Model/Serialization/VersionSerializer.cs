using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SharpPok.Database.Model.Serialization
{
    public class VersionSerializer : JsonConverter<Version>
    {
        public override Version? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return null;
        }

        public override void Write(Utf8JsonWriter writer, Version value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.Name);
        }
    }
}