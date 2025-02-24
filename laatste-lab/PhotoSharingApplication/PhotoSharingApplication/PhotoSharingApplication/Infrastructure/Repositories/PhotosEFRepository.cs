using Microsoft.EntityFrameworkCore;
using PhotoSharingApplication.Core.Interfaces;
using PhotoSharingApplication.Core.Models;
using PhotoSharingApplication.Infrastructure.Data;

namespace PhotoSharingApplication.Infrastructure.Repositories;

public class PhotosEFRepository(PhotosDbContext context) : IPhotosRepository
{
    public async Task<Photo> AddPhotoAsync(Photo photo)
    {
        context.Photos.Add(photo);
        await context.SaveChangesAsync();
        return photo;
    }

    public async Task<Photo?> DeletePhotoAsync(int id)
    {
        Photo? photo = context.Photos.Include(p=>p.PhotoImage).FirstOrDefault(p => p.Id == id);
        if (photo is not null)
        {
            context.Photos.Remove(photo);
            await context.SaveChangesAsync();
        }
        return photo;
    }

    public async Task<Photo?> GetPhotoByIdAsync(int id)
    {
        return await context.Photos.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<PhotoImage?> GetPhotoImageByIdAsync(int id)
    {
        return await context.PhotoImages.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Photo>> GetPhotosAsync()
    {
        return await context.Photos.AsNoTracking().ToListAsync();
    }

    public async Task<Photo?> GetPhotoWithImageByIdAsync(int id)
    {
        return await context.Photos.Include(p=>p.PhotoImage).AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task UpdatePhotoAsync(Photo photo)
    {
        context.Photos.Update(photo);
        await context.SaveChangesAsync();   
    }
}
