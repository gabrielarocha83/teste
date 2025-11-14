using FluentValidation;
using Yara.AppService.Dtos;

namespace Yara.WebApi.Validations
{
    /// <summary>
    /// 
    /// </summary>
    public class ProdutividadeMediaValidator : AbstractValidator<ProdutividadeMediaDto>
    {
        /// <summary>
        /// 
        /// </summary>
        public ProdutividadeMediaValidator()
        {
            RuleFor(c => c.Nome).NotNull().NotEmpty().WithMessage(Resources.Resources.fieldNamePattern);
            RuleFor(c => c.Nome).MaximumLength(50).WithMessage(Resources.Resources.fieldLengthPattern50);
        }
    }
}