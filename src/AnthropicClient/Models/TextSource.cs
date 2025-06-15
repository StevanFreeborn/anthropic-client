using System.Text.Json.Serialization;

namespace AnthropicClient.Models;

/// <summary>
/// Represents a text document source.
/// </summary>
public class TextSource : Source
{
  /// <summary>
  /// Gets the media type of the source.
  /// </summary>
  [JsonPropertyName("media_type")]
  public string MediaType { get; } = "text/plain";

  /// <summary>
  /// Gets the data of the source.
  /// </summary>
  public string Data { get; init; } = string.Empty;

  /// <summary>
  /// Initializes a new instance of the <see cref="TextSource"/> class.
  /// </summary>
  /// <param name="data">The data of the document.</param>
  /// <exception cref="ArgumentNullException">Thrown when the data is null.</exception>
  /// <returns>A new instance of the <see cref="TextSource"/> class.</returns>
  public TextSource(string data) : base(SourceType.Text)
  {
    Data = data;
  }
}