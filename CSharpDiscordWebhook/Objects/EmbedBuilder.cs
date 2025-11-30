using System;
using System.Collections.Generic;
using System.Drawing;

namespace CSharpDiscordWebhook.Objects;

public class EmbedBuilder
{
    public string? Title { get; set; }
    public string Type { get; } = "rich";
    public string? Description { get; set; }
    public string? Url { get; set; }
    public DateTime? Timestamp { get; set; }
    public Color? Color { get; set; }
    public EmbedFooterBuilder? Footer { get; set; }
    public EmbedMediaBuilder? Image { get; set; }
    public EmbedMediaBuilder? Thumbnail { get; set; }
    public EmbedMediaBuilder? Video { get; set; }
    public EmbedProviderBuilder?  Provider { get; set; }
    public EmbedAuthorBuilder?  Author { get; set; }
    public List<EmbedFieldBuilder>? Fields { get; set; } = new();
}

public class EmbedFooterBuilder
{
    public string Text { get; set; } = string.Empty;
    public string? IconUrl { get; set; }
    public string? ProxyIconUrl { get; set; }
}
public class EmbedMediaBuilder
{
    public string Url { get; set; } = string.Empty;
    public string? ProxyUrl { get; set; }
    public int? Width { get; set; }
    public int? Height { get; set; }
}

public class EmbedProviderBuilder
{
    public string? Name { get; set; }
    public string? Url { get; set; }
}

public class EmbedAuthorBuilder
{
    public string Name { get; set; } = string.Empty;
    public string? Url { get; set; }
    public string? IconUrl { get; set; }
    public string? ProxyIconUrl { get; set; }
}
public class EmbedFieldBuilder
{
    public string Name { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public bool? Inline { get; set; }
}