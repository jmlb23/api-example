
using System.Threading.Tasks;
using System;
using System.Threading;
using api.Features.Publications.Domain;
using api.Features;

namespace api.Features.Publications.UseCases;


public class RemovePublicationHandler(IPublicationRepository repository) : IHandler<RemovePublicationHandler.RemovePublicationCommand, Guid>
{

    public record RemovePublicationCommand(Guid Id) : Command;

    public async Task<Guid> Handle(RemovePublicationCommand req)
    {

        await repository.Remove(req.Id);
        return req.Id;
    }

}
