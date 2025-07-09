namespace AnthropicClient.Models;

/// <summary>
/// Represents whether citations are enabled for a document.
/// </summary>
public class CitationOption
{
  /// <summary>
  /// Gets a value indicating whether citations are enabled for the document.
  /// </summary>
  public bool Enabled { get; init; }
}