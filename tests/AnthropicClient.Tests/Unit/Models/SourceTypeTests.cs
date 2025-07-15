namespace AnthropicClient.Tests.Unit.Models;

public class SourceTypeTests
{
  [Fact]
  public void Base64_WhenCalled_ItShouldReturnCorrectValue()
  {
    SourceType.Base64.Should().Be("base64");
  }

  [Fact]
  public void Content_WhenCalled_ItShouldReturnCorrectValue()
  {
    SourceType.Content.Should().Be("content");
  }

  [Fact]
  public void Text_WhenCalled_ItShouldReturnCorrectValue()
  {
    SourceType.Text.Should().Be("text");
  }

  [Fact]
  public void File_WhenCalled_ItShouldReturnCorrectValue()
  {
    SourceType.File.Should().Be("file");
  }
}