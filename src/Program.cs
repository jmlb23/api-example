using api.Features.Users.Domain.Query;
using api.Features.Users.Infra;
using api.Features.Users.Handlers;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore;
using Microsoft.Extensions.DependencyInjection; 
using Microsoft.AspNetCore.Http;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IGetUserByIdQuery, GetUserByIdQuery>();
var app = builder.Build();


var todosApi = app.MapGroup("/users");
todosApi.MapGet("/{id}", async (Guid id, IGetUserByIdQuery query) => {
    var result = await ListUsers.Handle(new ListUsers.Request(id), query);
    return Results.Ok(result);
});

app.Run();
