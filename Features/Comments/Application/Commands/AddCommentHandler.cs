using System;
using System.Threading.Tasks;
using api.Features.Comments.Domain;
using api.Features;

namespace api.Features.Comments.Commands;

public class AddCommentHandler(ICommentRepository repository) : IHandler<AddCommentHandler.AddCommentCommand, AddCommentHandler.Response>
{
    public record AddCommentRequest(String Content) : Command;
    public record AddCommentCommand(Guid PostId, string Content) : Command;

    public record Response(Guid Id);

    public async Task<Response> Handle(AddCommentCommand req)
    {
        var result = await repository.Add(new Comment(Guid.CreateVersion7(), req.PostId, req.Content, DateTime.Today));
        return new Response(Id: result);
    }
}
