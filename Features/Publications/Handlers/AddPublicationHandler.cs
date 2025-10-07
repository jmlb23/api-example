namespace api.Features.Publications.Handlers;

using System.Threading.Tasks;
using System;
using api.Features.Publications.Domain.Commands;
using System.Threading;

public static class AddPublicationHandler
{

    public record Request(string Title, string Content);

    public static async Task<Guid> Handle(Request req, IAddPublicationCommand command, CancellationToken token)
    {
        var result = await command.ExecuteAsync(new Domain.Publication(Guid.CreateVersion7(), req.Title, req.Content, DateTime.Today), token);
        return result;
    }
}
