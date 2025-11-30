using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using CSharpDiscordWebhook.Json;
using CSharpDiscordWebhook.Objects;

namespace CSharpDiscordWebhook;

public class DiscordWebhook : IDisposable
{
    public async Task<WebhookResult<Webhook>> GetAsync()
    {
        using var req = new HttpRequestMessage(HttpMethod.Get, string.Empty);
        return await SendAsync<Webhook>(req);
    }

    public async Task<WebhookResult<Webhook>> ModifyAsync(ModifyWebhookCallback callback)
    {
        var r = await GetAsync();
        if(!r.Success) return r;

        var modify = new WebhookModify(r.Result!);
        callback(modify);
        
        return await SendJsonMessageAsync<Webhook, WebhookModify>(new("PATCH"), modify);
    }

    public async Task<bool> DeleteWebhookAsync()
    {
        using var res = await Client.DeleteAsync(string.Empty);
        return res.StatusCode == HttpStatusCode.NoContent;
    }

    public async Task<WebhookResult<Message>> GetMessageAsync(ulong id)
    {
        using var req = new HttpRequestMessage(HttpMethod.Get, $"/messages/{id}");
        return await SendAsync<Message>(req);
    }

    public async Task<WebhookResult<Message>> EditMessageAsync(ulong id, ModifyMessageCallback callback,
        bool withComponents = false)
    {
        var src = await GetMessageAsync(id);
        if (!src.Success) return src;

        return await EditMessageAsync(src.Result!, callback, withComponents);
    }

    public async Task<WebhookResult<Message>> EditMessageAsync(Message msg, ModifyMessageCallback callback,
        bool withComponents = false)
    {
        MessageModify modify = new(msg);
        callback(modify);

        string queriesStr = withComponents ? "?with_components=true" : string.Empty;

        if (modify.Attachments.Count == 0)
            return await EditJsonMessageAsync(modify, queriesStr);

        return await EditMessageWithAttachmentsAsync(modify, queriesStr);
    }

    public async Task<WebhookResult<bool>> DeleteMessageAsync(ulong id)
    {
        using var msg = new HttpRequestMessage(HttpMethod.Delete, $"/messages/{id}");
        using var res = await Client.SendAsync(msg);

        if (res.IsSuccessStatusCode) return new(true, null);

        using var s = await res.Content.ReadAsStreamAsync();
        return new(false, await JsonSerializer.DeserializeAsync<ErrorMessage>(s, DefaultJsonOptions));
    }

    public async Task<WebhookResult<Message?>> SendMessageAsync(MessageBuilder messageBuilder, bool wait = false,
        bool withComponents = false)
    {
        List<string> queries = new();
        string queriesStr = string.Empty;

        if (wait) queries.Add("wait=true");
        if (withComponents) queries.Add("with_components=true");

        if (queries.Count > 0) queriesStr = $"?{string.Join("&", queries)}";

        if (messageBuilder.Attachments.Count > 0)
            return await SendMessageWithAttachmentsAsync(messageBuilder, queriesStr);

        return await SendJsonMessageAsync<Message?, MessageBuilder>(HttpMethod.Post, messageBuilder, queriesStr);
    }

    public void Dispose()
    {
        Client.Dispose();
    }

    async Task<WebhookResult<TResult>> SendJsonMessageAsync<TResult, TJson>(HttpMethod method, TJson json,
        string subPath = "")
    {
        using var req = new HttpRequestMessage(method, $"/{subPath}");

        // JsonContent is not available on .net standard 2.0
        string jsonStr = JsonSerializer.Serialize(json, DefaultJsonOptions);
        req.Content = new StringContent(jsonStr, Encoding.UTF8, "application/json");

        return await SendAsync<TResult>(req);
    }

    async Task<WebhookResult<Message?>> SendMessageWithAttachmentsAsync(MessageBuilder messageBuilder,
        string queriesStr)
    {
        var content = new MultipartFormDataContent();
        var json = JsonSerializer.Serialize(messageBuilder, DefaultJsonOptions);

        content.Add(new StringContent(json, Encoding.UTF8, "application/json"), "payload_json");

        // Now add files
        foreach (var attachment in messageBuilder.Attachments)
        {
            var stream = new StreamContent(attachment.Open());
            stream.Headers.ContentType = new("application/octet-stream");

            content.Add(stream, $"files[{attachment.Id}]", attachment.Filename);
        }

        using var req = new HttpRequestMessage(HttpMethod.Post, $"/{queriesStr}");
        req.Content = content;

        var r = await SendAsync<Message?>(req);
        foreach (var attachment in messageBuilder.Attachments)
            attachment.Dispose();

        return r;
    }

    async Task<WebhookResult<Message>> EditJsonMessageAsync(MessageModify modify, string subPath)
    {
        using var req = new HttpRequestMessage(new HttpMethod("PATCH"), $"/messages/{modify.Id}{subPath}");

        // JsonContent is not available on .net standard 2.0
        string jsonStr = JsonSerializer.Serialize(modify, DefaultJsonOptions);
        req.Content = new StringContent(jsonStr, Encoding.UTF8, "application/json");

        return await SendAsync<Message>(req);
    }

    async Task<WebhookResult<Message>> EditMessageWithAttachmentsAsync(MessageModify modify, string subPath)
    {
        var content = new MultipartFormDataContent();
        var json = JsonSerializer.Serialize(modify, DefaultJsonOptions);

        content.Add(new StringContent(json, Encoding.UTF8, "application/json"), "payload_json");

        // Now add files
        foreach (var attachment in modify.Attachments)
        {
            if (attachment.StreamProvider == null) continue;

            var stream = new StreamContent(attachment.StreamProvider.Open());
            stream.Headers.ContentType = new("application/octet-stream");

            content.Add(stream, $"files[{attachment.Id}]", attachment.Filename);
        }

        using var req = new HttpRequestMessage(new HttpMethod("PATCH"), $"/messages/{modify.Id}{subPath}");
        req.Content = content;

        var r = await SendAsync<Message>(req);
        foreach (var attachment in modify.Attachments)
            attachment.StreamProvider?.Dispose();

        return r;
    }

    async Task<WebhookResult<T>> SendAsync<T>(HttpRequestMessage request)
    {
        using var resp = await Client.SendAsync(request);
        using var s = await resp.Content.ReadAsStreamAsync();

        if (resp.IsSuccessStatusCode)
        {
            return new(s.Length > 0 ? await JsonSerializer.DeserializeAsync<T>(s, DefaultJsonOptions) : default, null);
        }

        return new(default, await JsonSerializer.DeserializeAsync<ErrorMessage>(s, DefaultJsonOptions));
    }

    public DiscordWebhook(Uri url)
    {
        var match = _uriValidator.Match(url.AbsoluteUri);
        if (!match.Success)
            throw new FormatException("Invalid webhook url format");

        if (!match.Groups[1].Success)
            url = new Uri($"{API_PATH}/v{API_VERSION}/webhooks/{match.Groups[2].Value}/{match.Groups[3].Value}");

        Url = url;
        var proxy = new DiscordWebhookHttpHandler(url)
        {
            Proxy = new WebProxy("http://127.0.0.1:8080"),
            UseProxy = true
        };
        Client = new(proxy);
        Client.BaseAddress = new("https://discord.com/"); // Will be overriden by our handler
    }
    public DiscordWebhook(ulong id, string token) : this(new Uri($"{API_PATH}/v{API_VERSION}/webooks/{id}/{token}")) { }

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

    public const string API_PATH = "https://discord.com/api";
    public const int API_VERSION = 10;

    private static readonly Regex _uriValidator =
        new(@"https:\/\/discord.com\/api(\/v\d+)?\/webhooks\/(\d+)\/([\w\W]+)");
}

class DiscordWebhookHttpHandler(Uri basePath) : HttpClientHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        string abs = request.RequestUri.AbsolutePath;
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

public delegate void ModifyMessageCallback(MessageModify modify);
public delegate void ModifyWebhookCallback(WebhookModify modify);
