using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Common.Http.Interfaces.Authentication;

namespace Common.Http.Authentication;

public class BasicAuthenticationContext<TAuthenticationResult> : AuthenticationContextBase<IBasicAuthenticationConfiguration, TAuthenticationResult>,
    IAuthenticationContext<TAuthenticationResult> where TAuthenticationResult : IAuthenticationResult
{
    public BasicAuthenticationContext(IBasicAuthenticationConfiguration authenticationConfiguration, IHttpClientFactory clientFactory) 
        : base(authenticationConfiguration, clientFactory)
    {
    }

    protected override HttpRequestMessage BuildAuthenticationMessage()
    {
        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, AuthenticationConfiguration.AuthenticationApiUrl);
        var credentials = Encoding.UTF8.GetBytes($"{AuthenticationConfiguration.UserName}:{AuthenticationConfiguration.Password}");
        var encodedCredentials = Convert.ToBase64String(credentials);
        httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", encodedCredentials);
        return httpRequestMessage;
    }
}