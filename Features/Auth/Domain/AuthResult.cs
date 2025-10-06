namespace api.Features.Auth.Domain;

public record AuthResult(string Token, string RefreshToken);