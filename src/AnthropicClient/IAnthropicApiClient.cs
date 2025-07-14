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

  /// <summary>
  /// Creates a file asynchronously using the Files API.
  /// </summary>
  /// <param name="request">The file creation request.</param>
  /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
  /// <returns>A task that represents the asynchronous operation. The task result contains the response as an <see cref="AnthropicResult{T}"/> where T is <see cref="AnthropicFile"/>.</returns>
  Task<AnthropicResult<AnthropicFile>> CreateFileAsync(CreateFileRequest request, CancellationToken cancellationToken = default);

  /// <summary>
  /// Lists files asynchronously, returning a single page of results.
  /// </summary>
  /// <param name="request">The paging request to use for listing the files.</param>
  /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
  /// <returns>A task that represents the asynchronous operation. The task result contains the response as an <see cref="AnthropicResult{T}"/> where T is <see cref="Page{T}"/> where T is <see cref="AnthropicFile"/>.</returns>
  Task<AnthropicResult<Page<AnthropicFile>>> ListFilesAsync(PagingRequest? request = null, CancellationToken cancellationToken = default);

  /// <summary>
  /// Lists all files asynchronously, returning every page of results.
  /// </summary>
  /// <param name="limit">The maximum number of files to return in each page.</param>
  /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
  /// <returns>An asynchronous enumerable that yields the response as an <see cref="AnthropicResult{T}"/> where T is <see cref="Page{T}"/> where T is <see cref="AnthropicFile"/>.</returns>
  IAsyncEnumerable<AnthropicResult<Page<AnthropicFile>>> ListAllFilesAsync(int limit = 20, CancellationToken cancellationToken = default);

  /// <summary>
  /// Gets a file's metadata by its ID asynchronously.
  /// </summary>
  /// <param name="fileId">The ID of the file to get.</param>
  /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
  /// <returns>A task that represents the asynchronous operation. The task result contains the response as an <see cref="AnthropicResult{T}"/> where T is <see cref="AnthropicFile"/>.</returns>
  Task<AnthropicResult<AnthropicFile>> GetFileInfoAsync(string fileId, CancellationToken cancellationToken = default);

  /// <summary>
  /// Gets a file's content by its ID asynchronously.
  /// </summary>
  /// <param name="fileId">The ID of the file to get the content for.</param>
  /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
  /// <returns>A task that represents the asynchronous operation. The task result contains the response as an <see cref="AnthropicResult{T}"/> where T is a stream containing the file content.</returns>
  Task<AnthropicResult<Stream>> GetFileAsync(string fileId, CancellationToken cancellationToken = default);


  /// <summary>
  /// Deletes a file by its ID asynchronously.
  /// </summary>
  /// <param name="fileId">The ID of the file to delete.</param>
  /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
  /// <returns>A task that represents the asynchronous operation. The task result contains the response as an <see cref="AnthropicResult{T}"/> where T is <see cref="AnthropicFileDeleteResponse"/>.</returns>
  Task<AnthropicResult<AnthropicFileDeleteResponse>> DeleteFileAsync(string fileId, CancellationToken cancellationToken = default);
}