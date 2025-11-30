namespace CSharpDiscordWebhook.Tests;

public static class Utils
{
    public static DiscordWebhook CreateWebhook()
    {
        string? url = Environment.GetEnvironmentVariable("CSHARP_DISCORD_WEBHOOK");
        if(string.IsNullOrWhiteSpace(url))
            Assert.Fail("Environment variable \"CSHARP_DISCORD_WEBHOOK\" not set");
        
        return new(new Uri(url));
    }
}