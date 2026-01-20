using api.Features.Comments.Commands;
using api.Features.Comments.Domain;
using api.Features.Comments.Infra.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace api.Features.Comments;

public static class CommentsModule
{
    public static IServiceCollection AddCommentsModule(this IServiceCollection services)
    {

        // Register your services, repositories, DbContext, etc.
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<IHandler<AddCommentHandler.AddCommentCommand, AddCommentHandler.Response>, AddCommentHandler>();

        return services;
    }
}
