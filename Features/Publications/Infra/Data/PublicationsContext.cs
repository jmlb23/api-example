using System;
using Microsoft.EntityFrameworkCore;

namespace api.Features.Publications.Infra.Data;

public class PublicationsContext : DbContext
{
    public DbSet<Publication>  Publications { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("MyDatabase");
    }
}

public record Publication(Guid Id, string Title, string Slug, DateTime PublishDate);