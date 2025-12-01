using System.Drawing;
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
        var r = await Utils.CreateWebhook().ModifyAsync(modify => { modify.Name = name; });
        Assert.IsTrue(r.Success && r.Result!.Name == name, r.Error?.Message);
    }

    [TestMethod]
    public async Task SendPoll()
    {
        var r = await Utils.CreateWebhook().SendMessageAsync(new MessageBuilder
        {
            Poll = new()
            {
                Question = "Is this working??",
                Answers =
                [
                    new PollAnswerBuilder { Text = "Yes", Emoji = new("😎") },
                    new PollAnswerBuilder { Text = "No", Emoji = new("🤡") },
                    new PollAnswerBuilder { Text = "I Don't Know™", Emoji = new("☠️") }
                ],
                Duration = TimeSpan.FromHours(15),
                AllowMultiselect = true
            }
        });
        Assert.IsTrue(r.Success, r.Error?.Message);
    }

    [TestMethod]
    public async Task SendEmbeds()
    {
        var r = await Utils.CreateWebhook().SendMessageAsync(new MessageBuilder
        {
            Embeds =
            [
                new EmbedBuilder
                {
                    Description = "Red",
                    Color = Color.Red
                },
                new EmbedBuilder
                {
                    Description = "Green",
                    Color = Color.Green
                },
                new EmbedBuilder
                {
                    Description = "Blue",
                    Color = Color.Blue
                },
                new EmbedBuilder
                {
                    Author = new()
                    {
                        Name = "Test Author"
                    },
                    Footer = new()
                    {
                        Text = "Test Footer"
                    },
                    Fields =
                    [
                        new() { Name = "Field 1", Value = "Value 1", Inline = true },
                        new() { Name = "Field 2", Value = "Value 2", Inline = true },
                        new() { Name = "Field 3", Value = "Value 3" },
                    ],
                    Timestamp = DateTime.UtcNow
                }
            ]
        });
        Assert.IsTrue(r.Success, r.Error?.Message);
    }

    [TestMethod]
    public async Task SendFile()
    {
        byte[] wav = Utils.MakeWav();
        var slice = wav.AsSpan().Slice(44, 150);
        string b64 = Convert.ToBase64String(slice);

        using var s = new MemoryStream(wav);

        var r = await Utils.CreateWebhook().SendMessageAsync(new MessageBuilder
        {
            Attachments =
            [
                new StreamAttachmentBuilder()
                {
                    Filename = "test.wav",
                    Id = 0,
                    Stream = s,
                    Parameters = new()
                    {
                        Title = "Test Text File",
                        Description = "A test file",
                        Size = (uint)s.Length,
                        Duration = TimeSpan.FromHours(15),
                        Waveform = b64,
                        MimeType = "audio/wav"
                    }
                }
            ]
        });
        Assert.IsTrue(r.Success, r.Error?.Message);
    }

    [TestMethod]
    public async Task SendMessage()
    {
        var r = await Utils.CreateWebhook().SendMessageAsync(new MessageBuilder
        {
            Content = "Hello, World!",
            Username = "Test Username"
        }, wait: true);

        if (!r.Success) Assert.Fail(r.Error?.Message!);

        this.MessageToEdit = r.Result;
    }

    [TestMethod]
    public async Task EditMessage()
    {
        await SendMessage();

        Random rand = new();
        string text = $"Confirmation: {rand.Next()}";
        var r = await Utils.CreateWebhook().EditMessageAsync(MessageToEdit!, modify => { modify.Content = text; });

        if (!r.Success) Assert.Fail(r.Error?.Message!);
        if (r.Result?.Content != text) Assert.Fail("Content didn't change");
    }

    [TestMethod]
    public async Task DeleteMessage()
    {
        await SendMessage();
        var r = await Utils.CreateWebhook().DeleteMessageAsync(MessageToEdit!.Id);
        Assert.IsTrue(r.Result, r.Error?.Message);
    }

    private Message? MessageToEdit;
}