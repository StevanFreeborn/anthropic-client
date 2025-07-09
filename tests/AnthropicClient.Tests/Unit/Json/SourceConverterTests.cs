namespace AnthropicClient.Tests.Unit.Json;

public class SourceConverterTests : SerializationTest
{
  [Fact]
  public void JsonDeserialization_WhenTypeIsUnknown_ItThrowsException()
  {
    var json = @"{ ""type"": ""unknown"" }";

    var action = () => Deserialize<Source>(json);

    action.Should().Throw<JsonException>();
  }

  [Fact]
  public void JsonSerialization_WhenSourceIsNotKnown_ItShouldHaveExpectedShape()
  {
    var source = new TestSource();

    var result = Serialize<Source>(source);

    JsonAssert.Equal(@"{ ""type"": ""test"" }", result);
  }

  private class TestSource : Source
  {
    public TestSource() : base("test")
    {
    }
  }
}