
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using api.Features.Publications.Domain.Commands;
using api.Features.Publications.Infra.Data;

namespace api.Features.Publications.Infra.Commands;


public class RemovePublicationCommand(PublicationsContext context) : IRemovePublicationCommand
{
    public async Task ExecuteAsync(Guid id, CancellationToken cancellationToken)
    {
        await context.Publications.Where(x => x.Id == id).ExecuteDeleteAsync();
    }
}