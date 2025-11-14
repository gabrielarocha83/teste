using FluentValidation;
using Yara.AppService.Dtos;

namespace Yara.WebApi.Validations
{
    public class StatusCobrancaValidator : AbstractValidator<StatusCobrancaDto>
    {
        public StatusCobrancaValidator()
        {
            RuleFor(c => c.Descricao).NotNull().NotEmpty().WithMessage(Resources.Resources.fieldNamePattern);
        }
    }
}