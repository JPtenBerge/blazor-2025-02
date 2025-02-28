using BlazorApp1.Client.Entities;
using DemoProject.Auth;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DemoProject.DataAccess;

public class DemoContext : IdentityDbContext<DemoUser>
{
    public DbSet<Course> Courses { get; set; }
    // public DbSet<Park> Parks { get; set; }

    public DemoContext(DbContextOptions<DemoContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Course>().Property(x => x.Title).IsRequired();
        modelBuilder.Entity<Course>().Property(x => x.Title).HasMaxLength(200);

        modelBuilder.Entity<Course>().Property(x => x.FunPhoto).IsRequired();
        modelBuilder.Entity<Course>().Property(x => x.FunPhoto).HasMaxLength(1000);
        
        // modelBuilder.Entity<Course>().HasKey(x => new { x.Id, x.Title });
    }
}