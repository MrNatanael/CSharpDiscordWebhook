using System.Text.Json.Serialization;

namespace CSharpDiscordWebhook.Objects;

public class Webhook
{
    /// <summary>
    /// The id of the webhook
    /// </summary>
    [JsonInclude] public ulong Id { get; private set; }
    /// <summary>
    /// The type of the webhook
    /// </summary>
    [JsonInclude] public WebhookType Type { get; private set; }
    /// <summary>
    /// The guild id this webhook is for, if any
    /// </summary>
    [JsonInclude] public ulong? GuildId { get; private set; }
    /// <summary>
    /// The channel id this webhook is for, if any
    /// </summary>
    [JsonInclude] public ulong? ChannelId { get; private set; }
    /// <summary>
    /// The default name of the webhook
    /// </summary>
    [JsonInclude] public string? Name { get; private set; }
    /// <summary>
    /// the default user avatar hash of the webhook
    /// </summary>
    [JsonInclude] public string? Avatar { get; private set; }
    /// <summary>
    /// The secure token of the webhook (returned for Incoming Webhooks)
    /// </summary>
    [JsonInclude] public string? Token { get; private set; }
    /// <summary>
    /// The bot/OAuth2 application that created this webhook
    /// </summary>
    [JsonInclude] public ulong? ApplicationId { get; private set; }
    /// <summary>
    /// The url used for executing the webhook <remarks>(returned by the webhooks OAuth2 flow)</remarks>
    /// </summary>
    [JsonInclude] public string? Url { get; private set; }
}

public enum WebhookType : int
{
    Incoming = 1,
    ChannelFollower = 2,
    Application = 3
}