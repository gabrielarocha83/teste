using FluentValidation;
using Yara.AppService.Dtos;

namespace Yara.WebApi.Validations
{
    /// <summary>
    /// 
    /// </summary>
    public class ProdutoServicoValidator : AbstractValidator<ProdutoServicoDto>
    {
        /// <summary>
        /// 
        /// </summary>
        public ProdutoServicoValidator()
        {
            RuleFor(c => c.Nome).NotNull().NotEmpty().WithMessage(Resources.Resources.fieldNamePattern);
            RuleFor(c => c.Nome).MaximumLength(50).WithMessage(Resources.Resources.fieldLengthPattern50);
        }
    }
}