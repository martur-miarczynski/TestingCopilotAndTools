using System;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Http.Interfaces;

/// <summary>
/// Simplify sending HTTP requests that read data and cache results.
/// </summary>
public interface ICachingHttpServiceClient
{
    /// <summary>
    /// Sends a POST request to httpClient with named passed as the parameters and cache results in memory.
    /// Empty response is not cached.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    /// <param name="cacheKey"></param>
    /// <param name="requestPayload"></param>
    /// <param name="clientName"></param>
    /// <param name="endpoint"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Deserialized response.</returns>
    Task<TResponse> PostOrRetrieveFromCacheAsync<TRequest, TResponse>(string cacheKey, TRequest requestPayload,
        string clientName, string endpoint, CancellationToken cancellationToken);

    /// <summary>
    /// Sends a POST request to httpClient with named passed as the parameters and cache results in memory.
    /// Accept options.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    /// <param name="cacheKey"></param>
    /// <param name="requestPayload"></param>
    /// <param name="clientName"></param>
    /// <param name="endpoint"></param>
    /// <param name="optionsBuilder"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TResponse?> PostOrRetrieveFromCacheAsync<TRequest, TResponse>(string cacheKey, TRequest requestPayload,
        string clientName, string endpoint, Action<HttpServiceOptions> optionsBuilder, CancellationToken cancellationToken);

    /// <summary>
    /// Sends a GET request to httpClient with query params passed as the parameters and cache results in memory.
    /// Accept options.
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    /// <param name="cacheKey"></param>
    /// <param name="clientName"></param>
    /// <param name="endpoint"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Deserialized response.</returns>
    Task<TResponse?> GetOrRetrieveFromCacheAsync<TResponse>(string cacheKey, string clientName, string endpoint,
        CancellationToken cancellationToken);

    /// <summary>
    /// Sends a GET request to httpClient with query params passed as the parameters and cache results in memory.
    /// Empty response is not cached.
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    /// <param name="cacheKey"></param>
    /// <param name="clientName"></param>
    /// <param name="endpoint"></param>
    /// <param name="optionsBuilder"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Deserialized response.</returns>
    Task<TResponse?> GetOrRetrieveFromCacheAsync<TResponse>(string cacheKey, string clientName, string endpoint,
        Action<HttpServiceOptions> optionsBuilder, CancellationToken cancellationToken);
}