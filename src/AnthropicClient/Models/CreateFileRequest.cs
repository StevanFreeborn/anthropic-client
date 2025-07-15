using AnthropicClient.Utils;

namespace AnthropicClient.Models;

/// <summary>
/// Represents a request to create a file via the Anthropic Files API.
/// </summary>
public class CreateFileRequest
{
  /// <summary>
  /// The file content as a byte array.
  /// </summary>
  public byte[] File { get; init; }

  /// <summary>
  /// The original filename of the file being uploaded.
  /// </summary>
  public string FileName { get; init; }

  /// <summary>
  /// The MIME type of the file.
  /// </summary>
  public string FileType { get; init; }

  /// <summary>
  /// Initializes a new instance of the <see cref="CreateFileRequest"/> class.
  /// </summary>
  /// <param name="file">The file content as a byte array.</param>
  /// <param name="fileName">The original filename of the file being uploaded.</param>
  /// <param name="fileType">The MIME type of the file.</param>
  /// <exception cref="ArgumentNullException">Thrown when <paramref name="file"/> is null.</exception>
  /// <exception cref="ArgumentException">Thrown when <paramref name="fileName"/> or <paramref name="fileType"/> is null or whitespace.</exception>
  public CreateFileRequest(byte[] file, string fileName, string fileType)
  {
    ArgumentValidator.ThrowIfNull(file, nameof(file));
    ArgumentValidator.ThrowIfNullOrWhitespace(fileName, nameof(fileName));
    ArgumentValidator.ThrowIfNullOrWhitespace(fileType, nameof(fileType));

    File = file;
    FileName = fileName;
    FileType = fileType;
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="CreateFileRequest"/> class from a stream.
  /// </summary>
  /// <param name="stream">The stream containing the file content.</param>
  /// <param name="fileName">The original filename of the file being uploaded.</param>
  /// <param name="fileType">The MIME type of the file.</param>
  /// <exception cref="ArgumentNullException">Thrown when <paramref name="stream"/> is null.</exception>
  /// <exception cref="ArgumentException">Thrown when <paramref name="fileName"/> or <paramref name="fileType"/> is null or whitespace.</exception>
  public CreateFileRequest(Stream stream, string fileName, string fileType)
  {
    ArgumentValidator.ThrowIfNull(stream, nameof(stream));
    ArgumentValidator.ThrowIfNullOrWhitespace(fileName, nameof(fileName));
    ArgumentValidator.ThrowIfNullOrWhitespace(fileType, nameof(fileType));

    using var memoryStream = new MemoryStream();
    stream.CopyTo(memoryStream);
    var fileContent = memoryStream.ToArray();

    File = fileContent;
    FileName = fileName;
    FileType = fileType;
  }
}