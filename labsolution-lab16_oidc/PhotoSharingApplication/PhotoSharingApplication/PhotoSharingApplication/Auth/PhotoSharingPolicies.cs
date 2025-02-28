using Microsoft.AspNetCore.Authorization;

namespace PhotoSharingApplication.Auth;

public static class PhotoSharingPolicies
{
    public const string PhotoOwnerPolicy = "PhotoOwner";
}

public static class PhotoSharingPoliciesExtensions {
    public static AuthorizationBuilder RequirePhotoOwner(this AuthorizationBuilder builder)
    {
        builder.AddPolicy(PhotoSharingPolicies.PhotoOwnerPolicy, policy =>
        {
            policy.Requirements.Add(new PhotoOwnerRequirement());
            policy.RequireAuthenticatedUser();
        });
        return builder;
    }

    public static IServiceCollection AddPhotoSharingPolicies(this IServiceCollection services)
    {
        services.AddAuthorizationBuilder().RequirePhotoOwner();

        services.AddSingleton<IAuthorizationHandler, PhotoOwnerHandler>();
        return services;
    }
}
