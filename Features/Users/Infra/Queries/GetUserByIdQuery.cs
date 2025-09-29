namespace api.Features.Users.Infra;

using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System;

using api.Features.Users.Domain.Query;
using api.Features.Users.Domain;

public class GetUserByIdQuery : IGetUserByIdQuery 
{
    public Task<User> ExecuteAsync(Guid id, CancellationToken cancel)
    {
       return Task.FromResult(
               new List<User>(){ new User(Guid.Parse("5dcc325c-f37e-4955-b37c-25434f037af0"),"pepe","pepe23","pepe@gmail.com") }
               .AsEnumerable()
               .First(user => user.Id == id)
               ); 
    }

}
