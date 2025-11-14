using FluentValidation;
using Yara.AppService.Dtos;

namespace Yara.WebApi.Validations
{
    /// <summary>
    /// 
    /// </summary>
    public class ReceitaValidator : AbstractValidator<ReceitaDto>
    {
        /// <summary>
        /// 
        /// </summary>
        public ReceitaValidator()
        {
            RuleFor(c => c.Descricao).NotNull().NotEmpty().WithMessage(Resources.Resources.fieldDescriptionPattern);
            RuleFor(c => c.Descricao).MaximumLength(50).WithMessage(Resources.Resources.fieldLengthPattern50);
        }
      
    }
}