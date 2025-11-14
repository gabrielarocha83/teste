using FluentValidation;
using Yara.AppService.Dtos;

namespace Yara.WebApi.Validations
{
    /// <summary>
    /// Usuario Validator
    /// </summary>
    public class UsuarioValidator : AbstractValidator<UsuarioDto>
    {
        /// <summary>
        /// Construtor da classe Usuario
        /// </summary>
        public UsuarioValidator()
        {
            RuleFor(c => c.Nome).NotNull().NotEmpty().WithMessage(Resources.Resources.fieldNamePattern);
            RuleFor(c => c.Nome).MaximumLength(120).WithMessage(Resources.Resources.fieldLengthPattern120);
            RuleFor(c => c.Email).NotNull().NotEmpty().WithMessage(Resources.Resources.userEmail);
            RuleFor(c => c.Email).MaximumLength(120).WithMessage(Resources.Resources.fieldLengthPattern120);
            RuleFor(c => c.Login).NotNull().NotEmpty().WithMessage(Resources.Resources.userLogin);
            RuleFor(c => c.Login).MaximumLength(120).WithMessage(Resources.Resources.fieldLengthPattern120);
            RuleFor(c => c.TipoAcesso).NotNull().NotEmpty().IsInEnum().WithMessage(Resources.Resources.userType);
        }
    }
}