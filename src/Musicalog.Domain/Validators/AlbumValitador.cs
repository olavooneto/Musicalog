using FluentValidation;
using Musicalog.Domain.Constants;
using Musicalog.Models.Dtos;

namespace Musicalog.Models.Validators
{
    public class AlbumValitador : AbstractValidator<AlbumDto>
    {
        public AlbumValitador()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage(AppContants.ValidatorStringNotEmpty)
                .NotNull().WithMessage(AppContants.ValidatorStringNotNull);

            RuleFor(x => x.AlbumType)
               .IsInEnum().WithMessage(AppContants.ValidatorIsInEnum);

            RuleFor(x => x.Artists)
                .NotEmpty().WithMessage(AppContants.ValidatorStringNotEmpty)
                .NotNull().WithMessage(AppContants.ValidatorStringNotNull);

        }
    }
}
