using FluentValidation;
using Yara.AppService.Dtos;

namespace Yara.WebApi.Validations
{
    public class TipoPecuariaValidator : AbstractValidator<TipoPecuariaDto>
    {
        public TipoPecuariaValidator()
        {
            RuleFor(c => c.Tipo).NotNull().NotEmpty().WithMessage(Resources.Resources.fieldNamePattern);
            RuleFor(c => c.Tipo).MaximumLength(120).WithMessage(Resources.Resources.fieldLengthPattern120);
            RuleFor(c => c.UnidadeMedida).NotNull().NotEmpty().WithMessage(Resources.Resources.unidademedidaculturaNome);
        }
    }
}