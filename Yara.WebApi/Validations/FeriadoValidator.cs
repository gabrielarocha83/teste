using FluentValidation;
using Yara.AppService.Dtos;

namespace Yara.WebApi.Validations
{
    public class FeriadoValidator : AbstractValidator<FeriadoDto>
    {
        public FeriadoValidator()
        {
            RuleFor(c => c.DataFeriado).NotNull().WithMessage(Resources.Resources.dateHoliday);
            RuleFor(c => c.Descricao).MaximumLength(50).WithMessage(Resources.Resources.fieldLengthPattern50);
        }
    }
}