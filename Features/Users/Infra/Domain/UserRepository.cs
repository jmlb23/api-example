using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Features.Users.Domain;

namespace api.Features.Users.Infra.Domain;

public class UserRepository : IUserRepository
{
    public async Task<User?> GetUserByIdAsync(Guid id)
    {
        return await Task.FromResult(
               new List<User>(){ new User(Guid.Parse("5dcc325c-f37e-4955-b37c-25434f037af0"),"pepe","pepe23","pepe@gmail.com") }
               .AsEnumerable()
               .First(user => user.Id == id)
               ); 
    }
        

    public Task<IEnumerable<User>> GetUsersAsync()
    {
        return Task.FromResult(
                new List<User>() {
                    new User(Guid.Parse("5dcc325c-f37e-4955-b37c-25434f037af0"),"pepe","pepe23","pepe@gmail.com"),
                    new User(Guid.Parse("4dcc325c-f37e-4955-b37c-25434f037af0"),"juan","juan23","juan@gmail.com"),
                }.AsEnumerable()
        );
    }
    
}