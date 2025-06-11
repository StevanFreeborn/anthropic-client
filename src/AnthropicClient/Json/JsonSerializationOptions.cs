using System.Text.Json;
using System.Text.Json.Serialization;

namespace AnthropicClient.Json;

static class JsonSerializationOptions
{
  public static JsonSerializerOptions DefaultOptions => new()
  {
    PropertyNameCaseInsensitive = true,
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    Converters =
    {
      new ContentConverter(),
      new ToolChoiceConverter(),
      new ErrorConverter(),
      new EventDataConverter(),
      new ContentDeltaConverter(),
      new JsonStringEnumConverter(),
      new MessageBatchResultConverter(),
      new CitationConverter(),
    },
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
  };
}