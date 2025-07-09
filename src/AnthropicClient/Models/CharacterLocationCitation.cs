using System.Text.Json.Serialization;

namespace AnthropicClient.Models;

/// <summary>
/// Represents a citation for specific locations within text content.
/// </summary>
public class CharacterLocationCitation : Citation
{
  /// <summary>
  /// Gets the start character index of the citation.
  /// </summary>
  [JsonPropertyName("start_char_index")]
  public int StartCharIndex { get; init; }

  /// <summary>
  /// Gets the end character index of the citation.
  /// </summary>
  [JsonPropertyName("end_char_index")]
  public int EndCharIndex { get; init; }

  /// <summary>
  /// Initializes a new instance of the <see cref="CharacterLocationCitation"/> class.
  /// </summary>
  /// <returns>A new instance of <see cref="CharacterLocationCitation"/>.</returns>
  public CharacterLocationCitation() : base(CitationType.CharacterLocation)
  {
  }
}