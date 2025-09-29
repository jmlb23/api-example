namespace api.Features.Users.Handlers;

using api.Features.Users.Domain.Query;

public static class ListUsers
{

    public record Request(Guid Id);

    public static async Task<object> Handle(Request req, IGetUserByIdQuery query)
    {
        var result = await query.ExecuteAsync(req.Id, default);
        return result;
    }
}
