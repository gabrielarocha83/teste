using FluentValidation;
using Yara.AppService.Dtos;
#pragma warning disable 1591

namespace Yara.WebApi.Validations
{
    public class ContaClienteBuscaBensValidator : AbstractValidator<ContaClienteBuscaBensDto>
    {
        public ContaClienteBuscaBensValidator()
        {
            RuleFor(c => c.Patrimonio).NotNull().NotEmpty().WithMessage(Resources.Resources.fieldParecer);
        }
    }
}