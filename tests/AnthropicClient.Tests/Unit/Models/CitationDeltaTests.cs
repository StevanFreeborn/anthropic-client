namespace AnthropicClient.Tests.Unit.Models;

public class CitationDeltaTests
{
  [Fact]
  public void Constructor_WhenCalled_ItShouldSetProperties()
  {
    var result = new CitationDelta();

    result.Type.Should().Be("citations_delta");
    result.Citation.Should().BeEquivalentTo(new CharacterLocationCitation());
  }

  [Fact]
  public void Constructor_WhenCalledWithCitation_ItShouldSetProperties()
  {
    var citation = new ContentBlockLocationCitation();
    var result = new CitationDelta(citation);

    result.Type.Should().Be("citations_delta");
    result.Citation.Should().BeSameAs(citation);
  }

  [Fact]
  public void Constructor_WhenCalledWithNullCitation_ItShouldThrowException()
  {
    var act = () => new CitationDelta(null!);

    act.Should().Throw<ArgumentNullException>();
  }
}