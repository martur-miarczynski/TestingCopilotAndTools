namespace Common.Http.Interfaces.Authentication
{
    public interface IBearerScopeAuthenticationConfiguration : IClientCredentialAuthenticationConfiguration
    {
        public string Scope { get; }
    }
}
