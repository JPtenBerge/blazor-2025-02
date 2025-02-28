using Microsoft.AspNetCore.Authorization;

namespace PhotoSharingApplication.Auth;

public static class PhotoSharingPolicies
{
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

    public static IServiceCollection AddPhotoSharingPolicies(this IServiceCollection services)
    {
        services.AddAuthorizationBuilder().RequireCommentOwner();
        services.AddSingleton<IAuthorizationHandler, CommentOwnerHandler>();
        return services;
    }
}
