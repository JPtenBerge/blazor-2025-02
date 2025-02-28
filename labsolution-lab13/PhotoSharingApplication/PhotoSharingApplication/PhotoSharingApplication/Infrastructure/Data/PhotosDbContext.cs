using Microsoft.EntityFrameworkCore;
using PhotoSharingApplication.Client.Core.Models;
using PhotoSharingApplication.Core.Models;

namespace PhotoSharingApplication.Infrastructure.Data;

public class PhotosDbContext : DbContext
{
    public PhotosDbContext(DbContextOptions<PhotosDbContext> options) : base(options)
    {
        
    }

    public DbSet<Photo> Photos { get; set; }
    public DbSet<Comment> Comments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Photo>().Property(p => p.Title).HasMaxLength(100);
        modelBuilder.Entity<Photo>().Property(p => p.Description).HasMaxLength(300);
        modelBuilder.Entity<Photo>().Property(p => p.ImageMimeType).HasMaxLength(100);

        modelBuilder.Entity<Comment>().Property(c => c.Subject).HasMaxLength(100);
        modelBuilder.Entity<Comment>().Property(c => c.Body).HasMaxLength(300);
    }
}
