using System.Net.Http;
using System.Text;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Common.Http.Extensions;

public static class HttpClientExtension
{
    public static Task<HttpResponseMessage> PostAsync<TRequest>(this HttpClient client,
        TRequest requestPayload, string endpoint, JsonSerializerSettings serializerSettings, CancellationToken cancellationToken)
    {
        var httpContent = new StringContent(JsonConvert.SerializeObject(requestPayload, serializerSettings), Encoding.UTF8,
            MediaTypeNames.Application.Json);
        return client.PostAsync(endpoint, httpContent, cancellationToken);
    }

    public static Task<HttpResponseMessage> PostAsync<TRequest>(this HttpClient client,
        TRequest requestPayload, string endpoint, CancellationToken cancellationToken)
    {
        return client.PostAsync(requestPayload, endpoint, new JsonSerializerSettings(), cancellationToken);
    }
}
