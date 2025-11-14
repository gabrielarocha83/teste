using FluentValidation;
using Yara.AppService.Dtos;

namespace Yara.WebApi.Validations
{
    /// <summary>
    /// 
    /// </summary>
    public class PropostaLCAdicionalInserirValidator : AbstractValidator<PropostaLCAdicionalDto>
    {
        /// <summary>
        /// 
        /// </summary>
        public PropostaLCAdicionalInserirValidator()
        {
            RuleFor(c => c.AcompanharProposta).NotNull().NotEmpty().WithMessage(Resources.Resources.followProposalRequired);
            RuleFor(c => c.LCAdicional).NotNull().NotEmpty().WithMessage(Resources.Resources.valueProposalRequired);
            RuleFor(c => c.VigenciaAdicional).NotNull().NotEmpty().WithMessage(Resources.Resources.endDateProposalRequired);
            RuleFor(c => c.Parecer).NotNull().NotEmpty().WithMessage(Resources.Resources.seemProposalRequired);
        }
    }
}