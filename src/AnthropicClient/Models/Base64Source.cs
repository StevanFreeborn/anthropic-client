using System.Text.Json.Serialization;

using AnthropicClient.Utils;

namespace AnthropicClient.Models;

/// <summary>
/// Represents an base64 source.
/// </summary>
public class Base64Source : Source
{
  /// <summary>
  /// Gets the media type of the source.
  /// </summary>
  [JsonPropertyName("media_type")]
  public string MediaType { get; init; } = string.Empty;

  /// <summary>
  /// Gets the data of the source.
  /// </summary>
  public string Data { get; init; } = string.Empty;

  /// <summary>
  /// Initializes a new instance of the <see cref="Base64Source"/> class.
  /// </summary>
  /// <param name="mediaType">The media type of the source.</param>
  /// <param name="data">The data of the source.</param>
  /// <exception cref="ArgumentException">Thrown when the media type is invalid.</exception>
  /// <exception cref="ArgumentNullException">Thrown when the media type or data is null.</exception>
  /// <returns>A new instance of the <see cref="Base64Source"/> class.</returns>
  public Base64Source(string mediaType, string data) : base(SourceType.Base64)
  {
    ArgumentValidator.ThrowIfNull(mediaType, nameof(mediaType));
    ArgumentValidator.ThrowIfNull(data, nameof(data));

    MediaType = mediaType;
    Data = data;
  }
}