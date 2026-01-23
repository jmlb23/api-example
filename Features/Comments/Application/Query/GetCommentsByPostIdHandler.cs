using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Features.Comments.Domain;

namespace api.Features.Comments.Application.Query;

public class GetCommentsByPostIdHandler(ICommentRepository repository) :
    IHandler<GetCommentsByPostIdHandler.GetCommentsByPostIdQuery, IEnumerable<Comment>>
{
    public record GetCommentsByPostIdQuery(Guid PostId) : Query<string>;


    public async Task<IEnumerable<Comment>> Handle(GetCommentsByPostIdQuery command)
    {
        return await repository.Filter(new PostIdCriteria(command.PostId));
    }
    
}
