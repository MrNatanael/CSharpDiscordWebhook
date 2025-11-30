using System.Text.Json.Serialization;

namespace CSharpDiscordWebhook.Objects;

public class User
{
    [JsonInclude] public ulong Id { get; private set; }
    [JsonInclude] public string Username { get; private set; } = string.Empty;
    [JsonInclude] public string? Avatar { get; private set; }
}