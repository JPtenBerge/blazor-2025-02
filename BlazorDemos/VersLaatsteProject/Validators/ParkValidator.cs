using FluentValidation;

namespace VersLaatsteProject.Validators;

public class ParkValidator : AbstractValidator<Park>
{
    public ParkValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Naam is verplicht");

        When(x => x.Rating >= 8, () => { RuleFor(x => x.PhotoUrl).NotEmpty().WithMessage("Foto URL is verplicht"); });
        When(x => !string.IsNullOrEmpty(x.PhotoUrl),
            () =>
            {
                RuleFor(x => x.PhotoUrl).Matches(@"^https?:\/\/.+\/.+\.(jpg|gif|png)$")
                    .WithMessage("Dat is geen URL dude");
            });

        RuleFor(x => x.Location).NotEmpty().WithMessage("Locatie is verplicht");
        RuleFor(x => x.Rating).NotEmpty().WithMessage("Oordeel is verplicht");
        RuleFor(x => x.Rating).InclusiveBetween(1, 10).WithMessage("Tussen 1 en 10 graag");
        RuleFor(x => x.NrOfVisitors).NotEmpty().WithMessage("Aantal bezoekers is verplicht");
    }
}