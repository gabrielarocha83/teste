using FluentValidation;
using Yara.AppService.Dtos;

namespace Yara.WebApi.Validations
{
    public class TipoGarantiaValidator : AbstractValidator<TipoGarantiaDto>
    {
        public TipoGarantiaValidator()
        {
            RuleFor(c => c.Nome).NotNull().NotEmpty().WithMessage(Resources.Resources.fieldNamePattern);
            RuleFor(c => c.Nome).MaximumLength(120).WithMessage(Resources.Resources.tipogarantiaNome120);
         
        }
    }
}