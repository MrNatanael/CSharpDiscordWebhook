using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using CSharpDiscordWebhook.Json;
using CSharpDiscordWebhook.Objects;

namespace CSharpDiscordWebhook;

/// <summary>
/// Discord webhook API wrapper
/// </summary>
public class DiscordWebhook : IDisposable
{
    /// <summary>
    /// Get webhook object
    /// </summary>
    /// <returns>API result</returns>
    public async Task<WebhookResult<Webhook>> GetAsync()
    {
        using var req = new HttpRequestMessage(HttpMethod.Get, string.Empty);
        return await SendAsync<Webhook>(req);
    }

    /// <summary>
    /// Modify webhook object
    /// </summary>
    /// <param name="callback">This is called with the current webhook information that can be modified</param>
    /// <returns>If successful, then modified webhook is returned</returns>
    public async Task<WebhookResult<Webhook>> ModifyAsync(ModifyWebhookCallback callback)
    {
        var r = await GetAsync();
        if (!r.Success) return r;

        var modify = new WebhookModify(r.Result!);
        callback(modify);

        return await SendJsonMessageAsync<Webhook, WebhookModify>(new HttpMethod("PATCH"), modify);
    }

    /// <summary>
    /// Delete webhook object
    /// </summary>
    /// <returns>True if the webhook object has been deleted</returns>
    public async Task<bool> DeleteWebhookAsync()
    {
        using var res = await Client.DeleteAsync(string.Empty);
        return res.StatusCode == HttpStatusCode.NoContent;
    }

    /// <summary>
    /// Get a message data
    /// </summary>
    /// <param name="id">The message id</param>
    /// <returns>If successful, the message information is returned</returns>
    public async Task<WebhookResult<Message>> GetMessageAsync(ulong id)
    {
        using var req = new HttpRequestMessage(HttpMethod.Get, $"/messages/{id}");
        return await SendAsync<Message>(req);
    }

    /// <summary>
    /// Edit message by its ID
    /// </summary>
    /// <param name="id">The message id</param>
    /// <param name="callback">This is called with the current message information that can be edited</param>
    /// <param name="withComponents">True if components should be sent</param>
    /// <returns>If successful, the modified message is returned</returns>
    public async Task<WebhookResult<Message>> EditMessageAsync(ulong id, ModifyMessageCallback callback,
        bool withComponents = false)
    {
        var src = await GetMessageAsync(id);
        if (!src.Success) return src;

        return await EditMessageAsync(src.Result!, callback, withComponents);
    }

    /// <summary>
    /// Edit message by using a message object as the source
    /// </summary>
    /// <param name="msg">The message object</param>
    /// <param name="callback"></param>
    /// <param name="withComponents">True if components should be sent</param>
    /// <returns>If successful, the modified message is returned</returns>
    public async Task<WebhookResult<Message>> EditMessageAsync(Message msg, ModifyMessageCallback callback,
        bool withComponents = false)
    {
        MessageModify modify = new(msg);
        callback(modify);

        var queriesStr = withComponents ? "?with_components=true" : string.Empty;

        if (modify.Attachments.Count == 0)
            return await EditJsonMessageAsync(modify, queriesStr);

        return await EditMessageWithAttachmentsAsync(modify, queriesStr);
    }

    /// <summary>
    /// Delete a message by its id
    /// </summary>
    /// <param name="id">The message id</param>
    /// <returns>True if the message has been deleted</returns>
    public async Task<WebhookResult<bool>> DeleteMessageAsync(ulong id)
    {
        using var msg = new HttpRequestMessage(HttpMethod.Delete, $"/messages/{id}");
        using var res = await Client.SendAsync(msg);

        if (res.IsSuccessStatusCode) return new WebhookResult<bool>(true, null);

        using var s = await res.Content.ReadAsStreamAsync();
        return new WebhookResult<bool>(false,
            await JsonSerializer.DeserializeAsync<ErrorMessage>(s, DefaultJsonOptions));
    }

    /// <summary>
    /// Send a message
    /// </summary>
    /// <param name="messageBuilder">Message parameters</param>
    /// <param name="wait">If true, the created message will be returned</param>
    /// <param name="withComponents">True if components should be sent</param>
    /// <returns>If successful and <param name="wait"/> is set to <value>true</value>, the created message is returned </returns>
    public async Task<WebhookResult<Message?>> SendMessageAsync(MessageBuilder messageBuilder, bool wait = false,
        bool withComponents = false)
    {
        List<string> queries = new();
        var queriesStr = string.Empty;

        if (wait) queries.Add("wait=true");
        if (withComponents) queries.Add("with_components=true");

        if (queries.Count > 0) queriesStr = $"?{string.Join("&", queries)}";

        if (messageBuilder.Attachments.Count > 0)
            return await SendMessageWithAttachmentsAsync(messageBuilder, queriesStr);

        return await SendJsonMessageAsync<Message?, MessageBuilder>(HttpMethod.Post, messageBuilder, queriesStr);
    }

    /// <summary>
    /// Dispose webhook wrapper
    /// </summary>
    public void Dispose()
    {
        Client.Dispose();
    }

    private async Task<WebhookResult<TResult>> SendJsonMessageAsync<TResult, TJson>(HttpMethod method, TJson json,
        string subPath = "")
    {
        using var req = new HttpRequestMessage(method, $"/{subPath}");

        // JsonContent is not available on .net standard 2.0
        var jsonStr = JsonSerializer.Serialize(json, DefaultJsonOptions);
        req.Content = new StringContent(jsonStr, Encoding.UTF8, "application/json");

        return await SendAsync<TResult>(req);
    }

    private async Task<WebhookResult<Message?>> SendMessageWithAttachmentsAsync(MessageBuilder messageBuilder,
        string queriesStr)
    {
        var content = new MultipartFormDataContent();
        var json = JsonSerializer.Serialize(messageBuilder, DefaultJsonOptions);

        content.Add(new StringContent(json, Encoding.UTF8, "application/json"), "payload_json");

        // Now add files
        foreach (var attachment in messageBuilder.Attachments)
        {
            var stream = new StreamContent(attachment.Open());
            stream.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            content.Add(stream, $"files[{attachment.Id}]", attachment.Filename);
        }

        using var req = new HttpRequestMessage(HttpMethod.Post, $"/{queriesStr}");
        req.Content = content;

        var r = await SendAsync<Message?>(req);
        foreach (var attachment in messageBuilder.Attachments)
            attachment.Dispose();

        return r;
    }

    private async Task<WebhookResult<Message>> EditJsonMessageAsync(MessageModify modify, string subPath)
    {
        using var req = new HttpRequestMessage(new HttpMethod("PATCH"), $"/messages/{modify.Id}{subPath}");

        // JsonContent is not available on .net standard 2.0
        var jsonStr = JsonSerializer.Serialize(modify, DefaultJsonOptions);
        req.Content = new StringContent(jsonStr, Encoding.UTF8, "application/json");

        return await SendAsync<Message>(req);
    }

    private async Task<WebhookResult<Message>> EditMessageWithAttachmentsAsync(MessageModify modify, string subPath)
    {
        var content = new MultipartFormDataContent();
        var json = JsonSerializer.Serialize(modify, DefaultJsonOptions);

        content.Add(new StringContent(json, Encoding.UTF8, "application/json"), "payload_json");

        // Now add files
        foreach (var attachment in modify.Attachments)
        {
            if (attachment.StreamProvider == null) continue;

            var stream = new StreamContent(attachment.StreamProvider.Open());
            stream.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            content.Add(stream, $"files[{attachment.Id}]", attachment.Filename);
        }

        using var req = new HttpRequestMessage(new HttpMethod("PATCH"), $"/messages/{modify.Id}{subPath}");
        req.Content = content;

        var r = await SendAsync<Message>(req);
        foreach (var attachment in modify.Attachments)
            attachment.StreamProvider?.Dispose();

        return r;
    }

    private async Task<WebhookResult<T>> SendAsync<T>(HttpRequestMessage request)
    {
        using var resp = await Client.SendAsync(request);
        using var s = await resp.Content.ReadAsStreamAsync();

        if (resp.IsSuccessStatusCode)
            return new WebhookResult<T>(
                s.Length > 0 ? await JsonSerializer.DeserializeAsync<T>(s, DefaultJsonOptions) : default, null);

        return new WebhookResult<T>(default,
            await JsonSerializer.DeserializeAsync<ErrorMessage>(s, DefaultJsonOptions));
    }

    /// <summary>
    /// Create webhook wrapper from URL
    /// </summary>
    /// <param name="url">Webhook URL</param>
    /// <exception cref="FormatException">Thrown if the URL format is not a valid Discord webhook URL</exception>
    public DiscordWebhook(Uri url)
    {
        var match = _uriValidator.Match(url.AbsoluteUri);
        if (!match.Success)
            throw new FormatException("Invalid webhook url format");

        if (!match.Groups[1].Success)
            url = new Uri($"{API_PATH}/v{API_VERSION}/webhooks/{match.Groups[2].Value}/{match.Groups[3].Value}");

        Url = url;
        var handler = new DiscordWebhookHttpHandler(url)
        {
#if DEBUG
            Proxy = new WebProxy("http://127.0.0.1:8080"),
            UseProxy = true
#endif
        };
        Client = new HttpClient(handler);
        Client.BaseAddress = new Uri("https://discord.com/"); // Will be overriden by our handler
    }

    /// <summary>
    /// Create webhook wrapper from ID and Token
    /// </summary>
    /// <param name="id">Webhook ID</param>
    /// <param name="token">Webhook token</param>
    public DiscordWebhook(ulong id, string token) : this(new Uri($"{API_PATH}/v{API_VERSION}/webooks/{id}/{token}"))
    {
    }

    /// <summary>
    /// Webhook URL
    /// </summary>
    public Uri Url { get; }

    private HttpClient Client { get; }

    private static JsonSerializerOptions DefaultJsonOptions { get; } = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        NumberHandling = JsonNumberHandling.AllowReadingFromString,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
        Converters =
        {
            new AllowedMentionsJsonSerializer(),
            new PollBuilderJsonSerializer(),
            new DiscordColorJsonSerializer(),
            new DiscordDateTimeJsonSerializer(),
            new DiscordAttachmentsJsonSerializer(),
            new DiscordAttachmentsModifyJsonSerializer()
        }
    };

    /// <summary>
    /// Base API path
    /// </summary>
    public const string API_PATH = "https://discord.com/api";

    /// <summary>
    /// Default API version
    /// </summary>
    public const int API_VERSION = 10;

    private static readonly Regex _uriValidator =
        new(@"https:\/\/discord.com\/api(\/v\d+)?\/webhooks\/(\d+)\/([\w\W]+)");
}

internal class DiscordWebhookHttpHandler(Uri basePath) : HttpClientHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var abs = request.RequestUri.AbsolutePath;
        if (abs.EndsWith("/"))
            abs = abs.Remove(abs.Length - 1);

        StringBuilder sb = new();
        sb.Append(abs);
        sb.Append(request.RequestUri.Query);

        request.RequestUri = new Uri(BasePath.AbsoluteUri + sb);
        return base.SendAsync(request, cancellationToken);
    }

    protected Uri BasePath { get; } = basePath;
}

/// <summary>
/// Webhook message modify callback
/// </summary>
public delegate void ModifyMessageCallback(MessageModify modify);

/// <summary>
/// Webhook object modify callback
/// </summary>
public delegate void ModifyWebhookCallback(WebhookModify modify);