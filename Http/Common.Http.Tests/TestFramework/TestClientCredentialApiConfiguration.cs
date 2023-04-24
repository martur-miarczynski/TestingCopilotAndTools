using Common.Http.Interfaces.Authentication;

namespace Common.Http.Tests.TestFramework;

public class TestClientCredentialApiConfiguration : IClientCredentialAuthenticationConfiguration
{
    public string ClientKey { get; set; }
    public string ClientSecret { get; set; }
    public string AuthenticationApiUrl { get; set; }
    public string AuthenticationClientName { get; set; }
}