using System.Linq;
using FluentValidation;
using Yara.AppService.Dtos;

namespace Yara.WebApi.Validations
{
    /// <summary>
    /// 
    /// </summary>
    public class PropostaAbonoInserirValidator : AbstractValidator<PropostaAbonoInserirDto>
    {
        /// <summary>
        /// 
        /// </summary>
        public PropostaAbonoInserirValidator()
        {
            RuleFor(c => c.ContaClienteID).NotNull().NotEmpty().WithMessage(Resources.Resources.accountclienteID);
            RuleFor(c => c.Titulos).Must(c => c.Any()).WithMessage(Resources.Resources.PaymentCount);
        }
    }
}