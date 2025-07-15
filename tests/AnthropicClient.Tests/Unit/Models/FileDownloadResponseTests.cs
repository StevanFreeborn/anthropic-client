namespace AnthropicClient.Tests.Unit.Models;

public class FileDownloadResponseTests
{
  [Fact]
  public void Constructor_WhenCalled_ItShouldReturnAnInstanceWithPropertiesSet()
  {
    var content = "Hello World"u8.ToArray();
    var filename = "example.txt";
    var contentType = "text/plain";
    var sizeBytes = 1024;

    var response = new FileDownloadResponse(content, filename, contentType, sizeBytes);

    response.Content.Should().BeEquivalentTo(content);
    response.Filename.Should().Be(filename);
    response.ContentType.Should().Be(contentType);
    response.SizeBytes.Should().Be(sizeBytes);
  }
}