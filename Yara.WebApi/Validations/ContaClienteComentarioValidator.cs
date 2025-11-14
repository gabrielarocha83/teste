using FluentValidation;
using Yara.AppService.Dtos;
#pragma warning disable 1591

namespace Yara.WebApi.Validations
{
    public class ContaClienteComentarioValidator : AbstractValidator<ContaClienteComentarioDto>
    {
        public ContaClienteComentarioValidator()
        {
            RuleFor(c => c.ContaClienteID).NotNull().NotEmpty().WithMessage(Resources.Resources.contaclientecomentarioContaClienteID);
            RuleFor(c => c.Descricao).NotNull().NotEmpty().WithMessage(Resources.Resources.contaclientecomentarioDescricao);

            //RuleFor(c => c.Descricao).MaximumLength(255).WithMessage(Resources.Resources.contaclientecomentarioDescricao255);
            //RuleFor(c => c.Ativo).NotNull().NotEmpty().WithMessage(Resources.Resources.userName);
        }
    }
}