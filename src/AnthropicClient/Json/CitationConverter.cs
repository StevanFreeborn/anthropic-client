using System.Text.Json;
using System.Text.Json.Serialization;

using AnthropicClient.Models;

namespace AnthropicClient.Json;

class CitationConverter : JsonConverter<Citation>
{
  public override Citation Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    using var jsonDocument = JsonDocument.ParseValue(ref reader);
    var root = jsonDocument.RootElement;
    var type = root.GetProperty("type").GetString();
    return type switch
    {
      CitationType.CharacterLocation => JsonSerializer.Deserialize<CharacterLocationCitation>(root.GetRawText(), options)!,
      CitationType.PageLocation => JsonSerializer.Deserialize<PageLocationCitation>(root.GetRawText(), options)!,
      CitationType.ContentBlockLocation => JsonSerializer.Deserialize<ContentBlockLocationCitation>(root.GetRawText(), options)!,
      _ => throw new JsonException($"Unknown content type: {type}")
    };
  }

  public override void Write(Utf8JsonWriter writer, Citation value, JsonSerializerOptions options)
  {
    JsonSerializer.Serialize(writer, value, value.GetType(), options);
  }
}