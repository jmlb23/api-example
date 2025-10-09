using System;
using System.Threading.Tasks;
using api.Features.Users.Domain;

namespace api.Features.Users.Application.Queries;

public class GetUserByIdHandler(IUserRepository repository) : IHandler<GetUserByIdHandler.GetUsersByIdQuery, User?>
{
    public record GetUsersByIdQuery(Guid Id) : Query<User>;

    public async Task<User?> Handle(GetUserByIdHandler.GetUsersByIdQuery command)
    {
        var result = await repository.GetUserByIdAsync(command.Id);
        return result;
    }
}