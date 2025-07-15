namespace AnthropicClient.Tests.Unit.Models;

public class FileRequestTests
{
  [Fact]
  public void Constructor_WhenCalled_ItShouldReturnAnInstanceWithPropertiesSet()
  {
    var content = "Hello World"u8.ToArray();
    var filename = "example.txt";
    var contentType = "text/plain";
    var purpose = "user_upload";

    var request = new FileRequest(content, filename, contentType, purpose);

    request.Content.Should().BeEquivalentTo(content);
    request.Filename.Should().Be(filename);
    request.ContentType.Should().Be(contentType);
    request.Purpose.Should().Be(purpose);
  }

  [Fact]
  public void Constructor_WhenCalledWithDefaultPurpose_ItShouldSetPurposeToUserUpload()
  {
    var content = "Hello World"u8.ToArray();
    var filename = "example.txt";
    var contentType = "text/plain";

    var request = new FileRequest(content, filename, contentType);

    request.Content.Should().BeEquivalentTo(content);
    request.Filename.Should().Be(filename);
    request.ContentType.Should().Be(contentType);
    request.Purpose.Should().Be("user_upload");
  }

  [Fact]
  public void Constructor_WhenContentIsNull_ItShouldThrowArgumentNullException()
  {
    var act = () => new FileRequest(null!, "example.txt", "text/plain");

    act.Should().Throw<ArgumentNullException>().WithParameterName("content");
  }

  [Fact]
  public void Constructor_WhenFilenameIsNull_ItShouldThrowArgumentException()
  {
    var content = "Hello World"u8.ToArray();

    var act = () => new FileRequest(content, null!, "text/plain");

    act.Should().Throw<ArgumentException>().WithParameterName("filename");
  }

  [Fact]
  public void Constructor_WhenFilenameIsEmpty_ItShouldThrowArgumentException()
  {
    var content = "Hello World"u8.ToArray();

    var act = () => new FileRequest(content, "", "text/plain");

    act.Should().Throw<ArgumentException>().WithParameterName("filename");
  }

  [Fact]
  public void Constructor_WhenContentTypeIsNull_ItShouldThrowArgumentException()
  {
    var content = "Hello World"u8.ToArray();

    var act = () => new FileRequest(content, "example.txt", null!);

    act.Should().Throw<ArgumentException>().WithParameterName("contentType");
  }

  [Fact]
  public void Constructor_WhenContentTypeIsEmpty_ItShouldThrowArgumentException()
  {
    var content = "Hello World"u8.ToArray();

    var act = () => new FileRequest(content, "example.txt", "");

    act.Should().Throw<ArgumentException>().WithParameterName("contentType");
  }

  [Fact]
  public void Constructor_WhenPurposeIsNull_ItShouldThrowArgumentException()
  {
    var content = "Hello World"u8.ToArray();

    var act = () => new FileRequest(content, "example.txt", "text/plain", null!);

    act.Should().Throw<ArgumentException>().WithParameterName("purpose");
  }

  [Fact]
  public void Constructor_WhenPurposeIsEmpty_ItShouldThrowArgumentException()
  {
    var content = "Hello World"u8.ToArray();

    var act = () => new FileRequest(content, "example.txt", "text/plain", "");

    act.Should().Throw<ArgumentException>().WithParameterName("purpose");
  }
}