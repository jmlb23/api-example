using api.Features.Users.Domain.Query;
using api.Features.Users.Infra;
using api.Features.Users.Handlers;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IGetUserByIdQuery, GetUserByIdQuery>();

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });

}


var todosApi = app.MapGroup("/users");
todosApi.MapGet("/{id}", async (Guid id, IGetUserByIdQuery query) => {
    var result = await ListUsers.Handle(new ListUsers.Request(id), query);
    return Results.Ok(result);
});

app.Run();
