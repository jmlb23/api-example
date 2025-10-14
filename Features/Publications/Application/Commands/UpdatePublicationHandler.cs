using System;
using System.Threading.Tasks;
using api.Features;
using api.Features.Publications.Domain;


namespace api.Features.Publications.Commands;

public class UpdatePublicationHandler(IPublicationRepository repository) : IHandler<UpdatePublicationHandler.UpdatePublicationCommand, UpdatePublicationHandler.Response>
{
    public record UpdatePublicationCommand(Guid Id, string Title, string Content, DateTime PublishDate) : Command;
    public record UpdatePublicationDTO(string Title, string Content, DateTime PublishDate) : Command;

    public record Response(Guid Id);

    public async Task<Response> Handle(UpdatePublicationCommand req)
    {
        var result = await repository.Update(new Publication(req.Id, req.Title, req.Content, req.PublishDate));
        return new Response(Id: result);
    }
}
