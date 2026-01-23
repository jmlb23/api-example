using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Features.Comments.Domain;

namespace api.Features.Comments.Infra.Domain;

class CommentRepository() : ICommentRepository
{
    private static IList<Comment> _comments = new List<Comment>(); 

    public async Task<IEnumerable<Comment>> GetAll()
    {
        return _comments;
    }

    public async Task<Comment> GetById(Guid id)
    {
        return _comments.First(x => x.Id == id);
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

    public async Task<IEnumerable<Comment>> Filter(Criteria criteria)
    {
        return criteria switch
        {
            PostIdCriteria p => _comments.Where(c => c.PostId == p.PostId),
            _ => _comments,
        };
    }
}
