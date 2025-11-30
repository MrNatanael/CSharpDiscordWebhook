using System.Collections.Generic;

namespace CSharpDiscordWebhook.Objects;

public abstract class MessageComponentBuilder
{
    public abstract int Type { get; } 
}

public class ActionRowComponentBuilder : MessageComponentBuilder
{
    public override int Type { get; } = 1;
    public List<MessageComponentBuilder> Components = new();
}

public class ButtonComponentBuilder : MessageComponentBuilder
{
    public override int Type { get; } = 2;
    public ButtonComponentStyle Style { get; set; }
    public string? Label { get; set; }
    // TODO: EMOJI
    public string CustomId { get; set; } = string.Empty;
    public ulong? SkuId { get; set; }
    public string? Url { get; set; }
    public bool? Disabled { get; set; }
}

public enum ButtonComponentStyle
{
    Primary = 1,
    Secondary,
    Success,
    Danger,
    Link,
    Premium
}