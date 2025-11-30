using System;
using System.IO;

namespace CSharpDiscordWebhook.Objects;

public abstract class AttachmentBuilder : IAttachmentStreamProvider
{
    public abstract void Dispose();
    protected internal abstract Stream Open();

    Stream IAttachmentStreamProvider.Open()
    {
        return this.Open();
    }

    public uint Id { get; set; }
    public string Filename { get; set; } = string.Empty;

    public AttachmentParameters? Parameters { get; set; }
}

public class AttachmentParameters
{
    public AttachmentParameters()
    {
    }

    public AttachmentParameters(Attachment attachment)
    {
        Title = attachment.Title;
        Description = attachment.Description;
        MimeType = attachment.ContentType;
        Size = attachment.Size;
        Url = attachment.Url;
        ProxyUrl = attachment.ProxyUrl;
        Width = attachment.Width;
        Height = attachment.Height;
        Ephemeral = attachment.Ephemeral;
        Duration = attachment.Duration;
        Waveform = attachment.Waveform;
        IsRemix =
            attachment.Flags.HasValue && attachment.Flags.Value.HasFlag(AttachmentFlags.IS_REMIX);
    }

    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? MimeType { get; set; }
    public ulong? Size { get; set; }
    public string? Url { get; set; }
    public string? ProxyUrl { get; set; }
    public uint? Width { get; set; }
    public uint? Height { get; set; }
    public bool? Ephemeral { get; set; }
    public TimeSpan? Duration { get; set; }
    public string? Waveform { get; set; }
    public bool IsRemix { get; set; }
}

#region PROVIDERS

public interface IAttachmentStreamProvider : IDisposable
{
    public Stream Open();
}

public class FileAttachmentProvider : IAttachmentStreamProvider
{
    public void Dispose()
    {
        _fs?.Dispose();
    }

    public Stream Open()
    {
        if (_fs != null) return _fs;
        return (_fs = Source?.OpenRead())!;
    }

    public FileInfo? Source { get; set; }
    private FileStream? _fs;
}

public class StreamAttachmentProvider : IAttachmentStreamProvider
{
    public void Dispose()
    {
    }

    public Stream Open() => Stream!;

    public Stream? Stream { get; set; }
}

#endregion

#region BUILDERS

public class FileAttachmentBuilder : AttachmentBuilder
{
    public override void Dispose()
    {
        _provider.Dispose();
    }

    protected internal override Stream Open()
    {
        return _provider.Open();
    }

    public FileInfo? Source
    {
        get => _provider.Source;
        set => _provider.Source = value;
    }

    private readonly FileAttachmentProvider _provider = new();
}

public class StreamAttachmentBuilder : AttachmentBuilder
{
    public override void Dispose() => _provider.Dispose();
    protected internal override Stream Open() => _provider.Open();

    public Stream? Stream
    {
        get => _provider.Stream;
        set => _provider.Stream = value;
    }
    private readonly StreamAttachmentProvider _provider = new();
}

#endregion