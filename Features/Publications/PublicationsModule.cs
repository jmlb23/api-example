using System;
using api.Features.Publications.Commands;
using api.Features.Publications.Domain;
using api.Features.Publications.Infra.Data;
using api.Features.Publications.Infra.Domain;
using api.Features.Publications.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace api.Features.Publications.Module;

public static class PublicationsModule
{
    public static IServiceCollection AddPublicationsModule(this IServiceCollection services)
    {
        services.AddDbContext<PublicationsContext>();

        // Register your services, repositories, DbContext, etc.
        services.AddScoped<IPublicationRepository, PublicationRepository>();
        services.AddScoped<IHandler<AddPublicationHandler.AddPublicationCommand, AddPublicationHandler.Response>, AddPublicationHandler>();
        services.AddScoped<IHandler<RemovePublicationHandler.RemovePublicationCommand, Guid>, RemovePublicationHandler>();

        return services;
    }
}
