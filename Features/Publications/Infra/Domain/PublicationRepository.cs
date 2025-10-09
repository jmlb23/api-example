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
        var result  = await context.Publications.AddAsync(new Publication(publication.Id, publication.Title, publication.Content,
            publication.PublishDate));
        await context.SaveChangesAsync();
        return result.Entity.Id;
    }

    public Task<IEnumerable<PublicationDomain>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<PublicationDomain> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<Guid> Remove(Guid id)
    {
        await context.Publications.Where(x => x.Id == id).ExecuteDeleteAsync();
        return id;
    }
}