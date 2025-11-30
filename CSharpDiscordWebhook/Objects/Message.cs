using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CSharpDiscordWebhook.Objects;

public class Message
{
    [JsonInclude] public ulong Id { get; private set; }
    [JsonInclude] public ulong ChannelId { get; private set; }
    [JsonInclude] public string? Content { get; private set; }
    [JsonInclude] public MessageType Type { get; private set; }
    [JsonInclude] public MessageFlags? Flags { get; private set; }
    [JsonInclude] public IReadOnlyList<User> Mentions { get; private set; } = new List<User>();
    [JsonInclude] public IReadOnlyList<ulong> MentionRoles { get; private set; } = new List<ulong>();
    [JsonInclude] public IReadOnlyList<ChannelMention>? ChannelMentions { get; private set; }
    [JsonInclude] public IReadOnlyList<Attachment> Attachments { get; private set; } = new List<Attachment>();
    [JsonInclude] public IReadOnlyList<Embed> Embeds { get; private set; } = new List<Embed>();
    [JsonInclude] public DateTime Timestamp { get; private set; }
    [JsonInclude] public DateTime? EditedTimestamp { get; private set; }

    [JsonInclude]
    public IReadOnlyList<MessageComponent> Components { get; private set; } = new List<MessageComponent>();

    [JsonInclude] public User Author { get; private set; } = new();
    [JsonInclude] public bool Pinned { get; private set; }
    [JsonInclude] public bool MentionEveryone { get; private set; }
    [JsonInclude] public bool Tts { get; private set; }
    [JsonInclude] public ulong WebhookId { get; private set; }
    [JsonInclude] public Poll? Poll { get; private set; }
}

public class ChannelMention
{
    [JsonInclude] public ulong Id { get; private set; }
    [JsonInclude] public ulong GuildId { get; private set; }
    [JsonInclude] public ChannelType Type { get; private set; }
    [JsonInclude] public string Name { get; private set; } = string.Empty;
}

public enum ChannelType
{
    GUILD_TEXT = 0,
    DM = 1,
    GUILD_VOICE = 2,
    GROUP_DM = 3,
    GUILD_CATEGORY = 4,
    GUILD_ANNOUNCEMENT = 5,
    ANNOUNCEMENT_THREAD = 10,
    PUBLIC_THREAD = 11,
    PRIVATE_THREAD = 12,
    GUILD_STAGE_VOICE = 13,
    GUILD_DIRECTORY = 14,
    GUILD_FORUM = 15,
    GUILD_MEDIA = 16
}

public enum MessageType
{
    DEFAULT = 0,
    RECIPIENT_ADD = 1,
    RECIPIENT_REMOVE = 2,
    CALL = 3,
    CHANNEL_NAME_CHANGE = 4,
    CHANNEL_ICON_CHANGE = 5,
    CHANNEL_PINNED_MESSAGE = 6,
    USER_JOIN = 7,
    GUILD_BOOST = 8,
    GUILD_BOOST_TIER_1 = 9,
    GUILD_BOOST_TIER_2 = 10,
    GUILD_BOOST_TIER_3 = 11,
    CHANNEL_FOLLOW_ADD = 12,
    GUILD_DISCOVERY_DISQUALIFIED = 14,
    GUILD_DISCOVERY_REQUALIFIED = 15,
    GUILD_DISCOVERY_GRACE_PERIOD_INITIAL_WARNING = 16,
    GUILD_DISCOVERY_GRACE_PERIOD_FINAL_WARNING = 17,
    THREAD_CREATED = 18,
    REPLY = 19,
    CHAT_INPUT_COMMAND = 20,
    THREAD_STARTER_MESSAGE = 21,
    GUILD_INVITE_REMINDER = 22,
    CONTEXT_MENU_COMMAND = 23,
    AUTO_MODERATION_ACTION = 24,
    ROLE_SUBSCRIPTION_PURCHASE = 25,
    INTERACTION_PREMIUM_UPSELL = 26,
    STAGE_START = 27,
    STAGE_END = 28,
    STAGE_SPEAKER = 29,
    STAGE_TOPIC = 31,
    GUILD_APPLICATION_PREMIUM_SUBSCRIPTION = 32,
    GUILD_INCIDENT_ALERT_MODE_ENABLED = 36,
    GUILD_INCIDENT_ALERT_MODE_DISABLED = 37,
    GUILD_INCIDENT_REPORT_RAID = 38,
    GUILD_INCIDENT_REPORT_FALSE_ALARM = 39,
    PURCHASE_NOTIFICATION = 44,
    POLL_RESULT = 46,
}