using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using PhotoSharingApplication.Auth;
using PhotoSharingApplication.Core.Interfaces;
using PhotoSharingApplication.Core.Models;
using PhotoSharingApplication.Validators;
using System.Text.Json;

namespace PhotoSharingApplication.Core.Services;

public class PhotosService : IPhotosService
{
    private readonly IPhotosRepository _photosRepository;
    private readonly IAuthorizationService _authorizationService;
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly IDistributedCache _cache;
    private readonly IValidator<Photo> _photoValidator;

    public PhotosService(IPhotosRepository photosRepository, IAuthorizationService authorizationService, AuthenticationStateProvider authenticationStateProvider, IDistributedCache cache, IValidator<Photo> photoValidator)
    {
        _photosRepository = photosRepository;
        _authorizationService = authorizationService;
        _authenticationStateProvider = authenticationStateProvider;
        _cache = cache;
        _photoValidator = photoValidator;
    }

    public async Task<Photo> AddPhotoAsync(Photo photo)
    {
        var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        photo.Owner = authState?.User?.Identity?.Name ?? throw new UnauthorizedException();
        _photoValidator.ValidateAndThrow(photo);
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
        AuthorizationResult result = await _authorizationService.AuthorizeAsync(User, photo, "PhotoOwner");
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
        AuthorizationResult result = await _authorizationService.AuthorizeAsync(User, photo, "PhotoOwner");
        if (!result.Succeeded)
        {
            throw new UnauthorizedException();
        }
        Photo? photoToUpdate = await _photosRepository.GetPhotoByIdAsync(photo.Id);
        if (photoToUpdate is not null)
        {
            _photoValidator.ValidateAndThrow(photo);
            await _photosRepository.UpdatePhotoAsync(photo);
        }
    }

    public async Task<Photo?> GetPhotoWithImageByIdAsync(int id)
    {
        return await _photosRepository.GetPhotoWithImageByIdAsync(id);
    }

    public async Task<PhotoImage?> GetPhotoImageByIdAsync(int id)
    {
        string key = $"photo-image-{id}";
        byte[]? encodedPhotoImage = await _cache.GetAsync(key);
        PhotoImage? photoImage;
        if (encodedPhotoImage is null)
        {
            photoImage = await _photosRepository.GetPhotoImageByIdAsync(id);
            await _cache.SetAsync(key, JsonSerializer.SerializeToUtf8Bytes(photoImage));
        } else
        {
            photoImage = JsonSerializer.Deserialize<PhotoImage>(encodedPhotoImage);
        }
        return photoImage;
    }
}
