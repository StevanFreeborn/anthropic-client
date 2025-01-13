using AnthropicClient.Tests.Unit;

namespace AnthropicClient.Tests.Integration;

public class IntegrationTest : SerializationTest
{
  protected readonly MockHttpMessageHandler _mockHttpMessageHandler = new();
  protected AnthropicApiClient Client => CreateClient();

  private AnthropicApiClient CreateClient()
  {
    return new AnthropicApiClient("test-key", _mockHttpMessageHandler.ToHttpClient());
  }
}

public static class MockHttpMessageHandlerExtensions
{
  private const string BaseUrl = "https://api.anthropic.com/v1";
  private static readonly string MessagesEndpoint = $"{BaseUrl}/messages";
  private static readonly string CountTokensEndpoint = $"{BaseUrl}/messages/count_tokens";
  private static readonly string MessageBatchesEndpoint = $"{BaseUrl}/messages/batches";
  private static readonly string ModelsEndpoint = $"{BaseUrl}/models";

  private static MockedRequest SetupBaseRequest(
    this MockHttpMessageHandler mockHttpMessageHandler,
    HttpMethod method,
    string url
  )
  {
    return mockHttpMessageHandler
      .When(method, url)
      .WithHeaders(new Dictionary<string, string>
      {
        { "anthropic-version", "2023-06-01" },
        { "x-api-key", "test-key" }
      });
  }

  public static MockedRequest WhenCreateMessageRequest(this MockHttpMessageHandler mockHttpMessageHandler)
  {
    return mockHttpMessageHandler
      .SetupBaseRequest(HttpMethod.Post, MessagesEndpoint)
      .WithJsonContent<MessageRequest>(r => r.Stream == false, JsonSerializationOptions.DefaultOptions);
  }

  public static MockedRequest WhenCreateStreamMessageRequest(this MockHttpMessageHandler mockHttpMessageHandler)
  {
    return mockHttpMessageHandler
      .SetupBaseRequest(HttpMethod.Post, MessagesEndpoint)
      .WithJsonContent<StreamMessageRequest>(r => r.Stream == true, JsonSerializationOptions.DefaultOptions);
  }

  public static MockedRequest WhenCountMessageTokensRequest(this MockHttpMessageHandler mockHttpMessageHandler)
  {
    return mockHttpMessageHandler
      .SetupBaseRequest(HttpMethod.Post, CountTokensEndpoint);
  }

  public static MockedRequest WhenListModelsRequest(this MockHttpMessageHandler mockHttpMessageHandler)
  {
    return mockHttpMessageHandler
      .SetupBaseRequest(HttpMethod.Get, ModelsEndpoint);
  }

  public static MockedRequest WhenGetModelRequest(this MockHttpMessageHandler mockHttpMessageHandler, string modelId)
  {
    return mockHttpMessageHandler
      .SetupBaseRequest(HttpMethod.Get, $"{ModelsEndpoint}/{modelId}");
  }

  public static MockedRequest WhenCreateMessageBatchRequest(this MockHttpMessageHandler mockHttpMessageHandler)
  {
    return mockHttpMessageHandler
      .SetupBaseRequest(HttpMethod.Post, MessageBatchesEndpoint);
  }

  public static MockedRequest WhenGetMessageBatchRequest(this MockHttpMessageHandler mockHttpMessageHandler, string batchId)
  {
    return mockHttpMessageHandler
      .SetupBaseRequest(HttpMethod.Get, $"{MessageBatchesEndpoint}/{batchId}");
  }

  public static MockedRequest WhenGetMessageBatchResultsRequest(this MockHttpMessageHandler mockHttpMessageHandler, string batchId)
  {
    return mockHttpMessageHandler
      .SetupBaseRequest(HttpMethod.Get, $"{MessageBatchesEndpoint}/{batchId}/results");
  }

  public static MockedRequest WhenListMessageBatchesRequest(this MockHttpMessageHandler mockHttpMessageHandler)
  {
    return mockHttpMessageHandler
      .SetupBaseRequest(HttpMethod.Get, MessageBatchesEndpoint);
  }

  public static MockedRequest WhenCancelMessageBatchRequest(this MockHttpMessageHandler mockHttpMessageHandler, string batchId)
  {
    return mockHttpMessageHandler
      .SetupBaseRequest(HttpMethod.Post, $"{MessageBatchesEndpoint}/{batchId}/cancel");
  }
}