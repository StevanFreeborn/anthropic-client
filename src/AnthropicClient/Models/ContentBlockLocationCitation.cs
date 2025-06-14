using System.Text.Json.Serialization;

namespace AnthropicClient.Models;

/// <summary>
/// Represents a citation for content blocks within custom content.
/// </summary>
public class ContentBlockLocationCitation : Citation
{
  /// <summary>
  /// Gets the start block index of the citation.
  /// /// </summary>
  [JsonPropertyName("start_block_index")]
  public int StartBlockIndex { get; init; }

  /// <summary>
  /// Gets the end block index of the citation.
  /// </summary>
  [JsonPropertyName("end_block_index")]
  public int EndBlockIndex { get; init; }
}