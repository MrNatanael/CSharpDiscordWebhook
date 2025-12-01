using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CSharpDiscordWebhook.Objects;

public class MessageModify
{
    public MessageModify(Message msg)
    {
        Id = msg.Id;
        Content = msg.Content;
        foreach (var embed in msg.Embeds)
            Embeds.Add(new EmbedBuilder(embed));

        if (msg.Flags.HasValue) Flags = msg.Flags.Value;

        foreach (var attachment in msg.Attachments)
            Attachments.Add(new AttachmentModify(attachment));
    }

    /// <summary>
    /// Original message ID
    /// </summary>
    [JsonIgnore]
    public ulong Id { get; }

    /// <summary>
    /// New message content
    /// </summary>
    public string? Content { get; set; }

    /// <summary>
    /// New message embed list
    /// </summary>
    public List<EmbedBuilder> Embeds { get; set; } = new();

    /// <summary>
    /// New allowed mentions
    /// </summary>
    public AllowedMentions? AllowedMentions { get; set; }

    /// <summary>
    /// New message flags
    /// </summary>
    public MessageFlags Flags { get; set; } = MessageFlags.NONE;

    // TODO: Finish components
    /// <summary>
    /// New message attachments <remarks>Attachments can be removed and added, but editing is mostly ignored or returns errors</remarks>
    /// </summary>
    public List<AttachmentModify> Attachments { get; set; } = new();
}

public class AttachmentModify(ulong id, string filename)
{
    public AttachmentModify(Attachment attachment) : this(attachment.Id, attachment.Filename)
    {
        Parameters = new AttachmentParameters(attachment);
    }

    /// <summary>
    /// The original attachment ID
    /// </summary>
    public ulong Id { get; } = id;

    /// <summary>
    /// The original attachment filename
    /// </summary>
    public string Filename { get; } = filename;

    public AttachmentParameters Parameters { get; set; } = new();
    public IAttachmentStreamProvider? StreamProvider { get; set; }
}