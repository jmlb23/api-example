using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using api.Features.Auth.Domain;
using api.Features.Auth.Domain.Command;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace api.Features.Auth.Infra;

public class AuthUserCommand(IConfiguration configuration) : IAuthUserCommand
{
    public async Task<AuthResult?> ExecuteAsync(Domain.Auth credentials, CancellationToken ct = default)
    {
        var secHandler = new JwtSecurityTokenHandler();
        // simulate database Access
        await Task.Delay(5000, ct);
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
        return serialisedToken switch
        {
            null => null,
            _ => new AuthResult(serialisedToken, "")
        };
    }
}