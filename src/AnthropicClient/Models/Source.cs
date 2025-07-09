namespace AnthropicClient.Models;

/// <summary>
/// Represents a base class for sources.
/// </summary>
public abstract class Source
{
  /// <summary>
  /// Gets the type of the source.
  /// </summary>
  public string Type { get; init; }

  /// <summary>
  /// Initializes a new instance of the <see cref="Source"/> class.
  /// </summary>
  /// <param name="type">The type of the source.</param>
  /// <returns>A new instance of the <see cref="Source"/> class.</returns>
  protected Source(string type)
  {
    Type = type;
  }
}