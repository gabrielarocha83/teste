using FluentValidation;
using Yara.AppService.Dtos;
#pragma warning disable 1591

namespace Yara.WebApi.Validations
{
    public class ContaClienteTelefoneValidator : AbstractValidator<ContaClienteTelefoneDto>
    {
        public ContaClienteTelefoneValidator()
        {
            RuleFor(c => c.ContaClienteID).NotNull().NotEmpty().WithMessage(Resources.Resources.contaclienteNome);
            RuleFor(c => c.Telefone).NotNull().NotEmpty().WithMessage(Resources.Resources.contaclienteTelefone);
            RuleFor(c => c.Telefone).MaximumLength(24).WithMessage(Resources.Resources.fieldLengthPattern24);
            RuleFor(c => c.Tipo).NotNull().NotEmpty().WithMessage(Resources.Resources.userType);
            RuleFor(c => c.Tipo).IsInEnum().WithMessage(Resources.Resources.userType);
        }
    }
}