using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using CSharpDiscordWebhook.Json;
using CSharpDiscordWebhook.Objects;

namespace CSharpDiscordWebhook
{   
    public class DiscordWebhook : IDisposable
    {
        public async Task<WebhookResult<Webhook>> GetAsync()
        {
            using var req = new HttpRequestMessage(HttpMethod.Get, Url);
            return await SendAsync<Webhook>(req);
        }
        public async Task<WebhookResult<Webhook>> ModifyAsync(WebhookModify modify)
        {
            return await SendJsonMessageAsync<Webhook, WebhookModify>(new("PATCH"), modify);
        }
        public async Task<bool> DeleteWebhookAsync()
        {
            using var res = await Client.DeleteAsync(Url);
            return res.StatusCode == HttpStatusCode.NoContent;
        }
        
        public async Task<WebhookResult<Message?>> ExecuteAsync(MessageBuilder messageBuilder)
        {
            return await SendJsonMessageAsync<Message?, MessageBuilder>(HttpMethod.Post, messageBuilder);
            /*
            StringBuilder boundary = new();
            boundary.Append("------------------------");
            boundary.Append(DateTime.Now.Ticks);

            MultipartFormDataContent httpContent = new(boundary.ToString());
            foreach (var s in streams)
            {
                if (s.CanSeek) s.Position = 0;
                StreamContent sContent = new(s);
                sContent.Headers.ContentType = new("application/octet-stream");
                httpContent.Add(sContent);
            }

            // JsonContent is not available on .net standard 2.0
            using var msgStream = new MemoryStream();
            await JsonSerializer.SerializeAsync(msgStream, messageBuilder, DefaultJsonOptions);

            msgStream.Position = 0;

            StreamContent msgContent = new(msgStream);
            msgContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            httpContent.Add(msgContent);

            using var req = new HttpRequestMessage(HttpMethod.Post, Url);
            req.Content = httpContent;
            return await SendAsync<Message>(req);
            */
        }
        
        public void Dispose() {Client.Dispose(); }

        async Task<WebhookResult<TResult>> SendJsonMessageAsync<TResult, TJson>(HttpMethod method, TJson json)
        {
            using var req = new HttpRequestMessage(method, Url);
            
            // JsonContent is not available on .net standard 2.0
            using var s = new MemoryStream();
            await JsonSerializer.SerializeAsync(s, json, DefaultJsonOptions);

            s.Position = 0;
            req.Content = new StreamContent(s);
            req.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            
            return await SendAsync<TResult>(req);
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
            if(!match.Success)
                throw new FormatException("Invalid webhook url format");

            if (!match.Groups[1].Success)
                url = new Uri($"{API_PATH}/v{API_VERSION}/webhooks/{match.Groups[2].Value}/{match.Groups[3].Value}");
                
            Url = url;

            var proxy = new HttpClientHandler()
            {
                Proxy = new WebProxy("http://127.0.0.1:8080"),
                UseProxy = true
            };
            Client = new(proxy);
        }
        public DiscordWebhook(ulong id, string token) : this(new Uri($"{API_PATH}/v{API_VERSION}/webooks/{id}/{token}")) {}

        public Uri Url { get; }
        private HttpClient Client { get; }

        private static JsonSerializerOptions DefaultJsonOptions { get; } = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
            Converters =
            {
                new AllowedMentionsJsonSerializer(),
                new PollBuilderJsonSerializer()
            }
        };

        public const string API_PATH = "https://discord.com/api";
        public const int API_VERSION = 10;

        private static readonly Regex _uriValidator = new(@"https:\/\/discord.com\/api(\/v\d+)?\/webhooks\/(\d+)\/([\w\W]+)");
    }
}