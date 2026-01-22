using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Features.Comments.Domain;

namespace api.Features.Comments.Infra.Domain;

class CommentRepository() : ICommentRepository
{
    private IList<Comment> _comments = new List<Comment>(); 

    public async Task<IEnumerable<Comment>> GetAll()
    {
        throw new NotImplementedException(( ">>>>>>>>" + _comments.Count));
    }

    public Task<Comment> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<Guid> Add(Comment comment)
    {
        _comments.Add(comment);
        return comment.Id;
    }

    public Task<Guid> Remove(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Guid> Update(Comment comment)
    {
        throw new NotImplementedException();
    }

}
