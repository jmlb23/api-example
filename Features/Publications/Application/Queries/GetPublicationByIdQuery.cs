using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;

using api.Features.Publications.Domain;
using api.Features;

namespace api.Features.Publications.Application.Queries;

public class GetPublicationByIdQuery(IPublicationRepository repository) :
    IHandler<GetPublicationByIdQuery.Request, GetPublicationByIdQuery.Response>
{
    public record Request(Guid id) : Query<IEnumerable<Publication>>;
    public record Response(Publication? result);

    public async Task<Response> Handle(Request param)
    {
        var result = await repository.GetById(param.id);
        return new Response(result);
    }
}
