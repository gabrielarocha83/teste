using System.Linq;
using FluentValidation;
using Yara.AppService.Dtos;

namespace Yara.WebApi.Validations
{
    /// <summary>
    /// 
    /// </summary>
    public class PropostaProrrogacaoInserirValidator : AbstractValidator<PropostaProrrogacaoInserirDto>
    {
        /// <summary>
        /// 
        /// </summary>
        public PropostaProrrogacaoInserirValidator()
        {
            RuleFor(c => c.ContaClienteID).NotNull().NotEmpty().WithMessage(Resources.Resources.accountclienteID);
            RuleFor(c => c.NovoVencimento).NotNull().NotEmpty().WithMessage(Resources.Resources.NovoVencimentoValid);
            RuleFor(c => c.Titulos).Must(c => c.Any()).WithMessage(Resources.Resources.PaymentCount);
        }
    }
}