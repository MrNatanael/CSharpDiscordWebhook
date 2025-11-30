using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.Json.Serialization;

namespace CSharpDiscordWebhook.Objects;

public class Embed
{
    [JsonInclude] public string? Title { get; private set; }
    [JsonInclude] public string Type { get; } = "rich";
    [JsonInclude] public string? Description { get; private set; }
    [JsonInclude] public string? Url { get; private set; }
    [JsonInclude] public DateTime? Timestamp { get; private set; }
    [JsonInclude] public Color? Color { get; private set; }
    [JsonInclude] public EmbedFooter? Footer { get; private set; }
    [JsonInclude] public EmbedMedia? Image { get; private set; }
    [JsonInclude] public EmbedMedia? Thumbnail { get; private set; }
    [JsonInclude] public EmbedMedia? Video { get; private set; }
    [JsonInclude] public EmbedProvider?  Provider { get; private set; }
    [JsonInclude] public EmbedAuthor?  Author { get; private set; }
    [JsonInclude] public IReadOnlyList<EmbedField>? Fields { get; private set; }
}

public class EmbedFooter
{
    [JsonInclude] public string Text { get; private set; } = string.Empty;
    [JsonInclude] public string? IconUrl { get; private set; }
    [JsonInclude] public string? ProxyIconUrl { get; private set; }
}
public class EmbedMedia
{
    [JsonInclude] public string Url { get; private set; } = string.Empty;
    [JsonInclude] public string? ProxyUrl { get; private set; }
    [JsonInclude] public int? Width { get; private set; }
    [JsonInclude] public int? Height { get; private set; }
}

public class EmbedProvider
{
    [JsonInclude] public string? Name { get; private set; }
    [JsonInclude] public string? Url { get; private set; }
}

public class EmbedAuthor
{
    [JsonInclude] public string Name { get; private set; } = string.Empty;
    [JsonInclude] public string? Url { get; private set; }
    [JsonInclude] public string? IconUrl { get; private set; }
    [JsonInclude] public string? ProxyIconUrl { get; private set; }
}
public class EmbedField
{
    [JsonInclude] public string Name { get; private set; } = string.Empty;
    [JsonInclude] public string Value { get; private set; } = string.Empty;
    [JsonInclude] public bool? Inline { get; private set; }
}