namespace AnthropicClient.Tests.Integration;

public class AnthropicApiClientFileTests : IntegrationTest
{
  [Fact]
  public async Task CreateFileAsync_WhenCalled_ItShouldReturnFileResponse()
  {
    var fileResponseJson = @"{
      ""type"": ""file"",
      ""id"": ""file_abc123"",
      ""filename"": ""example.txt"",
      ""content_type"": ""text/plain"",
      ""size_bytes"": 1024,
      ""created_at"": ""2024-03-15T10:30:00Z""
    }";

    _mockHttpMessageHandler
      .WhenCreateFileRequest()
      .Respond(HttpStatusCode.OK, "application/json", fileResponseJson);

    var content = "Hello World"u8.ToArray();
    var request = new FileRequest(content, "example.txt", "text/plain");

    var result = await Client.CreateFileAsync(request);

    result.IsSuccess.Should().BeTrue();
    result.Value.Should().NotBeNull();
    result.Value!.Id.Should().Be("file_abc123");
    result.Value.Filename.Should().Be("example.txt");
    result.Value.ContentType.Should().Be("text/plain");
    result.Value.SizeBytes.Should().Be(1024);
  }

  [Fact]
  public async Task ListFilesAsync_WhenCalled_ItShouldReturnFilesPage()
  {
    var filesResponseJson = @"{
      ""data"": [
        {
          ""type"": ""file"",
          ""id"": ""file_abc123"",
          ""filename"": ""example.txt"",
          ""content_type"": ""text/plain"",
          ""size_bytes"": 1024,
          ""created_at"": ""2024-03-15T10:30:00Z""
        }
      ],
      ""has_more"": false,
      ""first_id"": ""file_abc123"",
      ""last_id"": ""file_abc123""
    }";

    _mockHttpMessageHandler
      .WhenListFilesRequest()
      .Respond(HttpStatusCode.OK, "application/json", filesResponseJson);

    var result = await Client.ListFilesAsync();

    result.IsSuccess.Should().BeTrue();
    result.Value.Should().NotBeNull();
    result.Value!.Data.Should().HaveCount(1);
    result.Value.Data[0].Id.Should().Be("file_abc123");
    result.Value.HasMore.Should().BeFalse();
  }

  [Fact]
  public async Task GetFileAsync_WhenCalled_ItShouldReturnFileResponse()
  {
    var fileResponseJson = @"{
      ""type"": ""file"",
      ""id"": ""file_abc123"",
      ""filename"": ""example.txt"",
      ""content_type"": ""text/plain"",
      ""size_bytes"": 1024,
      ""created_at"": ""2024-03-15T10:30:00Z""
    }";

    _mockHttpMessageHandler
      .WhenGetFileRequest("file_abc123")
      .Respond(HttpStatusCode.OK, "application/json", fileResponseJson);

    var result = await Client.GetFileAsync("file_abc123");

    result.IsSuccess.Should().BeTrue();
    result.Value.Should().NotBeNull();
    result.Value!.Id.Should().Be("file_abc123");
    result.Value.Filename.Should().Be("example.txt");
  }

  [Fact]
  public async Task DownloadFileAsync_WhenCalled_ItShouldReturnFileContent()
  {
    var fileContent = "Hello World"u8.ToArray();

    var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
    {
      Content = new ByteArrayContent(fileContent)
    };
    httpResponseMessage.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("text/plain");
    httpResponseMessage.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
    {
      FileName = "\"example.txt\""
    };

    _mockHttpMessageHandler
      .WhenDownloadFileRequest("file_abc123")
      .Respond(_ => httpResponseMessage);

    var result = await Client.DownloadFileAsync("file_abc123");

    result.IsSuccess.Should().BeTrue();
    result.Value.Should().NotBeNull();
    result.Value!.Content.Should().BeEquivalentTo(fileContent);
    result.Value.Filename.Should().Be("example.txt");
    result.Value.ContentType.Should().Be("text/plain");
    result.Value.SizeBytes.Should().Be(fileContent.Length);
  }

  [Fact]
  public async Task DeleteFileAsync_WhenCalled_ItShouldReturnDeleteResponse()
  {
    var deleteResponseJson = @"{
      ""type"": ""file_deleted"",
      ""id"": ""file_abc123"",
      ""deleted"": true
    }";

    _mockHttpMessageHandler
      .WhenDeleteFileRequest("file_abc123")
      .Respond(HttpStatusCode.OK, "application/json", deleteResponseJson);

    var result = await Client.DeleteFileAsync("file_abc123");

    result.IsSuccess.Should().BeTrue();
    result.Value.Should().NotBeNull();
    result.Value!.Id.Should().Be("file_abc123");
    result.Value.Deleted.Should().BeTrue();
  }

  [Fact]
  public async Task CreateFileAsync_WhenCalledAndErrorReturned_ItShouldHandleError()
  {
    var errorJson = @"{
      ""type"": ""error"",
      ""error"": {
        ""type"": ""invalid_request_error"",
        ""message"": ""File too large""
      }
    }";

    _mockHttpMessageHandler
      .WhenCreateFileRequest()
      .Respond(HttpStatusCode.BadRequest, "application/json", errorJson);

    var content = "Hello World"u8.ToArray();
    var request = new FileRequest(content, "example.txt", "text/plain");

    var result = await Client.CreateFileAsync(request);

    result.IsSuccess.Should().BeFalse();
    result.Error.Should().BeOfType<AnthropicError>();
    result.Error.Error.Should().BeOfType<InvalidRequestError>();
  }
}