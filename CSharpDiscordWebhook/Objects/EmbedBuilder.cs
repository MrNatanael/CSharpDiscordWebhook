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

    /// <summary>
    /// Title of embed
    /// </summary>
    public string? Title { get; set; }
    /// <summary>
    /// Type of embed <remarks>Always "rich" for webhook embeds</remarks>
    /// </summary>
    public string Type { get; } = "rich";
    /// <summary>
    /// Description of embed
    /// </summary>
    public string? Description { get; set; }
    /// <summary>
    /// Url of embed
    /// </summary>
    public string? Url { get; set; }
    /// <summary>
    /// Timestamp of embed content
    /// </summary>
    public DateTime? Timestamp { get; set; }
    /// <summary>
    /// Color code of the embed
    /// </summary>
    public Color? Color { get; set; }
    /// <summary>
    /// Footer information
    /// </summary>
    public EmbedFooterBuilder? Footer { get; set; }
    /// <summary>
    /// Image information
    /// </summary>
    public EmbedMediaBuilder? Image { get; set; }
    /// <summary>
    /// Thumbnail information
    /// </summary>
    public EmbedMediaBuilder? Thumbnail { get; set; }
    /// <summary>
    /// Video information
    /// </summary>
    public EmbedMediaBuilder? Video { get; set; }
    /// <summary>
    /// Provider information
    /// </summary>
    public EmbedProviderBuilder? Provider { get; set; }
    /// <summary>
    /// Author information
    /// </summary>
    public EmbedAuthorBuilder? Author { get; set; }

    /// <summary>
    /// Fields information, max of 25
    /// </summary>
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
    /// <summary>
    /// Footer text
    /// </summary>
    public string Text { get; set; } = string.Empty;
    /// <summary>
    /// Url of footer icon <remarks>Only supports http(s) and attachments</remarks>
    /// </summary>
    public string? IconUrl { get; set; }
    /// <summary>
    /// A proxied url of footer icon
    /// </summary>
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

    /// <summary>
    /// Source url of media
    /// </summary>
    public string Url { get; set; } = string.Empty;
    /// <summary>
    /// Proxied url of media
    /// </summary>
    public string? ProxyUrl { get; set; }
    /// <summary>
    /// Width of media
    /// </summary>
    public int? Width { get; set; }
    /// <summary>
    /// Height of media
    /// </summary>
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

    /// <summary>
    /// Name of provider
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// Url of provider
    /// </summary>
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

    /// <summary>
    /// Name of author
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// Url of author <remarks>Only supports https</remarks>
    /// </summary>
    public string? Url { get; set; }
    /// <summary>
    /// Url of author icon <remarks>only supports http(s) and attachments</remarks>
    /// </summary>
    public string? IconUrl { get; set; }
    /// <summary>
    /// Proxied url of author icon
    /// </summary>
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

    /// <summary>
    /// Name of the field
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// Value of the field
    /// </summary>
    public string Value { get; set; } = string.Empty;
    /// <summary>
    /// Whether this field should display inline
    /// </summary>
    public bool? Inline { get; set; }
}