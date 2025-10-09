
using System.Threading.Tasks;
using System;
using api.Features.Publications.Domain.Commands;
using System.Threading;

namespace api.Features.Publications.UseCases;


public static class RemovePublicationUseCase
{


    public static async Task Handle(Guid id, IRemovePublicationCommand command, CancellationToken token)
    {
        await command.ExecuteAsync(id, token);
    }
}
