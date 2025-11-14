using FluentValidation;
using Yara.AppService.Dtos;

namespace Yara.WebApi.Validations
{
    public class FluxoLiberacaoManualValidator : AbstractValidator<FluxoLiberacaoManualDto>
    {
        public FluxoLiberacaoManualValidator()
        {
            RuleFor(c => c.Nivel).NotNull().NotEmpty().WithMessage(Resources.Resources.flowLimitNivel);
            RuleFor(c => c.ValorDe).NotNull().NotNull().WithMessage(Resources.Resources.flowLimitDe);
            RuleFor(c => c.ValorAte).NotNull().NotEmpty().WithMessage(Resources.Resources.flowLimitAte);
        }
    }
}