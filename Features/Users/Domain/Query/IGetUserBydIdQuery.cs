namespace api.Features.Users.Domain.Query;

public interface IGetUserByIdQuery
{
    public Task<User> ExecuteAsync(Guid id, CancellationToken cancel);
}
