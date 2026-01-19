using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace api.Features.Comments.Domain;

public interface ICommentRepository
{
    Task<IEnumerable<Comment>> GetAll();
    Task<Comment> GetById(Guid id);
    Task<Guid> Add(Comment comment);
    Task<Guid> Remove(Guid id);
    Task<Guid> Update(Comment comment);
}
