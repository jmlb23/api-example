
using System.Threading.Tasks;
using System;
using System.Threading;
using api.Features.Publications.Domain;
using api.Features;

namespace api.Features.Publications.UseCases;

public static class AddPublicationUseCase
{

    public record AddPublicationCommand(string Title, string Content) : Command;

    public record Response(Guid Id);

    public static async Task<Response> Handle(AddPublicationCommand req, IPublicationRepository repository)
    {
        var result = await repository.Add(new Publication(Guid.CreateVersion7(), req.Title, req.Content, DateTime.Today));
        return new Response(Id: result);
    }
}


public static class RemovePublicationUseCase
{

    public record RemovePublicationCommand(Guid Id) : Command;

    public static async Task Handle(RemovePublicationCommand req, IPublicationRepository repository)
    {

        await repository.Remove(req.Id);
    }
}
