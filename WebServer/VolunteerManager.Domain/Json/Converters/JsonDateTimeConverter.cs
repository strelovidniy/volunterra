using Newtonsoft.Json;

namespace VolunteerManager.Domain.Json.Converters;

public class JsonDateTimeConverter : JsonConverter<DateTime>
{
    public override bool CanRead => false;

    public override void WriteJson(
        JsonWriter writer,
        DateTime value,
        JsonSerializer serializer
    ) => writer.WriteValue(value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ssZ"));

    public override DateTime ReadJson(
        JsonReader reader,
        Type objectType,
        DateTime existingValue,
        bool hasExistingValue,
        JsonSerializer serializer
    ) => DateTime.TryParse(reader.Value?.ToString(), out var value) ? value : DateTime.MinValue;
}