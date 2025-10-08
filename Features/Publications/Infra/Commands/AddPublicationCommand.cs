using System;
using System.Threading;
using System.Threading.Tasks;
using api.Features.Publications.Domain.Commands;
using api.Features.Publications.Infra.Data;
using PublicationDomain = api.Features.Publications.Domain.Publication;

namespace api.Features.Publications.Infra.Commands;

public class AddPublicationCommand(PublicationsContext context) : IAddPublicationCommand
{
    
    public async Task<Guid> ExecuteAsync(PublicationDomain publication, CancellationToken cancellationToken)
    {
        var result  = await context.Publications.AddAsync(new Publication(publication.Id, publication.Title, publication.Content,
            publication.PublishDate), cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return result.Entity.Id;
    }
}
