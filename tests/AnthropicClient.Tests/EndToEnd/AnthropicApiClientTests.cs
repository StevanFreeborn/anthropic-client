using AnthropicClient.Tests.Files;

namespace AnthropicClient.Tests.EndToEnd;

public class AnthropicApiClientTests(ConfigurationFixture configFixture) : EndToEndTest(configFixture)
{
  [Fact]
  public async Task CreateMessageAsync_WhenCalled_ItShouldReturnResponse()
  {
    var request = new MessageRequest(
      model: AnthropicModels.Claude3Haiku,
      messages: [new(MessageRole.User, [new TextContent("Hello!")])]
    );

    var result = await _client.CreateMessageAsync(request);

    result.IsSuccess.Should().BeTrue();
    result.Value.Should().BeOfType<MessageResponse>();
    result.Value.Content.Should().NotBeNullOrEmpty();
  }

  [Fact]
  public async Task CreateMessageAsync_WhenCalledWithStreamRequest_ItShouldReturnEvents()
  {
    var request = new StreamMessageRequest(
      model: AnthropicModels.Claude3Haiku,
      messages: [new(MessageRole.User, [new TextContent("Hello!")])]
    );

    var response = _client.CreateMessageAsync(request);

    var events = new List<AnthropicEvent>();

    await foreach (var e in response)
    {
      events.Add(e);
    }

    events.Should().NotBeEmpty();
  }

  [Fact]
  public async Task CreateMessageAsync_WhenCalledWithStreamRequest_ItShouldYieldAMessageCompleteEvent()
  {
    var request = new StreamMessageRequest(
      model: AnthropicModels.Claude3Haiku,
      messages: [new(MessageRole.User, [new TextContent("Hello!")])]
    );

    var response = _client.CreateMessageAsync(request);

    await foreach (var e in response)
    {
      if (e.Data is MessageCompleteEventData messageCompleteData)
      {
        messageCompleteData.Message.Should().NotBeNull();
        break;
      }
    }
  }

  [Fact]
  public async Task CreateMessageAsync_WhenImageIsSent_ItShouldReturnResponse()
  {
    var imagePath = TestFileHelper.GetTestFilePath("elephant.jpg");
    var mediaType = "image/jpeg";
    var bytes = await File.ReadAllBytesAsync(imagePath);
    var base64Data = Convert.ToBase64String(bytes);

    var request = new MessageRequest(
      model: AnthropicModels.Claude3Haiku,
      messages: [
        new(MessageRole.User, [
          new ImageContent(mediaType, base64Data),
          new TextContent("What is in this image?")
        ]),
      ]
    );

    var result = await _client.CreateMessageAsync(request);

    result.IsSuccess.Should().BeTrue();
    result.Value.Should().BeOfType<MessageResponse>();
    result.Value.Content.Should().NotBeNullOrEmpty();

    var text = result.Value.Content.Aggregate("", static (acc, content) =>
    {
      if (content is TextContent textContent)
      {
        acc += textContent.Text;
      }

      return acc;
    });

    text.Should().Contain("elephant");
  }

  [Fact]
  public async Task CreateMessageAsync_WhenSystemMessagesContainCacheControl_ItShouldUseCache()
  {
    var client = CreateClient(new HttpClient());

    var storyPath = TestFileHelper.GetTestFilePath("story.txt");
    var storyText = await File.ReadAllTextAsync(storyPath);

    var request = new MessageRequest(
      model: AnthropicModels.Claude3Haiku,
      systemMessages: [
        new("You are a helpful assistant who can answer questions about the following text:"),
        new(storyText, new EphemeralCacheControl())
      ],
      messages: [
        new(MessageRole.User, [
          new TextContent("Give me a one sentence summary of this story.")
        ]),
      ]
    );

    var resultOne = await _client.CreateMessageAsync(request);

    resultOne.IsSuccess.Should().BeTrue();
    resultOne.Value.Should().BeOfType<MessageResponse>();
    resultOne.Value.Content.Should().NotBeNullOrEmpty();
    resultOne.Value.Usage.Should().Match<Usage>(static u => u.CacheCreationInputTokens > 0 || u.CacheReadInputTokens > 0);

    request.Messages.Add(new(MessageRole.Assistant, resultOne.Value.Content));
    request.Messages.Add(new(MessageRole.User, [new TextContent("What is the main theme of this story?")]));

    var resultTwo = await client.CreateMessageAsync(request);

    resultTwo.IsSuccess.Should().BeTrue();
    resultTwo.Value.Should().BeOfType<MessageResponse>();
    resultTwo.Value.Content.Should().NotBeNullOrEmpty();
    resultTwo.Value.Usage.CacheReadInputTokens.Should().BeGreaterThan(0);
  }

  [Fact]
  public async Task CreateMessageAsync_WhenMessagesContainCacheControl_ItShouldUseCache()
  {
    var client = CreateClient(new HttpClient());

    var storyPath = TestFileHelper.GetTestFilePath("story.txt");
    var storyText = await File.ReadAllTextAsync(storyPath);

    var request = new MessageRequest(
      model: AnthropicModels.Claude3Haiku,
      messages: [
        new(MessageRole.User, [
          new TextContent("Give me a one sentence summary of this story."),
          new TextContent(storyText, new EphemeralCacheControl())
        ]),
      ]
    );

    var resultOne = await client.CreateMessageAsync(request);

    resultOne.IsSuccess.Should().BeTrue();
    resultOne.Value.Should().BeOfType<MessageResponse>();
    resultOne.Value.Content.Should().NotBeNullOrEmpty();
    resultOne.Value.Usage.Should().Match<Usage>(static u => u.CacheCreationInputTokens > 0 || u.CacheReadInputTokens > 0);

    request.Messages.Add(new(MessageRole.Assistant, resultOne.Value.Content));
    request.Messages.Add(new(MessageRole.User, [new TextContent("What is the main theme of this story?")]));

    var resultTwo = await client.CreateMessageAsync(request);

    resultTwo.IsSuccess.Should().BeTrue();
    resultTwo.Value.Should().BeOfType<MessageResponse>();
    resultTwo.Value.Content.Should().NotBeNullOrEmpty();
    resultTwo.Value.Usage.CacheReadInputTokens.Should().BeGreaterThan(0);
  }

  [Fact]
  public async Task CreateMessageAsync_WhenToolsContainCacheControl_ItShouldUseCache()
  {
    var client = CreateClient(new HttpClient());

    var func = (string ticker) => ticker;

    var tools = Enumerable
      .Range(0, 50)
      .Select(i => Tool.CreateFromFunction($"tool-{i}", $"Tool {i}", func))
      .ToList();

    tools.Last().CacheControl = new EphemeralCacheControl();

    var request = new MessageRequest(
      model: AnthropicModels.Claude3Haiku,
      messages: [
        new(MessageRole.User, [
          new TextContent("Hi could you tell me your name?"),
        ]),
      ],
      tools: tools
    );

    var resultOne = await client.CreateMessageAsync(request);

    resultOne.IsSuccess.Should().BeTrue();
    resultOne.Value.Should().BeOfType<MessageResponse>();
    resultOne.Value.Content.Should().NotBeNullOrEmpty();
    resultOne.Value.Usage.Should().Match<Usage>(u => u.CacheCreationInputTokens > 0 || u.CacheReadInputTokens > 0);

    request.Messages.Add(new(MessageRole.Assistant, resultOne.Value.Content));
    request.Messages.Add(new(MessageRole.User, [new TextContent("Could you tell me the stock price for AAPL?")]));

    var resultTwo = await client.CreateMessageAsync(request);

    resultTwo.IsSuccess.Should().BeTrue();
    resultTwo.Value.Should().BeOfType<MessageResponse>();
    resultTwo.Value.Content.Should().NotBeNullOrEmpty();
    resultTwo.Value.Usage.CacheReadInputTokens.Should().BeGreaterThan(0);
  }

  [Fact]
  public async Task CreateMessageAsync_WhenProvidedWithPDF_ItShouldReturnResponse()
  {
    var pdfPath = TestFileHelper.GetTestFilePath("addendum.pdf");
    var bytes = await File.ReadAllBytesAsync(pdfPath);
    var base64Data = Convert.ToBase64String(bytes);

    var request = new MessageRequest(
      model: AnthropicModels.Claude35Sonnet,
      messages: [
        new(MessageRole.User, [new TextContent("What is the title of this paper?")]),
        new(MessageRole.User, [new DocumentContent("application/pdf", base64Data)])
      ]
    );

    var client = CreateClient(new HttpClient());

    var result = await client.CreateMessageAsync(request);

    result.IsSuccess.Should().BeTrue();
    result.Value.Should().BeOfType<MessageResponse>();
    result.Value.Content.Should().NotBeNullOrEmpty();

    var text = result.Value.Content.Aggregate("", static (acc, content) =>
    {
      if (content is TextContent textContent)
      {
        acc += textContent.Text;
      }

      return acc;
    });

    text.Should().Contain("Model Card Addendum: Claude 3.5 Haiku and Upgraded Claude 3.5 Sonnet");
  }

  [Fact]
  public async Task CreateMessageAsync_WhenProvidedWithPDFWithCacheControl_ItShouldUseCache()
  {
    var pdfPath = TestFileHelper.GetTestFilePath("addendum.pdf");
    var bytes = await File.ReadAllBytesAsync(pdfPath);
    var base64Data = Convert.ToBase64String(bytes);

    var client = CreateClient(new HttpClient());

    var request = new MessageRequest(
      model: AnthropicModels.Claude35Sonnet,
      messages: [
        new(MessageRole.User, [
          new DocumentContent("application/pdf", base64Data, new EphemeralCacheControl()),
          new TextContent("What is the title of this paper?")
        ]),
      ]
    );

    var resultOne = await client.CreateMessageAsync(request);

    resultOne.IsSuccess.Should().BeTrue();
    resultOne.Value.Should().BeOfType<MessageResponse>();
    resultOne.Value.Content.Should().NotBeNullOrEmpty();
    resultOne.Value.Usage.Should().Match<Usage>(static u => u.CacheCreationInputTokens > 0 || u.CacheReadInputTokens > 0);

    request.Messages.Add(new(MessageRole.Assistant, resultOne.Value.Content));
    request.Messages.Add(new(MessageRole.User, [new TextContent("What is the main theme of this paper?")]));

    var resultTwo = await client.CreateMessageAsync(request);

    resultTwo.IsSuccess.Should().BeTrue();
    resultTwo.Value.Should().BeOfType<MessageResponse>();
    resultTwo.Value.Content.Should().NotBeNullOrEmpty();
    resultTwo.Value.Usage.CacheReadInputTokens.Should().BeGreaterThan(0);
  }

  [Fact]
  public async Task CreateMessageAsync_WhenCitationsAreEnabledForTextDocumentSource_ItShouldReturnCitationsInResponse()
  {
    var request = new MessageRequest(
      model: AnthropicModels.Claude35HaikuLatest,
      messages: [
        new(
          MessageRole.User,
          [
            new DocumentContent(
              new TextSource("The grass is green. The sky is blue.")
            )
            {
              Title = "My Document",
              Context = "This is a trustworthy document.",
              Citations = new() { Enabled = true }
            },
            new TextContent("What color is the grass and sky?"),
          ]
        )
      ]
    );

    var result = await _client.CreateMessageAsync(request);

    result.IsSuccess.Should().BeTrue();

    var citations = result.Value
      .Content
      .OfType<TextContent>()
      .SelectMany(static c => c.Citations is null ? [] : c.Citations);

    citations.OfType<CharacterLocationCitation>().Should().NotBeEmpty();
  }

  [Fact]
  public async Task CreateMessageAsync_WhenCitationsAreEnabledForPDFDocumentSource_ItShouldReturnCitationsInResponse()
  {
    var pdfPath = TestFileHelper.GetTestFilePath("addendum.pdf");
    var bytes = await File.ReadAllBytesAsync(pdfPath);
    var base64Data = Convert.ToBase64String(bytes);

    var request = new MessageRequest(
      model: AnthropicModels.Claude35HaikuLatest,
      messages: [
        new(
          MessageRole.User,
          [
            new DocumentContent("application/pdf", base64Data)
            {
              Title = "My PDF Document",
              Context = "This is a trustworthy document.",
              Citations = new() { Enabled = true }
            },
            new TextContent("What is the title of this paper?"),
          ]
        )
      ]
    );

    var result = await _client.CreateMessageAsync(request);

    result.IsSuccess.Should().BeTrue();

    var citations = result.Value
      .Content
      .OfType<TextContent>()
      .SelectMany(static c => c.Citations is null ? [] : c.Citations);

    citations.OfType<PageLocationCitation>().Should().NotBeEmpty();
  }

  [Fact]
  public async Task CreateMessageAsync_WhenCitationsAreEnabledForCustomDocumentSource_ItShouldReturnCitationsInResponse()
  {
    var request = new MessageRequest(
      model: AnthropicModels.Claude35HaikuLatest,
      messages: [
        new(
          MessageRole.User,
          [
            new DocumentContent(
              new CustomSource([
                new TextContent("The grass is green. The sky is blue.")
              ])
            )
            {
              Title = "My Custom Document",
              Context = "This is a trustworthy document.",
              Citations = new() { Enabled = true }
            },
            new TextContent("What color is the grass and sky?"),
          ]
        )
      ]
    );

    var result = await _client.CreateMessageAsync(request);

    result.IsSuccess.Should().BeTrue();

    var citations = result.Value
      .Content
      .OfType<TextContent>()
      .SelectMany(static c => c.Citations is null ? [] : c.Citations);

    citations.OfType<ContentBlockLocationCitation>().Should().NotBeEmpty();
  }

  [Fact]
  public async Task CreateMessageAsync_WhenStreamingAndCitationsAreEnabledForTextDocumentSource_ItShouldReturnCitationsInResponse()
  {
    var request = new StreamMessageRequest(
      model: AnthropicModels.Claude35HaikuLatest,
      messages: [
        new(
          MessageRole.User,
          [
            new DocumentContent(
              new TextSource("The grass is green. The sky is blue.")
            )
            {
              Title = "My Document",
              Context = "This is a trustworthy document.",
              Citations = new() { Enabled = true }
            },
            new TextContent("What color is the grass and sky?"),
          ]
        )
      ]
    );

    var result = _client.CreateMessageAsync(request);

    var messageCompleteEvent = await result
      .Where(e => e.Type is EventType.MessageComplete)
      .FirstAsync();

    var citations = messageCompleteEvent.Data
      .As<MessageCompleteEventData>()
      .Message
      .Content
      .OfType<TextContent>()
      .SelectMany(static c => c.Citations is null ? [] : c.Citations);

    citations.OfType<CharacterLocationCitation>().Should().NotBeEmpty();
  }

  [Fact]
  public async Task CreateMessageAsync_WhenStreamingAndCitationsAreEnabledForPDFDocumentSource_ItShouldReturnCitationsInResponse()
  {
    var pdfPath = TestFileHelper.GetTestFilePath("addendum.pdf");
    var bytes = await File.ReadAllBytesAsync(pdfPath);
    var base64Data = Convert.ToBase64String(bytes);

    var request = new StreamMessageRequest(
      model: AnthropicModels.Claude35HaikuLatest,
      messages: [
        new(
          MessageRole.User,
          [
            new DocumentContent("application/pdf", base64Data)
            {
              Title = "My PDF Document",
              Context = "This is a trustworthy document.",
              Citations = new() { Enabled = true }
            },
            new TextContent("What is the title of this paper?"),
          ]
        )
      ]
    );

    var result = _client.CreateMessageAsync(request);

    var messageCompleteEvent = await result
      .Where(e => e.Type is EventType.MessageComplete)
      .FirstAsync();

    var citations = messageCompleteEvent.Data
      .As<MessageCompleteEventData>()
      .Message
      .Content
      .OfType<TextContent>()
      .SelectMany(static c => c.Citations is null ? [] : c.Citations);

    citations.OfType<PageLocationCitation>().Should().NotBeEmpty();
  }

  [Fact]
  public async Task CreateMessageAsync_WhenStreamingAndCitationsAreEnabledForCustomDocumentSource_ItShouldReturnCitationsInResponse()
  {
    var request = new StreamMessageRequest(
      model: AnthropicModels.Claude35HaikuLatest,
      messages: [
        new(
          MessageRole.User,
          [
            new DocumentContent(
              new CustomSource([
                new TextContent("The grass is green. The sky is blue.")
              ])
            )
            {
              Title = "My Custom Document",
              Context = "This is a trustworthy document.",
              Citations = new() { Enabled = true }
            },
            new TextContent("What color is the grass and sky?"),
          ]
        )
      ]
    );

    var result = _client.CreateMessageAsync(request);

    var messageCompleteEvent = await result
      .Where(e => e.Type is EventType.MessageComplete)
      .FirstAsync();

    var citations = messageCompleteEvent.Data
      .As<MessageCompleteEventData>()
      .Message
      .Content
      .OfType<TextContent>()
      .SelectMany(static c => c.Citations is null ? [] : c.Citations);

    citations.OfType<ContentBlockLocationCitation>().Should().NotBeEmpty();
  }

  [Fact]
  public async Task CountMessageTokensAsync_WhenCalled_ItShouldReturnResponse()
  {
    var request = new CountMessageTokensRequest(
      model: AnthropicModels.Claude3Haiku,
      messages: [
        new(MessageRole.User, [new TextContent("Hello!")])
      ]
    );

    var result = await _client.CountMessageTokensAsync(request);

    result.IsSuccess.Should().BeTrue();
    result.Value.Should().BeOfType<TokenCountResponse>();
    result.Value.InputTokens.Should().BeGreaterThan(0);
  }

  [Fact]
  public async Task ListModelsAsync_WhenCalled_ItShouldReturnResponse()
  {
    var result = await _client.ListModelsAsync();

    result.IsSuccess.Should().BeTrue();
    result.Value.Should().BeOfType<Page<AnthropicModel>>();
    result.Value.Data.Should().HaveCountGreaterThan(0);
  }

  [Fact]
  public async Task ListModelsAsync_WhenCalledWithPagination_ItShouldReturnResponse()
  {
    var result = await _client.ListModelsAsync(new PagingRequest(limit: 1));

    result.IsSuccess.Should().BeTrue();
    result.Value.Should().BeOfType<Page<AnthropicModel>>();
    result.Value.Data.Should().HaveCount(1);
  }

  [Fact]
  public async Task ListAllModelsAsync_WhenCalled_ItShouldReturnResponse()
  {
    var responses = await _client.ListAllModelsAsync(limit: 1).ToListAsync();

    responses.Should().HaveCountGreaterThan(0);
  }

  [Fact]
  public async Task GetModelAsync_WhenCalled_ItShouldReturnResponse()
  {
    var result = await _client.GetModelAsync(AnthropicModels.Claude3Haiku);

    result.IsSuccess.Should().BeTrue();
    result.Value.Should().BeOfType<AnthropicModel>();
    result.Value.Id.Should().Be(AnthropicModels.Claude3Haiku);
  }

  [Fact]
  public async Task CreateMessageBatchAsync_WhenCalled_ItShouldReturnResponse()
  {
    var request = new MessageBatchRequest([
      new(
        Guid.NewGuid().ToString(),
        new(
          model: AnthropicModels.Claude3Haiku,
          messages: [new(MessageRole.User, [new TextContent("Hello!")])]
        )
      ),
    ]);

    var result = await _client.CreateMessageBatchAsync(request);

    result.IsSuccess.Should().BeTrue();
    result.Value.Should().BeOfType<MessageBatchResponse>();
    result.Value.Id.Should().NotBeNullOrEmpty();
  }

  [Fact]
  public async Task GetMessageBatchAsync_WhenCalled_ItShouldReturnResponse()
  {
    var request = new MessageBatchRequest([
      new(
        Guid.NewGuid().ToString(),
        new(
          model: AnthropicModels.Claude3Haiku,
          messages: [new(MessageRole.User, [new TextContent("Hello!")])]
        )
      ),
    ]);

    var createResult = await _client.CreateMessageBatchAsync(request);
    var getResult = await _client.GetMessageBatchAsync(createResult.Value.Id);

    getResult.IsSuccess.Should().BeTrue();
    getResult.Value.Should().BeOfType<MessageBatchResponse>();
    getResult.Value.Id.Should().Be(createResult.Value.Id);
  }

  [Fact]
  public async Task ListMessageBatchesAsync_WhenCalled_ItShouldReturnResponse()
  {
    var request = new MessageBatchRequest([
      new(
        Guid.NewGuid().ToString(),
        new(
          model: AnthropicModels.Claude3Haiku,
          messages: [new(MessageRole.User, [new TextContent("Hello!")])]
        )
      ),
    ]);

    var createResult = await _client.CreateMessageBatchAsync(request);
    var result = await _client.ListMessageBatchesAsync();

    result.IsSuccess.Should().BeTrue();
    result.Value.Should().BeOfType<Page<MessageBatchResponse>>();
    result.Value.Data.Should().HaveCountGreaterThan(0);
    result.Value.Data.Should().ContainSingle(b => b.Id == createResult.Value.Id);
  }

  [Fact]
  public async Task ListAllMessageBatchesAsync_WhenCalled_ItShouldReturnResponse()
  {
    var createRequest = (string id) => new MessageBatchRequest([
      new(
        id,
        new(
          model: AnthropicModels.Claude3Haiku,
          messages: [new(MessageRole.User, [new TextContent("Hello!")])]
        )
      ),
    ]);

    var requestNumberOne = createRequest(Guid.NewGuid().ToString());
    var requestNumberTwo = createRequest(Guid.NewGuid().ToString());

    var createResultOne = await _client.CreateMessageBatchAsync(requestNumberOne);
    var createResultTwo = await _client.CreateMessageBatchAsync(requestNumberTwo);

    var responses = await _client.ListAllMessageBatchesAsync(limit: 1).ToListAsync();

    responses.Should().HaveCountGreaterThan(2);

    var batches = responses
      .Where(r => r.IsSuccess)
      .Select(r => r.Value)
      .SelectMany(r => r.Data);

    batches.Should().ContainSingle(b => b.Id == createResultOne.Value.Id);
    batches.Should().ContainSingle(b => b.Id == createResultTwo.Value.Id);
  }

  [Fact]
  public async Task CancelMessageBatchAsync_WhenCalled_ItShouldReturnResponse()
  {
    var request = new MessageBatchRequest([
      new(
        Guid.NewGuid().ToString(),
        new(
          model: AnthropicModels.Claude3Haiku,
          messages: [new(MessageRole.User, [new TextContent("Hello!")])]
        )
      ),
    ]);

    var createResult = await _client.CreateMessageBatchAsync(request);
    var result = await _client.CancelMessageBatchAsync(createResult.Value.Id);

    result.IsSuccess.Should().BeTrue();
    result.Value.Should().BeOfType<MessageBatchResponse>();
    result.Value.Id.Should().Be(createResult.Value.Id);
    result.Value.ProcessingStatus.Should().Be(MessageBatchStatus.Canceling);
  }

  [Fact]
  public async Task CreateFileAsync_WhenCalled_ItShouldReturnResponse()
  {
    var fileName = "story.txt";
    var fileType = "text/plain";
    var filePath = TestFileHelper.GetTestFilePath("story.txt");
    var fileContent = await File.ReadAllBytesAsync(filePath);
    var request = new CreateFileRequest(fileContent, fileName, fileType);

    var httpClient = new HttpClient();
    httpClient.DefaultRequestHeaders.Add("anthropic-beta", "files-api-2025-04-14");
    var client = CreateClient(httpClient);

    var result = await client.CreateFileAsync(request);

    result.IsSuccess.Should().BeTrue();
    result.Value.Should().BeOfType<AnthropicFile>();
    result.Value.Name.Should().Be(fileName);
    result.Value.MimeType.Should().Be(fileType);
  }

  [Fact]
  public async Task ListFilesAsync_WhenCalled_ItShouldReturnPageOfFiles()
  {
    var httpClient = new HttpClient();
    httpClient.DefaultRequestHeaders.Add("anthropic-beta", "files-api-2025-04-14");
    var client = CreateClient(httpClient);

    var fileBytes = await File.ReadAllBytesAsync(TestFileHelper.GetTestFilePath("story.txt"));
    var createFileRequest = new CreateFileRequest(fileBytes, "story.txt", "text/plain");
    var createdFile = await client.CreateFileAsync(createFileRequest);

    var result = await client.ListFilesAsync();

    result.IsSuccess.Should().BeTrue();
    result.Value.Should().BeOfType<Page<AnthropicFile>>();
    result.Value.Data.Should().ContainSingle(f => f.Id == createdFile.Value.Id);
  }

  [Fact]
  public async Task ListAllFilesAsync_WhenCalled_ItShouldReturnAllFiles()
  {
    var httpClient = new HttpClient();
    httpClient.DefaultRequestHeaders.Add("anthropic-beta", "files-api-2025-04-14");
    var client = CreateClient(httpClient);

    var fileBytes = await File.ReadAllBytesAsync(TestFileHelper.GetTestFilePath("story.txt"));
    var createFileRequest = new CreateFileRequest(fileBytes, "story.txt", "text/plain");
    var createdFile = await client.CreateFileAsync(createFileRequest);

    var responses = await client.ListAllFilesAsync(limit: 1).ToListAsync();

    responses.Should().HaveCountGreaterThan(0);
    responses.Select(r => r.Value).SelectMany(p => p.Data)
      .Should().ContainSingle(f => f.Id == createdFile.Value.Id);
  }

  [Fact]
  public async Task GetFileInfoAsync_WhenCalled_ItShouldReturnFile()
  {
    var httpClient = new HttpClient();
    httpClient.DefaultRequestHeaders.Add("anthropic-beta", "files-api-2025-04-14");
    var client = CreateClient(httpClient);

    var fileBytes = await File.ReadAllBytesAsync(TestFileHelper.GetTestFilePath("story.txt"));
    var createFileRequest = new CreateFileRequest(fileBytes, "story.txt", "text/plain");
    var createdFile = await client.CreateFileAsync(createFileRequest);

    var result = await client.GetFileInfoAsync(createdFile.Value.Id);

    result.IsSuccess.Should().BeTrue();
    result.Value.Should().BeOfType<AnthropicFile>();
    result.Value.Id.Should().Be(createdFile.Value.Id);
  }

  [Fact]
  public async Task DeleteFileAsync_WhenCalled_ItShouldReturnDeleteResponse()
  {
    var httpClient = new HttpClient();
    httpClient.DefaultRequestHeaders.Add("anthropic-beta", "files-api-2025-04-14");
    var client = CreateClient(httpClient);

    var fileBytes = await File.ReadAllBytesAsync(TestFileHelper.GetTestFilePath("story.txt"));
    var createFileRequest = new CreateFileRequest(fileBytes, "story.txt", "text/plain");
    var createdFile = await client.CreateFileAsync(createFileRequest);

    var result = await client.DeleteFileAsync(createdFile.Value.Id);

    result.IsSuccess.Should().BeTrue();
    result.Value.Should().BeOfType<AnthropicFileDeleteResponse>();
    result.Value.Id.Should().Be(createdFile.Value.Id);
  }
}