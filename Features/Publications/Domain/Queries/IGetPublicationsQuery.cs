using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace api.Features.Publications.Domain.Queries;

public interface IGetPublicationQuery
{
    Task<IEnumerable<Publication>> ExecuteAsync(CancellationToken cancellationToken);

}