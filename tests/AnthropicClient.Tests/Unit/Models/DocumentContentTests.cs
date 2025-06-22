namespace AnthropicClient.Tests.Unit.Models;

public class DocumentContentTests : SerializationTest
{
  private readonly string _testJson = @"{
    ""source"": { 
      ""media_type"": ""application/pdf"",
      ""data"": ""data"",
      ""type"": ""base64""
    },
    ""type"": ""document""
  }";

  private readonly string _testJsonWithCacheControl = @"{
    ""source"": { 
      ""media_type"": ""application/pdf"",
      ""data"": ""data"",
      ""type"": ""base64""
    },
    ""cache_control"": { ""type"": ""ephemeral"" },
    ""type"": ""document""
  }";

  [Fact]
  public void Constructor_WhenCalled_ItShouldInitializeSource()
  {
    var expectedMediaType = "application/pdf";
    var expectedData = "data";

    var result = new DocumentContent(expectedMediaType, expectedData);

    result.Source.Should().BeEquivalentTo(new DocumentSource(expectedMediaType, expectedData));
  }

  [Fact]
  public void Constructor_WhenCalledAndMediaTypeIsNull_ItShouldThrowArgumentNullException()
  {
    var expectedData = "data";

    var action = () => new DocumentContent(null!, expectedData);

    action.Should().Throw<ArgumentNullException>();
  }

  [Fact]
  public void Constructor_WhenCalledAndDataIsNull_ItShouldThrowArgumentNullException()
  {
    var expectedMediaType = "application/pdf";

    var action = () => new DocumentContent(expectedMediaType, null!);

    action.Should().Throw<ArgumentNullException>();
  }

  [Fact]
  public void Constructor_WhenCalledWithCacheControl_ItShouldInitializeSourceAndCacheControl()
  {
    var expectedMediaType = "application/pdf";
    var expectedData = "data";
    var cacheControl = new EphemeralCacheControl();

    var result = new DocumentContent(expectedMediaType, expectedData, cacheControl);

    result.Source.Should().BeEquivalentTo(new DocumentSource(expectedMediaType, expectedData));
    result.CacheControl.Should().BeSameAs(cacheControl);
  }

  [Fact]
  public void Constructor_WhenCalledWithCacheControlAndMediaTypeIsNull_ItShouldThrowArgumentNullException()
  {
    var expectedData = "data";
    var cacheControl = new EphemeralCacheControl();

    var action = () => new DocumentContent(null!, expectedData, cacheControl);

    action.Should().Throw<ArgumentNullException>();
  }

  [Fact]
  public void Constructor_WhenCalledWithCacheControlAndDataIsNull_ItShouldThrowArgumentNullException()
  {
    var expectedMediaType = "application/pdf";
    var cacheControl = new EphemeralCacheControl();

    var action = () => new DocumentContent(expectedMediaType, null!, cacheControl);

    action.Should().Throw<ArgumentNullException>();
  }

  [Fact]
  public void Constructor_WhenCalledWithSource_ItShouldInitializeSource()
  {
    var source = new DocumentSource("application/pdf", "data");

    var result = new DocumentContent(source);

    result.Source.Should().BeSameAs(source);
  }

  [Fact]
  public void Constructor_WhenCalledWithSourceAndCacheControl_ItShouldInitializeSourceAndCacheControl()
  {
    var source = new DocumentSource("application/pdf", "data");
    var cacheControl = new EphemeralCacheControl();

    var result = new DocumentContent(source, cacheControl);

    result.Source.Should().BeSameAs(source);
    result.CacheControl.Should().BeSameAs(cacheControl);
  }

  [Fact]
  public void JsonSerialization_WhenSerialized_ItShouldHaveExpectedShape()
  {
    var content = new DocumentContent("application/pdf", "data");

    var actual = Serialize(content);

    JsonAssert.Equal(_testJson, actual);
  }

  [Fact]
  public void JsonSerialization_WhenSerializedWithCacheControl_ItShouldHaveExpectedShape()
  {
    var content = new DocumentContent("application/pdf", "data", new EphemeralCacheControl());

    var actual = Serialize(content);

    JsonAssert.Equal(_testJsonWithCacheControl, actual);
  }

  [Fact]
  public void JsonDeserialization_WhenDeserialized_ItShouldHaveExpectedShape()
  {
    var expected = new DocumentContent("application/pdf", "data");

    var actual = Deserialize<DocumentContent>(_testJson);

    actual.Should().BeEquivalentTo(expected);
  }
}