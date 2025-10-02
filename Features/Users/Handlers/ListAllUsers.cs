namespace api.Features.Users.Handlers;

using api.Features.Users.Domain.Query;
using System.Collections.Generic;
using api.Features.Users.Domain;
using System.Threading.Tasks;
using System.Threading;

public static class ListAllUsers
{

    public static async Task<IEnumerable<User>> Handle(IGetUsersQuery query, CancellationToken token)
    {
        var result = await query.ExecuteAsync(token);
        return result;
    }
}
