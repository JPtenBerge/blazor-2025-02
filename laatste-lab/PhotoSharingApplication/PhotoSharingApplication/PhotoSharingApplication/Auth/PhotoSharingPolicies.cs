using Microsoft.AspNetCore.Authorization;
using PhotoSharingApplication.Core.Models;

namespace PhotoSharingApplication.Auth;

public static class PhotoSharingPolicies
{
    public const string PhotoOwnerPolicy = "PhotoOwner";
    public const string CommentOwnerPolicy = "CommentOwner";
}

public static class PhotoSharingPoliciesExtensions {
    public static AuthorizationBuilder RequireCommentOwner(this AuthorizationBuilder builder)
    {
        builder.AddPolicy(PhotoSharingPolicies.CommentOwnerPolicy, policy => { 
            policy.Requirements.Add(new CommentOwnerRequirement());
            policy.RequireAuthenticatedUser();
        });
        return builder;
    }

    public static AuthorizationBuilder RequirePhotoOwner(this AuthorizationBuilder builder)
    {
        //builder.AddPolicy(PhotoSharingPolicies.PhotoOwnerPolicy, policy =>
        //{
        //    policy.Requirements.Add(new PhotoOwnerRequirement());
        //    policy.RequireAuthenticatedUser();
        //});
        builder.AddPolicy(PhotoSharingPolicies.PhotoOwnerPolicy, policy =>
        {
            policy
            .RequireAuthenticatedUser()
            .RequireAssertion(context =>
            {
                if (context.Resource is not Photo photo)
                {
                    return false;
                }
                System.Security.Claims.ClaimsPrincipal user = context.User;
                return photo.Owner == user.Identity.Name;
            });
        });
        return builder;
    }

    public static IServiceCollection AddPhotoSharingPolicies(this IServiceCollection services)
    {
        services.AddAuthorizationBuilder().RequireCommentOwner().RequirePhotoOwner();

        services.AddSingleton<IAuthorizationHandler, CommentOwnerHandler>();
        services.AddSingleton<IAuthorizationHandler, PhotoOwnerHandler>();
        return services;
    }
}
