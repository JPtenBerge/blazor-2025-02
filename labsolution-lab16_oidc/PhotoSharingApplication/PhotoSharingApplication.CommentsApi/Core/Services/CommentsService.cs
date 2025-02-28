using Microsoft.AspNetCore.Authorization;
using PhotoSharingApplication.Auth;
using PhotoSharingApplication.CommentsApi.Core.Interfaces;
using PhotoSharingApplication.CommentsApi.Core.Models;

namespace PhotoSharingApplication.CommentsApi.Core.Services;

public class CommentsService(ICommentsRepository commentsRepository, IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService) : ICommentsService
{
    public async Task<Comment?> GetCommentByIdAsync(int id)
    {
        return await commentsRepository.GetCommentByIdAsync(id);
    }
    public async Task<List<Comment>> GetCommentsForPhotoAsync(int photoId)
    {
        return await commentsRepository.GetCommentsForPhotoAsync(photoId);
    }
    public async Task DeleteCommentAsync(int id)
    {
        Comment comment = await commentsRepository.GetCommentByIdAsync(id) ?? throw new Exception("Comment not found");
        AuthorizationResult result = await authorizationService.AuthorizeAsync(httpContextAccessor.HttpContext.User, comment, PhotoSharingPolicies.CommentOwnerPolicy);
        if (!result.Succeeded)
        {
            throw new UnauthorizedException();
        }
        await commentsRepository.DeleteCommentAsync(id);
    }
    public async Task<Comment> AddCommentAsync(Comment comment)
    {
        comment.Author = httpContextAccessor.HttpContext?.User?.Identity?.Name ?? throw new UnauthorizedException();
        return await commentsRepository.AddCommentAsync(comment);
    }
    public async Task UpdateCommentAsync(Comment comment)
    {
        AuthorizationResult result = await authorizationService.AuthorizeAsync(httpContextAccessor.HttpContext.User, comment, PhotoSharingPolicies.CommentOwnerPolicy);
        if (!result.Succeeded)
        {
            throw new UnauthorizedException();
        }
        await commentsRepository.UpdateCommentAsync(comment);
    }
}
