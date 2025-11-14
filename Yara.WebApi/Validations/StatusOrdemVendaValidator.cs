using FluentValidation;
using Yara.AppService.Dtos;
#pragma warning disable 1591

namespace Yara.WebApi.Validations
{
    public class StatusOrdemVendaValidator : AbstractValidator<StatusOrdemVendasDto>
    {
        public StatusOrdemVendaValidator()
        {
            //RuleFor(c => c.Numero).NotNull().NotEmpty().WithMessage(Resources.Resources.statusNumberOrder);
            //RuleFor(c => c.Pagador).NotNull().NotEmpty().WithMessage(Resources.Resources.statusPagadorOrder);
            //RuleFor(c => c.BloqueioRemessa).NotNull().NotEmpty().WithMessage(Resources.Resources.statusBlockOrder);
        }
    }
}