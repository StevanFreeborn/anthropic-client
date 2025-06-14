using System.Text.Json.Serialization;

namespace AnthropicClient.Models;

/// <summary>
/// Represents a citation
/// </summary>
public abstract class Citation
{
  /// <summary>
  /// Gets the type of the citation.
  /// </summary>
  public string Type { get; init; } = string.Empty;

  /// <summary>
  /// Gets the text that is cited.
  /// </summary>
  [JsonPropertyName("cited_text")]
  public string CitedText { get; init; } = string.Empty;

  /// <summary>
  /// Gets the document index of the citation.
  /// </summary>
  [JsonPropertyName("document_index")]
  public int DocumentIndex { get; init; }

  /// <summary>
  /// Gets the title of the document from which the citation is made.
  /// </summary>
  [JsonPropertyName("document_title")]
  public string DocumentTitle { get; init; } = string.Empty;
}
