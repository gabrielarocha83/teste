using FluentValidation;
using Yara.AppService.Dtos;

namespace Yara.WebApi.Validations
{
    /// <summary>
    /// 
    /// </summary>
    public class MediaSacaValidator : AbstractValidator<MediaSacaDto>
    {
        /// <summary>
        /// 
        /// </summary>
        public MediaSacaValidator()
        {
            RuleFor(c => c.Nome).NotNull().NotEmpty().WithMessage(Resources.Resources.fieldNamePattern);
            RuleFor(c => c.Nome).MaximumLength(50).WithMessage(Resources.Resources.fieldLengthPattern50);
            RuleFor(c => c.Peso).NotNull().NotEmpty().WithMessage(Resources.Resources.fieldWeightPattern);
            RuleFor(c => c.Valor).NotNull().NotEmpty().WithMessage(Resources.Resources.fieldAmountPattern);
        }
    }
}