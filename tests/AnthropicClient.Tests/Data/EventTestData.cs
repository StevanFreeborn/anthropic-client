namespace AnthropicClient.Tests.Data;

public class EventTestData : IEnumerable<object[]>
{
  public static MemoryStream GetEventStream()
  {
    var events = new EventTestData().Select(x => x[0].ToString())!;
    var content = string.Join("\n\n", events);
    return new MemoryStream(Encoding.UTF8.GetBytes(content));
  }
  public static List<AnthropicEvent> GetAllEvents() => new EventTestData().Select(x => x[1] as AnthropicEvent).Append(MessageCompleteEvent).ToList()!;

  public static readonly AnthropicEvent MessageCompleteEvent = new()
  {
    Type = EventType.MessageComplete,
    Data = new MessageCompleteEventData(
      new()
      {
        Id = "msg_014p7gG3wDgGV9EUtLvnow3U",
        Type = "message",
        Role = MessageRole.Assistant,
        Model = AnthropicModels.Claude3Haiku,
        StopSequence = null,
        Usage = new Usage { InputTokens = 472, OutputTokens = 91 },
        StopReason = "tool_use",
        Content = [
          new TextContent("Okay, let's check the weather for San Francisco, CA:")
          {
            Citations = [
              new CharacterLocationCitation(),
              new CharacterLocationCitation(),
            ]
          },
          new ToolUseContent()
          {
            Id = "toolu_01T1x1fJ34qAmk2tNTrN7Up6",
            Name = "get_weather",
            Input = new Dictionary<string, object?>
            {
              { "location", "San Francisco, CA" },
              { "unit", "fahrenheit" }
            },
          },
        ]
      },
      new()
    )
  };

  public IEnumerator<object[]> GetEnumerator()
  {
    yield return new object[]
    {
      """
      event: message_start
      data: {"type":"message_start","message":{"id":"msg_014p7gG3wDgGV9EUtLvnow3U","type":"message","role":"assistant","model":"claude-3-haiku-20240307","stop_sequence":null,"usage":{"input_tokens":472,"output_tokens":2},"content":[],"stop_reason":null}}
      """,
      new AnthropicEvent()
      {
        Type = EventType.MessageStart,
        Data = new MessageStartEventData()
        {
          Message = new MessageResponse()
          {
            Id = "msg_014p7gG3wDgGV9EUtLvnow3U",
            Type = "message",
            Role = "assistant",
            Model = "claude-3-haiku-20240307",
            StopSequence = null,
            Usage = new Usage()
            {
              InputTokens = 472,
              OutputTokens = 2,
            },
            Content = [],
            StopReason = null,
          },
        },
      },
    };

    yield return new object[]
    {
      """
      event: content_block_start
      data: {"type":"content_block_start","index":0,"content_block":{"type":"text","text":""}}
      """,
      new AnthropicEvent()
      {
        Type = EventType.ContentBlockStart,
        Data = new ContentStartEventData()
        {
          Index = 0,
          ContentBlock = new TextContent()
          {
            Type = "text",
            Text = "",
          },
        },
      },
    };

    yield return new object[]
    {
      """
      event: ping
      data: {"type": "ping"}
      """,
      new AnthropicEvent()
      {
        Type = EventType.Ping,
        Data = new PingEventData(),
      },
    };

    yield return new object[]
    {
      """
      event: content_block_delta
      data: {"type":"content_block_delta","index":0,"delta":{"type":"text_delta","text":"Okay"}}
      """,
      new AnthropicEvent()
      {
        Type = EventType.ContentBlockDelta,
        Data = new ContentDeltaEventData()
        {
          Index = 0,
          Delta = new TextDelta()
          {
            Type = "text_delta",
            Text = "Okay",
          },
        },
      },
    };

    yield return new object[]
    {
      """
      event: content_block_delta
      data: {"type":"content_block_delta","index":0,"delta":{"type":"text_delta","text":","}}
      """,
      new AnthropicEvent()
      {
        Type = EventType.ContentBlockDelta,
        Data = new ContentDeltaEventData()
        {
          Index = 0,
          Delta = new TextDelta()
          {
            Type = "text_delta",
            Text = ",",
          },
        },
      },
    };

    yield return new object[]
    {
      """
      event: content_block_delta
      data: {"type":"content_block_delta","index":0,"delta":{"type":"text_delta","text":" let"}}
      """,
      new AnthropicEvent()
      {
        Type = EventType.ContentBlockDelta,
        Data = new ContentDeltaEventData()
        {
          Index = 0,
          Delta = new TextDelta()
          {
            Type = "text_delta",
            Text = " let",
          },
        },
      },
    };

    yield return new object[]
    {
      """
      event: content_block_delta
      data: {"type":"content_block_delta","index":0,"delta":{"type":"text_delta","text":"'s"}}
      """,
      new AnthropicEvent()
      {
        Type = EventType.ContentBlockDelta,
        Data = new ContentDeltaEventData()
        {
          Index = 0,
          Delta = new TextDelta()
          {
            Type = "text_delta",
            Text = "'s",
          },
        },
      },
    };

    yield return new object[]
    {
      """
      event: content_block_delta
      data: {"type":"content_block_delta","index":0,"delta":{"type":"text_delta","text":" check"}}
      """,
      new AnthropicEvent()
      {
        Type = EventType.ContentBlockDelta,
        Data = new ContentDeltaEventData()
        {
          Index = 0,
          Delta = new TextDelta()
          {
            Type = "text_delta",
            Text = " check",
          },
        },
      },
    };

    yield return new object[]
    {
      """
      event: content_block_delta
      data: {"type":"content_block_delta","index":0,"delta":{"type":"text_delta","text":" the"}}
      """,
      new AnthropicEvent()
      {
        Type = EventType.ContentBlockDelta,
        Data = new ContentDeltaEventData()
        {
          Index = 0,
          Delta = new TextDelta()
          {
            Type = "text_delta",
            Text = " the",
          },
        },
      },
    };

    yield return new object[]
    {
      """
      event: content_block_delta
      data: {"type":"content_block_delta","index":0,"delta":{"type":"text_delta","text":" weather"}}
      """,
      new AnthropicEvent()
      {
        Type = EventType.ContentBlockDelta,
        Data = new ContentDeltaEventData()
        {
          Index = 0,
          Delta = new TextDelta()
          {
            Type = "text_delta",
            Text = " weather",
          },
        },
      },
    };

    yield return new object[]
    {
      """
      event: content_block_delta
      data: {"type":"content_block_delta","index":0,"delta":{"type":"text_delta","text":" for"}}
      """,
      new AnthropicEvent()
      {
        Type = EventType.ContentBlockDelta,
        Data = new ContentDeltaEventData()
        {
          Index = 0,
          Delta = new TextDelta()
          {
            Type = "text_delta",
            Text = " for",
          },
        },
      },
    };

    yield return new object[]
    {
      """
      event: content_block_delta
      data: {"type":"content_block_delta","index":0,"delta":{"type":"text_delta","text":" San"}}
      """,
      new AnthropicEvent()
      {
        Type = EventType.ContentBlockDelta,
        Data = new ContentDeltaEventData()
        {
          Index = 0,
          Delta = new TextDelta()
          {
            Type = "text_delta",
            Text = " San",
          },
        },
      },
    };

    yield return new object[]
    {
      """
      event: content_block_delta
      data: {"type":"content_block_delta","index":0,"delta":{"type":"text_delta","text":" Francisco"}}
      """,
      new AnthropicEvent()
      {
        Type = EventType.ContentBlockDelta,
        Data = new ContentDeltaEventData()
        {
          Index = 0,
          Delta = new TextDelta()
          {
            Type = "text_delta",
            Text = " Francisco",
          },
        },
      },
    };

    yield return new object[]
    {
      """
      event: content_block_delta
      data: {"type":"content_block_delta","index":0,"delta":{"type":"text_delta","text":","}}
      """,
      new AnthropicEvent()
      {
        Type = EventType.ContentBlockDelta,
        Data = new ContentDeltaEventData()
        {
          Index = 0,
          Delta = new TextDelta()
          {
            Type = "text_delta",
            Text = " ,",
          },
        },
      },
    };

    yield return new object[]
    {
      """
      event: content_block_delta
      data: {"type":"content_block_delta","index":0,"delta":{"type":"text_delta","text":" CA"}}
      """,
      new AnthropicEvent()
      {
        Type = EventType.ContentBlockDelta,
        Data = new ContentDeltaEventData()
        {
          Index = 0,
          Delta = new TextDelta()
          {
            Type = "text_delta",
            Text = " CA",
          },
        },
      },
    };

    yield return new object[]
    {
      """
      event: content_block_delta
      data: {"type":"content_block_delta","index":0,"delta":{"type":"text_delta","text":":"}}
      """,
      new AnthropicEvent()
      {
        Type = EventType.ContentBlockDelta,
        Data = new ContentDeltaEventData()
        {
          Index = 0,
          Delta = new TextDelta()
          {
            Type = "text_delta",
            Text = " :",
          },
        },
      },
    };

    yield return new object[]
    {
      """
      event: content_block_delta
      data: {"type":"content_block_delta","index":0,"delta":{"type":"citations_delta","citation": {"type":"char_location","start_char_index":0,"end_char_index":0, "cited_text":"","document_index":0,"document_title":""}}}
      """,
      new AnthropicEvent()
      {
        Type = EventType.ContentBlockDelta,
        Data = new ContentDeltaEventData()
        {
          Index = 0,
          Delta = new CitationDelta()
          {
            Citation = new CharacterLocationCitation()
          }
        },
      },
    };

    yield return new object[]
    {
      """
      event: content_block_delta
      data: {"type":"content_block_delta","index":0,"delta":{"type":"citations_delta","citation":{"type":"char_location","start_char_index":0,"end_char_index":0,"cited_text":"","document_index":0,"document_title":""}}}
      """,
      new AnthropicEvent()
      {
        Type = EventType.ContentBlockDelta,
        Data = new ContentDeltaEventData()
        {
          Index = 0,
          Delta = new CitationDelta()
          {
            Citation = new CharacterLocationCitation()
          },
        },
      },
    };

    yield return new object[]
    {
      """
      event: content_block_stop
      data: {"type":"content_block_stop","index":0}
      """,
      new AnthropicEvent()
      {
        Type = EventType.ContentBlockStop,
        Data = new ContentStopEventData()
        {
          Index = 0,
        },
      },
    };

    yield return new object[]
    {
      """
      event: content_block_start
      data: {"type":"content_block_start","index":1,"content_block":{"type":"tool_use","id":"toolu_01T1x1fJ34qAmk2tNTrN7Up6","name":"get_weather","input":{}}}
      """,
      new AnthropicEvent()
      {
        Type = EventType.ContentBlockStart,
        Data = new ContentStartEventData()
        {
          Index = 1,
          ContentBlock = new ToolUseContent()
          {
            Type = "tool_use",
            Id = "toolu_01T1x1fJ34qAmk2tNTrN7Up6",
            Name = "get_weather",
            Input = [],
          },
        },
      },
    };

    yield return new object[]
    {
      """
      event: content_block_delta
      data: {"type":"content_block_delta","index":1,"delta":{"type":"input_json_delta","partial_json":""}}
      """,
      new AnthropicEvent()
      {
        Type = EventType.ContentBlockDelta,
        Data = new ContentDeltaEventData()
        {
          Index = 1,
          Delta = new JsonDelta()
          {
            Type = "input_json_delta",
            PartialJson = "",
          },
        },
      },
    };

    yield return new object[]
    {
      """
      event: content_block_delta
      data: {"type":"content_block_delta","index":1,"delta":{"type":"input_json_delta","partial_json":"{\"location\":"}}
      """,
      new AnthropicEvent()
      {
        Type = EventType.ContentBlockDelta,
        Data = new ContentDeltaEventData()
        {
          Index = 1,
          Delta = new JsonDelta()
          {
            Type = "input_json_delta",
            PartialJson = "{\"location\":",
          },
        },
      },
    };

    yield return new object[]
    {
      """
      event: content_block_delta
      data: {"type":"content_block_delta","index":1,"delta":{"type":"input_json_delta","partial_json":" \"San"}}
      """,
      new AnthropicEvent()
      {
        Type = EventType.ContentBlockDelta,
        Data = new ContentDeltaEventData()
        {
          Index = 1,
          Delta = new JsonDelta()
          {
            Type = "input_json_delta",
            PartialJson = " \"San",
          },
        },
      },
    };

    yield return new object[]
    {
      """
      event: content_block_delta
      data: {"type":"content_block_delta","index":1,"delta":{"type":"input_json_delta","partial_json":" Francisc"}}
      """,
      new AnthropicEvent()
      {
        Type = EventType.ContentBlockDelta,
        Data = new ContentDeltaEventData()
        {
          Index = 1,
          Delta = new JsonDelta()
          {
            Type = "input_json_delta",
            PartialJson = " Francisc",
          },
        },
      },
    };

    yield return new object[]
    {
      """
      event: content_block_delta
      data: {"type":"content_block_delta","index":1,"delta":{"type":"input_json_delta","partial_json":"o,"}}
      """,
      new AnthropicEvent()
      {
        Type = EventType.ContentBlockDelta,
        Data = new ContentDeltaEventData()
        {
          Index = 1,
          Delta = new JsonDelta()
          {
            Type = "input_json_delta",
            PartialJson = "o,",
          },
        },
      },
    };

    yield return new object[]
    {
      """
      event: content_block_delta
      data: {"type":"content_block_delta","index":1,"delta":{"type":"input_json_delta","partial_json":" CA\""}}
      """,
      new AnthropicEvent()
      {
        Type = EventType.ContentBlockDelta,
        Data = new ContentDeltaEventData()
        {
          Index = 1,
          Delta = new JsonDelta()
          {
            Type = "input_json_delta",
            PartialJson = " CA\"",
          },
        },
      },
    };

    yield return new object[]
    {
      """
      event: content_block_delta
      data: {"type":"content_block_delta","index":1,"delta":{"type":"input_json_delta","partial_json":", "}}
      """,
      new AnthropicEvent()
      {
        Type = EventType.ContentBlockDelta,
        Data = new ContentDeltaEventData()
        {
          Index = 1,
          Delta = new JsonDelta()
          {
            Type = "input_json_delta",
            PartialJson = ", ",
          },
        },
      },
    };

    yield return new object[]
    {
      """
      event: content_block_delta
      data: {"type":"content_block_delta","index":1,"delta":{"type":"input_json_delta","partial_json":"\"unit\": \"fah"}}
      """,
      new AnthropicEvent()
      {
        Type = EventType.ContentBlockDelta,
        Data = new ContentDeltaEventData()
        {
          Index = 1,
          Delta = new JsonDelta()
          {
            Type = "input_json_delta",
            PartialJson = "\"unit\": \"fah",
          },
        },
      },
    };

    yield return new object[]
    {
      """
      event: content_block_delta
      data: {"type":"content_block_delta","index":1,"delta":{"type":"input_json_delta","partial_json":"renheit\"}"}}
      """,
      new AnthropicEvent()
      {
        Type = EventType.ContentBlockDelta,
        Data = new ContentDeltaEventData()
        {
          Index = 1,
          Delta = new JsonDelta()
          {
            Type = "input_json_delta",
            PartialJson = "renheit\"}",
          },
        },
      },
    };

    yield return new object[]
    {
      """
      event: content_block_stop
      data: {"type":"content_block_stop","index":1}
      """,
      new AnthropicEvent()
      {
        Type = EventType.ContentBlockStop,
        Data = new ContentStopEventData()
        {
          Index = 1,
        },
      },
    };

    yield return new object[]
    {
      """
      event: message_delta
      data: {"type":"message_delta","delta":{"stop_reason":"tool_use","stop_sequence":null},"usage":{"output_tokens":89}}
      """,
      new AnthropicEvent()
      {
        Type = EventType.MessageDelta,
        Data = new MessageDeltaEventData()
        {
          Delta = new MessageDelta()
          {
            StopReason = "tool_use",
            StopSequence = null,
          },
          Usage = new Usage()
          {
            OutputTokens = 89,
          },
        },
      }
    };

    yield return new object[]
    {
      """
      event: message_stop
      data: {"type":"message_stop"}
      """,
      new AnthropicEvent()
      {
        Type = EventType.MessageStop,
        Data = new MessageStopEventData(),
      },
    };
  }

  IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}