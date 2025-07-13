namespace AnthropicClient.Tests.Unit.Models;

public class CreateFileRequestTests
{
  [Fact]
  public void Constructor_WhenCalledWithBytes_ItShouldInitializeProperties()
  {
    var fileContent = new byte[] { 1, 2, 3 };
    var fileName = "test.txt";
    var fileType = "text/plain";

    var request = new CreateFileRequest(fileContent, fileName, fileType);

    request.File.Should().BeSameAs(fileContent);
    request.FileName.Should().Be(fileName);
    request.FileType.Should().Be(fileType);
  }

  [Fact]
  public void Constructor_WhenCalledWithStream_ItShouldInitializeProperties()
  {
    using var stream = new MemoryStream([1, 2, 3]);
    var fileName = "test.txt";
    var fileType = "text/plain";

    var request = new CreateFileRequest(stream, fileName, fileType);

    request.File.Should().BeEquivalentTo(new byte[] { 1, 2, 3 });
    request.FileName.Should().Be(fileName);
    request.FileType.Should().Be(fileType);
  }

  [Fact]
  public void Constructor_WhenCalledWithNullBytes_ItShouldThrowArgumentNullException()
  {
    var act = () => new CreateFileRequest((byte[])null!, "test.txt", "text/plain");
    act.Should().Throw<ArgumentNullException>();
  }

  [Fact]
  public void Constructor_WhenCalledWithBytesAndNullFileName_ItShouldThrowArgumentException()
  {
    var act = () => new CreateFileRequest([1, 2, 3], null!, "text/plain");
    act.Should().Throw<ArgumentException>();
  }

  [Fact]
  public void Constructor_WhenCalledWithBytesAndFileNameIsEmpty_ItShouldThrowArgumentException()
  {
    var act = () => new CreateFileRequest([1, 2, 3], string.Empty, "text/plain");
    act.Should().Throw<ArgumentException>();
  }

  [Fact]
  public void Constructor_WhenCalledWithBytesAndNullFileType_ItShouldThrowArgumentException()
  {
    var act = () => new CreateFileRequest([1, 2, 3], "test.txt", null!);
    act.Should().Throw<ArgumentException>();
  }

  [Fact]
  public void Constructor_WhenCalledWithBytesAndFileTypeIsEmpty_ItShouldThrowArgumentException()
  {
    var act = () => new CreateFileRequest([1, 2, 3], "test.txt", string.Empty);
    act.Should().Throw<ArgumentException>();
  }

  [Fact]
  public void Constructor_WhenCalledWithNullStream_ItShouldThrowArgumentNullException()
  {
    var act = () => new CreateFileRequest((Stream)null!, "test.txt", "text/plain");
    act.Should().Throw<ArgumentNullException>();
  }

  [Fact]
  public void Constructor_WhenCalledWithStreamAndNullFileName_ItShouldThrowArgumentException()
  {
    using var stream = new MemoryStream([1, 2, 3]);
    var act = () => new CreateFileRequest(stream, null!, "text/plain");
    act.Should().Throw<ArgumentException>();
  }

  [Fact]
  public void Constructor_WhenCalledWithStreamAndFileNameIsEmpty_ItShouldThrowArgumentException()
  {
    using var stream = new MemoryStream([1, 2, 3]);
    var act = () => new CreateFileRequest(stream, string.Empty, "text/plain");
    act.Should().Throw<ArgumentException>();
  }

  [Fact]
  public void Constructor_WhenCalledWithStreamAndNullFileType_ItShouldThrowArgumentException()
  {
    using var stream = new MemoryStream([1, 2, 3]);
    var act = () => new CreateFileRequest(stream, "test.txt", null!);
    act.Should().Throw<ArgumentException>();
  }

  [Fact]
  public void Constructor_WhenCalledWithStreamAndFileTypeIsEmpty_ItShouldThrowArgumentException()
  {
    using var stream = new MemoryStream([1, 2, 3]);
    var act = () => new CreateFileRequest(stream, "test.txt", string.Empty);
    act.Should().Throw<ArgumentException>();
  }
}