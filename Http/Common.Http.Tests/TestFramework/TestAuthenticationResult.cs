using Common.Http.Interfaces.Authentication;

namespace Common.Http.Tests.TestFramework;

public class TestAuthenticationResult : IAuthenticationResult
{
    public string AccessToken { get; set; }
}