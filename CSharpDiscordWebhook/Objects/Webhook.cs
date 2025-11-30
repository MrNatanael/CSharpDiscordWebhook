using System.Text.Json.Serialization;

namespace CSharpDiscordWebhook.Objects;

public class Webhook
{
    [JsonInclude] public ulong Id { get; private set; }
    [JsonInclude] public WebhookType Type { get; private set; }
    [JsonInclude] public ulong? GuildId { get; private set; }
    [JsonInclude] public ulong? ChannelId { get; private set; }
    [JsonInclude] public string? Name { get; private set; }
    [JsonInclude] public string? Avatar { get; private set; }
    [JsonInclude] public string? Token { get; private set; }
    [JsonInclude] public ulong? ApplicationId { get; private set; }
    [JsonInclude] public string? Url { get; private set; }
}

public enum WebhookType : int
{
    Incoming = 1,
    ChannelFollower = 2,
    Application = 3
}