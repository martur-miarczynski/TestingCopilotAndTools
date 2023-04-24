using System.Net.Http;
using Common.Http.Interfaces.Authentication;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Http.DelegatingHandlers;

public class BearerTokenDelegatingHandler<TApiConfiguration, TAuthenticationResult> : DelegatingHandler 
    where TApiConfiguration : IAuthenticationConfiguration
    where TAuthenticationResult : IAuthenticationResult
{
    private readonly IAuthenticationContext<TAuthenticationResult> _authenticationContext;

    public BearerTokenDelegatingHandler(IAuthenticationContext<TAuthenticationResult> authenticationContext, TApiConfiguration apiConfiguration)
    {
        _authenticationContext = authenticationContext;
    }

    protected virtual async Task<string> GenerateBearerToken(CancellationToken cancellationToken)
    {
        var tokenResponse = await _authenticationContext.AcquireTokenAsync(cancellationToken);
        if (tokenResponse is null)
        {
            throw new HttpRequestException("Token is null");
        }
        return tokenResponse.AccessToken;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var bearerToken = await GenerateBearerToken(cancellationToken);
        request!.Headers.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

        return await base.SendAsync(request, cancellationToken);
    }
}