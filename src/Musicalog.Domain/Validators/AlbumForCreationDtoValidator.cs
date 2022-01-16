using FluentValidation;
using Musicalog.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Musicalog.Domain.Constants;
using Musicalog.Models.Dtos;

namespace Musicalog.Models.Validators
{
    public class AlbumForCreationDtoValidator : AbstractValidator<AlbumForCreationUpdateDto>
    {
        public AlbumForCreationDtoValidator ()
        {
            RuleFor(x=>x.Title)
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
