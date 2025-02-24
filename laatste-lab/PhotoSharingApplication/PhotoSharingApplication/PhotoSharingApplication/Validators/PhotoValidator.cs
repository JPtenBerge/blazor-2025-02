using FluentValidation;
using PhotoSharingApplication.Core.Models;

namespace PhotoSharingApplication.Validators;

public class PhotoValidator : AbstractValidator<Photo>
{
    public PhotoValidator()
    {
        RuleFor(photo => photo.Title).NotEmpty().MaximumLength(100);
        RuleFor(photo => photo.Description).MaximumLength(300);
    }
}
