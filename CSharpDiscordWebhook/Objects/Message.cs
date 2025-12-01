using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CSharpDiscordWebhook.Objects;

public class Message
{
    /// <summary>
    /// ID of the message
    /// </summary>
    [JsonInclude]
    public ulong Id { get; private set; }

    /// <summary>
    /// ID of the channel the message was sent in
    /// </summary>
    [JsonInclude]
    public ulong ChannelId { get; private set; }

    /// <summary>
    /// Contents of the message
    /// </summary>
    [JsonInclude]
    public string? Content { get; private set; }

    /// <summary>
    /// Type of message
    /// </summary>
    [JsonInclude]
    public MessageType Type { get; private set; }

    /// <summary>
    /// Message flags
    /// </summary>
    [JsonInclude]
    public MessageFlags? Flags { get; private set; }

    /// <summary>
    /// Users specifically mentioned in the message
    /// </summary>
    [JsonInclude]
    public IReadOnlyList<User> Mentions { get; private set; } = new List<User>();

    /// <summary>
    /// Roles specifically mentioned in this message
    /// </summary>
    [JsonInclude]
    public IReadOnlyList<ulong> MentionRoles { get; private set; } = new List<ulong>();

    /// <summary>
    /// Channels specifically mentioned in this message
    /// </summary>
    [JsonInclude]
    public IReadOnlyList<ChannelMention>? ChannelMentions { get; private set; }

    /// <summary>
    /// Any attached files
    /// </summary>
    [JsonInclude]
    public IReadOnlyList<Attachment> Attachments { get; private set; } = new List<Attachment>();

    /// <summary>
    /// Any embedded content
    /// </summary>
    [JsonInclude]
    public IReadOnlyList<Embed> Embeds { get; private set; } = new List<Embed>();

    /// <summary>
    /// When this message was sent
    /// </summary>
    [JsonInclude]
    public DateTime Timestamp { get; private set; }

    /// <summary>
    /// When this message was edited
    /// </summary>
    [JsonInclude]
    public DateTime? EditedTimestamp { get; private set; }

    /// <summary>
    /// Sent if the message contains components like buttons, action rows, or other interactive components
    /// </summary>
    [JsonInclude]
    public IReadOnlyList<MessageComponent> Components { get; private set; } = new List<MessageComponent>();

    /// <summary>
    /// The author of this message <remarks>Not guaranteed to be a valid user</remarks>
    /// </summary>
    [JsonInclude]
    public User Author { get; private set; } = new();

    /// <summary>
    /// Whether this message is pinned
    /// </summary>
    [JsonInclude]
    public bool Pinned { get; private set; }

    /// <summary>
    /// Whether this message mentions everyone
    /// </summary>
    [JsonInclude]
    public bool MentionEveryone { get; private set; }

    /// <summary>
    /// Whether this was a TTS message
    /// </summary>
    [JsonInclude]
    public bool Tts { get; private set; }

    /// <summary>
    /// This is the webhook's id
    /// </summary>
    [JsonInclude]
    public ulong WebhookId { get; private set; }

    /// <summary>
    /// A poll!
    /// </summary>
    [JsonInclude]
    public Poll? Poll { get; private set; }

    /// <summary>
    /// Reactions to the message
    /// </summary>
    [JsonInclude]
    public IReadOnlyList<Reaction> Reactions { get; private set; } = new List<Reaction>();
}

public class ChannelMention
{
    /// <summary>
    /// ID of the channel
    /// </summary>
    [JsonInclude]
    public ulong Id { get; private set; }

    /// <summary>
    /// ID of the guild containing the channel
    /// </summary>
    [JsonInclude]
    public ulong GuildId { get; private set; }

    /// <summary>
    /// The type of channel
    /// </summary>
    [JsonInclude]
    public ChannelType Type { get; private set; }

    /// <summary>
    /// The name of the channel
    /// </summary>
    [JsonInclude]
    public string Name { get; private set; } = string.Empty;
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
    POLL_RESULT = 46
}