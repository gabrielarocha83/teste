using FluentValidation;
using Yara.AppService.Dtos;

namespace Yara.WebApi.Validations
{
    public class TipoClienteValidator : AbstractValidator<TipoClienteDto>
    {
        public TipoClienteValidator()
        {
            RuleFor(c => c.Nome).NotNull().NotEmpty().WithMessage(Resources.Resources.fieldNamePattern);
            RuleFor(c => c.Nome).MaximumLength(120).WithMessage(Resources.Resources.fieldLengthPattern120);
            //RuleFor(c => c.DataCriacao).NotNull().NotEmpty().WithMessage(Resources.Resources.dateRequired);
        }
    }
}