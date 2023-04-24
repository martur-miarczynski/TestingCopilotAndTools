using Common.Http.Interfaces.Authentication;

namespace Common.Http.Tests.TestFramework;

public class TestBasicApiConfiguration : IBasicAuthenticationConfiguration
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string AuthenticationApiUrl { get; set; }
    public string AuthenticationClientName { get; set; }
}