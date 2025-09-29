using api.Features.Users.Domain.Query;
using api.Features.Users.Infra;
using api.Features.Users.Handlers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IGetUserByIdQuery, GetUserByIdQuery>();
var app = builder.Build();


var todosApi = app.MapGroup("/users");
todosApi.MapGet("/{id}", async (int id, IGetUserByIdQuery query) => {
    var result = await ListUsers.Handle(new ListUsers.Request(new Guid()), query);
    return Results.Ok(result);
});

app.Run();
