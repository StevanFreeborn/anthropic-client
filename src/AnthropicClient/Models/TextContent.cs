using System.Text.Json.Serialization;

using AnthropicClient.Utils;

namespace AnthropicClient.Models;

/// <summary>
/// Represents text content that is part of a message.
/// </summary>
public class TextContent : Content
{
  /// <summary>
  /// Gets the text of the content.
  /// </summary>
  public string Text { get; init; } = string.Empty;

  /// <summary>
  /// Gets the citations associated with the text content.
  /// </summary>
  public Citation[]? Citations { get; init; }

  [JsonConstructor]
  internal TextContent() : base(ContentType.Text)
  {
  }

  private void Validate(string text)
  {
    ArgumentValidator.ThrowIfNull(text, nameof(text));
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="TextContent"/> class.
  /// </summary>
  /// <param name="text">The text of the content.</param>
  /// <exception cref="ArgumentNullException">Thrown when the text is null.</exception>
  /// <returns>A new instance of the <see cref="TextContent"/> class.</returns>
  public TextContent(string text) : base(ContentType.Text)
  {
    Validate(text);

    Text = text;
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="TextContent"/> class.
  /// </summary>
  /// <param name="text">The text of the content.</param>
  /// <param name="cacheControl">The cache control to be used for the content.</param>
  /// <returns>A new instance of the <see cref="TextContent"/> class.</returns>
  /// <exception cref="ArgumentNullException">Thrown when the text or cache control is null.</exception>
  public TextContent(string text, CacheControl cacheControl) : base(ContentType.Text, cacheControl)
  {
    Validate(text);

    Text = text;
  }
}