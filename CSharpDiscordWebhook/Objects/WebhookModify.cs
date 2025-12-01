namespace CSharpDiscordWebhook.Objects;

public class WebhookModify(Webhook webhook)
{
    /// <summary>
    /// The new webhook default username
    /// </summary>
    public string Name { get; set; } = webhook.Name!;

    /// <summary>
    /// The new webhook default avatar
    /// </summary>
    public string? Avatar { get; set; } = webhook.Avatar;
}