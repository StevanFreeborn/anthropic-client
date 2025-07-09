namespace AnthropicClient.Tests.Unit.Models;

public class PageLocationCitationTests : SerializationTest
{
  private readonly string _testJson = @"{
    ""type"": ""page_location"",
    ""cited_text"": ""cited text"",
    ""document_index"": 1,
    ""document_title"": ""document title"",
    ""start_page_number"": 2,
    ""end_page_number"": 3
  }";

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

  [Fact]
  public void JsonSerialization_WhenSerialized_ItShouldHaveExpectedShape()
  {
    var citation = new PageLocationCitation
    {
      Type = "page_location",
      CitedText = "cited text",
      DocumentIndex = 1,
      DocumentTitle = "document title",
      StartPageNumber = 2,
      EndPageNumber = 3
    };

    var result = Serialize<Citation>(citation);

    JsonAssert.Equal(_testJson, result);
  }

  [Fact]
  public void JsonDeserialization_WhenDeserialized_ItShouldHaveExpectedShape()
  {
    var citation = Deserialize<Citation>(_testJson);

    var pageLocationCitation = citation.As<PageLocationCitation>();
    pageLocationCitation!.Type.Should().Be("page_location");
    pageLocationCitation.CitedText.Should().Be("cited text");
    pageLocationCitation.DocumentIndex.Should().Be(1);
    pageLocationCitation.DocumentTitle.Should().Be("document title");
    pageLocationCitation.StartPageNumber.Should().Be(2);
    pageLocationCitation.EndPageNumber.Should().Be(3);
  }
}