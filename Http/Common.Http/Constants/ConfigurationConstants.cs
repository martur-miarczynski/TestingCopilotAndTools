namespace Common.Http.Constants;

public static class ConfigurationConstants
{
    public static readonly string CacheConfigurationKey = "HttpClient:CacheSlidingExpirationTimeSpan";
    public static readonly System.TimeSpan DefaultCacheSlidingExpiration = System.TimeSpan.FromMinutes(5);
}
