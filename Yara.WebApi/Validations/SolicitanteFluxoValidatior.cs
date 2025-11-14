using FluentValidation;
using Yara.AppService.Dtos;

namespace Yara.WebApi.Validations
{
    public class SolicitanteFluxoValidatior : AbstractValidator<SolicitanteFluxoDto>
    {
        public SolicitanteFluxoValidatior()
        {
            //RuleFor(c => c.Usuario).NotNull().NotEmpty().WithMessage(Resources.Resources.requesterID);
            //RuleFor(c => c.Divisao).NotNull().NotEmpty().WithMessage(Resources.Resources.requesterDivisao);
            //RuleFor(c => c.ItemOrdemVenda).NotNull().NotEmpty().WithMessage(Resources.Resources.requesterItemOrdem);
            //RuleFor(c => c.OrdemVendaNumero).NotNull().NotEmpty().WithMessage(Resources.Resources.requesterNumeroOrdem);
        }
    }
}