using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

using api.Features.Publications.Domain;
using api.Features;

namespace api.Features.Publications.Application.Queries;

public class GetAllPublicationsHandler(IPublicationRepository repository) : 
    IHandler<GetAllPublicationsHandler.None, GetAllPublicationsHandler.Response>
{
    public record None: Query<IEnumerable<Publication>>;
    public record Response(IEnumerable<Publication> result);

    public async Task<Response> Handle(None none)
    {
        var result = await repository.GetAll(); 
        return new Response(result ?? Enumerable.Empty<Publication>());
    }
}
