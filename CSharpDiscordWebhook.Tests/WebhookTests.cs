using CSharpDiscordWebhook.Objects;

namespace CSharpDiscordWebhook.Tests;

[TestClass]
public sealed class WebhookTests
{
    [TestMethod]
    public async Task GetWebhook()
    {
        var r = await Utils.CreateWebhook().GetAsync();
        Assert.IsTrue(r.Success, r.Error?.Message);
    }

    [TestMethod]
    public async Task ModifyWebhook()
    {
        Random rand = new();
        string name = $"Test Webhook {rand.Next()}";
        var r = await Utils.CreateWebhook().ModifyAsync(new WebhookModify()
        {
            Name = name
        });
        Assert.IsTrue(r.Success && r.Result!.Name == name, r.Error?.Message);
    }

    [TestMethod]
    public async Task ExecuteWebhook()
    {
        var r = await Utils.CreateWebhook().ExecuteAsync(new MessageBuilder
        {
            Poll = new()
            {
                Question = "Hello",
                Answers = [ "Yes", "No" ],
                Duration = TimeSpan.FromHours(1)
            }
        });
        Assert.IsTrue(r.Success, r.Error?.Message);
    }
}