using FluentValidation;
using Yara.AppService.Dtos;
#pragma warning disable 1591

namespace Yara.WebApi.Validations
{
    public class GrupoEconomicoMembrosValidator : AbstractValidator<GrupoEconomicoMembrosDto>
    {
        public GrupoEconomicoMembrosValidator()
        {
            RuleFor(c => c.ContaClienteID).NotNull().NotEmpty().WithMessage(Resources.Resources.statusPagadorOrder);
            RuleFor(c => c.GrupoEconomicoID).NotNull().NotEmpty().WithMessage(Resources.Resources.codGrupoEconomicoId);
        }
    }
}