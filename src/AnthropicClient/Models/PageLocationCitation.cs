using System.Text.Json.Serialization;

namespace AnthropicClient.Models;

/// <summary>
/// Represents a citation for text within a page of a document.
/// </summary>
public class PageLocationCitation : Citation
{
  /// <summary>
  /// Gets the start page number of the citation.
  /// </summary>
  [JsonPropertyName("start_page_number")]
  public int StartPageNumber { get; init; }

  /// <summary>
  /// Gets the end page number of the citation.
  /// </summary>
  [JsonPropertyName("end_page_number")]
  public int EndPageNumber { get; init; }

  /// <summary>
  /// Initializes a new instance of the <see cref="PageLocationCitation"/> class.
  /// </summary>
  /// <returns>A new instance of <see cref="PageLocationCitation"/>.</returns>
  public PageLocationCitation() : base(CitationType.PageLocation)
  {
  }
}