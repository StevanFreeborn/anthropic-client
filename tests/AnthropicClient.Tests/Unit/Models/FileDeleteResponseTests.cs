namespace AnthropicClient.Tests.Unit.Models;

public class FileDeleteResponseTests : SerializationTest
{
  private const string SampleJson = @"{
    ""type"": ""file_deleted"",
    ""id"": ""file_abc123"",
    ""deleted"": true
  }";

  [Fact]
  public void Constructor_WhenCalled_ItShouldReturnAnInstanceWithPropertiesSet()
  {
    var response = new FileDeleteResponse();

    response.Type.Should().Be("file_deleted");
    response.Id.Should().BeEmpty();
    response.Deleted.Should().BeFalse();
  }

  [Fact]
  public void JsonDeserialization_WhenDeserialized_ItShouldHaveExpectedValues()
  {
    var result = Deserialize<FileDeleteResponse>(SampleJson);

    result.Should().NotBeNull();
    result!.Type.Should().Be("file_deleted");
    result.Id.Should().Be("file_abc123");
    result.Deleted.Should().BeTrue();
  }

  [Fact]
  public void JsonSerialization_WhenSerialized_ItShouldReturnExpectedShape()
  {
    var response = new FileDeleteResponse
    {
      Type = "file_deleted",
      Id = "file_abc123",
      Deleted = true
    };

    var result = Serialize(response);

    var expectedJson = @"{
      ""type"": ""file_deleted"",
      ""id"": ""file_abc123"",
      ""deleted"": true
    }";

    JsonAssert.Equal(expectedJson, result);
  }
}