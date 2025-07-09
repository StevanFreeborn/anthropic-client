namespace AnthropicClient.Tests.Unit.Models;

public class TextSourceTests : SerializationTest
{
  private readonly string _testJson = @"{
    ""type"": ""text"",
    ""media_type"": ""text/plain"",
    ""data"": ""data""
  }";

  [Fact]
  public void Constructor_WhenCalled_ItShouldSetProperties()
  {
    var result = new TextSource("data");

    result.Type.Should().Be("text");
    result.MediaType.Should().Be("text/plain");
    result.Data.Should().Be("data");
  }

  [Fact]
  public void JsonSerialization_WhenSerialized_ItShouldHaveExpectedShape()
  {
    var source = new TextSource("data");

    var result = Serialize<Source>(source);

    JsonAssert.Equal(_testJson, result);
  }

  [Fact]
  public void JsonDeserialization_WhenDeserialized_ItShouldHaveExpectedShape()
  {
    var source = Deserialize<Source>(_testJson);

    var textSource = source.As<TextSource>();
    textSource!.Type.Should().Be("text");
    textSource.MediaType.Should().Be("text/plain");
    textSource.Data.Should().Be("data");
  }
}