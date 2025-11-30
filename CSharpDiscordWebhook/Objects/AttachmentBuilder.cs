using System;
using System.IO;

namespace CSharpDiscordWebhook.Objects;

public abstract class AttachmentBuilder : IDisposable
{
    public abstract void Dispose();
    protected internal abstract Stream Open();
    
    public uint Id { get; set; }
    public string Filename { get; set; } = string.Empty;

    public AttachmentParameters? Parameters { get; set; }
}

public class AttachmentParameters
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? MimeType { get; set; }
    public uint? Size { get; set; }
    public string? Url { get; set; }
    public string? ProxyUrl { get; set; }
    public uint? Width { get; set; }
    public uint? Height { get; set; }
    public bool? Ephemeral { get; set; }
    public TimeSpan? Duration { get; set; }
    public string? Waveform { get; set; }
    public bool IsRemix { get; set; }
}

public class FileAttachmentBuilder : AttachmentBuilder
{
    public override void Dispose()
    {
        _fs?.Dispose();
    }

    protected internal override Stream Open()
    {
        if (_fs != null) return _fs;
        return (_fs = Source?.OpenRead())!;
    }

    public FileInfo? Source { get; set; }
    private FileStream? _fs;
}

public class StreamAttachmentBuilder : AttachmentBuilder
{
    public override void Dispose() {}
    protected internal override Stream Open() => Stream!;

    public Stream? Stream { get; set; }
}