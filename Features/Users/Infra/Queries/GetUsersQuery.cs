using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using api.Features.Users.Domain;
using api.Features.Users.Domain.Query;
using System.Linq;
using System;

namespace api.Features.Users.Infra.Queries;

public class GetUsersQuery : IGetUsersQuery
{
    public Task<IEnumerable<User>> ExecuteAsync(CancellationToken cancel)
    {
        return Task.FromResult(
                new List<User>() {
                    new User(Guid.Parse("5dcc325c-f37e-4955-b37c-25434f037af0"),"pepe","pepe23","pepe@gmail.com"),
                    new User(Guid.Parse("4dcc325c-f37e-4955-b37c-25434f037af0"),"juan","juan23","juan@gmail.com"),
                }.AsEnumerable()
        );
    }
}
