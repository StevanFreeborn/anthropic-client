using System.Text.Json.Serialization;

namespace AnthropicClient.Models;

/// <summary>
/// Represents a file source in the Anthropic API.
/// </summary>
public class FileSource : Source
{
  /// <summary>
  /// Gets or sets the unique identifier for the file source.
  /// </summary>
  [JsonPropertyName("file_id")]
  public string Id { get; init; } = string.Empty;

  /// <summary>
  /// Initializes a new instance of the <see cref="FileSource"/> class.
  /// </summary>
  /// <returns>A new instance of <see cref="FileSource"/> with the type set to "file".</returns>
  public FileSource() : base(SourceType.File)
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="FileSource"/> class with a specified file ID.
  /// </summary>
  /// <param name="id">The unique identifier for the file source.</param>
  /// <returns>A new instance of <see cref="FileSource"/>.</returns>
  public FileSource(string id) : base(SourceType.File)
  {
    Id = id;
  }
}