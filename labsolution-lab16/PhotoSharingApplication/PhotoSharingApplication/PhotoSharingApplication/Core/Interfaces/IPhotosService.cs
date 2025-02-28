using PhotoSharingApplication.Core.Models;

namespace PhotoSharingApplication.Core.Interfaces;

public interface IPhotosService
{
    Task<Photo> AddPhotoAsync(Photo photo);
    Task DeletePhotoAsync(int id);
    Task<Photo?> GetPhotoByIdAsync(int id);
    Task<IEnumerable<Photo>> GetPhotosAsync();
    Task UpdatePhotoAsync(Photo photo);
}