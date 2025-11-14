using FluentValidation;
using Yara.AppService.Dtos;

namespace Yara.WebApi.Validations
{
    /// <summary>
    /// 
    /// </summary>
    public class ParametroSistemaValidator : AbstractValidator<ParametroSistemaDto>
    {
        /// <summary>
        /// 
        /// </summary>
        public ParametroSistemaValidator()
        {
            RuleFor(c => c.Grupo).NotNull().NotEmpty().WithMessage(Resources.Resources.unidademedidaculturaNome);
            RuleFor(c => c.Tipo).NotNull().NotEmpty().WithMessage(Resources.Resources.unidademedidaculturaNome);
            //RuleFor(c => c.Chave).NotNull().NotEmpty().WithMessage(Resources.Resources.unidademedidaculturaNome);
            RuleFor(c => c.Valor).NotNull().NotEmpty().WithMessage(Resources.Resources.unidademedidaculturaNome);
        }
    }
}