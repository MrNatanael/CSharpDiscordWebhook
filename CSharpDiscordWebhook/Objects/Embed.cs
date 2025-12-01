using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.Json.Serialization;

namespace CSharpDiscordWebhook.Objects;

public class Embed
{
    /// <summary>
    /// Title of embed
    /// </summary>
    [JsonInclude]
    public string? Title { get; private set; }

    /// <summary>
    /// Type of embed <remarks>Always "rich" for webhook embeds</remarks>
    /// </summary>
    [JsonInclude]
    public string Type { get; } = "rich";

    /// <summary>
    /// Description of embed
    /// </summary>
    [JsonInclude]
    public string? Description { get; private set; }

    /// <summary>
    /// Url of embed
    /// </summary>
    [JsonInclude]
    public string? Url { get; private set; }

    /// <summary>
    /// Timestamp of embed content
    /// </summary>
    [JsonInclude]
    public DateTime? Timestamp { get; private set; }

    /// <summary>
    /// Color code of the embed
    /// </summary>
    [JsonInclude]
    public Color? Color { get; private set; }

    /// <summary>
    /// Footer information
    /// </summary>
    [JsonInclude]
    public EmbedFooter? Footer { get; private set; }

    /// <summary>
    /// Image information
    /// </summary>
    [JsonInclude]
    public EmbedMedia? Image { get; private set; }

    /// <summary>
    /// Thumbnail information
    /// </summary>
    [JsonInclude]
    public EmbedMedia? Thumbnail { get; private set; }

    /// <summary>
    /// Video information
    /// </summary>
    [JsonInclude]
    public EmbedMedia? Video { get; private set; }

    /// <summary>
    /// Provider information
    /// </summary>
    [JsonInclude]
    public EmbedProvider? Provider { get; private set; }

    /// <summary>
    /// Author information
    /// </summary>
    [JsonInclude]
    public EmbedAuthor? Author { get; private set; }

    /// <summary>
    /// Fields information, max of 25
    /// </summary>
    [JsonInclude]
    public IReadOnlyList<EmbedField>? Fields { get; private set; }
}

public class EmbedFooter
{
    /// <summary>
    /// Footer text
    /// </summary>
    [JsonInclude]
    public string Text { get; private set; } = string.Empty;

    /// <summary>
    /// Url of footer icon <remarks>Only supports http(s) and attachments</remarks>
    /// </summary>
    [JsonInclude]
    public string? IconUrl { get; private set; }

    /// <summary>
    /// A proxied url of footer icon
    /// </summary>
    [JsonInclude]
    public string? ProxyIconUrl { get; private set; }
}

public class EmbedMedia
{
    /// <summary>
    /// Source url of media
    /// </summary>
    [JsonInclude]
    public string Url { get; private set; } = string.Empty;

    /// <summary>
    /// Proxied url of media
    /// </summary>
    [JsonInclude]
    public string? ProxyUrl { get; private set; }

    /// <summary>
    /// Width of media
    /// </summary>
    [JsonInclude]
    public int? Width { get; private set; }

    /// <summary>
    /// Height of media
    /// </summary>
    [JsonInclude]
    public int? Height { get; private set; }
}

public class EmbedProvider
{
    /// <summary>
    /// Name of provider
    /// </summary>
    [JsonInclude]
    public string? Name { get; private set; }

    /// <summary>
    /// Url of provider
    /// </summary>
    [JsonInclude]
    public string? Url { get; private set; }
}

public class EmbedAuthor
{
    /// <summary>
    /// Name of author
    /// </summary>
    [JsonInclude]
    public string Name { get; private set; } = string.Empty;

    /// <summary>
    /// Url of author <remarks>Only supports https</remarks>
    /// </summary>
    [JsonInclude]
    public string? Url { get; private set; }

    /// <summary>
    /// Url of author icon <remarks>only supports http(s) and attachments</remarks>
    /// </summary>
    [JsonInclude]
    public string? IconUrl { get; private set; }

    /// <summary>
    /// Proxied url of author icon
    /// </summary>
    [JsonInclude]
    public string? ProxyIconUrl { get; private set; }
}

public class EmbedField
{
    /// <summary>
    /// Name of the field
    /// </summary>
    [JsonInclude]
    public string Name { get; private set; } = string.Empty;

    /// <summary>
    /// Value of the field
    /// </summary>
    [JsonInclude]
    public string Value { get; private set; } = string.Empty;

    /// <summary>
    /// Whether this field should display inline
    /// </summary>
    [JsonInclude]
    public bool? Inline { get; private set; }
}