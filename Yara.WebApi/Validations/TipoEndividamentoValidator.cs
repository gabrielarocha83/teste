using FluentValidation;
using Yara.AppService.Dtos;

namespace Yara.WebApi.Validations
{
    /// <summary>
    /// 
    /// </summary>
    public class TipoEndividamentoValidator : AbstractValidator<TipoEndividamentoDto>
    {
        /// <summary>
        /// 
        /// </summary>
        public TipoEndividamentoValidator()
        {
            RuleFor(c => c.Tipo).NotNull().NotEmpty().WithMessage(Resources.Resources.fieldNamePattern);
            RuleFor(c => c.Tipo).MaximumLength(50).WithMessage(Resources.Resources.fieldLengthPattern50);
        }
    }
}