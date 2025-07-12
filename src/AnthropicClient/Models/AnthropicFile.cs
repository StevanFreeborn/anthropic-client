using System.Text.Json.Serialization;

namespace AnthropicClient.Models;

/// <summary>
/// Represents a file object from the Anthropic Files API.
/// </summary>
public class AnthropicFile
{
  /// <summary>
  /// Unique object identifier.
  /// The format and length of IDs may change over time.
  /// </summary>
  [JsonPropertyName("id")]
  public string Id { get; init; } = string.Empty;

  /// <summary>
  /// Object type.
  /// For files, this is always "file".
  /// </summary>
  [JsonPropertyName("type")]
  public string Type { get; init; } = "file";

  /// <summary>
  /// Original filename of the uploaded file.
  /// </summary>
  [JsonPropertyName("file_name")]
  public string FileName { get; init; } = string.Empty;

  /// <summary>
  /// RFC 3339 datetime string representing when the file was created.
  /// </summary>
  [JsonPropertyName("created_at")]
  public string CreatedAt { get; init; } = string.Empty;

  /// <summary>
  /// Size of the file in bytes.
  /// </summary>
  [JsonPropertyName("size_bytes")]
  public long SizeBytes { get; init; }

  /// <summary>
  /// MIME type of the file.
  /// </summary>
  [JsonPropertyName("file_type")]
  public string FileType { get; init; } = string.Empty;

  /// <summary>
  /// Whether the file can be downloaded.
  /// </summary>
  [JsonPropertyName("downloadable")]
  public bool Downloadable { get; init; }
}
