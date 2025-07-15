namespace AnthropicClient.Models;

/// <summary>
/// Represents the response from downloading a file.
/// </summary>
public class FileDownloadResponse
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
  /// The size of the file in bytes.
  /// </summary>
  public int SizeBytes { get; }

  /// <summary>
  /// Initializes a new instance of the <see cref="FileDownloadResponse"/> class.
  /// </summary>
  /// <param name="content">The file content as a byte array.</param>
  /// <param name="filename">The filename of the file.</param>
  /// <param name="contentType">The MIME type of the file.</param>
  /// <param name="sizeBytes">The size of the file in bytes.</param>
  public FileDownloadResponse(byte[] content, string filename, string contentType, int sizeBytes)
  {
    Content = content;
    Filename = filename;
    ContentType = contentType;
    SizeBytes = sizeBytes;
  }
}