using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Common.Http.Exception;
using Common.Http.Interfaces.Authentication;
using Newtonsoft.Json;

namespace Common.Http.Authentication;

public abstract class AuthenticationContextBase<TAuthenticationConfiguration, TAuthenticationResult>
    where TAuthenticationConfiguration : IAuthenticationConfiguration
    where TAuthenticationResult : IAuthenticationResult
{
    protected readonly TAuthenticationConfiguration AuthenticationConfiguration;
    private readonly IHttpClientFactory _clientFactory;

    protected AuthenticationContextBase(TAuthenticationConfiguration authenticationConfiguration, IHttpClientFactory clientFactory)
    {
        AuthenticationConfiguration = authenticationConfiguration;
        _clientFactory = clientFactory;
    }

    public async Task<TAuthenticationResult> AcquireTokenAsync(CancellationToken cancellationToken)
    {
        var isUriValid = Uri.TryCreate(AuthenticationConfiguration!.AuthenticationApiUrl, UriKind.Absolute, out var uriResult) && uriResult.Scheme == Uri.UriSchemeHttps;

        if (!isUriValid)
        {
            throw new UriFormatException($"Cannot acquire token using given URL. The link has a bad format: {AuthenticationConfiguration.AuthenticationApiUrl}");
        }

        var client = _clientFactory.CreateClient(AuthenticationConfiguration.AuthenticationClientName);
        var message = BuildAuthenticationMessage();
        var response = await client.SendAsync(message, cancellationToken);

        response.EnsureSuccessStatusCode();

        var rawResult = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<TAuthenticationResult>(rawResult) ?? throw new ApiAuthenticationException($"Received an empty authentication response from  {AuthenticationConfiguration.AuthenticationApiUrl}");
    }

    protected abstract HttpRequestMessage BuildAuthenticationMessage();
}