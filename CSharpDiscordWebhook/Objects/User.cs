using System.Text.Json.Serialization;

namespace CSharpDiscordWebhook.Objects;

public class User
{
    /// <summary>
    /// the user's ID
    /// </summary>
    [JsonInclude] public ulong Id { get; private set; }
    /// <summary>
    /// The user's username, not unique across the platform
    /// </summary>
    [JsonInclude] public string Username { get; private set; } = string.Empty;
    /// <summary>
    /// The user's Discord-tag
    /// </summary>
    [JsonInclude] public string? Avatar { get; private set; }
}