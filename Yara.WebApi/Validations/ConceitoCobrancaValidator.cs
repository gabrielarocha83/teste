using FluentValidation;
using Yara.AppService.Dtos;

namespace Yara.WebApi.Validations
{
    public class ConceitoCobrancaValidator : AbstractValidator<ConceitoCobrancaDto>
    {
        public ConceitoCobrancaValidator()
        {
            RuleFor(c => c.Nome).NotNull().NotEmpty().WithMessage(Resources.Resources.nameRequired);
            RuleFor(c => c.Nome).MaximumLength(2).WithMessage(Resources.Resources.fieldLengthPattern2);
            // RuleFor(c => c.Ativo).NotNull().NotEmpty().WithMessage(Resources.Resources.statusRequired);
        }

    }
}