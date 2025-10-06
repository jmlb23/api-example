using System.Threading;
using System.Threading.Tasks;

namespace api.Features.Auth.Domain.Command;

public interface IAuthUserCommand
{
    Task<AuthResult?> ExecuteAsync(
        Auth credentials, 
        CancellationToken ct = default);
}