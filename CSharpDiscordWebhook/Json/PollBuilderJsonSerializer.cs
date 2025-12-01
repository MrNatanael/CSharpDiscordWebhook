using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using CSharpDiscordWebhook.Objects;

namespace CSharpDiscordWebhook.Json;

public class PollBuilderJsonSerializer : JsonConverter<PollBuilder>
{
    public override PollBuilder? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, PollBuilder value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        
        WritePollMediaObject(writer, "question", new PollMedia { Text = value.Question }, options);
        
        writer.WritePropertyName("answers");
        writer.WriteStartArray();
        foreach (var answer in value.Answers)
        {
            writer.WriteStartObject();
            WritePollMediaObject(writer, "poll_media", new PollMedia()
            {
                Text = answer.Text,
                Emoji = answer.Emoji
            }, options);
            writer.WriteEndObject();
        }
        writer.WriteEndArray();

        if (value.Duration.HasValue)
        {
            writer.WritePropertyName("duration");
            new DurationHoursJonSerializer().Write(writer, value.Duration, options);
            //  JsonSerializer.Serialize(writer, value.Duration.Value, options);
        }

        if (value.AllowMultiselect)
        {
            writer.WritePropertyName("allow_multiselect");
            writer.WriteBooleanValue(true);
        }

        if (value.Layout != PollLayoutType.DEFAULT)
        {
            writer.WritePropertyName("layout");
            JsonSerializer.Serialize(writer, value.Layout, options);
        }
        
        writer.WriteEndObject();
    }

    void WritePollMediaObject(Utf8JsonWriter writer, string property, PollMedia media, JsonSerializerOptions options)
    {
        writer.WritePropertyName(property);
        writer.WriteStartObject();
        
        writer.WritePropertyName("text");
        writer.WriteStringValue(media.Text);

        if (media.Emoji != null)
        {
            writer.WritePropertyName("emoji");
            JsonSerializer.Serialize(writer, media.Emoji, options);
        }
        
        writer.WriteEndObject();
    }
}