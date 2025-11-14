using FluentValidation;
using Yara.AppService.Dtos;

namespace Yara.WebApi.Validations
{
    public class EstruturaComercialValidator : AbstractValidator<EstruturaComercialDiretoriaDto>
    {
        public EstruturaComercialValidator()
        {
            RuleFor(c => c.Nome).MaximumLength(50).WithMessage(Resources.Resources.fieldLengthPattern120);
            RuleFor(c => c.Nome).NotNull().NotEmpty().WithMessage(Resources.Resources.fieldNamePattern);
            RuleFor(c => c.Gerentes).Must(c=>c.Count > 0).WithMessage(Resources.Resources.ManagerField);

        }
    }
}