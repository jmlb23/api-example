using api.Features.Users.Domain.Query;
using api.Features.Users.Infra.Queries;
using api.Features.Users.Handlers;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IGetUserByIdQuery, GetUserByIdQuery>();
builder.Services.AddScoped<IGetUsersQuery, GetUsersQuery>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument();
    
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseReDoc(); 
}

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
