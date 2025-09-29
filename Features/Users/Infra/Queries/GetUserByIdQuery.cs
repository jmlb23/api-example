namespace api.Features.Users.Infra;

using api.Features.Users.Domain.Query;
using api.Features.Users.Domain;

internal class GetUserByIdQuery : IGetUserByIdQuery 
{
    public Task<IEnumerable<User>> ExecuteAsync(Guid id, CancellationToken cancel)
    {
       return Task.FromResult(new List<User>(){ new User(Guid.NewGuid(),"","","") }.AsEnumerable()); 
    }

}
