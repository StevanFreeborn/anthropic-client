namespace AnthropicClient.Tests.Unit.Models;

public class FileSourceTests : SerializationTest
{
  private readonly string _testJson = @"{
    ""file_id"": ""id"",
    ""type"": ""file""
  }";

  [Fact]
  public void Constructor_WhenCalled_ItShouldInitializeProperties()
  {
    var result = new FileSource();

    result.Type.Should().Be("file");
    result.Id.Should().BeEmpty();
  }

  [Fact]
  public void Constructor_WhenCalledWithValues_ItShouldInitializeProperties()
  {
    var id = "id";
    var type = "type";

    var result = new FileSource()
    {
      Id = id,
      Type = type,
    };

    result.Id.Should().Be(id);
    result.Type.Should().Be(type);
  }

  [Fact]
  public void JsonSerialization_WhenSerialized_ItShouldHaveExpectedShape()
  {
    var source = new FileSource() { Id = "id" };

    var result = Serialize<Source>(source);

    JsonAssert.Equal(_testJson, result);
  }

  [Fact]
  public void JsonDeserialization_WhenDeserialized_ItShouldHaveExpectedValues()
  {
    var result = Deserialize<Source>(_testJson);

    result.Should().BeEquivalentTo(new FileSource() { Id = "id" });
  }
}