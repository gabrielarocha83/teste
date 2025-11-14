using FluentValidation;
using Yara.AppService.Dtos;

namespace Yara.WebApi.Validations
{
    /// <summary>
    /// 
    /// </summary>
    public class TipoEmpresaValidator : AbstractValidator<TipoEmpresaDto>
    {
        /// <summary>
        /// 
        /// </summary>
        public TipoEmpresaValidator()
        {
            RuleFor(c => c.Tipo).NotNull().NotEmpty().WithMessage(Resources.Resources.fieldNamePattern);
            RuleFor(c => c.Tipo).MaximumLength(50).WithMessage(Resources.Resources.fieldLengthPattern50);
        }
    }
}