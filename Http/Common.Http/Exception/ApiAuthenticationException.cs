namespace Common.Http.Exception;

public class ApiAuthenticationException : System.Exception
{
    public ApiAuthenticationException(string message) : base(message)
    {
    }
}