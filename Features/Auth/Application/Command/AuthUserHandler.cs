namespace api.Features.Auth.Applicaiton.Command;

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using api.Features;

public class AuthUserHandler(IConfiguration configuration) : IHandler<AuthUserHandler.Request, AuthUserHandler.Response>
{
    public record Request(string Username, string Password);
    public record Response(String Token, String RefreshToken);

    public async Task<Response> Handle(Request credentials)
    {
        var secHandler = new JwtSecurityTokenHandler();
        // simulate database Access
        await Task.Delay(5000, new CancellationToken());
        var token = secHandler.CreateToken(new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity([new Claim(ClaimTypes.Name, credentials.Username)]),
            Expires = DateTime.UtcNow.AddHours(2),
            Issuer = configuration["jwt:issuer"],
            Audience = configuration["jwt:audience"],
            SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwt:key"] ?? string.Empty)),
                    SecurityAlgorithms.HmacSha256)
        });
        var serialisedToken = secHandler.WriteToken(token);
        return new Response(
            serialisedToken ?? throw new SecurityTokenException("Could not be generated"),
            ""
        );

    }
}
