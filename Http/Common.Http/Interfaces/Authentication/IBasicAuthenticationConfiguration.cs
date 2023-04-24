namespace Common.Http.Interfaces.Authentication;

public interface IBasicAuthenticationConfiguration : IAuthenticationConfiguration
{
    public string UserName { get; }
    public string Password { get; }
}