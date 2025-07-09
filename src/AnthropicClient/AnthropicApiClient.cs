using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;

using AnthropicClient.Json;
using AnthropicClient.Models;
using AnthropicClient.Utils;

namespace AnthropicClient;

/// <inheritdoc cref="IAnthropicApiClient"/>
public class AnthropicApiClient : IAnthropicApiClient
{
  private const string BaseUrl = "https://api.anthropic.com/v1/";
  private const string ApiKeyHeader = "x-api-key";
  private const string MessagesEndpoint = "messages";
  private string CountTokensEndpoint => $"{MessagesEndpoint}/count_tokens";
  private string MessageBatchesEndpoint => $"{MessagesEndpoint}/batches";
  private const string ModelsEndpoint = "models";
  private const string JsonContentType = "application/json";
  private const string EventPrefix = "event:";
  private const string DataPrefix = "data:";
  private readonly Dictionary<string, string> _defaultHeaders = new()
  {
    { "anthropic-version", "2023-06-01" },
  };
  private readonly HttpClient _httpClient;

  /// <summary>
  /// Initializes a new instance of the <see cref="AnthropicApiClient"/> class.
  /// </summary>
  /// <param name="apiKey">The API key to use for the client.</param>
  /// <param name="httpClient">The HTTP client to use for the client.</param>
  /// <exception cref="ArgumentNullException">Thrown when the API key or HTTP client is null.</exception>
  /// <returns>A new instance of the <see cref="AnthropicApiClient"/> class.</returns>
  public AnthropicApiClient(string apiKey, HttpClient httpClient)
  {
    ArgumentValidator.ThrowIfNull(apiKey, nameof(apiKey));
    ArgumentValidator.ThrowIfNull(httpClient, nameof(httpClient));

    _httpClient = httpClient;
    _httpClient.BaseAddress = new Uri(BaseUrl);
    _httpClient.DefaultRequestHeaders.Add(ApiKeyHeader, apiKey);
    _httpClient.DefaultRequestHeaders
      .Accept
      .Add(new MediaTypeWithQualityHeaderValue(JsonContentType));

    foreach (var pair in _defaultHeaders)
    {
      _httpClient.DefaultRequestHeaders.Add(pair.Key, pair.Value);
    }
  }

  /// <inheritdoc/>
  public async Task<AnthropicResult<MessageResponse>> CreateMessageAsync(MessageRequest request, CancellationToken cancellationToken = default)
  {
    var response = await SendRequestAsync(MessagesEndpoint, request, cancellationToken);
    var anthropicHeaders = new AnthropicHeaders(response.Headers);
    var responseContent = await response.Content.ReadAsStringAsync();

    if (response.IsSuccessStatusCode is false)
    {
      var error = Deserialize<AnthropicError>(responseContent) ?? new AnthropicError();
      return AnthropicResult<MessageResponse>.Failure(error, anthropicHeaders);
    }

    var msgResponse = Deserialize<MessageResponse>(responseContent) ?? new MessageResponse();

    if (request.Tools is not null && request.Tools.Count > 0)
    {
      msgResponse.ToolCall = GetToolCall(msgResponse, request.Tools);
    }

    return AnthropicResult<MessageResponse>.Success(msgResponse, anthropicHeaders);
  }

  /// <inheritdoc/>
  public async IAsyncEnumerable<AnthropicEvent> CreateMessageAsync(StreamMessageRequest request, [EnumeratorCancellation] CancellationToken cancellationToken = default)
  {
    var response = await SendRequestAsync(MessagesEndpoint, request, cancellationToken);

    if (response.IsSuccessStatusCode is false)
    {
      var errorContent = await response.Content.ReadAsStringAsync();
      var error = Deserialize<AnthropicError>(errorContent) ?? new AnthropicError();
      yield return new AnthropicEvent(EventType.Error, new ErrorEventData(error.Error));
      yield break;
    }

    var anthropicHeaders = new AnthropicHeaders(response.Headers);

    using var responseContent = await response.Content.ReadAsStreamAsync();
    using var streamReader = new StreamReader(responseContent);

    MessageResponse? msgResponse = null;
    Content? content = null;
    var toolInputJsonStringBuilder = new StringBuilder();
    var currentEvent = new AnthropicEvent();

    do
    {
      var line = await streamReader.ReadLineAsync();

      // I know...this is not pretty, but here is why...
      // as events are being yielded I want to also
      // build up the complete response
      // so I can yield it as a special event to make tool
      // calling easier to handle

      // initialize response on message start
      if (currentEvent.Type is EventType.MessageStart && currentEvent.Data is MessageStartEventData msgStartData)
      {
        msgResponse = msgStartData.Message;
      }

      // initialize content block on content block start
      if (currentEvent.Type is EventType.ContentBlockStart && currentEvent.Data is ContentStartEventData contentStartData)
      {
        content = contentStartData.ContentBlock;
      }

      // update content block with deltas based on
      // current content type and delta type
      if (currentEvent.Type is EventType.ContentBlockDelta && currentEvent.Data is ContentDeltaEventData contentDeltaData)
      {
        if (content is TextContent textContent)
        {
          if (contentDeltaData.Delta is TextDelta textDelta)
          {
            var newText = textContent.Text + textDelta.Text;

            content = new TextContent(newText)
            {
              Citations = textContent.Citations,
            };
          }

          if (contentDeltaData.Delta is CitationDelta citationDelta)
          {
            var citations = new List<Citation>()
            {
              citationDelta.Citation,
            };

            if (textContent.Citations is not null)
            {
              citations.AddRange(textContent.Citations);
            }

            var newContent = new TextContent(textContent.Text)
            {
              Citations = [.. citations],
            };

            content = newContent;
          }
        }

        if (content is ToolUseContent toolUseContent && contentDeltaData.Delta is JsonDelta jsonDelta)
        {
          toolInputJsonStringBuilder.Append(jsonDelta.PartialJson);
        }
      }

      // finalize content block on content block stop
      // and add it to the response
      if (currentEvent.Type is EventType.ContentBlockStop)
      {
        if (content is not null && msgResponse is not null)
        {
          if (content is TextContent textContent)
          {
            msgResponse.Content.Add(textContent);
          }

          if (content is ToolUseContent toolUseContent)
          {
            var input = Deserialize<Dictionary<string, object?>>(toolInputJsonStringBuilder.ToString());
            var newToolUseContent = new ToolUseContent()
            {
              Id = toolUseContent.Id,
              Name = toolUseContent.Name,
              Input = input!,
            };

            msgResponse.Content.Add(newToolUseContent);
          }

          content = null;
        }
      }

      // update response with message delta data
      if (
        currentEvent.Type is EventType.MessageDelta &&
        currentEvent.Data is MessageDeltaEventData msgDeltaData &&
        msgResponse is not null
      )
      {
        var existingUsage = msgResponse.Usage;
        var newUsage = new Usage()
        {
          InputTokens = existingUsage.InputTokens + msgDeltaData.Usage.InputTokens,
          OutputTokens = existingUsage.OutputTokens + msgDeltaData.Usage.OutputTokens,
          CacheCreationInputTokens = existingUsage.CacheCreationInputTokens + msgDeltaData.Usage.CacheCreationInputTokens,
          CacheReadInputTokens = existingUsage.CacheReadInputTokens + msgDeltaData.Usage.CacheReadInputTokens,
        };

        msgResponse = new MessageResponse()
        {
          Id = msgResponse.Id,
          Model = msgResponse.Model,
          Role = msgResponse.Role,
          StopReason = msgDeltaData.Delta.StopReason,
          StopSequence = msgDeltaData.Delta.StopSequence,
          Type = msgResponse.Type,
          Usage = newUsage,
          Content = msgResponse.Content,
        };

        if (request.Tools is not null && request.Tools.Count > 0)
        {
          msgResponse.ToolCall = GetToolCall(msgResponse, request.Tools);
        }
      }

      // yield response on message stop
      if (currentEvent.Type is EventType.MessageStop && msgResponse is not null)
      {
        var eventData = new MessageCompleteEventData(msgResponse, anthropicHeaders);
        yield return new AnthropicEvent(EventType.MessageComplete, eventData);
        msgResponse = null;
      }

      if (line is null)
      {
        if (string.IsNullOrWhiteSpace(currentEvent.Type) is false)
        {
          yield return currentEvent;
          currentEvent = new AnthropicEvent();
        }

        break;
      }

      if (line == string.Empty)
      {
        yield return currentEvent;
        currentEvent = new AnthropicEvent();
      }

      if (line.StartsWith(EventPrefix))
      {
        var eventType = line.Substring(EventPrefix.Length).Trim();
        currentEvent = new AnthropicEvent(eventType, currentEvent.Data);
        continue;
      }

      if (line.StartsWith(DataPrefix))
      {
        var eventData = line.Substring(DataPrefix.Length).Trim();
        var eventDataJson = Deserialize<EventData>(eventData);
        currentEvent = new AnthropicEvent(currentEvent.Type, eventDataJson!);
        continue;
      }
    } while (true);
  }

  /// <inheritdoc/>
  public async Task<AnthropicResult<MessageBatchResponse>> CreateMessageBatchAsync(MessageBatchRequest request, CancellationToken cancellationToken = default)
  {
    var response = await SendRequestAsync(MessageBatchesEndpoint, request, cancellationToken);
    return await CreateResultAsync<MessageBatchResponse>(response);
  }

  /// <inheritdoc/>
  public async Task<AnthropicResult<MessageBatchResponse>> GetMessageBatchAsync(string batchId, CancellationToken cancellationToken = default)
  {
    var response = await SendRequestAsync($"{MessageBatchesEndpoint}/{batchId}", cancellationToken: cancellationToken);
    return await CreateResultAsync<MessageBatchResponse>(response);
  }

  /// <inheritdoc/>
  public async Task<AnthropicResult<Page<MessageBatchResponse>>> ListMessageBatchesAsync(PagingRequest? request = null, CancellationToken cancellationToken = default)
  {
    var pagingRequest = request ?? new PagingRequest();
    var endpoint = $"{MessageBatchesEndpoint}?{pagingRequest.ToQueryParameters()}";
    var response = await SendRequestAsync(endpoint, cancellationToken: cancellationToken);
    return await CreateResultAsync<Page<MessageBatchResponse>>(response);
  }

  /// <inheritdoc/>
  public async IAsyncEnumerable<AnthropicResult<Page<MessageBatchResponse>>> ListAllMessageBatchesAsync(int limit = 20, [EnumeratorCancellation] CancellationToken cancellationToken = default)
  {
    await foreach (var result in GetAllPagesAsync<MessageBatchResponse>(MessageBatchesEndpoint, limit, cancellationToken))
    {
      yield return result;
    }
  }

  /// <inheritdoc/>
  public async Task<AnthropicResult<MessageBatchResponse>> CancelMessageBatchAsync(string batchId, CancellationToken cancellationToken = default)
  {
    var endpoint = $"{MessageBatchesEndpoint}/{batchId}/cancel";
    var response = await SendRequestAsync(endpoint, HttpMethod.Post, cancellationToken);
    return await CreateResultAsync<MessageBatchResponse>(response);
  }

  /// <inheritdoc/>
  public async Task<AnthropicResult<MessageBatchDeleteResponse>> DeleteMessageBatchAsync(string batchId, CancellationToken cancellationToken = default)
  {
    var endpoint = $"{MessageBatchesEndpoint}/{batchId}";
    var response = await SendRequestAsync(endpoint, HttpMethod.Delete, cancellationToken);
    return await CreateResultAsync<MessageBatchDeleteResponse>(response);
  }

  /// <inheritdoc/>
  public async Task<AnthropicResult<IAsyncEnumerable<MessageBatchResultItem>>> GetMessageBatchResultsAsync(string batchId, CancellationToken cancellationToken = default)
  {
    var response = await SendRequestAsync($"{MessageBatchesEndpoint}/{batchId}/results", cancellationToken: cancellationToken);
    var anthropicHeaders = new AnthropicHeaders(response.Headers);

    if (response.IsSuccessStatusCode is false)
    {
      var content = await response.Content.ReadAsStringAsync();
      var error = Deserialize<AnthropicError>(content) ?? new AnthropicError();
      return AnthropicResult<IAsyncEnumerable<MessageBatchResultItem>>.Failure(error, anthropicHeaders);
    }

    return AnthropicResult<IAsyncEnumerable<MessageBatchResultItem>>.Success(ReadResultsAsync(), anthropicHeaders);

    async IAsyncEnumerable<MessageBatchResultItem> ReadResultsAsync()
    {
      using var responseContent = await response.Content.ReadAsStreamAsync();
      using var streamReader = new StreamReader(responseContent);

      var line = await streamReader.ReadLineAsync();

      while (line is not null)
      {
        var resultItem = Deserialize<MessageBatchResultItem>(line) ?? new MessageBatchResultItem();
        yield return resultItem;

        line = await streamReader.ReadLineAsync();
      }
    }
  }

  /// <inheritdoc/>
  public async Task<AnthropicResult<TokenCountResponse>> CountMessageTokensAsync(CountMessageTokensRequest request, CancellationToken cancellationToken = default)
  {
    var response = await SendRequestAsync(CountTokensEndpoint, request, cancellationToken);
    return await CreateResultAsync<TokenCountResponse>(response);
  }

  /// <inheritdoc/>
  public async Task<AnthropicResult<Page<AnthropicModel>>> ListModelsAsync(PagingRequest? request = null, CancellationToken cancellationToken = default)
  {
    var pagingRequest = request ?? new PagingRequest();
    var endpoint = $"{ModelsEndpoint}?{pagingRequest.ToQueryParameters()}";
    var response = await SendRequestAsync(endpoint, cancellationToken: cancellationToken);
    return await CreateResultAsync<Page<AnthropicModel>>(response);
  }

  /// <inheritdoc/>
  public async IAsyncEnumerable<AnthropicResult<Page<AnthropicModel>>> ListAllModelsAsync(int limit = 20, [EnumeratorCancellation] CancellationToken cancellationToken = default)
  {
    await foreach (var result in GetAllPagesAsync<AnthropicModel>(ModelsEndpoint, limit, cancellationToken))
    {
      yield return result;
    }
  }

  /// <inheritdoc/>
  public async Task<AnthropicResult<AnthropicModel>> GetModelAsync(string modelId, CancellationToken cancellationToken = default)
  {
    var endpoint = $"{ModelsEndpoint}/{modelId}";
    var response = await SendRequestAsync(endpoint, cancellationToken: cancellationToken);
    return await CreateResultAsync<AnthropicModel>(response);
  }

  private async IAsyncEnumerable<AnthropicResult<Page<T>>> GetAllPagesAsync<T>(string endpoint, int limit = 20, [EnumeratorCancellation] CancellationToken cancellationToken = default)
  {
    var pagingRequest = new PagingRequest(limit: limit);
    string Endpoint() => $"{endpoint}?{pagingRequest.ToQueryParameters()}";
    bool hasMore;

    do
    {
      var response = await SendRequestAsync(Endpoint(), cancellationToken: cancellationToken);
      var anthropicHeaders = new AnthropicHeaders(response.Headers);
      var responseContent = await response.Content.ReadAsStringAsync();

      if (response.IsSuccessStatusCode is false)
      {
        var error = Deserialize<AnthropicError>(responseContent) ?? new AnthropicError();
        yield return AnthropicResult<Page<T>>.Failure(error, anthropicHeaders);
        yield break;
      }

      var page = Deserialize<Page<T>>(responseContent) ?? new Page<T>();

      if (page.HasMore && page.LastId is not null)
      {
        hasMore = true;
        pagingRequest = new PagingRequest(limit: limit, afterId: page.LastId);
      }
      else
      {
        hasMore = false;
      }

      yield return AnthropicResult<Page<T>>.Success(page, anthropicHeaders);
    } while (hasMore);
  }

  private ToolCall? GetToolCall(MessageResponse response, List<Tool> tools)
  {
    var toolUse = response.Content.OfType<ToolUseContent>().FirstOrDefault();

    if (toolUse is null)
    {
      return null;
    }

    var tool = tools.FirstOrDefault(t => t.Name == toolUse.Name);

    if (tool is null)
    {
      return null;
    }

    return new ToolCall(tool, toolUse);
  }

  private async Task<AnthropicResult<T>> CreateResultAsync<T>(HttpResponseMessage response) where T : new()
  {
    var anthropicHeaders = new AnthropicHeaders(response.Headers);
    var responseContent = await response.Content.ReadAsStringAsync();

    if (response.IsSuccessStatusCode is false)
    {
      var error = Deserialize<AnthropicError>(responseContent) ?? new AnthropicError();
      return AnthropicResult<T>.Failure(error, anthropicHeaders);
    }

    var model = Deserialize<T>(responseContent) ?? new T();
    return AnthropicResult<T>.Success(model, anthropicHeaders);
  }

  private async Task<HttpResponseMessage> SendRequestAsync(string endpoint, HttpMethod? method = null, CancellationToken cancellationToken = default)
  {
    var request = new HttpRequestMessage(method ?? HttpMethod.Get, endpoint);
    return await _httpClient.SendAsync(request, cancellationToken);
  }

  private async Task<HttpResponseMessage> SendRequestAsync<T>(string endpoint, T request, CancellationToken cancellationToken = default)
  {
    var requestJson = Serialize(request);
    var requestContent = new StringContent(requestJson, Encoding.UTF8, JsonContentType);
    return await _httpClient.PostAsync(endpoint, requestContent, cancellationToken);
  }

  private string Serialize<T>(T obj) => JsonSerializer.Serialize(obj, JsonSerializationOptions.DefaultOptions);
  private T? Deserialize<T>(string json) => JsonSerializer.Deserialize<T>(json, JsonSerializationOptions.DefaultOptions);
}