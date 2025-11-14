using FluentValidation;
using Yara.AppService.Dtos;

namespace Yara.WebApi.Validations
{
    /// <summary>
    /// 
    /// </summary>
    public class ConceitoCobrancaLiberacaoLogValidation : AbstractValidator<ConceitoCobrancaLiberacaoLogDto>
    {
        /// <summary>
        /// 
        /// </summary>
        public ConceitoCobrancaLiberacaoLogValidation()
        {
            RuleFor(c => c.ComentarioStatus).MaximumLength(500).WithMessage("A justificativa deve ter 500 caracteres no máximo.");
            RuleFor(c => c.ComentarioStatus).NotNull().NotEmpty().WithMessage(Resources.Resources.chargesLogMessage);
        }
    }
}