using FluentValidation;
using Yara.AppService.Dtos;
#pragma warning disable 1591

namespace Yara.WebApi.Validations
{
    public class AnexoValidator : AbstractValidator<AnexoDto>
    {
        public AnexoValidator()
        {
            RuleFor(c => c.Descricao).MaximumLength(50).WithMessage(Resources.Resources.fieldLengthPattern50);
        }
    }
}