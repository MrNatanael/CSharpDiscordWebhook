using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CSharpDiscordWebhook.Objects;

public class MessageBuilder
{
    public string? Content { get; set; }
    public string? Username { get; set; }
    public string? AvatarUrl { get; set; }
    public bool Tts { get; set; }
    public List<EmbedBuilder> Embeds { get; set; } = new();
    public AllowedMentions? AllowedMentions { get; set; }
    public MessageFlags Flags { get; set; } = MessageFlags.NONE;
    public string? ThreadName { get; set; }
    public List<ulong> AppliedTags { get; set; } = new();
    public PollBuilder? Poll { get; set; }
    // TODO: Finish components
    
    public List<AttachmentBuilder> Attachments { get; set; } = new();
}

public class AllowedMentions
{
    [JsonPropertyName("parse")] public List<AllowedMentionType>? Types { get; set; }
    public List<ulong>? Roles { get; set; }
    public List<ulong>? Users { get; set; }
}

public readonly struct AllowedMentionType
{
    AllowedMentionType(string value)
    {
        Value = value;
    }

    public readonly string Value;
    public override string ToString() { return Value; }

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