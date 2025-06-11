using System.Text.Json.Serialization;

using AnthropicClient.Utils;

namespace AnthropicClient.Models;

/// <summary>
/// Represents content from a document that is part of a message.
/// </summary>
public class DocumentContent : Content
{
  /// <summary>
  /// Gets the source of the document.
  /// </summary>
  public DocumentSource Source { get; init; } = new();

  public string Title { get; init; } = string.Empty;

  public string Context { get; init; } = string.Empty;

  public CitationOption Citations { get; init; } = new CitationOption();

  [JsonConstructor]
  internal DocumentContent()
  {
  }

  private void Validate(string mediaType, string data)
  {
    ArgumentValidator.ThrowIfNull(mediaType, nameof(mediaType));
    ArgumentValidator.ThrowIfNull(data, nameof(data));
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="DocumentContent"/> class.
  /// </summary>
  /// <param name="mediaType">The media type of the document.</param>
  /// <param name="data">The data of the document.</param>
  /// <exception cref="ArgumentNullException">Thrown when the media type or data is null.</exception>
  /// <returns>A new instance of the <see cref="DocumentContent"/> class.</returns>
  public DocumentContent(string mediaType, string data) : base(ContentType.Document)
  {
    Validate(mediaType, data);

    Source = new(mediaType, data);
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="DocumentContent"/> class.
  /// </summary>
  /// <param name="mediaType">The media type of the document.</param>
  /// <param name="data">The data of the document.</param>
  /// <param name="cacheControl">The cache control to be used for the content.</param>
  /// <returns>A new instance of the <see cref="DocumentContent"/> class.</returns>
  /// <exception cref="ArgumentNullException">Thrown when the media type, data, or cache control is null.</exception>
  public DocumentContent(string mediaType, string data, CacheControl cacheControl) : base(ContentType.Document, cacheControl)
  {
    Validate(mediaType, data);

    Source = new(mediaType, data);
  }

  public DocumentContent(DocumentSource source) : base(ContentType.Document)
  {
    ArgumentValidator.ThrowIfNull(source, nameof(source));

    Source = source;
  }

  public DocumentContent(DocumentSource source, CacheControl cacheControl) : base(ContentType.Document, cacheControl)
  {
    ArgumentValidator.ThrowIfNull(source, nameof(source));

    Source = source;
  }
}


public class CitationOption
{
  public bool Enabled { get; init; }
}