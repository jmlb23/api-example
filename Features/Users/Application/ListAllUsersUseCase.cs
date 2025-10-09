
using api.Features.Users.Domain.Query;
using System.Collections.Generic;
using api.Features.Users.Domain;
using System.Threading.Tasks;
using System.Threading;

namespace api.Features.Users.UseCases;


public static class ListAllUsersUseCase
{

    public static async Task<IEnumerable<User>> Handle(IGetUsersQuery query, CancellationToken token)
    {
        var result = await query.ExecuteAsync(token);
        return result;
    }
}
