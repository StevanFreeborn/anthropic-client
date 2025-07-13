using System.Text.Json.Serialization;

namespace AnthropicClient.Models;

/// <summary>
/// Represents an image source.
/// </summary>
public class ImageSource : Base64Source
{
  [JsonConstructor]
  internal ImageSource() : base(string.Empty, string.Empty)
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="ImageSource"/> class.
  /// </summary>
  /// <param name="mediaType">The media type of the image.</param>
  /// <param name="data">The data of the image.</param>
  /// <exception cref="ArgumentException">Thrown when the media type is invalid.</exception>
  /// <exception cref="ArgumentNullException">Thrown when the media type or data is null.</exception>
  /// <returns>A new instance of the <see cref="ImageSource"/> class.</returns>
  public ImageSource(string mediaType, string data) : base(mediaType, data)
  {
    if (ImageType.IsValidImageType(mediaType) is false)
    {
      throw new ArgumentException($"Invalid media type: {mediaType}");
    }

    MediaType = mediaType;
    Data = data;
  }
}