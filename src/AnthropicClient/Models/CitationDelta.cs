using System.Text.Json.Serialization;

using AnthropicClient.Utils;

namespace AnthropicClient.Models;

/// <summary>
/// Represents a citation delta.
/// </summary>
public class CitationDelta : ContentDelta
{
  /// <summary>
  /// Gets the citation associated with this delta.
  /// </summary>
  public Citation Citation { get; init; } = new CharacterLocationCitation();

  [JsonConstructor]
  internal CitationDelta() : base(ContentDeltaType.CitationDelta)
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="CitationDelta"/> class.
  /// </summary>
  /// <param name="citation">The citation to associate with this delta.</param>
  /// <exception cref="ArgumentNullException">Thrown when <paramref name="citation"/> is null.</exception>
  /// <returns>A new instance of <see cref="CitationDelta"/>.</returns>
  public CitationDelta(Citation citation) : base(ContentDeltaType.CitationDelta)
  {
    ArgumentValidator.ThrowIfNull(citation, nameof(citation));
    Citation = citation;
  }
}