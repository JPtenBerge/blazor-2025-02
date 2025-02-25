using DemoProject.Entities;
using Microsoft.EntityFrameworkCore;

namespace DemoProject.DataAccess;

public class DemoContext : DbContext
{
    public DbSet<Course> Courses { get; set; }
    // public DbSet<Park> Parks { get; set; }

    public DemoContext(DbContextOptions<DemoContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>().Property(x => x.Title).IsRequired();
        modelBuilder.Entity<Course>().Property(x => x.Title).HasMaxLength(200);

        modelBuilder.Entity<Course>().Property(x => x.FunPhoto).IsRequired();
        modelBuilder.Entity<Course>().Property(x => x.FunPhoto).HasMaxLength(1000);
        
        // modelBuilder.Entity<Course>().HasKey(x => new { x.Id, x.Title });
    }
}