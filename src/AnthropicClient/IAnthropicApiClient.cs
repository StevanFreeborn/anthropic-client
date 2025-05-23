using AnthropicClient.Models;

namespace AnthropicClient;

/// <summary>
/// Represents a client for interacting with the Anthropic API.
/// </summary>
public interface IAnthropicApiClient
{
  /// <summary>
  /// Creates a message asynchronously.
  /// </summary>
  /// <param name="request">The message request to create.</param>
  /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
  /// <returns>A task that represents the asynchronous operation. The task result contains the response as an <see cref="AnthropicResult{T}"/>.</returns>
  Task<AnthropicResult<MessageResponse>> CreateMessageAsync(MessageRequest request, CancellationToken cancellationToken = default);

  /// <summary>
  /// Creates a message asynchronously and streams the response.
  /// </summary>
  /// <param name="request">The message request to create.</param>
  /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
  /// <returns>An asynchronous enumerable that yields the response event by event.</returns>
  IAsyncEnumerable<AnthropicEvent> CreateMessageAsync(StreamMessageRequest request, CancellationToken cancellationToken = default);

  /// <summary>
  /// Creates a batch of messages asynchronously.
  /// </summary>
  /// <param name="request">The message batch request to create.</param>
  /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
  /// <returns>A task that represents the asynchronous operation. The task result contains the response as an <see cref="AnthropicResult{T}"/> where T is <see cref="MessageBatchResponse"/>.</returns>
  Task<AnthropicResult<MessageBatchResponse>> CreateMessageBatchAsync(MessageBatchRequest request, CancellationToken cancellationToken = default);

  /// <summary>
  /// Gets a message batch asynchronously.
  /// </summary>
  /// <param name="batchId">The ID of the message batch to get.</param>
  /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
  /// <returns>A task that represents the asynchronous operation. The task result contains the response as an <see cref="AnthropicResult{T}"/> where T is <see cref="MessageBatchResponse"/>.</returns>
  Task<AnthropicResult<MessageBatchResponse>> GetMessageBatchAsync(string batchId, CancellationToken cancellationToken = default);

  /// <summary>
  /// Lists the message batches asynchronously.
  /// </summary>
  /// <param name="request">The paging request to use for listing the message batches.</param>
  /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
  /// <returns>A task that represents the asynchronous operation. The task result contains the response as an <see cref="AnthropicResult{T}"/> where T is <see cref="Page{T}"/> where T is <see cref="MessageBatchResponse"/>.</returns>
  Task<AnthropicResult<Page<MessageBatchResponse>>> ListMessageBatchesAsync(PagingRequest? request = null, CancellationToken cancellationToken = default);

  /// <summary>
  /// Lists all message batches asynchronously.
  /// </summary>
  /// <param name="limit">The maximum number of message batches to return in each page.</param>
  /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
  /// <returns>An asynchronous enumerable that yields the response as an <see cref="AnthropicResult{T}"/> where T is <see cref="Page{T}"/> where T is <see cref="MessageBatchResponse"/>.</returns>
  IAsyncEnumerable<AnthropicResult<Page<MessageBatchResponse>>> ListAllMessageBatchesAsync(int limit = 20, CancellationToken cancellationToken = default);

  /// <summary>
  /// Cancels a message batch asynchronously.
  /// </summary>
  /// <param name="batchId">The ID of the message batch to cancel.</param>
  /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
  /// <returns>A task that represents the asynchronous operation. The task result contains the response as an <see cref="AnthropicResult{T}"/> where T is <see cref="MessageBatchResponse"/>.</returns>
  Task<AnthropicResult<MessageBatchResponse>> CancelMessageBatchAsync(string batchId, CancellationToken cancellationToken = default);

  /// <summary>
  /// Deletes a message batch asynchronously.
  /// </summary>
  /// <param name="batchId">The ID of the message batch to delete.</param>
  /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
  /// <returns>A task that represents the asynchronous operation. The task result contains the response as an <see cref="AnthropicResult{T}"/> where T is <see cref="MessageBatchDeleteResponse"/>.</returns>
  Task<AnthropicResult<MessageBatchDeleteResponse>> DeleteMessageBatchAsync(string batchId, CancellationToken cancellationToken = default);

  /// <summary>
  /// Gets the results of a message batch asynchronously.
  /// </summary>
  /// <param name="batchId">The ID of the message batch to get the results for.</param>
  /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
  /// <returns>A task that represents the asynchronous operation. The task result contains the response as an <see cref="AnthropicResult{T}"/> where T is <see cref="IAsyncEnumerable{T}"/> where T is <see cref="MessageBatchResultItem"/>.</returns>
  Task<AnthropicResult<IAsyncEnumerable<MessageBatchResultItem>>> GetMessageBatchResultsAsync(string batchId, CancellationToken cancellationToken = default);

  /// <summary>
  /// Counts the tokens in a message asynchronously.
  /// </summary>
  /// <param name="request">The count message tokens request.</param>
  /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
  /// <returns>A task that represents the asynchronous operation. The task result contains the response as an <see cref="AnthropicResult{T}"/> where T is <see cref="TokenCountResponse"/>.</returns>
  Task<AnthropicResult<TokenCountResponse>> CountMessageTokensAsync(CountMessageTokensRequest request, CancellationToken cancellationToken = default);

  /// <summary>
  /// Lists models asynchronously, returning a single page of results.
  /// </summary>
  /// <param name="request">The paging request to use for listing the models.</param>
  /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
  /// <returns>A task that represents the asynchronous operation. The task result contains the response as an <see cref="AnthropicResult{T}"/> where T is <see cref="Page{T}"/> where T is <see cref="AnthropicModel"/>.</returns>
  Task<AnthropicResult<Page<AnthropicModel>>> ListModelsAsync(PagingRequest? request = null, CancellationToken cancellationToken = default);

  /// <summary>
  /// Lists all models asynchronously, returning every page of results.
  /// </summary>
  /// <param name="limit">The maximum number of models to return in each page.</param>
  /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
  /// <returns>An asynchronous enumerable that yields the response as an <see cref="AnthropicResult{T}"/> where T is <see cref="Page{T}"/> where T is <see cref="AnthropicModel"/>.</returns>
  /// 
  IAsyncEnumerable<AnthropicResult<Page<AnthropicModel>>> ListAllModelsAsync(int limit = 20, CancellationToken cancellationToken = default);

  /// <summary>
  /// Gets a model by its ID asynchronously.
  /// </summary>
  /// <param name="modelId">The ID of the model to get.</param>
  /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
  /// <returns>A task that represents the asynchronous operation. The task result contains the response as an <see cref="AnthropicResult{T}"/> where T is <see cref="AnthropicModel"/>.</returns>
  Task<AnthropicResult<AnthropicModel>> GetModelAsync(string modelId, CancellationToken cancellationToken = default);
}