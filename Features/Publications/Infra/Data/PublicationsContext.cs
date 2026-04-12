using System;
using Microsoft.EntityFrameworkCore;

namespace api.Features.Publications.Infra.Data;

public class PublicationsContext(DbContextOptions<PublicationsContext> options): DbContext(options)
{
    public DbSet<Publication> Publications { get; set; }

}

public record Publication(Guid Id, string Title, string Content, DateTime PublishDate);
