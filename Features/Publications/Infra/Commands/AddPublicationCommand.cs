using System;
using System.Threading;
using System.Threading.Tasks;
using api.Features.Publications.Domain;
using api.Features.Publications.Domain.Commands;

namespace api.Features.Publications.Infra.Commands;

public class AddPublicationCommand : IAddPublicationCommand
{
    public Task<Guid> ExecuteAsync(Publication publication, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}