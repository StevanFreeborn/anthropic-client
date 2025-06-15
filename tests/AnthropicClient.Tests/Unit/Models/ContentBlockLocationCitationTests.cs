namespace AnthropicClient.Tests.Unit.Models;

public class ContentBlockLocationCitationTests
{
  [Fact]
  public void Constructor_WhenCalled_ItShouldSetProperties()
  {
    var result = new ContentBlockLocationCitation();

    result.Type.Should().Be("content_block_location");
    result.CitedText.Should().BeEmpty();
    result.DocumentIndex.Should().Be(0);
    result.DocumentTitle.Should().BeEmpty();
    result.StartBlockIndex.Should().Be(0);
    result.EndBlockIndex.Should().Be(0);
  }
}