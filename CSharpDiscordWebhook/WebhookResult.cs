using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CSharpDiscordWebhook;

/// <summary>
/// Webhook API result wrapper
/// </summary>
/// <typeparam name="T">Result type</typeparam>
public class WebhookResult<T>
{
    internal WebhookResult(T? value, ErrorMessage? error)
    {
        Result = value;
        Error = error;
        Success = Error == null;
    }
    
    /// <summary>
    /// API call output object
    /// </summary>
    public T? Result { get; }
    /// <summary>
    /// Error information
    /// </summary>
    public ErrorMessage? Error { get; }
    /// <summary>
    /// API call result
    /// </summary>
    public bool Success { get; }
}

/// <summary>
/// Discord error information
/// </summary>
public class ErrorMessage
{
    /// <summary>
    /// Discord API error code
    /// </summary>
    [JsonInclude] public int Code { get; private set; }
    /// <summary>
    /// Discord API error message
    /// </summary>
    [JsonInclude] public string Message { get; private set; } = string.Empty;
    /// <summary>
    /// Discord API error details
    /// </summary>
    [JsonInclude] public Dictionary<string, string> Errors { get; private set; } = new();
}