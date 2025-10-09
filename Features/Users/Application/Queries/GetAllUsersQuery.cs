using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using api.Features.Users.Domain;

namespace api.Features.Users.Application.Queries;

public class GetAllUsersHandler(IUserRepository repository) : IHandler<GetAllUsersHandler.None, IEnumerable<User>>
{
    public record None : Query<IEnumerable<User>>;
    public async Task<IEnumerable<User>> Handle(None command)
    {
        var result = await repository.GetUsersAsync();
        return result;
    }
}