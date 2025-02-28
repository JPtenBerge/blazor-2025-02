using PhotoSharingApplication.Client.Core.Models;

namespace PhotoSharingApplication.Core.Interfaces;

public interface ICommentsService
{
    Task<Comment> AddCommentAsync(Comment comment);
    Task DeleteCommentAsync(int id);
    Task<Comment?> GetCommentByIdAsync(int id);
    Task<List<Comment>> GetCommentsForPhotoAsync(int photoId);
    Task UpdateCommentAsync(Comment comment);
}