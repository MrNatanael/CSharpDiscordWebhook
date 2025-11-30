using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using CSharpDiscordWebhook.Objects;

namespace CSharpDiscordWebhook.Json;

public class DiscordAttachmentsJsonSerializer : JsonConverter<AttachmentBuilder>
{
    public override AttachmentBuilder? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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

        if (value.Parameters != null)
        {
            if (value.Parameters.Title != null)
            {
                writer.WritePropertyName("title");
                writer.WriteStringValue(value.Parameters.Title);
            }

            if (value.Parameters.Description != null)
            {
                writer.WritePropertyName("description");
                writer.WriteStringValue(value.Parameters.Description);
            }

            if (value.Parameters.MimeType != null)
            {
                writer.WritePropertyName("content_type");
                writer.WriteStringValue(value.Parameters.MimeType);
            }

            if (value.Parameters.ProxyUrl != null)
            {
                writer.WritePropertyName("proxy_url");
                writer.WriteStringValue(value.Parameters.ProxyUrl);
            }

            if (value.Parameters.Url != null)
            {
                writer.WritePropertyName("url");
                writer.WriteStringValue(value.Parameters.Url);
            }

            if (value.Parameters.Width != null)
            {
                writer.WritePropertyName("width");
                writer.WriteNumberValue(value.Parameters.Width.Value);
            }

            if (value.Parameters.Height != null)
            {
                writer.WritePropertyName("height");
                writer.WriteNumberValue(value.Parameters.Height.Value);
            }
            
            if(value.Parameters.Ephemeral != null)
            {
                writer.WritePropertyName("ephemeral");
                writer.WriteBooleanValue(value.Parameters.Ephemeral.Value);
            }

            if (value.Parameters.Duration != null)
            {
                writer.WritePropertyName("duration_secs");
                writer.WriteNumberValue((float)value.Parameters.Duration.Value.TotalSeconds);
            }

            if (value.Parameters.Waveform != null)
            {
                writer.WritePropertyName("waveform");
                writer.WriteStringValue(value.Parameters.Waveform);
            }

            if (value.Parameters.IsRemix)
            {
                writer.WritePropertyName("flags");
                writer.WriteNumberValue(1 << 2);
            }
        }
        
        writer.WriteEndObject();
    }
}