using api.Features.Users.Domain.Query;

using System.Threading.Tasks;
using System;


namespace api.Features.Users.UseCases;

public static class ListUsersUseCase
{

    public record Request(Guid Id);

    public static async Task<object> Handle(Request req, IGetUserByIdQuery query)
    {
        var result = await query.ExecuteAsync(req.Id, default);
        return result;
    }
}
