using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CSharpDiscordWebhook;

public class WebhookResult<T>
{
    internal WebhookResult(T? value, ErrorMessage? error)
    {
        Result = value;
        Error = error;
        Success = Error == null;
    }
    
    public T? Result { get; }
    public ErrorMessage? Error { get; }
    public bool Success { get; }
}

public class ErrorMessage
{
    [JsonInclude] public int Code { get; private set; }
    [JsonInclude] public string Message { get; private set; } = string.Empty;
    [JsonInclude] public Dictionary<string, string> Errors { get; private set; } = new();
}