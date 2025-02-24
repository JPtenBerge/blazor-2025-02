using Microsoft.AspNetCore.Authorization;
using PhotoSharingApplication.Core.Models;

namespace PhotoSharingApplication.Auth;

public class PhotoOwnerHandler : AuthorizationHandler<PhotoOwnerRequirement, Photo>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PhotoOwnerRequirement requirement, Photo resource)
    {
        System.Security.Claims.ClaimsPrincipal user = context.User;
        if (user.Identity.IsAuthenticated && user.Identity.Name == resource.Owner)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
