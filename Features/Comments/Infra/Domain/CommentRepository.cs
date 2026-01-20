using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Features.Comments.Domain;

namespace api.Features.Comments.Infra.Domain;

class CommentRepository() : ICommentRepository
{
    public Task<IEnumerable<Comment>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<Comment> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Guid> Add(Comment comment)
    {
        throw new NotImplementedException();
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
