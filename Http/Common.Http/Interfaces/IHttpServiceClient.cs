using System.Threading;
using System.Threading.Tasks;

namespace Common.Http.Interfaces;

/// <summary>
/// Simplify sending HTTP requests. No caching.
/// </summary>
public interface IHttpServiceClient
{
    /// <summary>
    /// Sends a GET request to httpClient with query params passed as the parameters. Results are not cached.
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    /// <param name="clientName"></param>
    /// <param name="endpoint"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Deserialized response.</returns>
    Task<TResponse> GetAsync<TResponse>(string clientName, string endpoint, CancellationToken cancellationToken);
}
