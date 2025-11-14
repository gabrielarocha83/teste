using FluentValidation;
using Yara.AppService.Dtos;

#pragma warning disable 1591

namespace Yara.WebApi.Validations
{
    public class FluxoAlcadaAprovacaoValidator : AbstractValidator<FluxoAlcadaAprovacaoDto>
    {
        public FluxoAlcadaAprovacaoValidator()
        {
            RuleFor(c => c.Nivel).NotNull().NotEmpty().WithMessage(Resources.Resources.flowLimitNivel);
            RuleFor(c => c.ValorDe).NotNull().NotNull().WithMessage(Resources.Resources.flowLimitDe);
            RuleFor(c => c.ValorAte).NotNull().NotEmpty().WithMessage(Resources.Resources.flowLimitAte);
        }
    }
}