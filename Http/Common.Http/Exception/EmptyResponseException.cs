using System.Diagnostics.CodeAnalysis;

namespace Common.Http.Exception;

[ExcludeFromCodeCoverage]
public class EmptyResponseException : System.Exception
{
    public EmptyResponseException(string message) : base(message)
    {
    }
}
