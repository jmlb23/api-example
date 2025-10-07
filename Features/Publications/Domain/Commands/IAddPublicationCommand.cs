using System;
using System.Threading;
using System.Threading.Tasks;

namespace api.Features.Publications.Domain.Commands;

public interface IAddPublicationCommand
{
    Task<Guid> ExecuteAsync(Publication publication, CancellationToken cancellationToken);

}