using FluentValidation;
using Yara.AppService.Dtos;

namespace Yara.WebApi.Validations
{
    /// <summary>
    /// 
    /// </summary>
    public class CulturaEstadoValidator : AbstractValidator<CulturaEstadoDto>
    {
        /// <summary>
        /// 
        /// </summary>
        public CulturaEstadoValidator()
        {
            RuleFor(c => c.CulturaID).NotNull().NotEmpty().WithMessage(Resources.Resources.culturevalidation);
            RuleFor(c => c.EstadoID).NotNull().NotEmpty().WithMessage(Resources.Resources.statevalidation);

            RuleFor(c => c.MediaFertilizante).NotNull().NotEmpty().WithMessage(Resources.Resources.mediafertilizantevalidation);
            RuleFor(c => c.PorcentagemFertilizanteCusto).NotEmpty().WithMessage(Resources.Resources.porcentagemfertilizante);
            RuleFor(c => c.Preco).NotEmpty().WithMessage(Resources.Resources.PrecoValidation);

            RuleFor(c => c.ProdutividadeMedia).NotEmpty().WithMessage(Resources.Resources.produtividademediavalidation);
          
        }
    }
}