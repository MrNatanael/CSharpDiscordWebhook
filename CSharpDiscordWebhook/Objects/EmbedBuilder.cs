using System;
using System.Collections.Generic;
using System.Drawing;

namespace CSharpDiscordWebhook.Objects;

public class EmbedBuilder
{
    public EmbedBuilder()
    {
    }

    public EmbedBuilder(Embed embed)
    {
        Title = embed.Title;
        Description = embed.Description;
        Url = embed.Url;
        Timestamp = embed.Timestamp;
        Color = embed.Color;

        if (embed.Footer != null)
            Footer = new(embed.Footer);

        if (embed.Image != null)
            Image = new(embed.Image);

        if (embed.Thumbnail != null)
            Thumbnail = new(embed.Thumbnail);

        if (embed.Video != null)
            Video = new(embed.Video);

        if (embed.Provider != null)
            Provider = new(embed.Provider);

        if (embed.Author != null)
            Author = new(embed.Author);

        if (embed.Fields != null)
        {
            Fields = new();
            foreach (var field in embed.Fields)
                Fields.Add(new(field));
        }
    }

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
    public EmbedProviderBuilder? Provider { get; set; }
    public EmbedAuthorBuilder? Author { get; set; }
    public List<EmbedFieldBuilder>? Fields { get; set; }
    public List<EmbedFieldBuilder> Fields { get; set; } = new();
}

public class EmbedFooterBuilder
{
    public EmbedFooterBuilder()
    {
    }

    public EmbedFooterBuilder(EmbedFooter footer)
    {
        Text = footer.Text;
        IconUrl = footer.IconUrl;
        ProxyIconUrl = footer.ProxyIconUrl;
    }

    public string Text { get; set; } = string.Empty;
    public string? IconUrl { get; set; }
    public string? ProxyIconUrl { get; set; }
}

public class EmbedMediaBuilder
{
    public EmbedMediaBuilder()
    {
    }

    public EmbedMediaBuilder(EmbedMedia media)
    {
        Url = media.Url;
        ProxyUrl = media.ProxyUrl;
        Width = media.Width;
        Height = media.Height;
    }

    public string Url { get; set; } = string.Empty;
    public string? ProxyUrl { get; set; }
    public int? Width { get; set; }
    public int? Height { get; set; }
}

public class EmbedProviderBuilder
{
    public EmbedProviderBuilder()
    {
    }

    public EmbedProviderBuilder(EmbedProvider provider)
    {
        Name = provider.Name;
        Url = provider.Url;
    }

    public string? Name { get; set; }
    public string? Url { get; set; }
}

public class EmbedAuthorBuilder
{
    public EmbedAuthorBuilder()
    {
    }

    public EmbedAuthorBuilder(EmbedAuthor author)
    {
        Name = author.Name;
        Url = author.Url;
        IconUrl = author.IconUrl;
        ProxyIconUrl = author.ProxyIconUrl;
    }

    public string Name { get; set; } = string.Empty;
    public string? Url { get; set; }
    public string? IconUrl { get; set; }
    public string? ProxyIconUrl { get; set; }
}

public class EmbedFieldBuilder
{
    public EmbedFieldBuilder()
    {
    }

    public EmbedFieldBuilder(EmbedField field)
    {
        Name = field.Name;
        Value = field.Value;
        Inline = field.Inline;
    }

    public string Name { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public bool? Inline { get; set; }
}