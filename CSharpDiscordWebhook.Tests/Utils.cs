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
    
    public static byte[] MakeWav(int samples = 882000, float freq = 400) // 20 seconds by default
    {
        const int sampleRate = 44100;
        const short channels = 1;
        const short bits = 16;

        int dataSize = samples * channels * (bits / 8);

        byte[] wav = new byte[44 + dataSize];

        Buffer.BlockCopy("RIFF"u8.ToArray(), 0, wav, 0, 4);
        BitConverter.GetBytes(36 + dataSize).CopyTo(wav, 4);
        Buffer.BlockCopy("WAVE"u8.ToArray(), 0, wav, 8, 4);

        Buffer.BlockCopy("fmt "u8.ToArray(), 0, wav, 12, 4);
        BitConverter.GetBytes(16).CopyTo(wav, 16);
        BitConverter.GetBytes((short)1).CopyTo(wav, 20);
        BitConverter.GetBytes(channels).CopyTo(wav, 22);
        BitConverter.GetBytes(sampleRate).CopyTo(wav, 24);
        BitConverter.GetBytes(sampleRate * channels * (bits / 8)).CopyTo(wav, 28);
        BitConverter.GetBytes((short)(channels * (bits / 8))).CopyTo(wav, 32);
        BitConverter.GetBytes(bits).CopyTo(wav, 34);

        Buffer.BlockCopy("data"u8.ToArray(), 0, wav, 36, 4);
        BitConverter.GetBytes(dataSize).CopyTo(wav, 40);

        // Simple sine wave
        int offset = 44;
        double step = 2.0 * Math.PI * freq / sampleRate;

        for (int i = 0; i < samples; i++)
        {
            short sample = (short)(Math.Sin(i * step) * short.MaxValue);
            BitConverter.GetBytes(sample).CopyTo(wav, offset);
            offset += 2;
        }
        return wav;
    }
}