using System.Collections.Generic;
using System.Net.Http;
using Common.Http.Constants;
using Common.Http.Interfaces.Authentication;

namespace Common.Http.Authentication
{
    public class BearerAudienceAuthenticationContext<TAuthenticationResult> : AuthenticationContextBase<IBearerScopeAuthenticationConfiguration, TAuthenticationResult>,
        IAuthenticationContext<TAuthenticationResult> where TAuthenticationResult : IAuthenticationResult
    {
        public BearerAudienceAuthenticationContext(IBearerScopeAuthenticationConfiguration clientCredentialAuthenticationConfiguration, IHttpClientFactory clientFactory)
            : base(clientCredentialAuthenticationConfiguration, clientFactory)
        {
        }

        protected override HttpRequestMessage BuildAuthenticationMessage()
        {
            var values = new Dictionary<string, string>
            {
                { AuthenticationPayloadConstants.ClientId, AuthenticationConfiguration.ClientKey },
                { AuthenticationPayloadConstants.ClientSecret, AuthenticationConfiguration.ClientSecret },
                { AuthenticationPayloadConstants.Scope, AuthenticationConfiguration.Scope},
                { AuthenticationPayloadConstants.GrantType, AuthenticationPayloadConstants.GrantTypeClientCredential }
            };

            var content = new FormUrlEncodedContent(values);

            return new HttpRequestMessage(HttpMethod.Post, AuthenticationConfiguration.AuthenticationApiUrl)
            {
                Content = content
            };
        }
    }
}
