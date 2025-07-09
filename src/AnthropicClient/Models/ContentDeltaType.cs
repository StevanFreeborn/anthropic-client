namespace AnthropicClient.Models;

/// <summary>
/// Provides constants for content delta types.
/// </summary>
public static class ContentDeltaType
{
  /// <summary>
  /// The text_delta.
  /// </summary>
  public const string TextDelta = "text_delta";

  /// <summary>
  /// The input_json_delta.
  /// </summary>
  public const string JsonDelta = "input_json_delta";

  /// <summary>
  /// The citation_delta.
  /// </summary>
  public const string CitationDelta = "citations_delta";
}