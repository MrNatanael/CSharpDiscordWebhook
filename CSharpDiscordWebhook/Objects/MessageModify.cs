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

    [JsonIgnore] public ulong Id { get; }
    public string? Content { get; set; }
    public List<EmbedBuilder> Embeds { get; set; } = new();
    public AllowedMentions? AllowedMentions { get; set; }
    public MessageFlags Flags { get; set; } = MessageFlags.NONE;

    // TODO: Finish components
    public List<AttachmentModify> Attachments { get; set; } = new();
}

public class AttachmentModify(ulong id, string filename)
{
    public AttachmentModify(Attachment attachment) : this(attachment.Id, attachment.Filename)
    {
        Parameters = new(attachment);
    }

    public ulong Id { get; } = id;
    public string Filename { get; } = filename;
    public AttachmentParameters Parameters { get; set; } = new();
    public IAttachmentStreamProvider? StreamProvider { get; set; }
}