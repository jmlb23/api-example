using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using api.Features.Auth.Domain.Command;
using api.Features.Auth.Infra;
using api.Features.Auth.UseCases;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using api.Features.Publications.UseCases;
using api.Features.Users.Domain;
using api.Features.Publications;
using api.Features;
using api.Features.Publications.Commands;
using api.Features.Users;
using api.Features.Users.Application.Queries;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPublicationsModule();
builder.Services.AddUsersModule();
builder.Services.AddScoped<IAuthUserCommand, AuthUserCommand>();


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

usersApi.MapGet("/{id}", async (Guid id, IHandler<GetUserByIdHandler.GetUsersByIdQuery, User?> handler) =>
{
    var result = await handler.Handle(new GetUserByIdHandler.GetUsersByIdQuery(id));
    return TypedResults.Ok(result);
}).RequireAuthorization();

usersApi.MapGet("/", async (IHandler<GetAllUsersHandler.None, IEnumerable<User>> handler) =>
{
    var results = await handler.Handle(new GetAllUsersHandler.None());
    return TypedResults.Ok(results);
}).RequireAuthorization();


var publicationsApi = app.MapGroup("/publications");

publicationsApi.MapGet("/", () =>
{

}).RequireAuthorization();

publicationsApi.MapGet("/{id}", (Guid id) =>
{

}).RequireAuthorization();

publicationsApi.MapPost("/", async (AddPublicationHandler.AddPublicationCommand payload, IHandler<AddPublicationHandler.AddPublicationCommand, AddPublicationHandler.Response> handler, CancellationToken token) =>
{
    var result = await handler.Handle(payload);
    return TypedResults.Created(new Uri($"/publications/{result.Id}"),result);
}).RequireAuthorization();

publicationsApi.MapDelete("/{id}", async (Guid id, IHandler<RemovePublicationHandler.RemovePublicationCommand, Guid> handler) =>
{
    await handler.Handle(new RemovePublicationHandler.RemovePublicationCommand(id));
    return TypedResults.NoContent();

}).RequireAuthorization();

publicationsApi.MapPatch("/{id}", (Guid id, object payload) =>
{ 
    
}).RequireAuthorization();

app.Run("https://localhost:5288");
