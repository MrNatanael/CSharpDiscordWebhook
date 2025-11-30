using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using CSharpDiscordWebhook.Objects;

namespace CSharpDiscordWebhook.Json;

public class DiscordAttachmentsJsonSerializer : JsonConverter<AttachmentBuilder>
{
    public override AttachmentBuilder? Read(ref Utf8JsonReader reader, Type typeToConvert,
        JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, AttachmentBuilder value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        writer.WritePropertyName("id");
        writer.WriteNumberValue(value.Id);

        writer.WritePropertyName("filename");
        writer.WriteStringValue(value.Filename);

        WriteParameters(writer, value.Parameters);

        writer.WriteEndObject();
    }

    internal static void WriteParameters(Utf8JsonWriter writer, AttachmentParameters? parameters)
    {
        if (parameters == null) return;
        if (parameters.Title != null)
        {
            writer.WritePropertyName("title");
            writer.WriteStringValue(parameters.Title);
        }

        if (parameters.Description != null)
        {
            writer.WritePropertyName("description");
            writer.WriteStringValue(parameters.Description);
        }

        if (parameters.MimeType != null)
        {
            writer.WritePropertyName("content_type");
            writer.WriteStringValue(parameters.MimeType);
        }

        if (parameters.ProxyUrl != null)
        {
            writer.WritePropertyName("proxy_url");
            writer.WriteStringValue(parameters.ProxyUrl);
        }

        if (parameters.Url != null)
        {
            writer.WritePropertyName("url");
            writer.WriteStringValue(parameters.Url);
        }

        if (parameters.Width != null)
        {
            writer.WritePropertyName("width");
            writer.WriteNumberValue(parameters.Width.Value);
        }

        if (parameters.Height != null)
        {
            writer.WritePropertyName("height");
            writer.WriteNumberValue(parameters.Height.Value);
        }

        if (parameters.Ephemeral != null)
        {
            writer.WritePropertyName("ephemeral");
            writer.WriteBooleanValue(parameters.Ephemeral.Value);
        }

        if (parameters.Duration != null)
        {
            writer.WritePropertyName("duration_secs");
            writer.WriteNumberValue((float)parameters.Duration.Value.TotalSeconds);
        }

        if (parameters.Waveform != null)
        {
            writer.WritePropertyName("waveform");
            writer.WriteStringValue(parameters.Waveform);
        }

        if (parameters.IsRemix)
        {
            writer.WritePropertyName("flags");
            writer.WriteNumberValue(1 << 2);
        }
    }
}

public class DiscordAttachmentsModifyJsonSerializer : JsonConverter<AttachmentModify>
{
    public override AttachmentModify? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, AttachmentModify value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        writer.WritePropertyName("id");
        writer.WriteNumberValue(value.Id);

        writer.WritePropertyName("filename");
        writer.WriteStringValue(value.Filename);

        DiscordAttachmentsJsonSerializer.WriteParameters(writer, value.Parameters);

        writer.WriteEndObject();
    }
}