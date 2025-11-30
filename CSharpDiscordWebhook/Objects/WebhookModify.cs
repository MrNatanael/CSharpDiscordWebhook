namespace CSharpDiscordWebhook.Objects;

public class WebhookModify(Webhook webhook)
{
    public string Name { get; set; } = webhook.Name!;
    public string? Avatar { get; set; } = webhook.Avatar;
}