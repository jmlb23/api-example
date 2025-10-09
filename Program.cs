using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using api.Features.Auth.Domain.Command;
using api.Features.Auth.Infra;
using api.Features.Users.UseCases;
using api.Features.Auth.UseCases;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using api.Features.Publications.Domain.Commands;
using api.Features.Publications.Infra.Commands;
using api.Features.Publications.UseCases;
using api.Features.Publications.Infra.Data;
using api.Features.Users.Domain;
using api.Features.Users.Infra.Domain;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PublicationsContext>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthUserCommand, AuthUserCommand>();
builder.Services.AddScoped<IAddPublicationCommand, AddPublicationCommand>();
builder.Services.AddScoped<IRemovePublicationCommand, RemovePublicationCommand>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddAuthentication().AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, jwtOptions =>
{
    jwtOptions.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["jwt:issuer"] ?? throw new ArgumentException("Not found jwt:issuer"),
        ValidAudience = builder.Configuration["jwt:audience"] ?? throw new ArgumentException("Not found jwt:audience"),
        IssuerSigningKey =  new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["jwt:key"] ?? throw new ArgumentException("Not found jwt:key")))
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi("/swagger/v1/swagger.json");
    app.UseSwaggerUI(c =>
    {
        c.DocumentTitle = "OpenAPI Documentation";
        c.SwaggerDocumentUrlsPath = "/swagger/v1/swagger.json";
    });
}

app.MapPost("/login", async (IAuthUserCommand command, AuthUseCase.Request login, CancellationToken token) =>
{
    var auth = await AuthUseCase.HandleAsync(
        command,
        login,
        token
    );
    return TypedResults.Ok(auth);
});

app.MapPost("/logout", ([FromHeader(Name = "Authorization")] string jwt) =>
{
    app.Logger.LogInformation($"form jwt={jwt}");
    return TypedResults.Ok();
});

var usersApi = app.MapGroup("/users");

usersApi.MapGet("/{id}", async (Guid id, IUserRepository repo) =>
{
    var result = await GetUserByIdHandler.Handle(new GetUserByIdHandler.GetUsersByIdQuery(id), repo);
    return TypedResults.Ok(result);
}).RequireAuthorization();

usersApi.MapGet("/", async (IUserRepository repository, CancellationToken token) =>
{
    var results = await ListAllUsersHandler.Handle(new ListAllUsersHandler.GetAllUsersQuery(),repository, token);
    return TypedResults.Ok(results);
}).RequireAuthorization();


var publicationsApi = app.MapGroup("/publications");

publicationsApi.MapGet("/", () =>
{

}).RequireAuthorization();

publicationsApi.MapGet("/{id}", (Guid id) =>
{

}).RequireAuthorization();

publicationsApi.MapPost("/", async (AddPublicationUseCase.Request payload, IAddPublicationCommand command, CancellationToken token) =>
{
    var result = await AddPublicationUseCase.Handle(payload, command, token);
    return TypedResults.Created(new Uri($"/publications/{result.Id}"),result);
}).RequireAuthorization();

publicationsApi.MapDelete("/{id}", async (Guid id, IRemovePublicationCommand command, CancellationToken token) =>
{
    await RemovePublicationUseCase.Handle(id, command, token);
    return TypedResults.NoContent();

}).RequireAuthorization();

publicationsApi.MapPatch("/{id}", (Guid id, object payload) =>
{ 
    
}).RequireAuthorization();

app.Run("https://localhost:5288");
