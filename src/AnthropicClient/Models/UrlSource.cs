namespace AnthropicClient.Models;

/// <summary>
/// Represents a URL source in the Anthropic API.
/// </summary>
public class UrlSource : Source
{
  /// <summary>
  /// Gets or sets the URL of the source document.
  /// </summary>
  public string Url { get; init; } = string.Empty;

  /// <summary>
  /// Initializes a new instance of the <see cref="UrlSource"/> class.
  /// </summary>
  /// <returns>A new instance of <see cref="UrlSource"/> with the type set to "url".</returns>
  public UrlSource() : base(SourceType.Url)
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="UrlSource"/> class with a specified URL.
  /// </summary>
  /// <param name="url">The URL of the source document.</param>
  /// <returns>A new instance of <see cref="UrlSource"/>.</returns>
  public UrlSource(string url) : base(SourceType.Url)
  {
    Url = url;
  }
}