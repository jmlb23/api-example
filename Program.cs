using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using api.Features.Auth.Domain.Command;
using api.Features.Auth.Infra;
using api.Features.Users.Domain.Query;
using api.Features.Users.Handlers;
using api.Features.Users.Infra.Queries;
using api.Features.Auth.Handlers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IGetUserByIdQuery, GetUserByIdQuery>();
builder.Services.AddScoped<IGetUsersQuery, GetUsersQuery>();
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

app.MapPost("/login", async (IAuthUserCommand command, AuthHandler.Request login) =>
{
    var auth = await AuthHandler.HandleAsync(
        command,
        login,
        CancellationToken.None
    );
    return TypedResults.Ok(auth);
});

app.MapPost("/logout", ([FromHeader(Name = "Authorization")] String jwt) =>
{
    app.Logger.LogInformation($"form jwt={jwt}");
    return TypedResults.Ok();
});

var usersApi = app.MapGroup("/users");

usersApi.MapGet("/{id}", async (Guid id, IGetUserByIdQuery query) =>
{
    var result = await ListUsers.Handle(new ListUsers.Request(id), query);
    return TypedResults.Ok(result);
}).RequireAuthorization();

usersApi.MapGet("/", async (IGetUsersQuery query) =>
{
    var results = await ListAllUsers.Handle(query, default);
    return TypedResults.Ok(results);
}).RequireAuthorization();

app.Run("https://localhost:5288");