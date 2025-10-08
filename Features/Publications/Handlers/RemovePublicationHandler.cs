namespace api.Features.Publications.Handlers;

using System.Threading.Tasks;
using System;
using api.Features.Publications.Domain.Commands;
using System.Threading;

public static class RemovePublicationHandler
{


    public static async Task Handle(Guid id, IRemovePublicationCommand command, CancellationToken token)
    {
        await command.ExecuteAsync(id, token);
    }
}
