using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ForumsWeb.Models;

namespace ForumsWeb.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Forum> Forums { get; set; } = null!;
    public DbSet<Post> Posts { get; set; } = null!;
    public DbSet<Reply> Replies { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Relationship between Post and User
        modelBuilder.Entity<Post>()
            .HasOne(p => p.User)
            .WithMany()
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.SetNull);

        // Relationship between Reply and User
        modelBuilder.Entity<Reply>()
            .HasOne(r => r.User)
            .WithMany()
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.SetNull);

        // Seed initial forums
        modelBuilder.Entity<Forum>().HasData(
            new Forum { Id = 1, Title = "General Discussion", Description = "A place to talk about anything and everything." },
            new Forum { Id = 2, Title = "Tech Support", Description = "Get help with your hardware and software issues." },
            new Forum { Id = 3, Title = "Development", Description = "Programming, design, and architecture." }
        );
    }
}
