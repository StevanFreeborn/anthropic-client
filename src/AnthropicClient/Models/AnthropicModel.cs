using System.Text.Json.Serialization;

namespace AnthropicClient.Models;

/// <summary>
/// Represents an Anthropic model.
/// </summary>
public class AnthropicModel
{
  /// <summary>
  /// The type of the model.
  /// </summary>
  public string Type { get; init; } = string.Empty;

  /// <summary>
  /// The id of the model.
  /// </summary>
  public string Id { get; init; } = string.Empty;

  /// <summary>
  /// The display name of the model.
  /// </summary>
  [JsonPropertyName("display_name")]
  public string DisplayName { get; init; } = string.Empty;

  /// <summary>
  /// The created date of the model.
  /// </summary>
  [JsonPropertyName("created_at")]
  public DateTimeOffset CreatedAt { get; init; }
}