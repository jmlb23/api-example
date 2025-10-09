using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace api.Features.Publications.Domain;

public interface IPublicationRepository
{
    Task<IEnumerable<Publication>> GetAll();
    Task<Publication> GetById(Guid id);

    Task<Guid> Add(Publication publication);

    Task<Guid> Remove(Guid id);
}