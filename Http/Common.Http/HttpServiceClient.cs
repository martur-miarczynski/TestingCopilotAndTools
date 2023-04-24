using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Common.Http.Interfaces;

namespace Common.Http;

public class HttpServiceClient : HttpServiceClientBase, IHttpServiceClient
{
    public HttpServiceClient(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
    }

    public Task<TResponse> GetAsync<TResponse>(string clientName, string endpoint, CancellationToken cancellationToken)
    {
        var options = new HttpServiceOptions { CacheEmptyResponse = false };
        return HttpGetAndReadAsync<TResponse>(clientName, endpoint, options, cancellationToken);
    }
}
