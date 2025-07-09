namespace AnthropicClient.Models;

/// <summary>
/// Represents the types of document sources that can be used in the Anthropic API.
/// </summary>
public static class SourceType
{
  /// <summary>
  /// The base64 encoded document source type.
  /// </summary>
  public const string Base64 = "base64";

  /// <summary>
  /// The custom content document source type.
  /// </summary>
  public const string Content = "content";

  /// <summary>
  /// The text document source type.
  /// </summary>
  public const string Text = "text";
}