using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Yara.AppService.Dtos;

namespace Yara.WebApi.Validations
{
    public class LimiteYaraGalvaniValidator : AbstractValidator<LimiteYaraGalvaniDto>
    {
        public LimiteYaraGalvaniValidator()
        {
            RuleFor(c => c.LimiteGalvani).GreaterThanOrEqualTo(0).WithMessage("Limite Galvani não pode ser negativo.").NotNull().WithMessage("Limite Galvani não pode ser nulo.");
            RuleFor(c => c.LimiteYara).GreaterThanOrEqualTo(0).WithMessage("Limite Yara não pode ser negativo.").NotNull().WithMessage("Limite Yara não pode ser nulo.");
        }
    }
}