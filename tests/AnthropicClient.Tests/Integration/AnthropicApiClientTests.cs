namespace AnthropicClient.Tests.Integration;

public class AnthropicApiClientTests : IntegrationTest
{
  [Theory]
  [ClassData(typeof(ErrorTestData))]
  public async Task CreateMessageAsync_WhenCalledAndErrorReturned_ItShouldHandleError(
    HttpStatusCode statusCode,
    string content,
    Type errorType
  )
  {
    _mockHttpMessageHandler
      .WhenCreateMessageRequest()
      .Respond(
        statusCode,
        "application/json",
        content
      );

    var request = new MessageRequest(
      model: AnthropicModels.Claude3Haiku,
      messages: [new(MessageRole.User, [new TextContent("Hello!")])]
    );

    var result = await Client.CreateMessageAsync(request);

    result.IsSuccess.Should().BeFalse();
    result.Error.Should().BeOfType<AnthropicError>();

    var actualErrorType = result.Error.Error!.GetType();
    actualErrorType.Should().Be(errorType);
  }

  [Fact]
  public async Task CreateMessageAsync_WhenCalledAndMessageCreatedWithTextContent_ItShouldReturnMessage()
  {
    _mockHttpMessageHandler
      .WhenCreateMessageRequest()
      .Respond(
        HttpStatusCode.OK,
        "application/json",
        @"{
            ""content"": [
              {
                ""text"": ""Hi! My name is Claude."",
                ""type"": ""text""
              }
            ],
            ""id"": ""msg_013Zva2CMHLNnXjNJJKqJ2EF"",
            ""model"": ""claude-3-5-sonnet-20240620"",
            ""role"": ""assistant"",
            ""stop_reason"": ""end_turn"",
            ""stop_sequence"": null,
            ""type"": ""message"",
            ""usage"": {
              ""input_tokens"": 10,
              ""output_tokens"": 25
            }
        }"
      );

    var request = new MessageRequest(
      model: AnthropicModels.Claude35Sonnet,
      messages: [new(MessageRole.User, [new TextContent("Hello!")])]
    );

    var result = await Client.CreateMessageAsync(request);

    result.IsSuccess.Should().BeTrue();
    result.Value.Should().BeOfType<MessageResponse>();

    var message = result.Value;
    message.Id.Should().Be("msg_013Zva2CMHLNnXjNJJKqJ2EF");
    message.Model.Should().Be("claude-3-5-sonnet-20240620");
    message.Role.Should().Be("assistant");
    message.StopReason.Should().Be("end_turn");
    message.StopSequence.Should().BeNull();
    message.Type.Should().Be("message");
    message.Usage.InputTokens.Should().Be(10);
    message.Usage.OutputTokens.Should().Be(25);
    message.Content.Should().HaveCount(1);
    message.ToolCall.Should().BeNull();

    var textContent = message.Content[0];
    textContent.Should().BeOfType<TextContent>();
    textContent.As<TextContent>().Text.Should().Be("Hi! My name is Claude.");
    textContent.As<TextContent>().Type.Should().Be("text");
  }

  [Fact]
  public async Task CreateMessageAsync_WhenCalledAndMessageCreatedWithToolUseContentAndToolIsProvided_ItShouldReturnMessageWithToolCall()
  {
    _mockHttpMessageHandler
      .WhenCreateMessageRequest()
      .Respond(
        HttpStatusCode.OK,
        "application/json",
        @"{
            ""content"": [
              {
                ""id"": ""toolu_01D7FLrfh4GYq7yT1ULFeyMV"",
                ""name"": ""get_stock_price"",
                ""input"": { ""ticker"": ""^GSPC"" },
                ""type"": ""tool_use""
              }
            ],
            ""id"": ""msg_01D7FLrfh4GYq7yT1ULFeyMV"",
            ""model"": ""claude-3-5-sonnet-20240620"",
            ""role"": ""assistant"",
            ""stop_reason"": ""end_turn"",
            ""stop_sequence"": null,
            ""type"": ""message"",
            ""usage"": {
              ""input_tokens"": 10,
              ""output_tokens"": 25
            }
        }"
      );

    var func = (string ticker) => ticker;

    var request = new MessageRequest(
      model: AnthropicModels.Claude35Sonnet,
      messages: [
        new(MessageRole.User, [new TextContent("Hello!")]),
      ],
      tools: [
        Tool.CreateFromFunction("get_stock_price", "Gets the stock price for a given ticker", func)
      ]
    );

    var result = await Client.CreateMessageAsync(request);

    result.IsSuccess.Should().BeTrue();
    result.Value.Should().BeOfType<MessageResponse>();

    var message = result.Value;
    message.Id.Should().Be("msg_01D7FLrfh4GYq7yT1ULFeyMV");
    message.Model.Should().Be("claude-3-5-sonnet-20240620");
    message.Role.Should().Be("assistant");
    message.StopReason.Should().Be("end_turn");
    message.StopSequence.Should().BeNull();
    message.Type.Should().Be("message");
    message.Usage.InputTokens.Should().Be(10);
    message.Usage.OutputTokens.Should().Be(25);
    message.Content.Should().HaveCount(1);
    message.ToolCall.Should().NotBeNull();

    var toolUseContent = message.Content[0];
    toolUseContent.Should().BeOfType<ToolUseContent>();

    toolUseContent.As<ToolUseContent>().Id.Should().Be("toolu_01D7FLrfh4GYq7yT1ULFeyMV");
    toolUseContent.As<ToolUseContent>().Name.Should().Be("get_stock_price");
    toolUseContent.As<ToolUseContent>().Type.Should().Be("tool_use");

    var input = toolUseContent.As<ToolUseContent>().Input;
    var ticker = input.GetValueOrDefault("ticker");
    ticker!.ToString().Should().Be("^GSPC");

    var toolCallResult = await message.ToolCall!.InvokeAsync();
    toolCallResult.IsSuccess.Should().BeTrue();
    toolCallResult.Value!.ToString().Should().Be("^GSPC");
  }


  [Fact]
  public async Task CreateMessageAsync_WhenCalledAndMessageCreatedWithToolUseButNoToolProvided_ItShouldReturnMessageWithoutToolCall()
  {
    _mockHttpMessageHandler
      .WhenCreateMessageRequest()
      .Respond(
        HttpStatusCode.OK,
        "application/json",
        @"{
            ""content"": [
              {
                ""id"": ""toolu_01D7FLrfh4GYq7yT1ULFeyMV"",
                ""name"": ""get_stock_price"",
                ""input"": { ""ticker"": ""^GSPC"" },
                ""type"": ""tool_use""
              }
            ],
            ""id"": ""msg_01D7FLrfh4GYq7yT1ULFeyMV"",
            ""model"": ""claude-3-5-sonnet-20240620"",
            ""role"": ""assistant"",
            ""stop_reason"": ""end_turn"",
            ""stop_sequence"": null,
            ""type"": ""message"",
            ""usage"": {
              ""input_tokens"": 10,
              ""output_tokens"": 25
            }
        }"
      );

    var request = new MessageRequest(
      model: AnthropicModels.Claude35Sonnet,
      messages: [
        new(MessageRole.User, [new TextContent("Hello!")]),
      ]
    );

    var result = await Client.CreateMessageAsync(request);

    result.IsSuccess.Should().BeTrue();
    result.Value.Should().BeOfType<MessageResponse>();

    var message = result.Value;
    message.Id.Should().Be("msg_01D7FLrfh4GYq7yT1ULFeyMV");
    message.Model.Should().Be("claude-3-5-sonnet-20240620");
    message.Role.Should().Be("assistant");
    message.StopReason.Should().Be("end_turn");
    message.StopSequence.Should().BeNull();
    message.Type.Should().Be("message");
    message.Usage.InputTokens.Should().Be(10);
    message.Usage.OutputTokens.Should().Be(25);
    message.Content.Should().HaveCount(1);
    message.ToolCall.Should().BeNull();

    var toolUseContent = message.Content[0];
    toolUseContent.Should().BeOfType<ToolUseContent>();

    toolUseContent.As<ToolUseContent>().Id.Should().Be("toolu_01D7FLrfh4GYq7yT1ULFeyMV");
    toolUseContent.As<ToolUseContent>().Name.Should().Be("get_stock_price");
    toolUseContent.As<ToolUseContent>().Type.Should().Be("tool_use");

    var input = toolUseContent.As<ToolUseContent>().Input;
    var ticker = input.GetValueOrDefault("ticker");
    ticker!.ToString().Should().Be("^GSPC");
  }

  [Theory]
  [ClassData(typeof(EventTestData))]
  public async Task CreateMessageAsync_WhenCalledAndMessageIsStreamed_ItShouldHandleAllEventTypes(string eventString, AnthropicEvent anthropicEvent)
  {
    _mockHttpMessageHandler
      .WhenCreateStreamMessageRequest()
      .Respond(
        HttpStatusCode.OK,
        "text/event-stream",
        new MemoryStream(Encoding.UTF8.GetBytes(eventString))
      );

    var request = new StreamMessageRequest(
      model: AnthropicModels.Claude35Sonnet,
      messages: [
        new(MessageRole.User, [new TextContent("Hello!")]),
      ]
    );

    var result = Client.CreateMessageAsync(request);
    var e = await result.FirstOrDefaultAsync();

    e.Should().BeEquivalentTo(anthropicEvent);
  }

  [Fact]
  public async Task CreateMessageAsync_WhenCalledAndMessageIsStreamed_ItShouldReturnAllExpectedEvents()
  {
    var eventStream = EventTestData.GetEventStream();
    var events = EventTestData.GetAllEvents();

    _mockHttpMessageHandler
      .WhenCreateStreamMessageRequest()
      .Respond(
        HttpStatusCode.OK,
        "text/event-stream",
        eventStream
      );

    var request = new StreamMessageRequest(
      model: AnthropicModels.Claude35Sonnet,
      messages: [
        new(MessageRole.User, [new TextContent("Hello!")]),
      ]
    );

    var result = Client.CreateMessageAsync(request);

    var actualEvents = await result.ToListAsync();

    actualEvents.Should().BeEquivalentTo(events);
  }

  [Fact]
  public async Task CreateMessageAsync_WhenCalledMessageIsStreamedAndToolProvide_ItShouldReturnToolCall()
  {
    var eventStream = EventTestData.GetEventStream();

    _mockHttpMessageHandler
      .WhenCreateStreamMessageRequest()
      .Respond(
        HttpStatusCode.OK,
        "text/event-stream",
        eventStream
      );

    var getWeather = (string location, string unit) => $"The weather in {location} is 72°{unit}";

    var request = new StreamMessageRequest(
      model: AnthropicModels.Claude35Sonnet,
      messages: [
        new(MessageRole.User, [new TextContent("Hello!")]),
      ],
      tools: [
        Tool.CreateFromFunction("get_weather", "Gets the weather for a location", getWeather)
      ]
    );

    var result = Client.CreateMessageAsync(request);
    var msgCompleteEvent = await result
      .Where(e => e.Type is EventType.MessageComplete)
      .FirstAsync();

    msgCompleteEvent.Data.Should().BeOfType<MessageCompleteEventData>();

    var toolCall = msgCompleteEvent.Data.As<MessageCompleteEventData>().Message.ToolCall;
    var toolCallResult = await toolCall!.InvokeAsync<string>();

    toolCallResult.IsSuccess.Should().BeTrue();
    toolCallResult.Value.Should().Be(getWeather("San Francisco, CA", "fahrenheit"));
  }

  [Fact]
  public async Task CreateMessageAsync_WhenCalledMessageIsStreamAndRequestFails_ItShouldReturnErrorEvent()
  {
    _mockHttpMessageHandler
      .WhenCreateStreamMessageRequest()
      .Respond(
        HttpStatusCode.BadRequest,
        "application/json",
        @"{
          ""type"": ""error"",
          ""error"": {
            ""type"": ""invalid_request_error"",
            ""message"": ""messages: roles must alternate between user and assistant, but found multiple user roles in a row""
          }
        }"
      );

    var request = new StreamMessageRequest(
      model: AnthropicModels.Claude35Sonnet,
      messages: [
        new(MessageRole.User, [new TextContent("Hello!")]),
        new(MessageRole.User, [new TextContent("Hello!")])
      ]
    );

    var result = Client.CreateMessageAsync(request);
    var events = await result.ToListAsync();

    events.Should().HaveCount(1);
    events[0].Type.Should().Be(EventType.Error);
    events[0].Data.Should().BeOfType<ErrorEventData>();
    events[0].Data.Should().BeEquivalentTo(new ErrorEventData(
      new InvalidRequestError(
        "messages: roles must alternate between user and assistant, but found multiple user roles in a row"
      )
    ));
  }

  [Fact]
  public async Task CreateMessageAsync_WhenCalledAndMessageCreatedWithDocumentContent_ItShouldReturnMessage()
  {
    _mockHttpMessageHandler
      .WhenCreateMessageRequest()
      .Respond(
        HttpStatusCode.OK,
        "application/json",
        @"{
            ""content"": [
              {
                ""text"": ""It is a PDF"",
                ""type"": ""text""
              }
            ],
            ""id"": ""msg_013Zva2CMHLNnXjNJJKqJ2EF"",
            ""model"": ""claude-3-5-sonnet-20240620"",
            ""role"": ""assistant"",
            ""stop_reason"": ""end_turn"",
            ""stop_sequence"": null,
            ""type"": ""message"",
            ""usage"": {
              ""input_tokens"": 10,
              ""output_tokens"": 25
            }
        }"
      );

    var request = new MessageRequest(
      model: AnthropicModels.Claude35Sonnet,
      messages: [
        new(MessageRole.User, [new TextContent("What is this?")]),
        new(MessageRole.User, [new DocumentContent("application/pdf", "data")])
      ]
    );

    var result = await Client.CreateMessageAsync(request);

    result.IsSuccess.Should().BeTrue();
    result.Value.Should().BeOfType<MessageResponse>();

    var message = result.Value;
    message.Id.Should().Be("msg_013Zva2CMHLNnXjNJJKqJ2EF");
    message.Model.Should().Be("claude-3-5-sonnet-20240620");
    message.Role.Should().Be("assistant");
    message.StopReason.Should().Be("end_turn");
    message.StopSequence.Should().BeNull();
    message.Type.Should().Be("message");
    message.Usage.InputTokens.Should().Be(10);
    message.Usage.OutputTokens.Should().Be(25);
    message.Content.Should().HaveCount(1);
    message.ToolCall.Should().BeNull();

    var textContent = message.Content[0];
    textContent.Should().BeOfType<TextContent>();
    textContent.As<TextContent>().Text.Should().Be("It is a PDF");
    textContent.As<TextContent>().Type.Should().Be("text");
  }

  [Fact]
  public async Task CountMessageTokensAsync_WhenCalled_ItShouldReturnCountTokensResponse()
  {
    _mockHttpMessageHandler
      .WhenCountMessageTokensRequest()
      .Respond(
        HttpStatusCode.OK,
        "application/json",
        @"{
          ""input_tokens"": 10
        }"
      );

    var request = new CountMessageTokensRequest(
      model: AnthropicModels.Claude35Sonnet,
      messages: [
        new(MessageRole.User, [new TextContent("Hello!")]),
      ]
    );

    var result = await Client.CountMessageTokensAsync(request);

    result.IsSuccess.Should().BeTrue();
    result.Value.Should().BeOfType<TokenCountResponse>();
    result.Value.InputTokens.Should().Be(10);
  }

  [Fact]
  public async Task CountMessageTokensAsync_WhenCalledAndErrorReturned_ItShouldHandleError()
  {
    _mockHttpMessageHandler
      .WhenCountMessageTokensRequest()
      .Respond(
        HttpStatusCode.BadRequest,
        "application/json",
        @"{
          ""type"": ""error"",
          ""error"": {
            ""type"": ""invalid_request_error"",
            ""message"": ""messages: roles must alternate between user and assistant, but found multiple user roles in a row""
          }
        }"
      );

    var request = new CountMessageTokensRequest(
      model: AnthropicModels.Claude35Sonnet,
      messages: [
        new(MessageRole.User, [new TextContent("Hello!")]),
        new(MessageRole.User, [new TextContent("Hello!")])
      ]
    );

    var result = await Client.CountMessageTokensAsync(request);

    result.IsSuccess.Should().BeFalse();
    result.Error.Should().BeOfType<AnthropicError>();
    result.Error.Error.Should().BeOfType<InvalidRequestError>();
  }

  [Fact]
  public async Task CountMessageTokensAsync_WhenCalledRequestFailsAndCanNotSerializeError_ItShouldReturnUnknownError()
  {
    _mockHttpMessageHandler
      .WhenCountMessageTokensRequest()
      .Respond(
        HttpStatusCode.BadRequest,
        "application/json",
        @"{}"
      );

    var request = new CountMessageTokensRequest(
      model: AnthropicModels.Claude35Sonnet,
      messages: [
        new(MessageRole.User, [new TextContent("Hello!")]),
        new(MessageRole.User, [new TextContent("Hello!")])
      ]
    );

    var result = await Client.CountMessageTokensAsync(request);

    result.IsSuccess.Should().BeFalse();
    result.Error.Should().BeOfType<AnthropicError>();
    result.Error.Error.Should().BeOfType<ApiError>();
  }
}