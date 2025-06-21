namespace AnthropicClient.Tests.Unit.Models;

public class CustomSourceTests
{
  [Fact]
  public void Constructor_WhenCalled_ItShouldSetProperties()
  {
    var result = new CustomSource();

    result.Content.Should().BeEmpty();
    result.Type.Should().Be("content");
  }

  [Fact]
  public void Constructor_WhenCalledWithContent_ItShouldSetProperties()
  {
    var content = new List<TextContent>
    {
      new("Sample text")
    };

    var result = new CustomSource(content);

    result.Content.Should().BeSameAs(content);
    result.Type.Should().Be("content");
  }

  [Fact]
  public void Constructor_WhenCalledWithNullContent_ItShouldThrowArgumentNullException()
  {
    var act = () => new CustomSource(null!);

    act.Should().Throw<ArgumentNullException>();
  }
}