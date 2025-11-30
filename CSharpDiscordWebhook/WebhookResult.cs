using System.Collections.Generic;

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
    public int Code { get; private set; }
    public string Message { get; private set; } = string.Empty;
    public Dictionary<string, object> Errors { get; private set; } = new();
}