namespace AnthropicClient.Tests.Unit.Models;

public class CitationTypeTests
{
  [Fact]
  public void ContentBlockLocation_WhenCalled_ItShouldReturnCorrectValue()
  {
    CitationType.ContentBlockLocation.Should().Be("content_block_location");
  }

  [Fact]
  public void CharacterLocation_WhenCalled_ItShouldReturnCorrectValue()
  {
    CitationType.CharacterLocation.Should().Be("char_location");
  }

  [Fact]
  public void PageLocation_WhenCalled_ItShouldReturnCorrectValue()
  {
    CitationType.PageLocation.Should().Be("page_location");
  }
}