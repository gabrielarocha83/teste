using FluentValidation;
using Yara.AppService.Dtos;

namespace Yara.WebApi.Validations
{
    /// <summary>
    /// 
    /// </summary>
    public class PropostaAlcadaComercialValidator : AbstractValidator<PropostaAlcadaComercialDto>
    {
        /// <summary>
        /// 
        /// </summary>
        public PropostaAlcadaComercialValidator()
        {
            RuleFor(c => c.ContaClienteID).NotNull().NotEmpty().WithMessage(Resources.Resources.accountclienteID);
            //RuleFor(c => c.TermoAceite).NotEqual(false).WithMessage(Resources.Resources.termValidate);
           // RuleFor(c => c.LCProposto).LessThanOrEqualTo(100000).WithMessage(Resources.Resources.mustProposal);
        }
    }
}