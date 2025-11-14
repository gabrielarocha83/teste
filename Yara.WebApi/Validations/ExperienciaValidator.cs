using FluentValidation;
using Yara.AppService.Dtos;

namespace Yara.WebApi.Validations
{
    /// <summary>
    /// 
    /// </summary>
    public class ExperienciaValidator : AbstractValidator<ExperienciaDto>
    {
        /// <summary>
        /// 
        /// </summary>
        public ExperienciaValidator()
        {
            RuleFor(c => c.Descricao).NotNull().NotEmpty().WithMessage(Resources.Resources.fieldNamePattern);
            RuleFor(c => c.Descricao).MaximumLength(50).WithMessage(Resources.Resources.fieldLengthPattern50);
        }
    }
}