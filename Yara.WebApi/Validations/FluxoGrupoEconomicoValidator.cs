using FluentValidation;
using Yara.AppService.Dtos;
#pragma warning disable 1591

namespace Yara.WebApi.Validations
{
    public class FluxoGrupoEconomicoValidator : AbstractValidator<FluxoGrupoEconomicoDto>
    {
        public FluxoGrupoEconomicoValidator()
        {
            RuleFor(c => c.Nivel).NotNull().NotEmpty().WithMessage(Resources.Resources.flowLimitNivel);
        }
    }
}