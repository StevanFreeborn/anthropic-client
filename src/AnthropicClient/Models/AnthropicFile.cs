using System.Text.Json.Serialization;

namespace AnthropicClient.Models;

/// <summary>
/// Represents a file in the Anthropic API.
/// </summary>
public class AnthropicFile
{
  /// <summary>
  /// The type of the object.
  /// </summary>
  public string Type { get; init; } = "file";

  /// <summary>
  /// The unique identifier for the file.
  /// </summary>
  public string Id { get; init; } = string.Empty;

  /// <summary>
  /// The filename of the file.
  /// </summary>
  public string Filename { get; init; } = string.Empty;

  /// <summary>
  /// The MIME type of the file.
  /// </summary>
  [JsonPropertyName("content_type")]
  public string ContentType { get; init; } = string.Empty;

  /// <summary>
  /// The size of the file in bytes.
  /// </summary>
  [JsonPropertyName("size_bytes")]
  public int SizeBytes { get; init; }

  /// <summary>
  /// The date and time when the file was created.
  /// </summary>
  [JsonPropertyName("created_at")]
  public DateTimeOffset CreatedAt { get; init; }
}