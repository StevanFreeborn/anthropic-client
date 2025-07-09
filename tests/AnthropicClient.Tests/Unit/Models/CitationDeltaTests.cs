namespace AnthropicClient.Tests.Unit.Models;

public class CitationDeltaTests : SerializationTest
{
  private readonly string _testJson = @"{
    ""type"": ""citations_delta"",
    ""citation"": {
      ""type"": ""content_block_location"",
      ""cited_text"": ""cited text"",
      ""document_index"": 1,
      ""document_title"": ""document title"",
      ""start_block_index"": 2,
      ""end_block_index"": 3
    }
  }";

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

  [Fact]
  public void JsonSerialization_WhenSerialized_ItShouldHaveExpectedShape()
  {
    var citation = new ContentBlockLocationCitation
    {
      Type = "content_block_location",
      CitedText = "cited text",
      DocumentIndex = 1,
      DocumentTitle = "document title",
      StartBlockIndex = 2,
      EndBlockIndex = 3
    };

    var citationDelta = new CitationDelta(citation);
    var result = Serialize<ContentDelta>(citationDelta);

    JsonAssert.Equal(_testJson, result);
  }

  [Fact]
  public void JsonDeserialization_WhenDeserialized_ItShouldHaveExpectedShape()
  {
    var citation = Deserialize<ContentDelta>(_testJson);

    var citationDelta = citation.As<CitationDelta>();
    citationDelta!.Type.Should().Be("citations_delta");

    var contentBlockLocationCitation = citationDelta.Citation.As<ContentBlockLocationCitation>();
    contentBlockLocationCitation!.CitedText.Should().Be("cited text");
    contentBlockLocationCitation.DocumentIndex.Should().Be(1);
    contentBlockLocationCitation.DocumentTitle.Should().Be("document title");
    contentBlockLocationCitation.StartBlockIndex.Should().Be(2);
    contentBlockLocationCitation.EndBlockIndex.Should().Be(3);
  }
}