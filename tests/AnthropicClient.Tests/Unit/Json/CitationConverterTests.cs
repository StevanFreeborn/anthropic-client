namespace AnthropicClient.Tests.Unit.Json;

public class CitationConverterTests : SerializationTest
{
  [Fact]
  public void JsonDeserialization_WhenTypeIsUnknown_ItThrowException()
  {
    var json = @"{ ""type"": ""unknown"" }";

    var action = () => Deserialize<Citation>(json);

    action.Should().Throw<JsonException>();
  }
}