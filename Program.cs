using api.Features.Users.Domain.Query;
using api.Features.Users.Infra.Queries;
using api.Features.Users.Handlers;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IGetUserByIdQuery, GetUserByIdQuery>();
builder.Services.AddScoped<IGetUsersQuery, GetUsersQuery>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument();
    
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi(); 
}

app.MapPost("/login", (Login login) => {
    app.Logger.LogInformation($"form username={login.username} password={login.password}");
    return Results.Accepted();
});

app.MapPost("/logout", ([FromHeader(Name = "Authorization")] String jwt) =>
{
    app.Logger.LogInformation($"form jwt={jwt}");
    return Results.Accepted();
});


var usersApi = app.MapGroup("/users");

usersApi.MapGet("/{id}", async (Guid id, IGetUserByIdQuery query) => {
    var result = await ListUsers.Handle(new ListUsers.Request(id), query);
    return Results.Ok(result);
});

usersApi.MapGet("/", async (IGetUsersQuery query) => {
    var results = await ListAllUsers.Handle(query, default);
    return Results.Ok(results);
});

app.Run("http://localhost:5288");

public record Login(String username, String password);
