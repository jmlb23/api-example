using System.Collections.Generic;
using api.Features.Users.Application.Queries;
using api.Features.Users.Domain;
using api.Features.Users.Infra.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace api.Features.Users;

public static class UsersModule
{
    public static IServiceCollection AddUsersModule(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IHandler<GetAllUsersHandler.None, IEnumerable<User>>, GetAllUsersHandler>();
        services.AddScoped<IHandler<GetUserByIdHandler.GetUsersByIdQuery, User?>, GetUserByIdHandler>();
        return services;
    }
}