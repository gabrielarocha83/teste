using FluentValidation;
using Yara.AppService.Dtos;

namespace Yara.WebApi.Validations
{
    public class PermissaoValidator : AbstractValidator<PermissaoDto>
    {
        public PermissaoValidator()
        {
           // RuleFor(c=>c.Ativo).NotNull().NotEmpty().WithMessage(Resources.Resources.statusRequired);
            RuleFor(c => c.Nome).NotNull().NotEmpty().WithMessage(Resources.Resources.fieldNamePattern);
            RuleFor(c => c.Nome).MaximumLength(120).WithMessage(Resources.Resources.fieldLengthPattern120);
          
            //RuleFor(c => c.DataCriacao).NotNull().NotEmpty().WithMessage(Resources.Resources.dateRequired);
        }
    }
}