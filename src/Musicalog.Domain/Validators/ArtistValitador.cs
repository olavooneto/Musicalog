using FluentValidation;
using Musicalog.Domain.Constants;
using Musicalog.Models.Dtos;

namespace Musicalog.Models.Validators
{
    public class ArtistValitador: AbstractValidator<ArtistDto>
    {
        public ArtistValitador()
        {
            RuleFor(x=>x.Name)
                .NotEmpty().WithMessage(AppContants.ValidatorStringNotEmpty)
                .NotNull().WithMessage(AppContants.ValidatorStringNotNull);

        }
    }
}
