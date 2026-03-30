using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Features.Publications.Domain;
using api.Features.Publications.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Publication = api.Features.Publications.Infra.Data.Publication;
using PublicationDomain = api.Features.Publications.Domain.Publication;


namespace api.Features.Publications.Infra.Domain;

public class PublicationRepository(PublicationsContext context) : IPublicationRepository
{
    public async Task<Guid> Add(PublicationDomain publication)
    {
        var result = await context.Publications.AddAsync(new Publication(publication.Id, publication.Title, publication.Content,
            publication.PublishDate));
        await context.SaveChangesAsync();
        return result.Entity.Id;
    }

    public async Task<IEnumerable<PublicationDomain>> GetAll()
    {
        var result = context.Publications.AsEnumerable();
        var mapped = result.Select(pub =>
                new PublicationDomain(
                    pub.Id,
                    pub.Title,
                    pub.Content,
                    pub.PublishDate
                )
        );
        return mapped;
    }

    public async Task<PublicationDomain?> GetById(Guid id)
    {
        var result = await context
            .Publications
            .Select(pub =>
                new PublicationDomain(
                    pub.Id,
                    pub.Title,
                    pub.Content,
                    pub.PublishDate
                )
            )
            .FirstOrDefaultAsync(x => x.Id == id);

        return result;
    }

    public async Task<Guid> Remove(Guid id)
    {
        await context.Publications.Where(x => x.Id == id).ExecuteDeleteAsync();
        return id;
    }

    public async Task<Guid> Update(PublicationDomain publication)
    {
        var result = await context
        .Publications
        .Where(p => p.Id == publication.Id)
        .ExecuteUpdateAsync(update =>
            update
            .SetProperty(p => p.Content, publication.Content)
            .SetProperty(p => p.Title, publication.Title)
            .SetProperty(p => p.PublishDate, publication.PublishDate)
        );

        return publication.Id;
    }
}
