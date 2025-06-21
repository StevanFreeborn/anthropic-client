namespace AnthropicClient.Tests.Unit.Models;

public class TextSourceTests
{
  [Fact]
  public void Constructor_WhenCalled_ItShouldSetProperties()
  {
    var result = new TextSource("data");

    result.Type.Should().Be("text");
    result.MediaType.Should().Be("text/plain");
    result.Data.Should().Be("data");
  }
}