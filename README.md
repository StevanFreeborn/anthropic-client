# AnthropicClient

[![Pull Request](https://github.com/StevanFreeborn/anthropic-client/actions/workflows/pull_request.yml/badge.svg)](https://github.com/StevanFreeborn/anthropic-client/actions/workflows/pull_request.yml)
[![codecov](https://codecov.io/gh/StevanFreeborn/anthropic-client/graph/badge.svg?token=hZ3MjhxDv9)](https://codecov.io/gh/StevanFreeborn/anthropic-client)
[![Publish](https://github.com/StevanFreeborn/anthropic-client/actions/workflows/publish.yml/badge.svg)](https://github.com/StevanFreeborn/anthropic-client/actions/workflows/publish.yml)
[![semantic-release: angular](https://img.shields.io/badge/semantic--release-angular-e10079?logo=semantic-release)](https://github.com/semantic-release/semantic-release)
![NuGet Version](https://img.shields.io/nuget/v/AnthropicClient)
![NuGet Downloads](https://img.shields.io/nuget/dt/AnthropicClient)

This library for the Anthropic API is meant to simplify development in C# for Anthropic users.

> [!NOTE]
> This is an unofficial SDK for the Anthropic API. It was not built in consultation with Anthropic or any member of their organization.

This SDK was developed independently using existing libraries and the Anthropic API documentation as the starting point with the intention of making development of integrations done in C# with Anthropic quicker and more convenient.

> [!NOTE]  
> This client library is heavily inspired by the [Anthropic.SDK](https://github.com/tghamm/Anthropic.SDK) library. I chose to create a new library because I wanted to handle streaming and tool calling differently as well as have control over the client library as I plan to use it to build a connector for [SemanticKernel](https://github.com/microsoft/semantic-kernel). However if you are looking for a client library the Anthropic.SDK is a great place to start.

## 📝 Issues

If you encounter any issues while using this library please open an issue [here](https://github.com/StevanFreeborn/anthropic-client/issues).

## 📜 License

This library is licensed under the [MIT License](https://choosealicense.com/licenses/mit/) and is free to use and modify.

## 📝 Contributing

If you would like to contribute to this library please open a pull request [here](https://github.com/StevanFreeborn/anthropic-client/pulls).

## 🛠️ Dependencies

### [Microsoft.Bcl.AsyncInterfaces](https://www.nuget.org/packages/Microsoft.Bcl.AsyncInterfaces/)

![Microsoft.Bcl.AsyncInterfaces NuGet Version](https://img.shields.io/nuget/v/Microsoft.Bcl.AsyncInterfaces)

Used to support async interfaces when streaming messages

### [System.Text.Json](https://www.nuget.org/packages/System.Text.Json/)

![NuGet Version](https://img.shields.io/nuget/v/System.Text.Json)

Used for JSON serialization and deserialization

## 💾 Installation

Install the package from [NuGet](https://www.nuget.org) using the following command:

```bash
dotnet add package AnthropicClient
```

## 🔑 API Key

In order to use the Anthropic API you will need an API key. You can get one by signing up at [Anthropic](https://www.anthropic.com/api). Please keep your API key secure and do not share it with others. Be mindful of where you store your API key and do not commit it to a public repository.

## 👨🏻‍💻 Start Coding

### `AnthropicApiClient`

The most common way to use the SDK is to create an `AnthropicApiClient` instance and call its methods. Its constructor requires two parameters:

- `apiKey` - your Anthropic API key
- `httpClient` - an `HttpClient` instance. You can configure and customize the `HttpClient` instance as needed. This library however will perform the necessary configuration to work with the Anthropic API. Such as setting the base address and adding the proper headers.

> [!NOTE]
> This library does not manage the lifecycle of the `HttpClient` instance. You should create and manage the lifecycle of the `HttpClient` instance in your application.

It is best practice to read the API key from a secure location such as a configuration file or environment variable. For example using the `appsettings.json` file:

```json
{
  "AnthropicApiKey": "YOUR_API"
}
```

Example constructing an `AnthropicApiClient` instance:

```csharp
using AnthropicClient;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
  .AddJsonFile("appsettings.json")
  .Build();

var apiKey = configuration["AnthropicApiKey"];

var client = new AnthropicApiClient(apiKey, new HttpClient());
```

### `IAnthropicApiClient`

The library does expose an interface `IAnthropicApiClient` that can be used for dependency injection and testing. The interface is implemented by the `AnthropicApiClient` class.

### Full API Documentation

This library was developed to make using the Anthropic API easier within a .NET application. If you are looking for the full API documentation you can find it at [Anthropic API Documentation](https://docs.anthropic.com/).

## Usage

The primary use case for working with the Anthropic API is to create a message in response to a request that includes one or more other messages. The created message can then be received either as a complete response or a stream of events. This can be used to create a conversation between the caller and Anthropic's AI models and/or to use Anthropic's AI models to perform a task.

> [!NOTE]
> The following examples assume that you have already created an instance of the `AnthropicApiClient` class named `client`. You can also find these snippets in the examples directory.

### Count Message Tokens

The `AnthropicApiClient` exposes a method named `CountMessageTokensAsync` that can be used to count the number of tokens in a message. The method requires a `CountMessageTokensRequest` instance as a parameter.

```csharp
using AnthropicClient;
using AnthropicClient.Models;

var response = await client.CountMessageTokensAsync(new CountMessageTokensRequest(
  AnthropicModels.Claude3Haiku,
  [
    new(
      MessageRole.User, 
      [new TextContent("Please write a haiku about the ocean.")]
    )
  ]
));

if (response.IsFailure)
{
  Console.WriteLine("Failed to count message tokens");
  Console.WriteLine("Error Type: {0}", response.Error.Error.Type);
  Console.WriteLine("Error Message: {0}", response.Error.Error.Message);
  return;
}

Console.WriteLine("Token Count: {0}", response.Value.InputTokens);
```

### List Models

The `AnthropicApiClient` exposes a method named `ListModelsAsync` that can be used to list the available models. The method takes an optional `PagingRequest` instance as a parameter.

```csharp
using AnthropicClient;

var response = await client.ListModelsAsync();

if (response.IsFailure)
{
  Console.WriteLine("Failed to list models");
  Console.WriteLine("Error Type: {0}", response.Error.Error.Type);
  Console.WriteLine("Error Message: {0}", response.Error.Error.Message);
  return;
}

foreach (var model in response.Value.Data)
{
  Console.WriteLine("Model Id: {0}", model.Id);
  Console.WriteLine("Model Name: {0}", model.DisplayName);
}
```

Using the `PagingRequest` instance allows you to specify the number of models to return and the page of models to return.

```csharp
using AnthropicClient;
using AnthropicClient.Models;

var response = await client.ListModelsAsync(new PagingRequest(afterId: "claude-3-5-sonnet-20241022", limit: 2));

if (response.IsFailure)
{
  Console.WriteLine("Failed to list models");
  Console.WriteLine("Error Type: {0}", response.Error.Error.Type);
  Console.WriteLine("Error Message: {0}", response.Error.Error.Message);
  return;
}

foreach (var model in response.Value.Data)
{
  Console.WriteLine("Model Id: {0}", model.Id);
  Console.WriteLine("Model Name: {0}", model.DisplayName);
}
```

### Get Model

The `AnthropicApiClient` exposes a method named `GetModelAsync` that can be used to get a model by its id.

```csharp
using AnthropicClient;

var response = await client.GetModelAsync("claude-3-5-sonnet-20241022");

if (response.IsFailure)
{
  Console.WriteLine("Failed to get model");
  Console.WriteLine("Error Type: {0}", response.Error.Error.Type);
  Console.WriteLine("Error Message: {0}", response.Error.Error.Message);
  return;
}

Console.WriteLine("Model Id: {0}", response.Value.Id);
```

### Files API

The `AnthropicApiClient` provides support for the Anthropic Files API, which allows you to upload and manage files for use with the Anthropic API.

#### Create a File

You can create a file using the Files API in several ways:

##### From a Byte Array

```csharp
using AnthropicClient;
using AnthropicClient.Models;
using System.Text;

// Create a file from a byte array
var content = "This is a sample text file for the Anthropic Files API.";
var fileBytes = Encoding.UTF8.GetBytes(content);

var request = new CreateFileRequest(fileBytes, "sample.txt", "text/plain");
var response = await client.CreateFileAsync(request);

if (response.IsSuccess)
{
  Console.WriteLine("File created successfully from byte array!");
  Console.WriteLine("File ID: {0}", response.Value.Id);
  Console.WriteLine("File Name: {0}", response.Value.FileName);
  Console.WriteLine("File Type: {0}", response.Value.FileType);
  Console.WriteLine("File Size: {0} bytes", response.Value.SizeBytes);
}
else
{
  Console.WriteLine("Failed to create file");
  Console.WriteLine("Error Type: {0}", response.Error.Error.Type);
  Console.WriteLine("Error Message: {0}", response.Error.Error.Message);
}
```

##### From a Stream

```csharp
using AnthropicClient;
using AnthropicClient.Models;
using System.Text;

// Create a file from a stream
using var stream = new MemoryStream(Encoding.UTF8.GetBytes("Stream content"));
var request = new CreateFileRequest(stream, "stream-file.txt", "text/plain");
var response = await client.CreateFileAsync(request);

if (response.IsSuccess)
{
  Console.WriteLine("File created successfully from stream!");
  Console.WriteLine("File ID: {0}", response.Value.Id);
}
else
{
  Console.WriteLine("Failed to create file");
  Console.WriteLine("Error Type: {0}", response.Error.Error.Type);
  Console.WriteLine("Error Message: {0}", response.Error.Error.Message);
}
```

> [!NOTE]
> The Files API has certain limitations on file size, supported file types, and usage quotas. Please refer to the [Anthropic API Documentation](https://docs.anthropic.com/en/docs/build-with-claude/files) for the most up-to-date information on these limitations.

### Create a message

The `AnthropicApiClient` exposes a method named `CreateMessageAsync` that can be used to create a message. The method requires a `MessageRequest` or a `StreamMessageRequest` instance as a parameter. The `MessageRequest` class is used to create a message whose response is not streamed and the `StreamMessageRequest` class is used to create a message whose response is streamed. The `MessageRequest` instance's properties can be set to configure how the message is created.

#### Non-Streaming

```csharp
using AnthropicClient;
using AnthropicClient.Models;

var response = await client.CreateMessageAsync(new MessageRequest(
  AnthropicModels.Claude3Haiku,
  [
    new(
      MessageRole.User, 
      [new TextContent("Please write a haiku about the ocean.")]
    )
  ]
));

if (response.IsSuccess is false)
{
  Console.WriteLine("Failed to create message");
  Console.WriteLine("Error Type: {0}", response.Error.Error.Type);
  Console.WriteLine("Error Message: {0}", response.Error.Error.Message);
  return;
}

foreach (var content in response.Value.Content)
{
  switch (content)
  {
    case TextContent textContent:
      Console.WriteLine(textContent.Text);
      break;
  }
}
```

#### Streaming

Anthropic uses Server-Sent Events (SSE) to stream messages. The possible events and the format of those events are documented in the [Anthropic API Documentation](https://docs.anthropic.com/en/api/messages-streaming). This library provides a way to consume them after they have been deserialized into strongly-typed C# objects that are returned in an `IAsyncEnumerable` collection.

This allows you to consume the events as they are received and process them in the way that best fits your use case. The following example demonstrates how to consume the streamed events and build up the complete text response from the model.

```csharp
using AnthropicClient;
using AnthropicClient.Models;

var events = client.CreateMessageAsync(new StreamMessageRequest(
  AnthropicModels.Claude3Haiku,
  [
    new(
      MessageRole.User, 
      [new TextContent("Please write a haiku about the ocean.")]
    )
  ]
));

var msgBuilder = new StringBuilder();

await foreach (var e in events)
{
  switch (e.Data)
  {
    case var data when data is ContentDeltaEventData contentData:
      switch (contentData.Delta)
      {
        case var delta when delta is TextDelta textDelta:
          msgBuilder.Append(textDelta.Text);
          break;
      }
      break;
  }
}

Console.WriteLine(msgBuilder.ToString());
```

##### Message Complete Event

This library also provides a custom `message_complete` event that is yielded when all the message's events have been received. This event is not part of Anthropic's SSE events but is provided to allow for easier consumption of the entire message response if desired and make it easier to implement built-in tool calling.

```csharp
using AnthropicClient;
using AnthropicClient.Models;

var events = client.CreateMessageAsync(new StreamMessageRequest(
  AnthropicModels.Claude3Haiku,
  [
    new(
      MessageRole.User, 
      [new TextContent("Please write a haiku about the ocean.")]
    )
  ]
));

MessageResponse? response = null;

await foreach (var e in events)
{
  switch (e.Data)
  {
    case var data when data is MessageCompleteEventData msgData:
      response = msgData.Message;
      break;
  }
}

var textContent = response?.Content
  .OfType<TextContent>()
  .Aggregate(new StringBuilder(), (sb, c) => sb.Append(c.Text))
  .ToString();

Console.WriteLine(textContent);
```

### Tool Use

Anthropic's models support the use of tools to perform tasks. This allows the models to interact with external client-side tools that can perform actions the models cannot do natively. This gives you the ability to further extend the model's abilities with your own custom tools. This feature is covered in depth in [Anthropic's API Documentation](https://docs.anthropic.com/en/docs/build-with-claude/tool-use). This library aims to make using tools convenient by allowing you to create, provide, and call tools from within your application by leveraging the reflection capabilities of C#.

> [!NOTE]
> All tools are user provided. The models do no not have access to any built-in server-side tools.

#### Create a tool

You can create a tool in 4 different ways and then provide that tool when creating a message.

1. Create a tool from a class
1. Create a tool from a static method
1. Create a tool from an instance method
1. Create a tool from a delegate

##### Create a tool from a class

When creating a tool from a class the class must implement the `ITool` interface.

```csharp
using AnthropicClient.Models;

class GetWeatherTool : ITool
{
  public string Name => "Get Weather";

  public string Description => "Get the weather for a location in the specified units";

  public MethodInfo Function => typeof(GetWeatherTool).GetMethod(nameof(GetWeather))!;

  public static string GetWeather(string location, string units)
  {
    return $"The weather in {location} is 72 degrees {units}";
  }
}

var getWeatherTool = Tool.CreateFromClass<GetWeatherTool>();
```

##### Create a tool from a static method

When creating a tool from a static method the method must be public and static.

```csharp
using AnthropicClient.Models;

class GetWeatherTool
{
  public static string GetWeather(string location)
  {
    return $"The weather in {location} is 72 degrees Fahrenheit";
  }
}

var getWeatherTool = Tool.CreateFromStaticMethod(
  "Get Weather", 
  "Get the weather for a location in the specified units", 
  typeof(GetWeatherTool), 
  nameof(GetWeatherTool.GetWeather)
);
```

##### Create a tool from an instance method

When creating a tool from an instance method the method must be public and non-static.

```csharp
using AnthropicClient.Models;

class GetWeatherTool
{
  public string GetWeather(string location)
  {
    return $"The weather in {location} is 72 degrees Fahrenheit";
  }
}

var toolInstance = new GetWeatherTool();

var getWeatherTool = Tool.CreateFromInstanceMethod(
  "Get Weather", 
  "Get the weather for a location in the specified units", 
  toolInstance,
  nameof(toolInstance.GetWeather)
);
```

##### Create a tool from a delegate

When creating a tool from a delegate the delegate must be a `Func<TResult>`, `Func<T, TResult>`, or `Func<T1, T2, TResult>`. If you need to create a tool from a delegate that takes more than 2 parameters you should create a complex type and pass that as the parameter.

```csharp
using AnthropicClient.Models;

var tool = (string location, string units) => $"The weather in {location} is 72 degrees {units}";

var getWeatherTool = Tool.CreateFromFunction(
  "Get Weather", 
  "Get the weather for a location in the specified units", 
  tool
);
```

##### Function Parameter Attribute

When you create a tool from one of the methods above and send it to Anthropic in your request a JSON representation of the tool is provided in the message. This JSON representation includes the name, description, and input schema of the tool. This information is used by Anthropic's models to discern if and when it should use a tool.

This library provides a `FunctionParameterAttribute` that can be used to provide additional information about the parameters of the tool. This information is used to provide a more detailed input schema for the tool.

```csharp
using AnthropicClient.Models;

var tool = (
  [FunctionParameter(description: "The location of the weather being got", name: "Location", required: true)] 
  string location, 
  string units
) => $"The weather in {location} is 72 degrees {units}";

var getWeatherTool = Tool.CreateFromFunction(
  "Get Weather", 
  "Get the weather for a location in the specified units", 
  tool
);
```

##### Function Property Attribute

This library also provides a `FunctionPropertyAttribute` that can be used to provide additional information about the members of complex types used as parameters in the tool. This information is used to provide a more detailed input schema for the tool.

```csharp
using AnthropicClient.Models;

class GetWeatherInput
{
  [FunctionProperty(
    description: "The location of the weather being got",
    required: true
  )]
  public string Location { get; } = string.Empty;

  [FunctionProperty(
    description: "The units to get the weather in",
    required: false,
    defaultValue: "Fahrenheit",
    possibleValues: ["Fahrenheit", "Celsius"]
  )]
  public string Units { get; } = "Fahrenheit";
}

  var tool = (GetWeatherInput input) => $"The weather in {input.Location} is 72 degrees {input.Units}";

  var getWeatherTool = Tool.CreateFromFunction(
    "Get Weather", 
    "Get the weather for a location in the specified units", 
    tool
  );
```

#### Call a tool

It is important to remember that while Anthropic's models do support tool use they don't actually have access to any built-in server-side tools. All tools are user provided. This means that while Anthropic's models can respond to a request to create a message with a request to use a tool that is all it is - a request. It is still up to the client to handle the tool request by calling the tool with the input provided by the model and then providing the result of that call back to the model.

This library aims to make this process convenient by allowing you to simply provide the tools you want Anthropic's models to consider for use when creating a message, receive the response, check if the response contains a tool call, and if it does invoke the tool to get the result.

> [!NOTE]
> Anthropic's API expects requests to contain messages that alternate between the user and the assistant. In addition if you receive a tool use from the model the API expects you to respond with a message that contains the result of the tool call. The tool use content will always be from the assistant while the tool result will always be from the user.

```csharp
using AnthropicClient;
using AnthropicClient.Models;

class GetWeatherTool : ITool
{
  public string Name => "Get Weather";

  public string Description => "Get the weather for a location in the specified units";

  public MethodInfo Function => typeof(GetWeatherTool).GetMethod(nameof(GetWeather))!;

  public static string GetWeather(string location, string units)
  {
    return $"The weather in {location} is 72 degrees {units}";
  }
}

List<Message> messages = [
  new(
    MessageRole.User, 
    [new TextContent("What is the weather in New York?")]
  )
];

List<Tool> tools = [Tool.CreateFromClass<GetWeatherTool>()];

var response = await client.CreateMessageAsync(new MessageRequest(
  AnthropicModels.Claude3Haiku,
  messages,
  tools: tools
));

if (response.IsSuccess is false)
{
  Console.WriteLine("Failed to create message");
  Console.WriteLine("Error Type: {0}", response.Error.Error.Type);
  Console.WriteLine("Error Message: {0}", response.Error.Error.Message);
  return;
}

messages.Add(new(MessageRole.Assistant, response.Content));


foreach (var content in response.Value.Content)
{
  switch (content)
  {
    case TextContent textContent:
      Console.WriteLine(textContent.Text);
      break;
    case ToolUseContent toolUseContent:
      Console.WriteLine(toolUseContent.Name);
      break;
  }
}

if (response.Value.ToolCall is not null)
{
  var toolCallResult = await response.Value.ToolCall.InvokeAsync<string>();
  string toolResultContent;

  if (toolCallResult.IsSuccess && toolCallResult.Value is not null)
  {
    Console.WriteLine(toolCallResult.Value);
    toolResultContent = toolCallResult.Value;
  }
  else
  {
    Console.WriteLine(toolCallResult.Error.Message);
    toolResultContent = toolCallResult.Error.Message;
  }

  messages.Add(
    new(
      MessageRole.User, 
      [
        new ToolResultContent(
          response.Value.ToolCall.ToolUse.Id, 
          toolResultContent
        )
      ]
    )
  );
}

var finalResponse = await client.CreateMessageAsync(new MessageRequest(
  AnthropicModels.Claude3Haiku,
  messages,
  tools: tools
));

if (finalResponse.IsSuccess is false)
{
  Console.WriteLine("Failed to create message");
  Console.WriteLine("Error Type: {0}", finalResponse.Error.Error.Type);
  Console.WriteLine("Error Message: {0}", finalResponse.Error.Error.Message);
  return;
}

foreach (var content in finalResponse.Value.Content)
{
  switch (content)
  {
    case TextContent textContent:
      Console.WriteLine(textContent.Text);
      break;
  }
}
```

If an exception is thrown while invoking the tool the `InvokeAsync` method will return a `ToolCallResult` with the exception contained in the `Error` property.

> [!NOTE]
> The `InvokeAsync` method does accept a generic type parameter that can be used to specify the type of the `Value` property of the `ToolCallResult`. If it is not specified it will be an `object`.

#### Call a tool in streamed message

Tool calling is also supported when streaming the message response. The following example demonstrates how you can handle a tool call in a streamed message response.

```csharp
using AnthropicClient;
using AnthropicClient.Models;

var tool = (string location, string units) => $"The weather in {location} is 72 degrees {units}";

var messages = [
  new(
    MessageRole.User, 
    [new TextContent("What is the weather in New York?")]
  )
];

var tools = [Tool.CreateFromFunction(
  "Get Weather", 
  "Get the weather for a location in the specified units", 
  tool
)];

var events = client.CreateMessageAsync(new StreamMessageRequest(
  AnthropicModels.Claude3Haiku,
  messages,
  tools: tools
));

MessageResponse? response = null;

await foreach (var e in events)
{
  switch (e.Data)
  {
    case var data when data is MessageCompleteEventData msgData:
      response = msgData.Message;
      break;
  }
}

if (response is null)
{
  Console.WriteLine("Failed to get message response");
  return;
}

messages.Add(new(MessageRole.Assistant, response.Content));

if (response?.ToolCall is not null)
{
  var toolCallResult = await response.ToolCall.InvokeAsync<string>();
  string toolResultContent;

  if (toolCallResult.IsSuccess && toolCallResult.Value is not null)
  {
    toolResultContent = toolCallResult.Value;
  }
  else
  {
    toolResultContent = toolCallResult.Error.Message;
  }

  messages.Add(
    new(
      MessageRole.User, 
      [
        new ToolResultContent(
          response.ToolCall.ToolUse.Id, 
          toolResultContent
        )
      ]
    )
  );
}

var finalResponse = await client.CreateMessageAsync(new MessageRequest(
  AnthropicModels.Claude3Haiku,
  messages,
  tools: tools
));

if (finalResponse.IsSuccess is false)
{
  Console.WriteLine("Failed to create message");
  Console.WriteLine("Error Type: {0}", finalResponse.Error.Error.Type);
  Console.WriteLine("Error Message: {0}", finalResponse.Error.Error.Message);
  return;
}

foreach (var content in finalResponse.Value.Content)
{
  switch (content)
  {
    case TextContent textContent:
      Console.WriteLine(textContent.Text);
      break;
  }
}
```

If you do find that you need more control over how exactly provided tools are called and how the result of those tools are returned you can avoid using the `InvokeAsync` method and instead use the `Tool` and `ToolUse` properties of the `ToolCall` instance to implement your own solution.

### System Prompt

Anthropic's models support the use of system prompts to provide additional context to the user. This can be used to provide additional information to the user or to ask for additional information from the user. This feature is covered in depth in [Anthropic's API Documentation](https://docs.anthropic.com/en/docs/build-with-claude/prompt-engineering/system-prompts). This library aims to make using system prompts convenient by allowing you to provide the system prompts you want Anthropic's models to consider for use when creating a message.

#### System Message

You can create a system prompt by providing a `string` as the `system` parameter in the `MessageRequest` or `StreamMessageRequest` constructor.

```csharp
using AnthropicClient;
using AnthropicClient.Models;

var response = await client.CreateMessageAsync(new MessageRequest(
  AnthropicModels.Claude3Haiku,
  [
    new(
      MessageRole.User, 
      [new TextContent("Please write a haiku about the ocean.")]
    )
  ],
  system: "You are a internationally renowned poet. You excel at writing haikus.
));

if (response.IsSuccess is false)
{
  Console.WriteLine("Failed to create message");
  Console.WriteLine("Error Type: {0}", response.Error.Error.Type);
  Console.WriteLine("Error Message: {0}", response.Error.Error.Message);
  return;
}

foreach (var content in response.Value.Content)
{
  switch (content)
  {
    case TextContent textContent:
      Console.WriteLine(textContent.Text);
      break;
  }
}
```

#### System Messages

You can create a more complex system prompt by providing a `List<TextContent>` as the `systemMessages` parameter in the `MessageRequest` or `StreamMessageRequest` constructor.

```csharp
using AnthropicClient;
using AnthropicClient.Models;

var response = await client.CreateMessageAsync(new MessageRequest(
  AnthropicModels.Claude3Haiku,
  [
    new(
      MessageRole.User, 
      [new TextContent("Please write a haiku about the ocean.")]
    )
  ],
  systemMessages: [
    new TextContent("You are a internationally renowned poet. You excel at writing haikus."),
    new TextContent("You have been asked to write a haiku about the ocean.")
  ]
));

if (response.IsSuccess is false)
{
  Console.WriteLine("Failed to create message");
  Console.WriteLine("Error Type: {0}", response.Error.Error.Type);
  Console.WriteLine("Error Message: {0}", response.Error.Error.Message);
  return;
}

foreach (var content in response.Value.Content)
{
  switch (content)
  {
    case TextContent textContent:
      Console.WriteLine(textContent.Text);
      break;
  }
}
```

### Prompt Caching

Anthropic provides a feature called [Prompt Caching](https://docs.anthropic.com/en/docs/build-with-claude/prompt-caching) that allows you to cache all or part of the prompt you send to the model. This can be used to improve the performance of your application by reducing latency and token usage. This feature is covered in depth in [Anthropic's API Documentation](https://docs.anthropic.com/en/docs/build-with-claude/prompt-caching).

Prompt caching can be used to cache all parts of the prompt including system messages, user messages, and tools. You should refer to the [Anthropic API Documentation](https://docs.anthropic.com/en/docs/build-with-claude/prompt-caching) for specifics on limitations and requirements for using prompt caching. This library aims to make using prompt caching convenient and give you complete control over what parts of the prompt are cached. Currently there is only one type of cache control available - `EphemeralCacheControl`.

#### Caching System Messages

System messages can be cached by providing a `List<TextContent>` as the `systemMessages` parameter in the `MessageRequest` or `StreamMessageRequest` constructor and having one or more of the `TextContent` instances have the `CacheControl` property set.

```csharp
using AnthropicClient;
using AnthropicClient.Models;

var response = await client.CreateMessageAsync(new MessageRequest(
  AnthropicModels.Claude3Haiku,
  [
    new(
      MessageRole.User, 
      [new TextContent("Please write a haiku about the ocean.")]
    )
  ],
  systemMessages: [
    new TextContent("You are a internationally renowned poet. You excel at writing haikus. Please use the following as examples."),
    new TextContent(exampleHaikus, new EphemeralCacheControl())
  ]
));

if (response.IsSuccess is false)
{
  Console.WriteLine("Failed to create message");
  Console.WriteLine("Error Type: {0}", response.Error.Error.Type);
  Console.WriteLine("Error Message: {0}", response.Error.Error.Message);
  return;
}

foreach (var content in response.Value.Content)
{
  switch (content)
  {
    case TextContent textContent:
      Console.WriteLine(textContent.Text);
      break;
  }
}
```

#### Caching User Messages

User messages can be cached by providing a `List<Content>` as the `messages` parameter in the `MessageRequest` or `StreamMessageRequest` constructor and having one or more of the `Content` instances have the `CacheControl` property set.

```csharp
using AnthropicClient;
using AnthropicClient.Models;

var response = await client.CreateMessageAsync(new MessageRequest(
  AnthropicModels.Claude3Haiku,
  [
    new(
      MessageRole.User, 
      [
        new TextContent("Please write a haiku about the ocean. Here are some examples of haikus I like."),
        new TextContent(exampleHaikus, new EphemeralCacheControl())
      ]
    ),
  ]
));

if (response.IsSuccess is false)
{
  Console.WriteLine("Failed to create message");
  Console.WriteLine("Error Type: {0}", response.Error.Error.Type);
  Console.WriteLine("Error Message: {0}", response.Error.Error.Message);
  return;
}

foreach (var content in response.Value.Content)
{
  switch (content)
  {
    case TextContent textContent:
      Console.WriteLine(textContent.Text);
      break;
  }
}
```

#### Caching Tools

Tools can be cached by providing a `List<Tool>` as the `tools` parameter in the `MessageRequest` or `StreamMessageRequest` constructor and having one or more of the `Tool` instances have the `CacheControl` property set. This property can be set after the tool is created manually or by using one of the static methods on the `Tool` class.

```csharp
using AnthropicClient;
using AnthropicClient.Models;

var tool = (string location, string units) => $"The weather in {location} is 72 degrees {units}";

var getWeatherTool = Tool.CreateFromFunction(
  "Get Weather", 
  "Get the weather for a location in the specified units", 
  tool,
  new EphemeralCacheControl()
);

var response = await client.CreateMessageAsync(new MessageRequest(
  AnthropicModels.Claude3Haiku,
  [
    new(
      MessageRole.User, 
      [new TextContent("What is the weather in New York?")]
    )
  ],
  tools: [
    // Lots of other tools
    // ...
    getWeatherTool
  ]
));

if (response.IsSuccess is false)
{
  Console.WriteLine("Failed to create message");
  Console.WriteLine("Error Type: {0}", response.Error.Error.Type);
  Console.WriteLine("Error Message: {0}", response.Error.Error.Message);
  return;
}

foreach (var content in response.Value.Content)
{
  switch (content)
  {
    case TextContent textContent:
      Console.WriteLine(textContent.Text);
      break;
    case ToolUseContent toolUseContent:
      Console.WriteLine(toolUseContent.Name);
      break;
  }
}
```

### PDF Support

Anthropic provides a feature called [PDF Support](https://docs.anthropic.com/en/docs/build-with-claude/pdf-support) that allows Claude to support PDF input and understand both text and visual content within documents. This feature is covered in depth in [Anthropic's API Documentation](https://docs.anthropic.com/en/docs/build-with-claude/pdf-support).

PDF support can be used to provide a PDF document as input to the model. This can be used to provide additional context to the model or to ask for additional information from the model. This library aims to make using PDF support convenient by allowing you to provide the PDF document you want Anthropic's models to consider for use when creating a message.

#### PDF Document

You can provide a PDF document by providing its base64 encoded content as a `DocumentContent` instance in the list of messages in the `MessageRequest` or `StreamMessageRequest` constructor.

```csharp
using AnthropicClient;
using AnthropicClient.Models;

var request = new MessageRequest(
  model: AnthropicModels.Claude35Sonnet,
  messages: [
    new(MessageRole.User, [new TextContent("What is the title of this paper?")]),
    new(MessageRole.User, [new DocumentContent("application/pdf", base64Data)])
  ]
);

var response = await client.CreateMessageAsync(request);

if (response.IsSuccess is false)
{
  Console.WriteLine("Failed to create message");
  Console.WriteLine("Error Type: {0}", response.Error.Error.Type);
  Console.WriteLine("Error Message: {0}", response.Error.Error.Message);
  return;
}

foreach (var content in response.Value.Content)
{
  switch (content)
  {
    case TextContent textContent:
      Console.WriteLine(textContent.Text);
      break;
  }
}
```

### Citations

Anthropic provides a feature called [Citations](https://docs.anthropic.com/en/docs/build-with-claude/citations) that allows Claude to provide citations for information extracted from documents. This feature enables Claude to reference specific parts of the source material when answering questions, making it easier to verify information and understand the context of responses.

Citations can be enabled for documents and will return references to the specific locations in the source material where information was found. This library provides comprehensive support for citations through strongly-typed models that represent different types of citation locations.

#### Enabling Citations for Documents

You can enable citations for documents by setting the `Citations` property on `DocumentContent` instances:

```csharp
using AnthropicClient;
using AnthropicClient.Models;

var request = new MessageRequest(
  model: AnthropicModels.Claude35Sonnet,
  messages: [
    new(MessageRole.User, [
      new DocumentContent(new TextSource("The grass is green. The sky is blue."))
      {
        Title = "My Document",
        Context = "This is a trustworthy document.",
        Citations = new() { Enabled = true }
      },
      new TextContent("What color is the grass and sky?")
    ])
  ]
);

var response = await client.CreateMessageAsync(request);

if (response.IsSuccess is false)
{
  Console.WriteLine("Failed to create message");
  Console.WriteLine("Error Type: {0}", response.Error.Error.Type);
  Console.WriteLine("Error Message: {0}", response.Error.Error.Message);
  return;
}

foreach (var content in response.Value.Content)
{
  switch (content)
  {
    case TextContent textContent:
      Console.WriteLine("Response: {0}", textContent.Text);
      
      if (textContent.Citations is not null)
      {
        Console.WriteLine("Citations:");
        foreach (var citation in textContent.Citations)
        {
          Console.WriteLine("  - Cited Text: {0}", citation.CitedText);
          Console.WriteLine("    Document: {0}", citation.DocumentTitle);
          Console.WriteLine("    Type: {0}", citation.Type);
          
          switch (citation)
          {
            case CharacterLocationCitation charCitation:
              Console.WriteLine(
                "    Character Range: {0}-{1}", 
                charCitation.StartCharIndex, charCitation.EndCharIndex
              );
              break;
            case PageLocationCitation pageCitation:
              Console.WriteLine(
                "    Page Range: {0}-{1}", 
                pageCitation.StartPageNumber, pageCitation.EndPageNumber
              );
              break;
            case ContentBlockLocationCitation blockCitation:
              Console.WriteLine(
                "    Block Range: {0}-{1}", 
                blockCitation.StartBlockIndex, blockCitation.EndBlockIndex
              );
              break;
          }
        }
      }
      break;
  }
}
```

#### Citations with PDF Documents

Citations work particularly well with PDF documents, providing page-level references:

```csharp
using AnthropicClient;
using AnthropicClient.Models;

var pdfBytes = await File.ReadAllBytesAsync("document.pdf");
var base64Data = Convert.ToBase64String(pdfBytes);

var request = new MessageRequest(
  model: AnthropicModels.Claude35Sonnet,
  messages: [
    new(MessageRole.User, [
      new DocumentContent("application/pdf", base64Data)
      {
        Title = "Research Paper",
        Citations = new() { Enabled = true }
      },
      new TextContent("Summarize the key findings from this research paper.")
    ])
  ]
);

var response = await client.CreateMessageAsync(request);

if (response.IsSuccess is false)
{
  Console.WriteLine("Failed to create message");
  Console.WriteLine("Error Type: {0}", response.Error.Error.Type);
  Console.WriteLine("Error Message: {0}", response.Error.Error.Message);
  return;
}

foreach (var content in response.Value.Content)
{
  switch (content)
  {
    case TextContent textContent:
      Console.WriteLine("Summary: {0}", textContent.Text);
      
      if (textContent.Citations is not null)
      {
        Console.WriteLine("\nCitations:");
        foreach (var citation in textContent.Citations.OfType<PageLocationCitation>())
        {
          Console.WriteLine(
            "  - \"{0}\" (Pages {1}-{2})", 
            citation.CitedText, 
            citation.StartPageNumber, 
            citation.EndPageNumber
          );
        }
      }
      break;
  }
}
```

#### Citations in Streaming Responses

Citations are also supported in streaming responses through the `CitationDelta` events:

```csharp
using AnthropicClient;
using AnthropicClient.Models;

var request = new StreamMessageRequest(
  model: AnthropicModels.Claude35Sonnet,
  messages: [
    new(MessageRole.User, [
      new DocumentContent(new TextSource("The grass is green. The sky is blue."))
      {
        Citations = new() { Enabled = true }
      },
      new TextContent("What color is the grass?")
    ])
  ]
);

var events = client.CreateMessageAsync(request);

await foreach (var e in events)
{
  switch (e.Data)
  {
    case ContentDeltaEventData contentData:
      switch (contentData.Delta)
      {
        case CitationDelta citationDelta:
          Console.WriteLine("Citation: {0}", citationDelta.Citation.CitedText);
          Console.WriteLine("Type: {0}", citationDelta.Citation.Type);
          break;
        case TextDelta textDelta:
          Console.Write(textDelta.Text);
          break;
      }
      break;
  }
}
```

### Message Batches

Anthropic provides a feature called [Message Batches](https://docs.anthropic.com/en/docs/build-with-claude/message-batches) that allows you to send multiple messages in a single request. This feature is covered in depth in [Anthropic's API Documentation](https://docs.anthropic.com/en/docs/build-with-claude/message-batches).

#### Create a message batch

You can create a message batch that will consist of one or more requests to create messages.

```csharp
using AnthropicClient;
using AnthropicClient.Models;

var request = new MessageBatchRequest([
  new(
    Guid.NewGuid().ToString(),
    new(
      model: AnthropicModels.Claude3Haiku,
      messages: [new(MessageRole.User, [new TextContent("Hello!")])]
    )
  ),
]);

var response = await client.CreateMessageBatchAsync(request);

if (response.IsFailure)
{
  Console.WriteLine("Failed to create message batch");
  Console.WriteLine("Error Type: {0}", response.Error.Error.Type);
  Console.WriteLine("Error Message: {0}", response.Error.Error.Message);
  return;
}

Console.WriteLine("Message Batch Id: {0}", response.Value.Id);
```

#### Get a message batch

You can retrieve a message batch by its id.

```csharp
using AnthropicClient;
using AnthropicClient.Models;

var response = await client.GetMessageBatchAsync("batch-id");

if (response.IsFailure)
{
  Console.WriteLine("Failed to get message batch");
  Console.WriteLine("Error Type: {0}", response.Error.Error.Type);
  Console.WriteLine("Error Message: {0}", response.Error.Error.Message);
  return;
}

Console.WriteLine("Message Batch Id: {0}", response.Value.Id);
```

#### Get a message batch results

You can retrieve the results of a message batch by its id. The results are returned as an `IAsyncEnumerable` collection so that they can be streamed and processed as they are received.

```csharp
using AnthropicClient;
using AnthropicClient.Models;

var response = await client.GetMessageBatchResultsAsync("batch-id");

if (response.IsFailure)
{
  Console.WriteLine("Failed to get message batch results");
  Console.WriteLine("Error Type: {0}", response.Error.Error.Type);
  Console.WriteLine("Error Message: {0}", response.Error.Error.Message);
  return;
}

await foreach (var item in response.Value)
{
  Console.WriteLine("Item Custom Id: {0}", result.CustomId);
  
  switch (item.Result)
  {
    case SucceededMessageBatchResult successResult:
      foreach (var content in successResult.Message.Content)
      {
        if (content is TextContent textContent)
        {
          Console.WriteLine("Message Batch Result: {0}", textContent.Text);
        }
      }
      break;
    default:
      Console.WriteLine("Message Batch Result: {0}", item.Result.Type);
      break;
  }
}
```

#### List message batches

You can retrieve a page of message batches.

```csharp
using AnthropicClient;
using AnthropicClient.Models;

var response = await client.ListMessageBatchesAsync();

if (response.IsFailure)
{
  Console.WriteLine("Failed to list message batches");
  Console.WriteLine("Error Type: {0}", response.Error.Error.Type);
  Console.WriteLine("Error Message: {0}", response.Error.Error.Message);
  return;
}

foreach (var batch in response.Value.Data)
{
  Console.WriteLine("Message Batch Id: {0}", batch.Id);
}
```

#### List all message batches

You can also retrieve all the pages of message batches without having to implement pagination yourself. This is done by returning an `IAsyncEnumerable` collection that can be streamed and processed as the pages are received.

```csharp
using AnthropicClient;
using AnthropicClient.Models;

var pageResponses = client.ListAllMessageBatchesAsync();

await foreach (var response in pageResponses)
{
  if (response.IsFailure)
  {
    Console.WriteLine("Failed to list message batches");
    Console.WriteLine("Error Type: {0}", response.Error.Error.Type);
    Console.WriteLine("Error Message: {0}", response.Error.Error.Message);
    return;
  }

  foreach (var batch in response.Value.Data)
  {
    Console.WriteLine("Message Batch Id: {0}", batch.Id);
  }
}
```

#### Cancel a message batch

You can cancel a message batch by its id.

```csharp
using AnthropicClient;
using AnthropicClient.Models;

var response = await client.CancelMessageBatchAsync("batch-id");

if (response.IsFailure)
{
  Console.WriteLine("Failed to cancel message batch");
  Console.WriteLine("Error Type: {0}", response.Error.Error.Type);
  Console.WriteLine("Error Message: {0}", response.Error.Error.Message);
  return;
}

Console.WriteLine("Message Batch Id: {0}", response.Value.Id);
Console.WriteLine("Message Batch Status: {0}", response.Value.ProcessingStatus);
```

#### Delete a message batch

You can delete a message batch that is no longer being processed by its id.

```csharp
using AnthropicClient;
using AnthropicClient.Models;

var response = await client.DeleteMessageBatchAsync("batch-id");

if (response.IsFailure)
{
  Console.WriteLine("Failed to delete message batch");
  Console.WriteLine("Error Type: {0}", response.Error.Error.Type);
  Console.WriteLine("Error Message: {0}", response.Error.Error.Message);
  return;
}

Console.WriteLine("Message Batch Id: {0}", response.Value.Id);
```
