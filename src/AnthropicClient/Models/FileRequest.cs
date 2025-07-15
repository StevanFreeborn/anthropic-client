using AnthropicClient.Utils;

namespace AnthropicClient.Models;

/// <summary>
/// Represents a request to create a file.
/// </summary>
public class FileRequest
{
  /// <summary>
  /// The file content as a byte array.
  /// </summary>
  public byte[] Content { get; }

  /// <summary>
  /// The filename of the file.
  /// </summary>
  public string Filename { get; }

  /// <summary>
  /// The MIME type of the file.
  /// </summary>
  public string ContentType { get; }

  /// <summary>
  /// The purpose of the file.
  /// </summary>
  public string Purpose { get; }

  /// <summary>
  /// Initializes a new instance of the <see cref="FileRequest"/> class.
  /// </summary>
  /// <param name="content">The file content as a byte array.</param>
  /// <param name="filename">The filename of the file.</param>
  /// <param name="contentType">The MIME type of the file.</param>
  /// <param name="purpose">The purpose of the file (default: "user_upload").</param>
  /// <exception cref="ArgumentNullException">Thrown when content, filename, or contentType is null.</exception>
  /// <exception cref="ArgumentException">Thrown when filename or contentType is empty.</exception>
  public FileRequest(byte[] content, string filename, string contentType, string purpose = "user_upload")
  {
    ArgumentValidator.ThrowIfNull(content, nameof(content));
    ArgumentValidator.ThrowIfNullOrWhitespace(filename, nameof(filename));
    ArgumentValidator.ThrowIfNullOrWhitespace(contentType, nameof(contentType));
    ArgumentValidator.ThrowIfNullOrWhitespace(purpose, nameof(purpose));

    Content = content;
    Filename = filename;
    ContentType = contentType;
    Purpose = purpose;
  }
}