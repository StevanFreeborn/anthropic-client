namespace AnthropicClient.Tests.Unit.Models;

public class AnthropicFileDeleteResponseTests : SerializationTest
{
  private readonly string _testJson = @"{
    ""id"": ""file-12345"",
    ""type"": ""file_deleted""
  }";

  [Fact]
  public void Constructor_WhenCalled_ItShouldInitializeProperties()
  {
    var response = new AnthropicFileDeleteResponse();

    response.Id.Should().BeEmpty();
    response.Type.Should().BeEmpty();
  }

  [Fact]
  public void Constructor_WhenCalledWithValues_ItShouldInitializePropertiesWithValues()
  {
    var id = "file-12345";
    var type = "file_deleted";

    var response = new AnthropicFileDeleteResponse
    {
      Id = id,
      Type = type
    };

    response.Id.Should().Be(id);
    response.Type.Should().Be(type);
  }

  [Fact]
  public void JsonSerialization_WhenSerialized_ItShouldMatchExpectedJson()
  {
    var response = new AnthropicFileDeleteResponse
    {
      Id = "file-12345",
      Type = "file_deleted"
    };

    var json = JsonSerializer.Serialize(response, JsonSerializationOptions.DefaultOptions);

    JsonAssert.Equal(_testJson, json);
  }

  [Fact]
  public void JsonDeserialization_WhenDeserialized_ItShouldMatchExpectedObject()
  {
    var response = JsonSerializer.Deserialize<AnthropicFileDeleteResponse>(_testJson, JsonSerializationOptions.DefaultOptions);

    response.Should().NotBeNull();
    response.Id.Should().Be("file-12345");
    response.Type.Should().Be("file_deleted");
  }
}