namespace AnthropicClient.Tests.Unit.Models;

public class ContentBlockLocationCitationTests : SerializationTest
{
  private readonly string _testJson = @"{
    ""type"": ""content_block_location"",
    ""cited_text"": ""cited text"",
    ""document_index"": 1,
    ""document_title"": ""document title"",
    ""start_block_index"": 2,
    ""end_block_index"": 3
  }";

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

    var result = Serialize<Citation>(citation);

    JsonAssert.Equal(_testJson, result);
  }

  [Fact]
  public void JsonDeserialization_WhenDeserialized_ItShouldHaveExpectedShape()
  {
    var citation = Deserialize<Citation>(_testJson);

    var contentBlockLocationCitation = citation.As<ContentBlockLocationCitation>();
    contentBlockLocationCitation!.Type.Should().Be("content_block_location");
    contentBlockLocationCitation.CitedText.Should().Be("cited text");
    contentBlockLocationCitation.DocumentIndex.Should().Be(1);
    contentBlockLocationCitation.DocumentTitle.Should().Be("document title");
    contentBlockLocationCitation.StartBlockIndex.Should().Be(2);
    contentBlockLocationCitation.EndBlockIndex.Should().Be(3);
  }
}