using System;
using System.IO;

namespace CSharpDiscordWebhook.Objects;

public abstract class AttachmentBuilder : IAttachmentStreamProvider
{
    public abstract void Dispose();
    protected internal abstract Stream Open();

    Stream IAttachmentStreamProvider.Open()
    {
        return Open();
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
    /// <summary>
    /// Open attachment stream
    /// </summary>
    /// <returns>Opened stream</returns>
    public Stream Open();
}

public class FileAttachmentProvider : IAttachmentStreamProvider
{
    /// <summary>
    /// Dispose file stream
    /// </summary>
    public void Dispose()
    {
        _fs?.Dispose();
    }

    /// <summary>
    /// Open file stream
    /// </summary>
    /// <returns>File stream</returns>
    public Stream Open()
    {
        if (_fs != null) return _fs;
        return (_fs = Source?.OpenRead())!;
    }

    /// <summary>
    /// Source file
    /// </summary>
    public FileInfo? Source { get; set; }

    private FileStream? _fs;
}

public class StreamAttachmentProvider : IAttachmentStreamProvider
{
    /// <summary>
    /// The provided stream is *NOT* disposed by this function, you need to dispose it yourself
    /// </summary>
    public void Dispose()
    {
    }

    /// <summary>
    /// Returns the provided stream
    /// </summary>
    /// <returns>The provided stream</returns>
    public Stream Open()
    {
        return Stream!;
    }

    /// <summary>
    /// Provided stream
    /// </summary>
    public Stream? Stream { get; set; }
}

#endregion

#region BUILDERS

public class FileAttachmentBuilder : AttachmentBuilder
{
    /// <summary>
    /// Dispose file stream
    /// </summary>
    public override void Dispose()
    {
        _provider.Dispose();
    }

    /// <summary>
    /// Open file stream
    /// </summary>
    /// <returns>File stream</returns>
    protected internal override Stream Open()
    {
        return _provider.Open();
    }

    /// <summary>
    /// Source file
    /// </summary>
    public FileInfo? Source
    {
        get => _provider.Source;
        set => _provider.Source = value;
    }

    private readonly FileAttachmentProvider _provider = new();
}

public class StreamAttachmentBuilder : AttachmentBuilder
{
    /// <summary>
    /// The provided stream is *NOT* disposed by this function, you need to dispose it yourself
    /// </summary>
    public override void Dispose()
    {
        _provider.Dispose();
    }

    /// <summary>
    /// Returns the provided stream
    /// </summary>
    /// <returns>The provided stream</returns>
    protected internal override Stream Open()
    {
        return _provider.Open();
    }

    /// <summary>
    /// Provided stream
    /// </summary>
    public Stream? Stream
    {
        get => _provider.Stream;
        set => _provider.Stream = value;
    }

    private readonly StreamAttachmentProvider _provider = new();
}

#endregion