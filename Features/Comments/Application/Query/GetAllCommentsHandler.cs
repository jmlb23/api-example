using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using api.Features.Comments.Domain;
using api.Features;

namespace api.Features.Comments.Application.Query;

public class GetAllCommentsHandler(ICommentRepository repository) : 
    IHandler<GetAllCommentsHandler.None, IEnumerable<Comment>>
{
    public record None: Query<IEnumerable<Comment>>;

    public async Task<IEnumerable<Comment>> Handle(None none)
    {
        var result = await repository.GetAll();
        return result;
    }
}
