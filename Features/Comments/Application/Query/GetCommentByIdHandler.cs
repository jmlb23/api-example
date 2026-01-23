using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Features.Comments.Domain;

namespace api.Features.Comments.Application.Query;

public class GetCommentByIdHandler(ICommentRepository repository) :
    IHandler<GetCommentByIdHandler.GetCommentByCommentIdQuery, Comment>
{
    public record GetCommentByCommentIdQuery(Guid CommentId) : Query<string>;


    public async Task<Comment> Handle(GetCommentByCommentIdQuery command)
    {
        return await repository.GetById(command.CommentId);
    }
    
}