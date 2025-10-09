using System.Collections.Generic;
using api.Features.Users.Domain;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace api.Features.Users.UseCases;



public static class ListAllUsersHandler
{
    public record GetAllUsersQuery() : Query<IEnumerable<User>>;
    public static async Task<IEnumerable<User>> Handle(IOperation operation, IUserRepository repository, CancellationToken token)
    {
        var result = await repository.GetUsersAsync();
        return result;
    }
}


public static class GetUserByIdHandler
{

    public record GetUsersByIdQuery(Guid Id) : Query<User>;

    public static async Task<User?> Handle(GetUsersByIdQuery req, IUserRepository repository)
    {
        var result = await repository.GetUserByIdAsync(req.Id);
        return result;
    }
}
