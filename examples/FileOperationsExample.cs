using AnthropicClient;
using AnthropicClient.Models;

namespace AnthropicClient.Examples;

/// <summary>
/// Example demonstrating file operations with the Anthropic API.
/// </summary>
public class FileOperationsExample
{
  public static async Task RunExample()
  {
    // This is a demonstration of the file API methods
    // Note: You would need a real API key to run this example
    var client = new AnthropicApiClient("your-api-key", new HttpClient());

    // Create a file
    var fileContent = "Hello, this is a sample file content!"u8.ToArray();
    var fileRequest = new FileRequest(fileContent, "sample.txt", "text/plain");
    
    Console.WriteLine("Creating file...");
    var createResult = await client.CreateFileAsync(fileRequest);
    
    if (createResult.IsFailure)
    {
      Console.WriteLine($"Failed to create file: {createResult.Error.Error.Message}");
      return;
    }
    
    var fileId = createResult.Value.Id;
    Console.WriteLine($"File created with ID: {fileId}");

    // List files
    Console.WriteLine("\nListing files...");
    var listResult = await client.ListFilesAsync();
    
    if (listResult.IsSuccess)
    {
      Console.WriteLine($"Found {listResult.Value.Data.Length} files");
      foreach (var file in listResult.Value.Data)
      {
        Console.WriteLine($"- {file.Filename} ({file.Id})");
      }
    }

    // Get file metadata
    Console.WriteLine($"\nGetting file metadata for {fileId}...");
    var getResult = await client.GetFileAsync(fileId);
    
    if (getResult.IsSuccess)
    {
      var file = getResult.Value;
      Console.WriteLine($"File: {file.Filename}");
      Console.WriteLine($"Size: {file.SizeBytes} bytes");
      Console.WriteLine($"Content Type: {file.ContentType}");
      Console.WriteLine($"Created: {file.CreatedAt}");
    }

    // Download file
    Console.WriteLine($"\nDownloading file {fileId}...");
    var downloadResult = await client.DownloadFileAsync(fileId);
    
    if (downloadResult.IsSuccess)
    {
      var download = downloadResult.Value;
      var contentText = System.Text.Encoding.UTF8.GetString(download.Content);
      Console.WriteLine($"Downloaded content: {contentText}");
    }

    // List all files with pagination
    Console.WriteLine("\nListing all files (with pagination)...");
    await foreach (var pageResult in client.ListAllFilesAsync(limit: 10))
    {
      if (pageResult.IsSuccess)
      {
        Console.WriteLine($"Page with {pageResult.Value.Data.Length} files");
      }
    }

    // Delete file
    Console.WriteLine($"\nDeleting file {fileId}...");
    var deleteResult = await client.DeleteFileAsync(fileId);
    
    if (deleteResult.IsSuccess)
    {
      Console.WriteLine($"File deleted: {deleteResult.Value.Deleted}");
    }
  }
}