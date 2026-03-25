using Microsoft.Extensions.DependencyInjection;
using api.Features.Auth.Applicaiton.Command;

namespace api.Features.Auth;

public static class AuthModule
{
    public static IServiceCollection AddAuthModule(this IServiceCollection services)
    {

        services.AddScoped<IHandler<AuthUserHandler.Request, AuthUserHandler.Response>, AuthUserHandler>();

        return services;
    }
}
