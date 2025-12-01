using System.Text.Json.Serialization;

namespace CSharpDiscordWebhook.Objects;

public class Reaction
{
    /// <summary>
    /// Total number of times this emoji has been used to react <remarks>including super reacts</remarks>
    /// </summary>
    [JsonInclude]
    public int Count { get; private set; }

    /// <summary>
    /// Reaction count details
    /// </summary>
    [JsonInclude]
    public ReactionDetails CountDetails { get; private set; } = new();

    /// <summary>
    /// Emoji information
    /// </summary>
    [JsonInclude]
    public Emoji Emoji { get; private set; } = new();
}

public class ReactionDetails
{
    /// <summary>
    /// Count of normal reactions
    /// </summary>
    [JsonInclude]
    public int Normal { get; private set; }

    /// <summary>
    /// Count of super reactions
    /// </summary>
    [JsonInclude]
    public int Burst { get; private set; }
}