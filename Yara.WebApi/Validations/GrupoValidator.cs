using FluentValidation;
using Yara.AppService.Dtos;

namespace Yara.WebApi.Validations
{
    public class GrupoValidator : AbstractValidator<GrupoDto>
    {
        public GrupoValidator()
        {
            RuleFor(c => c.Nome).NotNull().NotEmpty().WithMessage(Resources.Resources.fieldNamePattern);
            RuleFor(c => c.Nome).MaximumLength(120).WithMessage(Resources.Resources.fieldLengthPattern120);
        }
    }
}