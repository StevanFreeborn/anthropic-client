namespace AnthropicClient.Models;

public class TextDocumentSource : DocumentSource
{
  public TextDocumentSource(string data) : base("text/plain", data)
  {
    Type = "text";
  }
}


