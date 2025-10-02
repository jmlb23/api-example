using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace api.Features.Users.Domain.Query;

public interface IGetUsersQuery
{
    Task<IEnumerable<User>> ExecuteAsync(CancellationToken cancel);
}