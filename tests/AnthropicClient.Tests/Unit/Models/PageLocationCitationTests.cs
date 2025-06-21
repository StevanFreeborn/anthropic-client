namespace AnthropicClient.Tests.Unit.Models;

public class PageLocationCitationTests
{
  [Fact]
  public void Constructor_WhenCalled_ItShouldSetProperties()
  {
    var result = new PageLocationCitation();

    result.Type.Should().Be("page_location");
    result.CitedText.Should().BeEmpty();
    result.DocumentIndex.Should().Be(0);
    result.DocumentTitle.Should().BeEmpty();
    result.StartPageNumber.Should().Be(0);
    result.EndPageNumber.Should().Be(0);
  }
}