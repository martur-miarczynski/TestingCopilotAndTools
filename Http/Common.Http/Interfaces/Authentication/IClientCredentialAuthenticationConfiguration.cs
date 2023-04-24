namespace Common.Http.Interfaces.Authentication;

public interface IClientCredentialAuthenticationConfiguration : IAuthenticationConfiguration
{
    public string ClientKey { get; }
    public string ClientSecret { get; }
}