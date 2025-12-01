using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CSharpDiscordWebhook.Objects;

public class Poll
{
    /// <summary>
    /// The question of the poll. <remarks>Only text is supported</remarks>
    /// </summary>
    [JsonInclude] public PollMedia Question { get; private set; } = new();
    /// <summary>
    /// Each of the answers available in the poll.
    /// </summary>
    [JsonInclude] public IReadOnlyList<PollAnswer> Answers { get; private set; } = new List<PollAnswer>();
    /// <summary>
    /// The time when the poll ends.
    /// </summary>
    [JsonInclude] public DateTime Expiry { get; private set; }
    /// <summary>
    /// Whether a user can select multiple answers
    /// </summary>
    [JsonInclude] public bool AllowMultiselect { get; private set; }
    /// <summary>
    /// The layout type of the poll
    /// </summary>
    [JsonInclude] public PollLayoutType LayoutType { get; private set; }
    /// <summary>
    /// The results of the poll
    /// </summary>
    [JsonInclude] public PollResults? Results { get; private set; }
}

public class PollMedia
{
    /// <summary>
    /// The text of the field
    /// </summary>
    [JsonInclude] public string Text { get; private set; } = string.Empty;
    /// <summary>
    /// The emoji of the field
    /// </summary>
    [JsonInclude] public Emoji? Emoji { get; private set; }
}

public class PollAnswer
{
    /// <summary>
    /// The ID of the answer
    /// </summary>
    [JsonInclude] public uint AnswerId { get; private set; }
    /// <summary>
    /// The data of the answer
    /// </summary>
    [JsonInclude] public PollMedia PollMedia { get; private set; } = new();
}

public class PollResults
{
    /// <summary>
    /// Whether the votes have been precisely counted
    /// </summary>
    [JsonInclude] public bool IsFinalized { get; private set; }
    /// <summary>
    /// The counts for each answer
    /// </summary>
    [JsonInclude]
    public IReadOnlyList<PollAnswerCount> AnswerCounts { get; private set; } = new List<PollAnswerCount>();
}

public class PollAnswerCount
{
    /// <summary>
    /// The answer ID
    /// </summary>
    [JsonInclude] public uint Id { get; private set; }
    /// <summary>
    /// The number of votes for this answer
    /// </summary>
    [JsonInclude] public uint Count { get; private set; }
    /// <summary>
    /// Whether the current user voted for this answer
    /// </summary>
    [JsonInclude] public bool MeVoted { get; private set; }
}