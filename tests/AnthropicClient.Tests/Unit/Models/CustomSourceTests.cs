namespace AnthropicClient.Tests.Unit.Models;

public class CustomSourceTests : SerializationTest
{
  private readonly string _testJson = @"{
    ""type"": ""content"",
    ""content"": [
      {
        ""type"": ""text"",
        ""text"": ""Sample text""
      }
    ]
  }";

  [Fact]
  public void Constructor_WhenCalled_ItShouldSetProperties()
  {
    var result = new CustomSource();

    result.Content.Should().BeEmpty();
    result.Type.Should().Be("content");
  }

  [Fact]
  public void Constructor_WhenCalledWithContent_ItShouldSetProperties()
  {
    var content = new List<TextContent>
    {
      new("Sample text")
    };

    var result = new CustomSource(content);

    result.Content.Should().BeSameAs(content);
    result.Type.Should().Be("content");
  }

  [Fact]
  public void Constructor_WhenCalledWithNullContent_ItShouldThrowArgumentNullException()
  {
    var act = () => new CustomSource(null!);

    act.Should().Throw<ArgumentNullException>();
  }

  [Fact]
  public void JsonSerialization_WhenSerialized_ItShouldHaveExpectedShape()
  {
    var content = new List<TextContent>
    {
      new("Sample text")
    };
    var source = new CustomSource(content);

    var result = Serialize<Source>(source);

    JsonAssert.Equal(_testJson, result);
  }

  [Fact]
  public void JsonDeserialization_WhenDeserialized_ItShouldHaveExpectedShape()
  {
    var source = Deserialize<Source>(_testJson);

    var customSource = source.As<CustomSource>();
    customSource!.Type.Should().Be("content");
    customSource.Content.Should().HaveCount(1);
    customSource.Content[0].Type.Should().Be("text");
    customSource.Content[0].Text.Should().Be("Sample text");
  }
}