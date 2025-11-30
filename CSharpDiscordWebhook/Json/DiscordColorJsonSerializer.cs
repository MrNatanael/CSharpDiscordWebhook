using System;
using System.Drawing;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CSharpDiscordWebhook.Json;

public class DiscordColorJsonSerializer : JsonConverter<Color>
{
    public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options)
    {
        uint rgba = (uint)((value.R << 16) | (value.G << 8) | value.B);
        writer.WriteNumberValue(rgba);
    }
}