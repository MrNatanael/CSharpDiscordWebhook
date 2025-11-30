using System;
using System.Text.Json.Serialization;
using CSharpDiscordWebhook.Json;

namespace CSharpDiscordWebhook.Objects;

/// <summary>
/// Message attachment info
/// </summary>
public class Attachment
{
    /// <summary>
    /// Attachment id
    /// </summary>
    [JsonInclude] public ulong Id { get; private set; }
    /// <summary>
    /// Name of file attached
    /// </summary>
    [JsonInclude] public string Filename { get; private set; } = string.Empty;
    /// <summary>
    /// The title of the file
    /// </summary>
    [JsonInclude] public string? Title { get; private set; }
    /// <summary>
    /// Description for the file (max 1024 characters)
    /// </summary>
    [JsonInclude] public string? Description { get; private set; }
    /// <summary>
    /// The attachment's media type, see https://en.wikipedia.org/wiki/Media_type
    /// </summary>
    [JsonInclude] public string? ContentType { get; private set; }
    /// <summary>
    /// Size of file in bytes
    /// </summary>
    [JsonInclude] public ulong Size { get; private set; }
    /// <summary>
    /// Source url of file
    /// </summary>
    [JsonInclude] public string Url { get; private set; } = string.Empty;
    /// <summary>
    /// A proxied url of file
    /// </summary>
    [JsonInclude] public string ProxyUrl { get; private set; } = string.Empty;
    /// <summary>
    /// Width of file (if image)
    /// </summary>
    [JsonInclude] public uint? Width { get; private set; }
    /// <summary>
    /// Height of file (if image)
    /// </summary>
    [JsonInclude] public uint? Height { get; private set; }
    /// <summary>
    /// Whether this attachment is ephemeral
    /// </summary>
    [JsonInclude] public bool? Ephemeral { get; private set; }

    /// <summary>
    /// The duration of the audio file (currently for voice messages)
    /// </summary>
    [JsonInclude]
    [JsonConverter(typeof(TimeSpanSecondsJsonSerializer))]
    [JsonPropertyName("duration_secs")]
    public TimeSpan? Duration { get; private set; }

    /// <summary>
    /// Base64 encoded bytearray representing a sampled waveform (currently for voice messages)
    /// </summary>
    [JsonInclude] public string? Waveform { get; private set; }
    /// <summary>
    /// Attachment flags
    /// </summary>
    [JsonInclude] public AttachmentFlags? Flags { get; private set; }
}

public enum AttachmentFlags
{
    NONE = 0,
    /// <summary>
    /// This attachment has been edited using the remix feature on mobile
    /// </summary>
    IS_REMIX = 1 << 2
}