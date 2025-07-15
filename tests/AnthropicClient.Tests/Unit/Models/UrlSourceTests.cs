namespace AnthropicClient.Tests.Unit.Models;

public class UrlSourceTests : SerializationTest
{
  private readonly string _testJson = @"{
    ""type"": ""url"",
    ""url"": ""https://example.com/document.pdf""
  }";

  [Fact]
  public void Constructor_WhenCalled_ItShouldInitializeProperties()
  {
    var result = new UrlSource();

    result.Url.Should().BeEmpty();
    result.Type.Should().Be("url");
  }

  [Fact]
  public void Constructor_WhenCalledWithValues_ItShouldInitializeProperties()
  {
    var url = "https://example.com/document.pdf";
    var type = "type";

    var result = new UrlSource()
    {
      Url = url,
      Type = type,
    };

    result.Url.Should().Be(url);
    result.Type.Should().Be(type);
  }

  [Fact]
  public void Constructor_WhenCalledWithUrl_ItShouldInitializeProperties()
  {
    var url = "https://example.com/document.pdf";

    var result = new UrlSource(url);

    result.Url.Should().Be(url);
    result.Type.Should().Be("url");
  }

  [Fact]
  public void JsonSerialization_WhenSerialized_ItShouldHaveExpectedShape()
  {
    var url = "https://example.com/document.pdf";
    var source = new UrlSource(url);

    var result = Serialize<Source>(source);

    JsonAssert.Equal(_testJson, result);
  }

  [Fact]
  public void JsonDeserialization_WhenDeserialized_ItShouldMatchExpectedObject()
  {
    var result = Deserialize<Source>(_testJson);

    result.Should().BeEquivalentTo(new UrlSource("https://example.com/document.pdf"));
  }
}