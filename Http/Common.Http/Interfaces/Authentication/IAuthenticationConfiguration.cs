namespace Common.Http.Interfaces.Authentication;

public interface IAuthenticationConfiguration
{
    public string AuthenticationApiUrl { get; }
    public string AuthenticationClientName { get; }
}