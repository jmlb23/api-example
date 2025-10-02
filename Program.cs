using api.Features.Users.Domain.Query;
using api.Features.Users.Infra;
using api.Features.Users.Handlers;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IGetUserByIdQuery, GetUserByIdQuery>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument();
    
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi(); 
}

var usersApi = app.MapGroup("/users");
usersApi.MapGet("/{id}", async (Guid id, IGetUserByIdQuery query) => {
    var result = await ListUsers.Handle(new ListUsers.Request(id), query);
    return Results.Ok(result);
});

app.Run("http://localhost:5288");
