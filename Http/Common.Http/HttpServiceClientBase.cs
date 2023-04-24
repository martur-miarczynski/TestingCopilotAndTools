using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Common.Http.Exception;
using Common.Http.Extensions;

namespace Common.Http;

public class HttpServiceClientBase
{
    private readonly IHttpClientFactory _httpClientFactory;

    protected HttpServiceClientBase(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    protected async Task<TResponse> HttpPostAndReadAsync<TRequest, TResponse>(TRequest requestPayload, string clientName,
        string endpoint, HttpServiceOptions httpServiceOptions, CancellationToken cancellationToken)
    {
        var httpClient = _httpClientFactory.CreateClient(clientName);
        var httpResponse = await httpClient.PostAsync(requestPayload, endpoint, httpServiceOptions!.JsonSerializerSettings, cancellationToken);

        return await ReadHttpResponseAsync<TResponse>(httpResponse, httpServiceOptions);
    }

    protected async Task<TResponse?> HttpGetAndReadAsync<TResponse>(string clientName, string requestUri, HttpServiceOptions httpServiceOptions, CancellationToken cancellationToken)
    {
        var httpClient = _httpClientFactory.CreateClient(clientName);
        var httpResponse = await httpClient.GetAsync(requestUri, cancellationToken);

        return await ReadHttpResponseAsync<TResponse>(httpResponse, httpServiceOptions);
    }

    protected static async Task<TResponse?> ReadHttpResponseAsync<TResponse>(HttpResponseMessage httpResponse, HttpServiceOptions httpServiceOptions)
    {
        var response = await httpResponse.ReadHttpResponseAsync<TResponse>(httpServiceOptions!.JsonSerializerSettings);
        return response is null && !httpServiceOptions.CacheEmptyResponse
            ? throw new EmptyResponseException("Response object is empty!")
            : response;
    }
}
