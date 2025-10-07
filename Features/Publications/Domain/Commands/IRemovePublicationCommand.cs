using System;
using System.Threading;
using System.Threading.Tasks;

namespace api.Features.Publications.Domain.Commands;

public interface IRemovePublicationCommand
{
    Task ExecuteAsync(Guid id, CancellationToken cancellationToken);

}