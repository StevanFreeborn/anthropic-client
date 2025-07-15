using System.Text.Json.Serialization;

namespace AnthropicClient.Models;

/// <summary>
/// Represents the response from a file deletion operation.
/// </summary>
public class FileDeleteResponse
{
  /// <summary>
  /// The type of the object.
  /// </summary>
  public string Type { get; init; } = "file_deleted";

  /// <summary>
  /// The unique identifier for the deleted file.
  /// </summary>
  public string Id { get; init; } = string.Empty;

  /// <summary>
  /// Indicates whether the file was successfully deleted.
  /// </summary>
  public bool Deleted { get; init; }
}