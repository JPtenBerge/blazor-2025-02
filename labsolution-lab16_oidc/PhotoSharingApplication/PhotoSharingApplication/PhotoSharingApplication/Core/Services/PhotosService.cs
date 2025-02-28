using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using PhotoSharingApplication.Auth;
using PhotoSharingApplication.Core.Interfaces;
using PhotoSharingApplication.Core.Models;

namespace PhotoSharingApplication.Core.Services;

public class PhotosService : IPhotosService
{
    private readonly IPhotosRepository _photosRepository;
    private readonly IAuthorizationService _authorizationService;
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public PhotosService(IPhotosRepository photosRepository, IAuthorizationService authorizationService, AuthenticationStateProvider authenticationStateProvider)
    {
        _photosRepository = photosRepository;
        _authorizationService = authorizationService;
        _authenticationStateProvider = authenticationStateProvider;
    }

    public async Task<Photo> AddPhotoAsync(Photo photo)
    {
        var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        photo.Owner = authState?.User?.Identity?.Name ?? throw new UnauthorizedException();
        return await _photosRepository.AddPhotoAsync(photo);
    }

    public Task<Photo?> GetPhotoByIdAsync(int id)
    {
        return _photosRepository.GetPhotoByIdAsync(id);
    }
    public async Task<IEnumerable<Photo>> GetPhotosAsync()
    {
        return await _photosRepository.GetPhotosAsync();
    }
    public async Task DeletePhotoAsync(int id)
    {
        Photo photo = await _photosRepository.GetPhotoByIdAsync(id) ?? throw new Exception("Photo not found");
        var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        var User = authState?.User ?? throw new UnauthorizedException();
        AuthorizationResult result = await _authorizationService.AuthorizeAsync(User, photo, PhotoSharingPolicies.PhotoOwnerPolicy);
        if (!result.Succeeded)
        {
            throw new UnauthorizedException();
        }
        await _photosRepository.DeletePhotoAsync(id);
    }

    public async Task UpdatePhotoAsync(Photo photo)
    {
        var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        var User = authState?.User ?? throw new UnauthorizedException();
        AuthorizationResult result = await _authorizationService.AuthorizeAsync(User, photo, PhotoSharingPolicies.PhotoOwnerPolicy);
        if (!result.Succeeded)
        {
            throw new UnauthorizedException();
        }
        Photo? photoToUpdate = await _photosRepository.GetPhotoByIdAsync(photo.Id);
        if (photoToUpdate is not null)
        {
            await _photosRepository.UpdatePhotoAsync(photo);
        }
    }
}
