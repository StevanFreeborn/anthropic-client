using System.Text.Json;
using System.Text.Json.Serialization;

using AnthropicClient.Models;

namespace AnthropicClient.Json;

class SourceConverter : JsonConverter<Source>
{
  public override Source Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    using var jsonDocument = JsonDocument.ParseValue(ref reader);
    var root = jsonDocument.RootElement;
    var type = root.GetProperty("type").GetString();
    return type switch
    {
      SourceType.Text => JsonSerializer.Deserialize<TextSource>(root.GetRawText(), options)!,
      SourceType.Content => JsonSerializer.Deserialize<CustomSource>(root.GetRawText(), options)!,
      SourceType.File => JsonSerializer.Deserialize<FileSource>(root.GetRawText(), options)!,
      SourceType.Url => JsonSerializer.Deserialize<UrlSource>(root.GetRawText(), options)!,
      SourceType.Base64 => DeserializeBase64Source(root, options),
      _ => throw new JsonException($"Unknown source type: {type}")
    };
  }

  private static Source DeserializeBase64Source(JsonElement root, JsonSerializerOptions options)
  {
    var mediaType = root.TryGetProperty("media_type", out var mediaTypeElement)
      ? mediaTypeElement.GetString() ?? throw new JsonException("Missing 'media_type' property")
      : throw new JsonException("Missing 'media_type' property");

    var isImage = ImageType.IsValidImageType(mediaType);

    return isImage
      ? JsonSerializer.Deserialize<ImageSource>(root.GetRawText(), options)!
      : JsonSerializer.Deserialize<DocumentSource>(root.GetRawText(), options)!;
  }

  public override void Write(Utf8JsonWriter writer, Source value, JsonSerializerOptions options)
  {
    if (value is TextSource textSource)
    {
      JsonSerializer.Serialize(writer, textSource, options);
      return;
    }

    if (value is CustomSource customSource)
    {
      JsonSerializer.Serialize(writer, customSource, options);
      return;
    }

    if (value is Base64Source base64Source)
    {
      JsonSerializer.Serialize(writer, base64Source, options);
      return;
    }

    if (value is FileSource fileSource)
    {
      JsonSerializer.Serialize(writer, fileSource, options);
      return;
    }

    if (value is UrlSource urlSource)
    {
      JsonSerializer.Serialize(writer, urlSource, options);
      return;
    }

    JsonSerializer.Serialize(writer, value, value.GetType(), options);
  }
}