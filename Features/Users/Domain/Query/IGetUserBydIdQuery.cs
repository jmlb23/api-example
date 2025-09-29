namespace api.Features.Users.Domain.Query;

using System.Threading.Tasks;
using System.Threading;
using System;

public interface IGetUserByIdQuery
{
    public Task<User> ExecuteAsync(Guid id, CancellationToken cancel);
}
