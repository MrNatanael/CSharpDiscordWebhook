using System.Text.Json.Serialization;

namespace CSharpDiscordWebhook.Objects;

public class Emoji
{
    public Emoji(ulong id)
    {
        Id = id;
    }

    public Emoji(string name)
    {
        Name = name;
    }

    public Emoji()
    {
    }

    [JsonInclude] public ulong? Id { get; private set; }
    [JsonInclude] public string? Name { get; private set; }
}