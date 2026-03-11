using ClaudeTestRepo.Domain.Models;
using Microsoft.EntityFrameworkCore;
using List = ClaudeTestRepo.Domain.Models.List;

namespace ClaudeTestRepo.Infrastructure.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<List> Lists { get; set; }
    public DbSet<ListItem> ListItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<List>(entity =>
        {
            entity.HasKey(l => l.Id);
            entity.HasMany(l => l.Items)
                  .WithOne(li => li.List)
                  .HasForeignKey(li => li.ListId);
        });

        modelBuilder.Entity<ListItem>(entity =>
        {
            entity.HasKey(li => li.Id);
        });
    }
}
