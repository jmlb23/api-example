using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace api.Features.Users.Domain;

public interface IUserRepository
{
    Task<User?> GetUserByIdAsync(Guid id);
    Task<IEnumerable<User>> GetUsersAsync();
    
}