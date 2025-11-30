using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CSharpDiscordWebhook.Objects;

public class Poll
{
    [JsonInclude] public PollMedia Question { get; private set; } = new();
    [JsonInclude] public IReadOnlyList<PollAnswer> Answers { get; private set; } = new List<PollAnswer>();
    [JsonInclude] public DateTime Expiry { get; private set; }
    [JsonInclude] public bool AllowMultiselect { get; private set; }
    [JsonInclude] public PollLayoutType LayoutType { get; private set; }
    [JsonInclude] public PollResults? Results { get; private set; }
}

public class PollMedia
{
    [JsonInclude] public string Text { get; private set; } = string.Empty;
    // TODO: EMOJIS
}

public class PollAnswer
{
    [JsonInclude] public uint AnswerId { get; private set; }
    [JsonInclude] public PollMedia PollMedia { get; private set; } = new();
}

public class PollResults
{
    [JsonInclude] public bool IsFinalized { get; private set; }

    [JsonInclude]
    public IReadOnlyList<PollAnswerCount> AnswerCounts { get; private set; } = new List<PollAnswerCount>();
}

public class PollAnswerCount
{
    [JsonInclude] public uint Id { get; private set; }
    [JsonInclude] public uint Count { get; private set; }
    [JsonInclude] public bool MeVoted { get; private set; }
}