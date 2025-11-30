using System;
using System.Text.Json.Serialization;
using CSharpDiscordWebhook.Json;

namespace CSharpDiscordWebhook.Objects;

public class Attachment
{
    [JsonInclude] public ulong Id { get; private set; }
    [JsonInclude] public string Filename { get; private set; } = string.Empty;
    [JsonInclude] public string? Title { get; private set; }
    [JsonInclude] public string? Description { get; private set; }
    [JsonInclude] public string? ContentType { get; private set; }
    [JsonInclude] public ulong Size { get; private set; }
    [JsonInclude] public string Url { get; private set; } = string.Empty;
    [JsonInclude] public string ProxyUrl { get; private set; } = string.Empty;
    [JsonInclude] public uint? Width { get; private set; }
    [JsonInclude] public uint? Height { get; private set; }
    [JsonInclude] public bool? Ephemeral { get; private set; }

    [JsonInclude]
    [JsonConverter(typeof(TimeSpanSecondsJsonSerializer))]
    [JsonPropertyName("duration_secs")]
    public TimeSpan? Duration { get; private set; }

    [JsonInclude] public string? Waveform { get; private set; }
    [JsonInclude] public AttachmentFlags? Flags { get; private set; }
}

public enum AttachmentFlags
{
    NONE = 0,
    IS_REMIX = 1 << 2
}