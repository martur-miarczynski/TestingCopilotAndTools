using System.Threading;
using System.Threading.Tasks;

namespace Common.Http.Interfaces.Authentication;

public interface IAuthenticationContext<TAuthenticationResult> where TAuthenticationResult : IAuthenticationResult

{
    Task<TAuthenticationResult> AcquireTokenAsync(CancellationToken cancellationToken = default);
}