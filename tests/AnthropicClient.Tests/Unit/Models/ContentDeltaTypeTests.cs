namespace AnthropicClient.Tests.Unit.Models;

public class ContentDeltaTypeTests
{
  [Fact]
  public void TextDelta_WhenCalled_ItShouldReturnExpectedValue()
  {
    var expected = "text_delta";

    var actual = ContentDeltaType.TextDelta;

    actual.Should().Be(expected);
  }

  [Fact]
  public void JsonDelta_WhenCalled_ItShouldReturnExpectedValue()
  {
    var expected = "input_json_delta";

    var actual = ContentDeltaType.JsonDelta;

    actual.Should().Be(expected);
  }

  [Fact]
  public void CitationDelta_WhenCalled_ItShouldReturnExpectedValue()
  {
    var expected = "citations_delta";

    var actual = ContentDeltaType.CitationDelta;

    actual.Should().Be(expected);
  }
}