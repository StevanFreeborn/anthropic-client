namespace AnthropicClient.Tests.Unit.Models;

public class CharacterLocationCitationTests : SerializationTest
{
  private readonly string _testJson = @"{
    ""type"": ""char_location"",
    ""cited_text"": ""cited text"",
    ""document_index"": 1,
    ""document_title"": ""document title"",
    ""start_char_index"": 2,
    ""end_char_index"": 3
  }";

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

  [Fact]
  public void JsonSerialization_WhenSerialized_ItShouldHaveExpectedShape()
  {
    var citation = new CharacterLocationCitation
    {
      Type = "char_location",
      CitedText = "cited text",
      DocumentIndex = 1,
      DocumentTitle = "document title",
      StartCharIndex = 2,
      EndCharIndex = 3
    };

    var result = Serialize<Citation>(citation);

    JsonAssert.Equal(_testJson, result);
  }

  [Fact]
  public void JsonDeserialization_WhenDeserialized_ItShouldHaveExpectedShape()
  {
    var citation = Deserialize<Citation>(_testJson);

    var characterLocationCitation = citation.As<CharacterLocationCitation>();
    characterLocationCitation!.Type.Should().Be("char_location");
    characterLocationCitation.CitedText.Should().Be("cited text");
    characterLocationCitation.DocumentIndex.Should().Be(1);
    characterLocationCitation.DocumentTitle.Should().Be("document title");
    characterLocationCitation.StartCharIndex.Should().Be(2);
    characterLocationCitation.EndCharIndex.Should().Be(3);
  }
}