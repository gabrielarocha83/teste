using FluentValidation;
using Yara.AppService.Dtos;

namespace Yara.WebApi.Validations
{
    public class MotivoProrrogacaoValidator : AbstractValidator<MotivoProrrogacaoDto>
    {
        public MotivoProrrogacaoValidator()
        {
            RuleFor(c => c.Nome).NotNull().NotEmpty().WithMessage(Resources.Resources.nameRequired);
            RuleFor(c => c.Nome).MaximumLength(120).WithMessage(Resources.Resources.fieldLengthPattern2);
        }
    }
}