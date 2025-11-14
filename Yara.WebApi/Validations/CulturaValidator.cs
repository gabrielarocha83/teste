using FluentValidation;
using Yara.AppService.Dtos;

namespace Yara.WebApi.Validations
{
    /// <summary>
    /// 
    /// </summary>
    public class CulturaValidator : AbstractValidator<CulturaDto>
    {
        /// <summary>
        /// 
        /// </summary>
        public CulturaValidator()
        {
            RuleFor(c => c.Descricao).NotNull().NotEmpty().WithMessage(Resources.Resources.fieldNamePattern);
            RuleFor(c => c.Descricao).MaximumLength(50).WithMessage(Resources.Resources.fieldLengthPattern50);

            RuleFor(c => c.UnidadeMedida).NotNull().NotEmpty().WithMessage(Resources.Resources.fieldNamePattern);
            RuleFor(c => c.UnidadeMedida).MaximumLength(10).WithMessage(Resources.Resources.fieldLengthPattern10);

        }
    }
}