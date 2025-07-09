using System.Text.Json.Serialization;

using AnthropicClient.Utils;

namespace AnthropicClient.Models;

/// <summary>
/// Represents a document source.
/// </summary>
public class DocumentSource : Base64Source
{
  [JsonConstructor]
  internal DocumentSource() : base(string.Empty, string.Empty)
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="DocumentSource"/> class.
  /// </summary>
  /// <param name="mediaType">The media type of the document.</param>
  /// <param name="data">The data of the document.</param>
  /// <exception cref="ArgumentException">Thrown when the media type is invalid.</exception>
  /// <exception cref="ArgumentNullException">Thrown when the media type or data is null.</exception>
  /// <returns>A new instance of the <see cref="DocumentSource"/> class.</returns>
  public DocumentSource(string mediaType, string data) : base(mediaType, data)
  {
  }
}