namespace AnthropicClient.Tests.Unit.Models;

public class Base64SourceTests : SerializationTest
{
  private readonly string _testJson = @"{
    ""type"": ""base64"",
    ""media_type"": ""application/pdf"",
    ""data"": ""base64data""
  }";

  [Fact]
  public void Constructor_WhenCalledWithValidArguments_ItShouldSetProperties()
  {
    var mediaType = "application/pdf";
    var data = "base64data";

    var source = new Base64Source(mediaType, data);

    source.MediaType.Should().Be(mediaType);
    source.Data.Should().Be(data);
    source.Type.Should().Be("base64");
  }

  [Fact]
  public void Constructor_WhenCalledWithNullMediaType_ItShouldThrowArgumentNullException()
  {
    string? mediaType = null;
    var data = "base64data";

    var action = () => new Base64Source(mediaType!, data);

    action.Should().Throw<ArgumentNullException>();
  }

  [Fact]
  public void Constructor_WhenCalledWithNullData_ItShouldThrowArgumentNullException()
  {
    var mediaType = "application/pdf";
    string? data = null;

    var action = () => new Base64Source(mediaType, data!);

    action.Should().Throw<ArgumentNullException>();
  }

  [Fact]
  public void JsonSerialization_WhenSerialized_ItShouldHaveExpectedShape()
  {
    var mediaType = "application/pdf";
    var data = "base64data";
    var source = new Base64Source(mediaType, data);

    var result = Serialize<Source>(source);

    JsonAssert.Equal(_testJson, result);
  }

  [Fact]
  public void JsonDeserialization_WhenDeserialized_ItShouldHaveExpectedShape()
  {
    var source = Deserialize<Source>(_testJson);

    var base64Source = source.As<Base64Source>();
    base64Source!.Type.Should().Be("base64");
    base64Source.MediaType.Should().Be("application/pdf");
    base64Source.Data.Should().Be("base64data");
  }

  [Fact]
  public void JsonDeserialization_WhenMediaTypeIsMissing_ItShouldThrowException()
  {
    var json = @"{ ""type"": ""base64"", ""data"": ""base64data"" }";

    var action = () => Deserialize<Source>(json);

    action.Should().Throw<JsonException>();
  }

  [Fact]
  public void JsonDeserialization_WhenMediaTypeIsNull_ItShouldThrowException()
  {
    var json = @"{ ""type"": ""base64"", ""media_type"": null, ""data"": ""base64data"" }";

    var action = () => Deserialize<Source>(json);

    action.Should().Throw<JsonException>();
  }
}