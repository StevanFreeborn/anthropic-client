namespace AnthropicClient.Models;

/// <summary>
/// Represents the response from deleting a file in the Anthropic API.
/// </summary>
public class AnthropicFileDeleteResponse
{
  /// <summary>
  /// Gets or sets the ID of the file that was deleted.
  /// </summary>
  public string Id { get; init; } = string.Empty;

  /// <summary>
  /// Gets or sets the response type
  /// </summary>
  public string Type { get; init; } = string.Empty;
}