using FluentValidation;
using Yara.AppService.Dtos;
#pragma warning disable 1591

namespace Yara.WebApi.Validations
{
    public class ContaClienteVisitaValidator : AbstractValidator<ContaClienteVisitaDto>
    {
        public ContaClienteVisitaValidator()
        {
            RuleFor(c => c.Parecer).NotNull().NotEmpty().WithMessage(Resources.Resources.fieldParecer);
        }
    }
}