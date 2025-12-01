using System;
using System.Collections.Generic;

namespace CSharpDiscordWebhook.Objects;

public class PollBuilder
{
    public PollBuilder() { }
    public PollBuilder(DateTime timestamp, Poll poll)
    {
        Question = poll.Question.Text;
        foreach(var answer in poll.Answers)
            Answers.Add(answer.PollMedia.Text);

        Duration = poll.Expiry - timestamp;
        AllowMultiselect = poll.AllowMultiselect;
        Layout = poll.LayoutType;
    }
    
    /// <summary>
    /// The question of the poll. 
    /// </summary>
    public string? Question { get; set; }
    /// <summary>
    /// Each of the answers available in the poll, up to 10
    /// </summary>
    public List<string> Answers { get; set; } = new();
    /// <summary>
    /// Number of hours the poll should be open for, up to 32 days. Defaults to 24
    /// </summary>
    public TimeSpan? Duration { get; set; }
    /// <summary>
    /// Whether a user can select multiple answers
    /// </summary>
    public bool AllowMultiselect { get; set; }
    /// <summary>
    /// The layout type of the poll
    /// </summary>
    public PollLayoutType Layout { get; set; }
}

public enum PollLayoutType : int
{
    DEFAULT	= 1
}