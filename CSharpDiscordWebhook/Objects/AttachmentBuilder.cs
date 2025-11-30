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

    /// <summary>
    /// Attachment id
    /// </summary>
    public uint Id { get; set; }
    /// <summary>
    /// Name of file attached
    /// </summary>
    public string Filename { get; set; } = string.Empty;

    public AttachmentParameters Parameters { get; set; } = new();
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
    /// <summary>
    /// The title of the file
    /// </summary>
    public string? Title { get; set; }
    /// <summary>
    /// Description for the file (max 1024 characters)
    /// </summary>
    public string? Description { get; set; }
    /// <summary>
    /// The attachment's media type, see https://en.wikipedia.org/wiki/Media_type
    /// </summary>
    public string? MimeType { get; set; }
    /// <summary>
    /// Size of file in bytes
    /// </summary>
    public ulong? Size { get; set; }
    /// <summary>
    /// Source url of file
    /// </summary>
    public string? Url { get; set; }
    /// <summary>
    /// A proxied url of file
    /// </summary>
    public string? ProxyUrl { get; set; }
    /// <summary>
    /// Width of file (if image)
    /// </summary>
    public uint? Width { get; set; }
    /// <summary>
    /// Height of file (if image)
    /// </summary>
    public uint? Height { get; set; }
    /// <summary>
    /// Whether this attachment is ephemeral
    /// </summary>
    public bool? Ephemeral { get; set; }
    /// <summary>
    /// The duration of the audio file (currently for voice messages)
    /// </summary>
    public TimeSpan? Duration { get; set; }
    /// <summary>
    /// Base64 encoded bytearray representing a sampled waveform (currently for voice messages)
    /// </summary>
    public string? Waveform { get; set; }
    /// <summary>
    /// Attachment flags
    /// </summary>
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