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
    
    public string? Question { get; set; }
    public List<string> Answers { get; set; } = new();
    public TimeSpan? Duration { get; set; }
    public bool AllowMultiselect { get; set; }
    public PollLayoutType Layout { get; set; }
}

public enum PollLayoutType : int
{
    DEFAULT	= 1
}