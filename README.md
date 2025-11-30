# CSharpDiscordWebhook

Simple C# written code to send messages embeds and files using discord webhooks

[![Patreon](https://badgen.net/badge/icon/Ko-fi?icon=kofi&label=Support)](https://ko-fi.com/natanm)
[![NuGet](https://badgen.net/badge/icon/nuget?icon=nuget&label=Package)](https://www.nuget.org/packages/CSharpDiscordWebhook.NET/)

**Getting started**
> Creating a webhook instance

```csharp
using System;
using CSharpDiscordWebook;
using CSharpDiscordWebhook.Objects;

// For safety, never store the webhook url directly in your code!
string webhookUrl = Environment.GetEnvironmentVariable("URL_ENV_VAR");
DiscordWebhook webhook = new DiscordWebhook(webhookUrl);

// OR
DiscordWebhook webhook = new DiscordWebhook(webhook_id, webhook_token);
```

> Sending a simple message
>

```csharp
using System.Drawing;

await webhook.SendMessageAsync(new MessageBuilder 
{
   Content = "Hello, World!",
   Username = "Sample Webhook",
   Embeds = [
        new EmbedBuilder 
        {
            Title = "Embed 1",
            Description = "Embed Description",
            Color = Color.Red
        },
        new EmbedBuilder 
        {
            Title = "Embed 2",
            Color = Color.Green,
            Fields = [
                new EmbedFieldBuilder {
                    Name = "Field 1",
                    Value = "Value"
                },
                new EmbedFieldBuilder 
                {
                    Name = "Field 2",
                    Value = "Value"
                }
            ]           
        }
   ],
   Poll = new PollBuilder 
   {
       Question = "Poll Question",
       Answers = [ "Answer 1", "Answer 2", "Answer 3" ],
       Duration = TimeSpan.FromHours(1), // Minimum is 1
       AllowMultiSelect = true
   }
});
```

> Adding mentions
```csharp
await webhook.SendMessageAsync(new MessageBuilder 
{
    Content = "Mention user: <@user_id>, Mention channel: <#channel_id>",
    AllowedMentions = new AllowedMentions 
    {
        Types = [ AllowedMentionType.Users ]
    }
});
```

> Sending files
```csharp
await message.SendMessageAsync(new MessageBuilder 
{
    Attachments = [
        new FileAttachmentBuilder 
        {
            Id = 0, // Required to be unique
            Filename = "filename.txt",
            Source = new FileInfo("path/to/file.txt"),
            Parameters = {
                Title = "File Title",
                Size = 1024, // File length in bytes (doesn't seem to have any effect)
                
                // More info can be found at https://en.wikipedia.org/wiki/Media_type
                MimeType = "audio/wav",
                
                // Audio parameters, only seem to be displayed on mobile devices
                Duration = TimeSpan.FromSeconds(15),
                Waveform = "...", // A base64 string containing the audio waveform
            }
        }
        
        // You can also provide data directly from memory using
        // StreamAttachmentBuilder
    ]
});
```

> Editing messages
```csharp
ulong messageId = 0;

// This function also supports Message objects instead of ID
// See the examples bellow to learn how to get a Message object

await webhook.EditMessageAsync(message_id, modify => 
{
    modify.Content = "New Content";
    
    // You can also remove or add new attachments
    modify.Attachments.Remove(0);
    
    ulong newAttachmentUniqueId = 1024;
    modify.Attachments.Add(new AttachmentModify(newAttachmentUniqueId, "filename.txt") 
    {
        Parameters =  {
            /* Same as the attachment example above */
        },
        
        // Here you need to set the data provider
        StreamProvider = new FileAttachmentProvider() 
        {
            Source = new FileInfo("/path/to/file.txt")
        }
    });
});
```

> Getting a message
```csharp
ulong messageId = 0;
WebhookResult<Message> result = await webhook.GetMessageAsync(messageId);
if(!result.Success) 
{
    // The reason can be read with
    // result.Error.Message
    return;
}
Message msg = result.Result!;

// webhook.SendMessageAsync can also return a message object
// but you need to set the wait argument to true
result = await webhook.SendMessageAsync(new MessageBuilder 
{
    /* Parameters */
}, wait: true);

/* Handle errors */

msg = result.Result!;
```

> Other functions
```csharp
// Get webhook info
WebhookResult<Webhook> result = await webhook.GetAsync();

// Modify webhook
result = await webhook.ModifyAsync(modify => 
{
    modify.Name = "Default Username";
    modify.Avatar = "url..."
});

// Delete webhook
bool deleted = await webhook.DeleteWebhookAsync();

// Delete a message
ulong messageId = 0;
bool deleted = await webhook.DeleteMessageAsync(messageId);
```