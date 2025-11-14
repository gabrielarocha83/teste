using FluentValidation;
using Yara.AppService.Dtos;
#pragma warning disable 1591

namespace Yara.WebApi.Validations
{
    public class BloqueioManualContaClienteValidator : AbstractValidator<BloqueioManualContaClienteDto>
    {
        public BloqueioManualContaClienteValidator()
        {
            RuleFor(c => c.Nome).NotNull().NotEmpty().WithMessage(Resources.Resources.contaclienteNome);
            RuleFor(c => c.Nome).MaximumLength(120).WithMessage(Resources.Resources.contaclienteNome120);
           
        }
    }
}