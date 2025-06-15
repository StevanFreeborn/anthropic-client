using System.Text.Json.Serialization;

using AnthropicClient.Utils;

namespace AnthropicClient.Models;

/// <summary>
/// Represents a custom source that contains a list of text content.
/// </summary>
public class CustomSource : Source
{
  /// <summary>
  ///  Gets the list of text content that makes up the custom source.
  /// </summary>
  public List<TextContent> Content { get; init; } = [];

  [JsonConstructor]
  internal CustomSource() : base(SourceType.Content)
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="CustomSource"/> class.
  /// </summary>
  /// <returns>A new instance of the <see cref="CustomSource"/> class.</returns>
  /// <exception cref="ArgumentNullException">Thrown when the content is null.</exception>
  public CustomSource(List<TextContent> content) : base(SourceType.Content)
  {
    ArgumentValidator.ThrowIfNull(content, nameof(content));

    Content = content;
  }
}