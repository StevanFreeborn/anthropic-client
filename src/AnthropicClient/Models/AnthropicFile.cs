using System.Text.Json.Serialization;

namespace AnthropicClient.Models;

/// <summary>
/// Represents a file object from the Anthropic Files API.
/// </summary>
public class AnthropicFile
{
  /// <summary>
  /// Unique object identifier.
  /// </summary>
  [JsonPropertyName("id")]
  public string Id { get; init; } = string.Empty;

  /// <summary>
  /// Object type.
  /// </summary>
  [JsonPropertyName("type")]
  public string Type { get; init; } = string.Empty;

  /// <summary>
  /// Original filename of the uploaded file.
  /// </summary>
  [JsonPropertyName("filename")]
  public string Name { get; init; } = string.Empty;

  /// <summary>
  /// Date file was created.
  /// </summary>
  [JsonPropertyName("created_at")]
  public DateTimeOffset CreatedAt { get; init; }

  /// <summary>
  /// Size of the file in bytes.
  /// </summary>
  [JsonPropertyName("size_bytes")]
  public long Size { get; init; }

  /// <summary>
  /// MIME type of the file.
  /// </summary>
  [JsonPropertyName("mime_type")]
  public string MimeType { get; init; } = string.Empty;

  /// <summary>
  /// Whether the file can be downloaded.
  /// </summary>
  [JsonPropertyName("downloadable")]
  public bool Downloadable { get; init; }
}