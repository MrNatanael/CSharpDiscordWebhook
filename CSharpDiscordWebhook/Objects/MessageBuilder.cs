using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CSharpDiscordWebhook.Objects;

public class MessageBuilder
{
    /// <summary>
    /// The message contents <remarks>Up to 2000 characters</remarks>
    /// </summary>
    public string? Content { get; set; }

    /// <summary>
    /// Override the default username of the webhook
    /// </summary>
    public string? Username { get; set; }

    /// <summary>
    /// Override the default avatar of the webhook
    /// </summary>
    public string? AvatarUrl { get; set; }

    /// <summary>
    /// True if this is a TTS message
    /// </summary>
    public bool Tts { get; set; }

    /// <summary>
    /// Embedded rich content
    /// </summary>
    public List<EmbedBuilder> Embeds { get; set; } = new();

    /// <summary>
    /// Allowed mentions for the message
    /// </summary>
    public AllowedMentions? AllowedMentions { get; set; }

    /// <summary>
    /// Message flags
    /// </summary>
    public MessageFlags Flags { get; set; } = MessageFlags.NONE;

    /// <summary>
    /// Name of thread to create (requires the webhook channel to be a forum or media channel)
    /// </summary>
    public string? ThreadName { get; set; }

    /// <summary>
    /// Array of tag ids to apply to the thread (requires the webhook channel to be a forum or media channel)
    /// </summary>
    public List<ulong> AppliedTags { get; set; } = new();

    /// <summary>
    /// A poll!
    /// </summary>
    public PollBuilder? Poll { get; set; }
    // TODO: Finish components

    /// <summary>
    /// Attachment objects with filename, description and content
    /// </summary>
    public List<AttachmentBuilder> Attachments { get; set; } = new();
}

public class AllowedMentions
{
    /// <summary>
    /// An array of allowed mention types to parse from the content
    /// </summary>
    [JsonPropertyName("parse")]
    public List<AllowedMentionType>? Types { get; set; }

    /// <summary>
    /// Array of role ids to mention, max 100
    /// </summary>
    public List<ulong>? Roles { get; set; }

    /// <summary>
    /// Array of user ids to mention, max 100
    /// </summary>
    public List<ulong>? Users { get; set; }
}

public readonly struct AllowedMentionType
{
    private AllowedMentionType(string value)
    {
        Value = value;
    }

    public readonly string Value;

    public override string ToString()
    {
        return Value;
    }

    public static AllowedMentionType Everyone { get; } = new("everyone");
    public static AllowedMentionType Users { get; } = new("users");
    public static AllowedMentionType Roles { get; } = new("roles");
}

public enum MessageFlags : int
{
    NONE = 0,
    SUPPRESS_EMBEDS = 1 << 2,
    SUPPRESS_NOTIFICATIONS = 1 << 12,
    IS_COMPONENTS_V2 = 1 << 15
}