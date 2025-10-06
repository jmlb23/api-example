using System;
using System.Threading;
using System.Threading.Tasks;
using api.Features.Auth.Domain.Command;

namespace api.Features.Auth.Handlers;

public static class AuthHandler
{
    public record Request(string Username, string Password);
    public record Response(String Token, String RefreshToken);
    
    public static async Task<Response?> HandleAsync(IAuthUserCommand authUserCommand, Request request, CancellationToken token)
    {
        var resultAuth = await authUserCommand.ExecuteAsync(new Domain.Auth(request.Username, request.Password), token);

        return new Response(
            resultAuth.Token,
            resultAuth.RefreshToken
        );
    }
}