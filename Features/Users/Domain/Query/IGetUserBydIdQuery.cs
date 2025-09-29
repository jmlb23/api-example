namespace api.Features.Users.Domain.Query;

public interface IGetUserByIdQuery
{
    public Task<IEnumerable<User>> ExecuteAsync(Guid id, CancellationToken cancel);
}
