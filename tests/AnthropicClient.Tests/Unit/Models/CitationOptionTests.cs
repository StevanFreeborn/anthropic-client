namespace AnthropicClient.Tests.Unit.Models;

public class CitationOptionTests
{
  [Fact]
  public void Constructor_WhenCalled_ItShouldSetProperties()
  {
    var result = new CitationOption();

    result.Enabled.Should().BeFalse();
  }
}