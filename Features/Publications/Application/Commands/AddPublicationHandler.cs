using System;
using System.Threading.Tasks;
using api.Features;
using api.Features.Publications.Domain;


namespace api.Features.Publications.Commands;

public class AddPublicationHandler(IPublicationRepository repository) : IHandler<AddPublicationHandler.AddPublicationCommand, AddPublicationHandler.Response>
{
    public record AddPublicationCommand(string Title, string Content) : Command;

    public record Response(Guid Id);

    public async Task<Response> Handle(AddPublicationCommand req)
    {
        var result = await repository.Add(new Publication(Guid.CreateVersion7(), req.Title, req.Content, DateTime.Today));
        return new Response(Id: result);
    }
}
