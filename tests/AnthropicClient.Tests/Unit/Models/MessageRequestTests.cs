namespace AnthropicClient.Tests.Unit.Models;

public class MessageRequestTests : SerializationTest
{
  private readonly string _testJson = @"{
    ""model"": ""claude-3-sonnet-20240229"",
    ""system"": [{
      ""type"": ""text"",
      ""text"": ""test-system""
    }],
    ""messages"": [
      { ""role"": ""user"", ""content"": [{ ""text"": ""Hello!"", ""type"": ""text"" }] }
    ],
    ""max_tokens"": 512,
    ""metadata"": { ""test"":""test"" },
    ""stop_sequences"": [],
    ""temperature"": 0.5,
    ""topK"": 10,
    ""topP"": 0.5,
    ""tool_choice"": { ""type"":""auto"" },
    ""tools"": [],
    ""stream"": false
  }";

  private readonly string _testJsonWithAnyToolChoice = @"{
    ""model"": ""claude-3-sonnet-20240229"",
    ""system"": [{
      ""type"": ""text"",
      ""text"": ""test-system""
    }], 
    ""messages"":[
      { ""role"": ""user"", ""content"": [{ ""text"": ""Hello!"", ""type"":""text"" }] }
    ],
    ""max_tokens"": 512,
    ""metadata"": {""test"":""test""},
    ""stop_sequences"": [],
    ""temperature"": 0.5,
    ""topK"": 10,
    ""topP"": 0.5,
    ""tool_choice"": {""type"":""any""},
    ""tools"": [],
    ""stream"":false
  }";

  private readonly string _testJsonWithSpecificToolChoice = @"{
    ""model"": ""claude-3-sonnet-20240229"",
    ""system"": [{
      ""type"": ""text"",
      ""text"": ""test-system""
    }],
    ""messages"": [
      { ""role"": ""user"", ""content"": [{ ""text"": ""Hello!"", ""type"": ""text"" }] }
    ],
    ""max_tokens"": 512,
    ""metadata"": { ""test"": ""test"" },
    ""stop_sequences"": [],
    ""temperature"": 0.5,
    ""topK"": 10,
    ""topP"": 0.5,
    ""tool_choice"": { ""type"": ""tool"", ""name"": ""test-tool"" },
    ""tools"": [],
    ""stream"": false
  }";

  private readonly string _testJsonWithImageContent = @"{
    ""model"": ""claude-3-sonnet-20240229"",
    ""system"": [{
      ""type"": ""text"",
      ""text"": ""test-system""
    }],
    ""messages"":[
      { 
        ""role"": ""user"", 
        ""content"": [
          { 
            ""type"": ""image"", 
            ""source"": { ""type"": ""base64"", ""media_type"": ""image/jpeg"", ""data"": ""data"" } 
          }
        ] 
      }
    ],
    ""max_tokens"": 512,
    ""metadata"": { ""test"": ""test"" },
    ""stop_sequences"": [],
    ""temperature"": 0.5,
    ""topK"": 10,
    ""topP"": 0.5,
    ""tool_choice"": { ""type"": ""auto"" },
    ""tools"":[],
    ""stream"": false
  }";

  private readonly string _testJsonWithUnknownContent = @"{
    ""model"": ""claude-3-sonnet-20240229"",
    ""system"": [{
      ""type"": ""text"",
      ""text"": ""test-system""
    }],
    ""messages"": [{ ""role"": ""user"", ""content"": [{ ""type"": ""unknown"", ""text"": ""text"" }] }],
    ""max_tokens"": 512,
    ""metadata"": { ""test"": ""test"" },
    ""stop_sequences"": [],
    ""temperature"": 0.5,
    ""topK"": 10,
    ""topP"": 0.5,
    ""tool_choice"": { ""type"":""auto"" },
    ""tools"": [],
    ""stream"": false
  }";

  private readonly string _testJsonWithToolUseContent = @"{
    ""model"": ""claude-3-sonnet-20240229"",
    ""system"": [{
      ""type"": ""text"",
      ""text"": ""test-system""
    }],
    ""messages"": [
      {
        ""role"": ""assistant"",
        ""content"": [
          {
            ""type"": ""tool_use"",
            ""name"": ""test-tool"",
            ""id"": ""test-tool-id"",
            ""input"": {
              ""test-property"": ""test-value""
            }
          }
        ]
      }
    ],
    ""max_tokens"": 512,
    ""metadata"": { ""test"": ""test"" },
    ""stop_sequences"": [],
    ""temperature"": 0.5,
    ""topK"": 10,
    ""topP"": 0.5,
    ""tool_choice"": { ""type"": ""auto"" },
    ""tools"": [],
    ""stream"": false
  }";

  private readonly string _testJsonWithToolResultContent = @"{
    ""model"": ""claude-3-sonnet-20240229"",
    ""system"": [{
      ""type"": ""text"",
      ""text"": ""test-system""
    }],
    ""messages"": [
      {
        ""role"": ""assistant"",
        ""content"": [
          {
            ""type"": ""tool_result"",
            ""tool_use_id"": ""test-tool"",
            ""content"": ""test-value""
          }
        ]
      }
    ],
    ""max_tokens"": 512,
    ""metadata"": { ""test"": ""test"" },
    ""stop_sequences"": [],
    ""temperature"": 0.5,
    ""topK"": 10,
    ""topP"": 0.5,
    ""tool_choice"": { ""type"": ""auto"" },
    ""tools"": [],
    ""stream"":false
  }";

  [Fact]
  public void Constructor_WhenCalled_ItShouldInitializeProperties()
  {
    var model = AnthropicModels.Claude3Sonnet;
    var messages = new List<Message> { new() };
    var maxTokens = 512;
    var system = "test-system";
    var metadata = new Dictionary<string, object> { ["test"] = "test" };
    var temperature = 0.5m;
    var topK = 10;
    var topP = 0.5m;
    var toolChoice = new AutoToolChoice();
    var tools = new List<Tool>();

    var messageRequest = new MessageRequest(
      model: model,
      messages: messages,
      maxTokens: maxTokens,
      system: system,
      metadata: metadata,
      temperature: temperature,
      topK: topK,
      topP: topP,
      toolChoice: toolChoice,
      tools: tools
    );

    messageRequest.Model.Should().Be(model);
    messageRequest.Messages.Should().BeSameAs(messages);
    messageRequest.MaxTokens.Should().Be(maxTokens);
    messageRequest.System.Should().Be(system);
    messageRequest.Metadata.Should().BeSameAs(metadata);
    messageRequest.Temperature.Should().Be(temperature);
    messageRequest.TopK.Should().Be(topK);
    messageRequest.TopP.Should().Be(topP);
    messageRequest.ToolChoice.Should().Be(toolChoice);
    messageRequest.Tools.Should().BeSameAs(tools);
    messageRequest.Stream.Should().BeFalse();
  }

  [Fact]
  public void Constructor_WhenCalledAndModelIsNull_ItShouldThrowArgumentNullException()
  {
    var action = () => new MessageRequest(
      model: null!,
      messages: [new()]
    );

    action.Should().Throw<ArgumentNullException>();
  }

  [Fact]
  public void Constructor_WhenCalledAndMessagesIsNull_ItShouldThrowArgumentNullException()
  {
    var action = () => new MessageRequest(
      model: AnthropicModels.Claude3Sonnet,
      messages: null!
    );

    action.Should().Throw<ArgumentNullException>();
  }

  [Fact]
  public void Constructor_WhenCalledAndModelIsInvalid_ItShouldNotThrowException()
  {
    var action = () => new MessageRequest(
      model: "invalid-model",
      messages: [new()]
    );

    action.Should().NotThrow();
  }

  [Fact]
  public void Constructor_WhenCalledAndMessagesIsEmpty_ItShouldThrowArgumentException()
  {
    var action = () => new MessageRequest(
      model: AnthropicModels.Claude3Sonnet,
      messages: []
    );

    action.Should().Throw<ArgumentException>();
  }

  [Fact]
  public void Constructor_WhenCalledAndMaxTokensIsInvalid_ItShouldThrowArgumentException()
  {
    var action = () => new MessageRequest(
      model: AnthropicModels.Claude3Sonnet,
      messages: [new()],
      maxTokens: 0
    );

    action.Should().Throw<ArgumentException>();
  }

  [Theory]
  [InlineData(-1)]
  [InlineData(2)]
  public void Constructor_WhenCalledAndTemperatureIsInvalid_ItShouldThrowArgumentException(decimal temperature)
  {
    var action = () => new MessageRequest(
      model: AnthropicModels.Claude3Sonnet,
      messages: [new()],
      temperature: temperature
    );

    action.Should().Throw<ArgumentException>();
  }

  [Fact]
  public void JsonSerialization_WhenSerialized_ItShouldHaveExpectedShape()
  {
    var messages = new List<Message>()
    {
      new()
      {
        Role = MessageRole.User,
        Content = [new TextContent("Hello!")]
      }
    };

    var model = AnthropicModels.Claude3Sonnet;
    var maxTokens = 512;
    var system = "test-system";
    var metadata = new Dictionary<string, object>
    {
      ["test"] = "test"
    };
    var temperature = 0.5m;
    var topK = 10;
    var topP = 0.5m;
    var toolChoice = new AutoToolChoice();
    var tools = new List<Tool>();

    var messageRequest = new MessageRequest(
      model: model,
      messages: messages,
      maxTokens: maxTokens,
      system: system,
      metadata: metadata,
      temperature: temperature,
      topK: topK,
      topP: topP,
      toolChoice: toolChoice,
      tools: tools
    );

    var actual = Serialize(messageRequest);

    JsonAssert.Equal(_testJson, actual);
  }

  [Fact]
  public void JsonSerialization_WhenSerializedAndSystemMessagesAndSystemAreNull_ItShouldNotHaveSystemProperty()
  {
    var messageRequest = new MessageRequest(
      model: AnthropicModels.Claude3Haiku,
      messages: [
        new()
        {
          Role = MessageRole.User,
          Content = [new TextContent("Hello!")]
        }
      ]
    );

    var expected = @"{
      ""model"": ""claude-3-haiku-20240307"",
      ""messages"": [
        {
          ""role"": ""user"",
          ""content"": [
            {
              ""text"": ""Hello!"",
              ""type"": ""text""
            }
          ]
        }
      ],
      ""max_tokens"": 1024,
      ""stop_sequences"": [],
      ""temperature"": 0.0,
      ""stream"": false
    }";

    var actual = Serialize(messageRequest);

    JsonAssert.Equal(expected, actual);
  }

  [Fact]
  public void JsonSerialization_WhenSerializedAndSystemMessagesAreProvided_ItShouldUseSystemMessagesForHaveSystemProperty()
  {
    var messageRequest = new MessageRequest(
      model: AnthropicModels.Claude3Haiku,
      messages: [
        new()
        {
          Role = MessageRole.User,
          Content = [new TextContent("Hello!")]
        }
      ],
      systemMessages: [
        new TextContent("You are a helpful assistant.")
      ],
      system: "test-system"
    );

    var expected = @"{
      ""model"": ""claude-3-haiku-20240307"",
      ""system"": [
        {
          ""text"": ""You are a helpful assistant."",
          ""type"": ""text""
        }
      ],
      ""messages"": [
        {
          ""role"": ""user"",
          ""content"": [
            {
              ""text"": ""Hello!"",
              ""type"": ""text""
            }
          ]
        }
      ],
      ""max_tokens"": 1024,
      ""stop_sequences"": [],
      ""temperature"": 0.0,
      ""stream"": false
    }";

    var actual = Serialize(messageRequest);

    JsonAssert.Equal(expected, actual);
  }

  [Fact]
  public void JsonSerialization_WhenSerializedAndSystemMessageIsProvided_ItShouldUseSystemMessageForHaveSystemProperty()
  {
    var messageRequest = new MessageRequest(
      model: AnthropicModels.Claude3Haiku,
      messages: [
        new()
        {
          Role = MessageRole.User,
          Content = [new TextContent("Hello!")]
        }
      ],
      system: "test-system"
    );

    var expected = @"{
      ""model"": ""claude-3-haiku-20240307"",
      ""system"": [
        {
          ""text"": ""test-system"",
          ""type"": ""text""
        }
      ],
      ""messages"": [
        {
          ""role"": ""user"",
          ""content"": [
            {
              ""text"": ""Hello!"",
              ""type"": ""text""
            }
          ]
        }
      ],
      ""max_tokens"": 1024,
      ""stop_sequences"": [],
      ""temperature"": 0.0,
      ""stream"": false
    }";

    var actual = Serialize(messageRequest);

    JsonAssert.Equal(expected, actual);
  }

  [Fact]
  public void JsonDeserialization_WhenDeserialized_ItShouldHaveExpectedShape()
  {
    var messageRequest = Deserialize<MessageRequest>(_testJson);

    messageRequest!.Model.Should().Be(AnthropicModels.Claude3Sonnet);
    messageRequest.System.Should().BeNull();
    messageRequest.Messages.Should().HaveCount(1);
    messageRequest.MaxTokens.Should().Be(512);
    messageRequest.Metadata.Should().HaveCount(1);

    var testValue = messageRequest.Metadata!.GetValueOrDefault("test")!.ToString();
    testValue.Should().Be("test");

    messageRequest.Temperature.Should().Be(0.5m);
    messageRequest.TopK.Should().Be(10);
    messageRequest.TopP.Should().Be(0.5m);
    messageRequest.ToolChoice.Should().BeOfType<AutoToolChoice>();
    messageRequest.ToolChoice!.Type.Should().Be("auto");
    messageRequest.Tools.Should().HaveCount(0);
    messageRequest.Stream.Should().BeFalse();
  }

  [Fact]
  public void JsonDeserialization_WhenDeserializedWithAnyToolChoice_ItShouldHaveExpectedShape()
  {
    var messageRequest = Deserialize<MessageRequest>(_testJsonWithAnyToolChoice);

    messageRequest!.Model.Should().Be(AnthropicModels.Claude3Sonnet);
    messageRequest.System.Should().BeNull();
    messageRequest.Messages.Should().HaveCount(1);
    messageRequest.MaxTokens.Should().Be(512);
    messageRequest.Metadata.Should().HaveCount(1);

    var testValue = messageRequest.Metadata!.GetValueOrDefault("test")!.ToString();
    testValue.Should().Be("test");

    messageRequest.Temperature.Should().Be(0.5m);
    messageRequest.TopK.Should().Be(10);
    messageRequest.TopP.Should().Be(0.5m);
    messageRequest.ToolChoice.Should().BeOfType<AnyToolChoice>();
    messageRequest.ToolChoice!.Type.Should().Be("any");
    messageRequest.Tools.Should().HaveCount(0);
  }

  [Fact]
  public void JsonDeserialization_WhenDeserializedWithSpecificToolChoice_ItShouldHaveExpectedShape()
  {
    var messageRequest = Deserialize<MessageRequest>(_testJsonWithSpecificToolChoice);

    messageRequest!.Model.Should().Be(AnthropicModels.Claude3Sonnet);
    messageRequest.System.Should().BeNull();
    messageRequest.Messages.Should().HaveCount(1);
    messageRequest.MaxTokens.Should().Be(512);
    messageRequest.Metadata.Should().HaveCount(1);

    var testValue = messageRequest.Metadata!.GetValueOrDefault("test")!.ToString();
    testValue.Should().Be("test");

    messageRequest.Temperature.Should().Be(0.5m);
    messageRequest.TopK.Should().Be(10);
    messageRequest.TopP.Should().Be(0.5m);
    messageRequest.ToolChoice.Should().BeOfType<SpecificToolChoice>();

    var specificToolChoice = messageRequest.ToolChoice as SpecificToolChoice;
    specificToolChoice!.Type.Should().Be("tool");
    specificToolChoice.Name.Should().Be("test-tool");
    messageRequest.Tools.Should().HaveCount(0);
  }

  [Fact]
  public void JsonDeserialization_WhenDeserializedWithUnknownToolChoice_ItShouldThrowJsonException()
  {
    var json = @"{""model"":""claude-3-sonnet-20240229"",""system"":""test-system"",""messages"":[{""role"":""user"",""content"":[{""text"":""Hello!"",""type"":""text""}]}],""max_tokens"":512,""metadata"":{""test"":""test""},""stop_sequences"":[],""temperature"":0.5,""topK"":10,""topP"":0.5,""tool_choice"":{""type"":""unknown""},""tools"":[{""name"":""test-tool"",""description"":""test-description"",""input_schema"":{""type"":""object"",""properties"":{""test-property"":{""type"":""string"",""description"":""test-description""}},""required"":[""test-property""]}}],""stream"":false}";

    var action = () => Deserialize<MessageRequest>(json);

    action.Should().Throw<JsonException>();
  }

  [Fact]
  public void JsonDeserialization_WhenDeserializedWithImageContent_ItShouldHaveExpectedShape()
  {
    var messageRequest = Deserialize<MessageRequest>(_testJsonWithImageContent);

    messageRequest!.Model.Should().Be(AnthropicModels.Claude3Sonnet);
    messageRequest.System.Should().BeNull();
    messageRequest.Messages.Should().HaveCount(1);
    messageRequest.MaxTokens.Should().Be(512);
    messageRequest.Metadata.Should().HaveCount(1);

    var testValue = messageRequest.Metadata!.GetValueOrDefault("test")!.ToString();
    testValue.Should().Be("test");

    messageRequest.Temperature.Should().Be(0.5m);
    messageRequest.TopK.Should().Be(10);
    messageRequest.TopP.Should().Be(0.5m);
    messageRequest.ToolChoice.Should().BeOfType<AutoToolChoice>();
    messageRequest.ToolChoice!.Type.Should().Be("auto");
    messageRequest.Tools.Should().HaveCount(0);
    messageRequest.Messages[0].Content.Should().HaveCount(1);
    messageRequest.Messages[0].Content[0].Should().BeOfType<ImageContent>();

    var imageContent = messageRequest.Messages[0].Content[0] as ImageContent;
    imageContent!.Type.Should().Be("image");

    imageContent.Source.As<ImageSource>().MediaType.Should().Be("image/jpeg");
    imageContent.Source.As<ImageSource>().Data.Should().Be("data");
  }

  [Fact]
  public void JsonDeserialization_WhenDeserializedWithToolUseContent_ItShouldHaveExpectedShape()
  {
    var messageRequest = Deserialize<MessageRequest>(_testJsonWithToolUseContent);

    messageRequest!.Model.Should().Be(AnthropicModels.Claude3Sonnet);
    messageRequest.System.Should().BeNull();
    messageRequest.Messages.Should().HaveCount(1);
    messageRequest.MaxTokens.Should().Be(512);
    messageRequest.Metadata.Should().HaveCount(1);

    var testValue = messageRequest.Metadata!.GetValueOrDefault("test")!.ToString();
    testValue.Should().Be("test");

    messageRequest.Temperature.Should().Be(0.5m);
    messageRequest.TopK.Should().Be(10);
    messageRequest.TopP.Should().Be(0.5m);
    messageRequest.ToolChoice.Should().BeOfType<AutoToolChoice>();
    messageRequest.ToolChoice!.Type.Should().Be("auto");
    messageRequest.Tools.Should().HaveCount(0);
    messageRequest.Messages[0].Content.Should().HaveCount(1);
    messageRequest.Messages[0].Content[0].Should().BeOfType<ToolUseContent>();

    var toolUseContent = messageRequest.Messages[0].Content[0] as ToolUseContent;
    toolUseContent!.Type.Should().Be("tool_use");
    toolUseContent.Name.Should().Be("test-tool");
    toolUseContent.Id.Should().Be("test-tool-id");
    toolUseContent.Input.Should().HaveCount(1);
    toolUseContent.Input["test-property"]!.ToString().Should().Be("test-value");
  }

  [Fact]
  public void JsonDeserialization_WhenDeserializedWithToolResultContent_ItShouldHaveExpectedShape()
  {
    var messageRequest = Deserialize<MessageRequest>(_testJsonWithToolResultContent);

    messageRequest!.Model.Should().Be(AnthropicModels.Claude3Sonnet);
    messageRequest.System.Should().BeNull();
    messageRequest.Messages.Should().HaveCount(1);
    messageRequest.MaxTokens.Should().Be(512);
    messageRequest.Metadata.Should().HaveCount(1);

    var testValue = messageRequest.Metadata!.GetValueOrDefault("test")!.ToString();
    testValue.Should().Be("test");

    messageRequest.Temperature.Should().Be(0.5m);
    messageRequest.TopK.Should().Be(10);
    messageRequest.TopP.Should().Be(0.5m);
    messageRequest.ToolChoice.Should().BeOfType<AutoToolChoice>();
    messageRequest.ToolChoice!.Type.Should().Be("auto");
    messageRequest.Tools.Should().HaveCount(0);
    messageRequest.Messages[0].Content.Should().HaveCount(1);
    messageRequest.Messages[0].Content[0].Should().BeOfType<ToolResultContent>();

    var toolResultContent = messageRequest.Messages[0].Content[0] as ToolResultContent;
    toolResultContent!.Type.Should().Be("tool_result");
    toolResultContent.ToolUseId.Should().Be("test-tool");
    toolResultContent.Content.Should().Be("test-value");
  }

  [Fact]
  public void JsonDeserialization_WhenDeserializedWithUnknownContent_ItShouldThrowJsonException()
  {
    var action = () => Deserialize<MessageRequest>(_testJsonWithUnknownContent);

    action.Should().Throw<JsonException>();
  }
}