namespace AnthropicClient.Tests.Unit.Models;

public class AnthropicFileTests : SerializationTest
{
  private readonly string _testJson = @"{
    ""id"": ""file-123"",
    ""type"": ""file"",
    ""filename"": ""test.txt"",
    ""created_at"": ""2023-10-01T00:00:00Z"",
    ""size_bytes"": 1024,
    ""mime_type"": ""text/plain"",
    ""downloadable"": true
  }";

  [Fact]
  public void Constructor_WhenCalled_ItShouldInitializeProperties()
  {
    var result = new AnthropicFile();

    result.Id.Should().BeEmpty();
    result.Type.Should().BeEmpty();
    result.Name.Should().BeEmpty();
    result.CreatedAt.Should().Be(DateTimeOffset.MinValue);
    result.Size.Should().Be(0);
    result.MimeType.Should().BeEmpty();
    result.Downloadable.Should().BeFalse();
  }

  [Fact]
  public void Constructor_WhenCalledWithValues_ItShouldInitializeProperties()
  {
    var file = new AnthropicFile
    {
      Id = "file-123",
      Type = "file",
      Name = "test.txt",
      CreatedAt = new DateTimeOffset(2023, 10, 1, 0, 0, 0, TimeSpan.Zero),
      Size = 1024,
      MimeType = "text/plain",
      Downloadable = true
    };

    file.Id.Should().Be("file-123");
    file.Type.Should().Be("file");
    file.Name.Should().Be("test.txt");
    file.CreatedAt.Should().Be(new DateTimeOffset(2023, 10, 1, 0, 0, 0, TimeSpan.Zero));
    file.Size.Should().Be(1024);
    file.MimeType.Should().Be("text/plain");
    file.Downloadable.Should().BeTrue();
  }

  [Fact]
  public void JsonSerialization_WhenSerialized_ItShouldHaveExpectedShape()
  {
    var file = new AnthropicFile
    {
      Id = "file-123",
      Type = "file",
      Name = "test.txt",
      CreatedAt = new DateTimeOffset(2023, 10, 1, 0, 0, 0, TimeSpan.Zero),
      Size = 1024,
      MimeType = "text/plain",
      Downloadable = true
    };

    var json = Serialize(file);

    var expectedJson = @"{
      ""id"": ""file-123"",
      ""type"": ""file"",
      ""filename"": ""test.txt"",
      ""created_at"": ""2023-10-01T00:00:00+00:00"",
      ""size_bytes"": 1024,
      ""mime_type"": ""text/plain"",
      ""downloadable"": true
    }";

    JsonAssert.Equal(expectedJson, json, true);
  }

  [Fact]
  public void JsonDeserialization_WhenDeserialized_ItShouldHaveExpectedValues()
  {
    var file = Deserialize<AnthropicFile>(_testJson);

    file.Should().NotBeNull();
    file!.Id.Should().Be("file-123");
    file.Type.Should().Be("file");
    file.Name.Should().Be("test.txt");
    file.CreatedAt.Should().Be(new DateTimeOffset(2023, 10, 1, 0, 0, 0, TimeSpan.Zero));
    file.Size.Should().Be(1024);
    file.MimeType.Should().Be("text/plain");
    file.Downloadable.Should().BeTrue();
  }
}