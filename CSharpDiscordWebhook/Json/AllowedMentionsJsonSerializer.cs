using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using CSharpDiscordWebhook.Objects;

namespace CSharpDiscordWebhook.Json;

public class AllowedMentionsJsonSerializer : JsonConverter<AllowedMentionType?>
{
    public override AllowedMentionType? Read(ref Utf8JsonReader reader, Type typeToConvert,
        JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null) return null;

        switch (reader.GetString())
        {
            case "everyone": return AllowedMentionType.Everyone;
            case "users": return AllowedMentionType.Users;
            case "roles": return AllowedMentionType.Roles;
            default: throw new NotSupportedException(reader.GetString());
        }
    }

    public override void Write(Utf8JsonWriter writer, AllowedMentionType? value, JsonSerializerOptions options)
    {
        if (value == null)
        {
            writer.WriteNullValue();
            return;
        }

        writer.WriteStringValue(value.Value.Value);
    }
}