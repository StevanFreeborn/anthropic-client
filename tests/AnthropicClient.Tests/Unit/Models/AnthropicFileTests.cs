namespace AnthropicClient.Tests.Unit.Models;

public class AnthropicFileTests : SerializationTest
{
  private const string SampleJson = @"{
    ""type"": ""file"",
    ""id"": ""file_abc123"",
    ""filename"": ""example.txt"",
    ""content_type"": ""text/plain"",
    ""size_bytes"": 1024,
    ""created_at"": ""2024-03-15T10:30:00Z""
  }";

  [Fact]
  public void Constructor_WhenCalled_ItShouldReturnAnInstanceWithPropertiesSet()
  {
    var file = new AnthropicFile();

    file.Type.Should().Be("file");
    file.Id.Should().BeEmpty();
    file.Filename.Should().BeEmpty();
    file.ContentType.Should().BeEmpty();
    file.SizeBytes.Should().Be(0);
    file.CreatedAt.Should().Be(default);
  }

  [Fact]
  public void JsonDeserialization_WhenDeserialized_ItShouldHaveExpectedValues()
  {
    var result = Deserialize<AnthropicFile>(SampleJson);

    result.Should().NotBeNull();
    result!.Type.Should().Be("file");
    result.Id.Should().Be("file_abc123");
    result.Filename.Should().Be("example.txt");
    result.ContentType.Should().Be("text/plain");
    result.SizeBytes.Should().Be(1024);
    result.CreatedAt.Should().Be(new DateTimeOffset(2024, 3, 15, 10, 30, 0, TimeSpan.Zero));
  }

  [Fact]
  public void JsonSerialization_WhenSerialized_ItShouldReturnExpectedShape()
  {
    var file = new AnthropicFile
    {
      Type = "file",
      Id = "file_abc123",
      Filename = "example.txt",
      ContentType = "text/plain",
      SizeBytes = 1024,
      CreatedAt = new DateTimeOffset(2024, 3, 15, 10, 30, 0, TimeSpan.Zero)
    };

    var result = Serialize(file);

    var expectedJson = @"{
      ""type"": ""file"",
      ""id"": ""file_abc123"",
      ""filename"": ""example.txt"",
      ""content_type"": ""text/plain"",
      ""size_bytes"": 1024,
      ""created_at"": ""2024-03-15T10:30:00+00:00""
    }";

    JsonAssert.Equal(expectedJson, result);
  }
}