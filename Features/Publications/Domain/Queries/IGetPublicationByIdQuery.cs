using System;
using System.Threading;
using System.Threading.Tasks;

namespace api.Features.Publications.Domain.Queries;

public interface IGetPublicationByIdQuery
{
    Task<Publication?> ExecuteAsync(Guid id, CancellationToken cancellationToken);

}