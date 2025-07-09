using System.Text.Json.Serialization;

using AnthropicClient.Utils;

namespace AnthropicClient.Models;

/// <summary>
/// Represents image content that is part of a message.
/// </summary>
public class ImageContent : Content
{
  /// <summary>
  /// Gets the source of the image.
  /// </summary>
  public Source Source { get; init; } = new ImageSource();

  [JsonConstructor]
  internal ImageContent()
  {
  }

  private void Validate(string mediaType, string data)
  {
    ArgumentValidator.ThrowIfNull(mediaType, nameof(mediaType));
    ArgumentValidator.ThrowIfNull(data, nameof(data));
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="ImageContent"/> class.
  /// </summary>
  /// <param name="mediaType">The media type of the image.</param>
  /// <param name="data">The data of the image.</param>
  /// <exception cref="ArgumentNullException">Thrown when the media type or data is null.</exception>
  /// <returns>A new instance of the <see cref="ImageContent"/> class.</returns>
  public ImageContent(string mediaType, string data) : base(ContentType.Image)
  {
    Validate(mediaType, data);

    Source = new ImageSource(mediaType, data);
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="ImageContent"/> class.
  /// </summary>
  /// <param name="mediaType">The media type of the image.</param>
  /// <param name="data">The data of the image.</param>
  /// <param name="cacheControl">The cache control to be used for the content.</param>
  /// <returns>A new instance of the <see cref="ImageContent"/> class.</returns>
  /// <exception cref="ArgumentNullException">Thrown when the media type, data, or cache control is null.</exception>
  public ImageContent(string mediaType, string data, CacheControl cacheControl) : base(ContentType.Image, cacheControl)
  {
    Validate(mediaType, data);

    Source = new ImageSource(mediaType, data);
  }
}