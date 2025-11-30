using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using CSharpDiscordWebhook.Json;

namespace CSharpDiscordWebhook.Objects;

public class PollBuilder
{
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