using Microsoft.AspNetCore.Authorization;
using PhotoSharingApplication.CommentsApi.Core.Models;

namespace PhotoSharingApplication.Auth;

public class CommentOwnerHandler : AuthorizationHandler<CommentOwnerRequirement, Comment>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CommentOwnerRequirement requirement, Comment resource)
    {
        System.Security.Claims.ClaimsPrincipal user = context.User;
        if (user.Identity.IsAuthenticated && user.Identity.Name == resource.Author)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
