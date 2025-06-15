namespace AnthropicClient.Tests.Unit.Models;

public class CharacterLocationCitationTests
{
  [Fact]
  public void Constructor_WhenCalled_ItShouldSetProperties()
  {
    var result = new CharacterLocationCitation();

    result.Type.Should().Be("char_location");
    result.CitedText.Should().BeEmpty();
    result.DocumentIndex.Should().Be(0);
    result.DocumentTitle.Should().BeEmpty();
    result.StartCharIndex.Should().Be(0);
    result.EndCharIndex.Should().Be(0);
  }
}