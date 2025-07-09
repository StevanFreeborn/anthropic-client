namespace AnthropicClient.Models;

/// <summary>
/// The types of citations that can be returned by the Anthropic API.
/// </summary>
public static class CitationType
{
  /// <summary>
  /// A citation that refers to a specific character in the text.
  /// </summary>
  public const string CharacterLocation = "char_location";

  /// <summary>
  /// A citation that refers to a specific page in the text.
  /// </summary>
  public const string PageLocation = "page_location";

  /// <summary>
  /// A citation that refers to a specific section in the text.
  /// </summary>
  public const string ContentBlockLocation = "content_block_location";
}